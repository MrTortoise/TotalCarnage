using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Custom.Zip;

namespace TileEditor
{
	/// <summary>
	/// Opens a dialogue that alows selection of a directory
	/// and a name for a zip file to then create.
	/// </summary>
	public partial class CreateNewGrpFile : Form
	{
		protected string mDirectory;
		protected string mFileName;

		/// <summary>
		/// Get / Set the directory Path
		/// </summary>
		public string Directory
		{
			get { return mDirectory; }
			set
			{
				mDirectory = value;
				txtTarget.Text = value;
			}
		}

		/// <summary>
		/// get / Set the Filename
		/// </summary>
		public string FileName
		{
			get { return mFileName; }
			set
			{
				mFileName = value;
				txtName.Text = value;
			}
		}
		

		/// <summary>
		/// Gets the Full Directory + path name
		/// </summary>
		public string FullPath
		{
			get
			{
				if (mDirectory.Contains(":"))
				{
					return mDirectory + mFileName;
				}
				else
				{
					return mDirectory + "\\" + mFileName;
				}
			}
		}



		public CreateNewGrpFile()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog d = new FolderBrowserDialog();
			d.Description = "Select a Directory to create the GRP File in.";
						
			DialogResult r = d.ShowDialog();

			if (r == DialogResult.OK)
			{
				Directory = d.SelectedPath;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			FileName = txtName.Text;
			ZipFile z = new ZipFile();
			z.CreateFile(FullPath);
			this.DialogResult = DialogResult.OK;	 			
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}



	}
}
