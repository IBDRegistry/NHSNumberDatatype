// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using NhsNumberDatatype;
using NhsNumberDatatype.Benchmarks;

// BenchmarkRunner.Run<ParseBenchmarks>();

Test();

return;

static void Test()
{
    Span<char> destination = stackalloc char[10];
    NhsNumber.CleanNhsNumber("123 456 7881", destination);
    
    Console.WriteLine(destination.ToString());
}