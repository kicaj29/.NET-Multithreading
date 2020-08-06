using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WhenToUseTaskRun
{
    public class Go
    {
        public async static void RunAsync()
        {
            Console.WriteLine($"RunAsync START: {Thread.CurrentThread.ManagedThreadId}");
            var iSvc = new ImageService();

            await iSvc.DownloadImageAsync("fake");

            Console.WriteLine($"RunAsync BETWEEN: {Thread.CurrentThread.ManagedThreadId}");

            await iSvc.BlurImageAsync("original.png");

            Console.WriteLine($"RunAsync END: {Thread.CurrentThread.ManagedThreadId}");
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
