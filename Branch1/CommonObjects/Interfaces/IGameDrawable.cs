﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{
   public interface IGameDrawable
    {        
       void Draw(DrawingArgs theDrawingArgs);

     

    //    int drawOrder { get; set; }
     //   event EventHandler DrawOrderChanged;   
    }
}
