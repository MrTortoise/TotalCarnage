using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// This is a structure to hold the filenames od the xml files to be loaded
    /// to construct this particular map
    /// </summary>
    public class MapContents
    {
        public int MapID;
        public string MapName;
        public string TerrainTypes;
        public string GeneralTextures;
        public string DetailedTextures;
        public string GeneralTiles;
        public string DetailedTiles;

        public string TileLayers;
    }
}
