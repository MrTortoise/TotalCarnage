using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TheGameEngine
{
    interface ITileDrawable
    {        
        void Draw(SpriteBatch theSpriteBatch, GameTime theGameTime, Vector2 thePosition);

        int drawOrder { get; set; }
        event EventHandler DrawOrderChanged;   
    }
}
