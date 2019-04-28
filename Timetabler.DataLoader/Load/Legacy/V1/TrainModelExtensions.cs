﻿using System.Collections.Generic;
using Timetabler.Data;
using Timetabler.XmlData;

namespace Timetabler.DataLoader.Load.Legacy.V1
{
    /// <summary>
    /// Extension methods for loading train objects from serializable form into in-memory form.
    /// </summary>
    public static class TrainModelExtensions
    {
        /// <summary>
        /// Convert a serializable <see cref="XmlData.Legacy.V1.TrainModel"/> object into a <see cref="Train"/> object.
        /// </summary>
        /// <param name="model">The object to load.</param>
        /// <param name="locations">Dictionary of locations occurring in the timetable.</param>
        /// <param name="trainClasses">Dictionary of train classes occurring in the timetable.</param>
        /// <param name="notes">Dictionary of footnotes occurring in the timetable.</param>
        /// <param name="options">Timetable document options object.</param>
        /// <returns>A <see cref="Train"/> instance.</returns>
        public static Train ToTrain(
            this XmlData.Legacy.V1.TrainModel model, 
            Dictionary<string, Location> locations, 
            Dictionary<string, TrainClass> trainClasses, 
            Dictionary<string, Note> notes,
            DocumentOptions options)
        {
            Train trn = new Train
            {
                Id = model.Id,
                Headcode = model.Headcode,
                LocoDiagram = string.Empty,
                TrainClass = string.IsNullOrEmpty(model.TrainClassId) ? null : trainClasses?[model.TrainClassId],
                GraphProperties = model.GraphProperties.ToGraphTrainProperties(),
            };

            foreach (TrainLocationTimeModel timingPoint in model.TrainTimes)
            {
                trn.TrainTimes.Add(timingPoint.ToTrainLocationTime(locations, notes, options));
            }

            foreach (string noteId in model.FootnoteIds)
            {
                trn.Footnotes.Add(notes?[noteId]);
            }

            return trn;
        }
    }
}