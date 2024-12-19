using DotNet_Batch14PKK.Share;
using System.Runtime.CompilerServices;

namespace DotNetBatch14PKK.MinimalAPi.Features.Blog
{
    public static class BlogEndPoint
    {
        public static IEndpointRouteBuilder MapBlogEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Blog", () =>
            {
                EfcoreSerives blogService = new EfcoreSerives();
                var models = blogService.GetBlogs();
                return Results.Ok(models);
            })
            .WithName("GetBlogs")
            .WithOpenApi();
            app.MapPost("/api/Blog/{id}", (string id) =>
            {
                EfcoreSerives blogService = new EfcoreSerives();
                var model = blogService.GetBlog(id);
                return Results.Ok(model);
            })
            .WithName("GetBlog")
            .WithOpenApi();
            app.MapPost("/api/Blog/{id}", (BlogModel blog) =>
            {
                EfcoreSerives blogService = new EfcoreSerives();
                var model = blogService.CreateBlog(blog);
                return Results.Ok(model);
            })
            .WithName("CreateBlog")
            .WithOpenApi();
            app.MapPatch("/api/Blog/{id}", static (string id, BlogModel blog) =>
            {
                blog.BlogId = id;
                EfcoreSerives blogService = new EfcoreSerives();
                var model = blogService.UpdateBlog(blog);
                return Results.Ok(model);
            })
            .WithName("UpdateBlog")
            .WithOpenApi();
            app.MapDelete("/api/Blog/{id}", static (string id) =>
            {
                EfcoreSerives blogService = new EfcoreSerives();
                var model = blogService.DeleteBlog(id);
                return Results.Ok(model);
            })
            .WithName("DeleteBlog")
            .WithOpenApi();
            return app;
        }
        public static void UseUserEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Blog", () =>
            {
                EfcoreSerives blogService = new EfcoreSerives();
                var models = blogService.GetBlogs();
                return Results.Ok(models);
            })
            .WithName("GetBlogs")
            .WithOpenApi();
            app.MapPost("/api/Blog/{id}", (string id) =>
            {
                EfcoreSerives blogService = new EfcoreSerives();
                var model = blogService.GetBlog(id);
                return Results.Ok(model);
            })
            .WithName("GetBlog")
            .WithOpenApi();
            app.MapPost("/api/Blog/{id}", (BlogModel blog) =>
            {
                EfcoreSerives blogService = new EfcoreSerives();
                var model = blogService.CreateBlog(blog);
                return Results.Ok(model);
            })
            .WithName("CreateBlog")
            .WithOpenApi();
            app.MapPatch("/api/Blog/{id}", static (string id, BlogModel blog) =>
            {
                blog.BlogId = id;
                EfcoreSerives blogService = new EfcoreSerives();
                var model = blogService.UpdateBlog(blog);
                return Results.Ok(model);
            })
            .WithName("UpdateBlog")
            .WithOpenApi();
            app.MapDelete("/api/Blog/{id}", static (string id) =>
            {
                EfcoreSerives blogService = new EfcoreSerives();
                var model = blogService.DeleteBlog(id);
                return Results.Ok(model);
            })
            .WithName("DeleteBlog")
            .WithOpenApi();
        }
    }
}
