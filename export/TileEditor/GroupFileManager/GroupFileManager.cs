using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Custom.Zip;
using Custom.Maths;
using Custom.Components;
using Custom.Exceptions;

namespace TileEditor
{
	public partial class GroupFileManager : Form
	{

		protected ZipFile mZip;

		public GroupFileManager()
		{
			InitializeComponent();
		}

		private void btnLoadZipFile_Click(object sender, EventArgs e)
		{

		}



		private void selectFileToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void createFileToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateNewGrpFile d = new CreateNewGrpFile();
			if (d.ShowDialog() == DialogResult.OK)
			{
				txtZipFile.Text = d.FullPath;
			}
		}

		private void selectExistingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog d = new OpenFileDialog();
			if (d.ShowDialog() == DialogResult.OK)
			{
				txtZipFile.Text = d.FileName;
			}
		}

		private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mZip = new ZipFile();
			mZip.FileName = txtZipFile.Text;
			mZip.Open();

			//List<string> s = mZip.GetDirectoryList("/t2");
			List<string> s = mZip.GetAllContents();

			foreach (string st in s)
			{
				// if directory then
				if (st.EndsWith("/"))
				{
					directoryListView1.AddItem(st, DirectoryListView.ItemType.folder);
				}
				else
				{
					directoryListView1.AddItem(st, DirectoryListView.ItemType.file);
				}
			}
		}	 
	}
}
