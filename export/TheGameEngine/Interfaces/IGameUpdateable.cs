using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.Text;
using Microsoft.Xna.Framework;


namespace TheGameEngine
{
    public interface IGameUpdateable
    {
        bool Enabled{get;set; }
        event EventHandler EnabledChanged;

        void Update(GameTime gameTime);

        int UpdateOrder { get; set; }
        event EventHandler UpdateOrderChanged;
    }
}
