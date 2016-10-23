﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ScintillaNET
{
    public partial class CodeEditor : Scintilla
    {
        #region FIELDS

        public bool EnableCodeHints = true;
        private static int HighlightIndicatorIndex = 8;
        public static int DebugIndicatorIndex = 9;
        private List<int[]>[] IndicatorRanges;
        private static string HiddenLines = $"{(char)177}";
        private string filename;
        private FileSystemWatcher FileWatcher;
        private bool watchChanges = true;

        #endregion

        #region PROPERTIES

        public new int FirstVisibleLine
        {
            get
            {
                int line = base.FirstVisibleLine;
                for (int i = 0; i < line && i < Lines.Count; i++)
                {
                    if (!Lines[i].Visible)
                        line++;
                }
                return line;
            }
        }
        public int LastVisibleLine
        {
            get
            {
                int i, line;
                for (i = FirstVisibleLine, line = i + LinesOnScreen; i < line && i < Lines.Count; i++)
                {
                    if (!Lines[i].Visible)
                        line++;
                }
                return line;
            }
        }
        public string Filename
        {
            get { return filename; }
            set
            {
                // stop watching
                if (value == null)
                {
                    if (FileWatcher != null)
                        FileWatcher.EnableRaisingEvents = false;
                    return;
                }

                // check input
                if (!File.Exists(value))
                    throw new ArgumentException("The file does not exist.");

                // get the path and the name of the file
                var path = Path.GetDirectoryName(value);
                var name = Path.GetFileName(value);
                filename = value;

                // create file watcher
                if (FileWatcher == null)
                {
                    FileWatcher = new FileSystemWatcher();
                    FileWatcher.Changed += new FileSystemEventHandler(HandleFileEvent);
                    FileWatcher.Renamed += new RenamedEventHandler(HandleFileEvent);
                    FileWatcher.NotifyFilter = NotifyFilters.LastAccess
                                             | NotifyFilters.LastWrite
                                             | NotifyFilters.FileName
                                             | NotifyFilters.DirectoryName;
                }

                FileWatcher.Path = path;
                FileWatcher.Filter = name;
                
                // begin watching.
                FileWatcher.EnableRaisingEvents = true;
            }
        }

        #endregion

        #region CONSTRUCTION

        /// <summary>
        /// Instantiate and initialize ScintillaNET based code editor for ProtoFX.
        /// </summary>
        /// <param name="text">[OPTIONAL] Initialize code editor with text.</param>
        public CodeEditor(string keywordsXml, string text = null)
        {
            InitializeFindAndReplace();
            InitializeHighlighting(keywordsXml);
            InitializeSelection();
            InitializeAutoC();
            InitializeCodeFolding();
            InitializeLayout();
            InitializeEvents();
            
            // insert text
            Text = text ?? string.Empty;
            UpdateLineNumbers();
            UpdateCodeFolding(0, Lines.Count);
        }

        /// <summary>
        /// Initialize Scintilla code folding.
        /// </summary>
        private void InitializeCodeFolding()
        {
            SetProperty("fold", "1");
            SetProperty("fold.compact", "1");

            Margins[2].Type = MarginType.Symbol;
            Margins[2].Mask = Marker.MaskFolders;
            Margins[2].Sensitive = true;
            Margins[2].Width = 20;

            CallTipSetForeHlt(Theme.ForeColor);
            
            SetFoldMarginColor(true, Theme.Workspace);
            SetFoldMarginHighlightColor(true, Theme.Workspace);
            Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
            Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;

            for (int i = Marker.FolderEnd; i <= Marker.FolderOpen; i++)
            {
                Markers[i].SetForeColor(Theme.Workspace);
                Markers[i].SetBackColor(Theme.ForeColor);
            }

            AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;
        }

        /// <summary>
        /// Initialize Scintilla editor layout.
        /// </summary>
        private void InitializeLayout()
        {
            // automatically adjust horizontal scroll bar
            ScrollWidthTracking = true;
            ScrollWidth = 5;

            // use tabs instead of spaces
            IndentWidth = 4;
            UseTabs = true;

            // UI style
            BorderStyle = BorderStyle.None;
            Dock = DockStyle.Fill;
            Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Location = new Point(0, 0);
            Margin = new Padding(0);
            TabIndex = 0;
        }

        #endregion

        #region LINE NUMBERS

        /// <summary>
        /// Update line number of the text editor.
        /// </summary>
        private void UpdateLineNumbers()
        {
            // UPDATE LINE NUMBERS
            int nLines = Lines.Count.ToString().Length;
            var width = TextRenderer.MeasureText(new string('9', nLines), Font).Width;
            if (Margins[0].Width != width)
                Margins[0].Width = width;
        }

        #endregion

        #region WATCH FOR CHANGES

        /// <summary>
        /// Pause watching for file changes.
        /// </summary>
        public void PauseFileWatch()
        {
            if (FileWatcher != null)
                FileWatcher.EnableRaisingEvents = false;
        }

        /// <summary>
        /// Resume watching for file changes.
        /// </summary>
        public void ResumeFileWatch()
        {
            if (FileWatcher != null)
                FileWatcher.EnableRaisingEvents = true;
        }
        
        /// <summary>
        /// Pause watching for text changes.
        /// </summary>
        public void PauseWatchChanges()
        {
            watchChanges = false;
        }
        
        /// <summary>
        /// Resume watching for text changes.
        /// </summary>
        public void ResumeWatchChanges()
        {
            watchChanges = true;
        }

        #endregion
    }
}
