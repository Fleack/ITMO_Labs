using Isu.Entities;

namespace Isu.Extra.Interfaces
{
    public interface IGroupExtra
    {
        IReadOnlyList<ILesson> Lessons { get; }
        IReadOnlyList<IStudentExtra> Students { get; }
        Group Group { get; }

        void AddStudent(IStudentExtra newStudent);
        void RemoveStudent(IStudentExtra student);
    }
}