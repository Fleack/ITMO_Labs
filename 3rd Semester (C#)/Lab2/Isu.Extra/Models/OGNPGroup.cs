using Isu.Exceptions;
using Isu.Extra.Interfaces;

namespace Isu.Extra.Models;

public class OgnpGroup : IOgnpGroup
{
    private const uint MaxStudentAmount = 25;
    private readonly List<ILesson> _lessons;
    private List<IStudentExtra> _students;

    public OgnpGroup(char faculty, string name, uint groupIndex, List<ILesson> lessons)
        : this(faculty, name, groupIndex, lessons, new List<IStudentExtra>()) { }

    public OgnpGroup(char faculty, string name, uint groupIndex, List<ILesson> lessons, List<IStudentExtra>? students)
    {
        if (lessons is null)
        {
            throw new IsuExtraException($"Failed to construct OgnpGroup, lessons list can not be null");
        }

        if (!char.IsLetter(faculty))
        {
            throw new IsuExtraException($"Failed to construct OgnpGroup, faculty: {faculty} has to be a letter");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new IsuExtraException($"Failed to construct OgnpStream, name: {faculty} can not be null or white space");
        }

        if (students is null)
        {
            _students = new ();
        }
        else
        {
            if (IsListOfStudentsTooBig(students))
            {
                throw new IsuExtraException($"Failed to construct OgnpGroup, given list of students: {students} has to be <= {MaxStudentAmount}");
            }

            _students = students;
        }

        Name = name + '/' + groupIndex.ToString();
        _lessons = lessons;
        Faculty = faculty;
        Id = Guid.NewGuid();
    }

    public IReadOnlyList<ILesson> Lessons => _lessons;
    public IReadOnlyList<IStudentExtra> Students => _students;
    public bool IsGroupFull => _students.Count == MaxStudentAmount;
    public char Faculty { get; }
    public string Name { get; }
    public Guid Id { get; }

    public void AddStudent(IStudentExtra newStudentExtra)
    {
        if (newStudentExtra is null)
        {
            throw new IsuExtraException($"Failed to AddStudent, IStudentExtra can not be null");
        }

        if (IsGroupFull)
        {
            throw new IsuExtraException($"Failed to AddStudent, list of students {Students} is full");
        }

        _students.Add(newStudentExtra);
    }

    public void RemoveStudent(IStudentExtra newStudentExtra)
    {
        if (newStudentExtra is null)
        {
            throw new IsuExtraException($"Failed to RemoveStudent, IStudentExtra can not be null");
        }

        _students.Remove(newStudentExtra);
    }

    private bool IsListOfStudentsTooBig(List<IStudentExtra> students)
    {
        return students.Count > MaxStudentAmount;
    }
}
