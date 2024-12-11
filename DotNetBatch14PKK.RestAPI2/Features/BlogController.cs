using DotNet_Batch14PKK.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14PKK.RestAPI2.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogServices _blogService;
        public BlogController()
        {
            _blogService = new BlogServices();
            // _blogService = new DapperBlogServices();
            //_blogService = new EfcoreSerives();
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            try
            {
                var blogs = _blogService.GetBlogs();
                //throw new Exception("Heee heee");
                BlogListResponseModel model = new BlogListResponseModel
                {
                    IsSuccessful = true,
                    Message = "Success",
                    Data = blogs

                };
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

        [HttpGet("{id}")]
        public IActionResult GetBlog(string id)
        {
            try
            {
                var blog = _blogService.GetBlog(id);
                if (blog == null) return NotFound("No data found.");
                ResponseModel model = new ResponseModel 
                { 
                    IsSuccessful=true,
                    Message="Success",
                    Data = blog
                    
                };

                return Ok(blog);
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
                var model = _blogService.CreateBlog(requestModel);
                if (!model.IsSuccessful) return BadRequest(model);
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
                var model = _blogService.UpdateBlog(requestModel);
                if (!model.IsSuccessful) return BadRequest(model);
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
                var model = _blogService.DeleteBlog(id);
                if( model is null)
                {
                    return BadRequest(model);
                }

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
