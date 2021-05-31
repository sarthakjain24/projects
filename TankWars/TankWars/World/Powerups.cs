using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankWars
{
    /// <summary>
    /// A class that represents the Powerups in the game
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    [JsonObject(MemberSerialization.OptIn)]
    public class Powerups
    {
        //A variable that represents the powerup's ID
        [JsonProperty(PropertyName = "power")]
        private int ID;

        // A variable that represents the location of the powerup
        [JsonProperty(PropertyName = "loc")]
        private Vector2D location;

        // A variable that represents the status of the powerup
        [JsonProperty(PropertyName = "died")]
        private bool dead;



        //An empty constructor used to deserialize the JSON strings
        public Powerups()
        {
        }

        //A constructor to set the powerup's ID, the location, and the status of the powerup
        public Powerups(int powerupID, Vector2D loc, bool isDead)
        {
            location = new Vector2D(loc);
            dead = isDead;
            ID = powerupID;
        }

        /// <summary>
        /// Gets the ID of the powerup
        /// </summary>
        public int GetID()
        {
            return ID;
        }

        /// <summary>
        /// Gets the location of the powerup
        /// </summary>
        public Vector2D GetLocation()
        {
            return location;
        }

        /// <summary>
        /// Sets the location of the powerup
        /// </summary>
        public void SetLocation(Vector2D loc)
        {
            location = loc;
        }

        /// <summary>
        /// Sets the status of the powerup to dead
        /// </summary>
        public void SetDead()
        {
            dead = true;
        }

        /// <summary>
        /// Sets the status of the powerup to alive
        /// </summary>
        public void SetAlive()
        {
            dead = false;
        }

        /// <summary>
        /// Returns the status of the powerup
        /// </summary>
        public bool GetDead()
        {
            return dead;
        }


        
    }
}
