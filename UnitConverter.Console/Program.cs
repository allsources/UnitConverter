using System;
using System.Linq;
using UnitConverter.Classes;
using UnitConverter.Enums;

namespace UnitConverter
{
    class Program
    {
        #region "PRIVATE MEMBERS"

        private static UnitConverter _converter;

        private const int InputLenght = 2;

        #endregion

        #region "MAIN"

        static void Main(string[] args)
        {
            Initialization();

            DisplayGreetingMessage();

            UnitConverterLoop();
        }

        #endregion

        #region "INITIALIZATION"

        private static void Initialization()
        {
            _converter = new UnitConverter();
        }

        private static void DisplayGreetingMessage()
        {
            Console.WriteLine("UNIT CONVERTER v.1.0\n");
            Console.WriteLine("Please note, that plural form of a unit is not fully supported.");
            Console.WriteLine("To sign out of the console, type 'exit' and press ENTER.\n");
            Console.WriteLine("Enter a value to convert, for example:");
            Console.WriteLine("\t1.45 kilometres, gigafoot");
        }

        #endregion

        #region "PROCESS"

        private static void UnitConverterLoop()
        {
            var input = string.Empty;

            while(ReadValueToConvert(out input))
            {
                if (ValidateInput(input))
                    DisplayResult(DoConvert(input));
            }
        }

        private static bool ReadValueToConvert(out string input)
        {
            Console.WriteLine();
            Console.Write("> ");
            input = Console.ReadLine();

            return !input.ToLowerInvariant().Equals("exit");
        }

        private static ConverterOutput DoConvert(string input)
        {
            var units = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return _converter.Convert(units[0], units[1]);
        }

        private static void DisplayResult(ConverterOutput output)
        {
            Console.WriteLine(GetResultMessage(output));
        }

        private static string GetResultMessage(ConverterOutput output)
        {
            if (output == null)
                return "Unknown error.";

            if (output.Status == OutputStatus.Error)
                return $"Error: {output.ErrorMessage}";

            return output.Result;
        }

        #region "INPUT VALIDATION"

        private static bool ValidateInput(string input)
        {
            return !IsEmpty(input) && HasCorrectLenght(input);
        }

        private static bool IsEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No units to process.");
                return true;
            }
            return false;
        }

        private static bool HasCorrectLenght(string input)
        {
            var units = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (units.Length != InputLenght && units.All(i => !string.IsNullOrWhiteSpace(i)))
            {
                Console.WriteLine("Incorrect lenght of the input.");
                return false;
            }
            return true;
        }

        #endregion

        #endregion
    }
}
