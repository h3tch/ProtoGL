﻿namespace App
{
    partial class App
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.splitRenderCoding = new System.Windows.Forms.SplitContainer();
            this.glControl = new OpenTK.GraphicControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCode = new System.Windows.Forms.TabPage();
            this.splitCodeError = new System.Windows.Forms.SplitContainer();
            this.tabCodeTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.tabSource = new System.Windows.Forms.TabControl();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolBtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolBtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolBtnRun = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDbg = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnPick = new System.Windows.Forms.ToolStripButton();
            this.tabOutput = new System.Windows.Forms.TabControl();
            this.tabCompile = new System.Windows.Forms.TabPage();
            this.codeError = new System.Windows.Forms.RichTextBox();
            this.tabDebugger = new System.Windows.Forms.TabPage();
            this.splitDebug = new System.Windows.Forms.SplitContainer();
            this.debugListView = new System.Windows.Forms.ListView();
            this.debugProperty = new System.Windows.Forms.PropertyGrid();
            this.tabResources = new System.Windows.Forms.TabPage();
            this.tabData = new System.Windows.Forms.TabControl();
            this.tabDataImg = new System.Windows.Forms.TabPage();
            this.tableLayoutImages = new System.Windows.Forms.TableLayoutPanel();
            this.numImgLayer = new System.Windows.Forms.NumericUpDown();
            this.comboImg = new System.Windows.Forms.ComboBox();
            this.panelImg = new System.Windows.Forms.Panel();
            this.pictureImg = new System.Windows.Forms.PictureBox();
            this.numImgLevel = new System.Windows.Forms.NumericUpDown();
            this.tabDataBuf = new System.Windows.Forms.TabPage();
            this.tableLayoutBufferDef = new System.Windows.Forms.TableLayoutPanel();
            this.tableBuf = new System.Windows.Forms.DataGridView();
            this.tableLayoutBuffers = new System.Windows.Forms.TableLayoutPanel();
            this.comboBuf = new System.Windows.Forms.ComboBox();
            this.comboBufType = new System.Windows.Forms.ComboBox();
            this.numBufDim = new System.Windows.Forms.NumericUpDown();
            this.tabProperties = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.comboProp = new System.Windows.Forms.ComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitRenderCoding)).BeginInit();
            this.splitRenderCoding.Panel1.SuspendLayout();
            this.splitRenderCoding.Panel2.SuspendLayout();
            this.splitRenderCoding.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCodeError)).BeginInit();
            this.splitCodeError.Panel1.SuspendLayout();
            this.splitCodeError.Panel2.SuspendLayout();
            this.splitCodeError.SuspendLayout();
            this.tabCodeTableLayout.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.RightToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tabCompile.SuspendLayout();
            this.tabDebugger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDebug)).BeginInit();
            this.splitDebug.Panel1.SuspendLayout();
            this.splitDebug.Panel2.SuspendLayout();
            this.splitDebug.SuspendLayout();
            this.tabResources.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tabDataImg.SuspendLayout();
            this.tableLayoutImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numImgLayer)).BeginInit();
            this.panelImg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numImgLevel)).BeginInit();
            this.tabDataBuf.SuspendLayout();
            this.tableLayoutBufferDef.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableBuf)).BeginInit();
            this.tableLayoutBuffers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBufDim)).BeginInit();
            this.tabProperties.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitRenderCoding
            // 
            this.splitRenderCoding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRenderCoding.Location = new System.Drawing.Point(0, 0);
            this.splitRenderCoding.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitRenderCoding.Name = "splitRenderCoding";
            // 
            // splitRenderCoding.Panel1
            // 
            this.splitRenderCoding.Panel1.Controls.Add(this.glControl);
            // 
            // splitRenderCoding.Panel2
            // 
            this.splitRenderCoding.Panel2.Controls.Add(this.tabControl);
            this.splitRenderCoding.Size = new System.Drawing.Size(2014, 1132);
            this.splitRenderCoding.SplitterDistance = 1045;
            this.splitRenderCoding.SplitterWidth = 6;
            this.splitRenderCoding.TabIndex = 0;
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 0);
            this.glControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(1045, 1132);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseUp);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCode);
            this.tabControl.Controls.Add(this.tabResources);
            this.tabControl.Controls.Add(this.tabProperties);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(963, 1132);
            this.tabControl.TabIndex = 1;
            // 
            // tabCode
            // 
            this.tabCode.Controls.Add(this.splitCodeError);
            this.tabCode.Location = new System.Drawing.Point(4, 31);
            this.tabCode.Name = "tabCode";
            this.tabCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabCode.Size = new System.Drawing.Size(955, 1097);
            this.tabCode.TabIndex = 0;
            this.tabCode.Text = "Code";
            this.tabCode.UseVisualStyleBackColor = true;
            // 
            // splitCodeError
            // 
            this.splitCodeError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCodeError.Location = new System.Drawing.Point(3, 3);
            this.splitCodeError.Margin = new System.Windows.Forms.Padding(0);
            this.splitCodeError.Name = "splitCodeError";
            this.splitCodeError.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCodeError.Panel1
            // 
            this.splitCodeError.Panel1.Controls.Add(this.tabCodeTableLayout);
            this.splitCodeError.Panel1MinSize = 150;
            // 
            // splitCodeError.Panel2
            // 
            this.splitCodeError.Panel2.Controls.Add(this.tabOutput);
            this.splitCodeError.Panel2MinSize = 75;
            this.splitCodeError.Size = new System.Drawing.Size(949, 1091);
            this.splitCodeError.SplitterDistance = 821;
            this.splitCodeError.SplitterWidth = 6;
            this.splitCodeError.TabIndex = 0;
            // 
            // tabCodeTableLayout
            // 
            this.tabCodeTableLayout.ColumnCount = 1;
            this.tabCodeTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabCodeTableLayout.Controls.Add(this.toolStripContainer, 0, 0);
            this.tabCodeTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCodeTableLayout.Location = new System.Drawing.Point(0, 0);
            this.tabCodeTableLayout.Name = "tabCodeTableLayout";
            this.tabCodeTableLayout.RowCount = 1;
            this.tabCodeTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabCodeTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 821F));
            this.tabCodeTableLayout.Size = new System.Drawing.Size(949, 821);
            this.tabCodeTableLayout.TabIndex = 1;
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.tabSource);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(906, 790);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(3, 3);
            this.toolStripContainer.Name = "toolStripContainer";
            // 
            // toolStripContainer.RightToolStripPanel
            // 
            this.toolStripContainer.RightToolStripPanel.Controls.Add(this.toolStrip);
            this.toolStripContainer.Size = new System.Drawing.Size(943, 815);
            this.toolStripContainer.TabIndex = 1;
            this.toolStripContainer.Text = "toolStripContainer2";
            // 
            // tabSource
            // 
            this.tabSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSource.Location = new System.Drawing.Point(0, 0);
            this.tabSource.Margin = new System.Windows.Forms.Padding(0);
            this.tabSource.Name = "tabSource";
            this.tabSource.SelectedIndex = 0;
            this.tabSource.Size = new System.Drawing.Size(906, 790);
            this.tabSource.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnClose,
            this.toolBtnNew,
            this.toolBtnOpen,
            this.toolBtnSave,
            this.toolBtnSaveAll,
            this.toolBtnSaveAs,
            this.toolStripSeparator3,
            this.toolBtnRun,
            this.toolBtnDbg,
            this.toolStripSeparator2,
            this.toolBtnPick});
            this.toolStrip.Location = new System.Drawing.Point(0, 3);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(37, 374);
            this.toolStrip.TabIndex = 0;
            // 
            // toolBtnClose
            // 
            this.toolBtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnClose.Image = global::App.Properties.Resources.ImgClose;
            this.toolBtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnClose.Name = "toolBtnClose";
            this.toolBtnClose.Size = new System.Drawing.Size(35, 36);
            this.toolBtnClose.Text = "Close Tab";
            this.toolBtnClose.Click += new System.EventHandler(this.toolBtnClose_Click);
            // 
            // toolBtnNew
            // 
            this.toolBtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnNew.Image = global::App.Properties.Resources.ImgNew;
            this.toolBtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnNew.Name = "toolBtnNew";
            this.toolBtnNew.Size = new System.Drawing.Size(35, 36);
            this.toolBtnNew.Text = "New";
            this.toolBtnNew.Click += new System.EventHandler(this.toolBtnNew_Click);
            // 
            // toolBtnOpen
            // 
            this.toolBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnOpen.Image = global::App.Properties.Resources.ImgOpen;
            this.toolBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnOpen.Name = "toolBtnOpen";
            this.toolBtnOpen.Size = new System.Drawing.Size(35, 36);
            this.toolBtnOpen.Text = "Open (Ctrl + O)";
            this.toolBtnOpen.Click += new System.EventHandler(this.toolBtnOpen_Click);
            // 
            // toolBtnSave
            // 
            this.toolBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSave.Image = global::App.Properties.Resources.ImgSave;
            this.toolBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSave.Name = "toolBtnSave";
            this.toolBtnSave.Size = new System.Drawing.Size(35, 36);
            this.toolBtnSave.Text = "Save (Ctrl + S)";
            this.toolBtnSave.Click += new System.EventHandler(this.toolBtnSave_Click);
            // 
            // toolBtnSaveAll
            // 
            this.toolBtnSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSaveAll.Image = global::App.Properties.Resources.ImgSaveAll;
            this.toolBtnSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSaveAll.Name = "toolBtnSaveAll";
            this.toolBtnSaveAll.Size = new System.Drawing.Size(35, 36);
            this.toolBtnSaveAll.Text = "Save all (Ctrl + Shift + S)";
            this.toolBtnSaveAll.Click += new System.EventHandler(this.toolBtnSaveAll_Click);
            // 
            // toolBtnSaveAs
            // 
            this.toolBtnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSaveAs.Image = global::App.Properties.Resources.ImgSaveAs;
            this.toolBtnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSaveAs.Name = "toolBtnSaveAs";
            this.toolBtnSaveAs.Size = new System.Drawing.Size(35, 36);
            this.toolBtnSaveAs.Text = "Save as (Alt + S)";
            this.toolBtnSaveAs.Click += new System.EventHandler(this.toolBtnSaveAs_Click);
            // 
            // toolBtnRun
            // 
            this.toolBtnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnRun.Image = global::App.Properties.Resources.ImgRun;
            this.toolBtnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnRun.Name = "toolBtnRun";
            this.toolBtnRun.Size = new System.Drawing.Size(35, 36);
            this.toolBtnRun.Text = "Run (F5)";
            this.toolBtnRun.Click += new System.EventHandler(this.toolBtnRunDebug_Click);
            // 
            // toolBtnDbg
            // 
            this.toolBtnDbg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDbg.Image = global::App.Properties.Resources.ImgDbg;
            this.toolBtnDbg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDbg.Name = "toolBtnDbg";
            this.toolBtnDbg.Size = new System.Drawing.Size(35, 36);
            this.toolBtnDbg.Text = "Debug (F6)";
            this.toolBtnDbg.Click += new System.EventHandler(this.toolBtnRunDebug_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(35, 6);
            // 
            // toolBtnPick
            // 
            this.toolBtnPick.CheckOnClick = true;
            this.toolBtnPick.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnPick.Image = global::App.Properties.Resources.ImgPick;
            this.toolBtnPick.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnPick.Name = "toolBtnPick";
            this.toolBtnPick.Size = new System.Drawing.Size(35, 36);
            this.toolBtnPick.Text = "Debug Fragment";
            this.toolBtnPick.CheckedChanged += new System.EventHandler(this.toolBtnPick_CheckedChanged);
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.tabCompile);
            this.tabOutput.Controls.Add(this.tabDebugger);
            this.tabOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOutput.Location = new System.Drawing.Point(0, 0);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.SelectedIndex = 0;
            this.tabOutput.Size = new System.Drawing.Size(949, 264);
            this.tabOutput.TabIndex = 1;
            // 
            // tabCompile
            // 
            this.tabCompile.Controls.Add(this.codeError);
            this.tabCompile.Location = new System.Drawing.Point(4, 31);
            this.tabCompile.Name = "tabCompile";
            this.tabCompile.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompile.Size = new System.Drawing.Size(941, 229);
            this.tabCompile.TabIndex = 0;
            this.tabCompile.Text = "Compiler Output";
            this.tabCompile.UseVisualStyleBackColor = true;
            // 
            // codeError
            // 
            this.codeError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codeError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeError.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeError.ForeColor = System.Drawing.SystemColors.WindowText;
            this.codeError.Location = new System.Drawing.Point(3, 3);
            this.codeError.Margin = new System.Windows.Forms.Padding(0);
            this.codeError.Name = "codeError";
            this.codeError.Size = new System.Drawing.Size(935, 223);
            this.codeError.TabIndex = 0;
            this.codeError.Text = "";
            this.codeError.WordWrap = false;
            // 
            // tabDebugger
            // 
            this.tabDebugger.Controls.Add(this.splitDebug);
            this.tabDebugger.Location = new System.Drawing.Point(4, 31);
            this.tabDebugger.Name = "tabDebugger";
            this.tabDebugger.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebugger.Size = new System.Drawing.Size(941, 229);
            this.tabDebugger.TabIndex = 1;
            this.tabDebugger.Text = "Debug Variables";
            this.tabDebugger.UseVisualStyleBackColor = true;
            // 
            // splitDebug
            // 
            this.splitDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDebug.Location = new System.Drawing.Point(3, 3);
            this.splitDebug.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitDebug.Name = "splitDebug";
            // 
            // splitDebug.Panel1
            // 
            this.splitDebug.Panel1.Controls.Add(this.debugListView);
            // 
            // splitDebug.Panel2
            // 
            this.splitDebug.Panel2.Controls.Add(this.debugProperty);
            this.splitDebug.Size = new System.Drawing.Size(935, 223);
            this.splitDebug.SplitterDistance = 529;
            this.splitDebug.SplitterWidth = 6;
            this.splitDebug.TabIndex = 0;
            // 
            // debugListView
            // 
            this.debugListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.debugListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugListView.Location = new System.Drawing.Point(0, 0);
            this.debugListView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.debugListView.Name = "debugListView";
            this.debugListView.Size = new System.Drawing.Size(529, 223);
            this.debugListView.TabIndex = 0;
            this.debugListView.UseCompatibleStateImageBehavior = false;
            // 
            // debugProperty
            // 
            this.debugProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugProperty.HelpVisible = false;
            this.debugProperty.Location = new System.Drawing.Point(0, 0);
            this.debugProperty.Name = "debugProperty";
            this.debugProperty.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.debugProperty.Size = new System.Drawing.Size(400, 223);
            this.debugProperty.TabIndex = 1;
            this.debugProperty.ToolbarVisible = false;
            this.debugProperty.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // tabResources
            // 
            this.tabResources.Controls.Add(this.tabData);
            this.tabResources.Location = new System.Drawing.Point(4, 31);
            this.tabResources.Name = "tabResources";
            this.tabResources.Padding = new System.Windows.Forms.Padding(3);
            this.tabResources.Size = new System.Drawing.Size(955, 1097);
            this.tabResources.TabIndex = 1;
            this.tabResources.Text = "Resources";
            this.tabResources.UseVisualStyleBackColor = true;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.tabDataImg);
            this.tabData.Controls.Add(this.tabDataBuf);
            this.tabData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabData.Location = new System.Drawing.Point(3, 3);
            this.tabData.Name = "tabData";
            this.tabData.SelectedIndex = 0;
            this.tabData.Size = new System.Drawing.Size(949, 1091);
            this.tabData.TabIndex = 0;
            // 
            // tabDataImg
            // 
            this.tabDataImg.Controls.Add(this.tableLayoutImages);
            this.tabDataImg.Location = new System.Drawing.Point(4, 31);
            this.tabDataImg.Name = "tabDataImg";
            this.tabDataImg.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataImg.Size = new System.Drawing.Size(941, 1056);
            this.tabDataImg.TabIndex = 0;
            this.tabDataImg.Text = "Images";
            this.tabDataImg.UseVisualStyleBackColor = true;
            // 
            // tableLayoutImages
            // 
            this.tableLayoutImages.ColumnCount = 3;
            this.tableLayoutImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutImages.Controls.Add(this.numImgLayer, 1, 0);
            this.tableLayoutImages.Controls.Add(this.comboImg, 0, 0);
            this.tableLayoutImages.Controls.Add(this.panelImg, 0, 1);
            this.tableLayoutImages.Controls.Add(this.numImgLevel, 2, 0);
            this.tableLayoutImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutImages.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutImages.Name = "tableLayoutImages";
            this.tableLayoutImages.RowCount = 2;
            this.tableLayoutImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutImages.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutImages.Size = new System.Drawing.Size(935, 1050);
            this.tableLayoutImages.TabIndex = 0;
            // 
            // numImgLayer
            // 
            this.numImgLayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numImgLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numImgLayer.Location = new System.Drawing.Point(564, 3);
            this.numImgLayer.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numImgLayer.Name = "numImgLayer";
            this.numImgLayer.Size = new System.Drawing.Size(181, 28);
            this.numImgLayer.TabIndex = 4;
            // 
            // comboImg
            // 
            this.comboImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboImg.FormattingEnabled = true;
            this.comboImg.Location = new System.Drawing.Point(3, 3);
            this.comboImg.Name = "comboImg";
            this.comboImg.Size = new System.Drawing.Size(555, 30);
            this.comboImg.TabIndex = 1;
            this.comboImg.SelectedIndexChanged += new System.EventHandler(this.comboImg_SelectedIndexChanged);
            // 
            // panelImg
            // 
            this.panelImg.AutoScroll = true;
            this.tableLayoutImages.SetColumnSpan(this.panelImg, 3);
            this.panelImg.Controls.Add(this.pictureImg);
            this.panelImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImg.Location = new System.Drawing.Point(3, 63);
            this.panelImg.Name = "panelImg";
            this.panelImg.Size = new System.Drawing.Size(929, 1095);
            this.panelImg.TabIndex = 2;
            // 
            // pictureImg
            // 
            this.pictureImg.Location = new System.Drawing.Point(0, 0);
            this.pictureImg.Name = "pictureImg";
            this.pictureImg.Size = new System.Drawing.Size(10, 10);
            this.pictureImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureImg.TabIndex = 0;
            this.pictureImg.TabStop = false;
            this.pictureImg.Click += new System.EventHandler(this.pictureImg_Click);
            // 
            // numImgLevel
            // 
            this.numImgLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numImgLevel.Location = new System.Drawing.Point(751, 3);
            this.numImgLevel.Name = "numImgLevel";
            this.numImgLevel.Size = new System.Drawing.Size(181, 28);
            this.numImgLevel.TabIndex = 5;
            // 
            // tabDataBuf
            // 
            this.tabDataBuf.Controls.Add(this.tableLayoutBufferDef);
            this.tabDataBuf.Location = new System.Drawing.Point(4, 31);
            this.tabDataBuf.Name = "tabDataBuf";
            this.tabDataBuf.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataBuf.Size = new System.Drawing.Size(941, 1056);
            this.tabDataBuf.TabIndex = 1;
            this.tabDataBuf.Text = "Buffers";
            this.tabDataBuf.UseVisualStyleBackColor = true;
            // 
            // tableLayoutBufferDef
            // 
            this.tableLayoutBufferDef.ColumnCount = 1;
            this.tableLayoutBufferDef.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutBufferDef.Controls.Add(this.tableBuf, 0, 1);
            this.tableLayoutBufferDef.Controls.Add(this.tableLayoutBuffers, 0, 0);
            this.tableLayoutBufferDef.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutBufferDef.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutBufferDef.Name = "tableLayoutBufferDef";
            this.tableLayoutBufferDef.RowCount = 2;
            this.tableLayoutBufferDef.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutBufferDef.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutBufferDef.Size = new System.Drawing.Size(935, 1050);
            this.tableLayoutBufferDef.TabIndex = 0;
            // 
            // tableBuf
            // 
            this.tableBuf.AllowUserToAddRows = false;
            this.tableBuf.AllowUserToDeleteRows = false;
            this.tableBuf.BackgroundColor = System.Drawing.SystemColors.Window;
            this.tableBuf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableBuf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableBuf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBuf.Location = new System.Drawing.Point(3, 43);
            this.tableBuf.Name = "tableBuf";
            this.tableBuf.ReadOnly = true;
            this.tableBuf.RowTemplate.Height = 28;
            this.tableBuf.Size = new System.Drawing.Size(929, 1006);
            this.tableBuf.TabIndex = 1;
            // 
            // tableLayoutBuffers
            // 
            this.tableLayoutBuffers.ColumnCount = 3;
            this.tableLayoutBuffers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutBuffers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutBuffers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutBuffers.Controls.Add(this.comboBuf, 0, 0);
            this.tableLayoutBuffers.Controls.Add(this.comboBufType, 1, 0);
            this.tableLayoutBuffers.Controls.Add(this.numBufDim, 2, 0);
            this.tableLayoutBuffers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutBuffers.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutBuffers.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutBuffers.Name = "tableLayoutBuffers";
            this.tableLayoutBuffers.RowCount = 1;
            this.tableLayoutBuffers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutBuffers.Size = new System.Drawing.Size(935, 40);
            this.tableLayoutBuffers.TabIndex = 2;
            // 
            // comboBuf
            // 
            this.comboBuf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBuf.FormattingEnabled = true;
            this.comboBuf.Location = new System.Drawing.Point(3, 3);
            this.comboBuf.Name = "comboBuf";
            this.comboBuf.Size = new System.Drawing.Size(555, 30);
            this.comboBuf.TabIndex = 0;
            this.comboBuf.SelectedIndexChanged += new System.EventHandler(this.comboBuf_SelectedIndexChanged);
            // 
            // comboBufType
            // 
            this.comboBufType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBufType.FormattingEnabled = true;
            this.comboBufType.Items.AddRange(new object[] {
            "byte",
            "char",
            "short",
            "ushort",
            "int",
            "uint",
            "long",
            "ulong",
            "float",
            "double"});
            this.comboBufType.Location = new System.Drawing.Point(564, 3);
            this.comboBufType.Name = "comboBufType";
            this.comboBufType.Size = new System.Drawing.Size(181, 30);
            this.comboBufType.TabIndex = 1;
            this.comboBufType.SelectedIndexChanged += new System.EventHandler(this.comboBufType_SelectedIndexChanged);
            // 
            // numBufDim
            // 
            this.numBufDim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numBufDim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numBufDim.Location = new System.Drawing.Point(751, 3);
            this.numBufDim.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.numBufDim.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBufDim.Name = "numBufDim";
            this.numBufDim.Size = new System.Drawing.Size(181, 28);
            this.numBufDim.TabIndex = 2;
            this.numBufDim.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numBufDim.ValueChanged += new System.EventHandler(this.numBufDim_ValueChanged);
            // 
            // tabProperties
            // 
            this.tabProperties.Controls.Add(this.tableLayoutPanel1);
            this.tabProperties.Location = new System.Drawing.Point(4, 31);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabProperties.Size = new System.Drawing.Size(955, 1097);
            this.tabProperties.TabIndex = 2;
            this.tabProperties.Text = "Properties";
            this.tabProperties.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.propertyGrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboProp, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(949, 1091);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // propertyGrid
            // 
            this.propertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(3, 43);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(943, 1045);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            this.propertyGrid.Click += new System.EventHandler(this.propertyGrid_Click);
            // 
            // comboProp
            // 
            this.comboProp.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboProp.FormattingEnabled = true;
            this.comboProp.Location = new System.Drawing.Point(3, 3);
            this.comboProp.Name = "comboProp";
            this.comboProp.Size = new System.Drawing.Size(943, 30);
            this.comboProp.TabIndex = 0;
            this.comboProp.SelectedIndexChanged += new System.EventHandler(this.comboProp_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(35, 6);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2014, 1132);
            this.Controls.Add(this.splitRenderCoding);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "App";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProtoGL";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_FormClosing);
            this.Load += new System.EventHandler(this.App_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.App_KeyUp);
            this.splitRenderCoding.Panel1.ResumeLayout(false);
            this.splitRenderCoding.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRenderCoding)).EndInit();
            this.splitRenderCoding.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabCode.ResumeLayout(false);
            this.splitCodeError.Panel1.ResumeLayout(false);
            this.splitCodeError.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCodeError)).EndInit();
            this.splitCodeError.ResumeLayout(false);
            this.tabCodeTableLayout.ResumeLayout(false);
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.RightToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.RightToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabOutput.ResumeLayout(false);
            this.tabCompile.ResumeLayout(false);
            this.tabDebugger.ResumeLayout(false);
            this.splitDebug.Panel1.ResumeLayout(false);
            this.splitDebug.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDebug)).EndInit();
            this.splitDebug.ResumeLayout(false);
            this.tabResources.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            this.tabDataImg.ResumeLayout(false);
            this.tableLayoutImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numImgLayer)).EndInit();
            this.panelImg.ResumeLayout(false);
            this.panelImg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numImgLevel)).EndInit();
            this.tabDataBuf.ResumeLayout(false);
            this.tableLayoutBufferDef.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableBuf)).EndInit();
            this.tableLayoutBuffers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numBufDim)).EndInit();
            this.tabProperties.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitRenderCoding;
        private OpenTK.GraphicControl glControl;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCode;
        private System.Windows.Forms.SplitContainer splitCodeError;
        private System.Windows.Forms.RichTextBox codeError;
        private System.Windows.Forms.TabPage tabResources;
        private System.Windows.Forms.TabControl tabData;
        private System.Windows.Forms.TabPage tabDataImg;
        private System.Windows.Forms.TabPage tabDataBuf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutImages;
        private System.Windows.Forms.PictureBox pictureImg;
        private System.Windows.Forms.ComboBox comboImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutBufferDef;
        private System.Windows.Forms.ComboBox comboBuf;
        private System.Windows.Forms.DataGridView tableBuf;
        private System.Windows.Forms.TableLayoutPanel tabCodeTableLayout;
        private System.Windows.Forms.Panel panelImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutBuffers;
        private System.Windows.Forms.ComboBox comboBufType;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.TabControl tabSource;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolBtnOpen;
        private System.Windows.Forms.ToolStripButton toolBtnSave;
        private System.Windows.Forms.ToolStripButton toolBtnSaveAll;
        private System.Windows.Forms.ToolStripButton toolBtnRun;
        private System.Windows.Forms.ToolStripButton toolBtnNew;
        private System.Windows.Forms.ToolStripButton toolBtnSaveAs;
        private System.Windows.Forms.ToolStripButton toolBtnClose;
        private System.Windows.Forms.NumericUpDown numBufDim;
        private System.Windows.Forms.TabPage tabProperties;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ComboBox comboProp;
        private System.Windows.Forms.NumericUpDown numImgLayer;
        private System.Windows.Forms.NumericUpDown numImgLevel;
        private System.Windows.Forms.TabControl tabOutput;
        private System.Windows.Forms.TabPage tabCompile;
        private System.Windows.Forms.TabPage tabDebugger;
        private System.Windows.Forms.PropertyGrid debugProperty;
        private System.Windows.Forms.ToolStripButton toolBtnDbg;
        private System.Windows.Forms.SplitContainer splitDebug;
        private System.Windows.Forms.ListView debugListView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolBtnPick;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}