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
	public class GameControl : IGameDrawable , IEquatable<GameControl>
	{
		// inherit and override memebers to enable textured controls
		// ToDo implement overriden public members
		#region Fields	
		protected int mID;
		protected string mName;	

		protected Vector2 mPosition = Vector2.Zero ;
		protected Vector2 mSize = new Vector2(100, 100);  
		
		protected Color mBackColor = Color.AliceBlue;
		protected Color mBorderColor = Color.Black;

		protected bool mGotFocus = false;
		protected bool mIsVisible = true;

		protected ControlManager mControlManager;
		protected GameControl mParent;
		protected List<GameControl> mChildren;
		#endregion

		#region Events
#region Declerations
		public EventHandler<EventArgs> NameChanged;
		public EventHandler<EventArgs> IdChanged;
		public EventHandler<EventArgs> BorderColorChanged;
		public EventHandler<EventArgs> BackrColorChanged;

		public EventHandler<GameControlEventArgs> GotFocus;		
		public EventHandler<GameControlEventArgs> LostFocus;
		public EventHandler<VisibilityEventArgs> VisibilityChanged;
		public EventHandler<ControlPositionDimsArgs> PositionSizeChanged;
#endregion
#region Handlers
		protected virtual void HandleGotFocusEvent(object sender, GameControlEventArgs theArgs)
		{
			//test to see if the event came from a child - if so then no other child controls to be effected

			//if it doesnt then need to tell other controls that they ahvve lost focus
		}
		protected virtual void HandleChildLostFocusEvent(object sender, GameControlEventArgs theArgs)
		{

		}
		
#endregion

	

		#region Event Raising helper Methods

		protected void RaiseEvent(EventHandler<InputEventArgs> theEvent, InputEventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<InputEventArgs> temp = theEvent;
			if (temp != null)
				temp(this, theEventArgs);
		}
		protected void RaiseEvent(EventHandler<GameControlEventArgs> theEvent, GameControlEventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<GameControlEventArgs> temp = theEvent;
			if (temp != null)
				temp(this, theEventArgs);
		}
		protected void RaiseEvent(EventHandler<VisibilityEventArgs> theEvent, VisibilityEventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<VisibilityEventArgs> temp = theEvent;
			if (temp != null)
				temp(this, theEventArgs);
		}
		protected void RaiseEvent(EventHandler<ControlPositionDimsArgs> theEvent, ControlPositionDimsArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<ControlPositionDimsArgs> temp = theEvent;
			if (temp != null)
				temp(this, theEventArgs);
		}
		protected void RaiseEvent(EventHandler<EventArgs> theEvent, EventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<EventArgs> temp = theEvent;
			if (temp != null)
				temp(this, theEventArgs);
		}

		#endregion
		#endregion
		#region	Constructors

		public GameControl(int theID, string theName, ControlManager thecontrolManager)
		{
			mID	= theID;
			mName =theName;
			mControlManager	= thecontrolManager;			
		}

		///	<summary>
		///	Creates	a game control and sets	its	parent
		///	</summary>
		///	<param name="theID"></param>
		///	<param name="theName"></param>
		///	<param name="theParent"></param>
		public GameControl(int theID, string theName, GameControl theParent)
		{				
			mID	= theID;
			mName =	theName;
			mParent	= theParent;  			
		}
		


		#endregion 
		#region	Properties
		///	<summary>
		///	Sets the Id	of the control - has no	checking for overlapping control ID's in the controls themselves
		///	</summary>
		///	<exception cref="System.ArgumentOutOfRangeException">Thrown	when ID	less than 0</exception>
		public int ID
		{
			get	{ return mID; }
			set
			{
				if (value >	-1)
				{
					mID	= value;
					RaiseEvent(IdChanged , new EventArgs());
				}
				else
					throw new ArgumentOutOfRangeException("Tried to	set	ID to negative value");
			}
		}

		///	<summary>
		///	Serts the name of the control
		///	</summary>
		///	<exception cref="Custom.ArgumentEmptyStringException">thrown on	setting	value to an	empty string</exception>
		public string Name
		{
			get	{ return mName;	}
			set
			{
				if (value != "")
				{
					if (mName != value)
					{
						mName =	value;
						RaiseEvent(NameChanged,	new	EventArgs());
					}
				}
				else
					throw new ArgumentEmptyStringException("Tried to set a game	control's name to an empty string");
			}
		}

		///	<summary>
		///	Gets or	sets wether	the	control	sohuld be drawn	or not
		///	</summary>
		public bool	IsVisible
		{
			get	{ return mIsVisible; }
			set
			{
				if (mIsVisible != value)
				{
					mIsVisible = value;
					RaiseEvent(VisibilityChanged, new VisibilityEventArgs(value));
				}
			}
		}

		///	<summary>
		///	Gets or	Sets the Position of the control relative to its parent
		///	<para>does not throw an	exception wit hnegative	values to allow	partially on screen	controls</para>
		///	</summary>
		public Vector2 Position
		{
			get	{ return mPosition;	}
			set
			{
				if (value != mPosition)
				{
					if (value != null)
					{
						mPosition =	value;
						RaiseEvent(PositionSizeChanged,	new	ControlPositionDimsArgs(mPosition, mSize));
					}
				}
			}
		}

		///	<summary>
		///	Gets or	sets the size of the control
		///	</summary>
		///	<exception cref="System.ArgumentOutOfRangeException">thrown	with negative values in	the	vector2</exception>
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
					if (value.X	< 0)
					{
						throw new ArgumentOutOfRangeException("tried to	set	negative width for control");
					}
					if (value.Y	< 0)
					{
						throw new ArgumentOutOfRangeException("Tried to	set	a negative height for a	control");
					}

					if (value != mSize)
					{
						mSize =	value;
						RaiseEvent(PositionSizeChanged,	new	ControlPositionDimsArgs(mPosition, mSize));
					}
				}
			}
		}

		///	<summary>
		///	Recursivley	gets the controls absolute position	relative to	 0,0
		///	</summary>
		public Vector2 GetAbsolutePosition
		{
			get
			{
				if (mParent	== null)
				{
					return mPosition;
				}
				else { return mParent.GetAbsolutePosition +	mPosition; }
			}
		}

		///	<summary>
		///	Gets wether	or not the control has the Focus
		///	</summary>
		public bool	HasFocus
		{ get {	return mGotFocus; }	}

		///	<summary>
		///	Gets the parent	control	for	this control
		///	</summary>
		public GameControl Parent
		{ get {	return mParent;	} }

		///	<summary>
		///	Gets ot	Sets the BackGround	of the Control
		///	</summary>
		public Color BackColor
		{
			get	{ return mBackColor; }
			set
			{
				if (mBackColor != value)
				{
					mBackColor = value;
					RaiseEvent(BackrColorChanged, new EventArgs());
				}
			}
		}

		///	<summary>
		///	Gets or	Sets the color of the border of	the	control
		///	</summary>
		public Color BorderColor
		{
			get	{ return mBorderColor; }
			set
			{
				if (mBorderColor !=	value)
				{
					mBorderColor = value;
					RaiseEvent(BorderColorChanged, new EventArgs());

				}
			}
		}


		#endregion 
		#region	Methods
		#region	Public Methods

		public void	RemoveFocus()
		{
			if (mChildren != null)
			{
				foreach	(GameControl gt	in mChildren)
				{
					gt.RemoveFocus();
				}					 
			}
			else
			{
				if (mGotFocus == true)
				{
					mGotFocus =	false;		// only	set	it here	for	the	sake of	the	input args
					RaiseEvent(LostFocus, new GameControlEventArgs(this));
				}
			}
			mGotFocus =	false;		

		}

		///	<summary>
		///	This function takes	the	Input event	args, updates itself as	necessary
		///	and	then passes	the	InputEventArgs to whatever other child controls	need them 		///	
		///	</summary>
		///	<param name="theArgs"></param>
		public virtual	void ProcessInput(InputEventArgs theArgs)
		{
			// RaiseGotFocusEvent(new GameControlEventArgs(this));	
			
			
			// establish control to	send input to					

			bool childHasFocus = false;
			if (mChildren != null)
			{
				int	counter	= 0;
				do
				{
					if (mChildren[counter].IsAbsolutePositionInsideControl(theArgs.CurrentMousePosition))
					{
						childHasFocus =	true;
						mChildren[counter].ProcessInput(theArgs);
						MoveControlToFront(counter);
						counter	= mChildren.Count;
					}							  
					counter++;
				}
				while (counter < mChildren.Count);
			}
			if (childHasFocus == false)
			{
				ProcessInternalInput(theArgs);
			}								  	


		}
		public void	AddChildControl(GameControl	Child)
		{
			if (Child != null)
			{
				if (!mChildren.Contains(Child))
				{
					//ToDo:Sign	up to the controls events
					
					mChildren.Add(Child);
				}
			}
		}
		///	<summary>
		///	This tests wether a	set	of coordinates relative	to 0,0 are inside this control
		///	</summary>
		///	<param name="thePosition"></param>
		///	<returns></returns>
		public bool	IsAbsolutePositionInsideControl(Vector2	thePosition)
		{
			bool retVal	= false;
			Vector2	absPosOfControl	= GetAbsolutePosition;
			//if below top
			if (thePosition.Y >	absPosOfControl.Y)
			{
				//if above bottom
				if (thePosition.Y <	(absPosOfControl.Y + mSize.Y))
				{
					//if right of the left side
					if (thePosition.X >	absPosOfControl.X)
					{
						//if left of the right side
						if (thePosition.X <	(absPosOfControl.X + mSize.X))
						{
							//its inside thePosition control
							retVal = true;
						}
					}
				}
			}	
			
			return retVal;

		}
		///	<summary>
		///	This tests wether a	set	of coordinates relative	to its parent are inside this control
		///	</summary>
		///	<param name="thePosition"></param>
		///	<returns></returns>
		public bool	IsPositionInsideControl(Vector2	thePosition)
		{
			bool retVal	= false;


			//if below top
			if (thePosition.Y >	mPosition.Y)
			{
				//if above bottom
				if (thePosition.Y<(mPosition.Y+mSize.Y))
				{
					//if right of the left side
					if (thePosition.X >	mPosition.X)
					{
						//if left of the right side
						if (thePosition.X <	(mPosition.X + mSize.X))
						{
							//its inside thePosition control
							retVal = true;
						}				  
					}							   
				}								   
			}							  
			return retVal;
		}
		#endregion

		

		#region	Protected Methods

		protected virtual void ProcessInternalInput(InputEventArgs theArgs)
		{
			RaiseGotFocusEvent(new GameControlEventArgs(this));
		}

		///	<summary>
		///	Will Only raise	got	focus if not already got focus .. not when a different child control gets it.
		///	</summary>
		///	<param name="theArgs"></param>
		protected virtual void RaiseGotFocusEvent(GameControlEventArgs theArgs)
		{
			mGotFocus =	true;
			RaiseEvent(GotFocus, new GameControlEventArgs(theArgs.Control));
		}
		

		protected Vector2 GetRelativePosition(Vector2 thePosition)
		{
			Vector2	retVal = new Vector2();
			retVal.X = thePosition.X - mPosition.X;
			retVal.Y = thePosition.Y - mPosition.Y;
			return retVal;
		}  
		protected void MoveControlToFront(int index)
		{
			List<GameControl> retVal = new List<GameControl>();

			retVal.Add(mChildren[index]);
			for	(int i = 0;	i <	index; i++)
			{
				retVal.Add(mChildren[i]);
			}
			for	(int i = index + 1;	i <	mChildren.Count; i++)
			{
				retVal.Add(mChildren[i]);
			}		   
			mChildren =	retVal;
		}	
		protected void MoveControlToBack(int index)
		{
			List<GameControl> retVal = new List<GameControl>();
			for	(int i = 0;	i <	index; i++)
			{
				retVal.Add(mChildren[i]);
			}
			for	(int i = index + 1;	i <	mChildren.Count; i++)
			{
				retVal.Add(mChildren[i]);
			}
			retVal.Add(mChildren[index]);
			mChildren =	retVal;
		}


		#endregion
		#endregion 
		#region IGameDrawable Members

		public void Draw(spriteBatchArgs thespriteBatchArgs)
		{
			if (mIsVisible == true)
			{
				//Draw Background
				VectorDraw.DrawRectangleFilled(mPosition, mSize, mBackColor, thespriteBatchArgs);
				//Draw Border
				VectorDraw.DrawRectangleEdge(mPosition, mSize, mBorderColor, 1, thespriteBatchArgs); 

				InnerDraw(thespriteBatchArgs);
				if (mChildren != null)
				{
					foreach (GameControl gc in mChildren)
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



		#region IEquatable<GameControl> Members

		public bool Equals(GameControl other)
		{
			if (other==null)
				return false;

			bool retVal = true;

			if (
				 (other.mID == mID) &&
				(other.mName == mName) &&
				(other.mPosition.Equals(mPosition)) &&
				(other.mSize.Equals(mSize)) &&
				(other.mBackColor == mBackColor) &&
				(other.BorderColor == mBorderColor) &&
				(other.mGotFocus == mGotFocus) &&
				(other.mIsVisible == mIsVisible)
				
				)
			{
				if (other.mControlManager == null)
				{
					if (mControlManager != null)
						retVal = false;
				}
				else
				{
					if (other.mControlManager != mControlManager)
					{
						retVal = false;
					}
				}

				if (other.mChildren == null)
				{
					if (mChildren != null)
						retVal = false;
				}
				else
				{
					if (other.mChildren != mChildren)
						retVal = false;
				}

				if (other.mParent == null)
				{
					if (mParent != null)
						retVal = false;
				}
				else
				{
					if (other.mParent != mParent)
						retVal = false;
				}
			}
			else
			{
				retVal = false;
			}
			return retVal;
		}

		#endregion
	}



}

