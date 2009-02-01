using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Custom.Maths;

namespace CommonObjects
{
	

	/// <summary>
	/// This class contains the values required to be able to draw a srpite in a spritebatch given that it has already begun
	/// The class that begins the sprite batch knows the camera and so it should determine visibility
	/// </summary>
    public class spriteBatchArgs
    {
		
        private SpriteBatch mSpriteBatch;
        private Vector2 mPosition;
        private float mRotation;
        private float mLayerDepth;

        private bool mRotationSet = false;

        public spriteBatchArgs(SpriteBatch theSpriteBatch)
        {
            SpriteBatch = theSpriteBatch;
	
           // Position = thePosition;
        }


		/// <summary>
		/// Gets or sets the spriteBatch object
		/// </summary>
		/// <exception cref="System.NullReferenceException">Cant set to be a null reference</exception>
        public SpriteBatch SpriteBatch
        {get{return mSpriteBatch;}
            set
            {
                if ((object)value == null)
                { throw new NullReferenceException("Tried to set null spritebatch in SpriteBatchArgs"); }
                mSpriteBatch = value;
            }
        }

	/*	public Camera Camera
        {
            get { return mCamera; }
            set
            {
                if ((object)Camera == null)
                { throw new NullReferenceException("Tried to set Camera to null in DrawingArgs"); }
            }
        }
	 * */

		/// <summary>
		/// Gets or sets the root position of the control
		/// </summary>
		/// <exception cref="System.NullReferenceException">Can't set null vales</exception>
        public Vector2 Position
        {
            get { return mPosition; }
            set
            {
                if ((object)value == null)
                { throw new NullReferenceException("Tried to set null position in spritebatchargs"); }

                mPosition = value;
            }
        }

		/// <summary>
		/// Gets or sets the rotation 
		/// </summary>
        public float Rotation
        {
            get { return mRotation; }
            set
            {
                RotationManager rm = new RotationManager();
                mRotation = rm.GetAbsRotation(value);
                mRotationSet = true;
            }
        }

		/// <summary>
		/// gets or sets the malyerdepth to draw the sprite at. 
		/// Must be greater or equal to 0 and less than or equal to 1
		/// </summary>
        public float LayerDepth
        {
            get { return mLayerDepth; }
            set
            {
                if (value < 0)
                { value = 0; }

                if (value > 1)
                { value = 1; }

                mLayerDepth = value;
            }
        }

        public bool IsRotationSet
        { get { return mRotationSet; } }
                

    }
}
