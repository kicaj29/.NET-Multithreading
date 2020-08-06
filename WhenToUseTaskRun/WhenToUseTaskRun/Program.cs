using System;

namespace WhenToUseTaskRun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Go.RunWithoutAwait();

            Console.ReadKey();
        }
    }
}
