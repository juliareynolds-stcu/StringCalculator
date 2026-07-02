namespace StringCalculatorApp;

/// <summary>
/// a command line interface for the string calculator
/// </summary>
public static class StringCalculatorCLI
{
    /// <summary>
    /// Main method for the command line interface. Loops until the user chooses to exit, prompting for input and displaying results.
    /// </summary>
    /// <param name="args"></param>
    public static void Main()
    {
        var continueProgram = true;

        while (continueProgram)
        {
            var userInput = GetUserInput();

            CalculateResult(userInput);

            continueProgram = ContinueRequest();
        }
    }

    /// <summary>
    /// Welcome the user and prompt them for input, returning a string formatted for the StringCalculator.Add() method.
    /// The string will be in the format of "//[delimiter]\n[numbers]" where [delimiter] is the user's chosen delimiter and [numbers] is the string of numbers to be summed.
    /// </summary>
    /// <returns>string containing the user's input of numbers with the user's desired delimiter at the front</returns>
    private static string GetUserInput()
    {
        Console.WriteLine("Welcome to String Calculator, a project following https://osherove.com/tdd-kata-1/");
        Console.WriteLine("\r\nPlease enter a string of numbers to be summed, separated by your chosen delimiter (default to a comma if unsure).");
        Console.WriteLine("Numbers to be summed must be between 0 and 1000 and can include decimals.");
        Console.Write("Numbers to add:  ");

        var userInputNums = Console.ReadLine();

        Console.WriteLine("Please enter your chosen delimiter. Leave this blank to use the default(,).");
        Console.WriteLine("If using multiple delimiters, separate them with ','");
        Console.Write("Delimiter(s):  ");

        var userInputDelimiters = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userInputDelimiters))
        {
            userInputDelimiters = ",";
        }
        else
        {
            string[] userDelimiters = userInputDelimiters.Split(",", StringSplitOptions.RemoveEmptyEntries);

            if (userDelimiters.Length == 0)
            {
                userInputDelimiters = ",";
            }
            else if (userDelimiters.Length > 1)
            {
                userInputDelimiters = $"[{string.Join("][", userDelimiters)}]";
            }
        }

        return $"//{userInputDelimiters}\n{userInputNums}";
    }

    /// <summary>
    /// Handles the calculation of the result and prints the result to the console, 
    /// along with the number of integers summed and the number of parse errors encountered.
    /// </summary>
    /// <param name="userInput">a string containing the user's chosen delimiter and the numbers to sum</param>
    private static void CalculateResult(string userInput)
    {
        var calculator = new StringCalculator();

        try
        {
            var result = calculator.Add(userInput);

            Console.WriteLine($"\r\nResult:\t\t{result}");
            Console.WriteLine($"#'s summed:\t{calculator.GetNumsProcessed()}");
            Console.WriteLine($"Parse errors:\t{calculator.GetParseErrors()}");
            Console.WriteLine($"Out of bounds:\t{calculator.GetNumsOutOfBounds()}");
        }
        catch (Exception caught)
        {
            Console.WriteLine($"\r\nAn error occurred while summing: {caught}");
        }
    }

    /// <summary>
    /// Asks the user if they would like to process another string of numbers. Returns true if they would like to continue, false if they would like to exit the program.
    /// </summary>
    /// <returns>boolean indicating whether to end the program or not</returns>
    private static bool ContinueRequest()
    {
        Console.Write("\r\nWould you like to process another string of numbers? (y/n)  ");

        switch (Console.ReadLine())
        {
            case "y" or "Y":
                Console.Clear();
                return true;

            case "n" or "N":
                Console.WriteLine("\r\nThank you for using String Calculator. Goodbye!");
                return false;

            default:
                Console.WriteLine("\r\nInvalid input. Exiting program.");
                return false;
        }
    }
}
