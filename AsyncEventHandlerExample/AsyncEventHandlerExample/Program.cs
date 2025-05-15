namespace AsyncEventHandlerExample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Started");

            AsyncEventHandlerOwner asyncEventHandlerOwner = new();

            for(int i = 0; i < 10; i++)
            {
                asyncEventHandlerOwner.ReceivedAsync += async (model, ea) => await ProcessMessageWithAsyncAwait(ea);
            }

            await asyncEventHandlerOwner.OnReceivedAsyncVer2(new AsyncEventArgs());

            Console.WriteLine("Finished");
        }

        static private async Task ProcessMessageWithAsyncAwait(AsyncEventArgs args)
        {
            Console.WriteLine("ProcessMessageWithAsyncAwait started");
            await Task.Delay(1);
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

        static private async Task InternalProcessing()
        {
            Console.WriteLine("InternalProcessing started");
            await Task.Delay(5000);
            Console.WriteLine("InternalProcessing finished");
        }
    }
}
