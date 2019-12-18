﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace Timetabler.SerialData.Xml
{
    /// <summary>
    /// This class describes a network map in serialisable form.
    /// </summary>
    public class NetworkMapModel
    {
        /// <summary>
        /// List of locations in this map.
        /// </summary>
        [XmlArray]
        [XmlArrayItem(ElementName = "Location")]
        public List<LocationModel> LocationList { get; } = new List<LocationModel>();

        /// <summary>
        /// List of block sections in this network map.
        /// </summary>
        [XmlArray]
        [XmlArrayItem(ElementName = "BlockSection")]
        public List<BlockSectionModel> BlockSections { get; } = new List<BlockSectionModel>();

        /// <summary>
        /// List of signalboxes in this network map.
        /// </summary>
        [XmlArray]
        [XmlArrayItem(ElementName = "Signalbox")]
        public List<SignalboxModel> Signalboxes { get; } = new List<SignalboxModel>();
    }
}
