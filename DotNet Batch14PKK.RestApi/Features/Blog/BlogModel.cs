using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet_Batch14PKK.RestApi.Features.Blog;
[Table ("tblBlog")]
public class BlogModel
{
    [Key]
    public string? BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
}

public class ResponseModel
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
}
