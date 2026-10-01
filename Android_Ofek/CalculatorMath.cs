using System;
using System.Globalization;

namespace Android_Ofek
{
    public static class CalculatorMath
    {
        public static bool TryCalculate(string firstText, string operation, string secondText, out string result)
        {
            result = null;
            double first, second;
            if (!double.TryParse(firstText, NumberStyles.Float, CultureInfo.InvariantCulture, out first)
                || !double.TryParse(secondText, NumberStyles.Float, CultureInfo.InvariantCulture, out second))
                return false;
            double value;
            switch (operation)
            {
                case "+": value = first + second; break;
                case "-": value = first - second; break;
                case "*": value = first * second; break;
                case "/": if (second == 0) return false; value = first / second; break;
                default: return false;
            }
            if (double.IsNaN(value) || double.IsInfinity(value)) return false;
            result = value.ToString(CultureInfo.InvariantCulture);
            return true;
        }
    }
}
