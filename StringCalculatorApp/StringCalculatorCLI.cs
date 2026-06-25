namespace StringCalculatorApp;

/// <summary>
/// a command line interface for the string calculator
/// </summary>
public static class StringCalculatorCLI
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to String Calculator, a project following https://osherove.com/tdd-kata-1/");
        Console.WriteLine("\r\nPlease enter a string to be summed following the format int,int,int.");
        Console.Write("input:  ");

        var userInput = Console.ReadLine();
        var calculator = new StringCalculator();
        var result = calculator.Add(userInput);
        
        Console.WriteLine($"\r\nresult:  {result}");
        Console.WriteLine($"Integer parse errors:  {calculator.getParseIntErrors}");
    }
}
