﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

using CommonObjects.Graphics;

namespace CommonObjects
{
    /// <summary>
    /// This class manages the rendering and updating of the tiles in the map
    /// All ties will be rendered between 0 and 0.1
    /// The height of the tiles will be determined by their index int he tileLayerList
    /// </summary>
    public class Map : IGameDrawable, IGraphicsUpdateable   
    {
		//ToDo Implement IAgroGarbageCollection
		//ToDo: rewrite from scratch .... the lists need keys if i am basically adding elements to them arbitrarily via lazy loading

        #region Fields
        protected GeneralTextureList mTextures;
        protected TextureAnimationList mAnimations;
        protected List<TextureAnimationInstance> mAnimationInstances;
        protected MapTileList mTiles;
        protected MapTileLayerList mTileLayers;
		

        #endregion
        #region constructor
        /// <summary>
        /// basic constructor, initialises the tileLayerList
        /// </summary>
        public Map()
        {
            mTextures = new GeneralTextureList();
            mAnimations = new TextureAnimationList();
            mTiles = new MapTileList();
            mTileLayers = new MapTileLayerList();
        }
        #endregion

        #region	properties
		///	<summary>
		///	Gets the GeneralTextureList	for	the	map		   /// 
		///	</summary>
		public GeneralTextureList Textures
		{
			get	{ return mTextures;	}
		}
		///	<summary>
		///	gets the textureanimationlist for the map
		///	Change manually	at peril
		///	</summary>
		public TextureAnimationList	TextureAnimations
		{
			get	{ return mAnimations; }
 
		}

		///	<summary>
		///	gets the MapTileList for the map
		///	change at own peril
		///	</summary>
		public MapTileList MapTiles
		{
			get	{ return mTiles; }
		}

		///	<summary>
		///	gets the MapTileLayers for the map
		///	change at own peril
		///	</summary>
		public MapTileLayerList	MapTileLayers
		{ get {	return mTileLayers;	} }

		#endregion	  
       
        #region Population Methods
        public void AddGeneralTexture(GeneralTexture theTexture)
        {
                mTextures.Add(theTexture);
        }

        /// <summary>
        /// adds the texture animation to the list
        /// also adds any new general textures to the general texture list
        /// </summary>
        /// <param name="theAnimation"></param>
        public void AddTextureAnimation(TextureAnimation theAnimation)
        {
            mAnimations.AddAnimation(theAnimation);
            AddGeneralTexture(theAnimation.GeneralTexture); 
            
        }

        public void AddtextureAnimationInstance(TextureAnimationInstance tai)
        {
            if (mAnimationInstances.Contains(tai) == false)
            {
                mAnimationInstances.Add(tai);
            }

            AddTextureAnimation(tai.TextureAnimation);
        }

        /// <summary>
        /// Adds the map tile and also adds any new child objects to the relevant lists
        /// </summary>
        /// <param name="theMapTile"></param>
        public void AddMapTile(MapTile theMapTile)
        {
            mTiles.Add(theMapTile);

            foreach (TextureAnimationInstance ti in theMapTile)
            {
                AddtextureAnimationInstance(ti);
            }
        }

        /// <summary>
        /// Adds a new MapTileLayer and also any new child objects to their lists
        /// </summary>
        /// <param name="theMapTileLayer"></param>
        public void AddMapTileLayer(MapTileLayer theMapTileLayer)
        {
            mTileLayers.Add(theMapTileLayer);

            foreach (MapTile m in theMapTileLayer)
            {
                if (mTiles.Contains(m) == false)
                { AddMapTile(m); }
            }
        }
#endregion

		#region IGameDrawable Members
		protected bool mIsVisible = true;

		public void Draw(DrawingArgs theDrawingArgs)
		{
			mTileLayers.Draw(theDrawingArgs);
		}

		public bool IsVisible
		{
			get { return mIsVisible; }
		}

		public void SetVisibility(bool theVisibility)
		{
			mIsVisible = theVisibility;
		}

		#endregion

		#region IGraphicsUpdateable Members

		protected bool mIsGraphicsActive = true;

		public void UpdateGraphics(GraphicsUpdateArgs theArgs)
		{	  		
            foreach (TextureAnimationInstance ti in mAnimationInstances)
            {
				ti.UpdateGraphics(theArgs);
            }		        
		}

		public bool IsGraphicsActive
		{
			get { return mIsGraphicsActive; }
		}

		public void SetGraphicsActive(bool value)
		{
			mIsGraphicsActive = value;
		}

		#endregion

		#region IGameObject Members

		public int ID
		{
			get { throw new NotImplementedException(); }
		}

		public string Name
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
	}
}
