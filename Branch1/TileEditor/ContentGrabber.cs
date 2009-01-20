using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using CommonObjects;
using Custom.IO;


namespace TileEditor
{
    public partial class ContentGrabber : Form
    {

        public delegate void NodeAdder(TreeView tree, TreeNode node);

        public ContentGrabber()
        {            
            InitializeComponent();
        }



 

        #region treeviewStuff

        private void TargetDirectoryLoad()
        {
            Thread loader = new Thread(new ThreadStart(TargetDirectoryLoader));
            loader.Start(); 
        }

        /// <summary>
        /// call TargetDirectoryLoad instead if multithreading desired
        /// </summary>
        private void TargetDirectoryLoader()
        {
            
            TreeNode tn = new TreeNode();

            DirectoryInfo theDirectory = new DirectoryInfo(targetPathTextBox.Text  );
            tn.Text = theDirectory.FullName;
            tn.Name = theDirectory.FullName;

            if (TargetDirectoryTV.InvokeRequired)
            {
                object[] paramArray = new object[2];
                paramArray[0] = TargetDirectoryTV;
                paramArray[1] = tn;

				TargetDirectoryTV.Invoke(new NodeAdder(SetNodes), paramArray);
                // treeViewSource.Invoke(new MethodInvoker(treeViewSource.Nodes.Add(AddChildNodes(tn, false))));
            }
            else
            {
				TargetDirectoryTV.Nodes.Clear();
				TargetListView.Clear();
                TargetDirectoryTV.Nodes.Add(AddChildNodes(tn, false));
            }
        }


        private void SourceDirectoryLoader()
        {
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                TreeNode tn = new TreeNode();
                string nodeText = string.Empty;
                if (di.IsReady)
                {
                    nodeText = string.Format(@"{0} ({1}:)", di.Name, di.VolumeLabel);
                }
                else
                {
                    nodeText = string.Format(@"({0}:)", di.Name);
                }
                tn.Text = nodeText;
                tn.Name = di.Name;

                if (treeViewSource.InvokeRequired)
                {
                    object[] paramArray = new object[2];
                    paramArray[0] = treeViewSource;
                    paramArray[1] = tn;

                    treeViewSource.Invoke(new NodeAdder(GetNodes), paramArray);
                    // treeViewSource.Invoke(new MethodInvoker(treeViewSource.Nodes.Add(AddChildNodes(tn, false))));
                }
                else
                {
                    treeViewSource.Nodes.Add(AddChildNodes(tn, false));
                }

                // treeViewSource.Invoke(treeViewSource.Nodes.Add(AddChildNodes(tn, false)), paramsd);

            }
        }

        private void GetNodes(TreeView tree, TreeNode node)
        {
            //tree.Nodes.Clear();
            tree.Nodes.Add(AddChildNodes(node, false));
        }

		private void SetNodes(TreeView tree, TreeNode node)
		{
			tree.Nodes.Clear();
			TargetListView.Clear();
			tree.Nodes.Add(AddChildNodes(node, false));
		}

        private TreeNode AddChildNodes(TreeNode node, bool final)
        {
            if (final)
            {
                return node;
            }
            DirectoryInfo parentDI = new DirectoryInfo(node.Name);

            if (parentDI.FullName.Length == 3)
            {
                DriveInfo di = new DriveInfo(parentDI.FullName);
                if (!di.IsReady)
                {
                    return node;
                }
            }

            DirectoryInfo[] arrDI = null;
            try
            {
                arrDI = parentDI.GetDirectories();
            }
            catch
            {
            }
            if (arrDI != null)
            {
                foreach (DirectoryInfo di in arrDI)
                {
                    TreeNode tn = new TreeNode();
                    string nodeText = string.Empty;

                    tn.Text = di.Name;
                    tn.Name = di.FullName;
                    node.Nodes.Add(AddChildNodes(tn, true));
                }
            }
            return node;
        }

        private void treeViewSource_AfterSelect(object sender, TreeViewEventArgs e)
        {

            listViewSource.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(e.Node.Name);
            FileInfo[] arrFI = null;
            try
            {
                arrFI = di.GetFiles();
            }
            catch
            { }

            if (arrFI == null)
            {
                return;
            }
            foreach (FileInfo fi in arrFI)
            {
                listViewSource.Items.Add(fi.Name);
            }
        }
        private void treeViewSource_AfterExpand(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode tn in e.Node.Nodes)
            {
                AddChildNodes(tn, false);
            }

            listViewSource_SelectedIndexChanged(sender, e);
        }

        private void TargetDirectoryTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            TargetListView.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(e.Node.Name);
            FileInfo[] arrFI = null;
            try
            {
                arrFI = di.GetFiles();
            }
            catch
            { }

            if (arrFI == null)
            {
                return;
            }
            foreach (FileInfo fi in arrFI)
            {
                TargetListView.Items.Add(fi.Name);
            }
        }

        private void TargetDirectoryTV_AfterExpand(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode tn in e.Node.Nodes)
            {
                AddChildNodes(tn, false);
            }

            targetListView_SelectedIndexChanged(sender, e);
        }
        #endregion

        #region Page Events
        private void button1_Click(object sender, EventArgs e)
        {
            targetFolderBrowserDialog.Description = "Select the folder that the game content files will be copied into";
            if (targetFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                //PopulateTargetListBox(targetFolderBrowserDialog.SelectedPath);
                targetPathTextBox.Text = targetFolderBrowserDialog.SelectedPath;
                              
                TargetDirectoryLoad();               
            }

        }
        private void ContentGrabber_Load(object sender, EventArgs e)
        {
            copyFile.Enabled = false;

            Thread loader = new Thread(new ThreadStart(SourceDirectoryLoader));
            loader.Start();
        }

        private void listViewSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSource.SelectedItems.Count > 0)
            {
                DirectoryInfo dir = new DirectoryInfo(treeViewSource.SelectedNode.Name);
                string fileName = string.Empty;
                if (dir.FullName.EndsWith("\\"))
                {
                    fileName = dir.FullName + listViewSource.SelectedItems[listViewSource.SelectedItems.Count - 1].Text;
                }
                else
                {
                    fileName = dir.FullName + "\\" + listViewSource.SelectedItems[listViewSource.SelectedItems.Count - 1].Text;
                }

                FileInfo file = new FileInfo(fileName);

                labelSourceFullName.Text = file.FullName;
                SourceSize.Text = (file.Length / 1000).ToString() + " k";
                SourceExtension.Text = file.Extension;

                try
                {
                    SourceImage.Load(file.FullName);
                    SourceDims.Text = SourceImage.Width + ", " + SourceImage.Height;
                }
                catch
                {
                    SourceImage.Image = null;
                }
            }
        }

        private void targetListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TargetListView.SelectedItems.Count == 1)
            {
                DirectoryInfo dir = new DirectoryInfo(targetPathTextBox.Text);
                string fileName = string.Empty;
                if (dir.FullName.EndsWith("\\"))
                {
                    fileName = dir.FullName + TargetListView.SelectedItems[0].Text;
                }
                else
                {
                    fileName = dir.FullName + "\\" + TargetListView.SelectedItems[0].Text;
                }

                FileInfo file = new FileInfo(fileName);

                // labelSourceFullName.Text = file.FullName;
                // SourceSize.Text = (file.Length / 1000).ToString() + " k";
                // SourceExtension.Text = file.Extension;

                try
                {
                    targetImage.Load(file.FullName);
                    // 
                    //SourceDims.Text = SourceImage.Width + ", " + SourceImage.Height;
                }
                catch
                {
                    targetImage.Image = null;
                }
            }

        }

        private void targetPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (targetPathTextBox.Text != "")
            {
                copyFile.Enabled = true;
            }
            else
            { copyFile.Enabled = false; }
        }

        private void copyFile_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(treeViewSource.SelectedNode.Name);


            foreach (ListViewItem li in listViewSource.SelectedItems)
            {
                string fileName = GetFullPath(dir.FullName, li.Text);
                FileInfo file = new FileInfo(fileName);

                string target = GetFullPath(TargetDirectoryTV.SelectedNode.FullPath, li.Text);
                file.CopyTo(target);
            }

            PopulateTargetListBox(targetPathTextBox.Text);
        }

        private void deleteTargetFiles_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in TargetListView.SelectedItems)
            {
                FileInfo file = new FileInfo(GetFullPath(targetPathTextBox.Text, li.Text));
                file.Delete();

            }

            PopulateTargetListBox(targetPathTextBox.Text);
        }

        private void CopyDirectory_Click(object sender, EventArgs e)
        {

            Thread copier = new Thread(new ThreadStart(DirectoryCopier));
            copier.Start();

        }
        #endregion

        private string GetFullPath(string directory, string file)
        {
            string retVal;
            if (directory.EndsWith("\\"))
            {
                retVal = directory + file;
            }
            else
            {
                retVal = directory + "\\" + file;
            }
            return retVal;
        }

        private void DirectoryCopier()
        {
            DirectoryInfo dir = new DirectoryInfo(treeViewSource.SelectedNode.Name);
            DirectoryInfo tar = new DirectoryInfo(targetPathTextBox.Text);


            FileIO files = new FileIO();
            files.CopyDirectory(dir, tar);
            PopulateTargetListBox(targetPathTextBox.Text);
        }

        private void PopulateTargetListBox(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            
            TargetListView.Clear();

            FileInfo[] arrFI = null;
            try
            {
                arrFI = di.GetFiles();
            }
            catch
            { }

            if (arrFI == null)
            {
                return;
            }
            foreach (FileInfo fi in arrFI)
            {
                TargetListView.Items.Add(fi.Name);
            }
        }

        private void CreateDirectoryButton_Click(object sender, EventArgs e)
        {
            if (CreateDirectoryTextBox.Text != "")
            {
                DirectoryInfo dir = new DirectoryInfo(TargetDirectoryTV.SelectedNode.FullPath);

                dir.CreateSubdirectory(CreateDirectoryTextBox.Text);
                TargetDirectoryLoad();
            }


        }






    }
}
