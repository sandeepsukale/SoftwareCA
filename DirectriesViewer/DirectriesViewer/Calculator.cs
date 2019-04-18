using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectriesViewer
{
    class Calculator
    {
        public static string calculate(string word)
        {
            string operand = ""; double ans = 0;
            char[] delimiters = { '+', '^', '-', '*', '%', '/' };
            string first = word.Split(delimiters).First();
            string second = word.Split(delimiters).Last();
            double f; double s;
            Double.TryParse(first, out f);
            Double.TryParse(second, out s);

            if (word.Contains("+"))
            {
                operand = "+";
                ans = f + s;
            }

            else if (word.Contains("-"))
            {
                operand = "-";
                ans = f - s;
            }
            else if (word.Contains("*"))
            {
                operand = "*";
                ans = f * s;
            }

            else if (word.Contains("%"))
            {
                operand = "%";
                ans = f % s;
            }

            else if (word.Contains("/"))
            {
                operand = "/";
                ans = f / s;
            }
            else if (word.Contains("^"))
            {
                operand = "^";
                ans = Math.Pow(f, s);
            }
            return ($"{f} {operand} {s } = { Math.Round(ans, 2)}");
        }
    }
}
