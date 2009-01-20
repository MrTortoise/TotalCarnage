namespace TileEditor
{
	partial class ImageGrid
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// Grid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Name = "Grid";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid_Paint);
			this.Resize += new System.EventHandler(this.Grid_Resize);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
