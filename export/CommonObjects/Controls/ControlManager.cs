using System;
using System.Collections.Generic;
using System.Text;
using CommonObjects.Controls;
using CommonObjects.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

namespace CommonObjects.Controls
{
	/// <summary>
	/// This class regsters and unregisters controls to input events
	/// It is also signed up to the same events in order to decide which controls to effect
	/// does this via mouse position and mouse button presses
	/// 
	/// </summary>
	public class ControlManager : IGameDrawable, IGameUpdateable,IEquatable<ControlManager>	,IDisposable 
	{
		/*
		 *	 to intercept mouse button events to identify which control is being clicked on
		 *	 each control carries out the same functio down the child chain
		 *	 each control in turn has focus
		 *	 The control manager needs access to the viewport to identify control visibility
		 *	 via events
		 *	 All objects always update and manage their own updates
		 *	 Controls load themselves in constructor
		 * 
		 * */
		#region Fields
		//protected EventManager mEventManager;
		protected List<GameControl> mAllControls = new List<GameControl>();
		protected List<GameControl> mChildControls = new List<GameControl>();
		protected GeneralTextureCellList mTextureCells = new GeneralTextureCellList();
		protected GeneralTextureList mGeneralTextureList = new GeneralTextureList();
		protected bool mIsVisible = true;
		protected bool mIsActive = true;
		
		//ToDo: should this control manager not use texture cells? - GD singleton manages the texture objects now
		
		#endregion

		#region Constructors
		public ControlManager( GeneralTextureList theTextureList)
		{
			
			EventManager em = EventManager.GetInstance(); 
			//em.AttemptedFocusEvent += OnAttemptedGetFocusEvent;

			em.KeyPressed += OnEMKeyPressed;
			em.KeyReleased += OnEMKeyReleased;
			em.MouseButtonPressed += OnEMMouseButtonPressed;
			em.MouseButtonReleased += OnEMMouseButtonReleased;
			em.MousePositionChanged += OnEMMousePositionChanged;
			em.MouseWheelScrolled += OnEMMouseWheelScrolled;

			mGeneralTextureList = theTextureList;

		}
		#endregion


		#region Methods	
	
		public void AddControl(GameControl theControl)
		{			
			mChildControls.Add(theControl);
			mAllControls.Add(theControl);

			ProcessTextureCellID(theControl);

			theControl.ChildControlAdded +=	OnChildControlAddedEventHandler;
			theControl.TextureCellIDChanged	+= OnControltextureCellIDChanged;
			theControl.ChildControlRemoved += OnChildControlRemovedEventHandler;

			if (theControl.ChildControls.Count > 0)
			{
				foreach	(GameControl gc	in theControl.ChildControls)
				{
					ProcessNewControl(gc);
				}						  
			}
		}  

		#region	Protected
		protected void MoveControlToFront(int index)
		{								 
			GameControl temp = mChildControls[index];
			mChildControls.RemoveAt(index);
			mChildControls.Insert(0, temp);	   
		}
		protected void MoveControlToBack(int index)
		{
			GameControl temp = mChildControls[index];
			mChildControls.RemoveAt(index);
			mChildControls.Add(temp);
		}

		protected void ProcessTextureCellID(GameControl theControl)
		{
			//set the controls textureCell
			if (theControl.TextureID != -1)
			{
				if (mTextureCells.ContainsKey(theControl.TextureID))
				{
					theControl.TextureCell = mTextureCells[theControl.TextureID];
				}
				else
				{ throw new Exception("OnControltextureCellIDChanged raised by object that cannto cast to game control."); }
			}
		}

		/// <summary>
		/// To be run when a game control gets a new control added to it. NOT when a new control is added to the Control Manager
		/// </summary>
		/// <param name="theControl"></param>
		protected void ProcessNewControl(GameControl theControl)
		{
			//need to use recursion to add all child controls to the master control list
			if (theControl.ChildControls.Count > 0)
			{
				foreach (GameControl gc in theControl.ChildControls)
				{
					ProcessNewControl(gc);
				}
			}
			ProcessTextureCellID(theControl);

			mAllControls.Add(theControl);
			theControl.ChildControlAdded += OnChildControlAddedEventHandler;
			theControl.TextureCellIDChanged += OnControltextureCellIDChanged;
			theControl.ChildControlRemoved += OnChildControlRemovedEventHandler;
		}

		protected void ProcessRemovedControl(GameControl theControl)
		{
			//need to use recursion to add all child controls to the master control list
			if (theControl.ChildControls.Count > 0)
			{
				foreach (GameControl gc in theControl.ChildControls)
				{
					ProcessRemovedControl(gc);
				}
			}

			mAllControls.Remove(theControl);
			theControl.ChildControlAdded -= OnChildControlAddedEventHandler;
			theControl.TextureCellIDChanged -= OnControltextureCellIDChanged;
			theControl.ChildControlRemoved -= OnChildControlRemovedEventHandler;

		}


		#endregion			   
		#endregion
		#region Events
		/// <summary>
		/// It is up to the reciever of this event to manage held buttons
		/// </summary>
		public EventHandler<InputEventArgs> MouseButtonPressed;
		public EventHandler<FocusMessageArgs> AttemptedFocusEvent;
		/// <summary>
		/// It is up to the reciever of this event to manage held buttons
		/// </summary>
		public EventHandler<InputEventArgs> MouseButtonReleased;

		public EventHandler<InputEventArgs> MouseWheelScrolled;
		public EventHandler<InputEventArgs> MousePositionChanged;

		/// <summary>
		/// It is up to the reciever of this event to manage held buttons
		/// </summary>
		public EventHandler<InputEventArgs> KeyPressed;
		/// <summary>
		/// It is up to the reciever of this event to manage held buttons
		/// </summary>
		public EventHandler<InputEventArgs> KeyReleased;
		#endregion
		#region	event Handlers
		protected void OnChildControlAddedEventHandler(object source, GameControlEventArgs theArgs)
		{
			GameControl theControl = theArgs.Control;
			ProcessNewControl(theControl);

		}
		protected void OnChildControlRemovedEventHandler(object source, GameControlEventArgs theArgs)
		{
			GameControl theControl = theArgs.Control;
			throw new NotImplementedException() ;

		}
		protected void OnControltextureCellIDChanged(object source, IntEventArgs theArgs)
		{
			GameControl	theControl = source	as GameControl;
			ProcessTextureCellID(theControl);
		} 
		protected void OnAttemptedGetFocusEvent(object e, FocusMessageArgs args)
		{
			bool gotFocus = false;
			int counter = 0;
			int theControl = 0;
			while (counter < mChildControls.Count)
			{
				if (mChildControls[counter].DetermineIfControlGetsFocus(args))
				{
					gotFocus = true;
					theControl = counter;
					counter = mChildControls.Count;
				}								 
				counter++;
			}

			if (gotFocus == true)
			{
				/*MoveControlToFront(theControl);
				counter = 1;
				while (counter < mAllControls.Count)
				{
					if (mAllControls[counter].HasFocus == true)
						mAllControls[counter].RemoveFocus(args);
					counter++;
				}  */
				mChildControls[theControl].EstablishFocus(args);
				for (int i = 0; i < mChildControls.Count; i++)
				{
					if (i != theControl)
					{
						if (mChildControls[i].HasFocus)
							mChildControls[i].RemoveFocus(args);
					} 
				}
				MoveControlToFront(theControl);
			}
			else
			{
				foreach (GameControl gc in mChildControls)
				{
					gc.RemoveFocus(args);
				}
			}
			
			
		}

		protected void OnEMMouseButtonPressed(object s, InputEventArgs theArgs)
		{
			RaiseEvent(MouseButtonPressed, theArgs);
			GraphicDeviceSingleton gds = GraphicDeviceSingleton.GetInstance();
			Vector2 adjustedPosition = new Vector2();
			adjustedPosition.X=theArgs.CurrentMousePosition.X * gds.hUnit;
			adjustedPosition.Y = theArgs.CurrentMousePosition.Y * gds.vUnit;
			RaiseEvent(AttemptedFocusEvent,new FocusMessageArgs(adjustedPosition ,
																this,
																Vector2.Zero,
																new Vector2(gds.NoHorizontalUnits,
																	gds.NoHorizontalUnits*gds.aspectRatio )));	


		}
		/*protected void OnEMAttemptedFocusEvent(object s, FocusMessageArgs theArgs) {
			RaiseEvent(AttemptedFocusEvent, theArgs);
		}*/	 
		protected void OnEMMouseButtonReleased(object s, InputEventArgs theArgs) {
			RaiseEvent(MouseButtonReleased , theArgs);
		}
		protected void OnEMMouseWheelScrolled(object s, InputEventArgs theArgs) {
			RaiseEvent(MouseWheelScrolled , theArgs);
		}
		protected void OnEMMousePositionChanged(object s, InputEventArgs theArgs) {
			RaiseEvent(MousePositionChanged , theArgs);
		}
		protected void OnEMKeyPressed(object s, InputEventArgs theArgs) {
			RaiseEvent(KeyPressed , theArgs);
		}
		protected void OnEMKeyReleased(object s, InputEventArgs theArgs) {
			RaiseEvent(KeyReleased , theArgs);
		}

		protected void RaiseEvent(EventHandler<IntEventArgs> theEvent, IntEventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<IntEventArgs> temp = theEvent;
			if (temp != null)
				temp(this, theEventArgs);
		}
		protected void RaiseEvent(EventHandler<InputEventArgs> theEvent, InputEventArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<InputEventArgs> temp = theEvent;
			if (temp != null)
				temp(this, theEventArgs);
		}
		protected void RaiseEvent(EventHandler<FocusMessageArgs> theEvent, FocusMessageArgs theEventArgs)
		{
			//Copy to be threadsafe
			EventHandler<FocusMessageArgs> temp = theEvent;
			if (temp != null)
				temp(this, theEventArgs);
		}
		#endregion	

		#region IGameDrawable Members

		public void Draw(DrawingArgs theDrawingArgs)
		{
			// This assumes that the 'game' will never get drawn as a control - however perhaps a subset of 'game' can be?

			SpriteBatch batch = new SpriteBatch(GraphicDeviceSingleton.GetInstance().graphicsDevice);
			batch.Begin(SpriteBlendMode.AlphaBlend);

			int counter = mChildControls.Count - 1;
			while (counter > -1)
			{
				mChildControls[counter].Draw(new ControlSpriteBatchArgs(batch));
				counter--;
			}

				batch.End();
		}

		#endregion

		#region IGameUpdateable Members

		public void Update(UpdateArgs theUpdateArgs)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region	IEquatable<ControlManager> Members

		public bool	Equals(ControlManager other)
		{
			//ToDo:	Implement interface		+ overridden member
			throw new NotImplementedException();
		}

		#endregion	 

		
		#region IDisposable Members

		protected bool misDisposed = false;

		protected void Dispose(bool disposing)
		{

			if (!misDisposed)
			{
				if (disposing)
				{
					EventManager em = EventManager.GetInstance();
					//ToDo; Unregister from Event Manager events
					foreach (GameControl g in mChildControls)
					{
						g.Dispose();				

					}
				}
				misDisposed = true;
			}

		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);

		}

		#endregion

		#region IGameDrawable Members

		public bool IsVisible
		{
			get
			{
				return mIsVisible ;
			}
		}

		#endregion

		#region IGameDrawable Members

		//ToDo: rendering of control sis likley to be done in a seperate sequence thread - its all in the order of the end calls ...
		public void SetVisibility(bool theVisibility)
		{
			mIsVisible = theVisibility;
		}

		#endregion



		#region IGameUpdateable Members


		public bool IsActive
		{
			get { return mIsActive; }
		}

		public void SetActive(bool value)
		{
			mIsActive = value;
		}

		#endregion
	}
}
