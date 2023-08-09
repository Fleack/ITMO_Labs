using Banks.Tools;

namespace Banks.Models;

public class PassportsNumber
{
    private const int MinPassportsNumberNumber = 100000;
    private const int MaxPassportsNumberNumber = 999999;

    public PassportsNumber(int number)
    {
        if (number < MinPassportsNumberNumber || number > MaxPassportsNumberNumber)
        {
            throw new BanksException($"Given value series: {number} has to be between {MinPassportsNumberNumber} and {MaxPassportsNumberNumber}");
        }

        Number = number;
    }

    public int Number { get; }
}
