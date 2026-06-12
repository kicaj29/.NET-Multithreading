using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TaskWhenAll
{
    public class TaskWhenAllTest
    {
        public async Task StartNewJob()
        {
            List<Task<DateTime>> tasks = new List<Task<DateTime>>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 10; i++)
            {
                int taskId = i; // Capture the current value of i
                tasks.Add(DoWork(taskId));
            }
            stopwatch.Stop();
            Console.WriteLine("All tasks started in {0} ms.", stopwatch.ElapsedMilliseconds);
            await Task.WhenAll(tasks);
            Console.WriteLine("All tasks completed.");
        }

        private async Task<DateTime> DoWork(int taskId)
        {
            // This sleep will slow down the loop in StartNewJob,
            // allowing us to see the effect of starting tasks sequentially.
            // So even we do not use await in the loop, the tasks will not start until the sleep is completed!!!
            Thread.Sleep(5000);

            Console.WriteLine($"Task {taskId} is starting.");
            // Simulate some work
            await Task.Delay(5000);
            Console.WriteLine($"Task {taskId} is completed.");
            return DateTime.Now;
        }
    }
}
