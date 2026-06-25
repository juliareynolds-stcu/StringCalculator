namespace StringCalculatorAppTests;

using StringCalculatorApp;
using AwesomeAssertions;

public class StringCalculatorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SumsWhenGivenString()
    {
        int              result;
        string           input;
        StringCalculator calculator;

        input      = "1,2,3";
        calculator = new StringCalculator();
        result     = calculator.Add(input);

        result.Should().Be(6);
    }

    // write fail test
}
