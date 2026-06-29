using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculatorApp;

/// <summary>
/// This class handles operations related to delimiters used in the StringCalculator. 
/// It can be extended to include methods for parsing and validating delimiters, 
/// as well as any other functionality related to delimiters in the context of the StringCalculator application.
/// </summary>
public static class Delimiters
{
    /// <summary>
    /// Parses custom delimiters from the input string. 
    /// If no custom delimiter is specified, returns the default delimiters (comma and newline).
    /// Also trims the input string to remove the custom delimiter specification if present.
    /// </summary>
    /// <param name="input">the input string</param>
    /// <returns>delimiter(s) to be used to parse the string of numbers</returns>
    /// <exception cref="ArgumentException"></exception>
    public static string[] ParseDelimiter(string input)
    {
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
    public static string RemoveDelimiterSpecification(string input)
    {
        if (input.StartsWith("//"))
        {
            var delimiterEndIndex = input.IndexOf('\n');

            return input.Substring(delimiterEndIndex + 1).Trim();
        }

        return input;
    }
}
