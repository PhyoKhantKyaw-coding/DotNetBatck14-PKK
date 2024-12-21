using DotNetBatch14PKK.RepoDbShared;

namespace DotNetBatch14PKK.RepoMinimalApi.Features
{
    public static class RepoEndPoint
    {
        public static IEndpointRouteBuilder MapRepoEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Blog", () =>
            {
                RepoService blogService = new RepoService();
                var models = blogService.GetBlogs();
                return Results.Ok(models);
            })
            .WithName("GetBlogs")
            .WithOpenApi();
            app.MapPost("/api/Blog", (string id) =>
            {
                RepoService blogService = new RepoService();
                var model = blogService.GetBlog(id);
                return Results.Ok(model);
            })
            .WithName("GetBlog")
            .WithOpenApi();
            app.MapPost("/api/Blog/{id}", (BlogModel blog) =>
            {
                RepoService blogService = new RepoService();
                var model = blogService.Create(blog.BlogTitle, blog.BlogAuthor, blog.BlogContent);
                return Results.Ok(model);
            })
            .WithName("CreateBlog")
            .WithOpenApi();
            app.MapPatch("/api/Blog/{id}", static (string id, BlogModel blog) =>
            {
                blog.BlogId = id;
                RepoService blogService = new RepoService();
                var model = blogService.Update(blog);
                return Results.Ok(model);
            })
            .WithName("UpdateBlog")
            .WithOpenApi();
            app.MapDelete("/api/Blog/{id}", static (string id) =>
            {
                RepoService blogService = new RepoService();
                var model = blogService.DeleteBlog(id);
                return Results.Ok(model);
            })
            .WithName("DeleteBlog")
            .WithOpenApi();
            return app;
        }
    }
}
