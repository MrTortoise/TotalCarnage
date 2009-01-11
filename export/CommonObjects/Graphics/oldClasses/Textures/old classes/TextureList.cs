using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// unused class?
    /// </summary>
 /*   public class TextureList
    {
        protected List<GeneralTexture> mTextures;      

        public TextureList(int ID, string Name)
        {
            mID = ID;
            mName = Name;
            mTextures = new List<GeneralTexture>();
        }

        /// <summary>
        /// gets the id of the textu
        /// </summary>
        public int ID
        { get { return mID; } }

        public string Name
        { get { return mName; } }

        public GeneralTexture Get(int index)
        {
            return mTextures[index];
        }

        public void Add(GeneralTexture Texture)
        {
            if (Texture.ID != mTextures.Count)
            {
                throw new Exception("Tried to add texture with wrong id");
            }
            else
            {
                mTextures.Add(Texture);
            }
        }

        public void BulkImport(params GeneralTexture[] GeneralTextures)
        {   
            mTextures.Capacity = GeneralTextures.GetLength(0);
            for (int i = 0; i < GeneralTextures.GetLength(0); i++)
            {
                if (GeneralTextures[i].ID != mTextures.Count)
                {
                    throw new Exception("Tried to bulk inset texture with wron gid");
                }
                else
                {
                    mTextures.Add(GeneralTextures[i]);
                }
            }                
        }
    }*/
}
