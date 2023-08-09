using Isu.Exceptions;
using Isu.Extra.Interfaces;

namespace Isu.Extra.Models;
public class OgnpStream : IOgnpStream
{
    private const uint MaxGroupsAmount = 5;
    private List<IOgnpGroup> _ognpGroups = new ();

    public OgnpStream(char faculty, string name, uint streamIndex)
    {
        if (!char.IsLetter(faculty))
        {
            throw new IsuExtraException($"Failed to construct OgnpStream, faculty: {faculty} has to be a letter");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new IsuExtraException($"Failed to construct OgnpStream, name: {faculty} can not be null or white space");
        }

        Faculty = faculty;
        Name = name + '/' + streamIndex.ToString();
    }

    public char Faculty { get; }
    public string Name { get; }
    public Guid Id { get; }
    public IReadOnlyList<IOgnpGroup> OgnpGroups => _ognpGroups;
    private bool IsGroupsListFull => _ognpGroups.Count == MaxGroupsAmount;

    public OgnpGroup AddNewGroup(List<ILesson> lessons, List<IStudentExtra>? students)
    {
        if (lessons is null)
        {
            throw new IsuExtraException("Failed to AddNewGroup, lessons can not be null");
        }

        if (IsGroupsListFull)
        {
            throw new IsuExtraException("Failed to AddNewGroup, group list is full");
        }

        if (students is null)
        {
            students = new ();
        }

        OgnpGroup newOgnpGroup = new (Faculty, Name, (uint)OgnpGroups.Count, lessons, students);
        _ognpGroups.Add(newOgnpGroup);
        return newOgnpGroup;
    }
}