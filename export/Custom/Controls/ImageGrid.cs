using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Custom.Interfaces;

namespace Custom.Controls
{
	/// <summary>
	/// Draws a grid of rows an columns with a transparent background
	/// swetting imagex/imageY allows control to take zoom factor into account
	/// </summary>
	public partial class ImageGrid : UserControl,
		IAgroGarbageCollection,
		IContainsResource
	{
		protected Bitmap mImage;
		protected string mPath;

		protected float mScale = 1;

		protected int mNoRows = 1;
		protected int mNoColumns = 1;

		protected int mTopLeftX = 0;
		protected int mTopLeftY = 0;

		protected int mBotRightX = 0;
		protected int mBotRightY = 0;

		protected int mNoReferences = 0;
		protected bool mIsDisposed = false;
		protected bool mIsOpen = false;

		#region Transparency fudges
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
				return cp;
			}
		}

		protected void InvalidateEx()
		{
			if (Parent == null)
				return;
			Rectangle rc = new Rectangle(this.Location, this.Size);
			Parent.Invalidate(rc, true);
		}

		/*
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			//do not allow the background to be painted 
		}
		 * */

		#endregion

		#region Constructor
		/// <summary>
		/// Creates an instance of Grid, sets its background to transparent
		/// </summary>
		public ImageGrid()
		{
			InitializeComponent();
	//		SetStyle(ControlStyles.SupportsTransparentBackColor, true);
	//		this.BackColor = Color.Transparent;
	//		this.UpdateStyles();
		}
		#endregion

		#region Properties

		/// <summary>
		/// Sets the filename of the image to be loaded
		/// </summary>
		public string FileName
		{
			get { return mPath; }
			set { mPath = value; }
		}

		/// <summary>
		/// The Number of rows to be represented by the control
		/// No rows drawn = value -1
		/// </summary>
		public int NoRows
		{
			get { return mNoRows; }
			set
			{
				if (value < 1)
				{
					mNoRows = 1;
				}
				else
				{
					mNoRows = value;
				}
			
				this.Refresh();
			}
		}

		/// <summary>
		/// The number of columns to be represented by the control
		/// No columns drawn = value -1
		/// </summary>
		public int NoColumns
		{
			get { return mNoColumns; }
			set
			{
				if (value < 1)
				{
					mNoColumns = 1;
				}
				else
				{
					mNoColumns = value;
				}

			
				this.Refresh();
			}
		}

		/// <summary>
		/// Gets the image height
		/// returns 0 is image not opened
		/// </summary>
		public int ImageHeight
		{
			get
			{
				if (mImage == null)
				{ return 0; }
				else
				{ return mImage.Height; }
			}
		}

		/// <summary>
		/// gets the image width
		/// returns 0 if image not opened
		/// </summary>
		public int ImageWidth
		{
			get
			{
				if (mImage == null)
				{ return 0; }
				else
				{ return mImage.Width; }
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// takes the image size in relation to the control size
		/// figures out the zoom factor and then
		/// sets variables for the bounds of the image
		/// allows the grid drawing function to overlay the image correctly
		/// </summary>
		protected void CalculateDrawArea()
		{
			// if the image hasn't been set or has been set to 0 then 
			// set the draw area to 0
			if (mImage==null)
			{
				mTopLeftX = 0;
				mTopLeftY = 0;
				mBotRightX = 0;
				mBotRightY = 0;
			}
			else
			{
				float xFactor = (float)this.Width / (float)mImage.Width;
				float yFactor = (float)this.Height / (float)mImage.Height;

				// find out which dimension is the limiting zoom factor
				// and then calculate the zoomed image dims
				// we know (or rather, for now ,we assert) that the image is always centered
				if (xFactor > yFactor)
				{
					// we know that the yFactor is the limiting factor
					// this means that there is space horizontally
					mScale=yFactor;
					int zoomedX = (int)((float)mImage.Width*mScale) ;

					mTopLeftX = ((this.Width) / 2) - (zoomedX / 2);
					mTopLeftY = 0;

					mBotRightY = this.Height; ;
					mBotRightX = ((this.Width) / 2) + (zoomedX / 2);
				}
				else
				{
					// we know that the xFactor is the limiting factor
					// this means that we know that there is space vertically	
					mScale=xFactor;
					int zoomedY = (int)((float)mImage.Height*mScale);

					mTopLeftX = 0;
					mTopLeftY = ((this.Height) / 2) - (zoomedY / 2);

					mBotRightX = this.Width;
					mBotRightY = ((this.Height) / 2) + (zoomedY / 2);
				}
			}
		}

		#endregion
		#region EventHandlers

		private void Grid_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			
			if (mImage != null)
			{
				graphics.DrawImage(mImage, mTopLeftX, mTopLeftY, (int)(mScale * mImage.Width), (int)(mScale * mImage.Height));
			}

			if (mNoRows > 1)
			{
				int verticalInterval = (mBotRightY - mTopLeftY) / mNoRows;
				for (int i = 1; i < (mNoRows); i++)
				{
					graphics.DrawLine(new Pen(Color.Yellow, 1), mTopLeftX, mTopLeftY + (verticalInterval * i),
						mBotRightX, mTopLeftY + (verticalInterval * i));
				}
			}

			if (mNoColumns > 1)
			{
				int horizontalInterval = (mBotRightX - mTopLeftX) / mNoColumns;
				for (int i = 1; i < (mNoColumns); i++)
				{
					graphics.DrawLine(new Pen(Color.Yellow, 1), mTopLeftX + (horizontalInterval * i), mTopLeftY,
						mTopLeftX + (horizontalInterval * i), mBotRightY);
				}
			}
		}

		private void Grid_Resize(object sender, EventArgs e)
		{
			CalculateDrawArea();
			//InvalidateEx();
			this.Refresh();
		}
		#endregion

		#region IAgroGarbageCollection Members

		/// <summary>
		/// Gets the number of references to this object
		/// This number is managed manually by referring classes
		/// </summary>
		public int NoReferences
		{
			get { return mNoReferences; }
		}

		/// <summary>
		/// Increments the referene counter to protect the object from 
		/// AgroGarbage Collection
		/// </summary>
		public void AddReference()
		{
			mNoReferences++;
		}

		/// <summary>
		/// removes a reference from the reference counter
		/// </summary>
		public void RemoveReference()
		{
			if (mNoReferences > 0)
				mNoReferences--;
		}

		/// <summary>
		/// if there are no more references to this object, 
		/// then this will dispose of the object
		/// </summary>
		public void Dispose()
		{
			// if object still has references then no need to 
			// do a forced disposal
			if (mNoReferences == 0)
			{
				if (!mIsDisposed)
				{
					Dispose(true);
				}
				GC.SuppressFinalize(this);
			}
		}

		protected void Dispose(bool disposing)
		{
			// if already disposing then dont do again
			if (!mIsDisposed)
			{
				if (disposing)
				{
					if (components != null)
					{
						components.Dispose();
					}
					mImage.Dispose();
				}
				mIsDisposed = true;
			}
			base.Dispose(disposing);
		}

		~ImageGrid()
		{
			Dispose(false);
		}



		#endregion
		#region IContainsResource Members

		/// <summary>
		/// Gets wether or not the Bitmap has been opened
		/// </summary>
		public bool IsOpen
		{
			get { return mIsOpen; }
		}
		
		/// <summary>
		/// Opens the image, if it is already open then it closes the current image
		/// </summary>
		public void Open()
		{
			if (mImage != null)
			{
				Close();
			}
			mImage = new Bitmap(mPath);
			CalculateDrawArea();
			mIsOpen=true;
		}

		/// <summary>
		/// closes and disposes of the image
		/// </summary>
		public void Close()
		{
			mImage.Dispose();
			mImage = null;
			mIsOpen=false;
		}

		#endregion
	}
}
