using BenchmarkDotNet.Attributes;

namespace NhsNumber.Benchmarks;

[MemoryDiagnoser(false)]
public class ParseBenchmarks
{
    [Benchmark]
    public void ParseNhsNumber()
    {
        var result = NhsNumber.Parse("943 579 7881".AsSpan());
    }
}