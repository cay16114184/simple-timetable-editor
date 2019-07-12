﻿using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using Timetabler.Adapters;
using Timetabler.CoreData.Helpers;
using Timetabler.Data;
using Timetabler.Data.Collections;
using Timetabler.Data.Display;
using Timetabler.Data.Events;
using Timetabler.Data.Interfaces;
using Timetabler.DataLoader;
using Timetabler.Helpers;
using Timetabler.Models;
using Timetabler.PdfExport;
using Timetabler.XmlData;

namespace Timetabler
{
    /// <summary>
    /// The main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        private TimetableDocument _model;
        private TimetableSectionModelDataAdapter _upTrainsAdapter = null;
        private TimetableSectionModelDataAdapter _downTrainsAdapter = null;

        /// <summary>
        /// The open document.
        /// </summary>
        public TimetableDocument Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (_model != null)
                {
                    _model.TimetableDocumentChanged -= DocumentChangedHandler;
                }
                _model = value;
                if (_model != null)
                {
                    _upTrainsAdapter = new TimetableSectionModelDataAdapter(_model.UpTrainsDisplay, dgvUp);
                    _downTrainsAdapter = new TimetableSectionModelDataAdapter(_model.DownTrainsDisplay, dgvDown);
                    _model.TimetableDocumentChanged += DocumentChangedHandler;
                    _model.SignalboxHoursSets.SignalboxHoursSetAdd += SignalboxHoursSetAddedHandler;
                    _model.SignalboxHoursSets.SignalboxHoursSetModified += SignalboxHoursSetModifiedHandler;
                    _model.SignalboxHoursSets.SignalboxHoursSetRemove += SignalboxHoursSetRemoveHandler;
                }
            }
        }

        private bool _documentChanged = false;

        private Dictionary<string, int> _signalboxHoursColumnMap = new Dictionary<string, int>();

        private static ILogger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Default constructor.  Creates an empty document, and initialises the file dialogs' default folders to My Documents.
        /// </summary>
        public MainForm()
        {
            Log.Info("Starting up Timetabler {0}", GetType().Assembly.GetName().Version);            
            InitializeComponent();

            Model = new TimetableDocument();
            UpdateTrainGraphLocationModel();

            ofdDocument.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfdDocument.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofdDocument.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfdLocations.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfdExport.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        /// <summary>
        /// Constructor which takes a filename to open as an initial parameter.
        /// </summary>
        /// <param name="initialFile">The filename to try to open.</param>
        public MainForm(string initialFile) : this()
        {
            OpenFile(initialFile);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: File>Exit");
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: File>Open...");
            if (!CheckIfUnsavedChanges(Resources.MainForm_FileOpen_UnsavedChanges))
            {
                Log.Trace("Action cancelled: unsaved changes warning.");
                return;
            }

            ofdDocument.SetInitialDirectory();
            DialogResult dialogResult = ofdDocument.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                Log.Trace("Action cancelled: OpenFileDialog.ShowDialog() returned {0}", dialogResult);
                return;
            }

            OpenFile(ofdDocument.FileName);
        }

        private void OpenFile(string fn)
        {
            try
            {
                using (FileStream fs = new FileStream(fn, FileMode.Open, FileAccess.Read))
                {
                    Model = Loader.LoadTimetableDocument(fs);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogWithMessageBox(Log, LogLevel.Error, ex, this, Resources.MainForm_FileOpen_Failure, ex.GetType().Name, ex.Message, ofdDocument.FileName);
            }

            UpdateFields();
            UpdateSignalboxHours();
            Model.DownTrainsDisplay.CheckCompulsaryLocationsAreVisible();
            Model.UpTrainsDisplay.CheckCompulsaryLocationsAreVisible();
            UpdateTrainGraphLocationModel();
            trainGraph.Model.SetPropertiesFromDocumentOptions(Model.Options);
            _documentChanged = false;
        }

        private void UpdateTrainGraphLocationModel()
        {
            trainGraph.Model = new TrainGraphModel
            {
                LocationList = Model.LocationList,
                TrainList = Model.TrainList,
                DisplayTrainLabels = Model.Options.DisplayTrainLabelsOnGraphs,
                TooltipFormattingString = Model.Options.FormattingStrings.Tooltip,
                EditTrainMethod = EditTrain,
            };
            trainGraph.Model.SelectedTrainChanged += TrainGraphModelSelectedTrainChanged;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: File>Save...");
            sfdDocument.SetInitialDirectory();
            DialogResult dialogResult = sfdDocument.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                Log.Trace("Action cancelled: SaveFileDialog.ShowDialog() returned {0}", dialogResult);
                return;
            }

            try
            {
                using (FileStream fs = new FileStream(sfdDocument.FileName, FileMode.Create, FileAccess.Write))
                {
                    Saver.Save(Model, fs);
                }
                _documentChanged = false;
            }
            catch (IOException ex)
            {
                bool showSecondMessage = true;
                if ((ex.HResult & 0xffff) == 0x20)
                {
                    MessageBox.Show(this, string.Format(Resources.MainForm_FileSave_SharingViolation, sfdDocument.FileName), Resources.MainForm_FileSave_SharingViolationTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    showSecondMessage = false;
                }
                LogHelper.LogWithMessageBox(Log, LogLevel.Error, ex, this, showSecondMessage, Resources.MainForm_FileSave_Failure, sfdDocument.FileName, ex.GetType().Name, ex.Message);
            }
            catch (Exception ex)
            {
                LogHelper.LogWithMessageBox(Log, LogLevel.Error, ex, this, Resources.MainForm_FileSave_Failure, ex.GetType().Name, ex.Message, sfdDocument.FileName);
            }
        }

        private void exportToPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: File>Export to PDF...");
            sfdExport.SetInitialDirectory();
            DialogResult dialogResult = sfdExport.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                Log.Trace("Action cancelled: SaveFileDialog.ShowDialog() returned {0}", dialogResult);
                return;
            }

            try
            {
                IExporter exporter = new PdfExporter { MainLineWidth = Model.ExportOptions.LineWidth, PassingTrainDashWidth = Model.ExportOptions.FillerDashLineWidth };
                using (FileStream fs = new FileStream(sfdExport.FileName, FileMode.Create, FileAccess.Write))
                {
                    exporter.Export(Model, fs);
                }
                MessageBox.Show(this, Resources.MainForm_Export_Completed);
            }
            catch (IOException ex)
            {
                bool showSecondMessage = true;
                if ((ex.HResult & 0xffff) == 0x20)
                {
                    MessageBox.Show(this, string.Format(Resources.MainForm_FileExport_SharingViolation, sfdExport.FileName), Resources.MainForm_FileExport_SharingViolationTitle,  
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    showSecondMessage = false;
                }
                LogHelper.LogWithMessageBox(Log, LogLevel.Error, ex, this, showSecondMessage, Resources.MainForm_FileExport_Failure, sfdExport.FileName, ex.GetType().Name, ex.Message);
            }
            catch (Exception ex)
            {
                LogHelper.LogWithMessageBox(Log, LogLevel.Error, ex, this, Resources.MainForm_FileExport_Failure, sfdExport.FileName, ex.GetType().Name, ex.Message);
            }
        }

        private TrainEditFormModel GetBaseTrainEditFormModel()
        {
            return new TrainEditFormModel
            {
                ValidLocations = Model.LocationList,
                ValidClasses = Model.TrainClassList,
                ValidTrainNotes = Model.NoteDefinitions.Where(n => n.AppliesToTrains).ToList(),
                ValidTimingPointNotes = Model.NoteDefinitions.Where(n => n.AppliesToTimings).ToList(),
                DocumentOptions = Model.Options,
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab == tabHours)
            {
                Log.Trace("MainForm: btnAdd clicked (signalbox hours mode)");
                AddBoxHours();
            }
            else
            {
                Log.Trace("MainForm: btnAdd clicked (normal mode)");
                AddTrain();
            }
        }

        private void AddTrain()
        {
            TrainEditFormModel formModel = GetBaseTrainEditFormModel();
            formModel.Data = new Train { Id = GeneralHelper.GetNewId(Model.TrainList) };
            TrainEditForm form = new TrainEditForm { Model = formModel };
            DialogResult result = form.ShowDialog();
            Log.Trace("TrainEditForm.ShowDialog() returned {0}", result);
            if (result != DialogResult.OK)
            {
                return;
            }

            if (form.Model != null && form.Model.Data != null)
            {
                Model.TrainList.Add(form.Model.Data);
            }
        }

        private void AddBoxHours()
        {
            SignalboxHoursSetEditFormModel model = new SignalboxHoursSetEditFormModel { InputMode = Model.Options.ClockType, Data = new SignalboxHoursSet() };
            foreach (var box in Model.Signalboxes)
            {
                model.Data.Hours.Add(box.Id, new SignalboxHours { Signalbox = box });
            }
            SignalboxHoursSetEditForm form = new SignalboxHoursSetEditForm { Model = model };
            DialogResult result = form.ShowDialog();
            Log.Trace("SignalboxHoursSetEditForm.ShowDialog() returned {0}", result);
            if (result == DialogResult.OK)
            {
                Model.SignalboxHoursSets.Add(form.Model.Data);
            }
        }
        
        private void DocumentChangedHandler(object sender, EventArgs e)
        {
            _documentChanged = true;
        }

        private void UpdateFields()
        {
            if (Model == null)
            {
                return;
            }
            tbTitle.Text = Model.Title;
            tbSubtitle.Text = Model.Subtitle;
            tbDateDescription.Text = Model.DateDescription;
            tbWrittenBy.Text = Model.WrittenBy;
            tbCheckedBy.Text = Model.CheckedBy;
            tbTimetableVersion.Text = Model.TimetableVersion;
            tbPublishedDate.Text = Model.PublishedDate;
        }

        private void trainClassesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: Edit>Train Classes...");
            var tclef = new TrainClassListEditForm { Model = new TrainClassCollection(Model.TrainClassList) };
            var result = tclef.ShowDialog();
            Log.Trace("TrainClassListEditForm.ShowDialog() returned {0}", result);
            if (result == DialogResult.OK)
            {
                Model.TrainClassList = tclef.Model;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab == tabHours)
            {
                Log.Trace("MainForm Edit button clicked (signalbox hours mode");
                SignalboxHoursSet selectedSignalboxHoursSet = GetSelectedSignalboxHoursSet();
                if (selectedSignalboxHoursSet != null)
                {
                    EditSignalboxHoursSet(selectedSignalboxHoursSet);
                }
                return;
            }
            Log.Trace("MainForm Edit button clicked (normal mode)");
            string trainId = GetSelectedTrainId();
            if (trainId == null)
            {
                return;
            }
            EditTrain(trainId);
        }

        private void EditSignalboxHoursSet(SignalboxHoursSet selectedSignalboxHoursSet)
        {
            SignalboxHoursSetEditForm form = new SignalboxHoursSetEditForm
            {
                Model = new SignalboxHoursSetEditFormModel { Data = selectedSignalboxHoursSet.Copy(), InputMode = Model.Options.ClockType }
            };
            DialogResult result = form.ShowDialog();
            Log.Trace("SignalboxHoursSetEditForm.ShowDialog() returned {0}", result);
            if (result != DialogResult.OK)
            {
                return;
            }

            form.Model.Data.CopyTo(selectedSignalboxHoursSet);
        }

        private SignalboxHoursSet GetSelectedSignalboxHoursSet()
        {
            if (dgvHours.SelectedCells.Count == 0)
            {
                return null;
            }
            string selectedId = dgvHours[_signalboxHoursIdColumn, dgvHours.SelectedCells[0].RowIndex].Value as string;
            foreach (SignalboxHoursSet set in Model.SignalboxHoursSets)
            {
                if (set.Id == selectedId)
                {
                    return set;
                }
            }

            return null;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab == tabHours)
            {
                Log.Trace("MainForm.btnDel clicked (signalbox hours mode)");
                SignalboxHoursSet selectedSignalboxHoursSet = GetSelectedSignalboxHoursSet();
                if (selectedSignalboxHoursSet != null)
                {
                    DeleteSignalboxHoursSet(selectedSignalboxHoursSet);
                }
                return;
            }

            Log.Trace("MainForm.btnDel clicked (normal mode)");
            string trainId = GetSelectedTrainId();
            if (trainId == null)
            {
                return;
            }
            DeleteTrain(trainId);
        }        

        private string GetSelectedTrainId()
        {
            DataGridView selDgv = null;
            TimetableSectionModelDataAdapter adapter = null;
            if (tcMain.SelectedTab == tabGraph)
            {
                return trainGraph.Model.SelectedTrain?.Id;
            }
            if (tcMain.SelectedTab == tabDown)
            {
                selDgv = dgvDown;
                adapter = _downTrainsAdapter;
            }
            else if (tcMain.SelectedTab == tabUp)
            {
                selDgv = dgvUp;
                adapter = _upTrainsAdapter;
            }
            if (selDgv == null || adapter == null)
            {
                return null;
            }
            if (selDgv.SelectedCells.Count == 0)
            {
                return null;
            }
            return adapter.GetTrainIdForViewColumn(selDgv.SelectedCells[0].ColumnIndex);
        }

        private void EditTrain(string trainId)
        {
            Log.Trace("Entering EditTrain(\"{0}\")", trainId);
            if (!Model.TrainList.Any(t => t.Id == trainId))
            {
                Log.Warn("Train {0} does not exist", trainId);
                return;
            }
            Train targetTrain = Model.TrainList.First(t => t.Id == trainId);
            Train copyTrain = targetTrain.Copy();
            copyTrain.Id = GeneralHelper.GetNewId(Model.TrainList);
            TrainEditFormModel formModel = GetBaseTrainEditFormModel();
            formModel.Data = copyTrain;
            TrainEditForm tef = new TrainEditForm { Model = formModel };
            DialogResult result = tef.ShowDialog();
            Log.Trace("TrainEditForm.ShowDialog() returned {0}", result);
            if (result != DialogResult.OK)
            {
                return;
            }

            Model.TrainList.Remove(targetTrain);
            Model.TrainList.Add(tef.Model.Data);
        }

        private void DeleteTrain(string trainId)
        {
            Train targetTrain = Model.TrainList.FirstOrDefault(t => t.Id == trainId);
            if (targetTrain == null)
            {
                return;
            }
            Model.TrainList.Remove(targetTrain);
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEditButtonEnabled();
        }

        private void SetEditButtonEnabled()
        {
            btnDel.Enabled = btnEdit.Enabled = 
                (tcMain.SelectedTab == tabGraph && trainGraph.Model.SelectedTrain != null) ||
                (tcMain.SelectedTab == tabDown && dgvDown.SelectedCells.Count > 0 && _downTrainsAdapter.IsColumnTrainColumn(dgvDown.SelectedCells[0].ColumnIndex)) ||
                (tcMain.SelectedTab == tabUp && dgvUp.SelectedCells.Count > 0 && _upTrainsAdapter.IsColumnTrainColumn(dgvUp.SelectedCells[0].ColumnIndex)) ||
                (tcMain.SelectedTab == tabHours && dgvHours.SelectedCells.Count > 0);
        }

        private void dgvDown_SelectionChanged(object sender, EventArgs e)
        {
            SetEditButtonEnabled();
        }

        private void dgvUp_SelectionChanged(object sender, EventArgs e)
        {
            SetEditButtonEnabled();
        }

        private void dgvDown_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Log.Trace("Down grid: cell col {0}, row {1} double-clicked", e.ColumnIndex, e.RowIndex);
            CellDoubleClicked(dgvDown, e);
        }    

        private void dgvUp_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Log.Trace("Up grid: cell col {0}, row {1} double-clicked", e.ColumnIndex, e.RowIndex);
            CellDoubleClicked(dgvUp, e);
        }

        private void CellDoubleClicked(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            TimetableSectionModelDataAdapter adapter = null;
            if (dgv == dgvDown)
            {
                Log.Trace("Down train double-clicked: grid coords {0}, {1}", e.ColumnIndex, e.RowIndex);
                adapter = _downTrainsAdapter;
            }
            else if (dgv == dgvUp)
            {
                Log.Trace("Up train double-clicked: grid coords {0}, {1}", e.ColumnIndex, e.RowIndex);
                adapter = _upTrainsAdapter;
            }
            if (adapter == null)
            {
                Log.Warn("Adapter not set!");
                return;
            }
            if (!adapter.IsColumnTrainColumn(e.ColumnIndex))
            {
                Log.Trace("Column {0} is not a train column.", e.ColumnIndex);
                return;
            }
            string trainId = adapter.GetTrainIdForViewColumn(e.ColumnIndex);
            Log.Trace("Train ID for selected column is {0}", trainId);
            if (string.IsNullOrWhiteSpace(trainId))
            {
                return;
            }

            EditTrain(trainId);
        }

        private void LocationEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: Edit>Locations>Edit...");
            EditLocations();
        }

        private void EditLocations()
        {
            LocationListEditForm form = new LocationListEditForm { Model = new LocationCollection(Model.LocationList) };
            DialogResult result = form.ShowDialog();
            Log.Trace("LocationListEditForm.ShowDialog() returned {0}", result);
            if (result == DialogResult.OK)
            {
                Model.LocationList = form.Model;
                Model.DownTrainsDisplay.CheckCompulsaryLocationsAreVisible();
                Model.UpTrainsDisplay.CheckCompulsaryLocationsAreVisible();
                trainGraph.Model.LocationList = form.Model;
                trainGraph.Invalidate();
            }
            Model.ResolveAll();
        }

        private void LocationSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu>Locations>Save...");
            sfdLocations.SetInitialDirectory();
            DialogResult result = sfdLocations.ShowDialog();
            Log.Trace("(SaveFileDialog)sfdLocations.ShowDialog() returned {0}", result);
            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LocationTemplateModel));
                using (FileStream fs = new FileStream(sfdLocations.FileName, FileMode.Create, FileAccess.Write))
                {
                    Saver.Save(Model.LocationList, fs);
                }
                MessageBox.Show(this, Resources.MainForm_LocationsTemplateFileSave_Success, Resources.MainForm_LocationsTemplateFileSave_Title, MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                LogHelper.LogWithMessageBox(Log, LogLevel.Error, ex, this, Resources.MainForm_FileSave_Failure, ex.GetType().Name, ex.Message, sfdLocations.FileName);
            }
        }

        private void openTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: Edit>Locations>Open...");
            if (Model.TrainList != null && Model.TrainList.Count > 0)
            {
                if (MessageBox.Show(this, Resources.MainForm_LocationsTemplateFileLoad_Warning, Resources.MainForm_LocationsTemplateFileLoad_WarningTitle, MessageBoxButtons.YesNo) 
                    != DialogResult.Yes)
                {
                    return;
                }
            }

            ofdLocations.SetInitialDirectory();
            DialogResult result = ofdLocations.ShowDialog();
            Log.Trace("(OpenFileDialog)odfLocations.ShowDialog() returned {0}", result);
            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                LocationCollection locations;
                using (FileStream fs = new FileStream(ofdLocations.FileName, FileMode.Open, FileAccess.Read))
                {
                    locations = Loader.LoadLocationTemplate(fs);
                }
                if (locations == null)
                {
                    Log.Warn("Null object loaded in location template loader.");
                    MessageBox.Show(this, Resources.MainForm_LocationsTemplateFileLoad_NullObject, Resources.MainForm_LocationsTemplateFileLoad_NullObjectTitle);
                    return;
                }

                Model.LocationList = locations;
                Model.DownTrainsDisplay.LocationMap = Model.LocationList;
                Model.DownTrainsDisplay.CheckCompulsaryLocationsAreVisible();
                Model.UpTrainsDisplay.LocationMap = Model.LocationList;
                Model.UpTrainsDisplay.CheckCompulsaryLocationsAreVisible();
                UpdateTrainGraphLocationModel();
            }
            catch (Exception ex)
            {
                LogHelper.LogWithMessageBox(Log, LogLevel.Error, ex, this, Resources.MainForm_LocationsTemplateFileLoad_Exception, ofdLocations.FileName, ex.GetType().Name, ex.Message);
            }
        }

        private void editOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: Edit>Document Options...");
            if (Model?.Options == null)
            {
                Log.Warn("Not displaying DocumentOptionsEditForm as model options object is null.");
                return;
            }
            DocumentOptionsEditForm doef = new DocumentOptionsEditForm { Model = Model.Options.Copy() };
            DialogResult result = doef.ShowDialog();
            Log.Trace("DocumentOptionsEditForm.ShowDialog() returned {0}", result);

            if (result != DialogResult.OK)
            {
                return;
            }
            doef.Model.CopyTo(Model.Options);
            trainGraph.Model.SetPropertiesFromDocumentOptions(Model.Options);
            trainGraph.Invalidate();
            Model.RefreshTrainDisplayFormatting();
            UpdateSignalboxHours();
        }

        private void EditExportOptions()
        {
            if (Model?.ExportOptions == null)
            {
                Log.Warn("Not displaying DocumentExportOptionsEditForm as model export options object is null.");
                return;
            }
            DocumentExportOptionsEditForm form = new DocumentExportOptionsEditForm { Model = Model.ExportOptions.Copy() };
            DialogResult result = form.ShowDialog();
            Log.Trace("DocumentExportOptionsEditForm.ShowDialog() returned {0}", result);
            if (result != DialogResult.OK)
            {
                return;
            }
            Model.ExportOptions = form.Model;
        }

        private bool CheckIfUnsavedChanges(string text)
        {
            if (_documentChanged)
            {
                return (MessageBox.Show(this, text, Resources.MainForm_FormClosing_UnsavedChangesTitle, MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
            return true;
        }

        private void NewEmptyDocumentEventHandler(object sender, EventArgs e)
        {
            Log.Trace("Menu: \"File>New>New Empty File\"");
            if (!CheckIfUnsavedChanges(Resources.MainForm_FileNew_UnsavedChanges))
            {
                Log.Trace("Action cancelled - unsaved changes warning.");
                return;
            }

            SetUpNewDocument(null);
        }

        private void SetUpNewDocument(DocumentTemplate template)
        {
            TimetableDocument newDocument = new TimetableDocument();
            if (template != null)
            {
                newDocument.LocationList = template.Locations;
                newDocument.Options = template.DocumentOptions;
                newDocument.ExportOptions = template.ExportOptions;
                newDocument.NoteDefinitions = template.NoteDefinitions;
                newDocument.TrainClassList = template.TrainClasses;
                newDocument.Signalboxes = template.Signalboxes;
            }
            Model = newDocument;
            UpdateTrainGraphLocationModel();
            UpdateFields();
            Model.UpdateTrainDisplays();
            _documentChanged = false;
        }

        private void editFootnotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: Edit>Footnotes");
            NoteListEditForm nef = new NoteListEditForm { Model = new NoteListEditFormModel { Data = Model.NoteDefinitions.ToDictionary(n => n.Id, n => n.Copy()) } };
            DialogResult result = nef.ShowDialog();
            Log.Trace("NoteListEditForm.ShowDialog() returned {0}", result);
            if (result != DialogResult.OK)
            {
                return;
            }
            Model.NoteDefinitions.CopyFrom(nef.Model.Data);
            if (nef.Model.ExistingNoteChanged)
            {
                Model.RefreshTrainDisplayFormatting();
            }
        }
        
        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            if (Model != null)
            {
                Model.Title = tbTitle.Text.Trim();
            }
        }

        private void tbSubtitle_TextChanged(object sender, EventArgs e)
        {
            if (Model != null)
            {
                Model.Subtitle = tbSubtitle.Text.Trim();
            }
        }

        private void tbDateDescription_TextChanged(object sender, EventArgs e)
        {
            if (Model != null)
            {
                Model.DateDescription = tbDateDescription.Text.Trim();
            }
        }

        private void tbWrittenBy_TextChanged(object sender, EventArgs e)
        {
            if (Model != null)
            {
                Model.WrittenBy = tbWrittenBy.Text.Trim();
            }
        }

        private void tbCheckedBy_TextChanged(object sender, EventArgs e)
        {
            if (Model != null)
            {
                Model.CheckedBy = tbCheckedBy.Text.Trim();
            }
        }

        private void tbTimetableVersion_TextChanged(object sender, EventArgs e)
        {
            if (Model != null)
            {
                Model.TimetableVersion = tbTimetableVersion.Text.Trim();
            }
        }

        private void tbPublishedDate_TextChanged(object sender, EventArgs e)
        {
            if (Model != null)
            {
                Model.PublishedDate = tbPublishedDate.Text.Trim();
            }
        }

        private void exportOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: Edit>Export Options...");
            EditExportOptions();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CheckIfUnsavedChanges(Resources.MainForm_FormClosing_UnsavedChanges))
            {
                e.Cancel = true;
            }
        }

        private void saveAsTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: File>Template>Save As Template...");
            sfdTemplate.SetInitialDirectory();
            DialogResult dialogResult = sfdTemplate.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                Log.Trace("Action cancelled: SaveFileDialog.ShowDialog() returned {0}", dialogResult);
                return;
            }

            DocumentTemplate template = new DocumentTemplate
            {
                DocumentOptions = Model.Options,
                ExportOptions = Model.ExportOptions,
                Locations = Model.LocationList,
                NoteDefinitions = Model.NoteDefinitions,
                TrainClasses = Model.TrainClassList,
                Signalboxes = Model.Signalboxes,
            };

            try
            {
                using (FileStream fs = new FileStream(sfdTemplate.FileName, FileMode.Create, FileAccess.Write))
                {
                    Saver.Save(template, fs);
                }
            }
            catch (IOException ex)
            {
                bool showSecondMessage = true;
                if ((ex.HResult & 0xffff) == 0x20)
                {
                    MessageBox.Show(this, string.Format(Resources.MainForm_TemplateFileSave_SharingViolation, sfdTemplate.FileName), Resources.MainForm_FileExport_SharingViolationTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    showSecondMessage = false;
                }
                LogHelper.LogWithMessageBox(Log, LogLevel.Error, ex, this, showSecondMessage, Resources.MainForm_FileSave_Failure, ex.GetType().Name, ex.Message, sfdTemplate.FileName);
            }
            catch (Exception ex)
            {
                LogHelper.LogWithMessageBox(Log, LogLevel.Error, ex, this, Resources.MainForm_FileSave_Failure, ex.GetType().Name, ex.Message, sfdTemplate.FileName);
            }
        }

        private void newFromTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: \"File>New>New From Template...\"");
            if (!CheckIfUnsavedChanges(Resources.MainForm_FileNew_UnsavedChanges))
            {
                Log.Trace("Action cancelled: unsaved changes warning.");
                return;
            }

            ofdTemplate.SetInitialDirectory();
            DialogResult dialogResult = ofdTemplate.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                Log.Trace("Action cancelled: OpenFileDialog.ShowDialog() returned {0}", dialogResult);
                return;
            }

            try
            {
                DocumentTemplate template;
                using (FileStream fs = new FileStream(ofdTemplate.FileName, FileMode.Open, FileAccess.Read))
                {
                    template = Loader.LoadDocumentTemplate(fs);
                }
                if (template == null)
                {
                    Log.Warn("Null object loaded in template loader.");
                    MessageBox.Show(this, Resources.MainForm_TemplateFileLoad_NullObject, Resources.MainForm_TemplateFileLoad_NullObjectTitle);
                    return;
                }

                SetUpNewDocument(template);
            }
            catch (Exception ex)
            {
                LogHelper.LogWithMessageBox(Log, LogLevel.Error, ex, this, Resources.MainForm_TemplateFileLoad_Exception, ofdTemplate.FileName, ex.GetType().Name, ex.Message);
            }
        }

        private void signalboxesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: Edit>Signalboxes...");
            SignalboxListEditForm form = new SignalboxListEditForm { Model = Model.Signalboxes.Copy() };
            DialogResult result = form.ShowDialog();
            Log.Trace("SignalboxListEditForm.ShowDialog() returned {0}", result);
            if (result != DialogResult.OK)
            {
                return;
            }
            Model.Signalboxes = form.Model;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Trace("Menu: Help>About");
            new AboutBox().ShowDialog();
        }

        private void SignalboxHoursSetAddedHandler(object sender, SignalboxHoursSetEventArgs e)
        {
            if (e.HoursSet != null)
            {
                AddSignalboxHoursToView(e.HoursSet);
            }
        }

        private const int _signalboxHoursIdColumn = 0;
        private const int _signalboxHoursCategoryColumn = 1;

        private void AddSignalboxHoursToView(SignalboxHoursSet hoursSet)
        {
            foreach (var box in hoursSet.Hours.Values.Select(b => b.Signalbox))
            {
                if (!_signalboxHoursColumnMap.ContainsKey(box.Id))
                {
                    int colIdx = dgvHours.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = box.Code,
                        DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
                    });
                    _signalboxHoursColumnMap.Add(box.Id, colIdx);
                }
            }
            int rowIdx = dgvHours.Rows.Add(dgvHours.RowTemplate.Clone());
            dgvHours[_signalboxHoursIdColumn, rowIdx].Value = hoursSet.Id;
            dgvHours[_signalboxHoursCategoryColumn, rowIdx].Value = hoursSet.Category;
            foreach (var hours in hoursSet.Hours.Values)
            {
                if (hours.StartTime != null && hours.EndTime != null)
                {
                    dgvHours[_signalboxHoursColumnMap[hours.Signalbox.Id], rowIdx].Value = hours.ToString(Model.Options.ClockType);
                }
            }
        }

        private void UpdateSignalboxHours()
        {
            dgvHours.Rows.Clear();
            foreach (SignalboxHoursSet set in Model.SignalboxHoursSets)
            {
                AddSignalboxHoursToView(set);
            }
        }

        private void SignalboxHoursSetModifiedHandler(object sender, SignalboxHoursSetEventArgs eventArgs)
        {
            foreach (var r in dgvHours.Rows)
            {
                DataGridViewRow row = r as DataGridViewRow;
                if (row == null || row.Cells[_signalboxHoursIdColumn].Value as string != eventArgs.HoursSet.Id)
                {
                    continue;
                }

                row.Cells[_signalboxHoursCategoryColumn].Value = eventArgs.HoursSet.Category;
                foreach (var hours in eventArgs.HoursSet.Hours.Values)
                {
                    if (hours.StartTime != null && hours.EndTime != null)
                    {
                        row.Cells[_signalboxHoursColumnMap[hours.Signalbox.Id]].Value = hours.ToString(Model.Options.ClockType);
                    }
                }
            }
        }

        private void DeleteSignalboxHoursSet(SignalboxHoursSet selectedSignalboxHoursSet)
        {
            Model.SignalboxHoursSets.Remove(selectedSignalboxHoursSet);
        }

        private void SignalboxHoursSetRemoveHandler(object sender, SignalboxHoursSetEventArgs eventArgs)
        {
            for (int i = 0; i < dgvHours.Rows.Count; ++i)
            {
                if (dgvHours[_signalboxHoursIdColumn, i].Value as string == eventArgs.HoursSet.Id)
                {
                    dgvHours.Rows.RemoveAt(i);
                    return;
                }
            }
        }

        private void supportSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SupportForm().ShowDialog();
        }

        private void TrainGraphModelSelectedTrainChanged(object sender, TrainEventArgs e)
        {
            SetEditButtonEnabled();
        }
    }
}
