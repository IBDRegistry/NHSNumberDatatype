namespace NhsNumberDatatype.Tests;

public class DefaultValueTests
{
    [Test]
    public void DefaultValue_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            var val = NhsNumber.None;
        });
    }

    [Test]
    public void DefaultValue_HasSomeValue()
    {
        var val = NhsNumber.None;
        Assert.That(string.IsNullOrEmpty(val.ToString()), Is.False);
    }
}