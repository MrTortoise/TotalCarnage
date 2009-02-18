using System;
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
	public class ControlManager : IGameDrawable, IGameUpdateable,IEquatable<ControlManager>
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
		protected List<GameControl> mAllControls;
		
		#endregion

		#region Constructors
		public ControlManager(EventManager theEventManager)
		{
			if (theEventManager == null)
				throw new ArgumentNullException("Cant pass a null event manager into control manager in constructor");

			theEventManager.AttemptedFocusEvent += OnAttemptedGetFocusEvent;
		}
		#endregion


		#region Methods	
	
		public void AddControl(GameControl theControl)
		{
			if (mAllControls == null)
				mAllControls = new List<GameControl>();
			mAllControls.Add(theControl);

		}

		#region	Protected
		protected void MoveControlToFront(int index)
		{
			List<GameControl> retVal = new List<GameControl>();

			retVal.Add(mAllControls[index]);
			for	(int i = 0;	i <	index; i++)
			{
				retVal.Add(mAllControls[i]);
			}
			for	(int i = index + 1;	i <	mAllControls.Count;	i++)
			{
				retVal.Add(mAllControls[i]);
			}
		}
		protected void MoveControlToBack(int index)
		{
			List<GameControl> retVal = new List<GameControl>();
			for	(int i = 0;	i <	index; i++)
			{
				retVal.Add(mAllControls[i]);
			}
			for	(int i = index + 1;	i <	mAllControls.Count;	i++)
			{
				retVal.Add(mAllControls[i]);
			}
			retVal.Add(mAllControls[index]);

		}

		#endregion			   
		#endregion
		#region	event Handlers

		protected void OnAttemptedGetFocusEvent(object e, FocusMessageArgs args)
		{
			bool gotFocus = false;
			int counter = 0;
			int theControl = 0;
			while (counter < mAllControls.Count)
			{
				if (mAllControls[counter].DetermineIfControlGetsFocus(args))
				{
					gotFocus = true;
					theControl = counter;
					counter = mAllControls.Count;
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
				mAllControls[theControl].EstablishFocus(args);
				for (int i = 0; i < mAllControls.Count; i++)
				{
					if (i != theControl)
					{
						if (mAllControls[i].HasFocus)
							mAllControls[i].RemoveFocus(args);
					}


				}
			}
			else
			{
				foreach (GameControl gc in mAllControls)
				{
					gc.RemoveFocus(args);
				}
			}
			
			
		}

		#endregion	

		#region IGameDrawable Members

		public void Draw(DrawingArgs theDrawingArgs)
		{

			SpriteBatch batch = new SpriteBatch(theDrawingArgs.GraphicsDevice);
			batch.Begin(SpriteBlendMode.AlphaBlend);

			int counter = mAllControls.Count - 1;
			while (counter > -1)
			{
				mAllControls[counter].Draw(new ControlSpriteBatchArgs(batch));
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

		#region IGameLoadable Members

		public void Load(GraphicsDevice theGraphicsDevice)
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
	}
}
