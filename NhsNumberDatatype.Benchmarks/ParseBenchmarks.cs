using BenchmarkDotNet.Attributes;

namespace NhsNumberDatatype.Benchmarks;

[MemoryDiagnoser(false)]
public class ParseBenchmarks
{
    [Benchmark]
    public void ParseNhsNumber()
    {
        NhsNumber.Parse("943 579 7881");
    }
}