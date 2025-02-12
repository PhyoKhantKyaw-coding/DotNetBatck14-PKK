﻿using DotNet_Batch14PKK.BlogShare;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14PKK.MVCApp.Controllers;

public class BlogController : Controller
{
    private readonly IBlogServices _blogServices;

    public BlogController(IBlogServices blogServices)
    {
        _blogServices = blogServices;
    }
    [ActionName("Index")]
    public IActionResult BlogList()
    {
        var lst = _blogServices.GetBlogs();
        return View("BlogList", lst);
    }
    [ActionName("Create")]
    public IActionResult CreateBlog()
    {
        return View("CreateBlog");
    }
    [HttpPost]
    [ActionName("Save")]
    public IActionResult SaveBlog(BlogModel requestModel)
    {
        List<string> errorList = new List<string>();
        if (string.IsNullOrEmpty(requestModel.BlogTitle))
        {
            errorList.Add("Blog Title is required");
        }
        if (string.IsNullOrEmpty(requestModel.BlogAuthor))
        {
            errorList.Add("Blog Author is required");
        }
        if (string.IsNullOrEmpty(requestModel.BlogContent))
        {
            errorList.Add("Blog Content is required");
        }
        if (errorList.Count > 0)
        {
            //test viewbag
            ViewBag.IsValidationError = true;
            ViewBag.ValidationErrors = errorList;
            return View("CreateBlog", requestModel);
        }

        var result = _blogServices.CreateBlog(requestModel);
        ViewBag.IsSuccess = result.IsSuccessful;
        ViewBag.Message = result.Message;

        // test tempdata
        TempData["IsSuccess"] = result.IsSuccessful;
        TempData["Message"] = result.Message;

        return RedirectToAction("Index");
        //return View("CreateBlog");
    }

    [ActionName("Edit")]
    public IActionResult EditBlog(string id)
    {
        var item = _blogServices.GetBlog(id);
        return View("EditBlog", item);
    }

    [HttpPost]
    [ActionName("Update")]
    public IActionResult UpdateBlog(BlogModel requestModel,String id)
    {
        requestModel.BlogId=id;
        _blogServices.UpdateBlog(requestModel);
        return RedirectToAction("Index");
    }
    [ActionName("Delete")]
    public IActionResult DeleteBlog(string id)
    {
        _blogServices.DeleteBlog(id);
        return RedirectToAction("Index");
    }
}
