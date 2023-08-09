using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
namespace Isu.Services;

public class IsuService : Isu.Services.IIsuService
{
    private const int MinID = 100000;
    private const int RangeOfID = 900000;
    private readonly GroupName _tempGroupName = new GroupName("A0000");
    private readonly List<Group> _groups = new List<Group>();
    private int _shiftID = 0;

    public bool GroupAlreadyExist(GroupName groupName) => _groups.Any(x => x.GroupName.Name == groupName.Name);

    public Group AddGroup(GroupName name)
    {
        return AddGroup(name, new List<Student>());
    }

    public Group AddGroup(string name)
    {
        return AddGroup(new GroupName(name), new List<Student>());
    }

    public Group AddGroup(string name, List<Student> students)
    {
        return AddGroup(new GroupName(name), students);
    }

    public Group AddGroup(GroupName name, List<Student> students)
    {
        if (name is null)
        {
            throw new NameOfGroupNullReferenceException("Failed to create a new group. Name of group can not be null");
        }

        if (GroupAlreadyExist(name))
        {
            throw new GroupAlreadyExistsException($"Failed to create a new group. Group with name {name} already exists");
        }

        var new_group = new Group(name, students);
        _groups.Add(new_group);
        return new_group;
    }

    public Student AddStudent(Group group, string name, string surname, DateTime birthday, string patronymic = "")
    {
        Student newStudent = new (name, surname, GenerareID(), birthday, group.GroupName, patronymic);
        group.AddStudent(newStudent);
        return newStudent;
    }

    public Student AddStudent(string name, string surname, DateTime birthday, string patronymic = "")
    {
        Student newStudent = new (name, surname, GenerareID(), birthday, _tempGroupName, patronymic);
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        Student? student = _groups.SelectMany(group => group.Students).SingleOrDefault(student => student.Id == id);
        if (student is null)
        {
            throw new FailedToGetStudentException($"Failed to get student: {student}. This student does not exist");
        }

        return student;
    }

    public Student? FindStudent(int id)
    {
        Student student = _groups.SelectMany(group => group.Students).First(student => student.Id == id);
        return student;
    }

    public IReadOnlyList<Student>? FindStudents(GroupName groupName)
    {
        return _groups?.SingleOrDefault(grp => grp?.GroupName == groupName)?.Students;
    }

    public IReadOnlyList<Student>? FindStudents(CourseNumber courseNumber)
    {
        return _groups.Where(group => group.GroupName.CourseNumber == courseNumber).SelectMany(group => group.Students).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups?.SingleOrDefault(grp => grp?.GroupName == groupName);
    }

    public IReadOnlyList<Group>? FindGroups(CourseNumber courseNumber)
    {
        List<Group> groupsOfCourse = new ();
        IEnumerable<Group>? list = _groups?.Where(grp => grp?.GroupName.CourseNumber == courseNumber);
        if (list is not null)
        {
            groupsOfCourse.Concat(list);
        }

        return groupsOfCourse;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (student is null)
        {
            throw new StudentNullReferenceException("Failed to change student's group. Reference to students can not be null");
        }

        if (newGroup is null)
        {
            throw new NullReferenceException("Failed to change student's group. Reference to group can not be null");
        }

        if (student.NameOfGroup == newGroup.GroupName)
        {
            throw new SameGroupNamesException($"Failed to change student: {student} group. The transfer group: {newGroup} is the same as the student's current group: {student.NameOfGroup}");
        }

        if (student.NameOfGroup != _tempGroupName)
        {
            FindGroup(student.NameOfGroup)?.RemoveStudent(student);
        }

        newGroup.AddStudent(student);
        student.NameOfGroup = newGroup.GroupName;
    }

    public void RemoveStudentFromGroup(Student student)
    {
        if (student is null)
        {
            throw new StudentNullReferenceException($"Failed to remove student from group. Reference to students can not be null");
        }

        if (student.NameOfGroup is null)
        {
            throw new NameOfGroupNullReferenceException($"Failed to remove student: {student}. Student is not a member of any group");
        }

        if (student.NameOfGroup != _tempGroupName)
        {
            FindGroup(student.NameOfGroup)?.RemoveStudent(student);
            student.NameOfGroup = _tempGroupName;
        }
    }

    private int GenerareID()
    {
        _shiftID++;
        if (_shiftID is > RangeOfID)
        {
            _shiftID = 1;
        }

        return MinID + _shiftID;
    }
}
