using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using static StringCalculatorApp.Delimiters;

namespace StringCalculatorApp;

/// <summary>
/// Takes a string and calculates the sum of the string
/// </summary>
public class StringCalculator()
{
    private int parseErrors;
    private int numsProcessed;
    private int addCalledCount;
    private int numsOutOfBounds;

    /// <summary>
    /// Event that is triggered when the Add method is called. It passes the input string and the result of the addition as parameters.
    /// </summary>
    public event Action<string, double>? AddOccured;

    /// <summary>
    /// Sums ints contained in string. Defaults to commas or new lines as delimiters. 
    /// Delimiters can be changed by including the new delimiter at the beginning of the string in the format "//[delimiter]\n[numbers...]"
    /// Ignores invalid inputs. Returns 0 for null or empty string. Negative numbers are not allowed and will throw an exception.
    /// Numbers greater than 1000 are ignored and not included in the sum.
    /// </summary>
    /// <param name="numbers">a string with ints separated by commas</param>
    /// <returns>an integer representing the sum of the numbers in the string, 0 for a null or empty string</returns>
    public double Add(string numbers)
    {
        this.addCalledCount += 1;

        if (string.IsNullOrWhiteSpace(numbers))
        {
            return 0;
        }

        var delimiters = ParseDelimiter(numbers);
        numbers = RemoveDelimiterSpecification(numbers);

        var numArray = numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        var parseErrors = 0;
        var sum = 0.0;
        var numsProcessed = 0;
        var numsOutOfBounds = 0;
        
        ArrayList negatives = new ArrayList();

        foreach (var number in numArray)
        {
            if (double.TryParse(number.Trim(), out var toSum))
            {
                if (toSum < 0)
                {
                    negatives.Add(toSum);
                    numsOutOfBounds += 1;
                    continue;
                }

                if (toSum > 1000)
                {
                    numsOutOfBounds += 1;
                    continue;
                }

                sum += toSum;
                numsProcessed += 1;
            }
            else
            {
                parseErrors += 1;
            }
        }

        this.parseErrors = parseErrors;
        this.numsProcessed = numsProcessed;
        this.numsOutOfBounds = numsOutOfBounds;

        if (negatives.Count > 0)
        {
            var builder = new StringBuilder();

            builder.AppendJoin(", ", negatives.ToArray());

            throw new ArgumentException($"Negative numbers are not allowed: {builder.ToString()}");
        }

        AddOccured?.Invoke(numbers, sum);

        return sum;
    }

    /// <summary>
    /// Returns the number of parse errors encountered during the last call
    /// </summary>
    /// <returns>int the number of parse errors</returns>
    public int GetParseErrors()
    {
        return this.parseErrors;
    }

    /// <summary>
    /// Returns the number of integers processed successfully during the last call
    /// </summary>
    /// <returns>int - the number of ints processed</returns>
    public int GetNumsProcessed()
    {
        return this.numsProcessed;
    }

    /// <summary>
    /// Returns the number of times the Add method has been called on this instance of StringCalculator
    /// </summary>
    /// <returns>int - number of add calls</returns>
    public int GetCalledCount()
    {
        return this.addCalledCount;
    }
  
    /// Returns the number of numbers that were out of bounds (greater than 1000) during the last call. 
    /// </summary>
    /// <returns>int - the number of numbers not processed due to being out of bounds</returns>
    public int GetNumsOutOfBounds()
    {
        return this.numsOutOfBounds;
    }
}
