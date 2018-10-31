using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class StringCalculator
    {
        public int Add(string input)
        {
            var delimiter = GetDelimiter(ref input);

            if (string.IsNullOrEmpty(input)) return 0;

            input = input.Replace("\n", delimiter);

            var numbers = input.Split(new [] { delimiter }, StringSplitOptions.None);

            var sum = 0;
            var negatives = new List<int>();

            foreach (var stringNumber in numbers)
            {
                var number = Convert.ToInt32(stringNumber);

                if (number < 0)
                {
                    negatives.Add(number);
                    continue;
                }
                if (number > 1000) continue;

                sum += Convert.ToInt32(number);
            }

            if (negatives.Count > 0) throw new Exception($"negatives not allowed {string.Join(", ", negatives.Select(x => x))}");

            return sum;
        }

        /// <summary>
        /// Determines if an explict delimiter has been set that should be used.  If one has, determine what it is and remove that portion from the input string.
        /// If no delimiter has been specified, defaults to a comma as the seperator.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetDelimiter(ref string input)
        {
            // case with no delimiter specified, use comma default
            if (!input.StartsWith("//"))
            {
                return ",";
            }

            var delimiterInput = input.Substring(2, input.IndexOf("\n", StringComparison.Ordinal) - 2);
            input = input.Substring(input.IndexOf("\n", StringComparison.Ordinal) + 1);

            // case single delimtter, no brackets
            if (!delimiterInput.Contains("["))
            {
                return delimiterInput;
            }

            // case with brackets
            const string pattern = @"\[(.*?)\]";
            var matches = Regex.Matches(delimiterInput, pattern);
            foreach (Match match in matches)
            {
                // for now assume there is only 1, more to come
                return match.Groups[1].Value;
            }

            throw new Exception("Delimiter not found");
        }
    }
}
