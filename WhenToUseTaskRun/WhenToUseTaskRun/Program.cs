using System;

namespace WhenToUseTaskRun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("START");

            Go.RunAsync();

            Console.WriteLine("END");

            Console.ReadKey();
        }
    }
}
