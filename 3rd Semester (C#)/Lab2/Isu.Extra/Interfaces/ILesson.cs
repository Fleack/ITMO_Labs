using Isu.Extra.Models;

namespace Isu.Extra.Interfaces;

public interface ILesson
{
    DayTime LessonBeginTime { get; }
    DayTime LessonEndTime { get; }
    string Teacher { get; }
}
