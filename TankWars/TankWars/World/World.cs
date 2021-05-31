using System;
using System.Collections.Generic;

namespace TankWars
{
    /// <summary>
    /// A class representing the World in the TankWars game
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    public class World
    {
        //A dictionary to keep track of the tanks in the game
        private Dictionary<int, Tank> Tanks;

        //A dictionary to keep track of the powerups in the game
        private Dictionary<int, Powerups> Powerups;

        //A dictionary to keep track of the walls in the game
        private Dictionary<int, Wall> Walls;

        //A dictionary to keep track of the projectiles in the game
        private Dictionary<int, Projectile> Projectile;

        //A dictionary to keep track of the beam= in the game
        private Dictionary<int, Beams> Beams;

        // A variable to represent whether the user is connected to the game
        private bool Connected;

        //A property for the size of the world
        public int Size
        { get; private set; }

        //A variable to represent the powerup delay to add a new powerup in the world
        private int powerupDelay;

        //A variable to represent the powerups in the world
        private int beamCounter;

        //A variable to represent the number of projectiles in the world
        private int projectileCount;

        //A variable representing the powerup id
        private int powerupID;


        /// <summary>
        /// A constructor that initializes the dictionaries, the size of the world, and set Connected to true
        /// </summary>
        public World(int _size)
        {
            Tanks = new Dictionary<int, Tank>();
            Powerups = new Dictionary<int, Powerups>();
            Walls = new Dictionary<int, Wall>();
            Projectile = new Dictionary<int, Projectile>();
            Beams = new Dictionary<int, Beams>();
            Size = _size;
            Connected = true;
            projectileCount = 0;
            beamCounter = 0;
            powerupID = 0;
            powerupDelay = 0;
        }

        /// <summary>
        /// Gets the Walls dictionary
        /// </summary>
        public Dictionary<int, Wall> GetWalls()
        {
            return Walls;
        }

        public void SetWalls(int wallID, Wall w)
        {
            //If the wall dictionary does not contain the key associated with the wallID, then it adds the wall to the wallID
            if (!Walls.ContainsKey(wallID))
            {
                Walls.Add(wallID, w);
            }
            //If the wall dictionary contains the key associated with the wa;lID, then it replaces it with the wall passed
            else
            {
                Walls[wallID] = w;
            }
        }

        public void SetTanks(int tankID, Tank t)
        {
            //If the tank dictionary does not contain the key associated with the tankID, then it adds the tank to the tankID
            if (!Tanks.ContainsKey(tankID))
            {
                Tanks.Add(tankID, t);
            }
            //If the tank dictionary contains the key associated with the tankID, then it replaces it with the tank passed
            else
            {
                Tanks[tankID] = t;
            }
        }

        /// <summary>
        /// Gets the Tank dictionary
        /// </summary>
        public Dictionary<int, Tank> GetTanks()
        {
            return Tanks;
        }
        public void SetPowerups(int powerupID, Powerups p)
        {
            //If the powerups dictionary does not contain the key associated with the powerupID, then it adds the powerup to the powerupID
            if (!Powerups.ContainsKey(powerupID))
            {
                Powerups.Add(powerupID, p);
            }
            //If the powerup dictionary contains the key associated with the powerupID, then it replaces it with the powerup passed
            else
            {
                Powerups[powerupID] = p;
            }
        }

        /// <summary>
        /// Gets the Powerup's dictionary
        /// </summary>
        public Dictionary<int, Powerups> GetPowerups()
        {
            return Powerups;
        }

        public void SetProjectile(int projID, Projectile p)
        {
            //If the projectile dictionary does not contain the key associated with the projID, then it adds the projectile to the projID
            if (!Projectile.ContainsKey(projID))
            {
                Projectile.Add(projID, p);
            }
            //If the projectile dictionary contains the key associated with the projID, then it replaces it with the projectile passed
            else
            {
                Projectile[projID] = p;
            }

        }

        /// <summary>
        /// Gets the Projectile's dictionary
        /// </summary>
        public Dictionary<int, Projectile> GetProjectile()
        {
            return Projectile;
        }
        /// <summary>
        /// Sets a Beam in the Dictionary
        /// </summary>
        public void SetBeams(int beamID, Beams b)
        {
            //If the beam dictionary does not contain the key associated with the beamID, then it adds the beam to the beamID
            if (!Beams.ContainsKey(beamID))
            {
                Beams.Add(beamID, b);
            }
            //If the beam dictionary contains the key associated with the beamID, then it replaces it with the beam passed
            else
            {
                Beams[beamID] = b;
            }
        }
        /// <summary>
        /// Gets the Beams dictionary
        /// </summary>
        public Dictionary<int, Beams> GetBeams()
        {
            return Beams;
        }


        /// <summary>
        /// Returns whether the world is connected or not
        /// </summary>
        public bool GetConnected()
        {
            return Connected;
        }

        /// <summary>
        /// Gets the powerup delay in the world
        /// </summary>
        public int GetPowerupDelay()
        {
            return powerupDelay;
        }

        /// <summary>
        /// Sets the powerup delay in the world
        /// </summary>
        public void SetPowerupDelay(int powerup)
        {
            powerupDelay = powerup;
        }

        /// <summary>
        /// Gets the beam counter in the world
        /// </summary>
        public int GetBeamCounter()
        {
            return beamCounter;
        }

        /// <summary>
        /// Sets the beam counter in the world
        /// </summary>
        public void SetBeamCounter(int beamCount)
        {
            beamCounter = beamCount;
        }

        /// <summary>
        /// Gets the projectile count in the world
        /// </summary>
        public int GetProjectileCount()
        {
            return projectileCount;
        }

        /// <summary>
        /// Sets the projectile count in the world
        /// </summary>
        public void SetProjectileCount(int projCount)
        {
            projectileCount = projCount;
        }

        /// <summary>
        /// Gets the powerup ID in the world
        /// </summary>
        public int GetPowerupID()
        {
            return powerupID;
        }

        /// <summary>
        /// Sets the powerup ID in the world
        /// </summary>
        public void SetPowerupID(int powerup)
        {
            powerupID = powerup;
        }

    }
}
