using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14PKK.MinimalApi2.Features.Emoji
{
    [Table("TBLEmoji")]
    public class EmojiModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string HtmlCode { get; set; } = string.Empty;
        public string Unicode { get; set; } = string.Empty;
    }
    public class EmojiRequestModel
    {
        public List<EmojiModel> Emojis { get; set; }
    }

    public class EmojiListResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<EmojiModel> Data { get; set; }
    }

    public class EmojiResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public EmojiModel Data { get; set; }
    }
}
