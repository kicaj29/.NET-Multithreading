using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NestedAwaits
{
    public class AwaitScopeExample
    {
        // Use await to frees up calling thread. Method that called MethodWithAwaitAsync continuos its execution!
        public async Task<string> MethodWithAwaitAsync()
        {
            Console.WriteLine($"MethodWithAwaitAsync START: {Thread.CurrentThread.ManagedThreadId}");

            var s = await Method2Async();
            Console.WriteLine($"Ala ma {s}");

            Console.WriteLine($"MethodWithAwaitAsync END: {Thread.CurrentThread.ManagedThreadId}");

            return s;
        }

        public Task<string> MethodWithoutAwaitAsync()
        {
            Console.WriteLine($"MethodWithoutAwaitAsync START: {Thread.CurrentThread.ManagedThreadId}");

            var t = Method2Async();

            Console.WriteLine($"MethodWithoutAwaitAsync END: {Thread.CurrentThread.ManagedThreadId}");

            return t;
        }

        public Task<string> MethodWithoutAwaitButWithResultAsync()
        {
            Console.WriteLine($"MethodWithoutAwaitAsync START: {Thread.CurrentThread.ManagedThreadId}");

            var t = Method2Async();
            Console.WriteLine($"Ala ma {t.Result}");

            Console.WriteLine($"MethodWithoutAwaitAsync END: {Thread.CurrentThread.ManagedThreadId}");

            return t;
        }

        public async Task<string> Method2Async()
        {
            Console.WriteLine($"Method2 START: {Thread.CurrentThread.ManagedThreadId}");

            var t = await Task.Run(() => {
                Console.WriteLine($"Task.Run START: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(3000);
                Console.WriteLine($"Task.Run END: {Thread.CurrentThread.ManagedThreadId}");
                return "Kota";
                }
            );

            Console.WriteLine($"Method2 END: {Thread.CurrentThread.ManagedThreadId}");

            return t;
        }
    }
}
