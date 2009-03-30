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
	public class GameControl :  IEquatable<GameControl>, IDisposable 
	{
		// inherit and override memebers to enable textured controls
		// ToDo implement overriden public members
		#region	Fields	
		protected int mID;
		protected string mName;	

		protected Vector2 mPosition	= Vector2.Zero ;
		protected Vector2 mSize	= new Vector2(100, 100);  
		
		protected Color	mBackColor = Color.AliceBlue;
		protected Color	mBorderColor = Color.Black;

		protected bool mCanGetFocus	= true;	//ToDo:	Implement this
		protected bool mGotFocus = false;
		protected bool mChildHasFocus =	false;
		protected bool mIsVisible =	true;

		protected bool mIsTextured = false;
		protected GeneralTextureCell mTextureCell;
		protected ETextureMode mTextureMode = ETextureMode.AsIs;

		protected int mTextureID;

		//protected	ControlManager mControlManager;
		protected GameControl mParent;
		protected List<GameControl>	mChildren =	new	List<GameControl>();
		#endregion 
		#region Events
			#region Declerations

		public EventHandler<ColorEventArgs> BorderColorChanged;
		public EventHandler<ColorEventArgs> BackColorChanged;  
		public EventHandler<EventArgs> GotFocus;				
		public EventHandler<EventArgs> LostFocus;
		public EventHandler<BoolEventArgs> VisibilityChanged;
		public EventHandler<BoolEventArgs> CanGetFocusChanged;
		public EventHandler<BoolEventArgs> ChildHasFocusChanged;
		public EventHandler<Vector2EventArgs> PositionChanged;
		public EventHandler<Vector2EventArgs> SizeChanged;
		public EventHandler<BoolEventArgs> IsTexturedChanged;
		public EventHandler<TextureModeEventArgs> TextureModeChanged;
		public EventHandler<GameControlEventArgs> ChildControlAdded;
		public EventHandler<GameControlEventArgs> ChildControlRemoved;
		public EventHandler<IntEventArgs> TextureCellIDChanged;	

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
		
		protected void RaiseEvent(EventHandler<ColorEventArgs> theEvent, ColorEventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<ColorEventArgs> temp = theEvent;
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
		protected void RaiseEvent(EventHandler<BoolEventArgs> theEvent, BoolEventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<BoolEventArgs> temp = theEvent;
			if (temp != null)
				temp(this, theEventArgs);
		}
		protected void RaiseEvent(EventHandler<Vector2EventArgs> theEvent, Vector2EventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<Vector2EventArgs> temp = theEvent;
			if (temp != null)
				temp(this, theEventArgs);
		}
		protected void RaiseEvent(EventHandler<TextureModeEventArgs> theEvent, TextureModeEventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<TextureModeEventArgs> temp = theEvent;
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
		protected void RaiseEvent(EventHandler<IntEventArgs> theEvent, IntEventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<IntEventArgs> temp = theEvent;
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

		public GameControl(int theID, string theName/*, ControlManager thecontrolManager*/,int textureID)
		{
			mID = theID;
			mName = theName;
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
		} 
		///	<summary>
		///	Serts the name of the control
		///	</summary>
		///	<exception cref="Custom.ArgumentEmptyStringException">thrown on	setting	value to an	empty string</exception>
		public string Name
		{
			get	{ return mName;	}  
		} 
		///	<summary>
		///	sets wether	the	control	sohuld be drawn	or not
		///	</summary>
		public bool	Visibility
		{
			set
			{
				if (mIsVisible != value)
				{
					RaiseEvent(VisibilityChanged, new BoolEventArgs(mIsVisible,	value));
					mIsVisible = value;																		
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
						RaiseEvent(PositionChanged,	new	Vector2EventArgs(mPosition,	value));
						mPosition =	value;						
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
						RaiseEvent(SizeChanged,	new	Vector2EventArgs(mSize,	value));
						mSize =	value;						
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
		/// <summary>
		/// Gets a rectangle representing the controls position relative to absolute 0,0
		/// </summary>
		public Rectangle GetAbsoluteRectangle
		{
			get
			{
				Vector2 ap = GetAbsolutePosition;
				return new Rectangle((int)ap.X, (int)ap.Y, (int)mSize.X, (int)mSize.Y);
			}

		}
		///	<summary>
		///	Gets wether	or not the control has the Focus
		///	</summary>
		public bool	HasFocus
		{ get {	return mGotFocus; }	}
		/// <summary>
		/// Boolean value representing wether a control can get the focus or not when it is clicked on
		/// </summary>
		public bool	CanGetFocus
		{
			get	{ return mCanGetFocus; }
			set
			{
				if (value != mCanGetFocus)
				{
					RaiseEvent(CanGetFocusChanged, new BoolEventArgs(mCanGetFocus, value));
					mCanGetFocus = value;
				}
			}
		}
		/// <summary>
		/// Gets wether or not a child of this control has the focus
		/// </summary>
		public bool	ChildHasFocus
		{ get {	return mChildHasFocus; } }	
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
					RaiseEvent(BackColorChanged, new ColorEventArgs(mBackColor,	value));
					mBackColor = value;					
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
					RaiseEvent(BorderColorChanged, new ColorEventArgs(mBorderColor,	value));
					mBorderColor = value;			

				}
			}
		}
		/// <summary>
		/// Bool determines wether to render using textures or vectors
		/// </summary>
		public bool	IsTextured
		{
			get	{ return mIsTextured; }
			set
			{
				if (mIsTextured	!= value)
				{
					RaiseEvent(IsTexturedChanged, new BoolEventArgs(mIsTextured, value));
					mIsTextured	= value;
				}
			}
		}
		/// <summary>
		/// Gets or sets the genreal texture of the control
		/// </summary>
		public GeneralTextureCell TextureCell
		{
			get	{ return mTextureCell; }
			set	{ mTextureCell = value;	}
		}
		/// <summary>
		/// Gets or sets the display mode of the texture in the control
		/// </summary>
		public ETextureMode	TextureMode
		{
			get	{ return mTextureMode; }
			set
			{
				if (mTextureMode !=	value)
				{
					RaiseEvent(TextureModeChanged, new TextureModeEventArgs(mTextureMode, value));
					mTextureMode = value;
				}
			}
		}
		/// <summary>
		/// Gets / sets the TextureId of the control
		/// </summary>
		public int TextureID
		{
			get { return mTextureID; }
			set
			{
				if (mTextureID > -1)
				{
					if (mTextureID != value)
					{
						RaiseEvent(TextureCellIDChanged, new IntEventArgs(mTextureID, value));
						mTextureID = value;
					}
				}
			}
		}
		public List<GameControl> ChildControls
		{
			get { return mChildren; }
		}
		#endregion	 
		#region	Methods

			#region	Public Methods
				#region	NonOverridable

		///	<summary>
		///	Draws itself and then any children
		///	</summary>
		///	<param name="theArgs"></param>
		public void	Draw(ControlSpriteBatchArgs	theArgs)
		{
			if (mIsVisible == true)
			{
				if (mIsTextured)
				{
					InnderDrawTextured(theArgs);
				}
				else
				{
					InnerDrawNotTextured(theArgs);
				}

				if (mChildren.Count>0)
				{
					for (int i = mChildren.Count - 1; i > 0; i--)
					{
						mChildren[i].Draw(theArgs);	
					}								
				}									
			}

		}  
		///	<summary>
		///	this causes	event cascades up from the deepest control if anything has the focus
		///	</summary>
		public void	RemoveFocus(FocusMessageArgs theArgs)
		{
			foreach	(GameControl gt	in mChildren)
			{
				gt.RemoveFocus(theArgs);
			}


			if (mGotFocus == true)
			{
				mGotFocus =	false;		// only	set	it here	for	the	sake of	the	input args
				ProcessSelfLostFocus(theArgs);
				RaiseEvent(LostFocus, new GameControlEventArgs(this));
			}
			mChildHasFocus = false;
			
		}	
		/// <summary>
		/// Moves down the structure of controls. If a control hasnt got the focus then it gives it the focus.
		/// It then determines wether child control gets focus. if so, it establishes it
		/// </summary>
		/// <param name="theArgs"></param>
		public void	EstablishFocus(FocusMessageArgs  theArgs)
		{
			// Only	the	control	that has focus has its mHasFocus = true	
			// establish control with focus	
			
			// we need to take the focus in	this control
			// a child control may then	take it	but	we need	it now

			if (mGotFocus == false)
				ProcessSelfGainedFocus(theArgs);

			if (mChildren.Count	> 0)
			{
				bool hit = false;
				int	counter	= 0;
				do
				{
					if (mChildren[counter].DetermineIfControlGetsFocus(theArgs))
					{
						hit	= true;
						mChildHasFocus = true;
						ProcessChildGotFocus(theArgs);	// must	come first because got focus events	need chance	to fire
						// if they fire	after then final gotfocus event	fired wont have	focus
						mChildren[counter].EstablishFocus(theArgs);

						MoveControlToFront(counter);
						counter	= mChildren.Count;
					}
					else					
					counter++;
				}
				while (counter < mChildren.Count);
				if (hit	== false)
				{ mChildHasFocus = false; }				
			}
			else
			{
				mChildHasFocus = false;
			}


			
			if (mChildHasFocus	== false)
			{
				//make sure	none of	them have focus
				//and process the input	as it is for this control
				if (mChildren.Count>0)
				{
					foreach	(GameControl gc	in mChildren)
					{
						if (gc.HasFocus||gc.ChildHasFocus)
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
					if (mChildren[counter].HasFocus	|| mChildren[counter].ChildHasFocus)
						mChildren[counter].RemoveFocus(theArgs);
					counter++;
				}
			}	 
		} 
		public void	AddChildControl(GameControl	Child)
		{
			if (Child != null)
			{
				if (mChildren == null)
				{
					mChildren =	new	List<GameControl>();
				}
				if (!mChildren.Contains(Child))
				{						
					SubscribeToChildsEvents(Child);
					mChildren.Add(Child);
					RaiseEvent(ChildControlAdded,new GameControlEventArgs(Child));
				}
			}
		}
		public void RemoveChildControl(GameControl child)
		{
		 if (mChildren.Contains(child))
		 {
			 UnsubscribeFromChildEvents(child);
			 mChildren.Remove(child);
			 RaiseEvent(ChildControlRemoved,new GameControlEventArgs(child));
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
		public List<GameControl> GetChildControlsRecursive()
		{
			List<GameControl> retVal = new List<GameControl>();

			foreach (GameControl gc in mChildren)
			{	
				retVal.Add(gc);
				retVal.AddRange(gc.GetChildControlsRecursive());
			}

			return retVal;	   
		}

				#endregion	
				#region	Virtual
		public virtual bool	DetermineIfControlGetsFocus(FocusMessageArgs theArgs)
		{
			bool retVal	= false;
			//ToDo:	write in enabled / disabled	states
			if (IsVisible(theArgs.Camera,theArgs.screenDimensions))
				retVal = IsAbsolutePositionInsideControl(theArgs.mousePosition);
			return retVal;
		}
		///	<summary>
		///	Test to	see	if the control is visible in the given rectangle in	coord space
		///	</summary>
		///	<param name="Origin"></param>
		///	<param name="Size"></param>
		///	<returns></returns>
		public virtual bool	IsVisible(Vector2 Origin, Vector2 Size)
		{
			bool retVal	= false;
			if (mIsVisible)
			{
				if (IsControlWithinRectangle(Origin, Size))
				{
					retVal = true;
				}
			}
			return retVal;

		}
				#endregion		  
			#endregion	
		
			#region	Protected Methods

				#region	NonOverridable
		protected Vector2 GetRelativePosition(Vector2 thePosition)
		{
			Vector2	retVal = new Vector2();
			retVal.X = thePosition.X - mPosition.X;
			retVal.Y = thePosition.Y - mPosition.Y;
			return retVal;
		}
		protected void MoveControlToFront(int index)
		{

			GameControl	temp = mChildren[index];
			mChildren.RemoveAt(index);
			mChildren.Insert(0,	temp);

		}
		protected void MoveControlToBack(int index)
		{
			GameControl	temp = mChildren[index];
			mChildren.RemoveAt(index);
			mChildren.Add(temp);
		} 
		protected bool IsControlWithinRectangle(Vector2	origin,	Vector2	dims)
		{
			bool retVal	= false;
			// if top above	bottom 

			//if bottom	below top

			// if left is left of right

			// and if right	is right of	left
			Vector2	absPos = GetAbsolutePosition;

			if (absPos.Y < (origin.Y + dims.Y))
			{
				if ((absPos.Y +	mSize.Y) > origin.Y)
				{
					if (absPos.X < (origin.X + dims.X))
					{
						if ((absPos.X +	mSize.X) > origin.X)
						{
							retVal = true;
						}
					}
				}
			}
			return retVal;
		}
		protected float	CalculateLayerDepth(int	NoDrawn)
		{
			float inverse =	1 /	(float)NoDrawn;
			float retVal = (float)0.1 +	(float)(inverse	/ 10); //ensures that layerdepth always	between	0.2	and	0.1	- ie at	the	front
			return retVal;

		}
				#endregion

				#region	Virtual
		protected virtual void SubscribeToChildsEvents(GameControl child) {/*ToDo: implement SignUpToChildEvents */}
		protected virtual void UnsubscribeFromChildEvents(GameControl child) { /*ToDo: Implement unsubscribe from Child Events */}
		///	<summary>
		///	Provides custom	drawing	for	vector drawing
		///	</summary>
		///	<param name="theArgs"></param>
		protected virtual void InnerDrawNotTextured(ControlSpriteBatchArgs theArgs)
		{
			//Draw Border
			VectorDraw.DrawRectangleEdge(mPosition,	mSize, mBorderColor, 1,	theArgs.theSpriteBatch,	CalculateLayerDepth(theArgs.NocontrolsDrawn));
			theArgs.NocontrolsDrawn++;
			//Draw Background
			VectorDraw.DrawRectangleFilled(mPosition, mSize, mBackColor, theArgs.theSpriteBatch, CalculateLayerDepth(theArgs.NocontrolsDrawn));
			theArgs.NocontrolsDrawn++;

		} 
		///	<summary>
		///	Provides custom	drawing	for	texture	drawing
		///	</summary>
		///	<param name="theArgs"></param>
		protected virtual void InnderDrawTextured(ControlSpriteBatchArgs theArgs)
		{
			SpriteBatch	sb = theArgs.theSpriteBatch;

			switch (mTextureMode)
			{
				case ETextureMode.AsIs:
					mTextureCell.DrawAsIs(sb,GetAbsolutePosition, CalculateLayerDepth(theArgs.NocontrolsDrawn));
					theArgs.NocontrolsDrawn++;
					break;
				case ETextureMode.Stretch:
					

					break;
				case ETextureMode.Tile:
					mTextureCell.DrawTiled(sb, GetAbsoluteRectangle, CalculateLayerDepth(theArgs.NocontrolsDrawn));
					theArgs.NocontrolsDrawn++;
					break;

				case ETextureMode.Zoom:

					break;
			}			

		} 
		///	<summary>
		///	Allows a control to	listen for cetain commands aimed at	a container
		///	</summary>
		///	<param name="theArge"></param>
		protected virtual void ProcessChildGotFocus(FocusMessageArgs theArgs)
		{
			//when a child control has the focus this control still needs to recieve input
			//some commands may trigger responsdes ... eg altf4 is managed by the container control
			mChildHasFocus = true;
			SubscribeToEventManager();
		}  
		///	<summary>
		///	This only fires	when the control directly gains	the	focus
		///	</summary>
		///	<param name="theArgs"></param>
		protected virtual void ProcessSelfGainedFocus(FocusMessageArgs theArgs)
		{
			mBackColor = Color.Pink;
			mGotFocus =	true;
			SubscribeToEventManager();

			RaiseEvent(GotFocus, new GameControlEventArgs(this));

		} 
		///	<summary>
		///	this fires when	the	control	loses the focus
		///	</summary>
		///	<param name="theArgs"></param>
		protected virtual void ProcessSelfLostFocus(FocusMessageArgs theArgs)
		{
			mGotFocus =	false;
			mBackColor = Color.SeaGreen;
			UnsubscribeFromEventManager();
			RaiseEvent(LostFocus, new GameControlEventArgs(this));

		}

		protected virtual void SubscribeToEventManager()
		{
			EventManager em	= EventManager.GetInstance();
			em.KeyPressed += HandleKeyPressed;
			em.KeyReleased += HandleKeyRelseased;
			em.MouseButtonPressed += HandleMouseButtonPressed;
			em.MouseButtonReleased += HandleMouseButtonReleased;
			em.MousePositionChanged	+= HandleMousePositionChanged;
			em.MouseWheelScrolled += HandleMouseWheelScrolled;

		}  
		protected virtual void UnsubscribeFromEventManager()
		{
			EventManager em = EventManager.GetInstance();
			em.KeyPressed -= HandleKeyPressed;
			em.KeyReleased -= HandleKeyRelseased;
			em.MouseButtonPressed -= HandleMouseButtonPressed;
			em.MouseButtonReleased -= HandleMouseButtonReleased;
			em.MousePositionChanged -= HandleMousePositionChanged;
			em.MouseWheelScrolled -= HandleMouseWheelScrolled;

		}
				#endregion	 
			#endregion

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

		#region IDisposable Members

		private bool mIsDisposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!mIsDisposed)
			{
				if (disposing)
				{
					//dispose all child controls

					foreach (GameControl gc in mChildren)
					{
						UnsubscribeFromChildEvents(gc);
						gc.Dispose();
					}
					//unregister from any events

					mChildren = null;
					mTextureCell = null;
					mParent = null;

					if (mGotFocus || mChildHasFocus)
					{
						UnsubscribeFromEventManager();
					}
				}								  
			}

		}

		public void Dispose()
		{
			Dispose(true);
		}

		#endregion
	}



}

