namespace TileEditor
{
    partial class ContentGrabber
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
            this.targetFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.targetPathTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.CopyDirectory = new System.Windows.Forms.Button();
            this.copyFile = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SourceSize = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SourceDims = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SourceExtension = new System.Windows.Forms.Label();
            this.labelSourceFullName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SourceImage = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewSource = new System.Windows.Forms.ListView();
            this.treeViewSource = new System.Windows.Forms.TreeView();
            this.deleteTargetFiles = new System.Windows.Forms.Button();
            this.targetImage = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TargetListView = new System.Windows.Forms.ListView();
            this.label9 = new System.Windows.Forms.Label();
            this.TargetDirectoryTV = new System.Windows.Forms.TreeView();
            this.CreateDirectoryButton = new System.Windows.Forms.Button();
            this.CreateDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetImage)).BeginInit();
            this.SuspendLayout();
            // 
            // targetFolderBrowserDialog
            // 
            this.targetFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // targetPathTextBox
            // 
            this.targetPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.targetPathTextBox.Cursor = System.Windows.Forms.Cursors.No;
            this.targetPathTextBox.Enabled = false;
            this.targetPathTextBox.Location = new System.Drawing.Point(88, 24);
            this.targetPathTextBox.Name = "targetPathTextBox";
            this.targetPathTextBox.Size = new System.Drawing.Size(855, 22);
            this.targetPathTextBox.TabIndex = 1;
            this.targetPathTextBox.TextChanged += new System.EventHandler(this.targetPathTextBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.targetPathTextBox);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(14, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(957, 50);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select target directory";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1010, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 763);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1010, 25);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 36);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.CopyDirectory);
            this.splitContainer1.Panel1.Controls.Add(this.copyFile);
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel1.Controls.Add(this.labelSourceFullName);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.SourceImage);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.listViewSource);
            this.splitContainer1.Panel1.Controls.Add(this.treeViewSource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CreateDirectoryTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.CreateDirectoryButton);
            this.splitContainer1.Panel2.Controls.Add(this.TargetDirectoryTV);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.deleteTargetFiles);
            this.splitContainer1.Panel2.Controls.Add(this.targetImage);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.TargetListView);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer1.Size = new System.Drawing.Size(986, 724);
            this.splitContainer1.SplitterDistance = 299;
            this.splitContainer1.TabIndex = 3;
            // 
            // CopyDirectory
            // 
            this.CopyDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyDirectory.Location = new System.Drawing.Point(770, 173);
            this.CopyDirectory.Name = "CopyDirectory";
            this.CopyDirectory.Size = new System.Drawing.Size(200, 29);
            this.CopyDirectory.TabIndex = 12;
            this.CopyDirectory.Text = "Copy Directories";
            this.CopyDirectory.UseVisualStyleBackColor = true;
            this.CopyDirectory.Click += new System.EventHandler(this.CopyDirectory_Click);
            // 
            // copyFile
            // 
            this.copyFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.copyFile.Location = new System.Drawing.Point(771, 138);
            this.copyFile.Name = "copyFile";
            this.copyFile.Size = new System.Drawing.Size(200, 29);
            this.copyFile.TabIndex = 11;
            this.copyFile.Text = "Copy Files";
            this.copyFile.UseVisualStyleBackColor = true;
            this.copyFile.Click += new System.EventHandler(this.copyFile_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.SourceSize, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SourceDims, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.SourceExtension, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(771, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // SourceSize
            // 
            this.SourceSize.AutoSize = true;
            this.SourceSize.Location = new System.Drawing.Point(103, 0);
            this.SourceSize.Name = "SourceSize";
            this.SourceSize.Size = new System.Drawing.Size(46, 17);
            this.SourceSize.TabIndex = 11;
            this.SourceSize.Text = "label7";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "dims:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "size:";
            // 
            // SourceDims
            // 
            this.SourceDims.AutoSize = true;
            this.SourceDims.Location = new System.Drawing.Point(103, 25);
            this.SourceDims.Name = "SourceDims";
            this.SourceDims.Size = new System.Drawing.Size(46, 17);
            this.SourceDims.TabIndex = 6;
            this.SourceDims.Text = "label5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "extension";
            // 
            // SourceExtension
            // 
            this.SourceExtension.AutoSize = true;
            this.SourceExtension.Location = new System.Drawing.Point(103, 50);
            this.SourceExtension.Name = "SourceExtension";
            this.SourceExtension.Size = new System.Drawing.Size(46, 17);
            this.SourceExtension.TabIndex = 13;
            this.SourceExtension.Text = "label8";
            // 
            // labelSourceFullName
            // 
            this.labelSourceFullName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSourceFullName.AutoSize = true;
            this.labelSourceFullName.Location = new System.Drawing.Point(155, 271);
            this.labelSourceFullName.Name = "labelSourceFullName";
            this.labelSourceFullName.Size = new System.Drawing.Size(30, 17);
            this.labelSourceFullName.TabIndex = 8;
            this.labelSourceFullName.Text = "null";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Selected Filename:";
            // 
            // SourceImage
            // 
            this.SourceImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SourceImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SourceImage.Location = new System.Drawing.Point(452, 32);
            this.SourceImage.MinimumSize = new System.Drawing.Size(128, 128);
            this.SourceImage.Name = "SourceImage";
            this.SourceImage.Size = new System.Drawing.Size(313, 232);
            this.SourceImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SourceImage.TabIndex = 4;
            this.SourceImage.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(242, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Choose Source File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Choose Source Directory";
            // 
            // listViewSource
            // 
            this.listViewSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewSource.Location = new System.Drawing.Point(235, 32);
            this.listViewSource.Name = "listViewSource";
            this.listViewSource.ShowGroups = false;
            this.listViewSource.Size = new System.Drawing.Size(211, 232);
            this.listViewSource.TabIndex = 1;
            this.listViewSource.UseCompatibleStateImageBehavior = false;
            this.listViewSource.View = System.Windows.Forms.View.List;
            this.listViewSource.SelectedIndexChanged += new System.EventHandler(this.listViewSource_SelectedIndexChanged);
            // 
            // treeViewSource
            // 
            this.treeViewSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewSource.Location = new System.Drawing.Point(14, 32);
            this.treeViewSource.Name = "treeViewSource";
            this.treeViewSource.Size = new System.Drawing.Size(215, 232);
            this.treeViewSource.TabIndex = 0;
            this.treeViewSource.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSource_AfterSelect);
            this.treeViewSource.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSource_AfterExpand);
            // 
            // deleteTargetFiles
            // 
            this.deleteTargetFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteTargetFiles.Location = new System.Drawing.Point(770, 392);
            this.deleteTargetFiles.Name = "deleteTargetFiles";
            this.deleteTargetFiles.Size = new System.Drawing.Size(196, 26);
            this.deleteTargetFiles.TabIndex = 7;
            this.deleteTargetFiles.Text = "Delete Files";
            this.deleteTargetFiles.UseVisualStyleBackColor = true;
            this.deleteTargetFiles.Click += new System.EventHandler(this.deleteTargetFiles_Click);
            // 
            // targetImage
            // 
            this.targetImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.targetImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.targetImage.Location = new System.Drawing.Point(452, 88);
            this.targetImage.MinimumSize = new System.Drawing.Size(128, 128);
            this.targetImage.Name = "targetImage";
            this.targetImage.Size = new System.Drawing.Size(313, 330);
            this.targetImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.targetImage.TabIndex = 6;
            this.targetImage.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 17);
            this.label8.TabIndex = 5;
            this.label8.Text = "Choose Source File";
            // 
            // TargetListView
            // 
            this.TargetListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.TargetListView.Location = new System.Drawing.Point(235, 88);
            this.TargetListView.Name = "TargetListView";
            this.TargetListView.ShowGroups = false;
            this.TargetListView.Size = new System.Drawing.Size(211, 330);
            this.TargetListView.TabIndex = 4;
            this.TargetListView.UseCompatibleStateImageBehavior = false;
            this.TargetListView.View = System.Windows.Forms.View.List;
            this.TargetListView.SelectedIndexChanged += new System.EventHandler(this.targetListView_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(173, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "Target Direcotry Structure";
            // 
            // TargetDirectoryTV
            // 
            this.TargetDirectoryTV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.TargetDirectoryTV.Location = new System.Drawing.Point(14, 89);
            this.TargetDirectoryTV.Name = "TargetDirectoryTV";
            this.TargetDirectoryTV.Size = new System.Drawing.Size(215, 329);
            this.TargetDirectoryTV.TabIndex = 10;
            this.TargetDirectoryTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TargetDirectoryTV_AfterSelect);
            this.TargetDirectoryTV.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TargetDirectoryTV_AfterExpand);
            // 
            // CreateDirectoryButton
            // 
            this.CreateDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateDirectoryButton.Location = new System.Drawing.Point(777, 116);
            this.CreateDirectoryButton.Name = "CreateDirectoryButton";
            this.CreateDirectoryButton.Size = new System.Drawing.Size(196, 26);
            this.CreateDirectoryButton.TabIndex = 11;
            this.CreateDirectoryButton.Text = "Create Directory";
            this.CreateDirectoryButton.UseVisualStyleBackColor = true;
            this.CreateDirectoryButton.Click += new System.EventHandler(this.CreateDirectoryButton_Click);
            // 
            // CreateDirectoryTextBox
            // 
            this.CreateDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateDirectoryTextBox.Location = new System.Drawing.Point(777, 88);
            this.CreateDirectoryTextBox.Name = "CreateDirectoryTextBox";
            this.CreateDirectoryTextBox.Size = new System.Drawing.Size(192, 22);
            this.CreateDirectoryTextBox.TabIndex = 12;
            // 
            // ContentGrabber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 788);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ContentGrabber";
            this.Text = "ContentGrabber";
            this.Load += new System.EventHandler(this.ContentGrabber_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog targetFolderBrowserDialog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox targetPathTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewSource;
        private System.Windows.Forms.ListView listViewSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox SourceImage;
        private System.Windows.Forms.Label SourceDims;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelSourceFullName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label SourceSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label SourceExtension;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView TargetListView;
        private System.Windows.Forms.Button copyFile;
        private System.Windows.Forms.PictureBox targetImage;
        private System.Windows.Forms.Button deleteTargetFiles;
        private System.Windows.Forms.Button CopyDirectory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TreeView TargetDirectoryTV;
        private System.Windows.Forms.TextBox CreateDirectoryTextBox;
        private System.Windows.Forms.Button CreateDirectoryButton;
    }
}