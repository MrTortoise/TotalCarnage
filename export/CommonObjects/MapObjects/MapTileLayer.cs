using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using CommonObjects.Graphics;

namespace CommonObjects
{
    public class MapTileLayer :  IEnumerable<MapTile>, IEnumerator<MapTile>//, IGameDrawable
    {
		//ToDo Implement IAgroGarbageCollection
        #region Fields

        protected int mID;
        protected int[,] mLayout;
        protected List<MapTile> mMapTiles;
        protected int mMapTileEnumerator = -1;
        protected float mLayerDepth;
        protected Vector2 mTileDims;
        

        #endregion
        
        #region Constructors

        public MapTileLayer(int theID, Vector2 theTileDims, int[,] theLayout,  List<MapTile> theTiles)
        {
            if ((object)theTiles == null)
            { throw new NullReferenceException("Tried to create a tile layer from a null tile list"); }                       
            
            mID = theID;
            mLayout = theLayout;
            mMapTiles = theTiles; 
            mTileDims = theTileDims;

            foreach (MapTile t in mMapTiles)
            {
                t.SetScale(mTileDims);
            }            
        }
        #endregion

        #region Properties
        /// <summary>
        /// returns the Id of the TileLayer
        /// </summary>
        public int ID
        { get { return mID; } }

        /// <summary>
        /// returns the array representing the ID's of the detailedTiles
        /// </summary>
        public int[,] Layout
        { get { return mLayout; } }


        /// <summary>
        /// Set by the tileLayers as new tiles are added
        /// </summary>
        public float LayerDepth
        {
            get { return mLayerDepth; }
            set { mLayerDepth = value; }
        }

        /// <summary>
        /// The tile dimensions in pixels (assumption is square)
        /// </summary>
        public Vector2  TileDims
        { get { return mTileDims; } }

        /// <summary>
        /// the number of tiles horizontally in the layer
        /// </summary>
        public int NoTilesAccross
        { get { return mLayout.GetLength(0); } }

        /// <summary>
        /// no of tiles vertically in a layer
        /// </summary>
        public int NoTilesVertical
        { get { return mLayout.GetLength(1); } }

        /// <summary>
        /// total width in pixels of the tile layer
        /// </summary>
        public int WidthInPixels
        {
            get { return NoTilesAccross * (int)(mTileDims.X) ;}
        }

        /// <summary>
        /// total height in pixels of the tile layer
        /// </summary>
        public int HeightInPixels
        { get { return NoTilesVertical * (int)mTileDims.Y ; } }
         
 
        #endregion

        #region Public Methods

        public void AddMapTile(MapTile theTile)
        {
            if ((object)theTile ==null)
            {throw new NullReferenceException("Tried to add a null MapTile");}

            foreach (MapTile m in mMapTiles)
            {
                if (m.ID == theTile.ID)
                { throw new Exception("Tried to add MapTile with existing ID"); }
            }

            if (this.Contains(theTile))
            { throw new Exception("Tried to add an already existing tile"); }

            mMapTiles.Add(theTile);

        }
        public bool Contains(MapTile theMapTile)
        {            
            foreach (MapTile m in mMapTiles)
            {
                if (m.Equals(theMapTile))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Private Methods

        #endregion

        #region IGameDrawable Members

        public void Draw(DrawingArgs theDrawingArgs)
        {			
            Vector2 position;
			

            SpriteBatch theBatch = new SpriteBatch(GraphicDeviceSingleton.GetInstance().graphicsDevice);
            theBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Texture, SaveStateMode.None, theDrawingArgs.Camera.Transform);		

			Vector2 boundary = mTileDims;

            for (int x = 0; x < mLayout.GetLength(0); x++)
            {
                for (int y = 0; y < mLayout.GetLength(1); y++)
                {
					position.X = x * mTileDims.X;
					position.Y = y * mTileDims.Y;
					if (theDrawingArgs.Camera.IsVisible(position, boundary))
					{	 						
						spriteBatchArgs sb = new spriteBatchArgs(theBatch);
						sb.Position = position;
						sb.LayerDepth = mLayerDepth;
						mMapTiles[mLayout[x, y]].Draw(sb);
					}
                }
            }
            theBatch.End();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()			
        {
			
            mMapTiles = null;			
			
        }

        #endregion


        #region IEnumerable<MapTile> Members

        public IEnumerator<MapTile> GetEnumerator()
        {
            return (IEnumerator<MapTile>)this;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion

        #region IEnumerator<MapTile> Members

        public MapTile Current
        {
            get {return mMapTiles[mMapTileEnumerator]; }
        }

        #endregion



        #region IEnumerator Members

        object IEnumerator.Current
        {
            get { return mMapTiles[mMapTileEnumerator]; }
        }

        public bool MoveNext()
        {
            mMapTileEnumerator++;
            if (mMapTileEnumerator >= mMapTiles.Count)
            { return false; }
            else
            { return true; }
        }

        public void Reset()
        {
            mMapTileEnumerator = -1;
        }

        #endregion
    }
}
