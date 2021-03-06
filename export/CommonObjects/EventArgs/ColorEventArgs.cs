﻿using System;
using Microsoft.Xna.Framework.Graphics;

namespace CommonObjects
{
	public class ColorEventArgs : EventArgs  
	{

		protected Color mOldValue;
		protected Color mNewValue;


		public ColorEventArgs(Color theOldValue, Color theNewValue)
		{
			mOldValue = theOldValue;
			mNewValue = theNewValue;
		}

		/// <summary>
		/// Returns the Old Value of the colour
		/// </summary>
		public Color OldValue
		{
			get { return mOldValue; }
			set { mOldValue = value; }
		}

		public Color NewValue
		{
			get { return mNewValue; }
			set { mNewValue = value; }
		}
	}
}
