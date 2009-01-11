using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using CommonObjects;

namespace TheGameEngine
{
    public interface IPlayerMoveable : IGameUpdateable 
    {
        void ProcessKeys(InputState  newState);
        event EventHandler<MovementEventArgs>  RequestMove; // this uses moveeventargs
        event EventHandler<FireEventArgs> Fire;

        void move(MoveArgs theMovement);
     
     
    }


}
