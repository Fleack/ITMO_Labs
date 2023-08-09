using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Entities;
using Isu.Extra.Interfaces;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;

namespace Isu.Test;

public class IsuExtraTests
{
    private readonly IsuExtraService _service = new IsuExtraService();

    [Fact]
    public void NewOgnpCourseAdding()
    {
        OgnpCourse ognpCourse = _service.AddNewOgnpCourse("test1", 'A');
        Assert.Contains(ognpCourse, _service.OgnpCourses);
    }

    [Fact]
    public void AddStudentToOgnp()
    {
        OgnpCourse ognpCourse = _service.AddNewOgnpCourse("test2", 'A');
        OgnpStream ognpStream = ognpCourse.AddNewStream();
        OgnpGroup ognpGroup = ognpStream.AddNewGroup(ListLessons2(), new List<IStudentExtra>());

        GroupName groupName = new ("M3105");
        GroupExtra group = _service.AddGroup(groupName, ListLessons1());

        StudentExtra studnet = _service.AddStudent(group, "Ivan", "Ivanov", DateTime.Now);

        _service.SetFirstOgnpToStudent(studnet, ognpGroup);

        Assert.Equal(studnet.FirstOgnpGroup, ognpGroup);
        Assert.Contains(studnet, ognpGroup.Students);
    }

    [Fact]
    public void RemoveStudentFromOgnp()
    {
        OgnpCourse ognpCourse = _service.AddNewOgnpCourse("test3", 'A');
        OgnpStream ognpStream = ognpCourse.AddNewStream();
        OgnpGroup ognpGroup = ognpStream.AddNewGroup(ListLessons2(), new List<IStudentExtra>());

        GroupName groupName = new ("M3106");
        GroupExtra group = _service.AddGroup(groupName, ListLessons1());

        StudentExtra studnet = _service.AddStudent(group, "Ivan", "Ivanov", DateTime.Now);

        _service.SetFirstOgnpToStudent(studnet, ognpGroup);
        _service.RemoveStudentsFirstOgnp(studnet);

        Assert.Null(studnet.FirstOgnpGroup);
        Assert.DoesNotContain(studnet, ognpGroup.Students);
    }

    [Fact]
    public void GetStreamsOfCourse()
    {
        OgnpCourse ognpCourse = _service.AddNewOgnpCourse("test4", 'A');
        OgnpStream ognpStream1 = ognpCourse.AddNewStream();
        OgnpStream ognpStream2 = ognpCourse.AddNewStream();
        OgnpStream ognpStream3 = ognpCourse.AddNewStream();

        Assert.Contains(ognpStream1, ognpCourse.OgnpStreams);
        Assert.Contains(ognpStream2, ognpCourse.OgnpStreams);
        Assert.Contains(ognpStream3, ognpCourse.OgnpStreams);
    }

    [Fact]
    public void StudentsOfOgnpGroup()
    {
        OgnpCourse ognpCourse = _service.AddNewOgnpCourse("test5", 'A');
        OgnpStream ognpStream = ognpCourse.AddNewStream();
        OgnpGroup ognpGroup = ognpStream.AddNewGroup(ListLessons2(), new List<IStudentExtra>());

        GroupName groupName = new ("M3107");
        GroupExtra group = _service.AddGroup(groupName, ListLessons1());

        StudentExtra studnet1 = _service.AddStudent(group, "Ivan", "Ivanov", DateTime.Now);
        StudentExtra studnet2 = _service.AddStudent(group, "Sanya", "Sanyok", DateTime.Now);
        StudentExtra studnet3 = _service.AddStudent(group, "Kolya", "Kolyanov", DateTime.Now);

        _service.SetSecondOgnpToStudent(studnet1, ognpGroup);
        _service.SetSecondOgnpToStudent(studnet2, ognpGroup);
        _service.SetSecondOgnpToStudent(studnet3, ognpGroup);

        Assert.Contains(studnet1, ognpGroup.Students);
        Assert.Contains(studnet2, ognpGroup.Students);
        Assert.Contains(studnet3, ognpGroup.Students);
    }

    [Fact]
    public void StudentsWithoutOgnp()
    {
        OgnpCourse ognpCourse1 = _service.AddNewOgnpCourse("test6", 'A');
        OgnpStream ognpStream1 = ognpCourse1.AddNewStream();
        OgnpGroup ognpGroup1 = ognpStream1.AddNewGroup(ListLessons2(), new List<IStudentExtra>());

        OgnpCourse ognpCourse2 = _service.AddNewOgnpCourse("test7", 'A');
        OgnpStream ognpStream2 = ognpCourse2.AddNewStream();
        OgnpGroup ognpGroup2 = ognpStream2.AddNewGroup(ListLessons2(), new List<IStudentExtra>());

        GroupName groupName = new ("M3108");
        GroupExtra group = _service.AddGroup(groupName, ListLessons1());

        StudentExtra studnet1 = _service.AddStudent(group, "Ivan", "Ivanov", DateTime.Now);
        StudentExtra studnet2 = _service.AddStudent(group, "Sanya", "Sanyok", DateTime.Now);
        StudentExtra studnet3 = _service.AddStudent(group, "Kolya", "Kolyanov", DateTime.Now);

        _service.SetFirstOgnpToStudent(studnet1, ognpGroup1);
        _service.SetSecondOgnpToStudent(studnet1, ognpGroup2);

        IReadOnlyList<IStudentExtra> list = _service.GetStudentsWithoutOgnp(group);

        Assert.DoesNotContain(studnet1, list);
        Assert.Contains(studnet2, list);
        Assert.Contains(studnet3, list);
    }

    [Fact]
    public void StudentSameFacultyAsOgnpException()
    {
        OgnpCourse ognpCourse = _service.AddNewOgnpCourse("test8", 'M');
        OgnpStream ognpStream = ognpCourse.AddNewStream();
        OgnpGroup ognpGroup = ognpStream.AddNewGroup(ListLessons2(), new List<IStudentExtra>());

        GroupName groupName = new ("M3107");
        GroupExtra group = _service.AddGroup(groupName, ListLessons1());

        StudentExtra studnet = _service.AddStudent(group, "Ivan", "Ivanov", DateTime.Now);

        Assert.Throws<IsuExtraException>(() => _service.SetFirstOgnpToStudent(studnet, ognpGroup));
    }

    [Fact]
    public void GroupExtraAndOgnpGroupLessonsIntersection()
    {
        OgnpCourse ognpCourse = _service.AddNewOgnpCourse("test", 'A');
        OgnpStream ognpStream = ognpCourse.AddNewStream();
        OgnpGroup ognpGroup = ognpStream.AddNewGroup(ListLessons3(), new List<IStudentExtra>());

        GroupName groupName = new ("M3109");
        GroupExtra group = _service.AddGroup(groupName, ListLessons1());

        StudentExtra studnet = _service.AddStudent(group, "Ivan", "Ivanov", DateTime.Now);

        Assert.Throws<IsuExtraException>(() => _service.SetFirstOgnpToStudent(studnet, ognpGroup));
    }

    private List<ILesson> ListLessons1()
    {
        List<ILesson> lessons = new ();

        TimeOnly lesson1BeginTime = new (8, 20);
        DayTime lesson1Begin = new (lesson1BeginTime, DayOfWeek.Friday);
        TimeOnly lesson1EndTime = new (9, 50);
        DayTime lesson1End = new (lesson1EndTime, DayOfWeek.Friday);

        Lesson lesson1 = new (lesson1Begin, lesson1End, 101, "Ivan");

        TimeOnly lesson2BeginTime = new (12, 00);
        DayTime lesson2Begin = new (lesson2BeginTime, DayOfWeek.Monday);
        TimeOnly lesson2EndTime = new (13, 30);
        DayTime lesson2End = new (lesson2EndTime, DayOfWeek.Monday);

        Lesson lesson2 = new (lesson2Begin, lesson2End, 404, "Vova");

        lessons.Add(lesson1);
        lessons.Add(lesson2);
        return lessons;
    }

    private List<ILesson> ListLessons2()
    {
        List<ILesson> lessons = new ();

        TimeOnly lesson1BeginTime = new (17, 30);
        DayTime lesson1Begin = new (lesson1BeginTime, DayOfWeek.Friday);
        TimeOnly lesson1EndTime = new (19, 00);
        DayTime lesson1End = new (lesson1EndTime, DayOfWeek.Friday);

        Lesson lesson1 = new (lesson1Begin, lesson1End, 202, "Sanya");

        TimeOnly lesson2BeginTime = new (21, 00);
        DayTime lesson2Begin = new (lesson2BeginTime, DayOfWeek.Sunday);
        TimeOnly lesson2EndTime = new (22, 30);
        DayTime lesson2End = new (lesson2EndTime, DayOfWeek.Sunday);

        Lesson lesson2 = new (lesson2Begin, lesson2End, 999, "Kolya");

        lessons.Add(lesson1);
        lessons.Add(lesson2);
        return lessons;
    }

    private List<ILesson> ListLessons3()
    {
        List<ILesson> lessons = new ();

        TimeOnly lesson1BeginTime = new (15, 00);
        DayTime lesson1Begin = new (lesson1BeginTime, DayOfWeek.Friday);
        TimeOnly lesson1EndTime = new (16, 30);
        DayTime lesson1End = new (lesson1EndTime, DayOfWeek.Friday);

        Lesson lesson1 = new (lesson1Begin, lesson1End, 101, "Ivan");

        TimeOnly lesson2BeginTime = new (12, 30);
        DayTime lesson2Begin = new (lesson2BeginTime, DayOfWeek.Monday);
        TimeOnly lesson2EndTime = new (14, 00);
        DayTime lesson2End = new (lesson2EndTime, DayOfWeek.Monday);

        Lesson lesson2 = new (lesson2Begin, lesson2End, 404, "Vova");

        lessons.Add(lesson1);
        lessons.Add(lesson2);
        return lessons;
    }
}