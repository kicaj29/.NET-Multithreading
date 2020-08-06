using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WhenToUseTaskRun
{
    public class Go
    {
        public async static void RunWithoutAwait()
        {
            Console.WriteLine($"RunWithoutAwait START: {Thread.CurrentThread.ManagedThreadId}");
            var iSvc = new ImageService();

            await iSvc.DownloadImageAsync("fake");

            Console.WriteLine($"RunWithoutAwait BETWEEN: {Thread.CurrentThread.ManagedThreadId}");

            iSvc.BlurImageAsync("original.png");

            Console.WriteLine($"RunWithoutAwait END: {Thread.CurrentThread.ManagedThreadId}");
        }


        private static void PrintArray(byte[] image)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var b in image)
            {
                sb.Append($"{b},");
                if (sb.Length > 20)
                {
                    break;
                }
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
