using System;

namespace AttachedAndDetachedChildTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            // DetachedChildTasks.GoWait();
            // DetachedChildTasks.GoWaitException();
            // DetachedChildTasks.GoResult();

            AttachedChildTasks.GoWaitException();
            

            Console.ReadKey();
        }
    }
}
