namespace AsyncEventHandlerExample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Started");

            // await ExecuteHandlersInParallel_WithAwait();
            await ExecuteHandlersInParallel_WithNoAwait();

            // await ExecuteHandlersSequentially_WithAwait();


            Console.WriteLine("Finished");
        }

        private static async Task ExecuteHandlersInParallel_WithNoAwait()
        {
            AsyncEventHandlerOwner asyncEventHandlerOwner = new();
            for (int i = 0; i < 10; i++)
            {
                asyncEventHandlerOwner.ReceivedAsync += (_, ea) => ProcessMessageWithReturnTask(ea);
            }
            await asyncEventHandlerOwner.OnReceivedAsyncAwaitEveryHandler();

            // Add delay to execute all handlers
            await Task.Delay(7000);
        }

        private static async Task ExecuteHandlersInParallel_WithAwait()
        {
            AsyncEventHandlerOwner asyncEventHandlerOwner = new();
            for (int i = 0; i < 10; i++)
            {
                asyncEventHandlerOwner.ReceivedAsync += async (_, ea) => await ProcessMessageWithAsyncAwait(ea);
            }
            await asyncEventHandlerOwner.OnReceivedAsyncTaskWhenAll();

            // Add delay to execute all handlers
            await Task.Delay(7000);
        }

        private static async Task ExecuteHandlersSequentially_WithAwait()
        {
            AsyncEventHandlerOwner asyncEventHandlerOwner = new();
            for (int i = 0; i < 10; i++)
            {
                asyncEventHandlerOwner.ReceivedAsync += async (_, ea) => await ProcessMessageWithAsyncAwait(ea);
            }
            await asyncEventHandlerOwner.OnReceivedAsyncAwaitEveryHandler();
        }

        static private async Task ProcessMessageWithAsyncAwait(AsyncEventArgs args)
        {
            Console.WriteLine("ProcessMessageWithAsyncAwait started");
            Task processingTask = Task.Run(async () =>
            {
                await InternalProcessing(args);
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
                await InternalProcessing(args);
            });

            processingTask.ContinueWith(t =>
            {
                Console.WriteLine("ProcessMessageWithAsyncAwait ContinueWith");
            });

            Console.WriteLine("ProcessMessageWithAsyncAwait finished");
            return Task.CompletedTask;
        }

        static private async Task InternalProcessing(AsyncEventArgs args)
        {
            Console.WriteLine($"InternalProcessing started: {args.Index}");
            await Task.Delay(5000);
            Console.WriteLine($"InternalProcessing finished: {args.Index}");
        }
    }
}
