using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.ConsoleApp2.EfCoreExamples;

internal class EfCoreExample
{
    private readonly AppDbContext _db = new AppDbContext();
    public void Read()
    {
        var blogs = _db.Blogs.ToList();
        foreach (var blog in blogs)
        {
            Console.WriteLine(blog.Id);
            Console.WriteLine(blog.Title);
            Console.WriteLine(blog.Author);
            Console.WriteLine(blog.Content);
        }
    }

    public void Edit(string id)
    {
        var blog = _db.Blogs.FirstOrDefault(x => x.Id == id);
        if (blog is null)
        {
            Console.WriteLine("No data found.");
            return;
        }
        Console.WriteLine(blog.Id);
        Console.WriteLine(blog.Title);
        Console.WriteLine(blog.Author);
        Console.WriteLine(blog.Content);
    }

    public void Create(string title, string author, string content)
    {
        var blog = new tblBlog
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            Author = author,
            Content = content
        };
        _db.Blogs.Add(blog);
        int result = _db.SaveChanges();
        string message = result > 0 ? "Saving successful." : "Saving failed.";
        Console.WriteLine(message);
    }

    public void Update(string id, string title, string author, string content)
    {
        var blog = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (blog is null)
        {
            Console.WriteLine("No data found.");
            return;
        }
        blog.Title = title;
        blog.Author = author;
        blog.Content = content;
        _db.Entry(blog).State = EntityState.Modified;
        int result = _db.SaveChanges();
        string message = result > 0 ? "Updating successful." : "Updating failed.";
        Console.WriteLine(message);
    }

    public void Delete(string id)
    {
        var blog = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (blog is null)
        {
            Console.WriteLine("No data found.");
            return;
        }
        _db.Entry(blog).State = EntityState.Deleted;
        int result = _db.SaveChanges();
        string message = result > 0 ? "Deleting successful." : "Deleting failed.";
        Console.WriteLine(message);
    }
}
