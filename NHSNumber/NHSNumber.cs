namespace NhsNumber;

public class NhsNumber : IParsable<NhsNumber>
{
    private readonly string _value;

    public NhsNumber(string value)
    {
        if (!IsValidNhsNumber(value, out var clean))
        {
            throw new ArgumentException("Invalid NHS Number", nameof(value));
        }

        _value = clean;
    }

    public override string ToString() => string.Join(string.Empty, _value);

    public string ToSpacedString()
    {
        var str = ToString();
        return str[..3] + " " + str.Substring(3, 3) + " " + str.Substring(6, 4);
    }

    public static implicit operator NhsNumber(string str) => Parse(str);

    public static NhsNumber Parse(string s, IFormatProvider? provider = null)
    {
        if (string.IsNullOrWhiteSpace(s))
            throw new ArgumentNullException(nameof(s));

        if (!IsValidNhsNumber(s, out var clean))
            throw new FormatException("Invalid NHS Number format.");

        return new NhsNumber(clean);
    }

    public static bool TryParse(string? str, IFormatProvider? provider, out NhsNumber result)
    {
        if (IsValidNhsNumber(str, out var clean))
        {
            result = new NhsNumber(clean);
            return true;
        }

        result = default!;
        return false;
    }

    private static bool IsValidNhsNumber(string? value, out string cleaned)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            cleaned = string.Empty;
            return false;
        }

        // Remove any formatting characters if needed (e.g., dashes, spaces)
        value = value
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty);

        // Check for null or incorrect length
        if (string.IsNullOrWhiteSpace(value) || value.Length != 10)
        {
            cleaned = string.Empty;
            return false;
        }

        // Ensure all characters are digits
        if (value.Any(c => !char.IsDigit(c)))
        {
            cleaned = string.Empty;
            return false;
        }

        // Validate the check digit (last digit in the number)
        var checkDigit = CalculateCheckDigit(value);
        if (checkDigit != int.Parse(value[^1..]))
        {
            cleaned = string.Empty;
            return false;
        }

        cleaned = value;
        return true;
    }

    private static int CalculateCheckDigit(string digits)
    {
        var weights = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var sum = 0;

        // Compute the weighted sum
        for (var i = 0; i < 9; i++)
        {
            sum += (digits[i] - '0') * weights[i];
        }

        // Compute the check digit
        var remainder = sum % 11;
        var checkDigit = 11 - remainder;

        // If the check digit turns out to be 11, it should be converted to 0
        if (checkDigit == 11)
        {
            checkDigit = 0;
        }

        // If the check digit is 10, then the number is invalid (NHS numbers cannot have a check digit of 10)
        if (checkDigit == 10)
        {
            return -1; // Indicates an invalid number
        }

        return checkDigit;
    }
}