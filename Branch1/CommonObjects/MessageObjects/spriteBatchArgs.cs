using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Custom.Maths;

namespace CommonObjects
{
    public class spriteBatchArgs
    {
        private SpriteBatch mSpriteBatch;
        private Vector2 mPosition;
        private float mRotation;
        private float mLayerDepth;

        private bool mRotationSet = false;

        public spriteBatchArgs(SpriteBatch theSpriteBatch)
        {
            propSpriteBatch = theSpriteBatch;
           // Position = thePosition;
        }

        public SpriteBatch propSpriteBatch
        {get{return mSpriteBatch;}
            set
            {
                if ((object)value == null)
                { throw new NullReferenceException("Tried to set null spritebatch in SpriteBatchArgs"); }
                mSpriteBatch = value;
            }
        }

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

        public bool RotationSet
        { get { return mRotationSet; } }
                

    }
}
