namespace StringCalculatorAppTests;

using StringCalculatorApp;
using AwesomeAssertions;
using Newtonsoft.Json.Bson;

public class StringCalculatorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [Category("Input")]
    public void SumsWhenGivenString()
    {
        // Arrange
        var input  = "1,2,3";

        // Act
        var sut    = new StringCalculator();    // sut = system under test
        var result = sut.Add(input);

        // Assert
        result.Should().Be(6);
    }

    [Test]
    [Category("Input")]
    public void ReturnsZeroWhenGivenNullOrEmptyString()
    {
        // Arrange
        var input = "";

        // Act
        var sut = new StringCalculator();    // sut = system under test
        var result = sut.Add(input);

        // Assert
        result.Should().Be(0);
    }

    [Test]
    [Category("Input")]
    public void IgnoresInvalidInputs()
    {
        // Arrange
        var input = "a, b, 1, c";

        // Act
        var sut = new StringCalculator();
        var result = sut.Add(input);

        // Assert
        result.Should().Be(1);
        sut.GetParseErrors().Should().Be(3);
    }

    [Test]
    [Category("Delimiters")]
    public void AcceptsNewlineAsDelimiter()
    {
        // Arrange
        var input = "1\n2\n3";

        // Act
        var sut = new StringCalculator();
        var result = sut.Add(input);

        // Assert
        result.Should().Be(6);
        sut.GetNumsProcessed().Should().Be(3);
    }

    [Test]
    [Category("Delimiters")]
    public void AcceptsAlternateDelimiters()
    {
        // Arrange
        var input = "//;\n1;2";

        // Act
        var sut = new StringCalculator();
        var result = sut.Add(input);

        // Assert
        result.Should().Be(3);
        sut.GetNumsProcessed().Should().Be(2);
        sut.GetParseErrors().Should().Be(0);
    }

    [Test]
    [Category("Delimiters")]
    public void AcceptsDelimitersOfAnyLength()
    {
        // Arrange
        var input = "//***\n1***2***3";

        // Act
        var sut = new StringCalculator();
        var result = sut.Add(input);

        // Assert
        result.Should().Be(6);
        sut.GetNumsProcessed().Should().Be(3);
        sut.GetParseErrors().Should().Be(0);
    }

    [Test]
    [Category("Delimiters")]
    public void AcceptsMultipleDelimiters()
    {
        // Arrange
        var input = "//[*][%]\n1*2%3";

        // Act
        var sut = new StringCalculator();
        var result = sut.Add(input);

        // Assert
        result.Should().Be(6);
        sut.GetNumsProcessed().Should().Be(3);
        sut.GetParseErrors().Should().Be(0);
    }

    [Test]
    [Category("Delimiters")]
    public void AcceptsMultipleDelimitersOfAnyLength()
    {
        // Arrange
        var input = "//[***][%%]\n1***2%%3";

        // Act
        var sut = new StringCalculator();
        var result = sut.Add(input);

        // Assert
        result.Should().Be(6);
        sut.GetNumsProcessed().Should().Be(3);
        sut.GetParseErrors().Should().Be(0);
    }

    [Test]
    [Category("Number of Calls")]
    public void GetCallCountGetsNumberOfTimesAddHasBeenCalled()
    {
        // Arrange
        var input = "1";

        // Act
        var sut = new StringCalculator();

        sut.Add(input);
        sut.Add(input);
        sut.Add(input);

        // Assert
        sut.GetCalledCount().Should().Be(3);
    }

    [Test]
    [Category("Number of Calls")]
    public void GetCalledCountReturnsZeroWhenAddHasNotBeenCalled()
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        var result = sut.GetCalledCount();

        // Assert
        result.Should().Be(0);
    }

    [Test]
    [Category("Input")]
    public void IgnoresNumsGreaterThan1000()
    {
        // Arrange
        var input = "2,1001";

        // Act
        var sut = new StringCalculator();
        var result = sut.Add(input);

        // Assert
        result.Should().Be(2);
        sut.GetNumsProcessed().Should().Be(1);
        sut.GetParseErrors().Should().Be(0);
        sut.GetNumsOutOfBounds().Should().Be(1);
    }

    [Test]
    [Category("Input")]
    public void NegativeNumbersThrowException()
    {
        // Arrange
        var input = "-1,2,-3";

        // Act
        var sut = new StringCalculator();

        // Assert
        Assert.Throws<ArgumentException>(() => sut.Add(input));
    }

    [Test]
    [Category("Event")]
    public void AddOccuredTest()
    {
        // Arrange
        var input = "1,2,3";
        var occurred = false;
        double sumValue = 0;
        var sut = new StringCalculator();
        
        sut.AddOccured += DelegateHandled; // subscribe with a function pointer (delegate)
        sut.AddOccured += DelegateHandled2;
        sut.AddOccured += (input, sum) => // subscribe with anonymous function
        { 
            occurred = true;
            sumValue = sum + 3;
        };

        // Act
        var result = sut.Add(input);

        // Assert
        occurred.Should().BeTrue();
        result.Should().Be(6);
        sumValue.Should().Be(9);
    }

    /// <summary>
    /// Function pointed to when event occurs. 
    /// Function isn't called on 219.
    /// Function is called inside Add() in StringCalculator
    /// </summary>
    /// <param name="input"></param>
    /// <param name="sum"></param>
    private void DelegateHandled(string input, double sum)
    {
        Console.WriteLine("DelegateHandled was triggered");
        Console.WriteLine($"Input: {input}");
        Console.WriteLine($"Sum: {sum}");
    }

    /// <summary>
    /// Function pointed to when event occurs. 
    /// Function isn't called on 219.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="sum"></param>
    private void DelegateHandled2(string input, double sum)
    {
        Console.WriteLine("DelegateHandled2 was triggered");
    }
}
