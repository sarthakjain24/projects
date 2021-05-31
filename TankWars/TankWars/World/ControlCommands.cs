using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TankWars
{

    /// <summary>
    /// A class for the Control Commands to send instructions to the server
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    [JsonObject(MemberSerialization.OptIn)]
    public class ControlCommands
    {
        //A string representing whether a tank is moving or not
        [JsonProperty(PropertyName = "moving")]
        private string moving;
        //A string representing whether a tank is firing or not
        [JsonProperty(PropertyName = "fire")]
        private string fire;
        //A 2D Vector representing the turret's direction
        [JsonProperty(PropertyName = "tdir")]
        private Vector2D direction;

        /// <summary>
        /// An empty constructor for the Json to deserialize while normalizing the vector
        /// </summary>
        public ControlCommands()
        {
        }

        /// <summary>
        /// A constructor that sets the data according to the way the user would like to send it including the x and y coordinates,
        /// to create a new vector and then normalizing the vector created
        /// </summary>
        public ControlCommands(string movingDir, string fireType, double xCoordinate, double yCoordinate)
        {
            moving = movingDir;
            fire = fireType;

            direction = new Vector2D(xCoordinate, yCoordinate);
            direction.Normalize();
        }

        /// <summary>
        /// A constructor that sets the data according to the way the user would like to send it including a Vector they would like to
        /// pass, normalizing the vector created
        /// </summary>
        public ControlCommands(string movingDir, string fireType, Vector2D vector)
        {
            moving = movingDir;
            fire = fireType;

            direction = new Vector2D(vector);
            direction.Normalize();
        }

        /// <summary>
        /// Gets the turret direction associated with the tank's control command
        /// </summary>
        public Vector2D GetDirection()
        {
            //Has the direction normalized and returns the direction
            direction.Normalize();
            return direction;
        }

        /// <summary>
        /// Sets the turret direction associated with the tank by changing it's control command
        /// </summary>
        public void SetDirection(Vector2D vector)
        {
            //Sets the direction and normalizes it
            direction = new Vector2D(vector);
            direction.Normalize();
        }

        /// <summary>
        /// Gets the moving direction of the control command
        /// </summary>
        public string GetMoving()
        {
            return moving;
        }

        /// <summary>
        /// Sets the moving direction of the control command
        /// </summary>
        public void SetMoving(string s)
        {
            moving = s;
        }

        /// <summary>
        /// Gets the fire type of the control command
        /// </summary>
        public string GetFireType()
        {
            return fire;
        }
        /// <summary>
        /// Sets the fire type of the control command
        /// </summary>
        public void SetFireType(string s)
        {
            fire = s;
        }
    }
}
