namespace AsyncEventHandlerExample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Started");

            await ExecuteHandlersSequentially();
            Console.WriteLine("Finished");
        }

        private static async Task ExecuteHandlersSequentially()
        {
            AsyncEventHandlerOwner asyncEventHandlerOwner = new();
            for (int i = 0; i < 10; i++)
            {
                asyncEventHandlerOwner.ReceivedAsync += async (_, ea) => await ProcessMessageWithAsyncAwait(ea);
            }
            await asyncEventHandlerOwner.OnReceivedAsyncAwaitEveryHandler(new AsyncEventArgs());
        }

        static private async Task ProcessMessageWithAsyncAwait(AsyncEventArgs args)
        {
            Console.WriteLine("ProcessMessageWithAsyncAwait started");
            Task processingTask = Task.Run(async () =>
            {
                await InternalProcessing();
            });

            await processingTask.ContinueWith(t =>
            {
                Console.WriteLine("ProcessMessageWithAsyncAwait ContinueWith");
            });

            Console.WriteLine("ProcessMessageWithAsyncAwait finished");
        }

        static private Task ProcessMessageWithReturnTask(AsyncEventArgs args)
        {
            Console.WriteLine("ProcessMessageWithAsyncAwait started");
            Task processingTask = Task.Run(async () =>
            {
                await InternalProcessing();
            });

            processingTask.ContinueWith(t =>
            {
                Console.WriteLine("ProcessMessageWithAsyncAwait ContinueWith");
            });

            Console.WriteLine("ProcessMessageWithAsyncAwait finished");
            return Task.CompletedTask;
        }

        static private async Task InternalProcessing()
        {
            Console.WriteLine("InternalProcessing started");
            await Task.Delay(5000);
            Console.WriteLine("InternalProcessing finished");
        }
    }
}
