using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadStaticWithAsyncAwait
{
    class Program
    {
        [ThreadStatic]
        private static string Secret;

        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread [{0}]", Thread.CurrentThread.ManagedThreadId);
            Start().Wait();
            Console.WriteLine("Main Thread [{0}]", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
            
        }

        private static async Task Start()
        {
            Secret = "moo moo";
            Console.WriteLine("Started on thread [{0}]", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Secret is [{0}]", Secret);

            await Sleepy();

            Console.WriteLine("Finished on thread [{0}]", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Secret is [{0}]", Secret);
        }

        private static async Task Sleepy()
        {
            Console.WriteLine("Was on thread [{0}]", Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000);
            Console.WriteLine("Now on thread [{0}]", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
