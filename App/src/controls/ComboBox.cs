using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace System.Windows.Forms
{
    public class ComboBoxEx : ListControl
    {
        #region Delegate Definitions

        public delegate void DroppedDownEventHandler(object sender, EventArgs e);
        public delegate void DrawItemEventHandler(object sender, DrawItemEventArgs e);
        public delegate void MeasureItemEventHandler(object sender, MeasureItemEventArgs e);

        #endregion

        #region Fields

        #pragma warning disable 0414
        private bool hovered = false;
        private bool pressed = false;
        private bool resize = false;
        #pragma warning restore 0414

        private Color _forecolor = SystemColors.ControlText;
        private Color _backcolor = SystemColors.Control;
        private Color _bordercolor = SystemColors.ActiveBorder;
        private Color _transparent = Color.FromArgb(0, 0, 0, 0);
        private Corners _radius = new Corners();

        private int _dropDownHeight = 200;
        private int _dropDownWidth = 0;
        private int _maxDropDownItems = 8;
        private int _selectedIndex = -1;
        private bool _isDroppedDown = false;

        private ComboBoxStyle _dropDownStyle = ComboBoxStyle.DropDownList;

        private Rectangle rectBtn = new Rectangle(0, 0, 1, 1);
        private Rectangle rectContent = new Rectangle(0, 0, 1, 1);

        private ToolStripControlHost _controlHost;
        private ListBox _listBox;
        private ToolStripDropDown _popupControl;
        private TextBox _textBox;

        #endregion
        
        #region Delegates

        [Category("Behavior"), Description("Occurs when IsDroppedDown changed to True.")]
        public event DroppedDownEventHandler DroppedDown;
        [Category("Behavior"), Description("Occurs when the SelectedIndex property changes.")]
        public event EventHandler SelectedIndexChanged;
        [Category("Behavior"), Description("Occurs whenever a particular item/area needs to be painted.")]
        public event DrawItemEventHandler DrawItem;
        [Category("Behavior"), Description("Occurs whenever a particular item's height needs to be calculated.")]
        public event MeasureItemEventHandler MeasureItem;

        #endregion
        
        #region Properties

        public Color Transparent
        {
            get => _transparent; set { _transparent = value; Invalidate(true); }
        }

        public Color BorderColor
        {
            get => _bordercolor; set { _bordercolor = value; Invalidate(true); }
        }

        public int DropDownHeight
        {
            get => _dropDownHeight; set => _dropDownHeight = value;
        }

        public ListBox.ObjectCollection Items
        {
            get { return _listBox.Items; }
        }

        public int DropDownWidth
        {
            get => _dropDownWidth; set => _dropDownWidth = value;
        }

        public int MaxDropDownItems
        {
            get => _maxDropDownItems; set => _maxDropDownItems = value;
        }

        public new object DataSource
        {
            get => base.DataSource; set 
            { 
                _listBox.DataSource = value;
                base.DataSource = value;
                OnDataSourceChanged(System.EventArgs.Empty);
            }
        }

        public bool Soreted
        {
            get => _listBox.Sorted; set => _listBox.Sorted = value;
        }

        [Category("Behavior"), Description("Indicates whether the code or the OS will handle the drawing of elements in the list.")]
        public DrawMode DrawMode
        {
            get => _listBox.DrawMode; set => _listBox.DrawMode = value;
        }
        
        public ComboBoxStyle DropDownStyle
        {
            get => _dropDownStyle; set 
            { 
                _dropDownStyle = value;
                _textBox.Visible = _dropDownStyle != ComboBoxStyle.DropDownList;
                Invalidate(true);
            }
        }

        public new Color BackColor
        {
            get => _backcolor; set 
            { 
                _backcolor = value;
                _textBox.BackColor = value;
                _listBox.BackColor = value;
                Invalidate(true);
            }
        }

        public new Color ForeColor
        {
            get => _forecolor; set
            {
                _forecolor = value;
                _textBox.ForeColor = value;
                _listBox.ForeColor = value;
                Invalidate(true);
            }
        }

        public bool IsDroppedDown
        {
            get => _isDroppedDown; set 
            {
                if (_isDroppedDown == true && value == false )
                {
                    if (_popupControl.IsDropDown)
                        _popupControl.Close();
                }

                _isDroppedDown = value;

                if (_isDroppedDown)
                {
                    _controlHost.Control.Width = _dropDownWidth;

                    _listBox.Refresh();

                    if (_listBox.Items.Count > 0) 
                    {
                        int h = 0;
                        int i = 0;
                        int maxItemHeight = 0;
                        int highestItemHeight = 0;
                        foreach(object item in _listBox.Items)
                        {
                            int itHeight = _listBox.GetItemHeight(i);
                            if (highestItemHeight < itHeight) 
                            {
                                highestItemHeight = itHeight;
                            }
                            h = h + itHeight;
                            if (i <= (_maxDropDownItems - 1)) 
                            {
                                maxItemHeight = h;
                            }
                            i = i + 1;
                        }

                        if (maxItemHeight > _dropDownHeight)
                            _listBox.Height = _dropDownHeight + 3;
                        else
                        {
                            if (maxItemHeight > highestItemHeight )
                                _listBox.Height = maxItemHeight + 3;
                            else
                                _listBox.Height = highestItemHeight + 3;
                        }
                    }
                    else
                    {
                        _listBox.Height = 15;
                    }

                    _popupControl.Show(this, CalculateDropPosition(), ToolStripDropDownDirection.BelowRight);
                }

                Invalidate();
                if (_isDroppedDown)
                    OnDroppedDown(this, EventArgs.Empty);
            }
        }

        public Corners CornerRadius => _radius;

        #endregion
        
        #region Constructor

        public ComboBoxEx()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserMouse, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Selectable, true);

            _radius.BottomLeft = 0;
            _radius.BottomRight = 0;
            _radius.TopLeft = 0;
            _radius.TopRight = 0;

            Height = 21;
            Width = 95;

            SuspendLayout();
            _textBox = new TextBox();
            _textBox.BorderStyle = BorderStyle.None;
            _textBox.Location = new Drawing.Point(3, 4);
            _textBox.Size = new Drawing.Size(60, 13);
            _textBox.TabIndex = 0;
            _textBox.WordWrap = false;
            _textBox.Margin = new Padding(0);
            _textBox.Padding = new Padding(0);
            _textBox.TextAlign = HorizontalAlignment.Left;
            _textBox.ForeColor = _forecolor;
            _textBox.BackColor = _backcolor;
            Controls.Add(_textBox);
            ResumeLayout(false);

            AdjustControls();

            _listBox = new ListBox();
            _listBox.IntegralHeight = true;
            _listBox.BorderStyle = BorderStyle.FixedSingle;
            _listBox.SelectionMode = SelectionMode.One;
            _listBox.ForeColor = _forecolor;
            _listBox.BackColor = _backcolor;
            _listBox.BindingContext = new BindingContext();

            _controlHost = new ToolStripControlHost(_listBox);
            _controlHost.Padding = new Padding(0);
            _controlHost.Margin = new Padding(0);
            _controlHost.AutoSize = false;

            _popupControl = new ToolStripDropDown();
            _popupControl.Padding = new Padding(0);
            _popupControl.Margin = new Padding(0);
            _popupControl.AutoSize = true;
            _popupControl.DropShadowEnabled = false;
            _popupControl.Items.Add(_controlHost);

            _dropDownWidth = Width;

            _listBox.MeasureItem += listBox_MeasureItem;
            _listBox.DrawItem += listBox_DrawItem;
            _listBox.MouseClick += listBox_MouseClick;
            _listBox.MouseMove += listBox_MouseMove;

            _popupControl.Closed += popupControl_Closed;

            _textBox.Resize += textBox_Resize;
            _textBox.TextChanged += textBox_TextChanged;

            base.BackColor = BackColor = _backcolor;
            ForeColor = _forecolor;
        }
        
        #endregion
        
        #region Overrides

        protected override void OnDataSourceChanged(EventArgs e)
        {
            SelectedIndex = 0;
            base.OnDataSourceChanged(e);
        }

        protected override void OnDisplayMemberChanged(EventArgs e)
        {
            _listBox.DisplayMember = DisplayMember;
            SelectedIndex = SelectedIndex;
            base.OnDisplayMemberChanged(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            Invalidate(true);
            base.OnEnabledChanged(e);
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            _textBox.ForeColor = ForeColor;
            base.OnForeColorChanged(e);
        }

        protected override void OnFormatInfoChanged(EventArgs e)
        {
            _listBox.FormatInfo = FormatInfo;
            base.OnFormatInfoChanged(e);
        }

        protected override void OnFormatStringChanged(EventArgs e)
        {
            _listBox.FormatString = FormatString;
            base.OnFormatStringChanged(e);
        }

        protected override void OnFormattingEnabledChanged(EventArgs e)
        {
            _listBox.FormattingEnabled = FormattingEnabled;
            base.OnFormattingEnabledChanged(e);
        }

        public override Font Font
        {
            get => base.Font; set
            {
                resize = true;
                _textBox.Font = value;
                base.Font = value;
                Invalidate(true);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.MouseDown += new MouseEventHandler(Control_MouseDown);
            e.Control.MouseEnter += new EventHandler(Control_MouseEnter);
            e.Control.MouseLeave += new EventHandler(Control_MouseLeave);
            e.Control.GotFocus += new EventHandler(Control_GotFocus);
            e.Control.LostFocus += new EventHandler(Control_LostFocus);
            base.OnControlAdded(e);
        }        

        protected override void OnMouseEnter(EventArgs e)
        {
            hovered = true;
            Invalidate(true);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!RectangleToScreen(ClientRectangle).Contains(MousePosition))
            {
                hovered = false;
                Invalidate(true);
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _textBox.Focus();
            if ((RectangleToScreen(rectBtn).Contains(MousePosition) ||
                (DropDownStyle == ComboBoxStyle.DropDownList)))
            {
                pressed = true;
                Invalidate(true);
                if (IsDroppedDown) 
                    IsDroppedDown = false;
                IsDroppedDown = true;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            pressed = false;
            hovered = RectangleToScreen(ClientRectangle).Contains(MousePosition);
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta < 0)
                SelectedIndex = SelectedIndex + 1;
            else if (e.Delta > 0 && SelectedIndex > 0)
                SelectedIndex = SelectedIndex - 1;

            base.OnMouseWheel(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            Invalidate(true);
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (!ContainsFocus)
                Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, e);
            base.OnSelectedIndexChanged(e);
        }

        protected override void OnValueMemberChanged(EventArgs e)
        {
            _listBox.ValueMember = ValueMember;
            SelectedIndex = SelectedIndex;
            base.OnValueMemberChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (resize)
            {
                resize = false;
                AdjustControls();
            }
            Invalidate(true);

            _dropDownWidth = Width;
        }

        public override string Text
        {
            get => _textBox.Text; set
            {
                _textBox.Text = value;
                base.Text = _textBox.Text;
                OnTextChanged(EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            //content border
            var rectCont = rectContent;
            rectCont.X += 1;
            rectCont.Y += 1;
            rectCont.Width -= 2;
            rectCont.Height -= 2;
            var pathContentBorder = CreateRoundRectangle(rectCont,
                CornerRadius.TopLeft, CornerRadius.TopRight, CornerRadius.BottomRight, CornerRadius.BottomLeft);

            //button border
            var rectButton = rectBtn;
            rectButton.X += 1;
            rectButton.Y += 1;
            rectButton.Width -= 2;
            rectButton.Height -= 2;
            var pathBtnBorder = CreateRoundRectangle(rectButton,
                0, CornerRadius.TopRight, CornerRadius.BottomRight, 0);

            //outer border
            var rectOuter = rectContent;
            rectOuter.Width -= 1;
            rectOuter.Height -= 1;
            var pathOuterBorder = CreateRoundRectangle(rectOuter,
                CornerRadius.TopLeft, CornerRadius.TopRight, CornerRadius.BottomRight, CornerRadius.BottomLeft);

            //inner border
            var rectInner = rectContent;
            rectInner.X += 1;
            rectInner.Y += 1;
            rectInner.Width -= 2;
            rectInner.Height -= 2;
            //GraphicsPath pathInnerBorder = CreateRoundRectangle(rectInner,
            //    Radius.TopLeft, Radius.TopRight, Radius.BottomRight, Radius.BottomLeft);

            //brushes and pens
            var brInnerBrush = new SolidBrush(ForeColor);
            var brBackground = new SolidBrush(BackColor);
            var penOuterBorder = new Pen(BorderColor, 0);
            var brButtonLeft = new LinearGradientBrush(rectBtn, ForeColor, ForeColor, LinearGradientMode.Vertical);
            var blend = new ColorBlend();
            blend.Colors = new Color[] { Transparent, ForeColor, Transparent };
            blend.Positions = new float[] { 0.0f, 0.5f, 1.0f};
            brButtonLeft.InterpolationColors = blend;
            var penLeftButton = new Pen(brButtonLeft, 0);
            var brButton = new SolidBrush(ForeColor);

            //draw
            e.Graphics.FillPath(brBackground, pathContentBorder);
            if (DropDownStyle != ComboBoxStyle.DropDownList)
                e.Graphics.FillPath(brButton, pathBtnBorder);
            e.Graphics.DrawPath(penOuterBorder, pathOuterBorder);

            e.Graphics.DrawLine(penLeftButton, rectBtn.Left + 1, rectInner.Top+1, rectBtn.Left + 1, rectInner.Bottom-1);

            //Glimph
            var rectGlimph = rectButton;
            rectButton.Width -= 4;
            e.Graphics.TranslateTransform(rectGlimph.Left + rectGlimph.Width / 2.0f, rectGlimph.Top + rectGlimph.Height / 2.0f);
            var path = new GraphicsPath();
            var points = new PointF[3];
            points[0] = new PointF(-6 / 2.0f, -3 / 2.0f);
            points[1] = new PointF(6 / 2.0f, -3 / 2.0f);
            points[2] = new PointF(0, 6 / 2.0f);
            path.AddLine(points[0], points[1]);
            path.AddLine(points[1], points[2]);
            path.CloseFigure();
            e.Graphics.RotateTransform(0);

            var br = new SolidBrush(Enabled ? ForeColor : BackColor);
            e.Graphics.FillPath(br, path);
            e.Graphics.ResetTransform();
            br.Dispose();
            path.Dispose();
            
            //text
            if (DropDownStyle == ComboBoxStyle.DropDownList)
            {
                var sf  = new StringFormat(StringFormatFlags.NoWrap);
                sf.Alignment = StringAlignment.Near;

                var rectText = _textBox.Bounds;
                rectText.Offset(-3, 0);

                var foreBrush = new SolidBrush(ForeColor);
                if (Enabled)
                    e.Graphics.DrawString(_textBox.Text, Font, foreBrush, rectText.Location);
                else
                    ControlPaint.DrawStringDisabled(e.Graphics, _textBox.Text, Font, BackColor, rectText, sf);
            }
            
            // dispose brushes and pens
            pathContentBorder.Dispose();
            pathOuterBorder.Dispose();
            //pathInnerBorder.Dispose();
            pathBtnBorder.Dispose();

            penOuterBorder.Dispose();
            penLeftButton.Dispose();

            brBackground.Dispose();
            brInnerBrush.Dispose();
            brButtonLeft.Dispose();
            brButton.Dispose();
        }

        #endregion
        
        #region List Control Overrides

        public override int SelectedIndex
        {
            get => _selectedIndex; set 
            { 
                if(_listBox != null)
                {
                    if (_listBox.Items.Count == 0)
                        return;

                    if ((DataSource != null) && value == -1)
                        return;

                    if (value <= (_listBox.Items.Count - 1) && value >= -1)
                    {
                        _listBox.SelectedIndex = value;
                        _selectedIndex = value;
                        _textBox.Text = _listBox.GetItemText(_listBox.SelectedItem);
                        OnSelectedIndexChanged(EventArgs.Empty);
                    }
                }
            }
        }

        public object SelectedItem
        {
            get => _listBox.SelectedItem; set 
            { 
                _listBox.SelectedItem = value;
                SelectedIndex = _listBox.SelectedIndex;
            }
        }

        public new object SelectedValue
        {
            get => base.SelectedValue; set => base.SelectedValue = value;
        }

        protected override void RefreshItem(int index)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        protected override void RefreshItems()
        {
            //base.RefreshItems();
        }

        protected override void SetItemCore(int index, object value)
        {
            //base.SetItemCore(index, value);
        }

        protected override void SetItemsCore(System.Collections.IList items)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region Nested Controls Events

        void Control_LostFocus(object sender, EventArgs e)
        {
            OnLostFocus(e);
        }

        void Control_GotFocus(object sender, EventArgs e)
        {
            OnGotFocus(e);
        }

        void Control_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        void Control_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        void Control_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        void listBox_MouseMove(object sender, MouseEventArgs e)
        {
            int i;
            for (i = 0; i < (_listBox.Items.Count); i++)
            {
                if (_listBox.GetItemRectangle(i).Contains(_listBox.PointToClient(MousePosition)))
                {
                    _listBox.SelectedIndex = i;
                    return;
                }
            }
        }

        void listBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (_listBox.Items.Count == 0 || _listBox.SelectedItems.Count != 1)
                return;

            SelectedIndex = _listBox.SelectedIndex;

            if (DropDownStyle == ComboBoxStyle.DropDownList)
                Invalidate(true);

            IsDroppedDown = false;
        }

        void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
                DrawItem?.Invoke(this, e);
        }

        void listBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            MeasureItem?.Invoke(this, e);
        }
        
        void popupControl_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            _isDroppedDown = false;
            pressed = false;
            if (!RectangleToScreen(ClientRectangle).Contains(MousePosition))
                hovered = false;
            Invalidate(true);
        }

        void textBox_Resize(object sender, EventArgs e)
        {
            AdjustControls();
        }

        void textBox_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

        #endregion

        #region Private Methods

        private void AdjustControls()
        {
            SuspendLayout();

            resize = true;
            _textBox.Top = 4;
            _textBox.Left = 5;
            Height = _textBox.Top + _textBox.Height + _textBox.Top;

            rectBtn = new System.Drawing.Rectangle(ClientRectangle.Width - 18,
                ClientRectangle.Top, 18, _textBox.Height + 2 * _textBox.Top);


            _textBox.Width = rectBtn.Left - 1 - _textBox.Left;

            rectContent = new Rectangle(ClientRectangle.Left, ClientRectangle.Top,
                ClientRectangle.Width, _textBox.Height + 2 * _textBox.Top);

            ResumeLayout();
            Invalidate(true);
        }

        private System.Drawing.Point CalculateDropPosition()
        {
            var point = new System.Drawing.Point(0, Height);
            if ((PointToScreen(new System.Drawing.Point(0, 0)).Y + Height + _controlHost.Height) > Screen.PrimaryScreen.WorkingArea.Height)
                point.Y = -_controlHost.Height - 7;
            return point;
        }

        private System.Drawing.Point CalculateDropPosition(int myHeight, int controlHostHeight)
        {
            var point = new System.Drawing.Point(0, myHeight);
            if ((PointToScreen(new System.Drawing.Point(0, 0)).Y + Height + controlHostHeight) > Screen.PrimaryScreen.WorkingArea.Height)
                point.Y = -controlHostHeight - 7;
            return point;
        }

        #endregion

        #region Virtual Methods

        public virtual void OnDroppedDown(object sender, EventArgs e)
        {
            DroppedDown?.Invoke(this, e);
        }

        #endregion

        #region Render

        public static GraphicsPath CreateRoundRectangle(Rectangle rectangle, 
            int topLeftRadius, int topRightRadius,
            int bottomRightRadius, int bottomLeftRadius)
        {
            var path = new GraphicsPath();
            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;

            if(topLeftRadius > 0)
                path.AddArc(l, t, topLeftRadius * 2, topLeftRadius * 2, 180, 90);
            
            path.AddLine(l + topLeftRadius, t, l + w - topRightRadius, t);

            if (topRightRadius > 0)
                path.AddArc(l + w - topRightRadius * 2, t, topRightRadius * 2, topRightRadius * 2, 270, 90);
            
            path.AddLine(l + w, t + topRightRadius, l + w, t + h - bottomRightRadius);

            if (bottomRightRadius > 0)
                path.AddArc(l + w - bottomRightRadius * 2, t + h - bottomRightRadius * 2,
                    bottomRightRadius * 2, bottomRightRadius * 2, 0, 90);
            
            path.AddLine(l + w - bottomRightRadius, t + h, l + bottomLeftRadius, t + h);

            if(bottomLeftRadius >0)
                path.AddArc(l, t + h - bottomLeftRadius * 2, bottomLeftRadius * 2, bottomLeftRadius * 2, 90, 90);

            path.AddLine(l, t + h - bottomLeftRadius, l, t + topLeftRadius);

            path.CloseFigure();
            return path;
        }

        #endregion

        #region Inner Classes
        
        public class Corners
        {
            private int _topLeft = 0;
            private int _topRight = 0;
            private int _bottomLeft = 0;
            private int _bottomRight = 0;

            public int TopLeft
            {
                get => _topLeft; set => _topLeft = value;
            }
            
            public int TopRight
            {
                get => _topRight; set => _topRight = value;
            }
            
            public int BottomLeft
            {
                get => _bottomLeft; set => _bottomLeft = value;
            }
            
            public int BottomRight
            {
                get => _bottomRight; set => _bottomRight = value;
            }
        }

        #endregion
    }
}
