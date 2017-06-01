using System;
using System.Linq;
using UnitConverter.BO;
using UnitConverter.Classes;
using UnitConverter.Enums;
using UnitConverter.InputParsers;
using UnitConverter.UnitProcessors;

namespace UnitConverter
{
    /// <summary>
    /// Represents a unit converter.
    /// </summary>
    public class UnitConverter
    {
        #region "PRIVATE MEMBERS"

        private readonly Prefixes _prefixes;

        private readonly UnitRatio _unitRatio;

        private readonly IInputParser _inputParser;

        private readonly UnitProcessor _unitProcessor;

        #endregion

        #region ".ctor"

        public UnitConverter()
        {
            _prefixes = new Prefixes();
            _unitRatio = new UnitRatio();
            _inputParser = new SimpleInputParser(_prefixes, _unitRatio.Units);
            _unitProcessor = new UnitProcessor(_unitRatio);
        }

        #endregion

        #region "PUBLIC MEMBERS"

        public ConverterOutput Convert(string source, string target)
        {
            var output = new ConverterOutput();

            try
            {
                var valueToConvert = _inputParser.Parse(source, target);

                _unitProcessor.Convert(valueToConvert.Item1, valueToConvert.Item2);

                var targetUnit = valueToConvert.Item2;

                output.Result = $"{targetUnit.Value} {targetUnit.Prefix}{targetUnit.Unit}";
            }
            catch(AggregateException ex)
            {
                var err = ex.Flatten()?.InnerExceptions?.Select(i => i.Message);
                output.ErrorMessage = err != null && err.Any() ? string.Join(" | ", err) : "Unknow error.";
            }
            catch (Exception ex)
            {
                output.ErrorMessage = ex.Message;
            }
            finally
            {
                output.Status =
                    string.IsNullOrWhiteSpace(output.ErrorMessage) ? OutputStatus.Success : OutputStatus.Error;
            }

            return output;
        }

        #endregion
    }
}
