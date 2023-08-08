using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActivityCurrentAndMultithreading
{
    /// <summary>
    /// This class shows that using Activity.Current in async methods works fine.
    /// </summary>
    internal class ActivityService
    {
        internal Activity StartActivityAsTraceIdCarrier(string traceId)
        {
            Activity activity = new Activity($"trace-id-carrier");
            // Set the format before calling SetParentId because by default the format is Unknown
            // and it causes that field activity.RootId has invalid value.
            activity.SetIdFormat(ActivityIdFormat.W3C);
            activity.SetParentId(ActivityTraceId.CreateFromString(traceId), ActivitySpanId.CreateRandom(), ActivityTraceFlags.Recorded);
            activity.Start();
            return activity;

        }

        internal async Task StartProccessing()
        {
            await Task.Run(() =>
            {
                _ = ExecuteHandler1(); // trace id which ends with 100
                Thread.Sleep(1000);
                Console.WriteLine($"ExecuteHandler between: {Activity.Current?.RootId}");
                _ = ExecuteHandler2(); // trace id which ends with 200
            });
        }

        async Task ExecuteHandler1()
        {
            StartActivityAsTraceIdCarrier("ed492ba209b2b11e2ba6a9e501aba100");
            //{
                Console.WriteLine($"ExecuteHandler1: {Activity.Current?.RootId}");
                await Handler1();
                // Add delay to check Activity.Current?.RootId when ExecuteHandler2 will be done.
                Thread.Sleep(2000);
                await Task.Run(async () =>
                {

                    Console.WriteLine($"ExecuteHandler1 (task.run before delay): {Activity.Current?.RootId}");
                    await Task.Delay(4000);
                    Console.WriteLine($"ExecuteHandler1 (task.run after delay): {Activity.Current?.RootId}");
                });
                Console.WriteLine("await Handler1 complete");
                Console.WriteLine($"ExecuteHandler1: {Activity.Current?.RootId}");
            //}
        }

        async Task Handler1()
        {
            Console.WriteLine($"Handler1: {Activity.Current?.RootId}");
        }

        async Task ExecuteHandler2()
        {
            Console.WriteLine($"ExecuteHandler2 (before starting activity): {Activity.Current?.RootId}");
            StartActivityAsTraceIdCarrier("916b478b997503ddbe9fd8bf2dfdd200");
            //{
                Console.WriteLine($"ExecuteHandler2 (activity started): {Activity.Current?.RootId}");
                await Handler2();
                Console.WriteLine($"ExecuteHandler2 (activity started): {Activity.Current?.RootId}");
            //}
        }

        async Task Handler2()
        {
            Console.WriteLine($"Handler2: {Activity.Current?.RootId}");
        }
    }
}
