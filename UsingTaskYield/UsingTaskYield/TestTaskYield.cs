using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingTaskYield
{
    public class TestTaskYield
    {
        private async Task RunUntilCancelAsync(Func<Task> handler, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine("Executing handler...");
                // When we reach the command await handler(),
                // because Task.CompletedTask is already completed,
                // the runtime keeps executing RunUntilCancelAsync synchronously and doesn’t yield control back
                // to the current context.
                await handler();
            }
            Console.WriteLine("================ TASK CANCEL ================");
        }

        public async Task DriverMethod(Func<Task> handler)
        {
            var cts = new CancellationTokenSource();

            var runTask = RunUntilCancelAsync(handler, cts.Token);
            await Task.Delay(2000);
            cts.Cancel();
            await runTask;
        }
    }
}
