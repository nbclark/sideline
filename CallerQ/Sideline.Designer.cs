namespace MobileSrc.Sideline
{
    partial class Sideline
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sideline));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this._detailsMenuItem = new System.Windows.Forms.MenuItem();
            this._menuMenuItem = new System.Windows.Forms.MenuItem();
            this._addCallMenuItem = new System.Windows.Forms.MenuItem();
            this._deleteCallMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this._filterMenuItem = new System.Windows.Forms.MenuItem();
            this._showActiveMenuItem = new System.Windows.Forms.MenuItem();
            this._showOverdueMenuItem = new System.Windows.Forms.MenuItem();
            this._showCompletedMenuItem = new System.Windows.Forms.MenuItem();
            this._sortMenuItem = new System.Windows.Forms.MenuItem();
            this._priorityMenuItem = new System.Windows.Forms.MenuItem();
            this._dueDateMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this._settingsMenuItem = new System.Windows.Forms.MenuItem();
            this._exitMenuItem = new System.Windows.Forms.MenuItem();
            this._registerMenuItem = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new VerticalStretchPictureBox();
            this._contactListBox = new MobileSrc.Sideline.ContactListBox();
            this._viewMenuItem = new System.Windows.Forms.MenuItem();
            this._smallMenuItem = new TaggedMenuItem();
            this._mediumMenuItem = new TaggedMenuItem();
            this._largeMenuItem = new TaggedMenuItem();
            this._addButton = new ImageButton();
            this._removeButton = new ImageButton();
            this._detailsButton = new ImageButton();
            this._contactListContextMenu = new System.Windows.Forms.ContextMenu();
            this._contactViewDetailsMenuItem = new System.Windows.Forms.MenuItem();
            this._contactDeleteMenuItem = new System.Windows.Forms.MenuItem();
            this._contactViewCardMenuItem = new System.Windows.Forms.MenuItem();
            this._contactSeperatorMenuItem = new System.Windows.Forms.MenuItem();
            this._contactCompleteMenuItem = new System.Windows.Forms.MenuItem();

            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this._detailsMenuItem);
            this.mainMenu1.MenuItems.Add(this._menuMenuItem);
            // 
            // _detailsMenuItem
            // 
            this._detailsMenuItem.Text = "Details";
            this._detailsMenuItem.Click += new System.EventHandler(this._detailsMenuItem_Click);
            // 
            // _menuMenuItem
            // 
            this._menuMenuItem.MenuItems.Add(this._addCallMenuItem);
            this._menuMenuItem.MenuItems.Add(this._deleteCallMenuItem);
            this._menuMenuItem.MenuItems.Add(this.menuItem2);
            this._menuMenuItem.MenuItems.Add(this._filterMenuItem);
            this._menuMenuItem.MenuItems.Add(this._sortMenuItem);
            this._menuMenuItem.MenuItems.Add(this.menuItem1);
            this._menuMenuItem.MenuItems.Add(this._viewMenuItem);
            this._menuMenuItem.MenuItems.Add(this._settingsMenuItem);
            this._menuMenuItem.MenuItems.Add(this._registerMenuItem);
            this._menuMenuItem.MenuItems.Add(this._exitMenuItem);
            this._menuMenuItem.Text = "Menu";
            // 
            // _addCallMenuItem
            // 
            this._addCallMenuItem.Text = "Add Call";
            this._addCallMenuItem.Click += new System.EventHandler(this._addCallMenuItem_Click);
            // 
            // _deleteCallMenuItem
            // 
            this._deleteCallMenuItem.Text = "Delete Call";
            this._deleteCallMenuItem.Click += new System.EventHandler(this._deleteCallMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // _filterMenuItem
            // 
            this._filterMenuItem.MenuItems.Add(this._showActiveMenuItem);
            this._filterMenuItem.MenuItems.Add(this._showOverdueMenuItem);
            this._filterMenuItem.MenuItems.Add(this._showCompletedMenuItem);
            this._filterMenuItem.Text = "Filter";
            // 
            // _showActiveMenuItem
            // 
            this._showActiveMenuItem.Checked = true;
            this._showActiveMenuItem.Text = "Show Active";
            this._showActiveMenuItem.Click += new System.EventHandler(this.filterMenuItem_Click);
            // 
            // _showOverdueMenuItem
            // 
            this._showOverdueMenuItem.Checked = true;
            this._showOverdueMenuItem.Text = "Show Overdue";
            this._showOverdueMenuItem.Click += new System.EventHandler(this.filterMenuItem_Click);
            // 
            // _showCompletedMenuItem
            // 
            this._showCompletedMenuItem.Text = "Show Completed";
            this._showCompletedMenuItem.Click += new System.EventHandler(this.filterMenuItem_Click);
            // 
            // _sortMenuItem
            // 
            this._sortMenuItem.MenuItems.Add(this._priorityMenuItem);
            this._sortMenuItem.MenuItems.Add(this._dueDateMenuItem);
            this._sortMenuItem.Text = "Sort";
            // 
            // _priorityMenuItem
            // 
            this._priorityMenuItem.Text = "Priority";
            this._priorityMenuItem.Click += new System.EventHandler(this._priorityMenuItem_Click);
            // 
            // _dueDateMenuItem
            // 
            this._dueDateMenuItem.Checked = true;
            this._dueDateMenuItem.Text = "Due Date";
            this._dueDateMenuItem.Click += new System.EventHandler(this._dueDateMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "-";
            // 
            // _settingsMenuItem
            // 
            this._settingsMenuItem.Text = "Settings";
            this._settingsMenuItem.Click += new System.EventHandler(_settingsMenuItem_Click);
            // 
            // _registerMenuItem
            // 
            this._registerMenuItem.Text = "Register";
            this._registerMenuItem.Click += new System.EventHandler(this._registerMenuItem_Click);
            // 
            // _exitMenuItem
            // 
            this._exitMenuItem.Text = "Exit";
            this._exitMenuItem.Click += new System.EventHandler(this._exitMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this._removeButton);
            this.panel1.Controls.Add(this._addButton);
            this.panel1.Controls.Add(this._detailsButton);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 476);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 80);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = Properties.Resources.SidelineLogo_Inverted;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(238, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            //
            // _addButton
            //
            this._addButton.BackColor = System.Drawing.Color.Black;
            this._addButton.Click += new System.EventHandler(_addCallMenuItem_Click);
            this._addButton.TabIndex = 1;

            this._addButton.Image = Properties.Resources.add;
            this._addButton.PushImage = Properties.Resources.add_hover;
            this._addButton.IsTransparent = true;

            this._addButton.Size = new System.Drawing.Size(80, 80);
            this._addButton.Dock = System.Windows.Forms.DockStyle.Left;
            this._addButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            //
            // _removeButton
            //
            this._removeButton.BackColor = System.Drawing.Color.Black;
            this._removeButton.Click += new System.EventHandler(_deleteCallMenuItem_Click);
            this._removeButton.TabIndex = 2;

            this._removeButton.Image = Properties.Resources.remove;
            this._removeButton.PushImage = Properties.Resources.remove_hover;
            this._removeButton.IsTransparent = true;

            this._removeButton.Size = new System.Drawing.Size(80, 80);
            this._removeButton.Dock = System.Windows.Forms.DockStyle.Left;
            this._removeButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            //
            // _detailsButton
            //
            this._detailsButton.BackColor = System.Drawing.Color.Black;
            this._detailsButton.Click += new System.EventHandler(_detailsButton_Click);
            this._detailsButton.TabIndex = 0;

            this._detailsButton.Image = Properties.Resources.properties;
            this._detailsButton.PushImage = Properties.Resources.properties_hover;
            this._detailsButton.IsTransparent = true;

            this._detailsButton.Size = new System.Drawing.Size(80, 80);
            this._detailsButton.Dock = System.Windows.Forms.DockStyle.Left;
            this._detailsButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            // 
            // _contactListBox
            // 
            this._contactListBox.BackColor = System.Drawing.SystemColors.Window;
            this._contactListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contactListBox.EmptySetText = null;
            this._contactListBox.Filter = null;
            this._contactListBox.Location = new System.Drawing.Point(0, 0);
            this._contactListBox.Name = "_contactListBox";
            this._contactListBox.SelectedIndex = -1;
            this._contactListBox.Size = new System.Drawing.Size(480, 476);
            this._contactListBox.SortDirection = System.ComponentModel.ListSortDirection.Ascending;
            this._contactListBox.SortProperty = "";
            this._contactListBox.TabIndex = 3;
            this._contactListBox.Text = "contactListBox1";
            this._contactListBox.ContextMenu = _contactListContextMenu;
            // 
            // _viewMenuItem
            // 
            this._viewMenuItem.MenuItems.Add(this._smallMenuItem);
            this._viewMenuItem.MenuItems.Add(this._mediumMenuItem);
            this._viewMenuItem.MenuItems.Add(this._largeMenuItem);
            this._viewMenuItem.Text = "View";
            // 
            // _smallMenuItem
            // 
            this._smallMenuItem.Text = "Small";
            this._smallMenuItem.Tag = TileSizeMode.Small;
            this._smallMenuItem.Click += new System.EventHandler(this._viewMenuItem_Click);
            // 
            // _mediumMenuItem
            // 
            this._mediumMenuItem.Text = "Medium";
            this._mediumMenuItem.Tag = TileSizeMode.Medium;
            this._mediumMenuItem.Click += new System.EventHandler(this._viewMenuItem_Click);
            // 
            // _largeMenuItem
            // 
            this._largeMenuItem.Text = "Large";
            this._largeMenuItem.Tag = TileSizeMode.Large;
            this._largeMenuItem.Click += new System.EventHandler(this._viewMenuItem_Click);
            // 
            // _contactListContextMenu
            // 
            this._contactListContextMenu.MenuItems.Add(this._contactViewDetailsMenuItem);
            this._contactListContextMenu.MenuItems.Add(this._contactViewCardMenuItem);
            this._contactListContextMenu.MenuItems.Add(this._contactSeperatorMenuItem);
            this._contactListContextMenu.MenuItems.Add(this._contactCompleteMenuItem);
            this._contactListContextMenu.MenuItems.Add(this._contactDeleteMenuItem);
            // 
            // _contactViewDetailsMenuItem
            // 
            this._contactViewDetailsMenuItem.Text = "View Call Details";
            this._contactViewDetailsMenuItem.Click += new System.EventHandler(_detailsMenuItem_Click);
            // 
            // _contactDeleteMenuItem
            // 
            this._contactDeleteMenuItem.Text = "Delete Call";
            this._contactDeleteMenuItem.Click += new System.EventHandler(_deleteCallMenuItem_Click);
            // 
            // _contactViewCardMenuItem
            // 
            this._contactViewCardMenuItem.Text = "View Contact Card";
            this._contactViewCardMenuItem.Click += new System.EventHandler(_contactViewCardMenuItem_Click);
            // 
            // _contactSeperatorMenuItem
            // 
            this._contactSeperatorMenuItem.Text = "-";
            // 
            // _contactCompleteMenuItem
            // 
            this._contactCompleteMenuItem.Text = "Complete Call";
            this._contactCompleteMenuItem.Click += new System.EventHandler(_contactCompleteMenuItem_Click);
            // 
            // Sideline
            // 
            this.AutoScaleDimensions = Properties.Resources.ScaleDimensions;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 536);
            this.Controls.Add(this._contactListBox);
            this.Controls.Add(this.panel1);
            this.Icon = Properties.Resources.sideline;
            this.Location = new System.Drawing.Point(0, 52);
            this.Menu = this.mainMenu1;
            this.Name = "Sideline";
            this.Text = "Sideline";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem _detailsMenuItem;
        private System.Windows.Forms.MenuItem _menuMenuItem;
        private System.Windows.Forms.MenuItem _filterMenuItem;
        private System.Windows.Forms.MenuItem _showActiveMenuItem;
        private System.Windows.Forms.MenuItem _showCompletedMenuItem;
        private System.Windows.Forms.MenuItem _sortMenuItem;
        private System.Windows.Forms.MenuItem _priorityMenuItem;
        private System.Windows.Forms.MenuItem _dueDateMenuItem;
        private System.Windows.Forms.MenuItem _registerMenuItem;
        private System.Windows.Forms.MenuItem _exitMenuItem;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem _settingsMenuItem;
        private System.Windows.Forms.MenuItem _showOverdueMenuItem;
        private System.Windows.Forms.Panel panel1;
        private VerticalStretchPictureBox pictureBox1;
        private ContactListBox _contactListBox;
        private System.Windows.Forms.MenuItem _addCallMenuItem;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem _deleteCallMenuItem;
        private System.Windows.Forms.MenuItem _viewMenuItem;
        private TaggedMenuItem _smallMenuItem;
        private TaggedMenuItem _mediumMenuItem;
        private TaggedMenuItem _largeMenuItem;
        private ImageButton _addButton;
        private ImageButton _removeButton;
        private ImageButton _detailsButton;

        private System.Windows.Forms.ContextMenu _contactListContextMenu;
        private System.Windows.Forms.MenuItem _contactViewDetailsMenuItem;
        private System.Windows.Forms.MenuItem _contactViewCardMenuItem;
        private System.Windows.Forms.MenuItem _contactSeperatorMenuItem;
        private System.Windows.Forms.MenuItem _contactCompleteMenuItem;
        private System.Windows.Forms.MenuItem _contactDeleteMenuItem;
    }
}

