using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    public class MapSubObjects
    {
        protected int mID;
        protected string mName;

        public TerrainTypeList mTerrainTypeList;

        public GeneralTextureList mgeneralTextureList;
        public DetailedTextureList mDetailedTextureList;

        public GeneralTileList mGeneralTileList;
        public DetailedTileList mDetailedTileList;

        public TileLayerList mTileLayerList;

        public int ID
        { get { return mID; } }

        public override string ToString()
        {
            return mName;
        }

       /* public override bool Equals(object obj)
        {
            bool retVal = false;
            if (obj.GetType() == this.GetType())
            {
                MapSubObjectsList test = (MapSubObjectsList)obj;
                if (test.ID == mID)
                {
                    if (test.mName == mName)
                    {
                        if (test.mgeneralTextureList == mgeneralTextureList)
                        {
                            if (test.mDetailedTextureList == mDetailedTextureList)
                            {
                                if (test.mDetailedTileList == mDetailedTileList)
                                {
                                    if (test.mGeneralTileList == mGeneralTileList)
                                    {
                                        if (test.mTileLayerList == mTileLayerList)
                                        {
                                            retVal = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            return retVal;
        }       
        * */

    }
}
