namespace NhsNumberDatatype.Tests;

public class NhsNumberTests
{
    private readonly string[] _validNhsNumbers =
    {
        "943 579 7881",
        "943 579 2103",
        "943 574 0820",
        "943 573 2992",
        "943 574 9895",
        "943 578 3309",
        "943 574 1894",
        "943 577 5039",
        "943 576 4428",
        "943 576 4479",
        "556 000 6033",
        "943 580 7194",
        "943 578 0156",
        "943 575 8649",
        "943 581 7777",
        "943 572 6097",
        "943 579 2170",
        "943 576 4126",
        "943 578 0334",
        "943 578 6766",
        "943 573 7048",
        "943 577 3982",
        "943578 9102",
        "943581 5065",
        "943580 6430",
        "943573 8540",
        "943577 2846",
        "943581 0624",
        "943575 4422",
        "943572 6755",
        "943580 1684",
        "943580 2508",
        "943575 3868",
        "9435756018",
        "9435809812",
        "9435714463",
        "9435719104",
        "9435762263",
        "9435806236",
        "9435722067",
        "9435814743",
        "9435777066",
        "9435797237",
        "9435769047",
        "9435753973",
    };
    
    [Test]
    public void AllValidNhsStrings_Constructor_CleansString()
    {
        // Assert
        foreach (var nhsNumber in _validNhsNumbers)
        {
            var result = new NhsNumber(nhsNumber);
            Assert.That(result.ToString(), Is.EqualTo(nhsNumber.Replace(" ", string.Empty)));
        }
    }
    
    [Test]
    public void AllValidNhsStrings_WithoutSpaces_ReturnValidNhsNumber()
    {
        // Assert
        foreach (var nhsNumber in _validNhsNumbers)
        {
            var nhsNumberNoSpaces = nhsNumber.Replace(" ", string.Empty);
            var result = NhsNumber.Parse(nhsNumberNoSpaces);
            
            Assert.Multiple(() =>
            {
                Assert.That(result.ToString(), Is.EqualTo(nhsNumberNoSpaces));
                Assert.That(result.IsValid, Is.True);
            });
        }
    }
    
    [Test]
    public void AllValidNhsStrings_WithSpaces_ReturnValidNhsNumber()
    {
        // Assert
        foreach (var nhsNumber in _validNhsNumbers)
        {
            var result = NhsNumber.Parse(nhsNumber);
            Assert.That(result.ToString(), Is.EqualTo(nhsNumber.Replace(" ", string.Empty)));
        }
    }

    [Test]
    public void TryParse_StringWithNumbers_ReturnsFalse()
    {
        // Arrange
        const string str = "123 abc 7890";

        // Act
        var parsed = NhsNumber.TryParse(str, null, out var result);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.False);
            Assert.That(result, Is.EqualTo(default(NhsNumber)));
        });
    }

    [Test]
    public void TryParse_WithEmptyString_ReturnsFalse()
    {
        // Arrange
        const string str = "";
        
        // Act
        var parsed = NhsNumber.TryParse(str, null, out var result);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.False);
            Assert.That(result, Is.EqualTo(default(NhsNumber)));
        });
    }

    [Test]
    public void TryParse_With11Characters_ReturnsFalse()
    {
        // Arrange
        const string str = "12345678901";
        
        // Act
        var parsed = NhsNumber.TryParse(str, null, out var result);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.False);
        });
    }
    
    [Test]
    public void TryParse_With11CharactersAndSpaces_ReturnsFalse()
    {
        // Arrange
        const string str = "123 456 78901";
        
        // Act
        var parsed = NhsNumber.TryParse(str, null, out var result);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Is.False);
            Assert.That(parsed.ToString(), Is.EqualTo(str));
        });
    }
    
    [Test]
    public void GetHashCode_WithValue_DoesntThrow()
    {
        // Arrange
        var nhsNumber = NhsNumber.Parse("123 456 7881");
        
        // Act
        Assert.DoesNotThrow(() =>
        {
            var hashCode = nhsNumber.GetHashCode();
        });
    }
}