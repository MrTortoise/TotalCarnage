namespace TileEditor
{
	partial class GroupFileManager
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupFileManager));
			this.treeZipDirectories = new System.Windows.Forms.TreeView();
			this.label2 = new System.Windows.Forms.Label();
			this.ListZipFiles = new System.Windows.Forms.ListView();
			this.label3 = new System.Windows.Forms.Label();
			this.btnAddFile = new System.Windows.Forms.Button();
			this.btnDeleteFile = new System.Windows.Forms.Button();
			this.btnCopyTo = new System.Windows.Forms.Button();
			this.btnCreateDirectory = new System.Windows.Forms.Button();
			this.btnCopyFile = new System.Windows.Forms.Button();
			this.btnRenameFile = new System.Windows.Forms.Button();
			this.btnSaveChanges = new System.Windows.Forms.Button();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectExistingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.txtZipFile = new System.Windows.Forms.ToolStripTextBox();
			this.directoryListView1 = new Custom.Components.DirectoryListView();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeZipDirectories
			// 
			this.treeZipDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.treeZipDirectories.Location = new System.Drawing.Point(227, 70);
			this.treeZipDirectories.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.treeZipDirectories.Name = "treeZipDirectories";
			this.treeZipDirectories.Size = new System.Drawing.Size(207, 383);
			this.treeZipDirectories.TabIndex = 9;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(223, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(131, 17);
			this.label2.TabIndex = 10;
			this.label2.Text = "Directory Structure:";
			// 
			// ListZipFiles
			// 
			this.ListZipFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ListZipFiles.GridLines = true;
			this.ListZipFiles.Location = new System.Drawing.Point(441, 70);
			this.ListZipFiles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ListZipFiles.Name = "ListZipFiles";
			this.ListZipFiles.Size = new System.Drawing.Size(199, 383);
			this.ListZipFiles.TabIndex = 11;
			this.ListZipFiles.UseCompatibleStateImageBehavior = false;
			this.ListZipFiles.View = System.Windows.Forms.View.List;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(437, 43);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 17);
			this.label3.TabIndex = 12;
			this.label3.Text = "Files:";
			// 
			// btnAddFile
			// 
			this.btnAddFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddFile.Location = new System.Drawing.Point(647, 70);
			this.btnAddFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnAddFile.Name = "btnAddFile";
			this.btnAddFile.Size = new System.Drawing.Size(127, 30);
			this.btnAddFile.TabIndex = 13;
			this.btnAddFile.Text = "Add Files";
			this.btnAddFile.UseVisualStyleBackColor = true;
			// 
			// btnDeleteFile
			// 
			this.btnDeleteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteFile.Location = new System.Drawing.Point(648, 224);
			this.btnDeleteFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnDeleteFile.Name = "btnDeleteFile";
			this.btnDeleteFile.Size = new System.Drawing.Size(127, 30);
			this.btnDeleteFile.TabIndex = 14;
			this.btnDeleteFile.Text = "Delete Files";
			this.btnDeleteFile.UseVisualStyleBackColor = true;
			// 
			// btnCopyTo
			// 
			this.btnCopyTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopyTo.Location = new System.Drawing.Point(648, 267);
			this.btnCopyTo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnCopyTo.Name = "btnCopyTo";
			this.btnCopyTo.Size = new System.Drawing.Size(127, 30);
			this.btnCopyTo.TabIndex = 15;
			this.btnCopyTo.Text = "Copy to Directory";
			this.btnCopyTo.UseVisualStyleBackColor = true;
			// 
			// btnCreateDirectory
			// 
			this.btnCreateDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCreateDirectory.Location = new System.Drawing.Point(647, 106);
			this.btnCreateDirectory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnCreateDirectory.Name = "btnCreateDirectory";
			this.btnCreateDirectory.Size = new System.Drawing.Size(127, 30);
			this.btnCreateDirectory.TabIndex = 16;
			this.btnCreateDirectory.Text = "Create Directory";
			this.btnCreateDirectory.UseVisualStyleBackColor = true;
			// 
			// btnCopyFile
			// 
			this.btnCopyFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopyFile.Location = new System.Drawing.Point(648, 142);
			this.btnCopyFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnCopyFile.Name = "btnCopyFile";
			this.btnCopyFile.Size = new System.Drawing.Size(125, 30);
			this.btnCopyFile.TabIndex = 17;
			this.btnCopyFile.Text = "Copy File";
			this.btnCopyFile.UseVisualStyleBackColor = true;
			// 
			// btnRenameFile
			// 
			this.btnRenameFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRenameFile.Location = new System.Drawing.Point(648, 177);
			this.btnRenameFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnRenameFile.Name = "btnRenameFile";
			this.btnRenameFile.Size = new System.Drawing.Size(125, 30);
			this.btnRenameFile.TabIndex = 18;
			this.btnRenameFile.Text = "Rename File";
			this.btnRenameFile.UseVisualStyleBackColor = true;
			// 
			// btnSaveChanges
			// 
			this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveChanges.Location = new System.Drawing.Point(651, 347);
			this.btnSaveChanges.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnSaveChanges.Name = "btnSaveChanges";
			this.btnSaveChanges.Size = new System.Drawing.Size(125, 55);
			this.btnSaveChanges.TabIndex = 19;
			this.btnSaveChanges.Text = "SAVE CHANGES";
			this.btnSaveChanges.UseVisualStyleBackColor = true;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.txtZipFile});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(781, 27);
			this.toolStrip1.TabIndex = 22;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewToolStripMenuItem,
            this.selectExistingToolStripMenuItem,
            this.loadFileToolStripMenuItem});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(45, 24);
			this.toolStripDropDownButton1.Text = "File";
			// 
			// createNewToolStripMenuItem
			// 
			this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
			this.createNewToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
			this.createNewToolStripMenuItem.Text = "Create New";
			this.createNewToolStripMenuItem.Click += new System.EventHandler(this.createNewToolStripMenuItem_Click);
			// 
			// selectExistingToolStripMenuItem
			// 
			this.selectExistingToolStripMenuItem.Name = "selectExistingToolStripMenuItem";
			this.selectExistingToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
			this.selectExistingToolStripMenuItem.Text = "Select Existing";
			this.selectExistingToolStripMenuItem.Click += new System.EventHandler(this.selectExistingToolStripMenuItem_Click);
			// 
			// loadFileToolStripMenuItem
			// 
			this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
			this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(173, 24);
			this.loadFileToolStripMenuItem.Text = "Load File";
			this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
			// 
			// txtZipFile
			// 
			this.txtZipFile.Name = "txtZipFile";
			this.txtZipFile.Size = new System.Drawing.Size(132, 27);
			// 
			// directoryListView1
			// 
			this.directoryListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.directoryListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.directoryListView1.Location = new System.Drawing.Point(0, 34);
			this.directoryListView1.Margin = new System.Windows.Forms.Padding(5);
			this.directoryListView1.Name = "directoryListView1";
			this.directoryListView1.Size = new System.Drawing.Size(219, 419);
			this.directoryListView1.TabIndex = 20;
			// 
			// GroupFileManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(781, 465);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.directoryListView1);
			this.Controls.Add(this.btnSaveChanges);
			this.Controls.Add(this.btnRenameFile);
			this.Controls.Add(this.btnCopyFile);
			this.Controls.Add(this.btnCreateDirectory);
			this.Controls.Add(this.btnCopyTo);
			this.Controls.Add(this.btnDeleteFile);
			this.Controls.Add(this.btnAddFile);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ListZipFiles);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.treeZipDirectories);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "GroupFileManager";
			this.Text = "Group File Manager";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView treeZipDirectories;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView ListZipFiles;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnAddFile;
		private System.Windows.Forms.Button btnDeleteFile;
		private System.Windows.Forms.Button btnCopyTo;
		private System.Windows.Forms.Button btnCreateDirectory;
		private System.Windows.Forms.Button btnCopyFile;
		private System.Windows.Forms.Button btnRenameFile;
		private System.Windows.Forms.Button btnSaveChanges;
		private Custom.Components.DirectoryListView directoryListView1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripTextBox txtZipFile;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectExistingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
	}
}