﻿using System.Collections.Generic;
using System.Linq;
using Timetabler.CoreData.Events;
using Timetabler.CoreData.Interfaces;
using Timetabler.Data.Display.Interfaces;

namespace Timetabler.Data.Display
{
    /// <summary>
    /// This class represents a displayed train segment: ie, a column in a timetable.
    /// </summary>
    public class TrainSegmentModel : IWatchableItem
    {
        /// <summary>
        /// The unique ID of the train this segment represents.
        /// </summary>
        public string TrainId { get; set; }

        /// <summary>
        /// The headcode.
        /// </summary>
        public string Headcode { get; set; }

        /// <summary>
        /// The loco diagram.  Displayed if any are present.
        /// </summary>
        public string LocoDiagram { get; set; }

        /// <summary>
        /// Footnotes to appear in the header of this segment.
        /// </summary>
        public string Footnotes { get; set; }

        /// <summary>
        /// The table code of the train class the train belongs to.
        /// </summary>
        public string TrainClass { get; set; }

        /// <summary>
        /// The text to appear in the am/pm row (if displayed).
        /// </summary>
        public string HalfOfDay
        {
            get
            {
                if (Timings == null || Timings.Count == 0)
                {
                    return "";
                }
                TrainLocationTimeModel firstTimingPoint = Timings.Select(e => e as TrainLocationTimeModel).FirstOrDefault(e => e?.ActualTime != null);
                if (firstTimingPoint == null)
                {
                    return "";
                }
                return firstTimingPoint.ActualTime.HalfOfDay.ToNameString();
            }
        }

        /// <summary>
        /// Draw separator above this segment if it does not start at the first location.
        /// </summary>
        public bool IncludeSeparatorAbove { get; set; }

        /// <summary>
        /// Draw separator below this segment if it does not end at the last location.
        /// </summary>
        public bool IncludeSeparatorBelow { get; set; }

        /// <summary>
        /// Text to include vertically in the column.
        /// </summary>
        public string InlineNote { get; set; }

        /// <summary>
        /// List of timings (or other data) that comprise this segment.
        /// </summary>
        public List<ILocationEntry> Timings { get; set; }

        /// <summary>
        /// Footnotes which are used by this segment whose definitions should appear on the page with the timetable section.
        /// </summary>
        public List<FootnoteDisplayModel> PageFootnotes { get; set; }

        /// <summary>
        /// The data contained in the <see cref="Timings"/> property, indexed by location ID.
        /// </summary>
        public Dictionary<string, ILocationEntry> TimingsIndex { get; set; }

        /// <summary>
        /// The data to be displayed in the "To Work" row of the timetable, if there is one.
        /// </summary>
        public GenericTimeModel ToWorkCell { get; set; }

        /// <summary>
        /// The data to be displayed in the "Loco To Work" row of the timetable, if there is one.
        /// </summary>
        public GenericTimeModel LocoToWorkCell { get; set; }

        /// <summary>
        /// True if this segment represents a continuation from an earlier segment (which may or may not be displayed) and therefore has a left-pointing arrow above its first cell.
        /// </summary>
        public bool ContinuationFromEarlier { get; set; }

        /// <summary>
        /// True if this segment is continued by a later segment (which may or may not be displayed) and therefore has a right-pointing arrow below its last cell.
        /// </summary>
        public bool ContinuesLater { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TrainSegmentModel()
        {
            Timings = new List<ILocationEntry>();
            TimingsIndex = new Dictionary<string, ILocationEntry>();
            PageFootnotes = new List<FootnoteDisplayModel>();
        }

        /// <summary>
        /// Constructor which initialises the header fields of this segment.
        /// </summary>
        /// <param name="train">The train to use for populating the header fields.</param>
        public TrainSegmentModel(Train train) : this()
        {
            TrainId = train.Id;
            Headcode = train.Headcode ?? string.Empty;
            LocoDiagram = train.LocoDiagram ?? string.Empty;
            TrainClass = (train.TrainClass != null) ? train.TrainClass.TableCode : string.Empty;
            Footnotes = string.Join(",", train.Footnotes.Select(f => f.Symbol));
            PageFootnotes.AddRange(train.Footnotes.Where(f => f.DefinedOnPages).Select(f => f.ToFootnoteDisplayModel()));
            IncludeSeparatorAbove = train.IncludeSeparatorAbove;
            IncludeSeparatorBelow = train.IncludeSeparatorBelow;
            InlineNote = train.InlineNote;
            ToWorkCell = CreateToWorkCell(train.ToWork);
            LocoToWorkCell = CreateToWorkCell(train.LocoToWork); 
        }

        /// <summary>
        /// Update page footnote data models to match the passed-in footnote data.
        /// </summary>
        /// <param name="notes"></param>
        public void UpdatePageFootnotes(ICollection<Note> notes)
        {
            if (notes == null || notes.Count == 0)
            {
                return;
            }
            for (int i = 0; i < PageFootnotes.Count; ++i)
            {
                Note note = notes.FirstOrDefault(n => n.Id == PageFootnotes[i].NoteId);
                if (note != null)
                {
                    PageFootnotes[i] = note.ToFootnoteDisplayModel();
                }
            }
        }

        private GenericTimeModel CreateToWorkCell(ToWork toWork)
        {
            if (toWork == null)
            {
                return null;
            }

            GenericTimeModel output = new GenericTimeModel();
            toWork.UpdateModel(output, null);

            return output;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public event ModifiedEventHandler Modified;

        /// <summary>
        /// Create a deep copy of this object.
        /// </summary>
        /// <returns>A <see cref="TrainSegmentModel" /> which is a deep copy of this object.</returns>
        public TrainSegmentModel Copy()
        {
            TrainSegmentModel tsm = new TrainSegmentModel
            {
                Footnotes = Footnotes,
                Headcode = Headcode,
                IncludeSeparatorAbove = IncludeSeparatorAbove,
                IncludeSeparatorBelow = IncludeSeparatorBelow,
                InlineNote = InlineNote,
                LocoDiagram = LocoDiagram,
                Timings = Timings.Select(t => t.Copy()).ToList(),
                ToWorkCell = ToWorkCell != null ? ToWorkCell.Copy() : null,
                TrainClass = TrainClass,
                TrainId = TrainId,
                PageFootnotes = PageFootnotes.Select(f => f.Copy()).ToList(),
                ContinuationFromEarlier = ContinuationFromEarlier,
                ContinuesLater = ContinuesLater,
            };
            tsm.UpdateTimingsIndex();
            return tsm;
        }

        /// <summary>
        /// When called with a parameter that is greater than zero and less than the index of the final timing point, this method will split the instance into two.  The instance called
        /// has its timing points after the index truncated in-place; the method returns a second instance which starts with the timing point at the given index.
        /// </summary>
        /// <param name="idx">The index of the timing point which will become the first timing point of the new segment after the split</param>
        /// <param name="overlap">The number of timing points that should overlap, and be retained by both segments.</param>
        /// <returns>A <see cref="TrainSegmentModel" /> consisting of the second part of the newly-split segment.</returns>
        public TrainSegmentModel SplitAtIndex(int idx, int overlap)
        {
            if (idx <= 0 || idx >= Timings.Count - 1)
            {
                return null;
            }
            TrainSegmentModel secondPortion = Copy();
            int firstStop = idx + overlap;
            if (firstStop < Timings.Count)
            {
                Timings.RemoveRange(firstStop, Timings.Count - firstStop);
            }
            secondPortion.Timings.RemoveRange(0, idx);
            UpdateTimingsIndex();
            secondPortion.UpdateTimingsIndex();
            ContinuesLater = true;
            secondPortion.ContinuationFromEarlier = true;
            return secondPortion;
        }

        private void UpdateTimingsIndex()
        {
            TimingsIndex = Timings.ToDictionary(t => t.LocationKey, t => t);
        }
    }
}
