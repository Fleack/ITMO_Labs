using Isu.Extra.Models;

namespace Isu.Extra.Interfaces;

public interface IOgnpCourse
{
    IReadOnlyList<IOgnpStream> OgnpStreams { get; }
    string Name { get; }
    char Faculty { get; }
    Guid Id { get; }

    OgnpStream AddNewStream();
}