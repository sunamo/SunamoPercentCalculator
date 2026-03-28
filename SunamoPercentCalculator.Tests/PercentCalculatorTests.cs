/// <summary>
/// Tests for the PercentCalculator class.
/// </summary>
public class PercentCalculatorTests
{
    /// <summary>
    /// Tests that AddOnePercent accumulates correctly over 100 iterations.
    /// </summary>
    [Fact]
    public void AddOnePercentTest()
    {
        PercentCalculator calculator = new PercentCalculator(100);
        for (int i = 0; i < 100; i++)
        {
            calculator.AddOnePercent();
        }

        Assert.Equal(100d, calculator.Last, 0.01);
    }

    /// <summary>
    /// Tests PercentFor with overallSum of 100 for correct percentage calculation and last-value adjustment.
    /// </summary>
    [Fact]
    public void PercentForTest()
    {
        PercentCalculator calculator = new PercentCalculator(100);

        var percentResult = calculator.PercentFor(10, false);
        var lastPercentResult = calculator.PercentFor(10, true);

        Assert.Equal(10, percentResult);
        Assert.Equal(90, lastPercentResult);
    }

    /// <summary>
    /// Tests PercentFor with overallSum of 1000 for correct percentage calculation and last-value adjustment.
    /// </summary>
    [Fact]
    public void PercentFor2Test()
    {
        PercentCalculator calculator = new PercentCalculator(1000);

        var percentResult = calculator.PercentFor(10, false);
        var lastPercentResult = calculator.PercentFor(10, true);

        Assert.Equal(1, percentResult);
        Assert.Equal(99, lastPercentResult);
    }
}
