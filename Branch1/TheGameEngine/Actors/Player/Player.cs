using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using CommonObjects;

namespace TheGameEngine
{
    public class Player : Actor ,  IGameDrawable, IPlayerMoveable
    {
        #region Fields        
        
        protected Int16 mHealth = 100;
        protected Int16 mAmmo;

        protected int mUpdateOrder;
        protected InputState  mOldState = new InputState();

        protected Int16  mMoveModifier = 10;
        protected ePlayerState mState = ePlayerState.normal;

        protected TerrainType mTerrain;


        #endregion

        #region Properties

        public Int16 Health
        { get { return mHealth; } }

        public Int16 Ammo
        { get { return mAmmo; } }

        #endregion

        #region Constructors
        public Player(string theAssetName, ContentManager thecontentManager, int theWidth, int theHeight, float theLayerDepth, Vector2 thePosition)
            : base(theAssetName, thecontentManager, theWidth, theHeight, theLayerDepth) { }

        public Player(string theName, string theAssetName, ContentManager thecontentManager, int theWidth, int theHeight, float theLayerDepth, Vector2 thePosition)
            : base(theName, theAssetName, thecontentManager, theWidth, theHeight, theLayerDepth) { }

        #endregion

        #region Methods

        protected bool IsKeyPressed(KeyboardState[] theStates, Keys theKey)
        {
            bool retVal = false;
            for (int i = 0; i < theStates.GetLength(0); i++)
            {
                if (theStates[i].IsKeyDown(theKey)==true)
                {
                    retVal = true;
                }
            }
            return retVal;
        }

        #region Interface Implementations
        #region IGameDrawable Members

        public void Draw(SpriteBatch theSpriteBatch, GameTime theGameTime)
        {            
            theSpriteBatch.Draw(mTexture, mPosition, null, Color.White,mRotation, mOrigin, mScale, SpriteEffects.None, 0.5F);
        }

        public bool visible
        {
            get { return mVisible; }
            set 
            { 
                mVisible = value; 
                OnVisibleChanged(new VisibilityEventArgs(mVisible)); 
            }
        }
        protected virtual void OnVisibleChanged(VisibilityEventArgs e)
        {
            if (VisibleChanged != null)
            {
                VisibleChanged(this, e);
            }
        }

        public int drawOrder
        {
            get { return (int)(1 / mLayerDepth); }
            set 
            { 
                mLayerDepth = 1 / value; 
                onDrawOrderChanged(new DrawOrderEventArgs(drawOrder)); 
            }
        }
        protected virtual void onDrawOrderChanged(DrawOrderEventArgs e)
        {
            if (DrawOrderChanged != null)
            {
                DrawOrderChanged(this, e);
            }
        }

        #endregion
       
        #region IPlayerMoveable Members


        public void ProcessKeys(InputState  newState)
        {
            
            if (mState == ePlayerState.normal)
            {
                if (IsKeyPressed(newState.CurrentKeyboardStates,Keys.Left) == true)
                {
                    onRequestMove(new MovementEventArgs(mPosition ,mTerrain,eDirection.Left,mMoveModifier));
                }
                else if (IsKeyPressed(mOldState.CurrentKeyboardStates ,Keys.Left) == true)
                {
                }

                
                if (IsKeyPressed( newState.CurrentKeyboardStates ,Keys.Right) == true)
                {
                    onRequestMove(new MovementEventArgs(mPosition, mTerrain, eDirection.right , mMoveModifier));
                }
                else if (IsKeyPressed(mOldState.CurrentKeyboardStates,Keys.Right) == true)
                {
                }


                if (IsKeyPressed(newState.CurrentKeyboardStates,Keys.Up) == true)
                {
                    onRequestMove(new MovementEventArgs(mPosition, mTerrain, eDirection.up , mMoveModifier));
                    
                }
                else if (IsKeyPressed(mOldState.CurrentKeyboardStates, Keys.Up) == true)
                {
                }

                if (IsKeyPressed(newState.CurrentKeyboardStates, Keys.Down ) == true)
                {
                    onRequestMove(new MovementEventArgs(mPosition, mTerrain, eDirection.down , mMoveModifier));
                }
                else if (IsKeyPressed(mOldState.CurrentKeyboardStates, Keys.Down ) == true)
                {
                }

                mOldState = newState;                
            }
        }

        public void move(MoveArgs theMovement)
        {
            if ((theMovement.OutOfPlay == true) || (theMovement.passableTarget==false)) // off level edges
            {
                mPosition = theMovement.position;
            }
            else
            {            
                if (theMovement.direction == eDirection.up)
                {                                          
                    { mPosition.Y = mPosition.Y - (mMoveModifier * theMovement.MoveDistance); }                    
                }
                if (theMovement.direction == eDirection.down )
                {
                   mPosition.Y = mPosition.Y + (mMoveModifier * theMovement.movementAdjust);
                }
                if (theMovement.direction == eDirection.Left)
                {
                        mPosition.X = mPosition.X  - (mMoveModifier * theMovement.movementAdjust);
                 }

                if (theMovement.direction == eDirection.right)
                {
                   mPosition.X = mPosition.X + (mMoveModifier * theMovement.movementAdjust);
                }
            }

            Vector2 theDirection = mPosition - mOldPosition;
            if (theDirection != Vector2.Zero)
            {
                float theAngle = (float)Math.Atan2((double)(mPosition.Y - mOldPosition.Y), (double)(mPosition.X - mOldPosition.X));
                mRotation = theAngle;
            }
            mOldPosition = mPosition;
            mTerrain = theMovement.terrain;
        }

        #endregion

        #region IGameUpdateable Members

                public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public bool Enabled
        {
            get { return mEnabled; }
            set
            {
                mEnabled = value;
                OnEnableChanged(new EnabledEventArgs(mEnabled));
            }
        }
        protected void OnEnableChanged(EnabledEventArgs e)
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(this, e);
            }
        }

        public int UpdateOrder
        {
            get { return mUpdateOrder; }
            set
            {
                mUpdateOrder = value;
                OnUpdateOrderChanged(new UpdataOrderEventArgs(mUpdateOrder));
            }
        }
        protected void OnUpdateOrderChanged(UpdataOrderEventArgs e)
        {
            if (UpdateOrderChanged != null)
            {
                UpdateOrderChanged(this, e);
            }
        }

        #endregion

        
         #endregion

        #region Events
            #region IGameDrawable Events
                public event EventHandler VisibleChanged;
                public event EventHandler DrawOrderChanged;
            #endregion
            #region IGameUpdateable Events
                public event EventHandler EnabledChanged;
                public event EventHandler UpdateOrderChanged;
            #endregion

            
        #endregion


        #endregion



                #region IPlayerMoveable Members


                public event  EventHandler<MovementEventArgs> RequestMove;
                protected virtual void onRequestMove(MovementEventArgs  e)
                {
                    EventHandler<MovementEventArgs> temp = RequestMove;

                    if (temp != null)
                    {
                        temp(this, e);
                    }
                }

                public delegate void RequestMoveHandler
                (
                   Player  thePlayer,
                   MovementEventArgs  e
                );



                public event EventHandler<FireEventArgs> Fire;
                protected virtual void onFire(FireEventArgs  e)
                {
                    if (Fire != null)
                    {
                        Fire(this, e);
                    }
                }



                #endregion



    }  
}
