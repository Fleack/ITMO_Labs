using Isu.Entities;

namespace Isu.Extra.Interfaces;

public interface IStudentExtra
{
    Student Student { get; }
    IOgnpGroup? FirstOgnpGroup { get; }
    IOgnpGroup? SecondOgnpGroup { get; }

    void SetFirstOgnpGroup(IOgnpGroup newFirstOgnpGroup);
    void RemoveFirstOgnpGroup();
    void SetSecondOgnpGroup(IOgnpGroup newSecondOgnpGroup);
    void RemoveSecondOgnpGroup();
}
