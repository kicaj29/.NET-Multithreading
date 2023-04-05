// See https://aka.ms/new-console-template for more information
using System.Threading;

Console.WriteLine("Hello, World!");

// 1.
List<int> list = new List<int> { 1, 2, 3};
list.ForEach(async (item) =>
{
    Console.WriteLine($"ForEach Waiting: {item}");
    await Task.Delay(2000);
    Console.WriteLine($"ForEach Completed: {item}");
});
Console.WriteLine("After .ForEach"); // List.ForEach will be executed immediately because there is no await!

// 2.
Parallel.ForEach(list, async (item) =>
{
    Console.WriteLine($"Parallel.ForEach Waiting: {item}");
    await Task.Delay(2000);
    Console.WriteLine($"Parallel.ForEach Completed: {item}");
});
Console.WriteLine("After Parallel.ForEach"); // After Parallel.ForEach will be executed immediately because there is no await!

// 3.
await Parallel.ForEachAsync(list, async(item, cancellationToken) =>
{
    Console.WriteLine($"Parallel.ForEachAsync Waiting: {item}");
    await Task.Delay(2000);
    Console.WriteLine($"Parallel.ForEachAsync Completed: {item}");
});
Console.WriteLine("After Parallel.ForEachAsync"); // After Parallel.ForEachAsync will be executed when all tasks are done

// 4.
var tasks = list.Select(async item =>
{
    Console.WriteLine($"Select Waiting: {item}");
    await Task.Delay(2000);
    Console.WriteLine($"Select Completed: {item}");
});
await Task.WhenAll(tasks);
Console.WriteLine("After .Select"); // After .Select will be executed when all tasks are done.


// 4.


Console.ReadKey();

