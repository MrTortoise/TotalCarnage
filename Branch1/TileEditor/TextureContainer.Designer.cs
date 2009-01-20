namespace TileEditor
{
	partial class TextureContainer
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
			this.lblTexture = new System.Windows.Forms.Label();
			this.imgTexture = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.imgTexture)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTexture
			// 
			this.lblTexture.AutoSize = true;
			this.lblTexture.Location = new System.Drawing.Point(0, 0);
			this.lblTexture.Name = "lblTexture";
			this.lblTexture.Size = new System.Drawing.Size(100, 23);
			this.lblTexture.TabIndex = 0;
			this.lblTexture.Text = "label1";
			// 
			// imgTexture
			// 
			this.imgTexture.Location = new System.Drawing.Point(0, 0);
			this.imgTexture.Name = "imgTexture";
			this.imgTexture.Size = new System.Drawing.Size(100, 50);
			this.imgTexture.TabIndex = 0;
			this.imgTexture.TabStop = false;
			((System.ComponentModel.ISupportInitialize)(this.imgTexture)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblTexture;
		private System.Windows.Forms.PictureBox imgTexture;

	}
}
