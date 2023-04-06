using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigureAwait
{
    public class ConfigureAwaitExample
    {
        public async Task<string> DoCurlAsync()
        {
            using (var httpClient = new HttpClient())
            using (var httpResonse = await httpClient.GetAsync("https://www.bynder.com"))
            {
                return await httpResonse.Content.ReadAsStringAsync();
            }
        }


        public async Task<string> DoCurlAsyncDoNotForceToContinueOnCallingThread()
        {
            using (var httpClient = new HttpClient())
            using (var httpResonse = await httpClient.GetAsync("https://www.bynder.com").ConfigureAwait(continueOnCapturedContext: false))
            {
                return await httpResonse.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> DoCurlStubAsync()
        {
            var res = await this.RunStub();
            return res;
        }

        public async Task<string> DoCurlStubAsyncDoNotForceToContinueOnCallingThread()
        {
            var res = await this.RunStub().ConfigureAwait(continueOnCapturedContext: false);
            return res;
        }

        private Task<string> RunStub()
        {
            return Task.Run<string>(() => {
                Thread.Sleep(2000);
                return DateTime.Now.ToString();
                }
            );
        }
    }
}
