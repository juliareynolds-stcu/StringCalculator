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

    // write fail test
}
