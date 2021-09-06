using System.Collections.Generic;

namespace NHSNumberDatatype
{
    internal static class StringExt
    {
        public static bool IsNumeric(this string text)
        {
            double test;
            return double.TryParse(text, out test);
        }

        public static Digit[] ToArray(this int number)
        {
            List<Digit> listOfInts = new List<Digit>();
            while (number > 0)
            {
                listOfInts.Add(number % 10);
                number = number / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }

        public static Digit[] ToArray(this long number)
        {
            List<Digit> listOfInts = new List<Digit>();
            while (number > 0)
            {
                listOfInts.Add((Digit)(number % 10));
                number = number / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }
    }
}
