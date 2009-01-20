using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TileEditor
{
	/// <summary>
	/// Used to show a texture and its basic information
	/// Also shows the grid overlay for spritesheets
	/// </summary>
	public partial class TextureCell : UserControl
	{
		#region Fields

		protected string mTitle = "";
		protected bool mShowInfo = true;

		protected int xSize = 128;
		protected int ySize = 128;
		#endregion

		#region Constructor
		/// <summary>
		/// Creates the objet with the image properties set to zoom
		/// </summary>
		public TextureCell()
		{			
			InitializeComponent();
			//this.UpdateStyles();
		}
		#endregion

		#region Properties

		/// <summary>
		/// Gets / Sets the number of rows in the image
		/// must be > 0
		/// </summary>
		public int NoRows
		{
			get { return imageGrid1.NoRows; }
			set
			{
				if (value < 1)
					value = 1;				
				
				imageGrid1.NoRows = value;
				UpdateInfo();
				this.Refresh();
				
			}
		}

		/// <summary>
		/// Gets / Sets the number of columns in the image
		/// must be > 0
		/// </summary>
		public int NoColumns
		{
			get { return imageGrid1.NoColumns; }
			set
			{
				if (value < 1)
					value = 1;				
				
				imageGrid1.NoColumns = value;
				UpdateInfo();
				this.Refresh();
			}
		}

		/// <summary>
		/// Get / Set the filename of the image file
		/// and then loads it
		/// </summary>
		public string FileName
		{
			//ToDo: check that the path refers to an actual file
			get { return imageGrid1.FileName; }
			set
			{
				if ((value == "") || (value == null))
				{ }
				else
				{
					imageGrid1.FileName = value;
					SetTitle();
					UpdateInfo();
					LoadImage();
				}
			}
		}

		/// <summary>
		/// get / Set the title of the Control
		/// </summary>
		public string Title
		{
			get { return mTitle; }
			set
			{
				if ((value == "") || (value==null))
				{}
				else
				{					
				mTitle = value;
				SetTitle();
				}
			}
		}
		#endregion

		/// <summary>
		/// Loads the image 
		/// Settign the filename executes this method automatically
		/// </summary>
		public void LoadImage()
		{
			imageGrid1.Open();		
			
				
				this.Refresh();
			
		}

		public void SetImageViewSize(int x, int y)
		{
			if (x < 0)
				x = 0;
			if (y < 0)
				y = 0;

			this.Width = x;
			this.Height = y + 55;
			this.Refresh();
		}

		protected void UpdateInfo()
		{
			if (imageGrid1 != null)
			{
				lblInfo.Text = imageGrid1.ImageWidth + ", " + imageGrid1.ImageHeight + ", " + imageGrid1.NoRows + ", " + imageGrid1.NoColumns;
			}
		}

		protected void SetTitle()
		{
			if (imageGrid1.FileName == null)
			{
				lblTitle.Text = mTitle;
			}
			else
			{
				lblTitle.Text = mTitle + " : " + imageGrid1.FileName.ToString(); ;
			}
		}  
		
	}
}
