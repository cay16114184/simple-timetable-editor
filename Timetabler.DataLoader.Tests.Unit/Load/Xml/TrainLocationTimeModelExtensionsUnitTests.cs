﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Utility.Extensions;
using Tests.Utility.Providers;
using Timetabler.Data;
using Timetabler.DataLoader.Load.Xml;
using Timetabler.DataLoader.Tests.Unit.TestHelpers.Extensions;
using Timetabler.SerialData.Xml;

namespace Timetabler.DataLoader.Tests.Unit.Load.Xml
{
    [TestClass]
    public class TrainLocationTimeModelExtensionsUnitTests
    {
        private static readonly Random _random = RandomProvider.Default;

        [TestMethod]
        public void TrainLocationTimeModelExtensionsToTrainLocationTimeMethodReturnsNonNullIfParametersAreNotNull()
        {
            TrainLocationTimeModel testObject = new TrainLocationTimeModel();

            TrainLocationTime testResult = testObject.ToTrainLocationTime(new Dictionary<string, Location>(), new Dictionary<string, Note>(), GetDocumentOptions());

            Assert.IsNotNull(testResult);
        }

        [TestMethod]
        public void TrainLocationTimeModelExtensionsClassToTrainLocationTimeMethodReturnsNullIfFirstParameterIsNull()
        {
            TrainLocationTimeModel testObject = null;

            TrainLocationTime testResult = testObject.ToTrainLocationTime(new Dictionary<string, Location>(), new Dictionary<string, Note>(), GetDocumentOptions());

            Assert.IsNull(testResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TrainLocationTimeModelExtensionsClass_ToTrainLocationTimeMethod_ThrowsArgumentNullException_IfFourthParameterIsNull()
        {
            TrainLocationTimeModel testObject = new TrainLocationTimeModel();

            _ = testObject.ToTrainLocationTime(new Dictionary<string, Location>(), new Dictionary<string, Note>(), null);

            Assert.Fail();
        }

        [TestMethod]
        public void TrainLocationTimeModelExtensionsClass_ToTrainLocationTimeMethod_ThrowsArgumentNullExceptionWithCorrectParamNameProperty_IfFourthParameterIsNull()
        {
            TrainLocationTimeModel testObject = new TrainLocationTimeModel();

            try
            {
                _ = testObject.ToTrainLocationTime(new Dictionary<string, Location>(), new Dictionary<string, Note>(), null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("options", ex.ParamName);
            }
        }

        [TestMethod]
        public void TrainLocationTimeModelExtensionsToTrainLocationTimeMethodReturnsObjectWithCorrectLocationPropertyIfLocationIdPropertyOfFirstParameterIsPresentAsKeyInSecondParameter()
        {
            Dictionary<string, Location> locationMap = GetRandomLocationMap();
            Dictionary<string, Note> noteMap = GetRandomNotes();
            TrainLocationTimeModel testObject = GetTrainLocationTimeModel(locationMap, noteMap);

            TrainLocationTime testResult = testObject.ToTrainLocationTime(locationMap, noteMap, GetDocumentOptions());

            Assert.AreSame(locationMap[testObject.LocationId], testResult.Location);
        }

        [TestMethod]
        public void TrainLocationTimeModelExtensionsToTrainLocationTimeMethodReturnsObjectWithNullLocationPropertyIfLocationIdPropertyOfFirstParameterIsNotPresentAsKeyInSecondParameter()
        {
            Dictionary<string, Location> locationMap = GetRandomLocationMap();
            Dictionary<string, Note> noteMap = GetRandomNotes();
            TrainLocationTimeModel testObject = GetTrainLocationTimeModel(locationMap, noteMap);
            do
            {
                testObject.LocationId = _random.NextHexString(8);
            } while (locationMap.ContainsKey(testObject.LocationId));

            TrainLocationTime testResult = testObject.ToTrainLocationTime(locationMap, noteMap, GetDocumentOptions());

            Assert.IsNull(testResult.Location);
        }

        [TestMethod]
        public void TrainLocationTimeModelExtensionsClassToTrainLocationTimeMethodReturnsObjectWithNullLocationPropertyIfLocationIdPropertyOfFirstParameterIsNull()
        {
            Dictionary<string, Location> locationMap = GetRandomLocationMap();
            Dictionary<string, Note> noteMap = GetRandomNotes();
            TrainLocationTimeModel testObject = GetTrainLocationTimeModel(locationMap, noteMap);
            testObject.LocationId = null;

            TrainLocationTime testResult = testObject.ToTrainLocationTime(locationMap, noteMap, GetDocumentOptions());

            Assert.IsNull(testResult.Location);
        }

        [TestMethod]
        public void TrainLocationTimeModelExtensionsClassToTrainLocationTimeMethodReturnsObjectWithCorrectPassProperty()
        {
            Dictionary<string, Location> locationMap = GetRandomLocationMap();
            Dictionary<string, Note> noteMap = GetRandomNotes();
            TrainLocationTimeModel testObject = GetTrainLocationTimeModel(locationMap, noteMap);

            TrainLocationTime testResult = testObject.ToTrainLocationTime(locationMap, noteMap, GetDocumentOptions());

            Assert.AreEqual(testObject.Pass, testResult.Pass);
        }

        [TestMethod]
        public void TrainLocationTimeModelExtensionsClassToTrainLocationTimeMethodReturnsObjectWithCorrectPathProperty()
        {
            Dictionary<string, Location> locationMap = GetRandomLocationMap();
            Dictionary<string, Note> noteMap = GetRandomNotes();
            TrainLocationTimeModel testObject = GetTrainLocationTimeModel(locationMap, noteMap);

            TrainLocationTime testResult = testObject.ToTrainLocationTime(locationMap, noteMap, GetDocumentOptions());

            Assert.AreEqual(testObject.Path, testResult.Path);
        }

        [TestMethod]
        public void TrainLocationTimeModelExtensionsClassToTrainLocationTimeMethodReturnsObjectWithCorrectPlatformProperty()
        {
            Dictionary<string, Location> locationMap = GetRandomLocationMap();
            Dictionary<string, Note> noteMap = GetRandomNotes();
            TrainLocationTimeModel testObject = GetTrainLocationTimeModel(locationMap, noteMap);

            TrainLocationTime testResult = testObject.ToTrainLocationTime(locationMap, noteMap, GetDocumentOptions());

            Assert.AreEqual(testObject.Platform, testResult.Platform);
        }

        [TestMethod]
        public void TrainLocationTimeModelExtensionsClassToTrainLocationTimeMethodReturnsObjectWithCorrectLineProperty()
        {
            Dictionary<string, Location> locationMap = GetRandomLocationMap();
            Dictionary<string, Note> noteMap = GetRandomNotes();
            TrainLocationTimeModel testObject = GetTrainLocationTimeModel(locationMap, noteMap);

            TrainLocationTime testResult = testObject.ToTrainLocationTime(locationMap, noteMap, GetDocumentOptions());

            Assert.AreEqual(testObject.Line, testResult.Line);
        }

        [TestMethod]
        public void TrainLocationTimeModelExtensionsClassToTrainLocationTimeMethodReturnsObjectWithCorrectFormattingStringsProperty()
        {
            Dictionary<string, Location> locationMap = GetRandomLocationMap();
            Dictionary<string, Note> noteMap = GetRandomNotes();
            DocumentOptions options = GetDocumentOptions();
            TrainLocationTimeModel testObject = GetTrainLocationTimeModel(locationMap, noteMap);

            TrainLocationTime testResult = testObject.ToTrainLocationTime(locationMap, noteMap, options);

            Assert.AreEqual(options.FormattingStrings.Complete, testResult.FormattingStrings.Complete);
            Assert.AreEqual(options.FormattingStrings.Hours, testResult.FormattingStrings.Hours);
            Assert.AreEqual(options.FormattingStrings.Minutes, testResult.FormattingStrings.Minutes);
            Assert.AreEqual(options.FormattingStrings.TimeWithoutFootnotes, testResult.FormattingStrings.TimeWithoutFootnotes);

        }

        private TrainLocationTimeModel GetTrainLocationTimeModel(Dictionary<string, Location> locationMap, Dictionary<string, Note> notes)
        {
            return GetTrainLocationTimeModel(locationMap, notes, _random.Next(2) == 0);
        }

        private static TrainLocationTimeModel GetTrainLocationTimeModel(Dictionary<string, Location> locationMap, Dictionary<string, Note> notes, bool hasArrivalTime)
        {
            string[] locationKeys = locationMap.Keys.ToArray();
            string[] noteKeys = notes.Keys.ToArray();
            TrainLocationTimeModel tlt = new TrainLocationTimeModel
            {
                ArrivalTime = hasArrivalTime ? new TrainTimeModel { Time = _random.NextTimeOfDayModel(0, 86399) } : null,
                Line = _random.NextString(_random.Next(3)),
                Path = _random.NextString(_random.Next(3)),
                Platform = _random.NextString(_random.Next(3)),
                Pass = (!hasArrivalTime) && _random.Next(2) == 0,
                LocationId = locationKeys[_random.Next(locationKeys.Length)],
            };
            tlt.DepartureTime = hasArrivalTime ? 
                new TrainTimeModel
                {
                    Time = _random.NextTimeOfDayModel(tlt.ArrivalTime.Time.Hours24 * 3600 + tlt.ArrivalTime.Time.Minutes * 60 + tlt.ArrivalTime.Time.Seconds, 0),
                } : 
                new TrainTimeModel { Time = _random.NextTimeOfDayModel(0, 0) };
            int fnCount = _random.Next(2);
            for (int i = 0, j = 0; i < fnCount && j < noteKeys.Length; ++j)
            {
                if (_random.Next((noteKeys.Length - j) / (fnCount - i)) == 0)
                {
                    tlt.DepartureTime.FootnoteIds.Add(noteKeys[j]);
                    ++i;
                }
            }
            if (hasArrivalTime)
            {
                fnCount = _random.Next(2);
                for (int i = 0, j = 0; i < fnCount && j < noteKeys.Length; ++j)
                {
                    if (_random.Next((noteKeys.Length - j) / (fnCount - i)) == 0)
                    {
                        tlt.ArrivalTime.FootnoteIds.Add(noteKeys[j]);
                        ++i;
                    }
                }
            }

            return tlt;
        }

        private Dictionary<string, Location> GetRandomLocationMap()
        {
            int count = _random.Next(1, 20);
            Dictionary<string, Location> map = new Dictionary<string, Location>(count);
            for (int i = 0; i < count; ++i)
            {
                Location loc = new Location
                {
                    DownArrivalDepartureAlwaysDisplayed = _random.NextArrivalDepartureOptions(),
                    DownRoutingCodesAlwaysDisplayed = _random.NextTrainRoutingOptions(),
                    EditorDisplayName = _random.NextString(_random.Next(5, 25)),
                    FontType = _random.NextLocationFontType(),
                    GraphDisplayName = _random.NextString(_random.Next(1, 5)),
                    Mileage = _random.NextDistance(),
                    TimetableDisplayName = _random.NextString(_random.Next(5, 25)),
                    Tiploc = _random.NextString(_random.Next(1, 5)),
                    UpArrivalDepartureAlwaysDisplayed = _random.NextArrivalDepartureOptions(),
                    UpRoutingCodesAlwaysDisplayed = _random.NextTrainRoutingOptions()
                };
                do
                {
                    loc.Id = _random.NextHexString(8);
                } while (map.ContainsKey(loc.Id));

                map.Add(loc.Id, loc);
            }

            return map;
        }

        private Dictionary<string, Note> GetRandomNotes()
        {
            int count = _random.Next(1, 50);
            Dictionary<string, Note> notes = new Dictionary<string, Note>(count);
            for (int i = 0; i < count; ++i)
            {
                Note note = new Note
                {
                    AppliesToTimings = true,
                    AppliesToTrains = _random.Next(2) == 0,
                    DefinedInGlossary = _random.Next(2) == 0,
                    Definition = _random.NextString(_random.Next(10, 70)),
                    Symbol = _random.NextString(_random.Next(1, 2)),
                };
                if (note.DefinedInGlossary)
                {
                    note.DefinedOnPages = _random.Next(10) == 0;
                }
                else
                {
                    note.DefinedOnPages = true;
                }

                do
                {
                    note.Id = _random.NextHexString(8);
                } while (notes.ContainsKey(note.Id));

                notes.Add(note.Id, note);
            }

            return notes;
        }

        private DocumentOptions GetDocumentOptions()
        {
            return new DocumentOptions
            {
                ClockType = _random.NextBoolean() ? ClockType.TwelveHourClock : ClockType.TwentyFourHourClock,
                DisplayTrainLabelsOnGraphs = _random.NextBoolean(),
            };
        }
    }
}
