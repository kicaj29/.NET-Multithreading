using TaskWhenAll;

Console.WriteLine("Hello, World!");

TaskWhenAllTest taskWhenAllTest = new TaskWhenAllTest();
await taskWhenAllTest.StartNewJob();

Console.WriteLine("Done!");