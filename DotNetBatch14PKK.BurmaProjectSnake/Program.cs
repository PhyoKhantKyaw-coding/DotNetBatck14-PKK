// See https://aka.ms/new-console-template for more information

using DotNetBatch14PKK.BurmaProjectSnake;
SnakeHttpsClientService httpsClientService = new SnakeHttpsClientService();
SnakeRefitClientServices refitClientService = new SnakeRefitClientServices();
SnakeRestSharpClientService restSharpClientService = new SnakeRestSharpClientService();

var datalist= new List<Rootobject>();
    datalist = await httpsClientService.GetSnakes();
foreach (Rootobject data in datalist)
{
    Console.WriteLine(data.EngName);
    Console.WriteLine(data.MMName);
}

var data1 = await httpsClientService.GetSnake(1);
Console.WriteLine(data1.EngName);

var lis = await refitClientService.Getsnakes();
foreach (Rootobject data in lis)
{
    Console.WriteLine(data.EngName);
    Console.WriteLine(data.MMName);
}

var data2 = await refitClientService.Getsnake(2);
Console.WriteLine(data2?.EngName);

var list = await restSharpClientService.GetSnakes();
foreach (Rootobject data in list)
{
    Console.WriteLine(data.EngName);
    Console.WriteLine(data.MMName);
}

var data3 = await restSharpClientService.GetSnake(3);
Console.WriteLine(data3.MMName);

Console.ReadLine();
