using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandlingInTasks.ConsoleApp
{
    internal class MethodToTest
    {
        public string GetStringThrows()
        {
            throw new Exception("GetStringThrows");
        }

        public async Task<string> GetStringThrowsAsync()
        {
            string result = null;
            try
            {
                result = await Task.Run<string>(() =>
                {
                    throw new Exception($"GetStringThrowsAsync: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
                    return "abc";
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            return result;
        }
    }
}
