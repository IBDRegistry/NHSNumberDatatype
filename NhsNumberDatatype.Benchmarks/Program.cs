// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using NhsNumberDatatype.Benchmarks;

BenchmarkRunner.Run<ParseBenchmarks>();

// Test();
//
// return;
//
// static void Test()
// {
//     var clean = NhsNumber.CleanNhsNumber("123 456 7881");
//     
//     Console.WriteLine(clean);
// }