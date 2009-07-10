using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;			
using Microsoft.Xna.Framework.Graphics;
using CommonObjects;
using System.Xml;
using System.Xml.Linq;
using System.Linq;


namespace CommonObjects.Graphics
{
    /// <summary>
    /// This class represents a list of general texture cell objects 
    /// and provides functionality to load from an xdoc or an xml file
    /// </summary>
	public class GeneralTextureCellList : ILoadXML 
	{

		protected Dictionary<int, GeneralTextureCell> mTextureCells = new Dictionary<int, GeneralTextureCell>();
        protected Dictionary<int, GeneralTexture> mTextures = new Dictionary<int, GeneralTexture>();

        /// <summary>
        /// Adds a generalTextureCell to the dictionary
        /// </summary>
        /// <param name="theCell"></param>
		public void Add(GeneralTextureCell theCell)
		{											   
			if (!mTextureCells.ContainsKey(theCell.ID))
				mTextureCells.Add(theCell.ID, theCell);				
		}

        /// <summary>
        /// returns the texturecells as a list
        /// </summary>
        /// <returns></returns>
		public List<GeneralTextureCell> Values()
		{
			List<GeneralTextureCell> retVal = new List<GeneralTextureCell>();

			foreach (int k in mTextureCells.Keys)
			{
				retVal.Add(mTextureCells[k]);
			}

			return retVal;

		}

        /// <summary>
        /// removes a general texture cell by ID
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// returns wether the dictionary contains this key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
		public bool ContainsKey(int key)
		{
			return mTextureCells.ContainsKey(key);
		}

        /// <summary>
        /// Sets the general texture cell dictionayr
        /// </summary>
        /// <param name="theTextures"></param>
        public GeneralTextureCellList(Dictionary<int, GeneralTexture> theTextures)
        {
            mTextures = theTextures;
        }

        #region ILoadXML Members

        protected bool mIsXMLLoaded;
        /// <summary>
        /// Gets wether or not the object has loaded an xdoc/xml file.
        /// </summary>
        public bool IsXMLLoaded
        {
            get { return mIsXMLLoaded; }
        }

        /// <summary>
        /// Popultes the List from a general texture cell XDocument
        /// </summary>
        /// <param name="theXML"></param>
        public void LoadFromXML(XDocument theXML)
        {
            var generalTextureCells = from i in theXML.Descendants("textureCell")
                                      select new GeneralTextureCell(Convert.ToInt32(i.Element("id").Value),
                                          mTextures[Convert.ToInt32(i.Element("textureID").Value)],
                                          new Vector2(Convert.ToSingle(i.Element("posX").Value),
                                              Convert.ToSingle(i.Element("posY").Value)),
                                              new Vector2(Convert.ToSingle(i.Element("cellSizeX").Value),
                                              Convert.ToSingle(i.Element("cellSizeY").Value)),
                                              new Vector2(Convert.ToSingle(i.Element("sizeX").Value),
                                              Convert.ToSingle(i.Element("sizeY").Value)),
                                              Convert.ToSingle(i.Element("rotation").Value));

            foreach (GeneralTextureCell gtc in generalTextureCells)
            {
                this.Add(gtc);
            }
            mIsXMLLoaded = true;
        }

        /// <summary>
        /// Popultes the List from a general texture cell xml file.
        /// </summary>
        /// <param name="thePath"></param>
        public void LoadFromXMLFile(string thePath)
        {
            XDocument xml = XDocument.Load(thePath);
            LoadFromXML(xml);
        }

        #endregion
    }
}
