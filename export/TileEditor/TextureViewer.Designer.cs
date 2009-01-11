namespace TileEditor
{
	partial class TextureViewer
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
			this.LoadFiles = new System.Windows.Forms.Button();
			this.PanelToggle = new System.Windows.Forms.Button();
			this.texturesList = new System.Windows.Forms.ListView();
			this.textureFilesList = new System.Windows.Forms.ListView();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.listViewContainer = new System.Windows.Forms.Panel();
			this.texturesPanel1 = new Custom.Controls.TexturesPanel();
			this.flowLayoutPanel1.SuspendLayout();
			this.listViewContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// LoadFiles
			// 
			this.LoadFiles.Location = new System.Drawing.Point(12, 12);
			this.LoadFiles.Name = "LoadFiles";
			this.LoadFiles.Size = new System.Drawing.Size(123, 29);
			this.LoadFiles.TabIndex = 0;
			this.LoadFiles.Text = "Load Group File";
			this.LoadFiles.UseVisualStyleBackColor = true;
			this.LoadFiles.Click += new System.EventHandler(this.LoadFiles_Click);
			// 
			// PanelToggle
			// 
			this.PanelToggle.Location = new System.Drawing.Point(141, 12);
			this.PanelToggle.Name = "PanelToggle";
			this.PanelToggle.Size = new System.Drawing.Size(90, 29);
			this.PanelToggle.TabIndex = 4;
			this.PanelToggle.Text = "Hide Lists";
			this.PanelToggle.UseVisualStyleBackColor = true;
			this.PanelToggle.Click += new System.EventHandler(this.PanelToggle_Click);
			// 
			// texturesList
			// 
			this.texturesList.Location = new System.Drawing.Point(364, 3);
			this.texturesList.Name = "texturesList";
			this.texturesList.Size = new System.Drawing.Size(271, 113);
			this.texturesList.TabIndex = 2;
			this.texturesList.UseCompatibleStateImageBehavior = false;
			// 
			// textureFilesList
			// 
			this.textureFilesList.Location = new System.Drawing.Point(3, 3);
			this.textureFilesList.Name = "textureFilesList";
			this.textureFilesList.Size = new System.Drawing.Size(276, 113);
			this.textureFilesList.TabIndex = 1;
			this.textureFilesList.UseCompatibleStateImageBehavior = false;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.listViewContainer);
			this.flowLayoutPanel1.Controls.Add(this.texturesPanel1);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 47);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(641, 452);
			this.flowLayoutPanel1.TabIndex = 6;
			// 
			// listViewContainer
			// 
			this.listViewContainer.Controls.Add(this.textureFilesList);
			this.listViewContainer.Controls.Add(this.texturesList);
			this.listViewContainer.Location = new System.Drawing.Point(3, 3);
			this.listViewContainer.Name = "listViewContainer";
			this.listViewContainer.Size = new System.Drawing.Size(638, 138);
			this.listViewContainer.TabIndex = 7;
			// 
			// texturesPanel1
			// 
			this.texturesPanel1.Location = new System.Drawing.Point(4, 148);
			this.texturesPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.texturesPanel1.Name = "texturesPanel1";
			this.texturesPanel1.Size = new System.Drawing.Size(634, 302);
			this.texturesPanel1.TabIndex = 8;
			// 
			// TextureViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(666, 511);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.PanelToggle);
			this.Controls.Add(this.LoadFiles);
			this.Name = "TextureViewer";
			this.Text = "TextureViewer";
			this.flowLayoutPanel1.ResumeLayout(false);
			this.listViewContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button LoadFiles;
		private System.Windows.Forms.Button PanelToggle;
		private System.Windows.Forms.ListView texturesList;
		private System.Windows.Forms.ListView textureFilesList;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Panel listViewContainer;
		private Custom.Controls.TexturesPanel texturesPanel1;

	}
}