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
        #region Fields
        private  float scale = 1;
        private float rotation = 0;
        private float speed = 0;
        private Vector2 position;

        private Matrix transform;

        #endregion

        #region Constructor

        public Camera(Vector2 thePosition)
        {
            SetPosition(thePosition);
        }

        #endregion

        #region Properties
        public Matrix Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        #endregion

        public void SetPosition(Vector2 thePosition)
        {
            transform=Matrix.CreateTranslation(new Vector3(thePosition.X,thePosition.Y ,0));
            position=thePosition;

        }
    }
}
