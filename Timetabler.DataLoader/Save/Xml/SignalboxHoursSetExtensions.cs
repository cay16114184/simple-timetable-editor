﻿using System;
using System.Linq;
using Timetabler.Data;
using Timetabler.SerialData.Xml;

namespace Timetabler.DataLoader.Save.Xml
{
    /// <summary>
    /// Extension methods for saving <see cref="SignalboxHoursSet" /> instances in a serialisable form.
    /// </summary>
    public static class SignalboxHoursSetExtensions
    {
        /// <summary>
        /// Convert a <see cref="SignalboxHoursSet" /> instance into a <see cref="SignalboxHoursSetModel" /> instance.
        /// </summary>
        /// <param name="hoursSet">The data to be converted.</param>
        /// <returns>A <see cref="SignalboxHoursSetModel" /> instance that is equivalent to the hoursSet parameter.</returns>
        public static SignalboxHoursSetModel ToSignalboxHoursSetModel(this SignalboxHoursSet hoursSet)
        {
            if (hoursSet is null)
            {
                throw new ArgumentNullException(nameof(hoursSet));
            }

            SignalboxHoursSetModel model = new SignalboxHoursSetModel
            {
                Category = hoursSet.Category,
            };
            model.Signalboxes.AddRange(hoursSet.Hours.Values.Select(h => h.ToSignalboxHoursModel()));
            return model;
        }
    }
}
