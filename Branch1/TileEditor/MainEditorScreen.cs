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
    public partial class MainEditorScreen : Form
    {
        private int childFormNumber = 0;

        public MainEditorScreen()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                if (FileName.Contains(".xml"))
                {
                    FileName.Remove(FileName.IndexOf(".xml"));                    
                }
                MapEditor formInstance = new MapEditor(FileName);
                formInstance.MdiParent = this;
                formInstance.Text = "Editing " + FileName;
                formInstance.Show();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void contentGrabberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContentGrabber theForm = new ContentGrabber();
            theForm.MdiParent = this;
            theForm.Show();

        }

		private void mapEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MapEditor theForm = new MapEditor("broke");
			theForm.MdiParent = this;
			theForm.Show();
		}

		private void textureToolToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TextureViewer tv = new TextureViewer();
			tv.MdiParent = this;
			tv.Show();
		}

		private void textureTestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			texturePanelTest tp = new texturePanelTest();
			tp.MdiParent = this;
			tp.Show();
		}

		private void groupFileManagerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GroupFileManager g = new GroupFileManager();
			g.MdiParent = this;
			g.Show();

		}
    }
}
