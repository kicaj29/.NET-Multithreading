using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WhenToUseTaskRun
{
    public class HttpClientStub
    {
        // it simluates real method
        // https://github.com/dotnet/runtime/blob/master/src/libraries/System.Net.Http/src/System/Net/Http/HttpClient.cs#L207
        // https://github.com/dotnet/runtime/blob/master/src/libraries/System.Net.Http/src/System/Net/Http/HttpContent.cs#L298
        // Because real HttpClient uses async/await it is also simulated here
        public async Task<byte[]> GetByteArrayAsync(string url)
        {
            Console.WriteLine($"GetByteArrayAsync START: {Thread.CurrentThread.ManagedThreadId}");
            var t = await Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"GetByteArrayAsync in Task.Run: {Thread.CurrentThread.ManagedThreadId}");
                return Encoding.ASCII.GetBytes("stub data");
            }
            ).ConfigureAwait(continueOnCapturedContext: true);
            Console.WriteLine($"GetByteArrayAsync END: {Thread.CurrentThread.ManagedThreadId}");
            return t;
        }
    }
}
