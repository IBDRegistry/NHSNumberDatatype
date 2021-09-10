# NHSNumberDatatype
A .NET datatype for NHS Numbers that doesn't lose the leading zero


    NHSNumber NHSNumberFromLong = 8019970231;

    Console.WriteLine((string)NHSNumberFromLong);
    //8019970231

    Console.WriteLine(NHSNumberFromLong.ToString());
    //801 997 0231

    NHSNumber NHSNumberFromIntWithLeadingZero = 0754575063;

    Console.WriteLine(NHSNumberFromIntWithLeadingZero);
    //075 457 5063

    NHSNumber NHSNumberFromString = "8019970321";
    Console.WriteLine(NHSNumberFromString);
    //801 997 0231
