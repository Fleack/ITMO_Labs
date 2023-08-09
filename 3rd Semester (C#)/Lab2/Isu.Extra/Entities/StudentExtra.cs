using Isu.Entities;
using Isu.Exceptions;
using Isu.Extra.Interfaces;
using Isu.Models;

namespace Isu.Extra.Entities;

public class StudentExtra : IStudentExtra
{
    public StudentExtra(string name, string surname, int id, DateTime birthday, GroupName groupName, string patronymic = "")
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new IsuExtraException("Failed to construct StudentExtra, name can not be null or white space");
        }

        if (string.IsNullOrWhiteSpace(surname))
        {
            throw new IsuExtraException("Failed to construct StudentExtra, surname can not be null or white space");
        }

        if (groupName is null)
        {
            throw new IsuExtraException("Failed to construct StudentExtra, GroupName can not be null");
        }

        Student = new (name, surname, id, birthday, groupName, patronymic);

        FirstOgnpGroup = null;
        SecondOgnpGroup = null;
    }

    public Student Student { get; }
    public IOgnpGroup? FirstOgnpGroup { get; private set; }
    public IOgnpGroup? SecondOgnpGroup { get; private set; }

    private bool IsFirstOgnpGroupNull => FirstOgnpGroup == null;
    private bool IsSecondOgnpGroupNull => SecondOgnpGroup == null;

    public void SetFirstOgnpGroup(IOgnpGroup newFirstOgnpGroup)
    {
        if (!IsFirstOgnpGroupNull)
        {
            throw new IsuExtraException($"Failed to SetFirstOgnpGroup, First Ognp group: {FirstOgnpGroup} is already set");
        }

        if (StudentSameMegafacultyAsOgnp(newFirstOgnpGroup))
        {
            throw new IsuExtraException($"Failed to SetFirstOgnpGroup, student: {this} is from same facilty as Ognp: {newFirstOgnpGroup}");
        }

        FirstOgnpGroup = newFirstOgnpGroup;
    }

    public void RemoveFirstOgnpGroup()
    {
        if (IsFirstOgnpGroupNull)
        {
            throw new IsuExtraException($"Failed to RemoveFirstOgnpGroup, student: {this} is not in the group");
        }

        FirstOgnpGroup = null;
    }

    public void SetSecondOgnpGroup(IOgnpGroup newSecondOgnpGroup)
    {
        if (!IsSecondOgnpGroupNull)
        {
            throw new IsuExtraException($"Failed to SetSecondOgnpGroup, Second Ognp group: {SecondOgnpGroup} is already set");
        }

        if (StudentSameMegafacultyAsOgnp(newSecondOgnpGroup))
        {
            throw new IsuExtraException($"Failed to SetSecondOgnpGroup, student: {this} is from same facilty as Ognp: {newSecondOgnpGroup}");
        }

        SecondOgnpGroup = newSecondOgnpGroup;
    }

    public void RemoveSecondOgnpGroup()
    {
        if (IsSecondOgnpGroupNull)
        {
            throw new IsuExtraException($"Failed to RemoveFirstOgnpGroup, student: {this} is not in the group");
        }

        FirstOgnpGroup = null;
    }

    private bool StudentSameMegafacultyAsOgnp(IOgnpGroup ognpgroup)
    {
        if (ognpgroup is null)
        {
            throw new IsuExtraException($"Failed to check if StudentSameMegafacultyAsOgnp, given IOgnpGroup can not be null");
        }

        return ognpgroup.Faculty == Student.NameOfGroup.Faculty;
    }
}
