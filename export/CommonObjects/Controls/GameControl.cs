using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CommonObjects.VectorDrawing;

using Custom.Exceptions;



namespace CommonObjects.Controls
{
	/// <summary>
	/// this class will form the backbone of all other controls that will exist on screen.
	/// The individual controls will end up being rendered by a control manager class which will interface with the .. haha i fell asleep drunk whilst writing that, I remmeber it beign a good idea tho 
	/// </summary>
	public class GameControl : IGameDrawable
	{

		protected int mID;
		protected string mName;

		protected VectorDraw mVectordraw;

		protected Vector2 mPosition = Vector2.Zero ;
		protected Vector2 mSize = new Vector2(100, 100);

		protected te
		protected Color mBackColor = Color.AliceBlue;
		protected Color mBorderColor = Color.Black;

		protected bool mHasFocus = false;
		protected bool mIsVisible = true;

		protected GameControl mParent;
		protected List<GameControl> mChilidren;

		#region Constructors

		/// <summary>
		/// creates a control with no parent
		/// </summary>
		/// <param name="theID"></param>
		/// <param name="theName"></param>
		public  GameControl(int theID, string theName,VectorDraw theVectorDrawer)
		{
			ID = theID;
			Name = theName;
			mVectordraw = theVectorDrawer;
		}

		/// <summary>
		/// Creates a game control and sets its parent
		/// </summary>
		/// <param name="theID"></param>
		/// <param name="theName"></param>
		/// <param name="theParent"></param>
		public GameControl(int theID, string theName, VectorDraw theVectorDrawer, GameControl theParent)
		{				
			ID = theID;
			Name = theName;
			mParent = theParent;
			mVectordraw = theVectorDrawer;
		}
		


		#endregion

		#region Properties
		/// <summary>
		/// Sets the Id of the control - has no checking for overlapping control ID's in the controls themselves
		/// </summary>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown when ID less than 0</exception>
		public int ID
		{
			get { return mID; }
			set
			{
				if (value > -1)
				{ mID = value; }
				else
					throw new ArgumentOutOfRangeException("Tried to set ID to negative value");
			}
		}

		/// <summary>
		/// Serts the name of the control
		/// </summary>
		/// <exception cref="Custom.ArgumentEmptyStringException">thrown on setting value to an empty string</exception>
		public string Name
		{
			get { return mName; }
			set
			{
				if (value != "")
				{
					if (mName != value)
						mName = value;
				}
				else
					throw new ArgumentEmptyStringException("Tried to set a game control's name to an empty string");
			}
		}

		/// <summary>
		/// Gets or sets wether the control sohuld be drawn or not
		/// </summary>
		public bool IsVisible
		{
			get { return mIsVisible; }
			set { mIsVisible = value; }
		}

		/// <summary>
		/// Gets or Sets the Position of the control relative to its parent
		/// <para>does not throw an exception wit hnegative values to allow partially on screen controls</para>
		/// </summary>
		public Vector2 Position
		{
			get { return mPosition; }
			set
			{
				if (value != null)
					mPosition = value;
			}
		}

		/// <summary>
		/// Gets or sets the size of the control
		/// </summary>
		/// <exception cref="System.ArgumentOutOfRangeException">thrown with negative values in the vector2</exception>
		public Vector2 Size
		{
			get
			{
				return mSize ;
			}
			set
			{
				if (value != null)
				{
					if (value.X < 0)
					{
						throw new ArgumentOutOfRangeException("tried to set negative width for control");
					}
					if (value.Y < 0)
					{
						throw new ArgumentOutOfRangeException("Tried to set a negative height for a control");
					}


					mSize = value;
				}
			}
		}

		/// <summary>
		/// Gets the controls absolute position relateive to the control managers 0,0
		/// </summary>
		public Vector2 AbsolutePosition
		{
			get
			{
				if (mParent == null)
				{
					return mPosition;
				}
				else { return mParent.AbsolutePosition + mPosition; }
			}
		}

		/// <summary>
		/// Gets wether or not the control has the Focus
		/// </summary>
		public bool HasFocus
		{ get { return mHasFocus; } }

		/// <summary>
		/// Gets the parent control for this control
		/// </summary>
		public GameControl Parent
		{ get { return mParent; } }

		/// <summary>
		/// Gets ot Sets the BackGround of the Control
		/// </summary>
		public Color BackColor
		{
			get { return mBackColor; }
			set { mBackColor = value; }
		}

		/// <summary>
		/// Gets or Sets the color of the border of the control
		/// </summary>
		public Color BorderColor
		{
			get { return mBorderColor; }
			set { mBorderColor = value; }
		}


		#endregion

		#region Public Methods
		public void AddChildControl(GameControl Child)
		{
			if (Child != null)
			{
				if (!mChilidren.Contains(Child))
				{
					mChilidren.Add(Child);
				}
			}
		}
		#endregion


		#region IGameDrawable Members

		public void Draw(spriteBatchArgs thespriteBatchArgs)
		{
			if (mIsVisible == true)
			{
				//Draw Background
				mVectordraw.DrawRectangleFilled(mPosition, mSize, mBackColor, thespriteBatchArgs);
				//Draw Border
				mVectordraw.DrawRectangleEdge(mPosition, mSize, mBorderColor, 1, thespriteBatchArgs);





				InnerDraw(thespriteBatchArgs);
				if (mChilidren != null)
				{
					foreach (GameControl gc in mChilidren)
					{
						gc.Draw(thespriteBatchArgs);
					}
				}

			}
			
		}

		protected virtual void InnerDraw(spriteBatchArgs thespriteBatchArgs)
		{

		}
		#endregion

		#region IGameDrawable Members

		public void Draw(DrawingArgs theDrawingArgs)
		{
			throw new NotImplementedException();
		}

		#endregion
	}



}

