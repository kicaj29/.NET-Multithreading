// See https://aka.ms/new-console-template for more information
using ActivityCurrentAndMultithreading;

Console.WriteLine("Hello, World!");

ActivityService service = new ActivityService();
await service.StartProccessing();

Console.ReadKey();