using Isu.Exceptions;
using Isu.Extra.Interfaces;

namespace Isu.Extra.Models;

public class Lesson : ILesson
{
    private const uint IncorrectClassroomNumber = 0;
    private const uint LessonTime = 90;
    public Lesson(DayTime lessonBeginTime, DayTime lessonEndTime, uint classroomNumber, string teacher)
    {
        if (!IsOneLesson(lessonBeginTime, lessonEndTime))
        {
            throw new IsuExtraException($"Failed to construct Lesson, difference between " +
                $"lessonBeginTime: {lessonBeginTime} and" +
                $"lessonEndTime: {lessonEndTime}" +
                $"has to be {LessonTime} minutes");
        }

        if (classroomNumber is IncorrectClassroomNumber)
        {
            throw new IsuExtraException($"Failed to construct Lesson, classroomNumber can not be {IncorrectClassroomNumber}");
        }

        if (string.IsNullOrWhiteSpace(teacher))
        {
            throw new IsuExtraException($"Failed to construct Lesson, teacher's name can not be null or white space");
        }

        LessonBeginTime = lessonBeginTime;
        LessonEndTime = lessonEndTime;
        СlassroomNumber = classroomNumber;
        Teacher = teacher;
    }

    public DayTime LessonBeginTime { get; }
    public DayTime LessonEndTime { get; }
    public uint СlassroomNumber { get; }
    public string Teacher { get; }

    private bool IsOneLesson(DayTime lessonBeginTime, DayTime lessonEndTime)
    {
        if (lessonEndTime.DayOfWeek != lessonBeginTime.DayOfWeek)
        {
            return false;
        }

        if ((lessonEndTime.Time - lessonBeginTime.Time).TotalMinutes != LessonTime)
        {
            return false;
        }

        return true;
    }
}
