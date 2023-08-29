namespace NhsNumberDatatype.Tests;

public class EqualityTests
{
    [Test]
    public void TwoNhsNumbersWithSameValue_AreEqual()
    {
        // Arrange
        var nhsNumber1 = NhsNumber.Parse("123 456 7881");
        var nhsNumber2 = NhsNumber.Parse("123 456 7881");

        // Act
        var areEqual = nhsNumber1 == nhsNumber2;

        // Assert
        Assert.That(areEqual, Is.True);
    }
    
    [Test]
    public void TwoNhsNumbersWithDifferentValue_AreNotEqual()
    {
        // Arrange
        var nhsNumber1 = NhsNumber.Parse("1234567881");
        var nhsNumber2 = NhsNumber.Parse("9435797881");

        // Act
        var areEqual = nhsNumber1 == nhsNumber2;

        // Assert
        Assert.That(areEqual, Is.False);
    }
    
    [Test]
    public void TwoNhsNumbersWithSameValue_HaveSameHashCode()
    {
        // Arrange
        var nhsNumber1 = NhsNumber.Parse("943 579 7881");
        var nhsNumber2 = NhsNumber.Parse("943 579 7881");

        // Act
        var hashCode1 = nhsNumber1.GetHashCode();
        var hashCode2 = nhsNumber2.GetHashCode();

        // Assert
        Assert.That(hashCode2, Is.EqualTo(hashCode1));
    }
}