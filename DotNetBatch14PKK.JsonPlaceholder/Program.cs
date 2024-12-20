// See https://aka.ms/new-console-template for more information
using DotNetBatch14PKK.JsonPlaceholder;
AlbumServiceRefit jsonplaceholderService = new();

var postsList = await jsonplaceholderService.GetAlbums();
foreach (var postModel in postsList)
{
    Console.WriteLine(postModel.Title);
}
Console.ReadLine();

var post = await jsonplaceholderService.GetAlbum(5);
Console.WriteLine(post.Title);
Console.ReadLine();

AlbumsModel createAlbumModel = new()
{
    UserId = 1,
    Id = 1,
    Title = "Refit Test Title"
};
var createResponse = await jsonplaceholderService.CreateAlbum(createAlbumModel);
Console.WriteLine(createResponse.Title);
Console.ReadLine();

AlbumsModel updateAlbumModel = new()
{
    UserId = 1,
    Id= 1,
    Title = "Updated Refit Test Title"
};
var updateResponse = await jsonplaceholderService.UpdateAlbum(5, updateAlbumModel);
Console.WriteLine(updateResponse.Title);
Console.ReadLine();

var deleteResponse = await jsonplaceholderService.DeleteAlbum(5);
Console.WriteLine(deleteResponse.Title);
Console.ReadLine();