namespace Isu.Extra.Interfaces;

public interface IOgnpGroup
{
    IReadOnlyList<ILesson> Lessons { get; }
    IReadOnlyList<IStudentExtra> Students { get; }
    char Faculty { get; }
    Guid Id { get; }

    void AddStudent(IStudentExtra newStudentExtra);
    void RemoveStudent(IStudentExtra newStudentExtra);
}