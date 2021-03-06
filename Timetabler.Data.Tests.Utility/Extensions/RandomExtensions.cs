﻿using System;
using Tests.Utility.Extensions;

namespace Timetabler.Data.Tests.Utility.Extensions
{
    public static class RandomExtensions
    {
        public static ClockType NextClockType(this Random random)
        {
            if (random is null)
            {
                throw new NullReferenceException();
            }

            return (random.Next(2) == 0) ? ClockType.TwelveHourClock : ClockType.TwentyFourHourClock; 
        }

        public static GraphEditStyle NextGraphEditStyle(this Random random)
        {
            if (random is null)
            {
                throw new NullReferenceException();
            }

            GraphEditStyle[] styles = new[] { GraphEditStyle.Free, GraphEditStyle.PreserveSectionTimes };

            return styles[random.Next(styles.Length)];
        }

        public static Note NextNote(this Random random)
        {
            if (random is null)
            {
                throw new NullReferenceException();
            }

            return new Note
            {
                AppliesToTimings = random.NextBoolean(),
                AppliesToTrains = random.NextBoolean(),
                DefinedInGlossary = random.NextBoolean(),
                DefinedOnPages = random.NextBoolean(),
                Definition = random.NextString(random.Next(50)),
                Id = random.NextHexString(8),
                Symbol = random.NextString(random.Next(1, 2)),
            };
        }
    }
}
