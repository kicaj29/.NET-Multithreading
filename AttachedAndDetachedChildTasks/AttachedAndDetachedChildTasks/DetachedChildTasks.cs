using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AttachedAndDetachedChildTasks
{
    public class DetachedChildTasks
    {
        public static void GoWait()
        {
            var parent = Task.Factory.StartNew(() => {
                Console.WriteLine("Outer task executing.");

                var child = Task<int>.Factory.StartNew(() => {                    
                    Console.WriteLine("Nested task starting.");
                    //Thread.SpinWait(500000);
                    Thread.Sleep(2000);
                    Console.WriteLine("Nested task completing.");
                    return 42;
                });
                Console.WriteLine("Waiting on child task");
                child.Wait();
                Console.WriteLine("Child task is finished");
            });
            Thread.Sleep(1000);
            // parent finishes before the child deatched task
            parent.Wait();
            Console.WriteLine("Outer has completed.");
        }

        // This will generate output
        // Outer has completed.
        // Outer task executing.
        // Parent waiting on result
        // Nested task starting.
        // Nested task completing.
        // Parent result: 42
        public static void GoResult()
        {
            var parent = Task.Factory.StartNew(() => {
                Console.WriteLine("Outer task executing.");

                var child = Task<int>.Factory.StartNew(() => {
                    Console.WriteLine("Nested task starting.");
                    //Thread.SpinWait(500000);
                    Thread.Sleep(2000);
                    Console.WriteLine("Nested task completing.");
                    return 42;
                });

                // If the child task is represented by a Task<TResult> object rather than a Task object,
                // you can ensure that the parent task will wait for the child to complete by accessing the Task<TResult>.Result
                // property of the child even if it is a detached child task
                Console.WriteLine("Parent waiting on result");
                var r = child.Result;
                Console.WriteLine("Parent result: " + r.ToString());
            });

            // parent waits on the detached child
            Console.WriteLine("Outer has completed.");
        }


        public static void GoWaitException()
        {
            var parent = Task.Factory.StartNew(() => {
                Console.WriteLine("Outer task executing.");
                throw new Exception("Parent task exception");
                var child = Task<int>.Factory.StartNew(() =>
                {
                    Console.WriteLine("Nested task starting.");
                    //Thread.SpinWait(500000);
                    Thread.Sleep(2000);
                    Console.WriteLine("Nested task completing.");
                    // this exception is not caught by the main thread because main thread finishes earlier                        
                    throw new Exception("Child task exception");
                });
            });
            Thread.Sleep(1000);
            try 
            {
                parent.Wait();
            }
            catch(Exception ex)
            {
                // it is caught as instnace of AggregateException
                // more in docs: https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/exception-handling-task-parallel-library#exceptions-from-detached-child-tasks
                Console.WriteLine(ex.Message);
            }
            
            Console.WriteLine("Outer has completed.");
        }
    }
}
