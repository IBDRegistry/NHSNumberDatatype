using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHSNumberDatatype
{
    public class NHSNumber
    {
        private Digit[] value = new Digit[9];

        public static implicit operator NHSNumber(string StringToUse) => new NHSNumber(StringToUse);
        public static explicit operator string(NHSNumber NHSNumberToCast) => NHSNumberToCast.GetNHSNumberAsString();

        public static implicit operator NHSNumber(int IntToConvert) => new NHSNumber(IntToConvert);

        private string GetNHSNumberAsString()
        {
            string[] LetterArrayOfNHSNumber = Array.ConvertAll(value, item => (string)item);
            return string.Join("", LetterArrayOfNHSNumber);
        }
        private string FormatNHSNumber()
        {
            string StringOfValue = GetNHSNumberAsString();
            return StringOfValue.Substring(0, 3) + " " + StringOfValue.Substring(3, 3) + " " + StringOfValue.Substring(6, 4);
        }
        public NHSNumber(string StringNHSNumber)
        {
            #region "Exceptions"
            if (StringNHSNumber.Contains(" "))
            {
                StringNHSNumber = StringNHSNumber.Replace(" ", "");
            }
            if (StringNHSNumber.IsNumeric() != true)
            {
                throw new TypeLoadException("NHS Number was not numeric");
            }
            if (StringNHSNumber.Length != 10)
            {
                throw new TypeLoadException("NHS Number was not 10 digits long");
            }
            #endregion

            char[] CharArray = StringNHSNumber.ToCharArray();
            string[] letters = Array.ConvertAll(CharArray, item => item.ToString());
            Digit[] digits = Array.ConvertAll(letters, item => (Digit)item);
            value = digits;
        }

        public NHSNumber(int IntNHSNumber)
        {
            value = IntNHSNumber.ToArray();
        }

        public NHSNumber(long LongNHSNumber)
        {
            value = LongNHSNumber.ToArray();
        }

        public override string ToString()
        {
            return FormatNHSNumber();
        }

    }
}
