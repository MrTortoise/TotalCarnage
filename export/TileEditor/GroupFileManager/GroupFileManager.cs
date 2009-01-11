using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TileEditor
{
	public partial class GroupFileManager : Form
	{
		public GroupFileManager()
		{
			InitializeComponent();
		}



		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog d = new OpenFileDialog();
			if (d.ShowDialog() == DialogResult.OK)
			{
				txtZipFile.Text = d.FileName;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			CreateNewGrpFile d = new CreateNewGrpFile();
			if (d.ShowDialog() == DialogResult.OK)
			{
				txtZipFile.Text = d.FullPath;
			}
		}
	}
}
