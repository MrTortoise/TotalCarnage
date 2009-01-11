using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CommonObjects;
//using XMLIO;


namespace TileEditor
{
    

    public partial class MapEditor : Form
    {
      //  SpriteBatch spriteBatch;
        Camera mCamera = new Camera(new Vector2(0,0));
        Map theMap;
        string mFile;

        public MapEditor(string theFile)
        {
            mFile = theFile;
            InitializeComponent();

            tileDisplay1.OnInitialize += new EventHandler(tileDisplay1_OnInitialize);
           // tileDisplay1.OnDraw += new EventHandler(tileDisplay1_OnDraw);

            Application.Idle += delegate { tileDisplay1.Invalidate(); };
        }

        public GraphicsDevice GraphicsDevice
        { get { return tileDisplay1.GraphicsDevice; } }

        void tileDisplay1_OnInitialize(object sender, EventArgs e)
        {
            /*      theMap = new Map();
                  MapObjectsLoader theLoader = new MapObjectsLoader("Map", 0);
                  theMap.SetConstituentObjects(theLoader.mapObjects);
                  theMap.Load(tileDisplay1.GraphicsDevice);

                  if (theMap.WidthInPixels  > tileDisplay1.Width)
                  {
                      hScrollBar1.Visible = true;
                      hScrollBar1.Minimum = 0;
                      hScrollBar1.Maximum = theMap.WidthInPixels/theMap.GetMinTileDim;
                  }

                  if (theMap.HeightInPixels  > tileDisplay1.Height)
                  {
                      vScrollBar1.Visible = true;
                      vScrollBar1.Minimum = 0;
                      vScrollBar1.Maximum = theMap.HeightInPixels/theMap.GetMinTileDim ;
                 }*/
        }

        void tileDisplay1_OnDraw(object sender, EventArgs e)
        {
            Logic();
            Render();
        }

        protected void Logic()
        {
          //  mCamera.SetPosition(new Vector2((float)(hScrollBar1.Value*theMap.GetMinTileDim), (float)(vScrollBar1.Value*theMap.GetMinTileDim )));

        }

        protected void Render()
        {
         /*   GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);
            theMap.Draw(new DrawingArgs(tileDisplay1.GraphicsDevice, mCamera));
          * */
        }



    }
}
