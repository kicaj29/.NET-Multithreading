using System;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Example1 ex1 = new Example1();
            await ex1.Run();
        }
    }
}
