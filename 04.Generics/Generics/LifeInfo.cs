using System;
using UntestableLibrary;

namespace Generics
{
    public static class LifeInfo
    {
        public static bool IsTodayHoliday()
        {
            var dayOfWeek = DateTime.Today.DayOfWeek;
            var holiday = ULConfigurationManager.GetProperty<DayOfWeek>("Holiday", DayOfWeek.Sunday);
            switch (holiday)
            {
                case DayOfWeek.Sunday:
                    return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
                case DayOfWeek.Monday:
                    return dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Monday;
                case DayOfWeek.Tuesday:
                    return dayOfWeek == DayOfWeek.Monday || dayOfWeek == DayOfWeek.Tuesday;
                case DayOfWeek.Wednesday:
                    return dayOfWeek == DayOfWeek.Tuesday || dayOfWeek == DayOfWeek.Wednesday;
                case DayOfWeek.Thursday:
                    return dayOfWeek == DayOfWeek.Wednesday || dayOfWeek == DayOfWeek.Thursday;
                case DayOfWeek.Friday:
                    return dayOfWeek == DayOfWeek.Thursday || dayOfWeek == DayOfWeek.Friday;
                case DayOfWeek.Saturday:
                    return dayOfWeek == DayOfWeek.Friday || dayOfWeek == DayOfWeek.Saturday;
                default:
                    return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
            }
        }
    }
}
