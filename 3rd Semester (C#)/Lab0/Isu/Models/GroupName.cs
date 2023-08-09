using Isu.Exceptions;
namespace Isu.Models;

public class GroupName
{
    private const int MinGroupNameLength = 5;
    private const int MaxGroupNameLength = 6;
    private const int FacultyIndex = 0;
    private const int CourseIndex = 1;

    public GroupName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new NullReferenceException("Failed to change group name. Given value can not be null");
        }

        if (name.Length < MinGroupNameLength ||
            name.Length > MaxGroupNameLength ||
            !char.IsLetter(name[FacultyIndex]) ||
            !name[1..].All(x => char.IsDigit(x)))
        {
            throw new IncorrectGourpNameException($"Failed to set name of group. Given value: {name} is incorrect name of group. Name of group has to be:\n" +
                $"1) Equal or longer than {MinGroupNameLength} in size\n" +
                $"2) Equal or shorter than {MaxGroupNameLength}\n" +
                $"3) Every character except first is a digit\n" +
                $"4) First character is a letter\n");
        }

        Name = name;
        CourseNumber = new CourseNumber(int.Parse(name[CourseIndex].ToString()));
        Faculty = name[FacultyIndex];
    }

    public CourseNumber CourseNumber { get; }

    public char Faculty { get; }

    public string? Name { get; }
}