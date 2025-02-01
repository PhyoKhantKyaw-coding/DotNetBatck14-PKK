using Dapper;
using DotNetBatch14PKK.Login.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.Login
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDbConnection _dbConnection;

        public AuthenticationMiddleware(RequestDelegate next, IDbConnection dbConnection)
        {
            _next = next;
            _dbConnection = dbConnection;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get token from request headers
            string? encryptedToken = context.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(encryptedToken))
            {
                await RespondWithUnauthorized(context, "Unauthorized: Token is missing.");
                return;
            }

            try
            {
                // Decrypt token to get TokenModel object
                string decryptedTokenJson = encryptedToken.ToDecrypt();
                TokenModel? tokenModel = decryptedTokenJson.ToObject<TokenModel>();

                if (tokenModel == null || tokenModel.ExpireTime < DateTime.Now)
                {
                    await RespondWithUnauthorized(context, "Unauthorized: Token expired.");
                    return;
                }

                // Get user role from database using Dapper
                string roleQuery = "SELECT RoleID, RoleName FROM Role WHERE RoleCode = @RoleCode";
                var role = await _dbConnection.QueryFirstOrDefaultAsync<RoleModel>(roleQuery, new { RoleCode = tokenModel.UserRoleCode });

                if (role == null)
                {
                    await RespondWithForbidden(context, "Forbidden: Role not found.");
                    return;
                }

                // Get permissions using Dapper
                string permissionQuery = @"
                    SELECT p.UserPermissionID, p.FeatureID
                    FROM UserPermission up
                    INNER JOIN UserPermission p ON up.FeatureID = p.UserPermissionID
                    WHERE up.RoleID = @RoleID";

                var permissions = await _dbConnection.QueryAsync(permissionQuery, new { RoleID = role.RoleID });

                // Create response model
                var responseModel = new ResponseModel
                {
                    Success = true,
                    Message = "User authenticated successfully",
                    Data = new
                    {
                        UserId = tokenModel.UserId,
                        UserName = tokenModel.UserName,
                        Email = tokenModel.Email,
                        Role = role.RoleName,
                        Permissions = permissions
                    }
                };

                // Attach user details to the request for further processing
                context.Items["User"] = responseModel;

                // Continue to next middleware or endpoint
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
            await context.Response.WriteAsJsonAsync(new ResponseModel
            {
                Success = false,
                Message = message,
                Data = null
            });
        }

        private async Task RespondWithForbidden(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new ResponseModel
            {
                Success = false,
                Message = message,
                Data = null
            });
        }

        private async Task RespondWithError(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ResponseModel
            {
                Success = false,
                Message = $"Internal Server Error: {message}",
                Data = null
            });
        }
    }
}
