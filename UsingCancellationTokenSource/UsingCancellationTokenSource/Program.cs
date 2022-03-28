using System;
using System.Threading;
using System.Threading.Tasks;

namespace UsingCancellationTokenSource
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CancellationTokenSource sourceDefault = default;

            CancellationTokenSource source = new CancellationTokenSource();

            try
            {
                Task.Run(async () =>
                {
                    for(int index = 0; index < 70; index++)
                    {
                        await Task.Delay(1000, source.Token);
                        if (source.Token.IsCancellationRequested)
                        {
                            throw new OperationCanceledException(source.Token);
                        }
                    }
                }, source.Token).Wait();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.ReadKey();
        }
    }
}
