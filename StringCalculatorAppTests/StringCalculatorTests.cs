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
        // Arrange
        var input  = "1,2,3";

        // Act
        var sut    = new StringCalculator();    // sut = system under test
        var result = sut.Add(input);

        // Assert
        result.Should().Be(6);
    }

    [Test]
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
    public void NegativeNumbersThrowException()
    {
        // Arrange
        var input = "-1,2,-3";

        // Act
        var sut = new StringCalculator();

        // Assert
        Assert.Throws<ArgumentException>(() => sut.Add(input));
    }

    // write fail test
}
