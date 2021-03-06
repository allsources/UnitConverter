﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnitConverter.Classes;
using UnitConverter.Enums;
using UnitConverter.Helpers;

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
            Console.WriteLine($"\t1{Localization.NumberDecimalSeparator}45 kilometres, gigafoot");
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

            return
                Localization.NumberDecimalSeparator == '.'
                    ? _converter.Convert(units[0], units[1])
                    : _converter.Convert(string.Concat(units[0], units[1]), units[2]);
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
            return new Regex(string.Concat(@"^\d+(\", Localization.NumberDecimalSeparator, @"\d+){0,1}\s{1}\w+\,\s+\w+$")).IsMatch(input);
        }

        #endregion

        #endregion
    }
}
