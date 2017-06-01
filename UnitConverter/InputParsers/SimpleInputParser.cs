using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitConverter.BO;
using UnitConverter.Classes;

namespace UnitConverter.InputParsers
{
    internal class SimpleInputParser : IInputParser
    {
        #region "PRIVATE MEMBERS"

        private readonly Prefixes _prefixes;

        private readonly IEnumerable<string> _units;

        #endregion

        #region ".ctor"

        public SimpleInputParser(Prefixes prefixes, IEnumerable<string> units)
        {
            _prefixes = prefixes;
            _units = units;
        }

        #endregion

        #region "INTERFACE METHODS IMPLEMENTATION"

        public Tuple<UnitItem, UnitItem> Parse(string source, string target)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target))
                throw new Exception("Both units of source and target must be specified.");

            source = source.ToLower().Trim();

            target = target.ToLower().Trim();

            UnitItem s = null, t = null;

            Task.WaitAll(new Task[]
            {
                Task.Factory.StartNew(() => { s = ParseSource(source); }),
                Task.Factory.StartNew(() => { t = ParseTarget(target); }),
            });

            return Tuple.Create(s, t);
        }

        #endregion

        #region "PRIVATE METHODS"

        private UnitItem ParseSource(string source)
        {
            if (!ValidateSource(source))
                throw new Exception("Source value has incorrect format.");

            var unitValue = ParseUnitValue(source);

            var prefix = ParsePrefix(source);

            var unit = ParseUnit(source);

            return new UnitItem
            {
                Value = unitValue,
                Multiplier = _prefixes.GetMultiplier(prefix),
                Prefix = prefix,
                Unit = unit,
            };
        }

        private UnitItem ParseTarget(string target)
        {
            if (!ValidateTarget(target))
                throw new Exception("Target value has incorrect format.");

            var prefix = ParsePrefix(target);

            var unit = ParseUnit(target);

            return new UnitItem
            {
                Multiplier = _prefixes.GetMultiplier(prefix),
                Prefix = prefix,
                Unit = unit,
            };
        }

        private bool ValidateSource(string source)
        {
            return new Regex(@"^\d+(\.\d+){0,1}\s{1}\w+$").IsMatch(source);
        }

        private bool ValidateTarget(string target)
        {
            return new Regex(@"^\w+$").IsMatch(target);
        }

        private double ParseUnitValue(string source)
        {
            double value = 0;

            var valueRaw = Regex.Matches(source, @"^\d+(\.\d+){0,1}")[0].Groups[0].Value;

            if (!double.TryParse(valueRaw, out value))
                throw new Exception("Source value has incorrect format.");

            return value;
        }

        private string ParsePrefix(string source)
        {
            var prefixRaw = Regex.Matches(source, @"\w+$")[0].Groups[0].Value;

            var prefix = _prefixes.Data.FirstOrDefault(i => prefixRaw.StartsWith(i.Key)).Key;

            return prefix;
        }

        private string ParseUnit(string source)
        {
            var unitRaw = Regex.Matches(source, @"\w+$")[0].Groups[0].Value;

            var unit = _units.FirstOrDefault(i => unitRaw.Contains(i));

            if (string.IsNullOrWhiteSpace(unit))
                throw new Exception("Specified unit is not supported.");

            return unit;
        }

        #endregion
    }
}
