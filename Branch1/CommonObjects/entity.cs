using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{
    public class entity
    {
        Tile mTile;

        Vector2 mPosition;
        Vector2 mVelocity;
        

        float mMaxVelocity;

        float mAcceleration;

        int mHealth;        //-1 = infinite
        int mMass;

        #region constructor

        #endregion

        #region properties
        public Vector2 position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }

        protected float mRotation
        {
            get { return mTile.Rotation; }
            set { mTile.Rotation  = value; }
        }

        #endregion




    }
}
