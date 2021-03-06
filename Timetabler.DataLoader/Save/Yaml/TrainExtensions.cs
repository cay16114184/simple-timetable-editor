﻿using System;
using System.Linq;
using Timetabler.Data;
using Timetabler.SerialData.Yaml;

namespace Timetabler.DataLoader.Save.Yaml
{
    /// <summary>
    /// YAML-related extension methods for the <see cref="Train" /> class.
    /// </summary>
    public static class TrainExtensions
    {
        /// <summary>
        /// Convert a <see cref="Train" /> instance to a <see cref="TrainModel" /> instance.
        /// </summary>
        /// <param name="train">The object to be converted.</param>
        /// <returns>A <see cref="TrainModel" /> instance containing the same data as the parameter in serialisable form.</returns>
        /// <exception cref="NullReferenceException">Thrown if the parameter is <c>null</c>.</exception>
        public static TrainModel ToYamlTrainModel(this Train train)
        {
            if (train is null)
            {
                throw new NullReferenceException();
            }

            TrainModel model = new TrainModel
            {
                Id = train.Id,
                GraphProperties = train.GraphProperties.ToYamlGraphTrainPropertiesModel(),
                Headcode = train.Headcode,
                LocoDiagram = train.LocoDiagram,
                TrainClassId = train.TrainClassId,
                IncludeSeparatorAbove = train.IncludeSeparatorAbove,
                IncludeSeparatorBelow = train.IncludeSeparatorBelow,
                InlineNote = train.InlineNote,
                SetToWork = train.ToWork?.ToYamlToWorkModel(),
                LocoToWork = train.LocoToWork?.ToYamlToWorkModel(),
            };

            model.FootnoteIds.AddRange(train.Footnotes.Select(n => n.Id));
            model.TrainTimes.AddRange(train.TrainTimes.Select(tt => tt.ToYamlTrainLocationTimeModel()));

            return model;
        }
    }
}
