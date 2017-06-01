using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UnitConverter.BO
{
    /// <summary>
    /// Represents a collection of the SI Prefixes.
    /// </summary>
    internal class Prefixes
    {
        #region "PRIVATE MEMBERS"

        private static ReadOnlyDictionary<string, int> _prefixes;

        #endregion

        #region "PUBLIC MEMBERS"

        public ReadOnlyDictionary<string, int> Data { get { return _prefixes; } }

        #endregion

        #region ".ctor"

        public Prefixes()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _prefixes = new ReadOnlyDictionary<string, int>(new Dictionary<string, int>
            {
                { "yotta", 24 },
                { "zetta", 21 },
                { "exa", 18 },
                { "peta", 15 },
                { "tera", 12 },
                { "giga", 9 },
                { "mega", 6 },
                { "kilo", 3 },
                { "hecto", 2 },
                { "deca", 1 },
                { "deci", -1 },
                { "centi", -2 },
                { "milli", -3 },
                { "micro", -6 },
                { "nano", -9 },
                { "pico", -12 },
                { "femto", -15 },
                { "atto", -18 },
                { "zepto", -21 },
                { "yocto", -24 },
            });
        }

        #endregion

        #region "PUBLIC METHODS"

        public int GetMultiplier(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                return 0;

            prefix = prefix.ToLower().Trim();

            if (_prefixes.ContainsKey(prefix))
                return _prefixes[prefix];

            return 0;
        }

        #endregion
    }
}
