using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace CommonObjects
{
    /// <summary>
    /// This is telling  player what to do.
    /// Doesnt simply do it as player may have overrides for particular terraintypes.
    /// Ie this response is generic to the terrain, not the recieving object.
    /// </summary>
    public class MoveArgs
    {
        public Vector2 position;                    // the new position
        public bool passableTarget = true;          // wether the target tile was passable (never make it to this tile though)
        public float movementAdjust = 1;            // the map movement modifier
        public bool OutOfPlay;
        public eDirection direction;
        public TerrainType terrain;
        public Int16 MoveDistance;                  // the maximum possible moving distance of the player
        // can be used to figure out if a movement into a tile is possible
        // eg if the object has a >1 modifier / or an additional effect


        public MoveArgs(Vector2 thePosition, bool IsPassable, float theMovementAdjust, eDirection thedirection, TerrainType theTerrain,
                Int16 theMoveDistance, bool IsOutOfPlay)
        {
            position = thePosition;
            passableTarget = IsPassable;
            movementAdjust = theMovementAdjust;
            direction = thedirection;
            terrain = theTerrain;
            MoveDistance = theMoveDistance;
            OutOfPlay = IsOutOfPlay;
        }
    }
}
