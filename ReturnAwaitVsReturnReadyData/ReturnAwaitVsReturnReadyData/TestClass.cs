using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReturnAwaitVsReturnReadyData
{
    internal class TestClass
    {

        internal async Task<string> ReturnString()
        {
            await Task.Delay(3000);
            return "aaaaa";
        }

        internal async Task<string> ReturnTaskString()
        {
            return await Task.Run<string>(async () =>
            {
                await Task.Delay(3000);
                return "bbbbb";
            });
        }

        internal Task<string> ReturnTaskStringWithoutFirstAwait()
        {
            return Task.Run<string>(async () =>
            {
                await Task.Delay(3000);
                return "bbbbb";
            });
        }

        internal Task<string> ReturnTaskStringNoAwaitAtAll()
        {
            return Task.Run<string>(async () =>
            {
                Task.Delay(3000);
                return "bbbbb";
            });
        }
    }
}
