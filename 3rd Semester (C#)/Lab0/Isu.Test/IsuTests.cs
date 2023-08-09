using Isu.Entities;
using Isu.Exceptions;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuTests
{
    private readonly IsuService _service = new IsuService();

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var dateTime1 = new DateTime(2000, 1, 1);
        Group new_group = _service.AddGroup("M3109");
        Student? new_student = _service.AddStudent(new_group, "Kolyan", "Bibrickov", dateTime1);

        Assert.Equal("M3109", new_student?.NameOfGroup?.Name);
        Assert.Contains(new_student, new_group.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var dateTime1 = new DateTime(2000, 1, 1);
        Group new_group = _service.AddGroup("M3106");
        for (int i = 0; i < Group.MaxGroupSize; i++)
        {
            _service.AddStudent(new_group, "Vitya", "Bochkin", dateTime1);
        }

        Assert.Throws<GroupAddStudentOverflowException>(() => new_group.AddStudent(_service.AddStudent("Vitya", "Bochkin", dateTime1)));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<IncorrectGourpNameException>(() => _service.AddGroup("BOBA"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var dateTime1 = new DateTime(2000, 1, 1);
        Student? new_student = _service.AddStudent("Sanya", "Kolyanchik", dateTime1);
        Group new_group = _service.AddGroup("M3105");
        _service.ChangeStudentGroup(new_student, new_group);
        Group new_group_to_transfer = _service.AddGroup("M32051");
        _service.ChangeStudentGroup(new_student, new_group_to_transfer);

        Assert.Equal("M32051", new_student?.NameOfGroup?.Name);
        Assert.DoesNotContain(new_student, new_group.Students);
        Assert.Contains(new_student, new_group_to_transfer.Students);
    }
}