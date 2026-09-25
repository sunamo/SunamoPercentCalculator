namespace SunamoPercentCalculator;

public class PercentCalculator
{
    public static Type PercentCalculatorType { get; set; } = typeof(PercentCalculator);

    private readonly double hundredPercent = 100d;
    private int sum;

    public double OnePercent { get; set; }

    public PercentCalculator(double overallSum)
    {
        if (overallSum == 0) ThrowEx.DivideByZero();
        OnePercent = hundredPercent / overallSum;
        OverallSum = overallSum;
    }

    public double Last { get; set; }

    public double OverallSum { get; set; }

    public static PercentCalculator Create(double overallSum) => new(overallSum);

    public void AddOnePercent()
    {
        Last += OnePercent;
    }

    public void ResetComputedSum()
    {
        sum = 0;
    }

    public int PercentFor(double value, bool isLast)
    {
        if (OverallSum == 0) return 0;

        var quotient = value / OverallSum;
        var result = (int)(hundredPercent * quotient);
        sum += result;
        if (isLast)
        {
            var difference = sum - 100;
            if (sum != 0) result -= difference;
            ResetComputedSum();
        }
        return result;
    }
}
