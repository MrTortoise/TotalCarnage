using System;
using Microsoft.Xna.Framework;

namespace CommonObjects
{
    /// <summary>
    /// Contains info on what the player wants to do. So can later add mode of movement etc (air/stealth?)
    /// allows asking for the appropiate part of the map.
    /// </summary>
    public class MovementEventArgs : EventArgs
    {
        public Vector2 position;
        public eDirection Direction;
        public Int16 Distance;
        public TerrainType Terrain;

        public MovementEventArgs(Vector2 thePosition, TerrainType theTerrain, eDirection theDirection, Int16 theDistance)
        {
            Terrain = theTerrain;
            position = thePosition;
            Direction = theDirection;
            Distance = theDistance;           //this is the max distance that the object would like to move
        }
    }
}
