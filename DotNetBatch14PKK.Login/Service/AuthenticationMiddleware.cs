using Dapper;
using DotNetBatch14PKK.Login.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.Login
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly string[] allowList = new string[] { "/api/login/login", "/api/login/register" };

        public AuthenticationMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string endpoint = context.Request.Path.ToString().ToLower();
            if (allowList.Contains(endpoint))
            {
                await _next(context);
                return;
            }

            string? encryptedToken = context.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(encryptedToken))
            {
                await RespondWithUnauthorized(context, "Unauthorized: Token is missing.");
                return;
            }

            using var scope = _scopeFactory.CreateScope();
            var dbConnection = scope.ServiceProvider.GetRequiredService<IDbConnection>();

            try
            {
                string decryptedTokenJson = encryptedToken.ToDecrypt();
                TokenModel? tokenModel = decryptedTokenJson.ToObject<TokenModel>();

                if (tokenModel == null || tokenModel.ExpireTime < DateTime.Now)
                {
                    await RespondWithUnauthorized(context, "Unauthorized: Token expired.");
                    return;
                }

                string roleQuery = "SELECT Id as RoleID, Name as RoleName FROM Roles WHERE Code = @RoleCode";
                var role = await dbConnection.QueryFirstOrDefaultAsync<RoleModel>(roleQuery, new { RoleCode = tokenModel.UserRoleCode });

                if (role == null)
                {
                    await RespondWithForbidden(context, "Forbidden: Role not found.");
                    return;
                }

                string permissionQuery = @"
                    SELECT P.Id as PermissionID, F.Id as FeatureID, F.Name as FeatureName, F.Endpoint
                    FROM Permissions P
                    INNER JOIN Features F ON P.FeatureID = F.Id
                    WHERE P.RoleID = @RoleID AND F.Endpoint = @Endpoint";

                var permission = await dbConnection.QueryFirstOrDefaultAsync<UserPermissionModel>(permissionQuery, new { RoleID = role.RoleID, Endpoint = endpoint });

                if (permission == null)
                {
                    await RespondWithUnauthorized(context, "Unauthorized: Access denied.");
                    return;
                }

                context.Items["User"] = new
                {
                    UserId = tokenModel.UserId,
                    UserName = tokenModel.UserName,
                    Email = tokenModel.Email,
                    Role = role.RoleName,
                    Permissions = permission
                };
                await _next(context);
            }
            catch (Exception ex)
            {
                await RespondWithError(context, ex.Message);
            }
        }

        private async Task RespondWithUnauthorized(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new ResponseModel { Success = false, Message = message });
        }

        private async Task RespondWithForbidden(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new ResponseModel { Success = false, Message = message });
        }

        private async Task RespondWithError(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ResponseModel { Success = false, Message = $"Internal Server Error: {message}" });
        }
    }
}
