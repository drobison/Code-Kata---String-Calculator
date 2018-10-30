using System;

namespace Calculator
{
    public class StringCalculator
    {
        public int Add(string input)
        {
            var delimiter = GetDelimiter(ref input);

            if (string.IsNullOrEmpty(input)) return 0;

            input = input.Replace("\n", delimiter.ToString());

            var numbers = input.Split(delimiter[0]);

            int sum = 0;

            foreach (var number in numbers)
            {
                sum += Convert.ToInt32(number);
            }

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
            if (input.StartsWith("//"))
            {
                var delimter = input.Substring(2, 1);
                input = input.Remove(0,4);
                return delimter;
            }

            return ",";
        }
    }
}
