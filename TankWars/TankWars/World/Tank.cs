using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TankWars
{
    /// <summary>
    /// A class representing the Tanks
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    [JsonObject(MemberSerialization.OptIn)]
    public class Tank
    {
        //A variable that represents the tank's ID
        [JsonProperty(PropertyName = "tank")]
        private int ID;

        //A variable that represents the tank's location in 2D vectors
        [JsonProperty(PropertyName = "loc")]
        private Vector2D location;

        //A variable that represents the tank's orientation in 2D vectors
        [JsonProperty(PropertyName = "bdir")]
        private Vector2D orientation;

        //A variable that represents the tank's aiming direction in 2D vectors
        [JsonProperty(PropertyName = "tdir")]
        private Vector2D aiming;

        //A variable that represents the tank's name
        [JsonProperty(PropertyName = "name")]
        private string name;

        //A variable that represents the tank's lives left
        [JsonProperty(PropertyName = "hp")]
        private int hitPoints;

        //A variable that represents the tank's score
        [JsonProperty(PropertyName = "score")]
        private int score;

        //A variable that represents whether the tank has died in the game or not
        [JsonProperty(PropertyName = "died")]
        private bool died;

        //A variable that represents whether the tank has disconnected from the game or not
        [JsonProperty(PropertyName = "dc")]
        private bool disconnected;

        //A variable that represents whether the tank has joined the game or not
        [JsonProperty(PropertyName = "join")]
        private bool joined;


        //A variable that represents the tank's color
        [JsonIgnore()]
        private Color color;

        //A variable that represents if a tank has a powerup
        [JsonIgnore()]
        private bool powerup;

        //A variable to represent the time to respawn a tank
        [JsonIgnore()]
        private int resetTime;

        //A variable to represent the time to reshoot a projectile
        [JsonIgnore()]
        private int projShooter;

        /// <summary>
        /// An empty constructor used to deserialize the JSON
        /// </summary>
        public Tank()
        {
        }

        /// <summary>
        /// A constructor where the playerName and ID be set according
        /// </summary>
        public Tank(string playerName, int playerID)
        {
            name = playerName;
            ID = playerID;
            location = new Vector2D(-463.92, -199.00);
            orientation = new Vector2D(1, 0);
            orientation.Normalize();
            aiming = new Vector2D(0, -1);
            aiming.Normalize();
            hitPoints = 3;
            died = false;
            disconnected = false;
            joined = true;

        }

        /// <summary>
        /// A constructor where the playerID, orientation, location can be set
        /// </summary>
        public Tank(int playerID, Vector2D orient, Vector2D location)
        {
            ID = playerID;
            this.location = new Vector2D(location);
            orientation = new Vector2D(orient);

        }
        /// <summary>
        /// A method that indicates that the tank has been hit by a projectile
        /// </summary>
        public void BeenHitByProjectile()
        {
            hitPoints = hitPoints - 1;
        }

        /// <summary>
        /// A method that indicates that the tank has been hit by a beam
        /// </summary>
        public void BeenHitByBeam()
        {
            hitPoints = 0;
            died = true;
        }

        /// <summary>
        /// Gets the name of the tank
        /// </summary>
        public String GetName()
        {
            return name;
        }

        /// <summary>
        /// Gets the ID of the tank
        /// </summary>
        public int GetID()
        {
            return ID;
        }
        /// <summary>
        /// Gets the score of the tank
        /// </summary>
        public int GetScore()
        {
            return score;
        }

        /// <summary>
        /// Gets to see if the tank IsDead or not
        /// </summary>
        public bool IsDead()
        {
            //If hitPoints is 0, then sets died to true and returns true, else returns false
            if (hitPoints == 0)
            {
                died = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the tank's color
        /// </summary>
        public Color GetColor()
        {
            return color;
        }

        /// <summary>
        /// Sets the color of the tank based on the player ID
        /// </summary>
        public void SetColor(int playerID)
        {
            //If the playerID is divisible by 8, then it sets the color to blue
            if (playerID % 8 == 0)
            {
                color = Color.Blue;
            }

            //If the playerID after divided by 8 gives a remainder of 1, then it sets the color to darkBlue
            else if (playerID % 8 == 1)
            {
                color = Color.DarkBlue;
            }

            //If the playerID after divided by 8 gives a remainder of 2, then it sets the color to Red
            else if (playerID % 8 == 2)
            {
                color = Color.Red;
            }

            //If the playerID after divided by 8 gives a remainder of 3, then it sets the color to purple
            else if (playerID % 8 == 3)
            {
                color = Color.Purple;
            }

            //If the playerID after divided by 8 gives a remainder of 4, then it sets the color to orange
            else if (playerID % 8 == 4)
            {
                color = Color.Orange;
            }

            //If the playerID after divided by 8 gives a remainder of 5, then it sets the color to green
            else if (playerID % 8 == 5)
            {
                color = Color.Green;
            }

            //If the playerID after divided by 8 gives a remainder of 1, then it sets the color to light green
            else if (playerID % 8 == 6)
            {
                color = Color.LightGreen;
            }

            //If the playerID after divided by 8 gives a remainder of 1, then it sets the color to yellow
            else if (playerID % 8 == 7)
            {
                color = Color.Yellow;
            }
        }

        /// <summary>
        /// Gets the HitPoints on the tank
        /// </summary>
        public int GetHitPoints()
        {
            return hitPoints;
        }

        public void SetHitPoints(int hp)
        {
            hitPoints = hp;
        }
        /// <summary>
        /// Gets the orientation of the tank
        /// </summary>
        public Vector2D GetOrientation()
        {
            return orientation;
        }

        public void SetOrientation(Vector2D orient)
        {
            orientation = orient;
        }


        /// <summary>
        /// Gets the location of the tank
        /// </summary>
        public Vector2D GetLocation()
        {
            return location;
        }

        public void SetLocation(Vector2D loc)
        {
            location = loc;
        }

        /// <summary>
        /// Gets the aiming direction of the tank
        /// </summary>
        public Vector2D GetAiming()
        {
            return aiming;
        }

        /// <summary>
        /// Sets the aiming vector of the tank
        /// </summary>
        public void SetAiming(Vector2D vector)
        {
            aiming = vector;
        }

        /// <summary>
        /// Sets the disconnection status of the tank
        /// </summary>
        public void SetDisconnected(bool disconnect)
        {
            disconnected = disconnect;
        }

        /// <summary>
        /// Sets the dying status of the tank
        /// </summary>
        public void SetDied(bool die)
        {
            died = die;
        }

        /// <summary>
        /// Gets the disconnected status of the tank
        /// </summary>
        public bool GetDisconnected()
        {
            return disconnected;
        }

        /// <summary>
        /// Gets the death status of the tank
        /// </summary>
        public bool GetDead()
        {
            return died;
        }

        /// <summary>
        /// Gets the joined status of the tank
        /// </summary>
        public bool GetJoined()
        {
            return joined;
        }

        /// <summary>
        /// Gets the status of the powerup
        /// </summary>
        public bool GetPowerup()
        {
            return powerup;
        }

        /// <summary>
        /// Enables the powerup
        /// </summary>
        public void HasPowerup()
        {
            powerup = true;
        }

        /// <summary>
        /// Disables the powerup
        /// </summary>
        public void UsedPowerup()
        {
            powerup = false;
        }

        /// <summary>
        /// Sets the score of the tank
        /// </summary>
        public void SetScore(int s)
        {
            score = s;
        }

        /// <summary>
        /// Gets the frame to respawn a tank
        /// </summary>
        public int GetRespawnTank()
        {
            return resetTime;
        }

        /// <summary>
        /// Sets the frame to respawn a tank
        /// </summary>
        public void SetRespawnTank(int v)
        {
            resetTime = v;
        }

        /// <summary>
        /// Gets the projectile reshoot counter for a tank
        /// </summary>
        public int GetProjectileReshootCounter()
        {
            return projShooter;
        }

        /// <summary>
        /// Sets the projectile reshoot counter for a tank
        /// </summary>
        public void SetProjectileReshootCounter(int v)
        {
            projShooter = v;
        }
    }
}
