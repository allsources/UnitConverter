using UnitConverter.Enums;

namespace UnitConverter.Classes
{
    /// <summary>
    /// Represents an output object of the conversion process.
    /// </summary>
    public class ConverterOutput
    {
        /// <summary>
        /// Converted value.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// A text of the error which may occur during conversion.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// <see cref="OutputStatus"/>
        /// </summary>
        public OutputStatus Status { get; set; }
    }
}
