using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ConcurrentBag<MyMessage> bag = new ConcurrentBag<MyMessage>();
            MyMessage message = null;
            bool res = bag.TryTake(out message);
            bag.Add(new MyMessage() { RequestId = "A" });
            bag.Add(new MyMessage() { RequestId = "B" });
            bag.Add(new MyMessage() { RequestId = "C" });
            bag.Add(new MyMessage() { RequestId = "D" });
            res = bag.TryTake(out message);
            res = bag.TryTake(out message);
            res = bag.TryTake(out message);
            res = bag.TryTake(out message);
            res = bag.TryTake(out message);

            Example1 ex1 = new Example1();
            await ex1.Run();
        }
    }
}
