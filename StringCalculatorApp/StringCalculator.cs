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
    /// Sums ints contained in string. Defaults to commas or new lines as delimiters. 
    /// Delimiters can be changed by including the new delimiter at the beginning of the string in the format "//[delimiter]\n[numbers...]"
    /// Ignores invalid inputs. Returns 0 for null or empty string.
    /// </summary>
    /// <param name="numbers">a string with ints separated by commas</param>
    /// <returns>an integer representing the sum of the numbers in the string, 0 for a null or empty string</returns>
    public double Add(string numbers)
    {
        if (string.IsNullOrWhiteSpace(numbers))
        {
            return 0;
        }

        var delimiters = parseDelimiter(numbers);
        numbers = removeDelimiterSpecification(numbers);

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
    /// Parses custom delimiters from the input string. 
    /// If no custom delimiter is specified, returns the default delimiters (comma and newline).
    /// Also trims the input string to remove the custom delimiter specification if present.
    /// </summary>
    /// <param name="input">the input string</param>
    /// <returns>delimiter(s) to be used to parse the string of numbers</returns>
    /// <exception cref="ArgumentException"></exception>
    private string[] parseDelimiter(string input)
    {
        string[] delimiters;

        if (input.StartsWith("//"))
        {
            var delimiterEndIndex = input.IndexOf('\n');

            if (delimiterEndIndex < 0)
            {
                throw new ArgumentException("Invalid input: missing newline after custom delimiter");
            }

            var customDelimiter = input.Substring(2, delimiterEndIndex - 2).Trim();

            return [customDelimiter];
        }

        return [",", "\r\n", "\n"];
    }

    /// <summary>
    /// Returns the input string with the delimeter specification removed from the beginning of the string, if present. 
    /// If no custom delimiter is specified, returns the input string unchanged.
    /// </summary>
    /// <param name="input">the input string</param>
    /// <returns>string with the delimeter specification removed</returns>
    /// <exception cref="ArgumentException"></exception>
    private string removeDelimiterSpecification(string input)
    {
        if (input.StartsWith("//"))
        {
            var delimiterEndIndex = input.IndexOf('\n');

            return input.Substring(delimiterEndIndex + 1).Trim();
        }

        return input;
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
