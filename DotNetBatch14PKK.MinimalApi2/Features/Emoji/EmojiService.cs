using DotNetBatch14PKK.MinimalApi2;
using DotNetBatch14PKK.MinimalApi2.Features.Emoji;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DotNetBatch14PKK.MinimalApi2.Features.Emoji;

public class EmojiService
{
    private readonly AppDbContent _context;
    private readonly HttpClient _httpClient;
    private readonly string _endpoint;

    public EmojiService(AppDbContent context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
        _endpoint = "https://gist.githubusercontent.com/oliveratgithub/0bf11a9aff0d6da7b46f1490f86a71eb/raw/d8e4b78cfe66862cf3809443c1dba017f37b61db/emojis.json";
    }

    public async Task InsertEmojisAsync()
    {
        var responseMessage = await _httpClient.GetAsync(_endpoint);
        var content = await responseMessage.Content.ReadAsStringAsync();
        var requestModel = JsonConvert.DeserializeObject<EmojiRequestModel>(content)!;

        if (requestModel?.Emojis != null)
        {
            foreach(var emoji in requestModel.Emojis)
            {
                _context.Add(emoji);
                _context.SaveChanges();
            }
        }
    }

    public async Task<EmojiListResponseModel> GetEmojisAsync()
    {
        var emojiList = await _context.EmojiModel.ToListAsync();
        return new EmojiListResponseModel
        {
            IsSuccess = true,
            Message = "Success",
            Data = emojiList
        };
    }

    public async Task<EmojiResponseModel> GetEmojiByIdAsync(int id)
    {
        var emoji = await _context.EmojiModel.FindAsync(id);
        if (emoji == null)
        {
            return new EmojiResponseModel
            {
                IsSuccess = false,
                Message = "No data found."
            };
        }
        return new EmojiResponseModel
        {
            IsSuccess = true,
            Message = "Success",
            Data = emoji
        };
    }

    public async Task<EmojiListResponseModel> FilterByNameAsync(string name)
    {
        var emojiList = await _context.EmojiModel
            .Where(e => EF.Functions.Like(e.Name, $"%{name}%"))
            .ToListAsync();
        return new EmojiListResponseModel
        {
            IsSuccess = true,
            Message = "Success",
            Data = emojiList
        };
    }
}

