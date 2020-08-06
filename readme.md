- [Async suffix vs async keyword](#async-suffix-vs-async-keyword)
- [await scope and blocking execution](#await-scope-and-blocking-execution)
  - [await](#await)
  - [no await](#no-await)
  - [no await but call Result](#no-await-but-call-result)
- [ThreadStaticWithAsyncAwait](#threadstaticwithasyncawait)
- [Async Await ConfigureAwait](#async-await-configureawait)
- [Task.Start caveats](#taskstart-caveats)
- [when to create and run new task (CPU vs. I/O bound code)](#when-to-create-and-run-new-task-cpu-vs-io-bound-code)
  - [CPU-bound and I/O-bound code](#cpu-bound-and-io-bound-code)
  - [example](#example)
- [Async Await in REST API Controllers](#async-await-in-rest-api-controllers)

# Async suffix vs async keyword

[stackoverflow](https://stackoverflow.com/questions/15951774/does-the-use-of-the-async-suffix-in-a-method-name-depend-on-whether-the-async) discussion.

>"
You could say that the Async suffix convention is to communicate to the API user that the method is awaitable. For a method to be awaitable, it must return Task for a void, or Task<T> for a value-returning method, which means only the latter can be suffixed with Async.
"   
So it means that it is ok to use Async suffix and not use async keyword on a method [example](https://github.com/dotnet/runtime/blob/master/src/libraries/System.Net.Http/src/System/Net/Http/HttpClient.cs#L205).

[MSDN doc](https://docs.microsoft.com/en-us/dotnet/csharp/async)
[Sonar](https://rules.sonarsource.com/csharp/tag/convention/RSPEC-4261)

# await scope and blocking execution

Related [article](https://www.pluralsight.com/guides/understand-control-flow-async-await).

>"**await doesn't block - it frees up the calling thread**".

Examples based on [AwaitScopeExample](./AwaitScope/AwaitScope/AwaitScopeExample.cs).

## await

>"When a method using await is not itself awaited, execution of the calling method continues before the called method has completed"

That`s why ```Console.WriteLine("End");``` is executed without any waiting!

```c#
public class AwaitScopeExample
{
    // Use await to frees up calling thread. Method that called MethodWithAwaitAsync continuos its execution!
    public async Task<string> MethodWithAwaitAsync()
    {
        Console.WriteLine($"MethodWithAwaitAsync START: {Thread.CurrentThread.ManagedThreadId}");

        var s = await Method2Async();
        Console.WriteLine($"Ala ma {s}");

        Console.WriteLine($"MethodWithAwaitAsync END: {Thread.CurrentThread.ManagedThreadId}");

        return s;
    }

    public async Task<string> Method2Async()
    {
        Console.WriteLine($"Method2 START: {Thread.CurrentThread.ManagedThreadId}");

        var t = await Task.Run(() => {
            Console.WriteLine($"Task.Run START: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(3000);
            Console.WriteLine($"Task.Run END: {Thread.CurrentThread.ManagedThreadId}");
            return "Kota";
            }
        );

        Console.WriteLine($"Method2 END: {Thread.CurrentThread.ManagedThreadId}");

        return t;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            var i = new AwaitScopeExample();

            var s = i.MethodWithAwaitAsync();

            Console.WriteLine("End");

            Console.ReadKey();
        }
    }
}

```
Result:
```
Start
MethodWithAwaitAsync START: 1
Method2 START: 1
End
Task.Run START: 4
Task.Run END: 4
Method2 END: 4
Ala ma Kota
MethodWithAwaitAsync END: 4
```

## no await

There is no await before calling so ```Console.WriteLine($"MethodWithoutAwaitAsync END: {Thread.CurrentThread.ManagedThreadId}");``` is executed without any waiting on Task from ```Method2Async()```.

```c#
public class AwaitScopeExample
{
    public Task<string> MethodWithoutAwaitAsync()
    {
        Console.WriteLine($"MethodWithoutAwaitAsync START: {Thread.CurrentThread.ManagedThreadId}");

        var t = Method2Async();

        Console.WriteLine($"MethodWithoutAwaitAsync END: {Thread.CurrentThread.ManagedThreadId}");

        return t;
    }

    public async Task<string> Method2Async()
    {
        Console.WriteLine($"Method2 START: {Thread.CurrentThread.ManagedThreadId}");

        var t = await Task.Run(() => {
            Console.WriteLine($"Task.Run START: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(3000);
            Console.WriteLine($"Task.Run END: {Thread.CurrentThread.ManagedThreadId}");
            return "Kota";
            }
        );

        Console.WriteLine($"Method2 END: {Thread.CurrentThread.ManagedThreadId}");

        return t;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            var i = new AwaitScopeExample();

            i.MethodWithoutAwaitAsync();

            Console.WriteLine("End");

            Console.ReadKey();
        }
    }
}
```
Result:
```
Start
MethodWithoutAwaitAsync START: 1
Method2 START: 1
MethodWithoutAwaitAsync END: 1
End
Task.Run START: 4
Task.Run END: 4
Method2 END: 4
```

## no await but call Result

Calling ```Task.Result``` blocks current thread until result is available.

```c#
public class AwaitScopeExample
{
    public Task<string> MethodWithoutAwaitButWithResultAsync()
    {
        Console.WriteLine($"MethodWithoutAwaitAsync START: {Thread.CurrentThread.ManagedThreadId}");

        var t = Method2Async();
        Console.WriteLine($"Ala ma {t.Result}");

        Console.WriteLine($"MethodWithoutAwaitAsync END: {Thread.CurrentThread.ManagedThreadId}");

        return t;
    }

    public async Task<string> Method2Async()
    {
        Console.WriteLine($"Method2 START: {Thread.CurrentThread.ManagedThreadId}");

        var t = await Task.Run(() => {
            Console.WriteLine($"Task.Run START: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(3000);
            Console.WriteLine($"Task.Run END: {Thread.CurrentThread.ManagedThreadId}");
            return "Kota";
            }
        );

        Console.WriteLine($"Method2 END: {Thread.CurrentThread.ManagedThreadId}");

        return t;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Start");

        var i = new AwaitScopeExample();

        i.MethodWithoutAwaitButWithResultAsync();

        Console.WriteLine("End");

        Console.ReadKey();
    }
}
```
Results:
```
Start
MethodWithoutAwaitAsync START: 1
Method2 START: 1
Task.Run START: 5
Task.Run END: 5
Method2 END: 5
Ala ma Kota
MethodWithoutAwaitAsync END: 1
End
```

# ThreadStaticWithAsyncAwait
This example demonstrates that we should not use ThreadStatic together with async/await because the callback delegate is executed on a different thread to one the async operation started on.
https://stackoverflow.com/questions/13010563/using-threadstatic-variables-with-async-await   

# Async Await ConfigureAwait
https://medium.com/bynder-tech/c-why-you-should-use-configureawait-false-in-your-library-code-d7837dce3d7f   

[Sources](./MultithreadingExamples/MultithreadingExamples/ConfigureAwaitExample.cs)

[Docs](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.configureawait?view=netcore-3.1) for ConfigureAwait. It is not clear why it is said **attempt**: "true to attempt to marshal the continuation back to the original context captured". It might suggest that there might be some situation when execution after awaiter will not be executed on the calling thread.   


By default when a task is completed awaiter tries to continue execution on the same thread that was used before starting the awaiter. For example:

```c#
private async void btn_TaskWithReturn_NoDeadlock_Click(object sender, EventArgs e)
{
    System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId); // thread 1 (main thread)
    var result = await cae.DoCurlAsync();
    System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId); // thread 1 (main thread)
    btn_TaskWithReturn_NoDeadlock.Text = "DONE";
}
```
In an ideal world people would always use await but it is also possible to call async method without await. In some cases it might create deadlock. Example is WinForms UI when calling thread (main thread) cannot go back to a message queue because it is waits for the results.

```c#
private void btn_TaskWithReturn_Deadlock_Click(object sender, EventArgs e)
{
    System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId); 
    // thread 1 (main thread)

    var result = cae.DoCurlAsync().Result;

    System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId); 
    // never will be executed (deadlock)
    
    btn_TaskWithReturn_Deadlock.Text = "DONE";
}
```

To avoid deadlock problem call ```ConfigureAwait(continueOnCapturedContext: false)``` inside of your [class/library](./MultithreadingExamples/MultithreadingExamples/ConfigureAwaitExample.cs#L26).

```c#
private void btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception_Click(object sender, EventArgs e)
{
    System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId);
    // thread 1 (main thread)

    var result = cae.DoCurlAsyncDoNotForceToContinueOnCallingThread().Result;

    System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId);
    // thread 1 (main thread)

    btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_noUIexception.Text = "DONE";
}
```

It will eliminate deadlock and execution after awaiter might be executed by different thread then calling thread. 

>NOTE: It looks that thread used to execute code after awaiter is different then calling thread only when  **await/ConfigureAwait** is used directly in method that handles click event:

```c#
private async void btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception_Click(object sender, EventArgs e)
{
    System.Diagnostics.Debug.WriteLine("Button Click - START [{0}]", Thread.CurrentThread.ManagedThreadId);
    // thread 1 (main thread)
    
    var result = await cae.DoCurlAsync().ConfigureAwait(continueOnCapturedContext: false);          
    System.Diagnostics.Debug.WriteLine("Button Click - END [{0}]", Thread.CurrentThread.ManagedThreadId); 
    // thread 3 (worker thread)

    btn_TaskWithReturn_NoDeadlockNoForceToContinueOnCallingThread_UIexception.Text = "DONE";
    //System.InvalidOperationException: 'Cross-thread operation not valid: Control 'btnAwaitWithOwnTask' accessed from a thread other than the thread it was created on.'


}
```

>NOTE: **In conclusion, it is good practice to always use ConfigureAwait(false) in your library code to prevent unwanted issues.**

# Task.Start caveats

Information based on [article](https://devblogs.microsoft.com/pfxteam/task-factory-startnew-vs-new-task-start/).

With TPL we can start a task in couple of different ways:
* [Task.Start](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.start?view=netcore-3.1)
* [Task.Run](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.run?view=netcore-3.1) (static)
* [TaskFactory.StartNew](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskfactory.startnew?view=netcore-3.1)
  
In general it is recommended to use **Task.Run** or **TaskFactory.StartNew** because **Task.Start** is less efficient.   

"
For example, we take a lot of care within TPL to make sure that when accessing tasks from multiple threads concurrently, the “right” thing happens.  A Task is only ever executed once, and that means we need to ensure that multiple calls to a task’s **Start** method from multiple threads concurrently will only result in the task being scheduled once.  This requires synchronization, and synchronization has a cost.
"

Also in [Task.Start documentation](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.start?view=netcore-3.1) is comment: **"we recommend that you call an overload of the Task.Run or TaskFactory.StartNew method"**.

# when to create and run new task (CPU vs. I/O bound code)

Chapter based on [article1](https://www.pluralsight.com/guides/using-task-run-async-await) and [article2](https://blog.stephencleary.com/2013/11/taskrun-etiquette-examples-dont-use.html).

## CPU-bound and I/O-bound code
>"By saying something is "bound" by the CPU, we're basically
saying that the computer's processor (or a particular thread running in the processor) is the bottleneck."

>"By contrast, for I/O-bound code the data transfer rate of an input or output process is the bottleneck."

>"Launching an operation on a separate thread via **Task.Run is mainly useful for CPU-bound operations**, not I/O-bound operations."

**My comment**: do not create a new task that calls other async methods (because they also create a task(s)) if they do I/O bound code. It will create only one more thread that will be doing nothing besides calling another thread that do the actual work!

## example

[Example source code](./WhenToUseTaskRun/WhenToUseTaskRun)

Result:
```
START
RunAsync START: 1
GetByteArrayAsync START: 1
END
GetByteArrayAsync in Task.Run: 4
GetByteArrayAsync END: 4
RunAsync BETWEEN: 4
BlurImageAsync START: 5
BlurImageAsync END: 5
RunAsync END: 5
```


# Async Await in REST API Controllers
https://www.c-sharpcorner.com/article/async-await-and-asynchronous-programming-in-mvc/