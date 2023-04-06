using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigureAwait
{
    public class AsyncAwaitVoid
    {
        public void Go()
        {
            System.Diagnostics.Debug.WriteLine("Go Thread - START [{0}]", Thread.CurrentThread.ManagedThreadId);

            // this.Start().Wait();
            // await this.Start();
            this.Start();

            System.Diagnostics.Debug.WriteLine("Go Thread - END [{0}]", Thread.CurrentThread.ManagedThreadId);

        }

        public async Task Start()
        {
            System.Diagnostics.Debug.WriteLine("Start - START Thread [{0}]", Thread.CurrentThread.ManagedThreadId);

            //if in Go we do this.Start().Wait() then ConfigureAwait is mandatory to avoid deadlock!!!
            //depends on this the code that is executed after the task is completed is executed by the original thread or not!!!

            await WaitWithRun().ConfigureAwait(continueOnCapturedContext: false);
            // await WaitWithRun();

            System.Diagnostics.Debug.WriteLine("Start - END Thread [{0}]", Thread.CurrentThread.ManagedThreadId);
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

        public Task WaitWithRun()
        {
            Task t = Task.Run(() =>
            {
                System.Diagnostics.Debug.WriteLine("WaitWithRun - START Thread [{0}]", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(5000);
                System.Diagnostics.Debug.WriteLine("WaitWithRun - END Thread [{0}]", Thread.CurrentThread.ManagedThreadId);
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
