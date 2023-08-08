using ActivityCurrentAndMultithreading;
using System;
using System.Threading.Tasks;

namespace ActivityCurrentAndMultithreadingNET31
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            ActivityService service = new ActivityService();
            await service.StartProccessing();

            Console.ReadKey();
        }
    }
}
