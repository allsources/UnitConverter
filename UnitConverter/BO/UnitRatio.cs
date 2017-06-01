using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UnitConverter.BO
{
    /// <summary>
    /// Represents a collection of unints ratio.
    /// </summary>
    internal class UnitRatio
    {
        #region "PRIVATE MEMBERS"

        private static ReadOnlyDictionary<Tuple<string, string>, double> _unitRatio;

        #endregion

        #region "PUBLIC MEMBERS"

        public ReadOnlyDictionary<Tuple<string, string>, double> Data { get { return _unitRatio; } }

        public ReadOnlyCollection<string> Units { get { return GetUnits(); } }

        #endregion

        #region ".ctor"

        public UnitRatio()
        {
            Initialize();
        }

        private void Initialize()
        {
            _unitRatio = new ReadOnlyDictionary<Tuple<string, string>, double>(new Dictionary<Tuple<string, string>, double>
            {
                #region "LENGTH"

                { Tuple.Create("metre", "inch"), 39.37010 },
                { Tuple.Create("metre", "foot"),  3.28084 },
                { Tuple.Create("foot", "inch"),  12.00000 },

                #endregion

                #region "DATA"

                { Tuple.Create("byte", "bit"), 8 },

                #endregion
            });
        }

        #endregion

        #region "PUBLIC METHODS"

        public bool CanConvert(string source, string target)
        {
            return
                !string.IsNullOrWhiteSpace(source) &&
                !string.IsNullOrWhiteSpace(target) &&
                (source.Equals(target) ||
                 _unitRatio.ContainsKey(Tuple.Create(source, target)) || _unitRatio.ContainsKey(Tuple.Create(target, source)));
        }

        public double GetFactor(string source, string target)
        {
            if (source.Equals(target, StringComparison.OrdinalIgnoreCase))
                return 1.00;

            var keyForward = Tuple.Create(source, target);

            var keyBackward = Tuple.Create(target, source);

            if (!_unitRatio.ContainsKey(keyForward) && !_unitRatio.ContainsKey(keyBackward))
                throw new ArgumentException("Unsupported ratio of units.");

            double factor;

            if (_unitRatio.TryGetValue(keyForward, out factor))
                return factor;

            return 1 / _unitRatio[keyBackward];
        }

        #endregion

        #region "PRIVATE METHODS"

        private ReadOnlyCollection<string> GetUnits()
        {
            return new ReadOnlyCollection<string>(
                _unitRatio.Select(i => i.Key.Item1).Union(_unitRatio.Select(i => i.Key.Item2)).Distinct().ToList());
        }

        #endregion
    }
}
