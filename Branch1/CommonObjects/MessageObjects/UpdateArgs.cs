using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{
    /// <summary>
    /// Arguments to be passed as part of an update
    /// </summary>
    public class UpdateArgs
    {
        private GameTime mTime;
        protected float mTimeScale;

        /// <summary>
        /// Creates the updateArgs object that is passed to all update methods
        /// </summary>
        /// <param name="theTime">Contains the gametime object from the game</param>
        /// <param name="timeScale">Contains a multiplier to allows time warps for entities updates</param>
        public UpdateArgs(GameTime theTime, float timeScale)
        {
            if ((object)theTime == null)
            { throw new NullReferenceException("tried to create updateArgs with null gametime"); }

            mTime = theTime;

            if (timeScale < 0)
            { throw new OverflowException("Cannot have negative time scale"); }

            mTimeScale = timeScale;

        }

        /// <summary>
        /// contains the gametime object
        /// </summary>
        public GameTime GameTime
        { get { return mTime; } }

        /// <summary>
        /// contains the timescale mmultiplier
        /// </summary>
        public float TimeScale
        { get { return mTimeScale; } }


    }
}
