// See https://aka.ms/new-console-template for more information
using UsingTaskYield;

Console.WriteLine("Hello, World!");

Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}"); // Print a number
await Task.CompletedTask;
Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}"); // Print the same number as above
await Task.FromResult(1);
Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}"); // Still the same number as above

// Task.Yield can force an asynchronous method to complete asynchronously.
// It means that the code after `Yield` will be executed on a different thread.
await Task.Yield();
Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}"); // Print a different number

// More here: https://duongnt.com/task-yield/

TestTaskYield test = new TestTaskYield();

Func<Task> asyncHandler = async () => await Task.Delay(500);
// await test.DriverMethod(asyncHandler);
//  This will print:
/*  Executing handler...
    Executing handler...
    Executing handler...
    Executing handler...
    ================ TASK CANCEL ================
*/


Func<Task> completedTaskHandler = () => Task.CompletedTask;
//await test.DriverMethod(completedTaskHandler);
// Program runs into an infinite loop.
/* Executing handler...
 * Executing handler...
 * Executing handler...
 * Executing handler...
 * ...
 */



Func<Task> taskYieldHandler = async () => await Task.Yield();
await test.DriverMethod(taskYieldHandler);
//  This will print:
/*  ...
 *  Executing handler...
 *  Executing handler...
 *  Executing handler...
 *  Executing handler...
 *  Executing handler...
    ================ TASK CANCEL ================
*/


Console.WriteLine("DONE");
Console.ReadKey();