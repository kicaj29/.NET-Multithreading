using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncEventHandlerExample
{
    public delegate Task AsyncEventHandler<in AsyncEventArgs>(object sender, AsyncEventArgs e);

    public class AsyncEventHandlerOwner
    {
        private AsyncEventHandler<AsyncEventArgs>? _receivedAsync;

        public event AsyncEventHandler<AsyncEventArgs> ReceivedAsync
        {
            add
            {
                _receivedAsync += value;
            }
            remove
            {
                _receivedAsync -= value;
            }
        }

        public async Task OnReceivedAsyncVer1(AsyncEventArgs e)
        {
            var handlers = _receivedAsync;
            if (handlers is not null)
            {
                var invocationList = handlers.GetInvocationList();
                var tasks = new Task[invocationList.Length];
                for (int i = 0; i < invocationList.Length; i++)
                {
                    tasks[i] = ((AsyncEventHandler<AsyncEventArgs>)invocationList[i])(this, e);
                }
                await Task.WhenAll(tasks);
            }
        }

        public async Task OnReceivedAsyncVer2(AsyncEventArgs e)
        {
            var handlers = _receivedAsync;
            if (handlers is not null)
            {
                var invocationList = handlers.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    await ((AsyncEventHandler<AsyncEventArgs>)invocationList[i])(this, e);
                }
            }
        }
    }
}
