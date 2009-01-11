using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TheGameEngine
{
    interface IGameDrawable
    {
        void LoadContent(ContentManager theContentManager);
        void Draw(SpriteBatch theSpriteBatch, GameTime theGameTime);

        bool visible { get; set; }
        event EventHandler VisibleChanged;

        int drawOrder { get; set; }
        event EventHandler DrawOrderChanged;       

    }

   
}
