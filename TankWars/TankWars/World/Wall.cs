using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankWars
{
    /// <summary>
    /// A class to represent Walls
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    [JsonObject(MemberSerialization.OptIn)]
    public class Wall
    {
        /// <summary>
        /// Gets the ID of the wall
        /// </summary>
        [JsonProperty(PropertyName = "wall")]
        private int ID;

        /// <summary>
        /// Gets the starting point of the wall
        /// </summary>
        [JsonProperty(PropertyName = "p1")]
        private Vector2D startingPoint;

        /// <summary>
        /// Gets the ending point of the wall
        /// </summary>
        [JsonProperty(PropertyName = "p2")]
        private Vector2D endingPoint;

        /// <summary>
        /// An empty constructor to deserialize the JSON
        /// </summary>
        public Wall()
        {

        }

        /// <summary>
        /// A constructor to deserialize the JSON with the ID's, the X and Y coordinates of the starting and ending point to 
        /// set the starting and ending points
        /// </summary>
        public Wall(int wallID, int startPointXCoordinate, int startPointYCoordinate, int endPointXCoordinate, int endPointYCoordinate)
        {
            ID = wallID;
            startingPoint = new Vector2D(startPointXCoordinate, startPointYCoordinate);
            endingPoint = new Vector2D(endPointXCoordinate, endPointYCoordinate);

        }

        public void SetStartingPoint(Vector2D vector)
        {
            startingPoint = vector;
        }

        public void SetEndingPoint(Vector2D vector)
        {
            endingPoint = vector;
        }
        /// <summary>
        /// Returns the starting point of the wall
        /// </summary>
        public Vector2D GetStartingPoint()
        {
            return startingPoint;
        }
        /// <summary>
        /// Returns the ending point of the wall
        /// </summary>
        public Vector2D GetEndingPoint()
        {
            return endingPoint;
        }

        /// <summary>
        /// Gets the ID of the wall
        /// </summary>
        public int GetID()
        {
            return ID;
        }
    }
}
