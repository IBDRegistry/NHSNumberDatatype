using System.Globalization;

namespace NhsNumberDatatype;

public readonly struct NhsNumber : IParsable<NhsNumber>
{
    private static readonly int[] Weights = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    private readonly string _value;

    private NhsNumber(string value)
    {
        _value = value;
    }

    public override string ToString() => string.Join(string.Empty, _value);

    public string ToSpacedString()
    {
        var str = ToString();
        return str[..3] + " " + str.Substring(3, 3) + " " + str.Substring(6, 4);
    }
    
    /// <summary>
    /// Given an NHS Number which may have spaces or dashes, clean it to be a 10 digit number
    /// </summary>
    /// <param name="value"></param>
    private static void CleanNhsNumber(ref string value)
    {
        // Convert the string to a mutable span of characters
        Span<char> span = stackalloc char[value.Length];
        value.AsSpan().CopyTo(span);
        
        var writeIndex = 0;
        
        // Iterate through the original string to populate the span without dashes and spaces
        for (var readIndex = 0; readIndex < span.Length; readIndex++)
        {
            if (span[readIndex] != ' ' && span[readIndex] != '-')
            {
                span[writeIndex++] = span[readIndex];
            }
        }
        
        // Convert the populated span back to a string
        value = new string(span[..writeIndex]);
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

        if (checkDigit != CharUnicodeInfo.GetDecimalDigitValue(value[^1]))
        {
            return false;
        }

        return true;
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
        if (s == null)
        {
            result = default!;
            return false;
        }

        CleanNhsNumber(ref s);

        if (IsValidNhsNumber(s))
        {
            result = new NhsNumber(s);
            return true;
        }

        result = default!;
        return false;
    }
}