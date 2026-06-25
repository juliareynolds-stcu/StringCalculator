namespace StringCalculatorApp;

/// <summary>
/// a command line interface for the string calculator
/// </summary>
public static class StringCalculatorCLI
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to String Calculator, a project following https://osherove.com/tdd-kata-1/");
            Console.WriteLine("\r\nPlease enter a string of numbers to be summed following the format #, #, #.");
            Console.Write("Numbers to add:  ");

            var userInput = Console.ReadLine();
            var calculator = new StringCalculator();
            var result = calculator.Add(userInput);

            Console.WriteLine($"\r\nResult:\t\t\t{result}");
            Console.WriteLine($"Integers summed:\t{calculator.getIntsProcessed()}");
            Console.WriteLine($"Integer parse errors:\t{calculator.getParseIntErrors()}");

            Console.Write("\r\nWould you like to process another string of numbers? (y/n)  ");

            switch (Console.ReadLine())
            {
                case "y":
                case "Y":
                    Console.Clear();
                    break;
                case "n":
                case "N":
                    Console.WriteLine("\r\nThank you for using String Calculator. Goodbye!");
                    return;
                default:
                    Console.WriteLine("\r\nInvalid input. Exiting program.");
                    return; 
            }
        }
    }
}
