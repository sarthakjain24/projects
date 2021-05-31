using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankWars
{
    /// <summary>
    /// A class to represent the beam
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    [JsonObject(MemberSerialization.OptIn)]
    public class Beams
    {
        //A variable that represents the beam's ID
        [JsonProperty(PropertyName = "beam")]
        private int beamID;

        //A variable that represents the beam owner's ID
        [JsonProperty(PropertyName = "owner")]
        private int ownerID;

        //A variable that represents the origin of the beam
        [JsonProperty(PropertyName = "org")]
        private Vector2D origin;

        //A variable that represents the direction of the beam
        [JsonProperty(PropertyName = "dir")]
        private Vector2D direction;

        /// <summary>
        /// An empty constructor to deserialize the JSON
        /// </summary>
        public Beams()
        {
        }

        /// <summary>
        /// A constructor to set the beamID and ownerID based on what the user wants
        /// </summary>
        public Beams(int beamID, int playerID)
        {
            ownerID = playerID;
            this.beamID = beamID;
        
        }

        /// <summary>
        /// A constructor to set the beam's details to the way they would like it to be
        /// </summary>
        public Beams(int beamID, int playerID, Vector2D newOrigin, Vector2D newDirection)
        {
            ownerID = playerID;
            this.beamID = beamID;
            origin = newOrigin;
            direction = newDirection;
        
        }

        /// <summary>
        /// Returns the beam's ID
        /// </summary>
        public int GetBeamID()
        {
            return beamID; 
        }

        /// <summary>
        /// Returns the beam owner's ID
        /// </summary>
        public int GetOwnerID()
        {
            return ownerID;
        }
        
        /// <summary>
        /// Gets the origin of the beam
        /// </summary>
        public Vector2D GetOrigin()
        {
            return origin;
        }
        
        /// <summary>
        /// Sets the origin of the beam
        /// </summary>
        public void SetOrigin(Vector2D vector)
        {
            origin = vector;
        }
        
        /// <summary>
        /// Gets the direction of the beam
        /// </summary>
        public Vector2D GetDirection()
        {
            return direction;
        }
        
        /// <summary>
        /// Sets the direction of the beam
        /// </summary>
        public void SetDirection(Vector2D vector)
        {
            direction = vector;
        }
     
    }
}
