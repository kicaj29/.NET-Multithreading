using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
                _ = ExecuteHandler1();
                _ = ExecuteHandler2();
            });
        }

        async Task ExecuteHandler1()
        {
            using (StartActivityAsTraceIdCarrier("ed492ba209b2b11e2ba6a9e501aba100"))
            {
                Console.WriteLine($"ExecuteHandler1: {Activity.Current?.RootId}");
                await Handler1();
                Console.WriteLine("await Handler1 complete");
                Console.WriteLine($"ExecuteHandler1: {Activity.Current?.RootId}");
            }
        }

        async Task Handler1()
        {
            Console.WriteLine($"Handler1: {Activity.Current?.RootId}");
            await Task.Delay(5000);
        }

        async Task ExecuteHandler2()
        {
            using (StartActivityAsTraceIdCarrier("916b478b997503ddbe9fd8bf2dfdd200"))
            {
                Console.WriteLine($"ExecuteHandler2: {Activity.Current?.RootId}");
                await Handler1();
                Console.WriteLine($"ExecuteHandler2: {Activity.Current?.RootId}");
            }
        }

        async Task Handler2()
        {
            Console.WriteLine($"Handler2: {Activity.Current?.RootId}");
        }
    }
}
