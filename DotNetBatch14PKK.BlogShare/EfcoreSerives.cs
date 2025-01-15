
using Microsoft.EntityFrameworkCore;

namespace DotNet_Batch14PKK.BlogShare;

public class EfcoreSerives : IBlogServices
{
    private readonly AppDbContent _db;

    public EfcoreSerives(AppDbContent db)
    {
        _db = db;
    }

    public ResponseModel CreateBlog(BlogModel requestModel)
    {
        requestModel.BlogId = Guid.NewGuid().ToString();
        _db.Blogs.Add(requestModel);
        var result = _db.SaveChanges();
        string message = result > 0 ? "Saving successful." : "Saving failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
    }

    public ResponseModel DeleteBlog(string id)
    {
        var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        if (item is null) 
        {
            return new ResponseModel() 
            { 
                IsSuccessful = false,
                Message = "no data found!"
            };
        }
        _db.Entry(item).State= EntityState.Deleted;
        var result = _db.SaveChanges();
        string message = result > 0 ? "Deleting successful." : "Deleting failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;

    }

    public BlogModel GetBlog(string id)
    {
        var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        return item;
    }

    public List<BlogModel> GetBlogs()
    {
        var lis = _db.Blogs.AsNoTracking().ToList();
        return lis;
    }

    public ResponseModel UpdateBlog(BlogModel requestModel)
    {
        var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == requestModel.BlogId);
        if (item is null)
        {
            return new ResponseModel()
            {
                IsSuccessful = false,
                Message = "no data found!"
            };
        }

        if (!String.IsNullOrEmpty(requestModel.BlogTitle))
        {
            item.BlogTitle =requestModel.BlogTitle ;
        }
        if (!String.IsNullOrEmpty(requestModel.BlogAuthor))
        {
            item.BlogAuthor= requestModel.BlogAuthor;
        }
        if (!String.IsNullOrEmpty(requestModel.BlogContent))
        {
            item.BlogContent =requestModel.BlogContent;
        }
        _db.Entry(item).State = EntityState.Modified;
        var result = _db.SaveChanges();
        string message = result > 0 ? "Updating successful." : "Updating failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
    }

    public ResponseModel UpsertBlog(BlogModel requestModel)
    {
        var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == requestModel.BlogId);
        if (item is null)
        {
            _db.Blogs.Add(requestModel);
            var result1 = _db.SaveChanges();
            string message1 = result1 > 0 ? "Saving successful." : "Saving failed.";
            ResponseModel model1 = new()
            {
                IsSuccessful = result1 > 0,
                Message = message1
            };
            return model1;
        }
        #region update
        if (!String.IsNullOrEmpty(requestModel.BlogTitle))
        {
            item.BlogTitle = requestModel.BlogTitle;
        }
        else if (!String.IsNullOrEmpty(requestModel.BlogAuthor))
        {
            item.BlogAuthor = requestModel.BlogAuthor;
        }
        else if (!String.IsNullOrEmpty(requestModel.BlogContent))
        {
            item.BlogContent = requestModel.BlogContent;
        }
        _db.Entry(item).State = EntityState.Modified;
        var result = _db.SaveChanges();
        string message = result > 0 ? "Updating successful." : "Updating failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
        #endregion
    }
}
