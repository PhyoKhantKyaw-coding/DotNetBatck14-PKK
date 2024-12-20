using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.RepoConsole
{
    public class BlogModel
    {
        public string? BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
    }

    public class ResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
    public class BlogListResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public List<object>? Data { get; set; }
    }
}
