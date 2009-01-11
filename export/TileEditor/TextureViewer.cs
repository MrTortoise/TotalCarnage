using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Custom.Controls;
using Custom.ConfigFile;
using Custom.Zip;

namespace TileEditor
{
	public partial class TextureViewer : Form
	{
		public TextureViewer()
		{  			
			InitializeComponent();							
		}

		private void LoadFiles_Click(object sender, EventArgs e)
		{
			ConfigFileLoader cfl;
			ConfigFile cf;
			ZipFile z = new ZipFile();
			z.FileName = Properties.Resources.groupFile;
			z.Open();
			cfl = new ConfigFileLoader(z.GetFile("config.cfg"));
			cf = cfl.PopulateConfigFile();

			
			




		}

		private void PanelToggle_Click(object sender, EventArgs e)
		{
			listViewContainer.Visible = !listViewContainer.Visible;
		}


	}
}
