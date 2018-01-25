using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingExamples
{
    public class AsyncAwaitSample
    {
        public async void Go()
        {
            System.Diagnostics.Debug.WriteLine("Main Thread [{0}]", Thread.CurrentThread.ManagedThreadId);

            //this.Start().Wait();
            await this.Start();

            System.Diagnostics.Debug.WriteLine("Main Thread [{0}]", Thread.CurrentThread.ManagedThreadId);

        }

        private async Task Start()
        {
            System.Diagnostics.Debug.WriteLine("Main - Start Thread [{0}]", Thread.CurrentThread.ManagedThreadId);

            //if in Go we do this.Start().Wait() then ConfigureAwait is mandatory to avoid deadlock!!!
            //depends on this the code that is executed after the task is completed is executed by the original thread or not!!!

            //await WaitWithRun().ConfigureAwait(continueOnCapturedContext: false);
            await WaitWithRun();

            System.Diagnostics.Debug.WriteLine("Main - End Thread [{0}]", Thread.CurrentThread.ManagedThreadId);
        }

        private Task Wait()
        {
            Task t = new Task(() => 
            {
                System.Diagnostics.Debug.WriteLine("Task Begin - Thread [{0}]", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(5000);
                System.Diagnostics.Debug.WriteLine("Task End - Thread [{0}]", Thread.CurrentThread.ManagedThreadId);
            });

            t.Start();

            return t;
        }

        private Task WaitWithRun()
        {
            Task t = Task.Run(() =>
            {
                System.Diagnostics.Debug.WriteLine("Task Begin (RUN) - Thread [{0}]", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(5000);
                System.Diagnostics.Debug.WriteLine("Task End (RUN) - Thread [{0}]", Thread.CurrentThread.ManagedThreadId);
                return;
            });

            return t;
        }

        private Task WaitWithDelay()
        {
            return Task.Delay(5000);
        }
    }
}
