using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace TankWars
{
    /// <summary>
    /// The controller class in the TankWars game, responsible for almost everything
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    public class GameController
    {
        // A delegate for the MessageHandler
        public delegate void MessageHandler();

        //An event for the delegate called by the view 
        public event MessageHandler UpdateArrived;

        //A delegate for the ConnectedHandler
        public delegate void ConnectedHandler();

        //An event for the delegate called by the view
        public event ConnectedHandler Connected;

        //A delegate for the ErrorHandling
        public delegate void ErrorHandler(string err);

        //An event for the delegate called by the view
        public event ErrorHandler Error;

        //A delegate for the MessageSender
        public delegate void MessageSender();

        //An event for the delegate called by the view
        public event MessageSender MessagesSent;

        //A variable that represents the player's name
        private String playerName;

        //A variable that represents the server
        private SocketState theServer;

        //A variable representing the x coordinate
        private double xCoord;

        //A variable representing the y coordinate
        private double yCoord;

        //A variable that represents a key was pressed up
        private bool keyPressedUp;

        //A variable that represents a key was pressed down
        private bool keyPressedDown;

        //A variable that represents a key was pressed left
        private bool keyPressedLeft;

        //A variable that represents a key was pressed right
        private bool keyPressedRight;

        //A variable that represents the left mouse was clicked
        private bool leftMousePressed;

        //A variable that represents the right mouse was clicked
        private bool rightMousePressed;

        //A variable that represents the player's number
        private int playerNum;

        //A variable that represents the world's dimension
        private int worldDimension;

        //A variable that represents that the world dimension has been given
        bool haveDimension;

        //A variable that represents that the player's number has been given
        bool havePlayerNum;

        //A constant variable representing the world size as 900 
        private const double worldSize = 900;

        //A variable representing the world 
        private World world;

        //A variable representing the vector to be passed when sending information to the server
        private Vector2D vec;


        /// <summary>
        /// Constructor that initializes the variables
        /// </summary>
        public GameController()
        {
            theServer = null;
            playerName = null;
            leftMousePressed = false;
            rightMousePressed = false;
            keyPressedUp = false;
            keyPressedDown = false;
            keyPressedLeft = false;
            keyPressedRight = false;
            vec = new Vector2D();
            world = new World(worldDimension);
        }

        /// <summary>
        /// Connects to the server
        /// </summary>
        public void Connect(string address, string name)
        {
            //Sets the player's name as the name
            playerName = name;

            //If the player's name is less than 16, then it shows an error
            if (playerName.Length > 16)
            {
                Error("Name has to be less than 16 characters");
                return;
            }
            //Connects to the server
            Networking.ConnectToServer(OnConnect, address, 11000);

        }

        /// <summary>
        /// Method to be invoked by the networking library when a connection is made
        /// </summary>
        /// <param name="state"></param>
        private void OnConnect(SocketState state)
        {
            if (state.ErrorOccured)
            {
                // inform the view
                Error("Error connecting to server");
                return;
            }

            // inform the view
            Connected();

            //Sets theServer with the state
            theServer = state;

            // Start an event loop to receive messages from the server
            state.OnNetworkAction = ReceiveStartup;
            Networking.Send(state.TheSocket, playerName + "\n");
            Networking.GetData(state);

        }


        /// <summary>
        /// Method to be invoked by the networking library when 
        /// data is available
        /// </summary>
        /// <param name="state"></param>
        private void ReceiveStartup(SocketState state)
        {
            if (state.ErrorOccured)
            {
                // inform the view
                Error("Lost connection to server");
                return;
            }


            //Receives the information about the world
            ReceiveWorld(state);

            //Gets the data from the network
            Networking.GetData(state);
        }



        /// <summary>
        /// Process any buffered messages separated by '\n'
        /// Then inform the view
        /// </summary>
        /// <param name="state"></param>
        private void ReceiveWorld(SocketState state)
        {
            //Gets the data from the state and splits it by a new line
            string totalData = state.GetData();
            string[] parts = Regex.Split(totalData, @"(?<=[\n])");

            // Loop until we have processed all messages.
            // We may have received more than one.

            //Gets the player number, which should only be once
            int playerNumber = 0;
            havePlayerNum = int.TryParse(parts[0], out playerNumber);
            parts[0] = "";
            if (playerNumber != 0)
            {
                playerNum = playerNumber;
            }

            //Gets the dimensions of the world that should only happen once
            int dim = 0;
            haveDimension = int.TryParse(parts[1], out dim);
            parts[1] = "";
            if (dim != 0)
            {
                worldDimension = dim;
                world = new World(worldDimension);
            }

            //Iterates through all the data given by the server
            foreach (string p in parts)
            {

                // Ignore empty strings added by the regex splitter
                if (p.Length == 0)
                    continue;
                // The regex splitter will include the last string even if it doesn't end with a '\n',
                // So we need to ignore it if this happens. 
                if (p[p.Length - 1] != '\n')
                    break;

                //Locks with a world so that we process information in a single thread
                lock (world)
                {
                    //Parses the object with the JSON
                    JObject jObject = JObject.Parse(p);

                    //Converts the JSON object to a token based on the name of the string
                    JToken projToken = jObject["proj"];
                    JToken beamToken = jObject["beam"];
                    JToken tankToken = jObject["tank"];
                    JToken wallToken = jObject["wall"];
                    JToken powerToken = jObject["power"];

                    //If the projToken is not null, i.e. if the JSON string passed was a projectile, then it goes in this condition
                    if (projToken != null)
                    {
                        //Deserializes the string and converts it to a projectile
                        Projectile proj = JsonConvert.DeserializeObject<Projectile>(p);

                        //Adds the projectile to the world
                        world.SetProjectile(proj.GetID(), proj);

                        //If projectile is dead, removes the projectile from the world
                        if (proj.GetDead() == true)
                        {
                            world.GetProjectile().Remove(proj.GetID());
                        }

                    }

                    //If the beamToken is not null, i.e. if the JSON string passed was a beam, then it goes in this condition
                    if (beamToken != null)
                    {
                        //Deserializes the string and converts it to a beam
                        Beams b = JsonConvert.DeserializeObject<Beams>(p);

                        //Adds the beam in the world's beam dictionary
                        world.SetBeams(b.GetBeamID(), b);
                    }

                    //If the tankToken is not null, i.e. if the JSON string passed was a tank, then it goes in this condition
                    if (tankToken != null)
                    {
                        //Deserializes the string and converts it to a tank
                        Tank t = JsonConvert.DeserializeObject<Tank>(p);

                        //Sets the color of the tank based on the tank's ID
                        t.SetColor(t.GetID());

                        //Adds the tank to the world's tank dictionary
                        world.SetTanks(t.GetID(), t);

                        //If the hitpoints of the tank are 0, then it remove it from the dictionary
                        if (t.GetHitPoints() == 0)
                        {
                            world.GetTanks().Remove(t.GetID());
                        }

                        //If the tank gets disconnected, then it remove it from the dictionary
                        if (t.GetDisconnected())
                        {
                            world.GetTanks().Remove(t.GetID());
                        }

                        //If the tank is dead, then it remove it from the dictionary
                        if (t.GetDead())
                        {
                            world.GetTanks().Remove(t.GetID());
                        }
                    }

                    //If the wallToken is not null, i.e. if the JSON string passed was a wall, then it goes in this condition
                    if (wallToken != null)
                    {
                        //Deserializes the string and converts it to a wall
                        Wall w = JsonConvert.DeserializeObject<Wall>(p);

                        //Adds the wall to the world's wall dictionary
                        world.SetWalls(w.GetID(), w);
                    }




                    //If the powerToken is not null, i.e. if the JSON string passed was a powerup, then it goes in this condition
                    if (powerToken != null)
                    {
                        //Deserializes the string and converts it to a powerup
                        Powerups power = JsonConvert.DeserializeObject<Powerups>(p);

                        //Adds the powerup to the world's powerup dictionary
                        world.SetPowerups(power.GetID(), power);

                        //If the powerup is dead, then it removes it from the dictionary
                        if (power.GetDead())
                        {
                            world.GetPowerups().Remove(power.GetID());
                        }
                    }
                }




                // Then remove it from the SocketState's growable buffer
                state.RemoveData(0, p.Length);
            }

            if (UpdateArrived != null)
            {
                // inform the view to redraw
                UpdateArrived();
            }

            //Inform the server
            Process();
        }

        /// <summary>
        /// Returns the world associated with the controller
        /// </summary>
        public World GetWorld()
        {
            return world;
        }

        /// <summary>
        /// Closes the connection with the server
        /// </summary>
        public void Close()
        {
            theServer?.TheSocket.Close();
        }
        /// <summary>
        /// Method that informs the server
        /// </summary>
        public void Process()
        {
            lock (world)
            {
                String movingDir = "";

                //If the keyPressed was up, then it sets moving dir to up
                if (keyPressedUp)
                {
                    movingDir = "up";
                }
                //If the keyPressed was left, then it sets moving dir to left
                if (keyPressedLeft)
                {
                    movingDir = "left";
                }
                //If the keyPressed was right, then it sets moving dir to right
                if (keyPressedRight)
                {
                    movingDir = "right";
                }
                //If the keyPressed was down, then it sets moving dir to down
                if (keyPressedDown)
                {
                    movingDir = "down";
                }
                //If the keyPressed was not pressed, then it sets moving dir to none
                if (!keyPressedLeft && !keyPressedDown && !keyPressedRight && !keyPressedUp)
                {
                    movingDir = "none";
                }


                String fireType = "";

                //If the left mouse was pressed, sets fireType to main
                if (leftMousePressed)
                {
                    fireType = "main";
                }
                //If the right mouse was pressed, sets fireType to alt
                else if (rightMousePressed)
                {
                    fireType = "alt";
                }
                //If the left mouse and right mouse were not pressed, sets fireType to none
                else if (!leftMousePressed && !rightMousePressed)
                {
                    fireType = "none";
                }

                //Creates a new vector with the xCoord, and yCoord and normalizes the new vector created
                vec = new Vector2D(xCoord, yCoord);
                vec.Normalize();

                //Passes this information in a ControlCommands
                ControlCommands c = new ControlCommands(movingDir, fireType, vec);

                //Serializes the control commands
                string serializedString = JsonConvert.SerializeObject(c);

                //If the server is open, then it passes the serialized string to the server
                if (theServer != null)
                {
                    Networking.Send(theServer.TheSocket, serializedString + "\n");
                }
            }
        }

        /// <summary>
        /// Gets the player's num
        /// </summary>
        public int GetPlayerNum()
        {
            return playerNum;
        }
        /// <summary>
        /// Sets the x and y coordinate of the tank centered in the world's size
        /// </summary>
        public void MouseHelper(double x, double y)
        {
            xCoord = x - (worldSize / 2);
            yCoord = y - (worldSize / 2);
        }

        /// <summary>
        /// Method that sets the keyPressedUp boolean to true
        /// </summary>
        public void HandleMoveUp()
        {
            keyPressedUp = true;
        }

        /// <summary>
        /// Method that sets the keyPressedDown boolean to true
        /// </summary>
        public void HandleMoveDown()
        {
            keyPressedDown = true;
        }
        /// <summary>
        /// Method that sets the keyPressedLeft boolean to true
        /// </summary>
        public void HandleMoveLeft()
        {
            keyPressedLeft = true;
        }

        /// <summary>
        /// Method that sets the keyPressedRight boolean to true
        /// </summary>
        public void HandleMoveRight()
        {
            keyPressedRight = true;
        }

        /// <summary>
        /// Method that sets the keyPressedUp boolean to false
        /// </summary>
        public void CancelMoveUp()
        {
            keyPressedUp = false;
        }

        /// <summary>
        /// Method that sets the keyPressedDown boolean to false
        /// </summary>
        public void CancelMoveDown()
        {
            keyPressedDown = false;
        }
        /// <summary>
        /// Method that sets the keyPressedLeft boolean to false
        /// </summary>
        public void CancelMoveLeft()
        {
            keyPressedLeft = false;
        }

        /// <summary>
        /// Method that sets the keyPressedRight boolean to false
        /// </summary>
        public void CancelMoveRight()
        {
            keyPressedRight = false;
        }
        /// <summary>
        /// Sets the leftMousePressed boolean to false
        /// </summary>
        public void LeftMouseLetGo()
        {
            leftMousePressed = false;
        }
        /// <summary>
        /// Sets the rightMousePressed boolean to true
        /// </summary>
        public void LeftMouseClicked()
        {
            leftMousePressed = true;
        }
        /// <summary>
        /// Sets the rightMousePressed boolean to false
        /// </summary>
        public void RightMouseLetGo()
        {
            rightMousePressed = false;
        }
        /// <summary>
        /// Sets the rightMousePressed boolean to true
        /// </summary>
        public void RightMouseClicked()
        {
            rightMousePressed = true;
        }
    }
}
