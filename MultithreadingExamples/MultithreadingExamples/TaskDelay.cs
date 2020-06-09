using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingExamples
{
    public class TaskDelay
    {
        public void Run()
        {
            CancellationTokenSource source = new CancellationTokenSource();

            var t = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromSeconds(1.5), source.Token);
                return 42;
            });
            Thread.Sleep(2000);
            source.Cancel();
            try
            {
                t.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                    Debug.WriteLine(string.Format("{0}: {1}", e.GetType().Name, e.Message));
            }
            Debug.Write(string.Format("Task t Status: {0}", t.Status));
            if (t.Status == TaskStatus.RanToCompletion)
                Debug.Write(string.Format(", Result: {0}", t.Result));
            source.Dispose();
        }
    }
}
