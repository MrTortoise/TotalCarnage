using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{
    /// <summary>
    /// This is an abstract class purley to seperate tilesets away from objects
    /// </summary>
    public class TileSet 
    {
        #region constants
        public int mTileDims = 64;
        #endregion

        #region properties

        public List<Tile> Tiles;       
       

        #endregion

        public TileSet()
        {
            Tiles = new List<Tile>();
        }

        #region Public Methods

        public void BulkInsert(params Tile[] theTiles)
        {
            foreach (Tile t in theTiles)
            {
                Tiles.Add(t);
            }
        }

        public Tile this[int index]
        {
            get { return Tiles[index]; }
            set {
                if (Tiles.Capacity < index)
                {
                    Tiles.Capacity = index;
                }
                Tiles[index] = value;
            }
        }

        
        public void Draw(int index, SpriteBatch theSpriteBatch, GameTime theGameTime, Vector2 thePosition)
        {
            Tiles[index].Draw(theSpriteBatch, theGameTime, thePosition);
        }

        public void Draw(SpriteBatch theSpriteBatch, GameTime theGameTime, Vector2 thePosition)
        {
            foreach (Tile t in Tiles)
            {
                t.Draw(theSpriteBatch, theGameTime, thePosition);
            }            
        }

      

        #endregion



    }
}
