﻿using System;
using Timetabler.Data;
using Timetabler.SerialData.Xml;

namespace Timetabler.DataLoader.Load.Xml
{
    /// <summary>
    /// Extension methods for loading a TrainClass object from serializable form.
    /// </summary>
    public static class TrainClassModelExtensions
    {
        /// <summary>
        /// Convert a <see cref="TrainClassModel"/> instance into a <see cref="TrainClass"/> instance.
        /// </summary>
        /// <param name="model">The instance to convert.</param>
        /// <returns>A <see cref="TrainClass"/> instance.</returns>
        public static TrainClass ToTrainClass(this TrainClassModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new TrainClass
            {
                Id = model.Id,
                Description = model.Description,
                TableCode = model.TableCode,
            };
        }
    }
}
