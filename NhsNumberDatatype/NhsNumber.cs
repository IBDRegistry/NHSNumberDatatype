using System.Globalization;

namespace NhsNumberDatatype;

public readonly struct NhsNumber : IParsable<NhsNumber>, IEquatable<NhsNumber>
{
    private static readonly int[] Weights = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    private readonly string _value;

    public bool IsValid { get; }
    
    public NhsNumber(string value)
    {
        _value = CleanNhsNumber(value);
        IsValid = IsValidNhsNumber(_value);
    }

    public override string ToString() => _value;

    public string ToSpacedString()
    {
        return _value[..3] + " " + _value.Substring(3, 3) + " " + _value.Substring(6, 4);
    }

    /// <summary>
    /// Given an NHS Number which may have spaces or dashes, clean it to be a 10 digit number
    /// </summary>
    /// <param name="value"></param>
    public static string CleanNhsNumber(string value)
    {
        value = value.Trim();
        value = value.Replace(" ", string.Empty);
        value = value.Replace("-", string.Empty);

        return value;
    }
    
    private static bool IsValidNhsNumber(ReadOnlySpan<char> value)
    {
        if (value.IsWhiteSpace())
            return false;

        // Check for null or incorrect length
        if (value.Length != 10)
            return false;

        // Ensure all characters are digits
        foreach (var c in value)
        {
            if (char.IsDigit(c))
                continue;

            return false;
        }

        // Validate the check digit (last digit in the number)
        var checkDigit = CalculateCheckDigit(value);

        return checkDigit == CharUnicodeInfo.GetDecimalDigitValue(value[^1]);
    }

    private static int CalculateCheckDigit(ReadOnlySpan<char> digits)
    {
        var sum = 0;

        // Compute the weighted sum
        for (var i = 0; i < 9; i++)
        {
            sum += (digits[i] - '0') * Weights[i];
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

    public static NhsNumber Parse(string s, IFormatProvider? provider = null)
    {
        if (!TryParse(s, provider, out var result))
        {
            throw new FormatException("Invalid NHS Number");
        }

        return result;
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out NhsNumber result)
    {
        if (s is null)
        {
            result = default;
            return false;
        }

        s = CleanNhsNumber(s);

        if (IsValidNhsNumber(s))
        {
            result = new NhsNumber(s);
            return true;
        }

        result = default;
        return false;
    }

    public bool Equals(NhsNumber other)
    {
        return _value == other._value;
    }

    public override bool Equals(object? obj)
    {
        return obj is NhsNumber other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
    
    public static bool operator ==(NhsNumber left, NhsNumber right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NhsNumber left, NhsNumber right)
    {
        return !(left == right);
    }
}