using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotNetBatch14PKK.RepoDbShared;

namespace DotNetBatch14PKK.RepoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly RepoDbShared.RepoService _repoService = new();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            try
            {
                var blogs = _repoService.GetBlogs();
                return Ok(new BlogListResponseModel
                {
                    IsSuccessful = true,
                    Message = "Success",
                    Data = blogs
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel
                {
                    Message = ex.ToString(),
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(string id)
        {
            try
            {
                var blog = _repoService.GetBlog(id);
                if (blog == null)
                    return NotFound(new ResponseModel
                    {
                        IsSuccessful = false,
                        Message = "No data found."
                    });

                return Ok(new ResponseModel
                {
                    IsSuccessful = true,
                    Message = "Success",
                    Data = blog
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel
                {
                    Message = ex.ToString(),
                });
            }
        }

        [HttpPost]
        public IActionResult CreateBlog([FromBody] BlogModel requestModel)
        {
            try
            {
                var model = _repoService.Create(requestModel.BlogTitle, requestModel.BlogAuthor, requestModel.BlogContent);
                if (!model.IsSuccessful)
                    return BadRequest(model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel
                {
                    Message = ex.ToString(),
                });
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateBlog(string id, [FromBody] BlogModel requestModel)
        {
            try
            {
                requestModel.BlogId = id;
                var model = _repoService.update(requestModel);
                if (!model.IsSuccessful)
                    return BadRequest(model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel
                {
                    Message = ex.ToString(),
                });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(string id)
        {
            try
            {
                var model = _repoService.Delete(id);
                if (!model.IsSuccessful)
                    return BadRequest(model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel
                {
                    Message = ex.ToString(),
                });
            }
        }
    }
}
