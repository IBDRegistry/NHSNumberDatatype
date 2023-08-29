// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using NhsNumberDatatype;
using NhsNumberDatatype.Benchmarks;

BenchmarkRunner.Run<ParseBenchmarks>();