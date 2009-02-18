﻿using System;
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
	public class GameControl : IEquatable<GameControl>
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
		protected bool mChildHasFocus = false;
		protected bool mIsVisible = true;

		//protected ControlManager mControlManager;
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
			#region	Handlers
		protected virtual void HandleGotFocusEvent(object sender, GameControlEventArgs theArgs)
		{
			//test to see if the event came	from a child - if so then no other child controls to be	effected

			//if it	doesnt then	need to	tell other controls	that they ahvve	lost focus
		}
		protected virtual void HandleChildLostFocusEvent(object	sender,	GameControlEventArgs theArgs)
		{

		}

		protected virtual void HandleMouseButtonPressed(object sender, InputEventArgs theArgs) { }
		protected virtual void HandleMouseButtonReleased(object	sender,	InputEventArgs theArgs)	{ }
		protected virtual void HandleMouseWheelScrolled(object sender, InputEventArgs theArgs) { }
		protected virtual void HandleMousePositionChanged(object sender, InputEventArgs	theArgs) { }
		protected virtual void HandleKeyPressed(object sender, InputEventArgs theArgs) { }
		protected virtual void HandleKeyRelseased(object sender, InputEventArgs	theArgs) { }
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

		public GameControl(int theID, string theName/*, ControlManager thecontrolManager*/)
		{
			mID	= theID;
			mName =theName;
			//mControlManager	= thecontrolManager;			
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

		///	<summary>
		///	this causes	event cascades up from the deepest control if anything has the focus
		///	</summary>
		public void	RemoveFocus(FocusMessageArgs theArgs)
		{
			bool childHasFocus = false;
			if (mChildren != null)
			{
				foreach	(GameControl gt	in mChildren)
				{
					if (gt.HasFocus)
					{
						childHasFocus =	true;
						gt.RemoveFocus(theArgs);
					}
				}
			}

			if (childHasFocus == false)
			{
				if (mGotFocus == true)
				{
					mGotFocus =	false;		// only	set	it here	for	the	sake of	the	input args
					ProcessSelfLostFocus(theArgs);
					RaiseEvent(LostFocus, new GameControlEventArgs(this));
				}	   
			}
		}

		

		public void	 EstablishFocus(FocusMessageArgs  theArgs)
		{
			// Only	the	control	that has focus has its mHasFocus = true	
			// establish control with focus					

			bool childHasFocus = false;
			if (mChildren != null)
			{  
				int	counter	= 0;
				do
				{
					if (mChildren[counter].DetermineIfControlGetsFocus(theArgs))						
					{
						childHasFocus =	true;
						ProcessChildGotFocus(theArgs);	// must	come first because got focus events	need chance	to fire
														// if they fire	after then final gotfocus event	fired wont have	focus
						mChildren[counter].EstablishFocus(theArgs);
						
						MoveControlToFront(counter);
						counter	= mChildren.Count;
					}							  
					counter++;
				}
				while (counter < mChildren.Count);

			}
			if (childHasFocus == false)
			{
				//make sure	none of	them have focus
				//and process the input	as it is for this control
				if (mChildren != null)
				{
					foreach	(GameControl gc	in mChildren)
					{
						if (gc.HasFocus)
							gc.RemoveFocus(theArgs);
					}
				}	 
				// we now know this	control	must have the focus
				// if it doesnt	already	have focus then	process	it as new
				if (mGotFocus == false)
				{
					ProcessSelfGainedFocus(theArgs);
				}
			}  		
			else
			{
				// we know child[0]	has	focus
				//make sure	all	but	the	one	with focus dont	have focus
				int	counter	= 1;
				while (counter < mChildren.Count)
				{
					if (mChildren[counter].HasFocus)
						mChildren[counter].RemoveFocus(theArgs);
					counter++;
				}
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
		public virtual bool DetermineIfControlGetsFocus(FocusMessageArgs theArgs)
		{
			bool retVal = false;
			//ToDo: write in enabled / disabled states
			if (IsVisible)
				retVal = IsAbsolutePositionInsideControl(theArgs.mousePosition);
			return retVal;
		}
		#endregion 		
			#region	Protected Methods



		/// <summary>
		/// Allows a control to listen for cetain commands aimed at a container
		/// </summary>
		/// <param name="theArge"></param>
		protected virtual void ProcessChildGotFocus(FocusMessageArgs theArge)
		{

		}

		/// <summary>
		/// This only fires when the control directly gains the focus
		/// </summary>
		/// <param name="theArgs"></param>
		protected virtual void ProcessSelfGainedFocus(FocusMessageArgs theArgs)
		{
			mBackColor = Color.Pink;
			mGotFocus = true;
			theArgs.eventManager.KeyPressed += HandleKeyPressed;
			theArgs.eventManager.KeyReleased += HandleKeyRelseased;
			theArgs.eventManager.MouseButtonPressed += HandleMouseButtonPressed;
			theArgs.eventManager.MouseButtonReleased += HandleMouseButtonReleased;
			theArgs.eventManager.MousePositionChanged += HandleMousePositionChanged;
			theArgs.eventManager.MouseWheelScrolled += HandleMouseWheelScrolled;

			RaiseEvent(GotFocus, new GameControlEventArgs(this));	

		}

		/// <summary>
		/// this fires when the control loses the focus
		/// </summary>
		/// <param name="theArgs"></param>
		protected virtual void ProcessSelfLostFocus(FocusMessageArgs theArgs)
		{
			mGotFocus = false;
			mBackColor = Color.SeaGreen;
			theArgs.eventManager.KeyPressed += HandleKeyPressed;
			theArgs.eventManager.KeyReleased += HandleKeyRelseased;
			theArgs.eventManager.MouseButtonPressed += HandleMouseButtonPressed;
			theArgs.eventManager.MouseButtonReleased += HandleMouseButtonReleased;
			theArgs.eventManager.MousePositionChanged += HandleMousePositionChanged;
			theArgs.eventManager.MouseWheelScrolled += HandleMouseWheelScrolled;
			RaiseEvent(LostFocus, new GameControlEventArgs(this));

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
		#region	IGameDrawable Members

		protected float CalculateLayerDepth(int NoDrawn)
		{
			float inverse = 1 / (float)NoDrawn;
			float retVal =(float)0.1-(float)( inverse / 10); //ensures that layerdepth always between 0 and 0.1 - ie at the front
				return retVal;

		}

		public void	Draw(ControlSpriteBatchArgs theArgs)
		{
			if (mIsVisible == true)
			{
				
				//Draw Border
				VectorDraw.DrawRectangleEdge(mPosition,	mSize, mBorderColor, 1,	theArgs.theSpriteBatch,CalculateLayerDepth(theArgs.NocontrolsDrawn));
				theArgs.NocontrolsDrawn++;
				//Draw Background
				VectorDraw.DrawRectangleFilled(mPosition, mSize, mBackColor, theArgs.theSpriteBatch, CalculateLayerDepth(theArgs.NocontrolsDrawn));
				theArgs.NocontrolsDrawn++;

				InnerDraw(theArgs);
				if (mChildren != null)
				{
					foreach	(GameControl gc	in mChildren)
					{
						gc.Draw(theArgs);
					}
				}

			}
			
		}

		protected virtual void InnerDraw(ControlSpriteBatchArgs  theArgs)
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
				/*if (other.mControlManager == null)
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
				 * */

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

		#region IGameDrawable Members



		#endregion
	}



}

