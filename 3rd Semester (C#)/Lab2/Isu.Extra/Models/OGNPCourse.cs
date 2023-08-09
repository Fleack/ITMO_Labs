using Isu.Exceptions;
using Isu.Extra.Interfaces;

namespace Isu.Extra.Models;
public class OgnpCourse : IOgnpCourse
{
    private const uint MaxStreamsAmount = 5;
    private List<IOgnpStream> _ognpStreams = new ();

    public OgnpCourse(char faculty, string name)
    {
        if (!char.IsLetter(faculty))
        {
            throw new IsuExtraException($"Failed to construct OgnpCourse, faculty: {faculty} has to be a letter");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new IsuExtraException($"Failed to construct OgnpCourse, name can not be null or white space");
        }

        Faculty = faculty;
        Name = name;
    }

    public IReadOnlyList<IOgnpStream> OgnpStreams => _ognpStreams;
    public string Name { get; }
    public char Faculty { get; }
    public Guid Id { get; }
    private bool IsStreamsListFull => _ognpStreams.Count == MaxStreamsAmount;

    public OgnpStream AddNewStream()
    {
        if (IsStreamsListFull)
        {
            throw new IsuExtraException($"Failed to AddNewStream, list: {OgnpStreams} of OgnpStreams is full");
        }

        OgnpStream newOgnpStream = new (Faculty, Name, (uint)OgnpStreams.Count);
        _ognpStreams.Add(newOgnpStream);
        return newOgnpStream;
    }
}