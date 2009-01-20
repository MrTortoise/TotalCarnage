namespace TileEditor
{
	partial class TextureCell
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblTitle = new System.Windows.Forms.Label();
			this.imageGrid1 = new ImageGrid();
			this.lblInfo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblTitle.Location = new System.Drawing.Point(1, 0);
			this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(256, 21);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "label1";
			// 
			// imageGrid1
			// 
			this.imageGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.imageGrid1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.imageGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.imageGrid1.FileName = null;
			this.imageGrid1.Location = new System.Drawing.Point(0, 55);
			this.imageGrid1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.imageGrid1.Name = "imageGrid1";
			this.imageGrid1.NoColumns = 1;
			this.imageGrid1.NoRows = 1;
			this.imageGrid1.Size = new System.Drawing.Size(258, 252);
			this.imageGrid1.TabIndex = 2;
			// 
			// lblInfo
			// 
			this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblInfo.Location = new System.Drawing.Point(1, 21);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(256, 23);
			this.lblInfo.TabIndex = 3;
			this.lblInfo.Text = "label1";
			// 
			// TextureCell
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.imageGrid1);
			this.Controls.Add(this.lblTitle);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "TextureCell";
			this.Size = new System.Drawing.Size(256, 306);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblTitle;
		private ImageGrid imageGrid1;
		private System.Windows.Forms.Label lblInfo;
		
		
	}
}
