public class PercentCalculatorTests
{
    [Fact]
    public void AddOneTest()
    {
        PercentCalculator pc = new PercentCalculator(100);
        for (int i = 0; i < 100; i++)
        {
            //pc.AddOne();
        }


    }

    [Fact]
    public void PercentForTest()
    {
        PercentCalculator pc = new PercentCalculator(100);

        var i = pc.PercentFor(10, false);
        var i2 = pc.PercentFor(10, true);

        var abc = 0;
    }

    [Fact]
    public void PercentFor2Test()
    {
        PercentCalculator pc = new PercentCalculator(1000);

        var i = pc.PercentFor(10, false);
        var i2 = pc.PercentFor(10, true);

        var abc = 0;
    }
}
