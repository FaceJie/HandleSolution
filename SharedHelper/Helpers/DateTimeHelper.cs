﻿using System;

namespace SharedHelper.Helpers
{
    public static class DateTimeHelper
    {
        public static bool OverlappingPeriods(DateTime startA, DateTime endA, DateTime startB, DateTime endB)
        {
            return startA <= endB && startB <= endA;
        }
    }
}
