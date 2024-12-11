// See https://aka.ms/new-console-template for more information
using DotNet_Batch14PKK.Share;

Console.WriteLine("Hello, World!");

IBlogServices blogServices = new BlogServices();
blogServices.CreateBlog(new BlogModel
{
    BlogId = Guid.NewGuid().ToString(),
    BlogAuthor = "author",
    BlogTitle = "title",
    BlogContent = "content",
});