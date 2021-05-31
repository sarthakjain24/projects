using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TankWars
{
    /// <summary>
    /// A class to represent the Projectile
    /// </summary>    
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    [JsonObject(MemberSerialization.OptIn)]
    public class Projectile
    {
        //A variable that represents the projectile ID
        [JsonProperty(PropertyName = "proj")]
        private int ID;

        //A variable that represents the projectile's location
        [JsonProperty(PropertyName = "loc")]
        private Vector2D location;

        //A variable that represents the projectile's orientation
        [JsonProperty(PropertyName = "dir")]
        private Vector2D orientation;

        //A variable that represents the projectile's owner's ID
        [JsonProperty(PropertyName = "owner")]
        private int owner;

        //A variable that represents the projectile's life
        [JsonProperty(PropertyName = "died")]
        private bool died;

        //A variable that represents the projectile's color, and is given by the JSON
        //so is not deserialized
        [JsonIgnore()]
        private Color color;

        /// <summary>
        /// An empty constructor to deserialize the JSON
        /// </summary>
        public Projectile()
        {

        }

        /// <summary>
        /// A constructor that sets the projectile ID, player ID and the color of the projectile
        /// </summary>
        public Projectile(int projID, int playerID)
        {
            ID = projID;
            died = false;
            owner = playerID;
            Tank t = new Tank(owner, location, orientation);
            color = t.GetColor();
        }

        /// <summary>
        /// A constructor that sets the projectile ID, player ID, location, orientation, and the color of the projectile
        /// </summary>
        public Projectile(int projID, int playerID, Vector2D loc, Vector2D orient)
        {
            ID = projID;
            location = new Vector2D(loc);
            orientation = new Vector2D(orient);
            died = false;
            owner = playerID;
            Tank t = new Tank(owner, location, orientation);
            color = t.GetColor();
        }

        /// <summary>
        /// Returns the ID of the projectile
        /// </summary>
        public int GetID()
        {
            return ID;
        }

        /// <summary>
        /// Returns the projectile's owner ID
        /// </summary>
        public int GetOwnerID()
        {
            return owner;
        }

        /// <summary>
        /// Gets wheteher the projectile is dead or alive
        /// </summary>
        public bool GetDead()
        {
            return died;
        }

        /// <summary>
        /// Sets the dead boolean of the projectile to true indicating the projectile is dead
        /// </summary>
        public void SetDead()
        {
            died = true;
        }

        /// <summary>
        /// Sets the dead boolean of the projectile to false indicating the projectile is alive
        /// </summary>
        public void SetAlive()
        {
            died = false;
        }

        /// <summary>
        /// Returns the orientation of the projectile
        /// </summary>
        public Vector2D GetOrientation()
        {
            return orientation;
        }

        /// <summary>
        /// Sets the orientation of the projectile
        /// </summary>
        public void SetOrientation(Vector2D newOrientation)
        {
            orientation = newOrientation;
        }

        /// <summary>
        /// Gets the location of the projectile
        /// </summary>
        public Vector2D GetLocation()
        {
            return location;
        }

        /// <summary>
        /// Sets the location of the projectile
        /// </summary>
        public void SetLocation(Vector2D newLocation)
        {
            location = newLocation;
        }

        /// <summary>
        /// Sets the owner of the projectile
        /// </summary>
        public void SetOwner(int id)
        {
            owner = id;
        }

        /// <summary>
        /// Sets the ID of the projectile
        /// </summary>
        public void SetID(int id)
        {
            ID = id; 
        }


    }
}

