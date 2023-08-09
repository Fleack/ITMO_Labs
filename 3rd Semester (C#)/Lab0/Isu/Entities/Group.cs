using Isu.Exceptions;
using Isu.Models;
namespace Isu.Entities;

public class Group
{
    public const int MaxGroupSize = 25;
    private readonly List<Student> _students = new List<Student>();

    public Group(GroupName groupName)
    {
        if (groupName is null)
        {
            throw new GroupNameNullReferenceException($"Failed to create a group. Given value groupName can not be null");
        }

        GroupName = groupName;
        _students = new List<Student>();
    }

    public Group(GroupName groupName, List<Student> students)
        : this(groupName)
    {
        if (students is null)
        {
            _students = new List<Student>();
        }
        else
        {
            if (students.Count > MaxGroupSize)
            {
                throw new GroupCreatingOverflowException($"Failed to create a group. List: {students} is too big, impossible to form a group of {students.Count} students. Max size of group is {MaxGroupSize}");
            }

            _students = students;
        }
    }

    public IReadOnlyList<Student> Students => _students;

    public GroupName GroupName { get; }

    private bool IsGroupFull => Students.Count == MaxGroupSize;

    public void AddStudent(Student newStudent)
    {
        if (IsGroupFull)
        {
            throw new GroupAddStudentOverflowException($"Failed to add student: {newStudent} to group: {this}. Group is already full. Max size of group is {MaxGroupSize} ");
        }

        _students.Add(newStudent);
    }

    public void RemoveStudent(Student student)
    {
        if (student is null)
        {
            throw new StudentNullReferenceException($"Failed to remove student from group: {this}. Given value can not be null");
        }

        if (student.NameOfGroup != GroupName)
        {
            throw new RemoveStudentStudentIsNotInThisGroupException($"Failed to remove student {student} from group: {this}. Student is not in this group");
        }

        _students.Remove(student);
    }
}