﻿using System;
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

        /// <summary>
        /// Starts all async handlers in parallel and waits for all of them to complete.
        /// </summary>
        public async Task OnReceivedAsyncTaskWhenAll()
        {
            var handlers = _receivedAsync;
            if (handlers is not null)
            {
                var invocationList = handlers.GetInvocationList();
                var tasks = new Task[invocationList.Length];
                for (int i = 0; i < invocationList.Length; i++)
                {
                    tasks[i] = ((AsyncEventHandler<AsyncEventArgs>)invocationList[i])(this, new AsyncEventArgs() { Index = i });
                }
                await Task.WhenAll(tasks);
            }
        }

        /// <summary>
        /// Starts all async handlers one by one and waits for each of them to complete before starting the next one.
        /// </summary>
        public async Task OnReceivedAsyncAwaitEveryHandler()
        {
            var handlers = _receivedAsync;
            if (handlers is not null)
            {
                var invocationList = handlers.GetInvocationList();
                for (int i = 0; i < invocationList.Length; i++)
                {
                    await ((AsyncEventHandler<AsyncEventArgs>)invocationList[i])(this, new AsyncEventArgs() { Index = i });
                }
            }
        }
    }
}
