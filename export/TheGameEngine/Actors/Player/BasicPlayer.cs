using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TheGameEngine
{
    public class BasicPlayer : Player 
    {
        public BasicPlayer(ContentManager theContentManager)
            : base("spaceship1a", theContentManager, 32, 32, 10f, new Vector2(20f, 20f))
        { }
    }
}
