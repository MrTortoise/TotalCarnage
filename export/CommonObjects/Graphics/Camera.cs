using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{


    public class Camera
    {
		//toDo: Implement static method to test wether something is visible by the camera

		//ToDo: this camera focusses its top left on a given position 
		// - Shouldn't it CENTER on a given position? - this requires a graphics device
		//ToDo: in the middle of figuring out the center / viewport issue
        #region Fields
        private  float mZoom = 1;
        private float mRotation = 0;                
		private Vector2 mCenter;		
		private GraphicsDevice mGraphicsDevice;
		private Vector2 mEffectiveViewportDimensions = new Vector2();
		private Vector2 mEffectiveViewportPosition = new Vector2();		

        private Matrix mTransform;

        #endregion

        #region Constructor

        public Camera(Vector2 thePosition,GraphicsDevice theGraphicsDevice)
        {				
			mGraphicsDevice = theGraphicsDevice;
			mGraphicsDevice.DeviceReset += new EventHandler(theGraphicsDevice_DeviceReset);
            SetCenter(thePosition);
        }

		void theGraphicsDevice_DeviceReset(object sender, EventArgs e)
		{
			SetCenter(mCenter);
		}

        #endregion

        #region Properties

		/// <summary>
		/// Gets a float repreenting the left most coordinate
		/// </summary>
		public float Left
		{ get { return mEffectiveViewportPosition.X; } }

		/// <summary>
		/// Gets a float representing the riht most coordinate
		/// </summary>
		public float Right
		{ get { return mEffectiveViewportPosition.X + mEffectiveViewportDimensions.X; } }

		/// <summary>
		/// Gets a float representing the bottommost coordinate
		/// </summary>
		public float Bottom
		{ get { return mEffectiveViewportPosition.Y + mEffectiveViewportDimensions.Y; } }

		/// <summary>
		/// Gets a flost representing the top most coordinate
		/// </summary>
		public float Top
		{ get { return mEffectiveViewportPosition.Y; } }

		/// <summary>
		/// Get a vector 2 representing the coordinate target of the camera
		/// </summary>
		public Vector2 Center
		{ get { return mCenter; } }
		
		/// <summary>
		/// Gets the transformation matrix that the psritebatch.Begin will use
		/// </summary>
        public Matrix Transform
        {
            get { return mTransform; }            
        }

		/// <summary>
		/// Gets or Sets the rotation of the camera relative to zero
		/// </summary>
        public float Rotation
        {
            get { return mRotation; }
            set { mRotation = value; }
        }

		/// <summary>
		/// Gets or Sets the zoom of the camera		/// 
		/// </summary>
		/// <exception cref="System.ArgumentOutOfRangeException">Fires when zoom <=0</exception>
        public float Zoom
        {
            get { return mZoom; }
            set {
				if (value <= 0)
					throw new ArgumentOutOfRangeException("tried to set a negative zoom on camera");
				mZoom = value; 
			}
        }

        #endregion

		/// <summary>
		/// Sets the coordinates that the camera is looking at
		/// </summary>
		/// <param name="thePosition"></param>
        public void SetCenter(Vector2 thePosition)
        {
			// need to establish the effective scren size
			mEffectiveViewportDimensions.X=mGraphicsDevice.Viewport.Width/mZoom;
			mEffectiveViewportDimensions.Y=mGraphicsDevice.Viewport.Height/mZoom;

			// then need to figure out the boundaries of this screen in space
			mCenter = thePosition;
			mEffectiveViewportPosition.X = mCenter.X - mEffectiveViewportDimensions.X / 2;
			mEffectiveViewportPosition.Y = mCenter.Y - mEffectiveViewportDimensions.Y / 2;

			Matrix theScaler = Matrix.CreateScale(mZoom);
			Vector3 theTranslation = Vector3.Transform(new Vector3(thePosition.X, thePosition.Y, 0), theScaler);
			//ToDo: fix from here
			mTransform = Matrix.CreateTranslation(theTranslation);          
            

        }

		/// <summary>
		/// Allows and object to pass its position and boundary to the camera to establish wether its visible or not
		/// <para>Does not test if the boundary shape is negative</para>
		/// </summary>
		/// <param name="thePosition">the position of the object to test in space</param>
		/// <param name="theBoundaryShape">the boundary box of the object at the given position</param>
		/// <returns>returns true if any part of boundary box is inside the viewport</returns>
		public bool IsVisible(Vector2 thePosition, Vector2 theBoundaryShape)
		{
			if ((theBoundaryShape.X < 0) || (theBoundaryShape.Y < 0))
			{
				throw new ArgumentOutOfRangeException("Boundary shape contained a negative component in test for visibility");
			}
			
			bool retVal = false;
			//

			//Visibility is true if
			//bottom of the object is below top of viewport
			//  and top of object above the bottom
			//    and left of object left of the right side of the viewport
			//      and right of object right of the left side of the viewport

			if ((thePosition.Y + theBoundaryShape.Y) > mEffectiveViewportPosition.Y)
			{
				if ((thePosition.Y) < (mEffectiveViewportPosition.Y + mEffectiveViewportDimensions.Y))
				{
					if (thePosition.X < (mEffectiveViewportPosition.X + mEffectiveViewportDimensions.X))
					{
						if ((thePosition.X + theBoundaryShape.X) > mEffectiveViewportPosition.X)
						{
							retVal = true;
						}
					}
				}
			}  		

			return retVal;
		}
    }
}
