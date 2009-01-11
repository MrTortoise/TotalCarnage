using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    public class ListOfMapContents
    {

        protected List<MapContents> mMapContants;

        public ListOfMapContents()
        {
            mMapContants = new List<MapContents>();
        }

        public MapContents this[int index]
        { get { return mMapContants[index]; } }

        public int count
        { get { return mMapContants.Count; } }

        public void Add(MapContents aList)
        {
            if (aList.MapID == mMapContants.Count)
            {
                mMapContants.Add(aList);
            }
            else
            {
                throw new Exception("Tried to add a map contents list to the wrong map Id");
            }
        }

        



    }
}
