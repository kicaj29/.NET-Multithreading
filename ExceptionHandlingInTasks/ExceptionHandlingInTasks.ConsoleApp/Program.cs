// See https://aka.ms/new-console-template for more information
using ExceptionHandlingInTasks.ConsoleApp;

Console.WriteLine("Hello, World!");

System.Timers.Timer timer = new System.Timers.Timer(8000);
timer.Elapsed += async (sender, e) => await OnTimer();
timer.AutoReset = true;
timer.Enabled = true;

async Task OnTimer()
{
    await Task.Run(() =>
    {
        throw new Exception($"Timer: exception from task. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
    });
    // throw new Exception($"Timer: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
}

var obj = new MethodToTest();
await obj.GetStringThrowsAsync();

Console.ReadKey();


