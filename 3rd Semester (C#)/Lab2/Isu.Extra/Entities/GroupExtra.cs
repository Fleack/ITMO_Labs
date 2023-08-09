using Isu.Exceptions;
using Isu.Extra.Interfaces;
using Isu.Models;
namespace Isu.Entities;

public class GroupExtra : IGroupExtra
{
    private readonly List<IStudentExtra> _students = new List<IStudentExtra>();
    private readonly IReadOnlyList<ILesson> _lessons;

    public GroupExtra(GroupName groupName, List<ILesson> lessons)
    {
        if (groupName is null)
        {
            throw new IsuExtraException("Failed to construct GroupExtra, GroupName can not be null");
        }

        if (lessons is null)
        {
            throw new IsuExtraException("Failed to construct GroupExtra, List of ILesson can not be null");
        }

        Group = new (groupName);
        _students = new List<IStudentExtra>();
        _lessons = lessons;
    }

    public GroupExtra(GroupName groupName, List<IStudentExtra> students, List<ILesson> lessons)
        : this(groupName, lessons)
    {
        if (students is null)
        {
            _students = new List<IStudentExtra>();
        }
        else
        {
            if (students.Count > Group.MaxGroupSize)
            {
                throw new IsuExtraException($"Failed to create a group. List: {students} is too big, impossible to form a group of {students.Count} students. Max size of group is {Group.MaxGroupSize}");
            }

            _students = students;
        }
    }

    public IReadOnlyList<ILesson> Lessons => _lessons;
    public IReadOnlyList<IStudentExtra> Students => _students;
    public Group Group { get; }

    private bool IsGroupFull => Group.Students.Count == Group.MaxGroupSize;

    public void AddStudent(IStudentExtra newStudent)
    {
        if (newStudent is null)
        {
            throw new IsuExtraException($"Failed to add student: newStudent to group: {this}. IStudentExtra can not be null");
        }

        if (IsGroupFull)
        {
            throw new IsuExtraException($"Failed to add student: {newStudent} to group: {this}. Group is already full. Max size of group is {Group.MaxGroupSize} ");
        }

        _students.Add(newStudent);
    }

    public void RemoveStudent(IStudentExtra student)
    {
        if (student is null)
        {
            throw new IsuExtraException($"Failed to remove student from group: {this}. Given value can not be null");
        }

        if (student.Student.NameOfGroup != Group.GroupName)
        {
            throw new IsuExtraException($"Failed to remove student {student} from group: {this}. Student is not in this group");
        }

        _students.Remove(student);
    }
}