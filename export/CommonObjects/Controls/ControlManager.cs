﻿using System;
using System.Collections.Generic;
using System.Text;
using CommonObjects.Controls;
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

		
		
		#endregion

		#region Constructors
		public ControlManager( GeneralTextureList theTextureList)
		{
			EventManager em = EventManager.GetInstance(); 
			em.AttemptedFocusEvent += OnAttemptedGetFocusEvent;
		}
		#endregion


		#region Methods	
	
		public void AddControl(GameControl theControl)
		{			
			mChildControls.Add(theControl);
			mAllControls.Add(theControl);

			ProcessTextureCellID(theControl);

			theControl.ChildControlAdded += OnChildControlAddedEventHandler;
			theControl.TextureCellIDChanged += OnControltextureCellIDChanged;
			theControl.ChildControlRemoved += OnChildControlRemovedEventHandler;

			if (theControl.ChildControls.Count > 0)
			{
				foreach (GameControl gc in theControl.ChildControls)
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
			GameControl theControl = source as GameControl;
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

		#endregion	

		#region IGameDrawable Members

		public void Draw(DrawingArgs theDrawingArgs)
		{
			//ToDo: draw in control manager could use same spritebatch as game?

			SpriteBatch batch = new SpriteBatch(theDrawingArgs.GraphicsDevice);
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

		#region IEquatable<ControlManager> Members

		public bool Equals(ControlManager other)
		{
			//ToDo: Implement interface		+ overridden member
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
					em.AttemptedFocusEvent -= OnAttemptedGetFocusEvent;
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
	}
}