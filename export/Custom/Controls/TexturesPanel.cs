using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Custom.Controls
{
	public partial class TexturesPanel : UserControl
	{
		public TexturesPanel()
		{
			InitializeComponent();
		}


		public void AddTextureCell(string Title, string Path, int NoRows, int NoColumns)
		{
			TextureCell tc = new TextureCell();
			tc.Title = Title;
			tc.FileName = Path;
			tc.NoRows = NoRows;
			tc.NoColumns = NoColumns;
			tc.Height = 291;
			tc.Width = 189;

			flowLayoutPanel1.Controls.Add(tc);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			foreach (Control c in flowLayoutPanel1.Controls)
			{
				TextureCell tc = c as TextureCell;

				if (tc != null)
				{
					tc.SetImageViewSize(Convert.ToInt16(txtImageSize.Text), Convert.ToInt16(txtImageSize.Text));

				}
			}
		
			this.Refresh();
		}
	}
}
