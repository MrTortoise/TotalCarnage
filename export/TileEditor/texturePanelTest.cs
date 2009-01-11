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
	public partial class texturePanelTest : Form
	{
		public texturePanelTest()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			texturesPanel1.AddTextureCell(textBox1.Text, textBox2.Text, Convert.ToInt16(textBox3.Text), Convert.ToInt16(textBox4.Text));
		}
	}
}
