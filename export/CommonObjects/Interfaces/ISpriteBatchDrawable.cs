using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    public interface ISpriteBatchDrawable   : IGameObject 
    {
       
            void Draw(spriteBatchArgs theSpriteBatchArgs);

    }
}
