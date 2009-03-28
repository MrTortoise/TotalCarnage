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
			if (!mTextureCells.ContainsKey(theCell.ID))
				mTextureCells.Add(theCell.ID, theCell);				
		}

		public List<GeneralTextureCell> Values()
		{
			List<GeneralTextureCell> retVal = new List<GeneralTextureCell>();

			foreach (int k in mTextureCells.Keys)
			{
				retVal.Add(mTextureCells[k]);
			}

			return retVal;

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

		public bool ContainsKey(int key)
		{
			return mTextureCells.ContainsKey(key);
		}

	}
}
