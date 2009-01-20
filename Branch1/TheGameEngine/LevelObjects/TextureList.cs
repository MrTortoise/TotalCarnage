using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using CommonObjects;

namespace TheGameEngine
{
    public class TextureList
    {
        public ContentManager mContentManager;
        public List<Texture2D> Textures;

        public TextureTranslator mT = new TextureTranslator();        

        public TextureList(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            Textures = new List<Texture2D>();
        }

        public Texture2D this[int index]
        {
            get { return Textures[index]; }
            set { Textures[index] = value; }
        }


        public void BulkImport(params eTexture[] textureFileNames)
        {            
            Texture2D t;
            Textures.Capacity = textureFileNames.GetLength(0);
            for (int i = 0; i < textureFileNames.GetLength(0) ; i++)
            {
                t = mContentManager.Load<Texture2D>(mT.GetDescription(textureFileNames[i]));
                Textures.Insert(i, t);
            }                
        }
    }
}
