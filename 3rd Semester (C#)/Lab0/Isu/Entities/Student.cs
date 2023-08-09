using Isu.Models;
namespace Isu.Entities;

public class Student
{
    private const int MinID = 100000;
    private const int MaxID = 999999;

    public Student(string name, string surname, int id, DateTime birthday, GroupName groupName, string patronymic = "")
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (string.IsNullOrEmpty(surname))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (id is < MinID or > MaxID)
        {
            throw new ArgumentOutOfRangeException($"Failed to create a student. Given value id: {id} has to be between {MinID} and {MaxID}");
        }

        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        Birthday = birthday;
        Id = id;
        NameOfGroup = groupName;
    }

    public string Name { get; private set; }

    public string Surname { get; private set; }

    public string Patronymic { get; private set; }

    public int Id { get; }

    public DateTime Birthday { get; private set; }

    public GroupName NameOfGroup { get; set; }
}