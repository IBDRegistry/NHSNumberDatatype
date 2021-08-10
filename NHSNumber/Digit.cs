using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHSNumberDatatype
{
    internal readonly struct Digit
    {
        private readonly byte digit;

        public Digit(byte digit)
        {
            if (digit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(digit), "Digit cannot be greater than nine.");
            }
            this.digit = digit;
        }

        public static implicit operator byte(Digit d) => d.digit;
        public static implicit operator string(Digit d) => d.ToString();

        public static explicit operator Digit(byte b) => new Digit(b);
        public static implicit operator Digit(int IntDigit) => new Digit((byte)IntDigit);
        public static explicit operator Digit(string letter) => int.Parse(letter);

        public override string ToString() => $"{digit}";
    }
}
