using System;

namespace NestedAwaits
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            var i = new AwaitScopeExample();

            // var s = i.MethodWithAwaitAsync();

            i.MethodWithoutAwaitButWithResultAsync();

            // i.MethodWithoutAwaitAsync();

            Console.WriteLine("End");

            Console.ReadKey();
        }
    }
}
