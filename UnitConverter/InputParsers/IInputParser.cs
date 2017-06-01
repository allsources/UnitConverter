using System;
using UnitConverter.Classes;

namespace UnitConverter.InputParsers
{
    internal interface IInputParser
    {
        Tuple<UnitItem, UnitItem> Parse(string source, string target);
    }
}
