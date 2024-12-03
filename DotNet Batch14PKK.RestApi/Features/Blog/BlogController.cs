using DotNet_Batch14PKK.RestApi.Features.Blog;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogServices _blogService;
    public BlogController()
    {
       // _blogService = new BlogServices();
       // _blogService = new DapperBlogServices();
        _blogService = new EfcoreSerives();
    }
    [HttpGet]
    public IActionResult GetBlogs()
    {
        var blogs = _blogService.GetBlogs();
        return Ok(blogs);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(string id)
    {
        var blog = _blogService.GetBlog(id);
        if (blog == null) return NotFound("No data found.");
        return Ok(blog);
    }

    [HttpPost]
    public IActionResult CreateBlog([FromBody] BlogModel requestModel)
    {
        var model = _blogService.CreateBlog(requestModel);
        if (!model.IsSuccessful) return BadRequest(model);
        return Ok(model);
    }

    [HttpPut("{id}")]
    public IActionResult UpsertBlog(string id, [FromBody] BlogModel requestModel)
    {
        requestModel.BlogId = id;
        var model = _blogService.UpsertBlog(requestModel);
        if (!model.IsSuccessful) return BadRequest(model);
        return Ok(model);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateBlog(string id, [FromBody] BlogModel requestModel)
    {
        requestModel.BlogId = id;
        var model = _blogService.UpdateBlog(requestModel);
        if (!model.IsSuccessful) return BadRequest(model);
        return Ok(model);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(string id)
    {
        var model = _blogService.DeleteBlog(id);
        if (!model.IsSuccessful) return NotFound(model);
        return Ok(model);
    }
}
