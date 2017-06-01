using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitConverter.Enums;
using UnitConverter.Helpers;

namespace UnitConverter.Tests
{
    [TestClass]
    public class UnitConverterTests
    {
        #region "PRIVATE MEMBERS"

        private readonly UnitConverter _converter = new UnitConverter();

        #endregion

        #region "TEST INPUT"

        [TestMethod]
        public void Input_Empty_Error()
        {
            var result = _converter.Convert(null, null);

            Assert.IsTrue(result.Status == OutputStatus.Error);
        }

        [TestMethod]
        public void Input_SourceWithoutValue_Error()
        {
            var result = _converter.Convert("foot", "inch");

            Assert.IsTrue(result.Status == OutputStatus.Error);
        }

        [TestMethod]
        public void Input_SourceIncorrectValue_Error()
        {
            var result = _converter.Convert("one foot", "inch");

            Assert.IsTrue(result.Status == OutputStatus.Error);
        }

        [TestMethod]
        public void Input_ValidInput_Ok()
        {
            var result = _converter.Convert($"1{Localization.NumberDecimalSeparator}0 foot", "inch");

            Assert.IsTrue(result.Status == OutputStatus.Success);
        }

        #endregion

        #region "TEST SOURCE/TARGET UNIT"

        [TestMethod]
        public void Unit_UnsupportedUnit_Error()
        {
            var result = _converter.Convert($"1{Localization.NumberDecimalSeparator}0 inch", "mile");

            Assert.IsTrue(result.Status == OutputStatus.Error);
        }

        [TestMethod]
        public void Unit_UnsupportedConvertion_Error()
        {
            var result = _converter.Convert($"1{Localization.NumberDecimalSeparator}0 inch", "byte");

            Assert.IsTrue(result.Status == OutputStatus.Error);
        }

        [TestMethod]
        public void Unit_BothEqual_Ok()
        {
            var result = _converter.Convert($"1{Localization.NumberDecimalSeparator}0 inch", "inch");

            Assert.IsTrue(result.Status == OutputStatus.Success);
        }

        [TestMethod]
        public void Unit_ValidUnit_Ok()
        {
            var result = _converter.Convert($"1{Localization.NumberDecimalSeparator}0 inch", "foot");

            Assert.IsTrue(result.Status == OutputStatus.Success);
        }

        #endregion

        #region "TEST TARGET VALUE"

        [TestMethod]
        public void Target_LengthWithTheSameUnit_Ok()
        {
            var result = _converter.Convert($"10{Localization.NumberDecimalSeparator}0 foot", "foot");

            Assert.IsTrue(result.Result == "10 foot");
            Assert.IsTrue(result.Status == OutputStatus.Success);
        }


        [TestMethod]
        public void Target_Length_Ok()
        {
            var result = _converter.Convert($"1000{Localization.NumberDecimalSeparator}0 inch", "metre");

            //Assert.IsTrue(result.Result == "Hello Kitty");
            Assert.IsTrue(result.Status == OutputStatus.Success);
        }

        [TestMethod]
        public void Target_Data_Ok()
        {
            var result = _converter.Convert($"1000{Localization.NumberDecimalSeparator}0 byte", "kilobyte");

            Assert.IsTrue(result.Result == "1 kilobyte");
            Assert.IsTrue(result.Status == OutputStatus.Success);
        }

        #endregion
    }
}
