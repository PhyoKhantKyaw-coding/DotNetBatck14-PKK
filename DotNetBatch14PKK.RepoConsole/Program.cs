// See https://aka.ms/new-console-template for more information
using DotNetBatch14PKK.RepoConsole;

Console.WriteLine("Hello, World!");

RepoHttpsClient httpClientService = new();
BlogListResponseModel responseModel = await httpClientService.GetBlogs();
foreach(var data in responseModel.Data)
{
	Console.WriteLine(data);
}
Console.ReadLine();
