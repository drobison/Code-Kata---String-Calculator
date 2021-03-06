﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class StringCalculator
    {
        public int Compute(string input, OperationEnum operation = OperationEnum.Additon)
        {
            var delimiter = GetDelimiters(ref input);

            if (string.IsNullOrEmpty(input)) return 0;

            input = input.Replace("\n", delimiter.First());

            var numbers = input.Split( delimiter.ToArray(), StringSplitOptions.None);

            var sum = 0;
            var negatives = new List<int>();

            bool first = true;
            foreach (var stringNumber in numbers)
            {
                var number = Convert.ToInt32(stringNumber);

                if (number < 0)
                {
                    negatives.Add(number);
                    continue;
                }
                if (number > 1000) continue;
                if (first)
                {
                    first = false;
                    sum = number;
                }
                else
                {
                    sum = PerformOperation(operation, number, sum);
                }
                
            }

            if (negatives.Count > 0) throw new Exception($"negatives not allowed {string.Join(", ", negatives.Select(x => x))}");

            return sum;
        }

        private int PerformOperation(OperationEnum operation, int number, int sum)
        {
            switch (operation)
            {
                case OperationEnum.Additon:
                    return sum + number;
                case OperationEnum.Subtraction:
                    return sum - number;
                case OperationEnum.Multiplication:
                    return sum * number;
                case OperationEnum.Division:
                    return sum / number;
                default:
                    throw new Exception("Unsupported operation");

            }
        }

        /// <summary>
        /// Determines if delimiter have been set that should be used.  If so, determine what it/they is/are and remove that portion from the input string.
        /// If no delimiter has been specified, defaults to a comma as the seperator.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private List<string> GetDelimiters(ref string input)
        {
            // case with no delimiter specified, use comma default
            if (!input.StartsWith("//"))
            {
                return new List<string>(){ "," };
            }

            var delimiterInput = input.Substring(2, input.IndexOf("\n", StringComparison.Ordinal) - 2);
            input = input.Substring(input.IndexOf("\n", StringComparison.Ordinal) + 1);

            // case single delimtter, no brackets
            if (!delimiterInput.Contains("["))
            {
                return new List<string>() {delimiterInput};
            }

            // case with brackets
            const string pattern = @"\[(.*?)\]";
            var matches = Regex.Matches(delimiterInput, pattern);
            var delimiters = new List<string>();
            foreach (Match match in matches)
            {
                // for now assume there is only 1, more to come
                delimiters.Add(match.Groups[1].Value);
            }

            return delimiters;

        }
    }
}
