using System;

namespace Calculator
{
    public class StringCalculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) return 0;

            var delimitter = ',';

            var numbers = input.Split(delimitter);

            int sum = 0;

            foreach (var number in numbers)
            {
                sum += Convert.ToInt32(number);
            }

            return sum;
        }
    }
}
