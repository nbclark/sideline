using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MobileSrc.Sideline
{
    internal interface IGrouper<T>
    {
        string GetGroupText(T o);
    }
    // Base custom control for DrawFontList
    class OwnerDrawnListBox<T> : Control
    {
        const int SCROLL_WIDTH = 20;
        int itemHeight = -1;
        int selectedIndex = -1;
        int viewableItemCount = 0;
        int _groupHeight = 0;

        static Bitmap GroupingBar = Properties.Resources.groupingbar;
        Bitmap offScreen;
        VScrollBar vs;
        SortableList<T> items, filteredItems;
        Font _groupingFont = null;

        public bool _useGrouping = true;
        IGrouper<T> _grouping = null;
        IEquatable<T> _filter = null;
        private string _sortProperty = string.Empty;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private Dictionary<Rectangle, int> _screenIndices = new Dictionary<Rectangle, int>();

        public event EventHandler ItemActivate;
        
        public OwnerDrawnListBox()
        {
            this.vs = new VScrollBar();
            this.vs.Parent = this;
            this.vs.Visible = false;
            this.vs.SmallChange = 1;
            this.vs.ValueChanged += new EventHandler(this.ScrollValueChanged);

            this.items = new SortableList<T>();
            this.items.ListChanged += new ListChangedEventHandler(items_ListChanged);
            this.filteredItems = new SortableList<T>();

            if (null == _groupingFont)
            {
                _groupingFont = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
            }
            using (Graphics g = Graphics.FromImage(GroupingBar))
            {
                _groupHeight = (int)Math.Ceiling(g.MeasureString("A", _groupingFont).Height * 1.5);
            }
        }

        public string EmptySetText
        {
            get;
            set;
        }

        public bool UseGrouping
        {
            get { return (_useGrouping && this.Grouping != null); }
            set { _useGrouping = value; }
        }

        public string SortProperty
        {
            get { return _sortProperty; }
            set
            {
                if (_sortProperty != value)
                {
                    _sortProperty = value;
                    UpdateFiltered();
                }
            }
        }

        public ListSortDirection SortDirection
        {
            get
            {
                return _sortDirection;
            }
            set
            {
                if (_sortDirection != value)
                {
                    _sortDirection = value;
                    UpdateFiltered();
                }
            }
        }

        protected virtual void OnItemAdded(object sender, EventArgs e)
        {
            //
        }

        void items_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateFiltered();
            OnItemAdded(this, new EventArgs());
        }

        private void UpdateFiltered()
        {
            this.filteredItems.Clear();

            foreach (T item in this.Items)
            {
                if (null == _filter || _filter.Equals(item))
                {
                    this.filteredItems.Add(item);
                }
            }
            if (!string.IsNullOrEmpty(this.SortProperty))
            {
                this.filteredItems.Sort(this.SortProperty, this.SortDirection);
            }

            OnResize(null);
        }

        public IEquatable<T> Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                UpdateFiltered();
            }
        }

        public IGrouper<T> Grouping
        {
            get { return _grouping; }
            set
            {
                _grouping = value;
                UpdateFiltered();
            }
        }

        public SortableList<T> FilteredItems
        {
            get
            {
                return this.filteredItems;
            }
        }

        public SortableList<T> Items
        {
            get
            {
                return this.items;
            }
        }

        protected Bitmap OffScreen
        {
            get
            {
                return this.offScreen;
            }
        }

        protected VScrollBar VScrollBar
        {
            get
            {
                return this.vs;
            }
        }

        public new ContextMenu ContextMenu
        {
            get { return base.ContextMenu; }
            set
            {
                try
                {
                    if (base.ContextMenu != null)
                    {
                        base.ContextMenu.Popup -= new EventHandler(ContextMenu_Popup);
                    }
                    base.ContextMenu = value;

                    if (base.ContextMenu != null)
                    {
                        base.ContextMenu.Popup += new EventHandler(ContextMenu_Popup);
                    }
                }
                catch
                {
                }
            }
        }

        public event EventHandler SelectedIndexChanged;

        // Raise the SelectedIndexChanged event
        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            if (this.SelectedIndexChanged != null)
            {
                this.SelectedIndexChanged(this, e);
            }
        }

        private Point _mouseDown = default(Point);
        private int _startVsValue = 0;

        public int PointToIndex(Point p)
        {
            foreach (Rectangle rect in _screenIndices.Keys)
            {
                if (rect.Top <= p.Y && rect.Bottom >= p.Y)
                {
                    return _screenIndices[rect];
                }
            }
            return -1;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mouseDown = new Point(e.X, e.Y);
            _startVsValue = this.vs.Value;
            this.SelectedIndex = PointToIndex(_mouseDown);// Math.Min(this.FilteredItems.Count - 1, Math.Max(0, this.vs.Value + (e.Y / this.ItemHeight)));

            // Invalidate the control so we can draw the item as selected.
            this.Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            //if (Math.Abs(e.X - _mouseDown.X) <= 12 && Math.Abs(e.Y - _mouseDown.Y) <= 12)
            {
                int index = PointToIndex(new Point(e.X, e.Y));

                if (index == this.SelectedIndex)
                {
                    OnItemActivate(this, e);
                }
            }
            //this.SelectedIndex = this.vs.Value + (e.Y / this.ItemHeight);
            // Invalidate the control so we can draw the item as selected.
            this.Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.vs.Value = _startVsValue - ((e.Y - _mouseDown.Y) / this.ItemHeight);
            base.OnMouseMove(e);
        }

        protected void ContextMenu_Popup(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Control.MousePosition);
            this.SelectedIndex = PointToIndex(p);// Math.Min(this.FilteredItems.Count - 1, Math.Max(0, this.vs.Value + (p.Y / this.ItemHeight)));

            foreach (MenuItem item in ((ContextMenu)sender).MenuItems)
            {
                item.Enabled = (this.SelectedIndex > -1);
            }

            // Invalidate the control so we can draw the item as selected.
            this.Refresh();
        }

        // Need to set focus to the control when it
        // is clicked so that keyboard events occur.
        protected override void OnClick(EventArgs e)
        {
            this.Focus();

            base.OnClick(e);
        }

        // Get or set index of selected item.
        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                this.selectedIndex = value;

                if (this.SelectedIndexChanged != null)
                {
                    this.SelectedIndexChanged(this, EventArgs.Empty);
                }
            }
        }

        protected void ScrollValueChanged(object o, EventArgs e)
        {
            this.Refresh();
        }

        protected virtual int ItemHeight
        {
            get
            {
                return this.itemHeight;
            }

            set
            {
                this.itemHeight = value;
            }
        }

        // If the requested index is before the first visible index then set the
        // first item to be the requested index. If it is after the last visible
        // index, then set the last visible index to be the requested index.
        public void EnsureVisible(int index)
        {
            if (index < this.vs.Value)
            {
                this.vs.Value = index;
                this.Refresh();
            }
            else if (index >= this.vs.Value + this.DrawCount)
            {
                this.vs.Value = index - this.DrawCount + 1;
                this.Refresh();
            }
        }

        // Selected item moves when you use the keyboard up/down keys.
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (this.SelectedIndex < this.FilteredItems.Count-1)
                    {
                        EnsureVisible(++this.SelectedIndex);
                        this.Refresh();
                    }
                    break;
                case Keys.Up:
                    if (this.SelectedIndex > 0)
                    {
                        EnsureVisible(--this.SelectedIndex);
                        this.Refresh();
                    }
                    break;
                case Keys.PageDown:
                    this.SelectedIndex = Math.Min(this.FilteredItems.Count - 1, this.SelectedIndex + this.DrawCount);
                    EnsureVisible(this.SelectedIndex);
                    this.Refresh();
                    break;
                case Keys.PageUp:
                    this.SelectedIndex = Math.Max(0, this.SelectedIndex - this.DrawCount);
                    EnsureVisible(this.SelectedIndex);
                    this.Refresh();
                    break;
                case Keys.Home:
                    this.SelectedIndex = 0;
                    EnsureVisible(this.SelectedIndex);
                    this.Refresh();
                    break;
                case Keys.End:
                    this.SelectedIndex = this.FilteredItems.Count - 1;
                    EnsureVisible(this.SelectedIndex);
                    this.Refresh();
                    break;
                case Keys.Enter:
                    {
                        OnItemActivate(this, new EventArgs());
                        break;
                    }
            }

            base.OnKeyDown(e);
        }

        protected virtual void OnItemActivate(object sender, EventArgs e)
        {
            if (null != ItemActivate)
            {
                if (this.SelectedIndex >= 0 && this.SelectedIndex < this.FilteredItems.Count)
                {
                    ItemActivate(sender, e);
                }
            }
        }

        // Calculate how many items we can draw given the height of the control.
        protected int DrawCount
        {
            get
            {
                if (this.vs.Value + this.vs.LargeChange > this.vs.Maximum)
                {
                    return this.vs.Maximum - this.vs.Value + 1;
                }
                else
                {
                    return this.vs.LargeChange;
                }
            }
        }

        // Calculate how many items we can draw given the height of the control.
        protected int PaintCount
        {
            get
            {
                if (this.vs.Value + this.viewableItemCount > this.vs.Maximum)
                {
                    return this.vs.Maximum - this.vs.Value + 1;
                }
                else
                {
                    return this.viewableItemCount;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            viewableItemCount = (int)Math.Ceiling((double)this.ClientSize.Height / this.ItemHeight);
            int scrollableItemCount = this.ClientSize.Height / this.ItemHeight;

            this.vs.Bounds = new Rectangle(this.ClientSize.Width - SCROLL_WIDTH,
                0,
                SCROLL_WIDTH,
                this.ClientSize.Height);


            Bitmap old = this.offScreen;
            // Determine if scrollbars are needed
            if (this.FilteredItems.Count > scrollableItemCount)
            {
                this.vs.Visible = true;
                this.vs.LargeChange = scrollableItemCount;

                this.offScreen = new Bitmap(this.ClientSize.Width - SCROLL_WIDTH, this.ClientSize.Height);
            }
            else
            {
                this.vs.Visible = false;
                this.vs.LargeChange = this.FilteredItems.Count;
                this.offScreen = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            }

            if (null != old)
            {
                old.Dispose();
            }

            this.vs.Maximum = this.FilteredItems.Count - 1;

            if (this.UseGrouping)
            {
                int groupCount = 0;
                string groupText = string.Empty;

                foreach (T item in this.FilteredItems)
                {
                    string currText = this.Grouping.GetGroupText(item);

                    if (groupText != currText)
                    {
                        groupCount++;
                        groupText = currText;
                    }
                }
                this.vs.Maximum += (int)Math.Floor((double)groupCount * this._groupHeight / this.ItemHeight);
            }
        }

        // Determine what the text color should be
        // for the selected item drawn as highlighted
        Color CalcTextColor(Color backgroundColor)
        {
            if (backgroundColor.Equals(Color.Empty))
            {
                return Color.Black;
            }

            int sum = backgroundColor.R + backgroundColor.G + backgroundColor.B;

            if (sum > 256)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // The base class contains a bitmap, offScreen, for constructing
            // the control and is rendered when all items are populated.
            // This technique prevents flicker.
            _screenIndices.Clear();
            using (Graphics gOffScreen = Graphics.FromImage(this.OffScreen))
            {
                using (Brush backBrush = new SolidBrush(this.BackColor))
                {
                    gOffScreen.FillRectangle(backBrush, this.ClientRectangle);
                }

                int itemTop = 0;
                int itemWidth = this.Width;

                if (this.VScrollBar.Visible)
                {
                    itemWidth -= this.VScrollBar.Width;
                }

                if (this.FilteredItems.Count == 0)
                {
                    using (Brush foreBrush = new SolidBrush(this.ForeColor))
                    {
                        StringFormat format = new StringFormat();
                        format.LineAlignment = StringAlignment.Center;
                        format.Alignment = StringAlignment.Center;

                        gOffScreen.DrawString(this.EmptySetText, this.Font, foreBrush, this.ClientRectangle, format);
                    }
                }
                else
                {
                    string groupText = string.Empty;

                    if (this.VScrollBar.Value > 0 && this.UseGrouping)
                    {
                        groupText = this.Grouping.GetGroupText(this.FilteredItems[this.VScrollBar.Value - 1]);
                    }

                    // Draw the fonts in the list.
                    for (int n = this.VScrollBar.Value; n < this.VScrollBar.Value + this.PaintCount + 10 && n < this.FilteredItems.Count; n++)
                    {
                        string currGroupText = string.Empty;

                        if (this.UseGrouping)
                        {
                            currGroupText = this.Grouping.GetGroupText(this.FilteredItems[n]);
                        }

                        if (currGroupText != groupText)
                        {
                            groupText = currGroupText;

                            using (SolidBrush brush = new SolidBrush(this.ForeColor))
                            {
                                Rectangle rectGroup = new Rectangle(5, itemTop, itemWidth - 5, _groupHeight);
                                StringFormat fmat = new StringFormat();
                                fmat.LineAlignment = StringAlignment.Center;
                                gOffScreen.DrawString(groupText, _groupingFont, brush, rectGroup, fmat);
                                itemTop += _groupHeight;

                                _screenIndices.Add(rectGroup, -1);
                            }
                            gOffScreen.DrawImage(GroupingBar, new Rectangle(5, itemTop - 4, itemWidth - 5, GroupingBar.Height), new Rectangle(0, 0, GroupingBar.Width, GroupingBar.Height), GraphicsUnit.Pixel);
                        }

                        Color backColor, fontColor;
                        // Draw the selected item to appear highlighted
                        if (n == this.SelectedIndex)
                        {
                            Color highlightColor = Color.FromArgb(0xaa, 0xcd, 0xff);
                            using (SolidBrush highlightBrush = new SolidBrush(highlightColor))
                            {
                                gOffScreen.FillRectangle(highlightBrush,//SystemColors.Highlight),
                                    1,
                                    itemTop + 1,
                                    // If the scroll bar is visible, subtract the scrollbar width
                                    // otherwise subtract 2 for the width of the rectangle
                                    this.ClientSize.Width - (this.VScrollBar.Visible ? this.VScrollBar.Width : 2),
                                    this.ItemHeight);
                                backColor = highlightColor;
                                fontColor = CalcTextColor(highlightColor);
                            }
                        }
                        else
                        {
                            backColor = this.BackColor;
                            fontColor = this.ForeColor;
                        }

                        Rectangle bounds = new Rectangle(0, itemTop, itemWidth, this.ItemHeight);
                        DrawItemEventArgs ea = new OwnerDrawnListBox<T>.DrawItemEventArgs(n, this.FilteredItems[n], bounds, gOffScreen, backColor, fontColor);
                        _screenIndices.Add(bounds, n);
                        OnDrawItem(ea);

                        itemTop += this.ItemHeight;

                        if (itemTop > this.Bottom)
                        {
                            break;
                        }
                    }
                }

                // Draw the list box
                e.Graphics.DrawImage(this.OffScreen, 0, 0);
            }
        }

        protected virtual void OnDrawItem(DrawItemEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.Item), this.Font, brush, e.Bounds);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //e.Graphics.Clear(BackColor);
            //base.OnPaintBackground(e);
        }

        internal class DrawItemEventArgs
        {
            private int _index = -1;
            private T _obj;
            private Rectangle _bounds;
            private Graphics _graphics;
            private Color _backColor, _foreColor;

            public DrawItemEventArgs(int index, T obj, Rectangle bounds, Graphics graphics, Color backColor, Color foreColor)
            {
                _index = index;
                _obj = obj;
                _bounds = bounds;
                _graphics = graphics;
                _backColor = backColor;
                _foreColor = foreColor;
            }

            public int Index
            {
                get { return _index; }
            }

            public T Item
            {
                get { return _obj; }
            }

            public Rectangle Bounds
            {
                get { return _bounds; }
            }

            public Graphics Graphics
            {
                get { return _graphics; }
            }

            public Color BackColor
            {
                get { return _backColor; }
            }

            public Color ForeColor
            {
                get { return _foreColor; }
            }
        }
    }
}