using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculatorApp;

/// <summary>
/// Takes a string and calculates the sum of the string
/// </summary>
public class StringCalculator()
{
    /// <summary>
    /// Sums ints contained in string. String must use commas (,) to delimit different ints.
    /// </summary>
    /// <param name="numbers">a string with ints separated by commas</param>
    /// <returns>an integer representing the sum of the numbers in the string, 0 for a null or empty string</returns>
    public int Add(string numbers)
    {
        if (string.IsNullOrWhiteSpace(numbers))
        {
            return 0;
        }

        var numArray = numbers.Split(",");
        var parseIntErrors = 0;
        var sum = 0;

        foreach (string number in numArray)
        {
            if (int.TryParse(number.Trim(), out var toSum))
            {
                sum += toSum;
            }
            else
            {
                parseIntErrors++;
            }
        }

        Console.WriteLine($"\r\nInteger parse errors:  {parseIntErrors}");

        return sum;
    }
}
