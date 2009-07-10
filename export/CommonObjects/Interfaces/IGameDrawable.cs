using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{
   public interface IGameDrawable : IGameObject 
    {
	   bool IsVisible
	   { get;}

	    void SetVisibility(bool theVisibility);
	   

       void Draw(DrawingArgs theDrawingArgs);



     

    //    int drawOrder { get; set; }
     //   event EventHandler DrawOrderChanged;   
    }
}
