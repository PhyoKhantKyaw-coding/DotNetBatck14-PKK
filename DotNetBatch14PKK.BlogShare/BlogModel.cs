using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet_Batch14PKK.BlogShare;
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
    public string? Message { get; set; }
    public BlogModel? Data { get; set; }
}

public class BlogListResponseModel
{
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
    public List<BlogModel>? Data { get; set; }
}
