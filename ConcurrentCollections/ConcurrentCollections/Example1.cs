using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    public class Example1
    {
        private readonly ConcurrentDictionary<string, ConcurrentBag<MyMessage>> _dict;

        public Example1()
        {
            _dict = new ConcurrentDictionary<string, ConcurrentBag<MyMessage>>();
        }

        private async Task AddMessage(MyMessage message, int index)
        {
            await Task.Run(() =>
            {
                _dict.TryAdd(message.RequestId, new ConcurrentBag<MyMessage>());
                _dict[message.RequestId].Add(message);
                Console.WriteLine($"Current count: {_dict[message.RequestId].Count} for index: {index}");
            });
        }

        private async Task<MyMessage> TakeMessage(string requestId)
        {
            return await Task.Run<MyMessage>(async () =>
            {
                //await Task.Delay(10);
                MyMessage result = null;
                while(result == null)
                {
                    if (!_dict[requestId].TryTake(out result))
                    {
                        await Task.Delay(10);
                    }
                    Console.WriteLine($"Count after removal: {_dict[requestId].Count}");
                }
                return result;
            });
        }

        public async Task Run()
        {
            Task[] addMessageTasks = new Task[100];
            Task[] removeMessageTasks = new Task[100];

            MyMessage message = new MyMessage()
            {
                RequestId = Guid.NewGuid().ToString()
            };


            for(int index = 0; index < 100; index++)
            {
                addMessageTasks[index] = AddMessage(message, index);
                removeMessageTasks[index] = TakeMessage(message.RequestId);
            }

            List<Task> allTasks = new List<Task>();
            allTasks.AddRange(addMessageTasks);
            allTasks.AddRange(removeMessageTasks);

            await Task.WhenAll(allTasks);

            Console.WriteLine($"Amount of elements in the bag: {_dict[message.RequestId].Count}");
        }
    }
}
