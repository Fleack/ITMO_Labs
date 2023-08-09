using Isu.Exceptions;
namespace Isu.Models;

public class CourseNumber
{
    private const int MaxCourseNumber = 5;
    private const int MinCourseNumber = 0;
    private const int NoCourse = 0;

    public CourseNumber(int? number)
    {
        if (number is > MaxCourseNumber or < MinCourseNumber)
        {
            throw new IncorrectCourseNumberException($"Failed to set course number. Given value: {number} is incorrect course number. Course number has to be between {MinCourseNumber} and {MaxCourseNumber}");
        }

        Number = number ?? NoCourse;
    }

    public int Number { get; }
}