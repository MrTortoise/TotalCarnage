using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonObjects.Graphics
{
	class TileLayer	:IGameDrawable, IEnumerable<MapTile>, IEnumerator<MapTile>
	{
		#region Fields
		protected int mID;
		protected int[,] mLayout;
		protected List<Tile> mTiles;
		protected int mTileEnumerator = -1;
		protected float mLayerDepth;
		protected Vector2 mTileDims;
		#endregion

		#region Constructor

		public TileLayer(int theID, int[,] theLayout, Vector2 theTileDims, List<Tile> theTiles)
		{


		}

		#endregion


		#region IGameDrawable Members

		public void Draw(DrawingArgs theDrawingArgs)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable<MapTile> Members

		public IEnumerator<MapTile> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerator<MapTile> Members

		public MapTile Current
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerator Members

		object System.Collections.IEnumerator.Current
		{
			get { throw new NotImplementedException(); }
		}

		public bool MoveNext()
		{
			throw new NotImplementedException();
		}

		public void Reset()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IGameDrawable Members

		public bool IsVisible
		{
			get { throw new NotImplementedException(); }
		}

		public void SetVisibility(bool theVisibility)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
