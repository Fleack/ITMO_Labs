using Isu.Extra.Models;

namespace Isu.Extra.Interfaces;

public interface IOgnpStream
{
    char Faculty { get; }
    Guid Id { get; }
    IReadOnlyList<IOgnpGroup> OgnpGroups { get; }

    OgnpGroup AddNewGroup(List<ILesson> lessons, List<IStudentExtra>? students);
}