/*=======================================================================================

	OpenNETCF.Windows.Forms.OwnerDrawnList
	Copyright � 2003, OpenNETCF.org

	This library is free software; you can redistribute it and/or modify it under 
	the terms of the OpenNETCF.org Shared Source License.

	This library is distributed in the hope that it will be useful, but 
	WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
	FITNESS FOR A PARTICULAR PURPOSE. See the OpenNETCF.org Shared Source License 
	for more details.

	You should have received a copy of the OpenNETCF.org Shared Source License 
	along with this library; if not, email licensing@opennetcf.org to request a copy.

	If you wish to contact the OpenNETCF Advisory Board to discuss licensing, please 
	email licensing@opennetcf.org.

	For general enquiries, email enquiries@opennetcf.org or visit our website at:
	http://www.opennetcf.org

=======================================================================================*/
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
//using ScrollBarsProject;
using System.ComponentModel;


namespace MobileSrc.Sideline
{
	
	/// <summary>
	/// Represents the method that will handle the DrawItem.
	/// </summary>
	public delegate void DrawItemEventHandler(object sender, DrawItemEventArgs e);


	#region DrawItemState enum
	/// <summary>
	/// Specifies the state of an item that is being drawn.
	/// </summary>
	public enum DrawItemState
	{
		/// <summary>
		/// The item currently has no state.
		/// </summary>
		None = 0,
		/// <summary>
		/// The item is selected.
		/// </summary>
		Selected = 1,
		/// <summary>
		/// The item is disabled.
		/// </summary>
		Disabled = 4,
		/// <summary>
		/// The item has focus.
		/// </summary>
		Focus = 16
	}
	#endregion

	#region DrawItemEventArgs

	/// <summary>
	/// Provides data for the DrawItem event.
	/// </summary>
	public class DrawItemEventArgs : System.EventArgs
	{
		//private members
		private Color backColor;
		private Color foreColor;
		private Font font;
		private int index;
		private Graphics graphics;
		private Rectangle rect;
		private DrawItemState state;
		
		/// <summary>
		/// Initializes a new instance
		/// </summary>
		public DrawItemEventArgs(System.Drawing.Graphics graphics, System.Drawing.Font font , System.Drawing.Rectangle rect , System.Int32 index , DrawItemState state , System.Drawing.Color foreColor , System.Drawing.Color backColor )
		{
			this.graphics = graphics;
			this.font = font;
			this.rect = rect;
			this.index = index;
			this.state = state;
			this.foreColor = foreColor;
			this.backColor = backColor;
			
		}
		
		/// <summary>
		/// Initializes a new instance
		/// </summary>
		public DrawItemEventArgs(System.Drawing.Graphics graphics, System.Drawing.Font font , System.Drawing.Rectangle rect , System.Int32 index , DrawItemState state )
		{
			this.graphics = graphics;
			this.font = font;
			this.rect = rect;
			this.index = index;
			this.state = state;
			this.foreColor = SystemColors.ControlText;
			this.backColor = SystemColors.Window;
		}
		
		/// <summary>
		/// Gets the rectangle that represents the bounds of the item that is being drawn.
		/// </summary>
		public Rectangle Bounds 
		{
			get 
			{
				return rect;
			}
		}

		/// <summary>
		/// Draws the background within the bounds specified in the DrawItemEventArgs constructor and with the appropriate color.
		/// </summary>
		public virtual void DrawBackground()
		{
			Brush brush;
			if (state == DrawItemState.Selected)
			{
				brush = new SolidBrush(SystemColors.Highlight);
			}
			else
			{
				brush = new SolidBrush(backColor);
			}		
			//graphics.FillRectangle(brush, rect);
			Rectangle rc = new Rectangle(rect.X + 1, rect.Y, rect.Width, rect.Height - 1);
			rc.Y+=1;
			graphics.FillRectangle(brush, rc);
			brush.Dispose();
		}

		/// <summary>
		/// Draws the background within the bounds specified in the DrawItemEventArgs constructor and with the appropriate color.
		/// </summary>
		public virtual void DrawBackground(Color color)
		{
			Brush brush;
			brush = new SolidBrush(color);
			Rectangle rc = new Rectangle(rect.X + 1, rect.Y, rect.Width, rect.Height - 1);
			rc.Y+=1;
			//rc.Inflate(0, -1);
			graphics.FillRectangle(brush, rc);
			brush.Dispose();
		}

		/// <summary>
		/// Draws a focus rectangle.
		/// </summary>
		public virtual void DrawFocusRectangle()
		{
			Rectangle focusRect = rect;
			focusRect.Width--;
			focusRect.Inflate(-1, 0);
			graphics.DrawRectangle(new Pen(SystemColors.Highlight), focusRect);
		}

		/// <summary>
		/// Gets the state of the item being drawn.
		/// </summary>
		public DrawItemState State
		{
			get{return state;}
		}

		/// <summary>
		/// Gets the graphics surface to draw the item on.
		/// </summary>
		public Graphics Graphics
		{
			get{return graphics;}
		}

		/// <summary>
		/// Gets the index value of the item that is being drawn.
		/// </summary>
		public int Index
		{
			get{return index;}
		}

		/// <summary>
		/// Gets the font assigned to the item being drawn.
		/// </summary>
		public System.Drawing.Font Font
		{
			get{return font;}
		}

		/// <summary>
		/// Gets the background color of the item that is being drawn.
		/// </summary>
		public Color BackColor
		{
			get{return backColor;}
		}

		/// <summary>
		/// Gets the foreground color of the of the item being drawn.
		/// </summary>
		public Color ForeColor
		{
			get{return foreColor;}
		}

	}

	#endregion

	#region OwnerDrawnList
	/// <summary>
	/// Summary description for OwnerDrawnList.
	/// </summary>
	public abstract class OwnerDrawnList : Control
	{
		/// <summary>
		/// Raised when the DataSource property changes.
		/// </summary>
#if DESIGN
		[Category("Property Changed"),
		Description("Event fired when the value of DataSource property is changed.")]
#endif
		public event EventHandler DataSourceChanged;

		/// <summary>
		/// Raised when the DataMember property changes.
		///</summary>
#if DESIGN
		[Category("Property Changed"),
		Description("Event fired when the value of DisplayMember property is changed.")]
#endif
		public event EventHandler DisplayMemberChanged;
		
		#region private members
		
		/// <summary>
		/// Occurs when the DrawItem has changed
		/// </summary>
#if DESIGN
		[Category("Behavior"),
		Description("Occurs whenever a particular item needs to be painted.")]
#endif
		public event DrawItemEventHandler DrawItem;
		/// <summary>
		/// Occurs when the SelectedIndex property has changed.
		/// </summary>
#if DESIGN
		[Category("Behavior"),
		Description("Event fired when the SelectedIndex property for this control changes.")]
#endif
		public event EventHandler SelectedIndexChanged;
		//Items array
		private IList listItems;
		//Some default item setting
		private int itemHeight = 12;
		private int selectedIndex = -1;
		private int SCROLL_WIDTH = 20;
		//Bitmap for double-buffering
		private Bitmap m_bmpOffscreen;

		private int topIndex;

		private int itemWidth;

		private bool showScrollbar;
		//Vertical scroll
		private VScrollBar vScroll;

		private Image backgroundImage;

		private int origHeight;
		private int prevSelection;

		private object m_dataSource;
		private string m_DataMember;
		private CurrencyManager m_currencyManager;
		private IBindingList m_bindingList;
		internal PropertyDescriptorCollection m_properties;
		private bool m_changingIndex;

		private bool bStopDrawing = false;

		Graphics gxOff = null;

		#endregion

		/// <summary>
		/// Initializes a new instance of the OwnerDrawnList class with default values.
		/// </summary>
		public OwnerDrawnList()
		{
			listItems = new ArrayList();
			//Create a ScrollBar instance
			vScroll = new VScrollBar();
			//Hide it for now
			vScroll.Hide();
			//Hookup into its ValueChanged event
			vScroll.ValueChanged+=new EventHandler(vScrollcroll_ValueChanged);
			//Add Scrollbar
			vScroll.Value = 0;

			this.Controls.Add(vScroll);
			
			SCROLL_WIDTH = vScroll.Width;

			itemWidth = this.Width;

			origHeight = this.Height;

			this.BackColor = Color.Red;
		}

		#region public properties

		//		public VScrollBarEx ScrollBar
		//		{
		//			get{return vScroll;}
		//		}

		/// <summary>
		/// Get/set the ShowScrollbar property.  
		/// </summary>
#if DESIGN
		[Category("Appearance"),
		Description("Specifies whether to show the Scrollbar.")]
#endif
		public bool ShowScrollbar
		{
			get
			{
				return showScrollbar;
			}
			set
			{
				if (showScrollbar != value)
				{
					showScrollbar = value;
					vScroll.Visible = showScrollbar;
					OnResize(null);
				}
			}
		}

		/// <summary>
		/// Gets or sets the index of the first visible item in the OwnerDrawnList. 
		/// </summary>
#if DESIGN
		[Category("Appearance"),
		Description("Specifies the index of the first visible item in the list.")]
#endif
		public int TopIndex
		{
			get
			{
				return topIndex;
			}
			set
			{
				topIndex = value;
				this.Invalidate();
			}
		}


		/// <summary>
		/// Gets the items of the List.  
		/// </summary>
		protected virtual IList BaseItems
		{

            get
            {
                return listItems;
            }
		}


		/// <summary>
		/// Gets or sets a ItemHeight
		/// </summary>
		public virtual int ItemHeight
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

		/// <summary>
		/// Gets or sets the zero-based index of the currently selected item in a OwnerDrawnList.  
		/// </summary>
		public int SelectedIndex
		{
			get
			{
				return this.selectedIndex;
			}
			set
			{
				if (value == (-1))
					return;

				if (this.selectedIndex != value)
				{
					prevSelection = selectedIndex;
					this.selectedIndex = value;
				
					using (Graphics gxTemp = this.CreateGraphics())
					{

						if (prevSelection != -1)
							PaintItem(gxTemp, prevSelection);
			
						PaintItem(gxTemp, selectedIndex);

						DrawBorder(gxTemp);
					}

					this.OnSelectedIndexChanged(EventArgs.Empty);
				}

				if (!this.Focused)
					this.Focus();
			}
		}

		/// <summary>
		/// Gets or sets the background image for the control.
		/// </summary>
		public Image BackgroundImage
		{
			get
			{
				return backgroundImage;
			}
			set
			{
				backgroundImage = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the height of the control.  
		/// </summary>
		public new int Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value; 
				origHeight = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Ensures that the specified item is visible within the control, scrolling the contents of the control if necessary.  
		/// </summary>
		/// <param name="index">The zero-based index of the item to scroll into view.</param>
		public void EnsureVisible(int index)
		{
			if(index < this.vScroll.Value)
			{
				
				this.vScroll.Value = index;
				//this.Invalidate();
			}
			else if(index >= this.vScroll.Value + this.DrawCount)
			{
				this.vScroll.Value = index - this.DrawCount + 1;
				//this.Invalidate();
			}
		}

		private bool ScrollIsNeeded(int index)
		{
			bool result = false;

			if(index < this.vScroll.Value)
			{
				result = true;
			}
			else if(index >= this.vScroll.Value + this.DrawCount)
			{
				result = true;
			}

			return result;
		}

		#endregion

		#region virtual methods
		/// <summary>
		/// Raises the SelectedIndexChanged event.  
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if(this.SelectedIndexChanged != null)
				this.SelectedIndexChanged(this, e);

			if (!m_changingIndex && this.SelectedIndex > 0 && m_currencyManager != null)
			{
				m_currencyManager.Position = this.SelectedIndex;
			}

		}
		/// <summary>
		/// Raises the DrawItem event.  
		/// </summary>
		protected virtual void OnDrawItem(object sender, DrawItemEventArgs e)
		{
			if (this.DrawItem != null)
				this.DrawItem(sender, e);

		}
		#endregion

		#region public methods
		/// <summary>
		/// Forces the control to invalidate its client area and immediately redraw itself and any items in the list.   
		/// </summary>
		public void RefreshItems()
		{
			if (bStopDrawing)
				return;

			if ((this.Width == 0) || (this.Height == 0))
				return;

			if (!this.showScrollbar)
			{
				vScroll.Visible = false;
				this.Height = origHeight;
				if (this.m_bmpOffscreen != null)
					m_bmpOffscreen.Dispose();

				this.m_bmpOffscreen = new Bitmap(this.Width, this.Height);
				
			}
			else
			{
				base.Height = ((int)base.Height / itemHeight) * itemHeight; 
				//How many items are visible
				int viewableItemCount = this.Height / this.ItemHeight;

				this.vScroll.Bounds = new Rectangle(this.Width - SCROLL_WIDTH,
					0,
					SCROLL_WIDTH,
					this.Height);

				// Determine if scrollbars are needed
				if(this.BaseItems.Count > viewableItemCount)
				{
					this.vScroll.Visible = true;
					this.vScroll.LargeChange = viewableItemCount;
					//					if (this.m_bmpOffscreen != null)
					//						m_bmpOffscreen.Dispose();
					//
					//					this.m_bmpOffscreen = new Bitmap(this.Width - SCROLL_WIDTH, this.Height);
				}
				else
				{
					//showScrollbar = false;
					this.vScroll.Visible = false;
					this.vScroll.LargeChange = this.BaseItems.Count;

					//					if (this.m_bmpOffscreen != null)
					//						m_bmpOffscreen.Dispose();
					//
					//					this.m_bmpOffscreen = new Bitmap(this.Width, this.Height);
				}

				this.vScroll.Maximum = this.BaseItems.Count - 1;
			}
			//}
			this.Invalidate();
		}

		#endregion

		#region overrides

		protected override void OnGotFocus(EventArgs e)
		{
			vScroll.Invalidate();
			base.OnGotFocus (e);

			//this.Parent.Refresh();
		}

		protected override void OnResize(EventArgs e)
		{
			
			if ((this.Width == 0) || (this.Height == 0))
				return;

				
			if (!this.showScrollbar)
			{
				vScroll.Visible = false;
				//this.Height = origHeight;
				this.m_bmpOffscreen = new Bitmap(this.Width, this.Height);
				if (gxOff != null)
					gxOff.Dispose();

				gxOff = Graphics.FromImage(m_bmpOffscreen);
				
			}
			else
			{
				//base.Height = ((int)base.Height / itemHeight) * itemHeight; 
				//How many items are visible
				int viewableItemCount = this.Height / this.ItemHeight;

				this.vScroll.Bounds = new Rectangle(this.Width - SCROLL_WIDTH,
					0,
					SCROLL_WIDTH,
					this.Height);

				// Determine if scrollbars are needed
				if(this.BaseItems.Count > viewableItemCount)
				{
					this.vScroll.Visible = true;
					this.vScroll.LargeChange = viewableItemCount;
					
					if (this.m_bmpOffscreen!= null)
						this.m_bmpOffscreen.Dispose();

					//this.m_bmpOffscreen = new Bitmap(this.Width - SCROLL_WIDTH, this.Height);

					this.m_bmpOffscreen = new Bitmap(this.Width, this.Height);


					if (gxOff != null)
						gxOff.Dispose();

					gxOff = Graphics.FromImage(m_bmpOffscreen);
				}
				else
				{
					//showScrollbar = false;
					
					this.vScroll.Visible = false;
					this.vScroll.LargeChange = this.BaseItems.Count;

					if (this.m_bmpOffscreen!=null)
						this.m_bmpOffscreen.Dispose();
					this.m_bmpOffscreen = new Bitmap(this.Width, this.Height);

					if (gxOff != null)
						gxOff.Dispose();

					gxOff = Graphics.FromImage(m_bmpOffscreen);
				}

				this.vScroll.Maximum = this.BaseItems.Count - 1;
			}
			//}
			this.Invalidate();
		}

		//Override this for reducing a flicker
		protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e )
		{
			//Do nothing
		}


		protected override void OnMouseDown(MouseEventArgs e)
		{
			
			base.OnMouseDown(e);

			//get out if there are no items
			if (BaseItems.Count == 0)
				return;

			prevSelection = selectedIndex;

			selectedIndex = this.vScroll.Value + (e.Y / this.ItemHeight);
			
			using (Graphics gxTemp = this.CreateGraphics())
			{

				if (prevSelection != -1)
					PaintItem(gxTemp, prevSelection);
			
				PaintItem(gxTemp, selectedIndex);

				DrawBorder(gxTemp);
			}
			//this.Invalidate();
			if (!this.Focused)
				this.Focus();
		}
	
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);
			if (prevSelection != selectedIndex)
			{
				//this.SelectedIndex = selIndex;
				this.OnSelectedIndexChanged(EventArgs.Empty);

			}

			if (!this.Focused)
				this.Focus();

		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Down:
					if(this.SelectedIndex < this.vScroll.Maximum)
					{
						if (ScrollIsNeeded(this.selectedIndex + 1))
						{
							this.selectedIndex++;
							EnsureVisible(this.selectedIndex);
							this.OnSelectedIndexChanged(EventArgs.Empty);
						}
						else
							this.SelectedIndex++;
					}
					break;
				case Keys.Up:
					if(this.SelectedIndex > this.vScroll.Minimum)
					{
						if (ScrollIsNeeded(this.selectedIndex - 1))
						{
							this.selectedIndex--;	
							EnsureVisible(this.selectedIndex);
							this.OnSelectedIndexChanged(EventArgs.Empty);
						}
						else
							this.SelectedIndex--;
					}
					break;
				case Keys.PageDown:
					this.SelectedIndex = Math.Min(this.vScroll.Maximum, this.SelectedIndex + this.DrawCount);
					EnsureVisible(this.SelectedIndex);
					break;
				case Keys.PageUp:
					this.SelectedIndex = Math.Max(this.vScroll.Minimum, this.SelectedIndex - this.DrawCount);
					EnsureVisible(this.SelectedIndex);
					break;
				case Keys.Home:
					this.SelectedIndex = 0;
					EnsureVisible(this.SelectedIndex);
					break;
				case Keys.End:
					this.SelectedIndex = this.BaseItems.Count - 1;
					EnsureVisible(this.SelectedIndex);
					break;
			}

			base.OnKeyDown(e);
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e )
		{	
			
			if (bStopDrawing)
				return;

			//Create Graphics for off screen painting
			//Graphics gxOff = Graphics.FromImage(m_bmpOffscreen);
			//gxOff = e.Graphics;

			//Paint the background.
			SolidBrush backColorBrush = new SolidBrush(this.BackColor);
			gxOff.FillRectangle(backColorBrush, 0, 0, this.Width, this.Height);
			backColorBrush.Dispose();

			//Draw Background image if available
			if (backgroundImage != null)
				gxOff.DrawImage(backgroundImage, 1, 0);
			
			int drawCount = 0;

			//Get the first visible item
			if (showScrollbar)
			{
				if (vScroll.Value == -1)
					topIndex = 0;
				else
					topIndex = vScroll.Value;
			}
			else
				topIndex = 0;

			//Get the count of the items to draw
			if (showScrollbar)
				drawCount = DrawCount;
			else
				drawCount = BaseItems.Count;

			if (vScroll.Visible)
				itemWidth = this.Width - vScroll.Width;
			else
				itemWidth = this.Width;
				

			for(int index=topIndex;index<drawCount+topIndex;index++)
			{
				PaintItem(gxOff, index);
			}	

			DrawBorder(gxOff);

			//Blit on the control's Graphics
			e.Graphics.DrawImage(m_bmpOffscreen, 0, 0);

			vScroll.Value = vScroll.Value;		
			//gxOff.Dispose();
		}

		//Scroll event
		private void vScrollcroll_ValueChanged(object sender, EventArgs e)
		{
			this.Refresh();
		}

		protected override void OnParentChanged ( System.EventArgs e )
		{
			//			if (!showScrollbar)
			//				this.Height =  listItems.Count * itemHeight;
			//			else //Adjust the height
			//			{
			//				this.Height = ((int)this.Height / itemHeight) * itemHeight; 
			//			}

		}


		#endregion

		#region Helper functions

		private void ChangeSelection(int index)
		{
			//get out if there are no items
			if (BaseItems.Count == 0)
				return;

			prevSelection = selectedIndex;

			selectedIndex = index;
			
			//if (prevSelection == selectedIndex)
			//	return;

			using (Graphics gxTemp = this.CreateGraphics())
			{

				if (prevSelection != -1)
					PaintItem(gxTemp, prevSelection);
			
				PaintItem(gxTemp, selectedIndex);

				DrawBorder(gxTemp);
			}
			//this.Invalidate();
			if (!this.Focused)
				this.Focus();


		}
		//Helper function
		private void DrawBorder(Graphics gr)
		{
			Rectangle rc = this.ClientRectangle;
			rc.Height--;
			rc.Width--;
			//Draw border
			gr.DrawRectangle(new Pen(Color.Black), rc);

		}

		private void PaintItem(Graphics graphics, int Index)
		{
			//			MeasureItemEventArgs measArgs = new MeasureItemEventArgs(graphics, Index, itemHeight);
			//			
			//			//Raise MeasureItemEvent
			//			if (drawMode == DrawMode.OwnerDrawVariable)
			//			{	
			//				OnMeasureItem(this, measArgs);
			//			}
			//			
			//			this.itemHeight = measArgs.ItemHeight;

			//check if it's valid 
			if (BaseItems.Count - 1 < Index)
				return;

			Rectangle itemRect = new Rectangle(0, (Index-topIndex)*this.itemHeight, itemWidth, this.itemHeight);	

			//graphics.Clip = new Region(itemRect);

			DrawItemState state;

			//Set the appropriate selected state
			if (Index ==  selectedIndex)
				state = DrawItemState.Selected;
			else
				state = DrawItemState.None;

			//Prepare Args
			DrawItemEventArgs drawArgs = new DrawItemEventArgs(graphics, this.Font, itemRect, Index, state);
			//Raise drawing event for inheritors
			OnDrawItem(this, drawArgs);
	
		}

		// Calculate how many listItems we can draw given the height of the control.
		internal int DrawCount
		{
			get
			{
				if(this.vScroll.Value + this.vScroll.LargeChange > this.vScroll.Maximum)
					return this.vScroll.Maximum - this.vScroll.Value + 1;
				else
					return this.vScroll.LargeChange;
			}
		}

		#endregion

		/// <summary>
		/// Prevents the control from drawing until the EndUpdate method is called.  
		/// </summary>
		public void BeginUpdate()
		{
			bStopDrawing = true;

		}

		/// <summary>
		/// Resumes drawing of the list view control after drawing is suspended by the BeginUpdate method.  
		/// </summary>
		public void EndUpdate()
		{
			bStopDrawing = false;
			RefreshItems();
		}

		#region Data Binding

		/// <summary>
		/// Gets or sets the data source for this ListBoxEx control.  
		/// </summary>
#if DESIGN
//        [Category("Data")]
//        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
		[Browsable(false)]
#endif
		public object DataSource
		{
			get
			{
				return m_dataSource;
			}
			set
			{
				if (m_dataSource != value)
				{
					// Must be either a list or a list source
					if (value != null && !(value is IList) &&
						!(value is IListSource))
					{
						throw new ArgumentException(
							"Data source must be IList or IListSource");
					}
					m_dataSource = value;

					if (m_dataSource == null)
					{
						m_currencyManager = null;
						m_DataMember = "";
						this.BaseItems.Clear();
						this.RefreshItems();
						OnDataSourceChanged(EventArgs.Empty);
						return;
					}

					SetDataBinding(false);
					OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a string that specifies the property of the data source whose contents you want to display.
		/// </summary>
		/// <value>A <see cref="System.String"/> specifying the name of a property of the object specified by the <see cref="DataSource"/> property.
		/// The default is an empty string ("").</value>
#if DESIGN
        [Category("Data")]
        [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design",
                typeof(System.Drawing.Design.UITypeEditor))]
#endif
		public string DisplayMember
		{
			get
			{
				return m_DataMember;
			}
			set
			{
				if (m_DataMember != value)
				{
					m_DataMember = value;
					SetDataBinding(true);
					OnDisplayMemberChanged(EventArgs.Empty);
				}
			}
		}
		

		/// <summary>
		/// Raises the ListBoxEx.DataSourceChanged event.  
		/// </summary>
		/// <param name="e">The EventArgs that will be passed to any handlers
		/// of the DataSourceChanged event.</param>
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			if (DataSourceChanged != null)
				DataSourceChanged(this, e);
		}



		/// <summary>
		/// Raises the DataMemberChanged event.
		/// </summary>
		/// <param name="e">The EventArgs that will be passed to any handlers
		/// of the DataMemberChanged event.</param>
		protected virtual void OnDisplayMemberChanged(EventArgs e)
		{
			if (DisplayMemberChanged != null)
				DisplayMemberChanged(this, e);
		}


		/// <summary>
		/// Handles binding context changes
		/// </summary>
		/// <param name="e">The EventArgs that will be passed to any handlers
		/// of the BindingContextChanged event.</param>
		protected override void OnBindingContextChanged(EventArgs e)
		{
			base.OnBindingContextChanged (e);

			// If our binding context changes, we must rebind, since we will
			// have a new currency managers, even if we are still bound to the
			// same data source.
			SetDataBinding(false);
		}

		// Attaches the control to a data source.
		private void SetDataBinding(bool reload)
		{
			// The BindingContext is initially null - in general we will not
			// obtain a BindingContext until we are attached to our parent
			// control. (OnParentBindingContextChanged will be called when
			// that happens, so this method will run again. This means it's
			// OK to ignore this call when we don't yet have a BindingContext.)
			if (BindingContext != null)
			{

				// Obtain the CurrencyManager and (if available) IBindingList
				// for the current data source.
				CurrencyManager currencyManager = null;
				IBindingList bindingList = null;

				if (DataSource != null)
				{
					currencyManager = (CurrencyManager)
						BindingContext[DataSource, null];
					if (currencyManager != null)
					{
						bindingList = currencyManager.List as IBindingList;
					}
				}

				// Now see if anything has changed since we last bound to a source.

				bool reloadMetaData = false;
				bool reloadItems = false;
				if (currencyManager != m_currencyManager)
				{
					// We have a new CurrencyManager. If we were previously
					// using another CurrencyManager (i.e. if this is not the
					// first time we've seen one), we'll have some event
					// handlers attached to the old one, so first we must
					// detach those.
					if (m_currencyManager != null)
					{
						//currencyManager.MetaDataChanged -=
						//    new EventHandler(currencyManager_MetaDataChanged);
						currencyManager.PositionChanged -=
							new EventHandler(currencyManager_PositionChanged);
						currencyManager.ItemChanged -=
							new ItemChangedEventHandler(currencyManager_ItemChanged);
					}

					// Now hook up event handlers to the new CurrencyManager.
					// This enables us to detect when the currently selected
					// row changes. It also lets us find out more major changes
					// such as binding to a different list object (this happens
					// when binding to related views - each time the currently
					// selected row in a parent changes, the child list object
					// is replaced with a new object), or even changes in the
					// set of properties.
					m_currencyManager = currencyManager;
					if (currencyManager != null)
					{
						reloadMetaData = true;
						reloadItems = true;
						// currencyManager.MetaDataChanged +=
						//    new EventHandler(currencyManager_MetaDataChanged);
						currencyManager.PositionChanged +=
							new EventHandler(currencyManager_PositionChanged);
						currencyManager.ItemChanged +=
							new ItemChangedEventHandler(currencyManager_ItemChanged);
					}
				}

				if (bindingList != m_bindingList)
				{
					// The IBindingList has changed. If we were previously
					// bound to an IBindingList, detach the event handler.
					if (m_bindingList != null)
					{
						m_bindingList.ListChanged -=
							new ListChangedEventHandler(bindingList_ListChanged);
					}

					// Now hook up a handler to the new IBindingList - this
					// will notify us of any changes in the list. (This is
					// more detailed than the CurrencyManager ItemChanged
					// event. However, we need both, because the only way we
					// know when the list is replaced completely is when the
					// CurrencyManager raises the ItemChanged event.)
					m_bindingList = bindingList;
					if (bindingList != null)
					{
						reloadItems = true;
						bindingList.ListChanged +=
							new ListChangedEventHandler(bindingList_ListChanged);
					}
				}
				

				// If a change occurred that means the set of items to be
				// shown in the list may have changed, reload those.
				if ((reloadItems) || (reload) && (m_currencyManager != null))
				{
					m_properties = m_currencyManager.GetItemProperties();
					LoadItemsFromSource();
				}
			}

		}


		/// <summary>
		/// Adds item to the items collection.
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		protected abstract object BuildItemForRow(object row);


		// Reload list items from the data source.
		private void LoadItemsFromSource()
		{
			// Tell the control not to bother redrawing until we're done
			// adding new items - avoids flicker and speeds things up.
			BeginUpdate();

			try
			{
				// We're about to rebuild the list, so get rid of the current
				// items.
				//Items.Clear();
				BaseItems.Clear();

				// m_bindingList won't be set if the data source doesn't
				// implement IBindingList, so always ask the CurrencyManager
				// for the IList. (IList is all we need to retrieve the rows.)

				IList items = m_currencyManager.List;

				// Add items to list.
				int nItems = items.Count;
				for (int i = 0; i < nItems; ++i)
				{
					//Items.Add(BuildItemForRow(items[i]));
					//BaseItems.Add(BuildItemForRow(items[i]));
					BuildItemForRow(items[i]);
				}
				int index = m_currencyManager.Position;
				if (index != -1)
				{
					SetSelectedIndex(index);
				}
			}
			finally
			{
				// In finally block just in case the data source does something
				// nasty to us - it feels like it might be bad to leave the
				// control in a state where we called BeginUpdate without a
				// corresponding EndUpdate.
				EndUpdate();
			}
		}

		// The CurrencyManager calls this if the data source looks
		// different. We just reload everything.
		private void currencyManager_MetaDataChanged(object sender,EventArgs e)
		{
			//LoadColumnsFromSource();
			LoadItemsFromSource();
		}


		// Called by the CurrencyManager when the currently selected item
		// changes. We update the ListView selection so that we stay in sync
		// with any other controls bound to the same source.
		private void currencyManager_PositionChanged(object sender, EventArgs e)
		{
			SetSelectedIndex(m_currencyManager.Position);
		}

		private void bindingList_ListChanged(object sender,
			ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
					// Well, usually fine-grained... The whole list has changed
					// utterly, so reload it.

				case ListChangedType.Reset:
					LoadItemsFromSource();
					break;


					// A single item has changed, so just rebuild that.

				case ListChangedType.ItemChanged:
					object changedRow = m_currencyManager.List[e.NewIndex];
					BeginUpdate();
					BaseItems[e.NewIndex] = BuildItemForRow(changedRow);
					EndUpdate();
					break;


					// A new item has appeared, so add that.

				case ListChangedType.ItemAdded:
					object newRow = m_currencyManager.List[e.NewIndex];
					// We get this event twice if certain grid controls
					// are used to add a new row to a datatable: once when
					// the editing of a new row begins, and once again when
					// that editing commits. (If the user cancels the creation
					// of the new row, we never see the second creation.)
					// We detect this by seeing if this is a view on a
					// row in a DataTable, and if it is, testing to see if
					// it's a new row under creation.
					DataRowView drv = newRow as DataRowView;
					if (drv == null || !drv.IsNew)
					{
						// Either we're not dealing with a view on a data
						// table, or this is the commit notification. Either
						// way, this is the final notification, so we want
						// to add the new row now!
						BeginUpdate();
						//BaseItems.Insert(e.NewIndex, BuildItemForRow(newRow));
						BuildItemForRow(newRow);
						EndUpdate();
					}
					break;


					// An item has gone away.

				case ListChangedType.ItemDeleted:
					if (e.NewIndex < BaseItems.Count)
					{
						BaseItems.RemoveAt(e.NewIndex);
					}
					break;


					// An item has changed its index.

				case ListChangedType.ItemMoved:
					BeginUpdate();
					object moving = BaseItems[e.OldIndex];
					BaseItems.Insert(e.NewIndex, moving);
					EndUpdate();
					break;


					// Something has changed in the metadata. (This control is
					// too lazy to deal with this in a fine-grained fashion,
					// mostly because the author has never seen this event
					// occur... So we deal with it the simple way: reload
					// everything.)

				case ListChangedType.PropertyDescriptorAdded:
				case ListChangedType.PropertyDescriptorChanged:
				case ListChangedType.PropertyDescriptorDeleted:
					//LoadColumnsFromSource();
					LoadItemsFromSource();
					break;
			}
		}

		private void SetSelectedIndex(int index)
		{
			if (!m_changingIndex)
			{
				m_changingIndex = true;
				//SelectedItems.Clear();
				if (BaseItems.Count > index)
				{
					//item.Selected = true;
					ChangeSelection(index);
					this.EnsureVisible(index);
					OnSelectedIndexChanged(EventArgs.Empty);
				}
				m_changingIndex = false;
			}
		}


		//		protected override void OnSelectedIndexChanged(EventArgs e)
		//		{
		//			base.OnSelectedIndexChanged (e);
		//
		//			// Did this originate from us, or was this caused by the
		//			// CurrencyManager in the first place. If we're sure it was us,
		//			// and there is actually a selected item (this event is also raised
		//			// when transitioning to the 'no items selected' state), and we
		//			// definitely do have a CurrencyManager (i.e. we are actually bound
		//			// to a data source), then we notify the CurrencyManager.
		//
		//			if (!m_changingIndex && this.SelectedIndex > 0 && m_currencyManager != null)
		//			{
		//				m_currencyManager.Position = this.SelectedIndex;
		//			}
		//		}
		
		private void currencyManager_ItemChanged(object sender, ItemChangedEventArgs e)
		{
			// An index of -1 seems to be the indication that lots has
			// changed. (I've not found where it says this in the
			// documentation - I got this information from a comment in Mark
			// Boulter's code.) So we always reload all items from the
			// source when this happens.
			if (e.Index == -1)
			{
				// ...but before we reload all items from source, we also look
				// to see if the list we're supposed to bind to has changed
				// since last time, and if it has, reattach our event handlers.

				if (!m_bindingList.Equals(m_currencyManager.List))
				{
					m_bindingList.ListChanged -=
						new ListChangedEventHandler(bindingList_ListChanged);
					m_bindingList = m_currencyManager.List as IBindingList;
					if (m_bindingList != null)
					{
						m_bindingList.ListChanged +=
							new ListChangedEventHandler(bindingList_ListChanged);
					}
				}
				LoadItemsFromSource();                
			}
		}
		#endregion


		
	}

	#endregion


}
