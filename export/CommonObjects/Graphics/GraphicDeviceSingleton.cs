using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Threading;
using System.Configuration;

namespace CommonObjects.Graphics
{
	/// <summary>
	/// multithreaded class that exposes the graphics device as a static.
	/// Manages objects dependant on Graphics device (lifecycle) ... eg lists of textureLists that other managers hold references to
	/// </summary>
	class GraphicDeviceSingleton
	{
		//ToDo: implement I disposable to manage no references to general texture.
		protected static  GraphicDeviceSingleton mInstance;
		protected static  GraphicsDevice mGraphicsDevice;
		protected  object mLock;
		private   bool mIsLoaded;
		protected   float mVUnit;
		protected  float mHUnit;
		protected int mNoHorizontalUnits;
		protected  int mViewportWidth;
		protected  int mViewportHeight;

		protected Dictionary<int, GeneralTextureList > mTextureLists = new Dictionary<int, GeneralTextureList >();

		#region Constructor Related

		protected void GraphicsDeviceSingleton()
		{
			Initialise();
		}

		public static GraphicDeviceSingleton GetInstance()
		{
			if (mInstance == null)
			{ throw new Exception("Singleton not yet created, use other constructor."); }
			return mInstance;			
		}

		public static GraphicDeviceSingleton GetInstance(GraphicsDevice theDevice)
		{
			if (mInstance != null)
			{ throw new Exception("Singleton already created, use other constructor."); }

			mInstance=new GraphicDeviceSingleton( );
			mGraphicsDevice  = theDevice;						
			return mInstance; 		
		}

		public void Initialise()
		{
			if (mIsLoaded)
			{ throw new Exception("Tried to initialise after already loaded"); }
			//sign up to events	
			mGraphicsDevice.DeviceReset+=new EventHandler(mGraphicsDevice_DeviceReset);
			SetNoHorizontalUnits();				
		}

		#endregion

		void mGraphicsDevice_DeviceReset(object sender, EventArgs e)
		{
			try
			{
				Monitor.Enter(this);
				GraphicsDevice gd = (GraphicsDevice)sender;
				mGraphicsDevice = gd;
			}
			catch (Exception ex)
			{ throw ex; }
			finally
			{ Monitor.Exit(this); }
			SetNoHorizontalUnits();
			Load();
		}

		public  GraphicsDevice graphicsDevice
		{
			get { return mGraphicsDevice; }
		}

		public int NoHorizontalUnits
		{ get { return mNoHorizontalUnits; } }

		public int ViewPortWidth
		{ get { return mViewportWidth; } }

		public int ViewPortHeight
		{ get { return mViewportHeight; } }

		public float vUnit
		{ get { return mVUnit; } }

		public float hUnit
		{ get { return mHUnit; } }

		public float aspectRatio
		{ get { return mGraphicsDevice.Viewport.AspectRatio; } }


		public void AddTextureList(eGeneralTextureList  theList, GeneralTextureList theTextureList)
		{
			if (mTextureLists.ContainsKey((int)theList) || mTextureLists.ContainsValue(theTextureList))
			{ throw new ArgumentException("Either ID or List is already in the dictionary of lists"); }

			mTextureLists.Add((int)theList, theTextureList);	  
		}

		public GeneralTextureList GetGeneralTextureList(eGeneralTextureList theList)
		{
			return mTextureLists[(int)theList];
		}

		public GeneralTexture GetGeneralTexture(eGeneralTextureList theList, int theID)
		{
			return mTextureLists[(int)theList][theID]; 
		}

		/// <summary>
		/// Should only be run externally when all texture lists have been added.
		/// </summary>
		public  void Load()
		{
			foreach (GeneralTextureList gtl in mTextureLists.Values)
			{
				gtl.Load(mGraphicsDevice);
			}

		}
			
		protected void SetNoHorizontalUnits()
		{
			try
			{
				Monitor.Enter(this);
				mViewportHeight = mGraphicsDevice.Viewport.Height;
				mViewportWidth = mGraphicsDevice.Viewport.Width;
				mNoHorizontalUnits = Convert.ToInt32(ConfigurationSettings.AppSettings["NoHorizontalUnits"]);				;
				mHUnit = mViewportWidth / mNoHorizontalUnits;
				mVUnit = mViewportHeight / (mGraphicsDevice.Viewport.AspectRatio * mNoHorizontalUnits);
			}
			catch (Exception ex)
			{
				throw new Exception("Error whilst setting No Horizontal Units - multithreaded.",ex);
			}
			finally
			{ Monitor.Exit(this); }

		}



	   

		


	}
}
