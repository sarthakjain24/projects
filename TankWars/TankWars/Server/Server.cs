using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace TankWars
{
    /// <summary>
    /// A class representing the Server of the TankWars game
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    public class Server
    {
        //A random object in order to randomly place a tank
        private static Random r = new Random();

        //A constant variable representing the startingHitPoints to be 3
        private const int startingHitPoints = 3;

        //A constant variable representing the projectile speed to be 25
        private const int projectileSpeed = 25;

        //A constant variable representing the tank speed to be 3 units
        private const double tankSpeed = 3.0;

        //A constant variable representing the tank's length to be 60 units
        private const int tankLength = 60;

        //A constant variable representing the tank's width to be 60 units
        private const int tankWidth = 60;

        //A constant variable representing the size of the wall being 50 units
        private const int wallSize = 50;

        //A constant variable representing the number of powerups allowed
        private const int maxPowerup = 2;

        //A constant variable representing the delay after which a powerup will be shown
        private const int maxPowerupDelay = 1650;

        //A string representing the universe size
        private static string universeSize;

        //A string representing the time to refresh per frame
        private static string timePerFrame;

        //A long that represents the milliseconds per frame
        private static long MSPerFrame;

        //A string representing the frames per shot for the projectile
        private static string framePerShot;

        //A string representing the frames per shot for a respawn
        private static string respawnDelay;

        


        //A World instance that represents the world
        private static World world;

        //A dictionary to represent the sockets that have been connected in the server
        private static Dictionary<long, SocketState> connectedSockets = new Dictionary<long, SocketState>();


        /// <summary>
        /// The main where everything runs
        /// </summary>
        public static void Main(string[] args)
        {
            //A string to get the directory
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            //Reads the settings
            ReadSettings(dir + "\\..\\..\\Resources/settings.xml");

            //A message to indicate the server is running and the user can connect clients
            Console.WriteLine("Server is running");

            //Starts the server
            Networking.StartServer(OnConnect, 11000);

            //Creates a new thread for the busy loop to update and send the client information on each frame
            Thread thread = new Thread(BusyLoopToUpdate);
            thread.Start();
        
            //Ensures that the main thread doesn't automatically end
            Console.Read();
        }

        /// <summary>
        /// A busy loop that runs infinitely to update the world for all the clients, and update on 60fps
        /// </summary>
        private static void BusyLoopToUpdate(object obj)
        {
            //A stopwatch to continuously update a server
            Stopwatch watch = new Stopwatch();

            //Starts an infinite busy loop to update the world approximately every 60fps
            while (true)
            {

                watch.Start();

                while (watch.ElapsedMilliseconds < MSPerFrame)
                {

                }
                watch.Restart();

                Update();

            }
        }

        /// <summary>
        /// An Update loop that updates the frame by sending all the world's information to a client
        /// </summary>
        private static void Update()
        {
            SendAllInfo();
        }

        /// <summary>
        /// The first part of the handshake when a client connects to a server
        /// </summary>
        private static void OnConnect(SocketState socket)
        {
            //Changes the network action to receive the name of the tank
            socket.OnNetworkAction = ReceiveName;

            //Gets the data from the socket
            Networking.GetData(socket);
        }

        /// <summary>
        /// A method that receives the name of the client
        /// </summary>
        private static void ReceiveName(SocketState socket)
        {
            //Changes the network action to receive the information
            socket.OnNetworkAction = ReceiveAllInfo;

            //Changes the handshake in order to create a tank
            SendStartup(socket);

            //Gets the data from the socket
            Networking.GetData(socket);
        }

        /// <summary>
        /// A method that sends the startup information to the client
        /// </summary>
        private static void SendStartup(SocketState socket)
        {
            //Only accepts clients whose name is less than 16 characters
            if (socket.GetData().Trim().Length < 16)
            {
                Console.WriteLine(socket.GetData().Trim() + ", " + socket.ID.ToString() + " has Connected to the server");

                //Creates a new tank
                Tank tank = new Tank(socket.GetData().Trim(), (int)socket.ID);
                int tankID = tank.GetID();
                Vector2D orient = new Vector2D(0, 1);
                orient.Normalize();
                tank.SetOrientation(orient);
                DrawTank(tank);

                //Changes the handshake to receive the information from the client
                ReceiveAllInfo(socket);

                //Sends the client the tank's ID, the universe size and the walls, something that should only happen once as these things don't change
                SendInformationThatCannotChange(socket, tankID, universeSize);



                //Adds the tank into the world and if the tank is disconnected, then it removes the tank from the world
                lock (world)
                {
                    if (!world.GetTanks().ContainsKey(tank.GetID()))
                    {
                        if (world.GetTanks().Count >= 0)
                        {
                            world.GetTanks().Add(tank.GetID(), tank);
                        }
                    }
                    if (tank.GetDisconnected() == true)
                    {
                        world.GetTanks().Remove(tank.GetID());
                    }
                }

                //Locks because socket can be disconnected whenever, and adds the socket to the dictionary of the socket's in the server
                lock (connectedSockets)
                {
                    if (!connectedSockets.ContainsKey(socket.ID))
                    {
                        connectedSockets.Add(socket.ID, socket);
                    }
                }

                //Gets the data of the socket
                Networking.GetData(socket);
            }

        }

        /// <summary>
        /// A method that receives and processes all information from the client
        /// </summary>
        private static void ReceiveAllInfo(SocketState socket)
        {
            //Only tries to receive if the socket is connected
            if (connectedSockets.ContainsKey((int)socket.ID))
            {
                //Gets the data from the socket
                string totalData = socket.GetData();

                //Splits the data by a new line
                string[] dataSplit = Regex.Split(totalData, @"(?<=[\n])");

                //Iterates through all the strings in the data split
                foreach (string data in dataSplit)
                {
                    //If the data is null, then it breaks
                    if (data == null)
                    {
                        break;
                    }

                    //Trims the data
                    string trimmedData = data.Trim();

                    //If the trimmed data's length is 0, then it continues to the next iteration
                    if (trimmedData.Length == 0)
                        continue;

                    //If the trimmed data's length is less than 16, then it continues, to avoid the possibility of a name
                    if (trimmedData.Length <= 16)
                    {
                        continue;
                    }

                    //Creates a JSON object and parses the trimmed data, and if the data throws an exception, then it continues to the next iteration
                    JObject jObject;

                    try
                    {
                        jObject = JObject.Parse(trimmedData);
                    }
                    catch (JsonReaderException e)
                    {
                        continue;
                    }

                    //Deserializes the trimmedData
                    ControlCommands control = JsonConvert.DeserializeObject<ControlCommands>(trimmedData);

                    //Gets the moving direction
                    string movingDir = control.GetMoving();

                    //Gets the shooting command
                    string shooting = control.GetFireType();

                    if (trimmedData.Length > 16)
                    {
                        //Locks everything in a world in order to prevent race conditions as we modify the world
                        lock (world)
                        {
                            //If the world contains the tank, then it goes into this condition
                            if (world.GetTanks().ContainsKey((int)socket.ID))
                            {
                                //Gets the tank associated with the socket ID from the world
                                Tank tank = world.GetTanks()[(int)socket.ID];

                                //If the tank is not dead, then it goes in this condition
                                if (tank.GetDead() == false)
                                {
                                    //If the moving direction is none, then it keeps the same location
                                    if ((movingDir == "none") || (movingDir == ""))
                                    {
                                        Vector2D temp = tank.GetLocation();
                                        tank.SetLocation(temp);
                                    }
                                    //If the moving direction is up, then it goes in this condition
                                    else if (movingDir == "up")
                                    {
                                        //Changes the orientation of the tank
                                        Vector2D upOr = new Vector2D(0, -1);
                                        tank.SetOrientation(upOr);

                                        //Sets the tank to a new location
                                        Vector2D temp = tank.GetLocation() - new Vector2D(0, tankSpeed);
                                        tank.SetLocation(temp);

                                        //Initializes the boolean to crash as false
                                        bool crash = false;

                                        //Checks if the new location will cause the tank to go out of bounds, and if it does, then it wraps around to the other side in the world
                                        if (temp.GetY() <= (-world.Size / 2))
                                        {
                                            Vector2D wrapAround = new Vector2D(temp.GetX(), (world.Size / 2) - 30);
                                            tank.SetLocation(wrapAround);
                                        }

                                        //Checks if a wall collides with the tank's new location and if it does, then it sets the crash boolean to true
                                        foreach (int wallID in world.GetWalls().Keys.ToList())
                                        {
                                            Wall w = world.GetWalls()[wallID];

                                            if (UpCollision(w, tank))
                                            {
                                                crash = true;
                                                break;
                                            }
                                        }

                                        //If crash is true, then it reset's the tank location to back where it was
                                        if (crash == true)
                                        {
                                            Vector2D reset = tank.GetLocation() + new Vector2D(0, tankSpeed);
                                            tank.SetLocation(reset);
                                        }
                                        //Resets the moving to none
                                        control.SetMoving("none");
                                    }
                                    else if (movingDir == "down")
                                    {
                                        //Changes the orientation of the tank
                                        Vector2D downOr = new Vector2D(0, 1);
                                        tank.SetOrientation(downOr);

                                        //Sets the tank to a new location
                                        Vector2D temp = tank.GetLocation() + new Vector2D(0, tankSpeed);
                                        tank.SetLocation(temp);

                                        //Initializes the boolean to crash as false
                                        bool crash = false;

                                        //Checks if the new location will cause the tank to go out of bounds, and if it does, then it wraps around to the other side in the world
                                        if (temp.GetY() >= (world.Size / 2))
                                        {
                                            Vector2D wrapAround = new Vector2D(temp.GetX(), (-world.Size / 2) + 30);
                                            tank.SetLocation(wrapAround);
                                        }

                                        //Checks if a wall collides with the tank's new location and if it does, then it sets the crash boolean to true
                                        foreach (int wallID in world.GetWalls().Keys.ToList())
                                        {
                                            Wall w = world.GetWalls()[wallID];

                                            if (DownCollision(w, tank))
                                            {
                                                crash = true;
                                                break;
                                            }
                                        }
                                        //If crash is true, then it reset's the tank location to back where it was
                                        if (crash == true)
                                        {
                                            Vector2D reset = tank.GetLocation() - new Vector2D(0, tankSpeed);
                                            tank.SetLocation(reset);
                                        }
                                        //Resets the moving to none
                                        control.SetMoving("none");
                                    }
                                    //If the moving direction is left, then it goes into this condition
                                    else if (movingDir == "left")
                                    {
                                        //Changes the orientation of the tank
                                        Vector2D leftOr = new Vector2D(-1, 0);
                                        tank.SetOrientation(leftOr);

                                        //Sets the tank to a new location
                                        Vector2D temp = tank.GetLocation() - new Vector2D(tankSpeed, 0);
                                        tank.SetLocation(temp);

                                        //Initializes the boolean to crash as false
                                        bool crash = false;

                                        //Checks if the new location will cause the tank to go out of bounds, and if it does, then it wraps around to the other side in the world
                                        if (temp.GetX() <= (-world.Size / 2))
                                        {
                                            Vector2D wrapAround = new Vector2D((world.Size / 2) - 30, temp.GetY());
                                            tank.SetLocation(wrapAround);
                                        }


                                        //Checks if a wall collides with the tank's new location and if it does, then it sets the crash boolean to true
                                        foreach (int wallID in world.GetWalls().Keys.ToList())
                                        {
                                            Wall w = world.GetWalls()[wallID];
                                            if (LeftCollision(w, tank))
                                            {
                                                crash = true;
                                                break;
                                            }

                                        }

                                        //If crash is true, then it reset's the tank location to back where it was
                                        if (crash == true)
                                        {
                                            Vector2D reset = tank.GetLocation() + new Vector2D(tankSpeed, 0);
                                            tank.SetLocation(reset);
                                        }
                                        //Resets the moving to none
                                        control.SetMoving("none");
                                    }
                                    //If the moving direction is right, then it goes in this condition
                                    else if (movingDir == "right")
                                    {
                                        //Changes the orientation of the tank
                                        Vector2D rightOr = new Vector2D(1, 0);
                                        tank.SetOrientation(rightOr);

                                        //Sets the tank to a new location
                                        Vector2D temp = tank.GetLocation() + new Vector2D(tankSpeed, 0);
                                        tank.SetLocation(temp);

                                        //Initializes the boolean to crash as false
                                        bool crash = false;

                                        //Checks if the new location will cause the tank to go out of bounds, and if it does, then it wraps around to the other side in the world
                                        if (temp.GetX() >= (world.Size / 2))
                                        {
                                            Vector2D wrapAround = new Vector2D((-world.Size / 2) + 30, temp.GetY());
                                            tank.SetLocation(wrapAround);
                                        }

                                        //Checks if a wall collides with the tank's new location and if it does, then it sets the crash boolean to true
                                        foreach (int wallID in world.GetWalls().Keys.ToList())
                                        {
                                            Wall w = world.GetWalls()[wallID];

                                            if (RightCollision(w, tank))
                                            {
                                                crash = true;
                                                break;
                                            }

                                        }

                                        //If crash is true, then it reset's the tank location to back where it was
                                        if (crash == true)
                                        {
                                            Vector2D reset = tank.GetLocation() - new Vector2D(tankSpeed, 0);
                                            tank.SetLocation(reset);
                                        }
                                        //Resets the moving to none
                                        control.SetMoving("none");
                                    }
                                    //If the tank reports shooting as main, then it goes into this condition
                                    if (shooting == "main")
                                    {
                                        //If enough time has passed for a tank to reshoot a projectile, then it goes into the condition
                                        if (tank.GetProjectileReshootCounter() >= int.Parse(framePerShot))
                                        {
                                            //Creates a projectile for the tank
                                            Projectile p = new Projectile();
                                            p.SetAlive();
                                            p.SetOwner(tank.GetID());
                                            p.SetLocation(tank.GetLocation());
                                            p.SetOrientation(tank.GetAiming());
                                            p.SetID(world.GetProjectileCount());

                                            //Adds the projectile to the world
                                            if (!world.GetProjectile().ContainsKey(world.GetProjectileCount()))
                                            {
                                                world.GetProjectile().Add(world.GetProjectileCount(), p);
                                            }
                                            else
                                            {
                                                world.SetProjectileCount(world.GetProjectileCount() + 1);

                                            }

                                            //If projectile collides with a wall, kill the projectile and remove it from the dictionary
                                            foreach (Wall w in world.GetWalls().Values)
                                            {

                                                if (ProjectileCollidesWithWall(w, p))
                                                {
                                                    world.GetProjectile().Remove(p.GetID());
                                                }
                                            }
                                            //Resets the projectile shooting counter
                                            tank.SetProjectileReshootCounter(0);

                                        }
                                    }
                                    //If the command from the tank indicates "alt", then it goes in here
                                    if (shooting == "alt")
                                    {
                                        //If the tank has a powerup, then goes into this condition
                                        if (tank.GetPowerup())
                                        {
                                            //Adds a beam for that tank and adds it in the world
                                            Beams b = new Beams(world.GetBeamCounter(), tank.GetID(), tank.GetLocation(), tank.GetAiming());
                                            world.GetBeams().Add(world.GetBeamCounter(), b);

                                            world.SetBeamCounter(world.GetBeamCounter() + 1);
                                        }
                                        //If the tank has no powerup, then it goes in to this condition
                                        else if (!tank.GetPowerup())
                                        {
                                            //If enough time has passed for a tank to reshoot a projectile, then it goes into the condition
                                            if (tank.GetProjectileReshootCounter() >= int.Parse(framePerShot))
                                            {
                                                //Creates a projectile for the tank
                                                Projectile p = new Projectile();
                                                p.SetAlive();
                                                p.SetOwner(tank.GetID());
                                                p.SetLocation(tank.GetLocation());
                                                p.SetOrientation(tank.GetAiming());
                                                p.SetID(world.GetProjectileCount());

                                                //Adds the projectile to the world
                                                if (!world.GetProjectile().ContainsKey(world.GetProjectileCount()))
                                                {
                                                    world.GetProjectile().Add(world.GetProjectileCount(), p);
                                                }
                                                else
                                                {
                                                    world.SetProjectileCount(world.GetProjectileCount() + 1);

                                                }

                                                //If projectile collides with a wall, kill the projectile and remove it from the dictionary
                                                foreach (Wall w in world.GetWalls().Values)
                                                {

                                                    if (ProjectileCollidesWithWall(w, p))
                                                    {
                                                        world.GetProjectile().Remove(p.GetID());
                                                    }
                                                }
                                                //Resets the projectile shooting counter
                                                tank.SetProjectileReshootCounter(0);
                                            }
                                        }

                                    }
                                    //Sets the turret's direction of the tank
                                    tank.SetAiming(control.GetDirection());

                                    //Removes the data from the socket in order to receive data on each frame
                                    socket.RemoveData(0, socket.GetData().Length);
                                    socket.ClearData();
                                }
                                //Increments the tank's projectile reshoot counter by 1
                                tank.SetProjectileReshootCounter(tank.GetProjectileReshootCounter() + 1);
                            }
                        }
                    }
                    //Breaks from the loop in order to receive only once
                    break;
                }

                //Asks for data again from the socket
                Networking.GetData(socket);
            }
        }

        /// <summary>
        /// A method that sends the world's information to all the connected sockets
        /// </summary>
        private static void SendAllInfo()
        {
            //Locks everything in the world in order to be thread safe
            lock (world)
            {
                //If the powerup delay of the world matches the maximum powerup delay, then it adds a powerup
                //if needed, and resets the world's powerup delay
                if (world.GetPowerupDelay() == maxPowerupDelay)
                {
                    AddPowerupIfNeeded();
                    world.SetPowerupDelay(0);
                }


                //Creates a stringBuilder in order to send the information only once
                StringBuilder strBuilder = new StringBuilder();



                //Iterates through the tanks in the world
                foreach (int tankID in world.GetTanks().Keys.ToList())
                {
                    //If the world contains the tank, then goes into this condition 
                    if (world.GetTanks().ContainsKey(tankID))
                    {
                        //Gets the tank associated with the ID
                        Tank t = world.GetTanks()[tankID];

                        //If the tank is dead, then checks if their is time to respawn the tank if the respawn delay matches the tank's
                        //respawn time, where it sets the died flag of the tank to false, randomly draws a tank, sets the hit points to 3
                        // and resets the tank's respawn time, otherwise increments the tank's respawn time by 1
                        if (t.GetDead() == true)
                        {
                            if (int.Parse(respawnDelay) == t.GetRespawnTank())
                            {
                                t.SetDied(false);
                                DrawTank(t);
                                t.SetHitPoints(3);
                                t.SetRespawnTank(0);
                            }
                            else
                            {
                                t.SetRespawnTank(t.GetRespawnTank() + 1);
                            }
                        }

                        //Serializes the tank and appends it to the string builder
                        string serialized = JsonConvert.SerializeObject(t);
                        strBuilder.Append(serialized + "\n");
                    }
                }

                //Iterates through the projectiles in the world
                foreach (int projectileID in world.GetProjectile().Keys.ToList())
                {
                    //Gets the projectile and updates it's new location
                    Projectile p = world.GetProjectile()[projectileID];
                    Vector2D newLocation = p.GetLocation() + (p.GetOrientation() * projectileSpeed);
                    p.SetLocation(newLocation);

                    //If the projectile goes out of bounds, then it sets the projectile to dead
                    if (((-world.Size / 2) > p.GetLocation().GetX()) || (p.GetLocation().GetX() > (world.Size / 2)))
                    {
                        p.SetDead();
                    }
                    if ((-world.Size / 2) > p.GetLocation().GetY() || p.GetLocation().GetY() > (world.Size / 2))
                    {
                        p.SetDead();
                    }

                    //Iterates through the tanks in the world
                    foreach (Tank t in world.GetTanks().Values)
                    {
                        //If the projectile's owner and tank's ID don't match, then it goes into this condition
                        if (t.GetID() != p.GetOwnerID())
                        {
                            //If a projectile collides with a tank, then it goes into the condition
                            if (ProjectileCollidesWithTank(t, p))
                            {
                                //If the hit points of the tank is greater than 0, then it goes into this condition
                                if (t.GetHitPoints() > 0)
                                {
                                    //Decrements the tank's hit point by 1
                                    t.BeenHitByProjectile();

                                    //Sets the projectile as dead
                                    p.SetDead();

                                    //Removes the projectile from the world
                                    world.GetProjectile().Remove(p.GetID());

                                    //If the tank's hit points is 0, then it goes into this condition
                                    if (t.GetHitPoints() == 0)
                                    {
                                        //Sets the tank's dead status as true
                                        t.SetDied(true);

                                        //Increments the projectile owner's score by 1
                                        int projOwner = p.GetOwnerID();

                                        //If the projectile owner is still alive, then it increments the projectile owner's score by 1
                                        if (world.GetTanks().ContainsKey(projOwner))
                                        {
                                            Tank tank = world.GetTanks()[projOwner];
                                            tank.SetScore(tank.GetScore() + 1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Iterates through the walls in the world
                    foreach (Wall w in world.GetWalls().Values)
                    {
                        //If a projectile collides with a wall, then it sets the projectile as dead, and removes it from the world's dictionary
                        if (ProjectileCollidesWithWall(w, p))
                        {
                            p.SetDead();
                            world.GetProjectile().Remove(p.GetID());
                        }

                    }

                    //Serializes the projectile and appends it to the string builder
                    string serialized = JsonConvert.SerializeObject(p);
                    strBuilder.Append(serialized + "\n");
                }

                //Iterates through the powerups in the world
                foreach (int powerupID in world.GetPowerups().Keys.ToList())
                {
                    //Gets the powerup associated with the powerup ID
                    Powerups p = world.GetPowerups()[powerupID];

                    //Iterates through all the tanks in the world
                    foreach (Tank t in world.GetTanks().Values.ToList())
                    {
                        //If the powerup collides with a tank, then it goes in this condition
                        if (PowerupCollidesWithTank(t, p))
                        {
                            //Adds the powerup for the tank
                            t.HasPowerup();

                            //Removes the powerup from the world and sets it dead
                            world.GetPowerups().Remove(p.GetID());
                            p.SetDead();
                        }
                    }

                    //Serializes the powerup and appends it to the string builder
                    string serialized = JsonConvert.SerializeObject(p);
                    strBuilder.Append(serialized + "\n");
                }

                //Iterates through the beams in the world
                foreach (int beamID in world.GetBeams().Keys.ToList())
                {
                    //Gets the beam associated with the id
                    Beams beam = world.GetBeams()[beamID];

                    //Gets the tank which owns the beam
                    Tank beamOwner = world.GetTanks()[beam.GetOwnerID()];

                    //Iterates through the tanks in the world
                    foreach (Tank t in world.GetTanks().Values)
                    {
                        //If the tank's ID doesn't match the beam's owner ID, then it goes into this condition
                        if (t.GetID() != beam.GetOwnerID())
                        {

                            //If the beam intersects a tank, then it goes into this condition
                            if (Intersects(beam.GetOrigin(), beam.GetDirection(), t.GetLocation(), (tankLength / 2)))
                            {
                                //Indicates that the tank has been hit by a beam, and sets it as dead
                                t.BeenHitByBeam();
                                t.SetDied(true);


                                //If the beam owner is still alive, then it increments the beam owner's score by 1
                                if (world.GetTanks().ContainsValue(beamOwner))
                                {
                                    //Increments the beam owner's score by 1
                                    beamOwner.SetScore(beamOwner.GetScore() + 1);

                                }

                            }

                        }
                        //Indicates that the beam owner has used a powerup
                        beamOwner.UsedPowerup();
                    }
                    //Removes the beam from the world regardless of how it's been used
                    world.GetBeams().Remove(beamID);

                    //Serializes the beam and appends it to the string builder
                    string serialized = JsonConvert.SerializeObject(beam);
                    strBuilder.Append(serialized + "\n");
                }



                //Iterates through the sockets that are connected
                foreach (int socketID in connectedSockets.Keys.ToList())
                {
                    //Gets the socketState associated with the socketID
                    SocketState socketState = connectedSockets[socketID];

                    //If the socket state does not have an error, then it sends the information of the world to the socketState
                    if (!socketState.ErrorOccured)
                    {
                        Networking.Send(socketState.TheSocket, strBuilder.ToString());
                    }

                    //If the socket state does have an error, then it enters this condition
                    if (socketState.ErrorOccured == true)
                    {
                        //Gets the tank associated with the socketID
                        Tank t = world.GetTanks()[socketID];
                        //Sets it as dead, disconnected, and sets it's hitpoints to 0
                        t.SetDied(true);
                        t.SetDisconnected(true);
                        t.SetHitPoints(0);

                        //Serializes the tank and appends it to the string builder
                        string removedTank = JsonConvert.SerializeObject(t);
                        strBuilder.Append(removedTank + "\n");


                        //Removes the tank from the world's dictionary and removes the socket from the socket dictionary 
                        world.GetTanks().Remove(socketID);
                        connectedSockets.Remove(socketID);

                        //Indicates in the server that we have a client disconnecting
                        Console.WriteLine(t.GetName() + ", " + socketID.ToString() + " has disconnected from the server");

                        //Clears and closes the socket
                        socketState.ClearData();
                        socketState.TheSocket.Close();

                        //Sends the new string to all the connected sockets in the server
                        foreach (SocketState ss in connectedSockets.Values.ToList())
                        {
                            Networking.Send(ss.TheSocket, strBuilder.ToString());
                        }
                    }
                }
                //Increments the world's powerup delay by 1
                world.SetPowerupDelay(world.GetPowerupDelay() + 1);
            }
        }

        /// <summary>
        /// A method that adds a powerup if needed
        /// </summary>
        private static void AddPowerupIfNeeded()
        {
            lock (world)
            {
                //Adds a powerup if the number of powerups in the world is less than the maximum powerups allowed
                if (world.GetPowerups().Count < maxPowerup)
                {
                    //Sets a random location to draw the powerup
                    double randomX = (r.NextDouble() * world.Size) - (world.Size / 2);
                    double randomY = (r.NextDouble() * world.Size) - (world.Size / 2);
                    Vector2D vec = new Vector2D(randomX, randomY);
                    Powerups p = new Powerups(world.GetPowerupID(), vec, false);
                    bool collision = true;
                    bool collisionOccurred = false;
                    //A loop that occurs while a collision is true
                    while (collision)
                    {

                        //If a powerup collides with a wall, then it sets a collision occurred boolean to true
                        foreach (Wall w in world.GetWalls().Values)
                        {
                            if (PowerupCollidesWithWall(w, p))
                            {
                                collisionOccurred = true;
                            }
                        }

                        //If a collision occurred boolean is false, then it sets collision to false, and breaks from the while loop
                        if (collisionOccurred == false)
                        {
                            collision = false;
                        }

                        //If the collision occurred is true, then it redraws a powerup
                        if (collisionOccurred == true)
                        {
                            //Sets a random location to draw the powerup
                            randomX = (r.NextDouble() * world.Size) - (world.Size / 2);
                            randomY = (r.NextDouble() * world.Size) - (world.Size / 2);
                            Vector2D newVecLoc = new Vector2D(randomX, randomY);
                            p.SetLocation(newVecLoc);
                        }

                    }

                    //Adds a powerup to the world
                    world.GetPowerups().Add(world.GetPowerupID(), p);

                    //Increments the powerup ID
                    world.SetPowerupID(world.GetPowerupID() + 1);
                }
            }
        }

        /// <summary>
        /// A method that draws a tank
        /// </summary>
        private static void DrawTank(Tank tank)
        {
            //Sets a random location to draw the tank
            double randomX = (r.NextDouble() * world.Size) - (world.Size / 2);
            double randomY = (r.NextDouble() * world.Size) - (world.Size / 2);
            Vector2D vec = new Vector2D(randomX, randomY);
            tank.SetLocation(vec);
            bool collision = true;
            //A loop that runs while the collision is true
            while (collision)
            {

                bool upCollision = false;
                bool downCollision = false;
                bool leftCollision = false;
                bool rightCollision = false;
                bool tankCollision = false;
                //Checks if the wall collides with a tank, and if it does, then it sets a collision boolean to true
                foreach (Wall w in world.GetWalls().Values.ToList())
                {
                    if (UpCollision(w, tank))
                    {
                        upCollision = true;

                    }
                    if (DownCollision(w, tank))
                    {
                        downCollision = true;
                    }
                    if (RightCollision(w, tank))
                    {
                        leftCollision = true;
                    }
                    if (LeftCollision(w, tank))
                    {
                        rightCollision = true;
                    }
                }
                //Checks if the tank collides within another tank vicinity and if it does, then it sets a collision boolean to true
                foreach (Tank t in world.GetTanks().Values.ToList())
                {
                    if ((t.GetLocation().GetX() - tankLength <= tank.GetLocation().GetX()) && (t.GetLocation().GetX() + tankLength >= tank.GetLocation().GetX())
                        && (t.GetLocation().GetY() - tankWidth >= tank.GetLocation().GetY()) && (t.GetLocation().GetY() + tankWidth <= tank.GetLocation().GetY()))
                    {
                        collision = true;
                    }
                }
                //If all collisions are false, then it exits the loop
                if ((upCollision == false) && (downCollision == false) && (rightCollision == false) && (leftCollision == false) && (tankCollision == false))
                {
                    collision = false;
                }
                //If a collision is true, then it redraws the tank at a new random location
                if ((upCollision == true) || (downCollision == true) || (rightCollision == true) || (leftCollision == true) || (tankCollision == true))
                {
                    randomX = (r.NextDouble() * world.Size) - (world.Size / 2);
                    randomY = (r.NextDouble() * world.Size) - (world.Size / 2);
                    Vector2D newLoc = new Vector2D(randomX, randomY);
                    tank.SetLocation(newLoc);
                }
            }
        }

        /// <summary>
        /// A method that checks if a powerup collides with a wall
        /// </summary>
        private static bool PowerupCollidesWithWall(Wall w, Powerups p)
        {
            lock (world.GetWalls())
            {

                //Sets the top left coordinate of the wall with an extended buffer accounting for the wall's size
                Vector2D topLeftWall = new Vector2D(w.GetStartingPoint().GetX() - 35, w.GetStartingPoint().GetY() - 35);

                //Sets the bottom right coordinate of the wall with an extended buffer accounting for the wall's size
                Vector2D bottomRightWall = new Vector2D(w.GetEndingPoint().GetX() + 35, w.GetEndingPoint().GetY() + 35);

                //Gets the powerup's location
                Vector2D powerupLocation = new Vector2D(p.GetLocation());


                //Initializes the variables
                bool xCheck = false;
                bool yCheck = false;

                //If the y coordinates match of a wall, i.e, the wall is horizontal, then it goes in this condition
                if (w.GetStartingPoint().GetY() == w.GetEndingPoint().GetY())
                {
                    //If the x coordinate of the starting point is less than the x coordinate of the ending point, then it goes in this condition
                    if (w.GetStartingPoint().GetX() < w.GetEndingPoint().GetX())
                    {
                        //If the powerup's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                        if ((topLeftWall.GetX() <= powerupLocation.GetX()) && (powerupLocation.GetX() <= bottomRightWall.GetX()))
                        {
                            xCheck = true;
                        }
                        //If the powerup's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() <= powerupLocation.GetY()) && (powerupLocation.GetY() <= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                        return xCheck && yCheck;

                    }
                    //If the x coordinate of the starting point is greater than the x coordinate of the ending point, then it goes in this condition
                    else if (w.GetStartingPoint().GetX() > w.GetEndingPoint().GetX())
                    {
                        //Changes the bottom right and top left wall coordinates accordingly
                        bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 35, w.GetStartingPoint().GetY() + 35);
                        topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 35, w.GetEndingPoint().GetY() - 35);

                        //If the powerup's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                        if ((topLeftWall.GetX() <= powerupLocation.GetX()) && (powerupLocation.GetX() <= bottomRightWall.GetX()))
                        {
                            xCheck = true;
                        }

                        //If the topLeft wall's y coordinate is greater than the bottom right wall's y coordinate then goes into this condition
                        if (topLeftWall.GetY() > bottomRightWall.GetY())
                        {
                            //If the powerup's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                            if ((topLeftWall.GetY() >= powerupLocation.GetY()) && (powerupLocation.GetY() >= bottomRightWall.GetY()))
                            {
                                yCheck = true;
                            }
                        }
                        //If the topLeft wall's y coordinate is less than the bottom right wall's y coordinate then goes into this condition
                        else
                        {
                            //If the powerup's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                            if ((topLeftWall.GetY() <= powerupLocation.GetY()) && (powerupLocation.GetY() <= bottomRightWall.GetY()))
                            {
                                yCheck = true;
                            }
                        }

                        return xCheck && yCheck;

                    }
                }
                //Else If the x coordinates match of a wall, i.e, the wall is vertical, then it goes in this condition
                else if (w.GetStartingPoint().GetX() == w.GetEndingPoint().GetX())
                {
                    //If the y coordinate of the starting point is greater than the y coordinate of the ending point, then it goes in this condition
                    if (w.GetStartingPoint().GetY() > w.GetEndingPoint().GetY())
                    {
                        //Changes the bottom right and top left wall coordinates accordingly
                        bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 35, w.GetStartingPoint().GetY() + 35);
                        topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 35, w.GetEndingPoint().GetY() - 35);

                        //If the projectile's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                        if ((topLeftWall.GetX() <= powerupLocation.GetX()) && (powerupLocation.GetX() <= bottomRightWall.GetX()))
                        {
                            xCheck = true;
                        }

                        //If the projectile's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() <= powerupLocation.GetY()) && (powerupLocation.GetY() <= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                        return xCheck && yCheck;
                    }
                    //If the y coordinate of the starting point is less than the y coordinate of the ending point, then it goes in this condition
                    else
                    {
                        //Changes the bottom right and top left wall coordinates accordingly
                        bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 35, w.GetEndingPoint().GetY() + 35);
                        topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 35, w.GetStartingPoint().GetY() - 35);

                        //If the projectile's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                        if ((topLeftWall.GetX() <= powerupLocation.GetX()) && (powerupLocation.GetX() <= bottomRightWall.GetX()))
                        {
                            xCheck = true;
                        }
                        //If the projectile's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() <= powerupLocation.GetY()) && (powerupLocation.GetY() <= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                        return xCheck && yCheck;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Method to check if a projectile collides with a wall
        /// </summary>
        private static bool ProjectileCollidesWithWall(Wall w, Projectile p)
        {
            //Sets the top left coordinate of the wall with an extended buffer accounting for the tank and wall's size
            Vector2D topLeftWall = new Vector2D(w.GetStartingPoint().GetX() - 25, w.GetStartingPoint().GetY() - 25);

            //Sets the bottom right coordinate of the wall with an extended buffer accounting for the tank and wall's size
            Vector2D bottomRightWall = new Vector2D(w.GetEndingPoint().GetX() + 25, w.GetEndingPoint().GetY() + 25);

            //Gets the projectile's location
            Vector2D projLocation = new Vector2D(p.GetLocation());


            //Initializes the variables
            bool xCheck = false;
            bool yCheck = false;

            //If the y coordinates match of a wall, i.e, the wall is horizontal, then it goes in this condition
            if (w.GetStartingPoint().GetY() == w.GetEndingPoint().GetY())
            {
                //If the x coordinate of the starting point is less than the x coordinate of the ending point, then it goes in this condition
                if (w.GetStartingPoint().GetX() < w.GetEndingPoint().GetX())
                {
                    //If the projectile's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= projLocation.GetX()) && (projLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the projectile's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= projLocation.GetY()) && (projLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;

                }
                //If the x coordinate of the starting point is greater than the x coordinate of the ending point, then it goes in this condition
                else if (w.GetStartingPoint().GetX() > w.GetEndingPoint().GetX())
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 25, w.GetStartingPoint().GetY() + 25);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 25, w.GetEndingPoint().GetY() - 25);

                    //If the projectile's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= projLocation.GetX()) && (projLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }

                    //If the topLeft wall's y coordinate is greater than the bottom right wall's y coordinate then goes into this condition
                    if (topLeftWall.GetY() > bottomRightWall.GetY())
                    {
                        //If the projectile's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() >= projLocation.GetY()) && (projLocation.GetY() >= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                    }
                    //If the topLeft wall's y coordinate is less than the bottom right wall's y coordinate then goes into this condition
                    else
                    {
                        //If the projectile's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() <= projLocation.GetY()) && (projLocation.GetY() <= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                    }

                    return xCheck && yCheck;

                }
            }
            //Else If the x coordinates match of a wall, i.e, the wall is vertical, then it goes in this condition
            else if (w.GetStartingPoint().GetX() == w.GetEndingPoint().GetX())
            {
                //If the y coordinate of the starting point is greater than the y coordinate of the ending point, then it goes in this condition
                if (w.GetStartingPoint().GetY() > w.GetEndingPoint().GetY())
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 25, w.GetStartingPoint().GetY() + 25);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 25, w.GetEndingPoint().GetY() - 25);

                    //If the projectile's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= projLocation.GetX()) && (projLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }

                    //If the projectile's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= projLocation.GetY()) && (projLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }
                //If the y coordinate of the starting point is less than the y coordinate of the ending point, then it goes in this condition
                else
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 25, w.GetEndingPoint().GetY() + 25);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 25, w.GetStartingPoint().GetY() - 25);

                    //If the projectile's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= projLocation.GetX()) && (projLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the projectile's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= projLocation.GetY()) && (projLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }
            }
            return false;
        }

        /// <summary>
        /// A helper method that checks if a tank receives a powerup
        /// </summary>
        private static bool PowerupCollidesWithTank(Tank t, Powerups p)
        {
            //Gets the powerup location and stores it in a vector
            Vector2D powerupLoc = new Vector2D(p.GetLocation());

            //Gets the tank's location and stores it in a vector
            Vector2D tankLoc = new Vector2D(t.GetLocation());

            //Checks if the distance from the powerup and the tank is less than half of the length of the tank, and then returns true

            if ((powerupLoc - tankLoc).Length() <= (tankLength / 2))
            {
                return true;
            }
            //Returns false if we didn't collide
            return false;
        }

        /// <summary>
        /// A helper method that checks if a projectile collides with a tank
        /// </summary>
        private static bool ProjectileCollidesWithTank(Tank t, Projectile p)
        {
            //Gets the projectile location and stores it in a vector
            Vector2D projLoc = new Vector2D(p.GetLocation());

            //Gets the tank's location and stores it in a vector
            Vector2D tankLoc = new Vector2D(t.GetLocation());

            //Checks if the distance from the projectile and the tank is less than half of the length of the tank, and then returns true
            if ((projLoc - tankLoc).Length() <= (tankLength / 2))
            {
                return true;
            }
            //Returns false if we didn't collide
            return false;
        }

        /// <summary>
        /// Sends information that can't change for a socket, at the time of their connection
        /// </summary>
        private static void SendInformationThatCannotChange(SocketState socket, int tankID, string universeSize)
        {
            //Initializes the string builder
            StringBuilder strBuilder = new StringBuilder();
            
            //Appends the tankID and the universe size
            strBuilder.Append(tankID + "\n");
            strBuilder.Append(universeSize + "\n");

            //Serializes each wall and appends it to a stringBuilder
            foreach (int id in world.GetWalls().Keys.ToList())
            {
                Wall w = world.GetWalls()[id];
                string serialized = JsonConvert.SerializeObject(w);
                strBuilder.Append(serialized + "\n");
                //Networking.Send(socket.TheSocket, serialized + "\n");
            }
            //Sends the string to the client
            Networking.Send(socket.TheSocket, strBuilder.ToString());
        }

        /// <summary>
        /// Checks for a collision between a tank and a wall if a tank is moving up
        /// </summary>
        /// <Citation> @1935 on Piazza </Citation>
        /// <Citation> https://www.geeksforgeeks.org/find-two-rectangles-overlap/ </Citation>
        private static bool UpCollision(Wall w, Tank tank)
        {
            //Sets the top left coordinate of the wall with an extended buffer accounting for the tank and wall's size
            Vector2D topLeftWall = new Vector2D(w.GetStartingPoint().GetX() - 55, w.GetStartingPoint().GetY() - 55);

            //Sets the bottom right coordinate of the wall with an extended buffer accounting for the tank and wall's size
            Vector2D bottomRightWall = new Vector2D(w.GetEndingPoint().GetX() + 55, w.GetEndingPoint().GetY() + 55);

            //Gets the tank's location
            Vector2D tankLocation = new Vector2D(tank.GetLocation().GetX(), tank.GetLocation().GetY());

            //Initializes the booleans for checking x and y coordinates
            bool xCheck = false;
            bool yCheck = false;

            //If the y coordinates match of a wall, i.e, the wall is horizontal, then it goes in this condition
            if (w.GetStartingPoint().GetY() == w.GetEndingPoint().GetY())
            {
                //If the x coordinate of the starting point is less than the x coordinate of the ending point, then it goes in this condition
                if (w.GetStartingPoint().GetX() < w.GetEndingPoint().GetX())
                {
                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }
                //If the x coordinate of the starting point is greater than the x coordinate of the ending point, then it goes in this condition
                else if (w.GetStartingPoint().GetX() > w.GetEndingPoint().GetX())
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 55, w.GetStartingPoint().GetY() + 55);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 55, w.GetEndingPoint().GetY() - 55);

                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the topLeft wall's y coordinate is greater than the bottom right wall's y coordinate then goes into this condition
                    if (topLeftWall.GetY() > bottomRightWall.GetY())
                    {
                        //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() >= tankLocation.GetY()) && (tankLocation.GetY() >= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                    }
                    //If the topLeft wall's y coordinate is less than the bottom right wall's y coordinate then goes into this condition
                    else
                    {
                        //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                    }
                    return xCheck && yCheck;

                }
            }
            //Else If the x coordinates match of a wall, i.e, the wall is vertical, then it goes in this condition
            else if (w.GetStartingPoint().GetX() == w.GetEndingPoint().GetX())
            {
                //If the y coordinate of the starting point is greater than the y coordinate of the ending point, then it goes in this condition
                if (w.GetStartingPoint().GetY() > w.GetEndingPoint().GetY())
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 55, w.GetEndingPoint().GetY() + 55);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 55, w.GetStartingPoint().GetY() - 55);

                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }

                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() >= tankLocation.GetY()) && (tankLocation.GetY() >= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }

                //If the y coordinate of the starting point is less than the y coordinate of the ending point, then it goes in this condition
                else
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 55, w.GetEndingPoint().GetY() + 55);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 55, w.GetStartingPoint().GetY() - 55);

                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }

                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for a collision between a tank and a wall if a tank is moving down
        /// </summary>
        /// <Citation> @1935 on Piazza </Citation>
        /// <Citation> https://www.geeksforgeeks.org/find-two-rectangles-overlap/ </Citation>
        private static bool DownCollision(Wall w, Tank tank)
        {

            //Sets the top left coordinate of the wall with an extended buffer accounting for the tank and wall's size
            Vector2D topLeftWall = new Vector2D(w.GetStartingPoint().GetX() - 55, w.GetStartingPoint().GetY() - 55);

            //Sets the bottom right coordinate of the wall with an extended buffer accounting for the tank and wall's size
            Vector2D bottomRightWall = new Vector2D(w.GetEndingPoint().GetX() + 55, w.GetEndingPoint().GetY() + 55);

            //Gets the tank's location
            Vector2D tankLocation = new Vector2D(tank.GetLocation().GetX(), tank.GetLocation().GetY());

            //Initializes the booleans for checking x and y coordinates
            bool xCheck = false;
            bool yCheck = false;

            //If the y coordinates match of a wall, i.e, the wall is horizontal, then it goes in this condition
            if (w.GetStartingPoint().GetY() == w.GetEndingPoint().GetY())
            {
                //If the x coordinate of the starting point is less than the x coordinate of the ending point, then it goes in this condition
                if (w.GetStartingPoint().GetX() < w.GetEndingPoint().GetX())
                {
                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }

                //If the x coordinate of the starting point is greater than the x coordinate of the ending point, then it goes in this condition
                else if (w.GetStartingPoint().GetX() > w.GetEndingPoint().GetX())
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 55, w.GetStartingPoint().GetY() + 55);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 55, w.GetEndingPoint().GetY() - 55);

                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the topLeft wall's y coordinate is greater than the bottom right wall's y coordinate, then it goes in this condition
                    if (topLeftWall.GetY() > bottomRightWall.GetY())
                    {
                        //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() >= tankLocation.GetY()) && (tankLocation.GetY() >= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                    }
                    //If the topLeft wall's y coordinate is less than the bottom right wall's y coordinate, then it goes in this condition
                    else
                    {
                        //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                    }
                    return xCheck && yCheck;
                }
            }
            //Else If the x coordinates match of a wall, i.e, the wall is vertical, then it goes in this condition
            else if (w.GetStartingPoint().GetX() == w.GetEndingPoint().GetX())
            {

                //If the y coordinate of the starting point is greater than the y coordinate of the ending point, then it goes in this condition
                if (w.GetStartingPoint().GetY() > w.GetEndingPoint().GetY())
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 55, w.GetStartingPoint().GetY() + 55);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 55, w.GetEndingPoint().GetY() - 55);

                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }

                //If the y coordinate of the starting point is less than the y coordinate of the ending point, then it goes in this condition
                else
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 55, w.GetEndingPoint().GetY() + 55);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 55, w.GetStartingPoint().GetY() - 55);

                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for a collision between a tank and a wall if a tank is moving right
        /// </summary>
        /// <Citation> @1935 on Piazza </Citation>
        /// <Citation> https://www.geeksforgeeks.org/find-two-rectangles-overlap/ </Citation>
        private static bool RightCollision(Wall w, Tank tank)
        {
            //Sets the top left coordinate of the wall with an extended buffer accounting for the tank and wall's size
            Vector2D topLeftWall = new Vector2D(w.GetStartingPoint().GetX() - 55, w.GetStartingPoint().GetY() - 55);

            //Sets the bottom right coordinate of the wall with an extended buffer accounting for the tank and wall's size
            Vector2D bottomRightWall = new Vector2D(w.GetEndingPoint().GetX() + 55, w.GetEndingPoint().GetY() + 55);

            //Gets the tank's location
            Vector2D tankLocation = new Vector2D(tank.GetLocation().GetX(), tank.GetLocation().GetY());

            //Initializes the booleans for checking x and y coordinates
            bool xCheck = false;
            bool yCheck = false;

            //If the y coordinates match of a wall, i.e, the wall is horizontal, then it goes in this condition
            if (w.GetStartingPoint().GetY() == w.GetEndingPoint().GetY())
            {
                //If the x coordinate of the starting point is less than the x coordinate of the ending point, then it goes in this condition
                if (w.GetStartingPoint().GetX() < w.GetEndingPoint().GetX())
                {
                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;

                }
                //If the x coordinate of the starting point is greater than the x coordinate of the ending point, then it goes in this condition
                else if (w.GetStartingPoint().GetX() > w.GetEndingPoint().GetX())
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 55, w.GetStartingPoint().GetY() + 55);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 55, w.GetEndingPoint().GetY() - 55);

                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the topLeftWall's y coordinate is greater than the bottomRightWall's y coordinate, then it goes into this condition
                    if (topLeftWall.GetY() > bottomRightWall.GetY())
                    {
                        //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() >= tankLocation.GetY()) && (tankLocation.GetY() >= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                    }
                    //If the topLeftWall's y coordinate is greater than the bottomRightWall's y coordinate, then it goes into this condition
                    else
                    {
                        //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                    }
                    return xCheck && yCheck;
                }
            }
            //Else If the x coordinates match of a wall, i.e, the wall is vertical, then it goes in this condition
            else if (w.GetStartingPoint().GetX() == w.GetEndingPoint().GetX())
            {
                //If the y coordinate of the starting point of a wall is greater than the y coordinate of the ending point, then goes into this condition
                if (w.GetStartingPoint().GetY() > w.GetEndingPoint().GetY())
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 55, w.GetStartingPoint().GetY() + 55);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 55, w.GetEndingPoint().GetY() - 55);

                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }
                //If the y coordinate of the starting point of a wall is less than the y coordinate of the ending point, then goes into this condition
                else
                {
                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }

            }
            return false;
        }

        /// <summary>
        /// Checks for a collision between a tank and a wall if a tank is moving left
        /// </summary>
        /// <Citation> @1935 on Piazza </Citation>
        /// <Citation> https://www.geeksforgeeks.org/find-two-rectangles-overlap/ </Citation>
        private static bool LeftCollision(Wall w, Tank tank)
        {
            //Sets the top left coordinate of the wall with an extended buffer accounting for the tank and wall's size
            Vector2D topLeftWall = new Vector2D(w.GetStartingPoint().GetX() - 55, w.GetStartingPoint().GetY() - 55);

            //Sets the bottom right coordinate of the wall with an extended buffer accounting for the tank and wall's size
            Vector2D bottomRightWall = new Vector2D(w.GetEndingPoint().GetX() + 55, w.GetEndingPoint().GetY() + 55);

            //Gets the tank's location
            Vector2D tankLocation = new Vector2D(tank.GetLocation().GetX(), tank.GetLocation().GetY());

            //Initializes the booleans for checking x and y coordinates
            bool xCheck = false;
            bool yCheck = false;

            //If the y coordinates match of a wall, i.e, the wall is horizontal, then it goes in this condition
            if (w.GetStartingPoint().GetY() == w.GetEndingPoint().GetY())
            {
                //If the x coordinate of the starting point is less than the x coordinate of the ending point, then it goes in this condition
                if (w.GetStartingPoint().GetX() < w.GetEndingPoint().GetX())
                {
                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }
                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;

                }
                //If the x coordinate of the starting point is greater than the x coordinate of the ending point, then it goes in this condition
                else if (w.GetStartingPoint().GetX() > w.GetEndingPoint().GetX())
                {
                    //Changes the bottom right and top left wall coordinates accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 55, w.GetStartingPoint().GetY() + 55);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 55, w.GetEndingPoint().GetY() - 55);

                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }

                    //If the topLeftWall's y coordinate is greater than the bottomRightWall's y coordinate, then it goes into this condition
                    if (topLeftWall.GetY() > bottomRightWall.GetY())
                    {
                        //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() >= tankLocation.GetY()) && (tankLocation.GetY() >= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                    }
                    //Else If the topLeftWall's y coordinate is less than the bottomRightWall's y coordinate, then it goes into this condition
                    else
                    {
                        //If the tank's y coordinate is in the range of the wall's x coordinates, then it sets yCheck to true
                        if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                        {
                            yCheck = true;
                        }
                    }
                    return xCheck && yCheck;
                }
            }
            //Else If the x coordinates match of a wall, i.e, the wall is vertical, then it goes in this condition
            else if (w.GetStartingPoint().GetX() == w.GetEndingPoint().GetX())
            {
                //If the y coordinate of the starting point is greater than the y coordinate of the ending point, then it goes in this condition
                if (w.GetStartingPoint().GetY() > w.GetEndingPoint().GetY())
                {
                    //Changes the bottom right and top left walls accordingly
                    bottomRightWall = new Vector2D(w.GetStartingPoint().GetX() + 55, w.GetStartingPoint().GetY() + 55);
                    topLeftWall = new Vector2D(w.GetEndingPoint().GetX() - 55, w.GetEndingPoint().GetY() - 55);

                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }

                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }
                //If the y coordinate of the starting point is less than the y coordinate of the ending point, then it goes in this condition
                else
                {
                    //If the tank's x coordinate is in the range of the wall's x coordinates, then it sets xCheck to true
                    if ((topLeftWall.GetX() <= tankLocation.GetX()) && (tankLocation.GetX() <= bottomRightWall.GetX()))
                    {
                        xCheck = true;
                    }

                    //If the tank's y coordinate is in the range of the wall's y coordinates, then it sets yCheck to true
                    if ((topLeftWall.GetY() <= tankLocation.GetY()) && (tankLocation.GetY() <= bottomRightWall.GetY()))
                    {
                        yCheck = true;
                    }
                    return xCheck && yCheck;
                }

            }
            return false;
        }

        /// <summary>
        /// Reads the settings of the game in an XML form
        /// </summary>
        /// <param name="file"></param>
        /// <Citation> https://stackoverflow.com/questions/1068473/c-a-simple-way-to-bring-one-line-xml-files-to-human-readable-multi-line </Citation>
        /// <Citation> https://stackoverflow.com/questions/11847965/foreach-loop-xmlnodelist </Citation>
        private static void ReadSettings(String file)
        {
            //A temporary dictionary to store the walls. Done so to avoid an issue with the world being null
            Dictionary<int, Wall> walls = new Dictionary<int, Wall>();

            try
            {
                //Loads the file to an xmlDocument so that it is more readable
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(file);

                //Gets everything within the GameSettings node and adds it into an XmlNode list
                XmlNodeList xmlNode = xmlDoc.SelectNodes("GameSettings");
                if (xmlNode.Count > 0)
                {
                    for (int i = 0; i < xmlNode.Count; i++)
                    {
                        //Gets the text within the UniverseSize tags and sets that to the universeSize
                        universeSize = xmlNode[i].SelectSingleNode("UniverseSize").InnerText;

                        //Creates a world based on the universeSize dimensions
                        world = new World(int.Parse(universeSize));

                        //Gets the text within the MSPerFrame tag and sets that to the timePerFrame
                        timePerFrame = xmlNode[i].SelectSingleNode("MSPerFrame").InnerText;

                        //Parses the timePerFrame and stores it to MSPerFrame
                        MSPerFrame = long.Parse(timePerFrame);

                        //Gets the text within the FramesPerShot tag and set that to the framePerShot
                        framePerShot = xmlNode[i].SelectSingleNode("FramesPerShot").InnerText;

                        //Gets the text within the RespawnRate tag and set that to the respawnDelay
                        respawnDelay = xmlNode[i].SelectSingleNode("RespawnRate").InnerText;

                        //Gets all the walls in the document and adds it to an XmlNodeList
                        XmlNodeList wallNodeList = xmlDoc.GetElementsByTagName("Wall");

                        //Iterates through all the walls
                        for (int j = 0; j < wallNodeList.Count; j++)
                        {
                            //Initializes the x and y coordinates for p1 and p2
                            int wallp1x = 0;
                            int wallp1y = 0;
                            int wallp2x = 0;
                            int wallp2y = 0;

                            //Gets everything within the "p1" nodes of a wall
                            XmlNodeList wallp1Node = wallNodeList[j].SelectNodes("p1");

                            //Gets the x and y coordinates associated with p1
                            for (int p1 = 0; p1 < wallp1Node.Count; p1++)
                            {
                                wallp1x = int.Parse(wallp1Node[p1].SelectSingleNode("x").InnerText);
                                wallp1y = int.Parse(wallp1Node[p1].SelectSingleNode("y").InnerText);
                            }


                            //Gets everything within the "p2" nodes of a wall
                            XmlNodeList wallp2Node = wallNodeList[j].SelectNodes("p2");

                            //Gets the x and y coordinates associated with p2
                            for (int p2 = 0; p2 < wallp2Node.Count; p2++)
                            {
                                wallp2x = int.Parse(wallp2Node[p2].SelectSingleNode("x").InnerText);
                                wallp2y = int.Parse(wallp2Node[p2].SelectSingleNode("y").InnerText);
                            }

                            //Creates a wall and adds it to a dictionary associated to the walls
                            Wall w = new Wall(j, wallp1x, wallp1y, wallp2x, wallp2y);

                            if (!walls.ContainsKey(j))
                            {
                                walls.Add(j, w);
                            }

                        }
                    }
                }
                lock (world)
                {
                    foreach (int id in walls.Keys.ToList())
                    {
                        Wall w = walls[id];
                        world.GetWalls().Add(id, w);
                    }
                }
            }
            catch (Exception e)
            {
                //Throws an exception saying the file was incorrectly formatted if an issue was found with the file
                throw new Exception("File was incorrectly formatted");
            }
        }

        /// <summary>
        /// Determines if a ray interescts a circle
        /// </summary>
        /// <param name="rayOrig">The origin of the ray</param>
        /// <param name="rayDir">The direction of the ray</param>
        /// <param name="center">The center of the circle</param>
        /// <param name="r">The radius of the circle</param>
        /// <returns></returns>
        /// <Citation> Code given to us by Prof. Kopta </Citation>
        public static bool Intersects(Vector2D rayOrig, Vector2D rayDir, Vector2D center, double r)
        {
            // ray-circle intersection test
            // P: hit point
            // ray: P = O + tV
            // circle: (P-C)dot(P-C)-r^2 = 0
            // substituting to solve for t gives a quadratic equation:
            // a = VdotV
            // b = 2(O-C)dotV
            // c = (O-C)dot(O-C)-r^2
            // if the discriminant is negative, miss (no solution for P)
            // otherwise, if both roots are positive, hit

            double a = rayDir.Dot(rayDir);
            double b = ((rayOrig - center) * 2.0).Dot(rayDir);
            double c = (rayOrig - center).Dot(rayOrig - center) - r * r;

            // discriminant
            double disc = b * b - 4.0 * a * c;

            if (disc < 0.0)
                return false;

            // find the signs of the roots
            // technically we should also divide by 2a
            // but all we care about is the sign, not the magnitude
            double root1 = -b + Math.Sqrt(disc);
            double root2 = -b - Math.Sqrt(disc);

            return (root1 > 0.0 && root2 > 0.0);
        }

    }
}