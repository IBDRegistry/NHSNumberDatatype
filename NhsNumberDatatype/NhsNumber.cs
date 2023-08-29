using System.Globalization;

namespace NhsNumberDatatype;

public readonly struct NhsNumber : ISpanParsable<NhsNumber>
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

    public static NhsNumber Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
    {
        if (!TryParse(s, provider, out var result))
        {
            throw new FormatException("Invalid NHS Number");
        }

        return result;
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out NhsNumber result)
    {
        var clean = CleanNhsNumber(s);

        if (IsValidNhsNumber(clean))
        {
            result = new NhsNumber(clean.ToString());
            return true;
        }

        result = default!;
        return false;
    }

    private static ReadOnlySpan<char> CleanNhsNumber(ReadOnlySpan<char> value)
    {

        var newSpan = new char[10];
        var newIndex = 0;

        for (var i = 0; i < value.Length && newIndex < 10; i++)
        {
            var current = value[i];
            if (current != ' ' && current != '-')
            {
                newSpan[newIndex++] = current;
            }
        }

        // Ensure the span is exactly 10 characters long
        if (newIndex != 10)
        {
            throw new ArgumentException("The cleaned NHS number must be exactly 10 digits.");
        }

        return newSpan;
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
        return Parse(s.AsSpan(), provider);
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out NhsNumber result)
    {
        return TryParse(s.AsSpan(), provider, out result);
    }

    public static implicit operator NhsNumber(string str) => Parse(str);
    public static implicit operator string(NhsNumber nhsNumber) => nhsNumber.ToString();
    public static implicit operator NhsNumber(int intValue) => new(intValue.ToString());
}