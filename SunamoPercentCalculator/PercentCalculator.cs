namespace SunamoPercentCalculator;

/// <summary>
/// Calculator for percentage distribution. Typically called multiple times via DonePartially().
/// </summary>
public class PercentCalculator
{
    /// <summary>
    /// The type of this class, for reflection purposes.
    /// </summary>
    public static Type PercentCalculatorType { get; set; } = typeof(PercentCalculator);

    private readonly double hundredPercent = 100d;
    private int sum;

    /// <summary>
    /// The value of one percent relative to the overall sum.
    /// </summary>
    public double OnePercent { get; set; }

    /// <summary>
    /// Creates a new instance of PercentCalculator with the specified overall sum.
    /// </summary>
    /// <param name="overallSum">The total sum representing 100%.</param>
    public PercentCalculator(double overallSum)
    {
        if (overallSum == 0) ThrowEx.DivideByZero();
        OnePercent = hundredPercent / overallSum;
        OverallSum = overallSum;
    }

    /// <summary>
    /// The last computed percentage value.
    /// </summary>
    public double Last { get; set; }

    /// <summary>
    /// The overall sum representing 100%.
    /// </summary>
    public double OverallSum { get; set; }

    /// <summary>
    /// Creates a new PercentCalculator instance with the specified overall sum.
    /// </summary>
    /// <param name="overallSum">The total sum representing 100%.</param>
    /// <returns>A new PercentCalculator instance.</returns>
    public static PercentCalculator Create(double overallSum)
    {
        return new PercentCalculator(overallSum);
    }

    /// <summary>
    /// Adds one percent to the accumulated Last value.
    /// </summary>
    public void AddOnePercent()
    {
        Last += OnePercent;
    }

    /// <summary>
    /// Resets the computed sum to zero. Automatically called by PercentFor when isLast is true.
    /// </summary>
    public void ResetComputedSum()
    {
        sum = 0;
    }

    /// <summary>
    /// Calculates the percentage for a given value relative to the overall sum.
    /// </summary>
    /// <param name="value">The value to calculate the percentage for.</param>
    /// <param name="isLast">Whether this is the last calculation, triggering adjustment to ensure total equals 100%.</param>
    /// <returns>The calculated percentage as an integer.</returns>
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
#if DEBUG
        if (result == -2147483648) Debugger.Break();
#endif
        return result;
    }
}
