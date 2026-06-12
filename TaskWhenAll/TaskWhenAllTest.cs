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
            return await Task.Run<DateTime>(async () =>
            {
                // This sleep will not slow down the loop in StartNewJob,
                // because it's running in a separate task. Each task will sleep for 5 seconds independently.
                Thread.Sleep(5000);

                Console.WriteLine($"Task {taskId} is starting.");
                // Simulate some work
                await Task.Delay(5000);
                Console.WriteLine($"Task {taskId} is completed.");
                return DateTime.Now;
            });
        }
    }
}
