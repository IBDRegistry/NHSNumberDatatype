using BenchmarkDotNet.Attributes;

namespace NhsNumberDatatype.Benchmarks;

[MemoryDiagnoser(false)]
public class ParseBenchmarks
{
    [Benchmark]
    public void ParseNhsNumber()
    {
        var result = NhsNumber.Parse("943 579 7881");
    }
    
    [Benchmark]
    public void ParseNhsNumberWithValidation()
    {
        Span<char> destination = stackalloc char[10];
        NhsNumber.CleanNhsNumber("943 579 7881", destination);
    }
}