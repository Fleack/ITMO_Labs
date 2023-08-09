namespace Banks.Models;

public class PassportData
{
    public PassportData(int passportNumber, int passportSeries)
    {
        Number = new PassportsNumber(passportNumber);
        Series = new PassportSeries(passportSeries);
    }

    public PassportsNumber Number { get; }
    public PassportSeries Series { get; }
}
