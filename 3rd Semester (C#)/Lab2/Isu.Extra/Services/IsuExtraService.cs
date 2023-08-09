using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Interfaces;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService : IsuService
{
    private const int MinID = 100000;
    private const int RangeOfID = 900000;
    private const uint LessonTime = 90;

    private readonly List<IOgnpCourse> _courses = new ();
    private readonly List<IGroupExtra> _groups = new List<IGroupExtra>();
    private readonly GroupName _tempGroupName = new GroupName("A0000");

    private int _shiftID = 0;

    public IReadOnlyList<IOgnpCourse> OgnpCourses => _courses;
    public new bool GroupAlreadyExist(GroupName groupName) => _groups.Any(x => x.Group.GroupName.Name == groupName.Name);

    public GroupExtra AddGroup(GroupName name, List<ILesson> lessons)
    {
        return AddGroup(name, new List<IStudentExtra>(), lessons);
    }

    public GroupExtra AddGroup(string name, List<ILesson> lessons)
    {
        return AddGroup(new GroupName(name), new List<IStudentExtra>(), lessons);
    }

    public GroupExtra AddGroup(string name, List<IStudentExtra> students, List<ILesson> lessons)
    {
        return AddGroup(new GroupName(name), students, lessons);
    }

    public GroupExtra AddGroup(GroupName name, List<IStudentExtra> students, List<ILesson> lessons)
    {
        if (name is null)
        {
            throw new NameOfGroupNullReferenceException("Failed to create a new group. Name of group can not be null");
        }

        if (GroupAlreadyExist(name))
        {
            throw new GroupAlreadyExistsException($"Failed to create a new group. Group with name {name} already exists");
        }

        var new_group = new GroupExtra(name, students, lessons);
        _groups.Add(new_group);
        return new_group;
    }

    public StudentExtra AddStudent(GroupExtra group, string name, string surname, DateTime birthday, string patronymic = "")
    {
        StudentExtra newStudent = new (name, surname, GenerareID(), birthday, group.Group.GroupName, patronymic);
        group.AddStudent(newStudent);
        return newStudent;
    }

    public new StudentExtra AddStudent(string name, string surname, DateTime birthday, string patronymic = "")
    {
        StudentExtra newStudent = new (name, surname, GenerareID(), birthday, _tempGroupName, patronymic);
        return newStudent;
    }

    public new IStudentExtra GetStudent(int id)
    {
        IStudentExtra? student = _groups.SelectMany(group => group.Students).SingleOrDefault(student => student.Student.Id == id);
        if (student is null)
        {
            throw new FailedToGetStudentException($"Failed to get student: {student}. This student does not exist");
        }

        return student;
    }

    public new IStudentExtra? FindStudent(int id)
    {
        IStudentExtra student = _groups.SelectMany(group => group.Students).First(student => student.Student.Id == id);
        return student;
    }

    public new IReadOnlyList<IStudentExtra>? FindStudents(GroupName groupName)
    {
        return _groups?.SingleOrDefault(grp => grp?.Group.GroupName == groupName)?.Students;
    }

    public new IReadOnlyList<IStudentExtra>? FindStudents(CourseNumber courseNumber)
    {
        return _groups.Where(group => group.Group.GroupName.CourseNumber == courseNumber).SelectMany(group => group.Students).ToList();
    }

    public new IGroupExtra? FindGroup(GroupName groupName)
    {
        return _groups?.SingleOrDefault(grp => grp?.Group?.GroupName == groupName);
    }

    public IGroupExtra GetGroup(GroupName groupName)
    {
        IGroupExtra? group = _groups.SingleOrDefault(grp => grp?.Group?.GroupName == groupName);

        if (group is null)
        {
            throw new IsuExtraException($"Failed to GetGroup");
        }

        return group;
    }

    public new IReadOnlyList<IGroupExtra>? FindGroups(CourseNumber courseNumber)
    {
        List<IGroupExtra> groupsOfCourse = new ();
        IEnumerable<IGroupExtra>? list = _groups?.Where(grp => grp?.Group?.GroupName.CourseNumber == courseNumber);
        if (list is not null)
        {
            groupsOfCourse.Concat(list);
        }

        return groupsOfCourse;
    }

    public void ChangeStudentGroup(IStudentExtra student, IGroupExtra newGroup)
    {
        if (student is null)
        {
            throw new StudentNullReferenceException("Failed to change student's group. Reference to students can not be null");
        }

        if (newGroup is null)
        {
            throw new NullReferenceException("Failed to change student's group. Reference to group can not be null");
        }

        if (student.Student.NameOfGroup == newGroup.Group.GroupName)
        {
            throw new SameGroupNamesException($"Failed to change student: {student} group. The transfer group: {newGroup} is the same as the student's current group: {student.Student.NameOfGroup}");
        }

        if (student.Student.NameOfGroup != _tempGroupName)
        {
            FindGroup(student.Student.NameOfGroup)?.RemoveStudent(student);
        }

        newGroup.AddStudent(student);
        student.Student.NameOfGroup = newGroup.Group.GroupName;
    }

    public void RemoveStudentFromGroup(IStudentExtra student)
    {
        if (student is null)
        {
            throw new StudentNullReferenceException($"Failed to remove student from group. Reference to students can not be null");
        }

        if (student.Student.NameOfGroup is null)
        {
            throw new NameOfGroupNullReferenceException($"Failed to remove student: {student}. Student is not a member of any group");
        }

        if (student.Student.NameOfGroup != _tempGroupName)
        {
            FindGroup(student.Student.NameOfGroup)?.RemoveStudent(student);
            student.Student.NameOfGroup = _tempGroupName;
        }
    }

    public OgnpCourse AddNewOgnpCourse(string name, char faculty)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new IsuExtraException($"Failed to AddNewOgnpCourse");
        }

        if (DoesOgnpCourseAlreadyExist(name))
        {
            throw new IsuExtraException($"Failed to AddNewOgnpCourse");
        }

        OgnpCourse newOgnp = new (faculty, name);
        _courses.Add(newOgnp);
        return newOgnp;
    }

    public void SetFirstOgnpToStudent(IStudentExtra student, IOgnpGroup ognpgroup)
    {
        if (student is null)
        {
            throw new IsuExtraException($"Failed to SetFirstOgnpToStudent");
        }

        if (ognpgroup is null)
        {
            throw new IsuExtraException($"Failed to SetFirstOgnpToStudent");
        }

        if (student.SecondOgnpGroup == ognpgroup)
        {
            throw new IsuExtraException($"Failed to SetFirstOgnpToStudent");
        }

        AddStudentToOgnpGroup(student, ognpgroup);
        student.SetFirstOgnpGroup(ognpgroup);
    }

    public void SetSecondOgnpToStudent(IStudentExtra student, IOgnpGroup ognpgroup)
    {
        if (student is null)
        {
            throw new IsuExtraException($"Failed to SetFirstOgnpToStudent");
        }

        if (ognpgroup is null)
        {
            throw new IsuExtraException($"Failed to SetFirstOgnpToStudent");
        }

        if (student.FirstOgnpGroup == ognpgroup)
        {
            throw new IsuExtraException($"Failed to SetSecondOgnpToStudent");
        }

        AddStudentToOgnpGroup(student, ognpgroup);
        student.SetSecondOgnpGroup(ognpgroup);
    }

    public void RemoveStudentsFirstOgnp(IStudentExtra student)
    {
        if (student is null)
        {
            throw new IsuExtraException($"Failed to RemoveStudentsFirstOgnp");
        }

        if (student.FirstOgnpGroup is null)
        {
            throw new IsuExtraException($"Failed to RemoveStudentsFirstOgnp");
        }

        student.FirstOgnpGroup.RemoveStudent(student);
        student.RemoveFirstOgnpGroup();
    }

    public void RemoveStudentsSecondOgnp(IStudentExtra student)
    {
        if (student is null)
        {
            throw new IsuExtraException($"Failed to RemoveStudentsSecondOgnp");
        }

        if (student.SecondOgnpGroup is null)
        {
            throw new IsuExtraException($"Failed to RemoveStudentsSecondOgnp");
        }

        student.SecondOgnpGroup.RemoveStudent(student);
        student.RemoveSecondOgnpGroup();
    }

    public IReadOnlyList<IOgnpStream> GetStreamsOfCourse(IOgnpCourse course)
    {
        return course.OgnpStreams;
    }

    public IReadOnlyList<IStudentExtra> GetStudentsOfGroup(IOgnpGroup group)
    {
        return group.Students;
    }

    public IReadOnlyList<IStudentExtra> GetStudentsWithoutOgnp(IGroupExtra group)
    {
        return group.Students.Where(student => student.FirstOgnpGroup is null || student.SecondOgnpGroup is null).ToList();
    }

    private void AddStudentToOgnpGroup(IStudentExtra student, IOgnpGroup ognpgroup)
    {
        if (student is null)
        {
            throw new IsuExtraException($"Failed to AddStudentToOgnpGroup");
        }

        if (ognpgroup is null)
        {
            throw new IsuExtraException($"Failed to AddStudentToOgnpGroup");
        }

        if (student.Student.NameOfGroup.Faculty == ognpgroup.Faculty)
        {
            throw new IsuExtraException($"Failed to AddStudentToOgnpGroup");
        }

        if (DoesOgnpSchedulesIntersect(ognpgroup, GetGroup(student.Student.NameOfGroup)))
        {
            throw new IsuExtraException($"Failed to AddStudentToOgnpGroup");
        }

        ognpgroup.AddStudent(student);
    }

    private bool DoesOgnpCourseAlreadyExist(string name)
    {
        return _courses.Any(x => x.Name == name);
    }

    private bool DoesOgnpSchedulesIntersect(IOgnpGroup ognpgroup, IGroupExtra group)
    {
        foreach (ILesson ognp_lesson in ognpgroup.Lessons)
        {
            foreach (ILesson group_lesson in group.Lessons)
            {
                if (DoesLessonsIntersect(ognp_lesson, group_lesson))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool DoesLessonsIntersect(ILesson lesson1, ILesson lesson2)
    {
        if (lesson1.LessonBeginTime.DayOfWeek != lesson2.LessonBeginTime.DayOfWeek)
        {
            return false;
        }

        double absTimeDifference = Math.Abs((lesson1.LessonBeginTime.Time - lesson2.LessonBeginTime.Time).TotalMinutes);

        if (absTimeDifference <= LessonTime)
        {
            return true;
        }

        return false;
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