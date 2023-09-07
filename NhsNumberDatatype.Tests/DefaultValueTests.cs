namespace NhsNumberDatatype.Tests;

public class DefaultValueTests
{
    [Test]
    public void DefaultValue_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            var val = default(NhsNumber);
        });
    }
}