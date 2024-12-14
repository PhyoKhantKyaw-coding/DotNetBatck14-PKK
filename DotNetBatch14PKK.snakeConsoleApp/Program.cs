// See https://aka.ms/new-console-template for more information
using DotNetBatch14PKK.snakeConsoleApp;

SnakeRefitClientServices snakerefit= new SnakeRefitClientServices();
var lst = await snakerefit.Getsnakes();
foreach(var item in lst)
{
    Console.WriteLine(item.MMName);
    Console.WriteLine(item.EngName);
    Console.WriteLine(item.Detail);
    Console.WriteLine(item.IsDanger);
    Console.WriteLine(item.IsPoison);
}
