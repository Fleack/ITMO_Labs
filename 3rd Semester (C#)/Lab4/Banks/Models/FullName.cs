using Banks.Tools;

namespace Banks.Models;

public class FullName
{
    public FullName(string name, string surname)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BanksException($"Given value name: {name} can not be null or whitespace");
        }

        if (string.IsNullOrWhiteSpace(surname))
        {
            throw new BanksException($"Given value surname: {surname} can not be null or whitespace");
        }

        Name = name;
        Surname = surname;
    }

    public string Name { get; }
    public string Surname { get; }
}
