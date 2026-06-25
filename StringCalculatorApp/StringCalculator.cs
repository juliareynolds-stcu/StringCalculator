using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculatorApp;

/// <summary>
/// Takes a string and calculates the sum of the string
/// </summary>
public class StringCalculator()
{
    private int parseIntErrors;
    private int intsProcessed;

    /// <summary>
    /// Sums ints contained in string. String must use commas (,) to delimit different ints.
    /// </summary>
    /// <param name="numbers">a string with ints separated by commas</param>
    /// <returns>an integer representing the sum of the numbers in the string, 0 for a null or empty string</returns>
    public double Add(string numbers)
    {
        if (string.IsNullOrWhiteSpace(numbers))
        {
            return 0;
        }

        var delimiters = new string[] { ",", "\r\n", "\n" };
        var numArray = numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        var parseIntErrors = 0;
        var sum = 0.0;
        var intsProcessed = 0;

        foreach (var number in numArray)
        {
            if (double.TryParse(number.Trim(), out var toSum))
            {
                sum += toSum;
                intsProcessed += 1;
            }
            else
            {
                parseIntErrors += 1;
            }
        }

        this.parseIntErrors = parseIntErrors;
        this.intsProcessed = intsProcessed;

        return sum;
    }

    /// <summary>
    /// Returns the number of parse errors encountered during the last call
    /// </summary>
    /// <returns>int the number of parse errors</returns>
    public int getParseIntErrors()
    {
        return this.parseIntErrors;
    }

    /// <summary>
    /// Returns the number of integers processed successfully during the last call
    /// </summary>
    /// <returns>int - the number of ints processed</returns>
    public int getIntsProcessed()
    {
        return this.intsProcessed;
    }
}
