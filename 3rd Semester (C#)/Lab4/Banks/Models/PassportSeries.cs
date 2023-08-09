using Banks.Tools;

namespace Banks.Models;

public class PassportSeries
{
    private const int MinSeriesNumber = 1000;
    private const int MaxSeriesNumber = 9999;

    public PassportSeries(int series)
    {
        if (series < MinSeriesNumber || series > MaxSeriesNumber)
        {
            throw new BanksException($"Given value series: {series} has to be between {MinSeriesNumber} and {MaxSeriesNumber}");
        }

        Series = series;
    }

    public int Series { get; }
}
