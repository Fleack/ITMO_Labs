namespace Isu.Extra.Models
{
    public class DayTime
    {
        public DayTime(TimeOnly time, DayOfWeek dayOfWeek)
        {
            Time = time;
            DayOfWeek = dayOfWeek;
        }

        public TimeOnly Time { get; }
        public DayOfWeek DayOfWeek { get; }
    }
}
