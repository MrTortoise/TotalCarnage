using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// A list of general textures ... holds the texture set for a map.
    /// can be referenced by id ... allows for reuse
    /// </summary>
    public class DetailedTextureList
    {

        protected List<DetailedTexture> mTextures;

        //public DetailedTextureList(int ID, string Name)
            public DetailedTextureList()
        {
            mTextures = new List<DetailedTexture>();
        }
    
       
        /// <summary>
        /// Gets a detailed texture at a given index
        /// will error if index out of range
        /// </summary>
        /// <param name="index"></param>
        /// <returns>DetailedTexture</returns>
        public DetailedTexture Get(int index)
        {
            return mTextures[index];
        }

        /// <summary>
        /// Adds a texture to the list
        /// texture id must = items index
        /// </summary>
        /// <param name="Texture">the texture to be added</param>
        public void Add(DetailedTexture Texture)
        {
            if (Texture.ID == mTextures.Count)
            {
                mTextures.Add(Texture);
            }
            else
            {
                throw new Exception("Tred to add detail texture to list with invalid ID");
            }
        }
        

    }
}
