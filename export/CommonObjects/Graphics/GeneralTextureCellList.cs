using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;			
using Microsoft.Xna.Framework.Graphics;
namespace CommonObjects
{
	class GeneralTextureCellList	
	{

		protected Dictionary<int, GeneralTextureCell> mTextureCells = new Dictionary<int, GeneralTextureCell>();

		public void Add(GeneralTextureCell theCell)
		{
			mTextureCells.Add(theCell.ID, theCell);
		}

		public void Remove(int id)
		{ mTextureCells.Remove(id); }

		public GeneralTextureCell this[int index]
		{
			get { return mTextureCells[index]; }
			set
			{
				mTextureCells[index] = value;
			}
		}

	}
}
