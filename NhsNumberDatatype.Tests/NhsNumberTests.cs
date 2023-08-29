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
        "943 578 9102",
        "943 581 5065",
        "943 580 6430",
        "943 573 8540",
        "943 577 2846",
        "943 581 0624",
        "943 575 4422",
        "943 572 6755",
        "943 580 1684",
        "943 580 2508",
        "943 575 3868",
        "943 575 6018",
        "943 580 9812",
        "943 571 4463",
        "943 571 9104",
        "943 576 2263",
        "943 580 6236",
        "943 572 2067",
        "943 581 4743",
        "943 577 7066",
        "943 579 7237",
        "943 576 9047",
        "943 575 3973",
    };

    [Test]
    public void AllValidNhsStrings_ReturnValidNhsNumber()
    {
        // Assert
        foreach (var nhsNumber in _validNhsNumbers)
        {
            var result = NhsNumber.Parse(nhsNumber);
            Assert.That(result.ToSpacedString(), Is.EqualTo(nhsNumber));
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
}