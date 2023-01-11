// See https://aka.ms/new-console-template for more information
using ReturnAwaitVsReturnReadyData;
using System.Diagnostics;

Console.WriteLine("Hello, World!");

TestClass tc = new TestClass();

Stopwatch sw = new Stopwatch();
sw.Start();
Console.WriteLine("Before t1: " + sw.Elapsed.ToString());
Task<string> t1 = tc.ReturnString();
Console.WriteLine("After t1: " + sw.Elapsed.ToString());
if (t1.IsCompleted)
{
    Console.WriteLine(tc.ReturnString().Result);
}

Console.WriteLine("Before t2: " + sw.Elapsed.ToString());
Task<string> t2 = tc.ReturnTaskString();
Console.WriteLine("After t2: " + sw.Elapsed.ToString());
if (t2.IsCompleted)
{
    Console.WriteLine(tc.ReturnTaskString().Result);
}

Console.WriteLine("Before s2: " + sw.Elapsed.ToString());
string s2 = await tc.ReturnTaskString();
Console.WriteLine("After s2: " + sw.Elapsed.ToString());


Console.WriteLine("Before s3: " + sw.Elapsed.ToString());
string s3 = await tc.ReturnTaskStringWithoutFirstAwait();
Console.WriteLine("After s3: " + sw.Elapsed.ToString());
if (t2.IsCompleted)
{
    Console.WriteLine(tc.ReturnTaskStringWithoutFirstAwait().Result);
}

Console.WriteLine("Before s4: " + sw.Elapsed.ToString());
string s4 = await tc.ReturnTaskStringNoAwaitAtAll();
Console.WriteLine("After s4: " + sw.Elapsed.ToString());
if (t2.IsCompleted)
{
    Console.WriteLine(tc.ReturnTaskStringNoAwaitAtAll().Result);
}

Console.WriteLine("DONE");

Console.ReadKey();