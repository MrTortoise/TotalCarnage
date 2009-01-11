using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    public enum eTexture : int
    {
        [Description("TileTextures/dirt_texture")]
        dirt = 0,
        [Description("TileTextures/grass_texture")]
        grass = 1,
        [Description("TileTextures/ground_texture")]
        ground = 2,
        [Description("TileTextures/mud_texture")]
        mud = 3,
        [Description("TileTextures/road_texture")]
        road = 4,
        [Description("TileTextures/rock_texture")]
        rock = 5,
        [Description("TileTextures/wood_texture")]
        wood = 6
    }
}
