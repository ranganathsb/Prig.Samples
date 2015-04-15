using System;

namespace QuickTour
{
    public class LifeInfo
    {
        public static bool IsNowLunchBreak()
        {
            var now = DateTime.Now;
            return 12 <= now.Hour && now.Hour < 13;
        }
    }
}
