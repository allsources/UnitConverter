using System;
using System.Globalization;

namespace UnitConverter.Helpers
{
    public static class Localization
    {
        public static char NumberDecimalSeparator = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
    }
}
