using System;
using UnitConverter.BO;
using UnitConverter.Classes;

namespace UnitConverter.UnitProcessors
{
    internal class UnitProcessor
    {
        #region "PRIVATE MEMBERS"

        private readonly UnitRatio _unitRatio;

        #endregion

        #region ".ctor"

        public UnitProcessor(UnitRatio unitRatio)
        {
            _unitRatio = unitRatio;
        }

        #endregion

        #region "PUBLIC METHODS"

        public void Convert(UnitItem source, UnitItem target)
        {
            if (!_unitRatio.CanConvert(source.Unit, target.Unit))
                throw new ArgumentException("The type of Source and Target units are different.");

            var factor = _unitRatio.GetFactor(source.Unit, target.Unit);

            try
            {
                target.Value = source.Value * Math.Pow(10, source.Multiplier) * factor / Math.Pow(10, target.Multiplier);
            }
            catch (OverflowException)
            {
                throw new OverflowException("The result value is too large or too small. Try to specify other units of measurement.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
