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

# Async Await in REST API Controllers
https://www.c-sharpcorner.com/article/async-await-and-asynchronous-programming-in-mvc/