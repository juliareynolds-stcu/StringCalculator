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
        sut.getParseIntErrors().Should().Be(3);
    }

    // write fail test
}
