using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankWars
{
    /// <summary>
    /// A DrawingPanel that is represented in the Form, where all the drawing happens
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    public class DrawingPanel : Panel
    {
        //A World variable to represent the world
        private World world;

        //A GameController variable to represent the controller
        private GameController controller;

        //An Image variable to represent the backgrouds
        private Image background;

        //An Image variable to represent the wall
        private Image wall;

        //An Image variable to represent a blue tank
        private Image blueTank;

        //An Image variable to represent a blue turret
        private Image blueTurret;

        //An Image variable to represent a dark blue tank
        private Image darkBlueTank;

        //An Image variable to represent a dark blue turret
        private Image darkBlueTurret;

        //An Image variable to represent a green tank
        private Image greenTank;

        //An Image variable to represent a green turret
        private Image greenTurret;

        //An Image variable to represent a light green tank
        private Image lightGreenTank;

        //An Image variable to represent a light green turret
        private Image lightGreenTurret;

        //An Image variable to represent an orange tank
        private Image orangeTank;

        //An Image variable to represent an orange turret
        private Image orangeTurret;

        //An Image variable to represent a red tank
        private Image redTank;

        //An Image variable to represent a red turret
        private Image redTurret;

        //An Image variable to represent a yellow tank
        private Image yellowTank;

        //An Image variable to represent a yellow turret
        private Image yellowTurret;

        //An Image variable to represent a purple tank
        private Image purpleTank;

        //An Image variable to represent a purple turret
        private Image purpleTurret;

        //An Image variable to represent a purple shot
        private Image purpleShot;

        //An Image variable to represent a blue shot
        private Image blueShot;

        //An Image variable to represent a red shot
        private Image redShot;

        //An Image variable to represent a yellow shot
        private Image yellowShot;

        //An Image variable to represent a brown shot
        private Image brownShot;

        //An Image variable to represent a white shot
        private Image whiteShot;

        //An Image variable to represent a green shot
        private Image greenShot;

        //An Image variable to represent a grey shot
        private Image greyShot;

        //A variable to keep track of the player's num
        private int playerNum;

        //A constant variable to keep track of the view size
        private const int viewSize = 900;

        //A variable representing the player's X coordinate in world space
        private double playerX;

        //A variable representing the player's Y coordinate in world space
        private double playerY;

        //A variable to keep track of the beam frame count
        private int beamFrameCount;

        //Initializes the variables
        public DrawingPanel(World w, GameController ctr)
        {
            controller = ctr;
            playerNum = controller.GetPlayerNum();
            beamFrameCount = 0;
            DoubleBuffered = true;
            world = w;
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            background = Image.FromFile(dir + "\\..\\Resources/Images/Background.png");
            wall = Image.FromFile(dir + "\\..\\Resources/Images/WallSprite.png");
            blueTank = Image.FromFile(dir + "\\..\\Resources/Images/BlueTank.png");
            blueTurret = Image.FromFile(dir + "\\..\\Resources/Images/BlueTurret.png");
            darkBlueTank = Image.FromFile(dir + "\\..\\Resources/Images/DarkTank.png");
            darkBlueTurret = Image.FromFile(dir + "\\..\\Resources/Images/DarkTurret.png");
            greenTank = Image.FromFile(dir + "\\..\\Resources/Images/GreenTank.png");
            greenTurret = Image.FromFile(dir + "\\..\\Resources/Images/GreenTurret.png");
            lightGreenTank = Image.FromFile(dir + "\\..\\Resources/Images/LightGreenTank.png");
            lightGreenTurret = Image.FromFile(dir + "\\..\\Resources/Images/LightGreenTurret.png");
            orangeTank = Image.FromFile(dir + "\\..\\Resources/Images/OrangeTank.png");
            orangeTurret = Image.FromFile(dir + "\\..\\Resources/Images/OrangeTurret.png");
            redTank = Image.FromFile(dir + "\\..\\Resources/Images/RedTank.png");
            redTurret = Image.FromFile(dir + "\\..\\Resources/Images/RedTurret.png");
            purpleTank = Image.FromFile(dir + "\\..\\Resources/Images/PurpleTank.png");
            purpleTurret = Image.FromFile(dir + "\\..\\Resources/Images/PurpleTurret.png");
            yellowTank = Image.FromFile(dir + "\\..\\Resources/Images/YellowTank.png");
            yellowTurret = Image.FromFile(dir + "\\..\\Resources/Images/YellowTurret.png");
            purpleShot = Image.FromFile(dir + "\\..\\Resources/Images/shot_violet.png");
            blueShot = Image.FromFile(dir + "\\..\\Resources/Images/shot_blue.png");
            redShot = Image.FromFile(dir + "\\..\\Resources/Images/shot_red_new.png");
            yellowShot = Image.FromFile(dir + "\\..\\Resources/Images/shot-yellow.png");
            brownShot = Image.FromFile(dir + "\\..\\Resources/Images/shot-brown.png");
            greenShot = Image.FromFile(dir + "\\..\\Resources/Images/shot-green.png");
            greyShot = Image.FromFile(dir + "\\..\\Resources/Images/shot_grey.png");
            whiteShot = Image.FromFile(dir + "\\..\\Resources/Images/shot-white.png");

            //Sets the background to black
            this.BackColor = Color.Black;
        }



        /// <summary>
        /// Helper method for DrawObjectWithTransform
        /// </summary>
        /// <param name="size">The world (and image) size</param>
        /// <param name="w">The worldspace coordinate</param>
        /// <returns></returns>
        private static int WorldSpaceToImageSpace(int size, double w)
        {
            return (int)w + size / 2;
        }

        // A delegate for DrawObjectWithTransform
        // Methods matching this delegate can draw whatever they want using e  
        public delegate void ObjectDrawer(object o, PaintEventArgs e);


        /// <summary>
        /// This method performs a translation and rotation to drawn an object in the world.
        /// </summary>
        /// <param name="e">PaintEventArgs to access the graphics (for drawing)</param>
        /// <param name="o">The object to draw</param>
        /// <param name="worldSize">The size of one edge of the world (assuming the world is square)</param>
        /// <param name="worldX">The X coordinate of the object in world space</param>
        /// <param name="worldY">The Y coordinate of the object in world space</param>
        /// <param name="angle">The orientation of the objec, measured in degrees clockwise from "up"</param>
        /// <param name="drawer">The drawer delegate. After the transformation is applied, the delegate is invoked to draw whatever it wants</param>
        private void DrawObjectWithTransform(PaintEventArgs e, object o, int worldSize, double worldX, double worldY, double angle, ObjectDrawer drawer)
        {
            // "push" the current transform
            System.Drawing.Drawing2D.Matrix oldMatrix = e.Graphics.Transform.Clone();

            int x = WorldSpaceToImageSpace(worldSize, worldX);
            int y = WorldSpaceToImageSpace(worldSize, worldY);
            e.Graphics.TranslateTransform(x, y);
            e.Graphics.RotateTransform((float)angle);
            drawer(o, e);

            // "pop" the transform
            e.Graphics.Transform = oldMatrix;
        }


        /// <summary>
        /// Acts as a drawing delegate for DrawObjectWithTransform
        /// After performing the necessary transformation (translate/rotate)
        /// DrawObjectWithTransform will invoke this method
        /// </summary>
        /// <param name="o">The object to draw</param>
        /// <param name="e">The PaintEventArgs to access the graphics</param>
        private void HealthDrawer(object o, PaintEventArgs e)
        {
            //Converts the object to a Tank
            Tank t = o as Tank;

            //Sets the fullHealth size
            int fullHealthWidth = 60;

            //Sets the height of the bar
            int height = 10;

            //Sets the once hit health width
            int onceHitHealthWidth = 40;

            //Sets the twice hit health width
            int twiceHitHealthWidth = 15;

            //Doesn't smoothen the graphics
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            using (System.Drawing.SolidBrush redBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
            using (System.Drawing.SolidBrush greenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green))
            using (System.Drawing.SolidBrush yellowBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow))
            using (System.Drawing.SolidBrush transparentBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Transparent))

            {
                // Rectangles are drawn starting from the top-left corner.
                // So if we want the rectangle centered on the player's location, we have to offset it
                // by half its size to the left (-width/2) and up (-height/2)
                Rectangle fullHealth = new Rectangle(-(fullHealthWidth / 2), -(height / 2), fullHealthWidth, height);
                Rectangle partialHealth = new Rectangle(-(onceHitHealthWidth / 2), -(height / 2), onceHitHealthWidth, height);
                Rectangle almostDeadHealth = new Rectangle(-(twiceHitHealthWidth / 2), -(height / 2), twiceHitHealthWidth, height);

                //If hit points is 1, then it draws a smaller red rectangle
                if (t.GetHitPoints() == 1)
                {
                    e.Graphics.FillRectangle(redBrush, almostDeadHealth);
                }
                //If hit points is 2, then it draws a slightly bigger yellow rectangle
                else if (t.GetHitPoints() == 2)
                {
                    e.Graphics.FillRectangle(yellowBrush, partialHealth);
                }
                //If hit points is 3, then it draws a full green rectangle
                else if (t.GetHitPoints() == 3)
                {
                    e.Graphics.FillRectangle(greenBrush, fullHealth);
                }
                //If hit points is 0, then it draws a transparent rectangle, i.e, no health
                else
                {
                    e.Graphics.FillRectangle(transparentBrush, fullHealth);
                }
            }
        }



        /// <summary>
        /// Draws the background
        /// </summary>
        private void BackgroundDrawer(object o, PaintEventArgs e)
        {
            //Sets the width and height of the world size
            int width = world.Size;
            int height = world.Size;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            //Centers the background based on the width and height
            Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);

            //Draws the background using the image
            e.Graphics.DrawImage(background, r);

        }

        /// <summary>
        /// Draws a tank
        /// </summary>
        private void TankDrawer(object o, PaintEventArgs e)
        {
            //Converts the object as a Tank
            Tank t = o as Tank;
            //Creates a tank of size 60
            int tankWidth = 60;
            //Doesn't attempt to smoothen the drawing
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            //Centers the tank
            Rectangle r = new Rectangle(-(tankWidth / 2), -(tankWidth / 2), tankWidth, tankWidth);

            //Draws the tank based on the tank's color
            if (t.GetColor() == Color.Red)
            {
                e.Graphics.DrawImage(redTank, r);
            }
            else if (t.GetColor() == Color.Yellow)
            {
                e.Graphics.DrawImage(yellowTank, r);
            }
            else if (t.GetColor() == Color.Blue)
            {
                e.Graphics.DrawImage(blueTank, r);
            }
            else if (t.GetColor() == Color.DarkBlue)
            {
                e.Graphics.DrawImage(darkBlueTank, r);
            }
            else if (t.GetColor() == Color.Purple)
            {
                e.Graphics.DrawImage(purpleTank, r);
            }
            else if (t.GetColor() == Color.Orange)
            {
                e.Graphics.DrawImage(orangeTank, r);
            }
            else if (t.GetColor() == Color.Green)
            {
                e.Graphics.DrawImage(greenTank, r);
            }
            else if (t.GetColor() == Color.LightGreen)
            {
                e.Graphics.DrawImage(lightGreenTank, r);
            }

        }

        /// <summary>
        /// Draws a turret
        /// </summary>
        private void TurretDrawer(object o, PaintEventArgs e)
        {
            //Converts the object to a tank
            Tank t = o as Tank;
            //Sets the turret width of a tank
            int turretWidth = 50;

            //Doesn not smoothen the drawing
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            //Centers the turret
            Rectangle r = new Rectangle(-(turretWidth / 2), -(turretWidth / 2), turretWidth, turretWidth);

            //Draws the turret based on the color of the tank
            if (t.GetColor() == Color.Red)
            {
                e.Graphics.DrawImage(redTurret, r);
            }
            else if (t.GetColor() == Color.Yellow)
            {
                e.Graphics.DrawImage(yellowTurret, r);
            }
            else if (t.GetColor() == Color.Blue)
            {
                e.Graphics.DrawImage(blueTurret, r);
            }
            else if (t.GetColor() == Color.DarkBlue)
            {
                e.Graphics.DrawImage(darkBlueTurret, r);
            }
            else if (t.GetColor() == Color.Purple)
            {
                e.Graphics.DrawImage(purpleTurret, r);
            }
            else if (t.GetColor() == Color.Orange)
            {
                e.Graphics.DrawImage(orangeTurret, r);
            }
            else if (t.GetColor() == Color.Green)
            {
                e.Graphics.DrawImage(greenTurret, r);
            }
            else if (t.GetColor() == Color.LightGreen)
            {
                e.Graphics.DrawImage(lightGreenTurret, r);
            }

        }


        /// <summary>
        /// Draws the beam
        /// </summary>
        private void BeamDrawer(object o, PaintEventArgs e)
        {
            //Converts the object as a beam
            Beams b = o as Beams;
            //Gets the width of the tank
            int tankWidth = 50;
            //Sets AntiAlias as on, in order to smoothen the beam
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Uses the different brushes to draw the beam
            using (System.Drawing.SolidBrush blueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue))
            using (System.Drawing.SolidBrush darkBlueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkBlue))
            using (System.Drawing.SolidBrush lightGreenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen))
            using (System.Drawing.SolidBrush greenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green))
            using (System.Drawing.SolidBrush yellowBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow))
            using (System.Drawing.SolidBrush orangeBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Orange))
            using (System.Drawing.SolidBrush redBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
            using (System.Drawing.SolidBrush purpleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Purple))
            {

                //Creates a rectangle to represent the center of the tank
                Rectangle r = new Rectangle(-(tankWidth / 2), -(tankWidth / 2), tankWidth, tankWidth);

                //Draws the beam based on the color of the pen decided by the beam owner's ID's color starting from the center of the 
                //tank going to the end of the screen in any direction
                if (b.GetOwnerID() % 8 == 0)
                {
                    Pen bluePen = new Pen(blueBrush);
                    e.Graphics.DrawLine(bluePen, new Point(0, 0), new Point(0, -2000));
                }
                else if (b.GetOwnerID() % 8 == 1)
                {
                    Pen darkBluePen = new Pen(darkBlueBrush);
                    e.Graphics.DrawLine(darkBluePen, new Point(0, 0), new Point(0, -2000));
                }
                else if (b.GetOwnerID() % 8 == 2)
                {
                    Pen redPen = new Pen(redBrush);
                    e.Graphics.DrawLine(redPen, new Point(0, 0), new Point(0, -2000));
                }
                else if (b.GetOwnerID() % 8 == 3)
                {
                    Pen purplePen = new Pen(purpleBrush);
                    e.Graphics.DrawLine(purplePen, new Point(0, 0), new Point(0, -2000));
                }
                else if (b.GetOwnerID() % 8 == 4)
                {
                    Pen orangePen = new Pen(orangeBrush);
                    e.Graphics.DrawLine(orangePen, new Point(0, 0), new Point(0, -2000));
                }
                else if (b.GetOwnerID() % 8 == 5)
                {
                    Pen greenPen = new Pen(greenBrush);
                    e.Graphics.DrawLine(greenPen, new Point(0, 0), new Point(0, -2000));
                }
                else if (b.GetOwnerID() % 8 == 6)
                {
                    Pen lightGreenPen = new Pen(lightGreenBrush);
                    e.Graphics.DrawLine(lightGreenPen, new Point(0, 0), new Point(0, -2000));
                }
                else if (b.GetOwnerID() % 8 == 7)
                {
                    Pen yellowPen = new Pen(yellowBrush);
                    e.Graphics.DrawLine(yellowPen, new Point(0, 0), new Point(0, -2000));
                }
            }




        }
        /// <summary>
        /// Draws the name and the score of the user
        /// </summary>
        private void NameDrawer(object o, PaintEventArgs e)
        {
            //Converts the object to a Tank
            Tank t = o as Tank;

            //Gets the width of the tank
            int tankWidth = 50;

            //Sets the graphics setting of the name
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            //Draws the string centered on the player's location, by using a whiteBrush and using an Arial font 8
            using (System.Drawing.SolidBrush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
            {
                Rectangle r = new Rectangle(-(tankWidth / 2), -(tankWidth / 2), tankWidth, tankWidth);
                Font f = new Font("Arial", 8);
                e.Graphics.DrawString(t.GetName() + ": " + t.GetScore(), f, whiteBrush, r);
            }
        }

        /// <summary>
        /// Draws the wall in the background
        /// </summary>
        private void WallDrawer(object o, PaintEventArgs e)
        {
            //Converts the object to a Wall
            Wall w = o as Wall;

            //Sets the length and width of the wall to 50
            int width = 50;
            int height = 50;

            //Does not smoothen the graphics
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

            //Centers the wall
            Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);

            //Draws the wall
            e.Graphics.DrawImage(wall, r);
        }



        /// <summary>
        /// Acts as a drawing delegate for DrawObjectWithTransform
        /// After performing the necessary transformation (translate/rotate)
        /// DrawObjectWithTransform will invoke this method
        /// </summary>
        /// <param name="o">The object to draw</param>
        /// <param name="e">The PaintEventArgs to access the graphics</param>
        /// 


        /// <summary>
        /// Draws the outside part of a powerup
        /// </summary>
        private void PowerupDrawer(object o, PaintEventArgs e)
        {
            //Converts the object as a powerup
            Powerups p = o as Powerups;

            //Sets the width and height of the powerup as 15
            int width = 15;
            int height = 15;

            //Smoothens the circle
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (System.Drawing.SolidBrush redBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
            using (System.Drawing.SolidBrush yellowBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow))
            using (System.Drawing.SolidBrush blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black))
            using (System.Drawing.SolidBrush darkGreenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkGreen))
            using (System.Drawing.SolidBrush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White))

            {
                // Circles are drawn starting from the top-left corner.
                // So if we want the circle centered on the powerup's location, we have to offset it
                // by half its size to the left (-width/2) and up (-height/2)
                Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);

                //Draws a black circle as the background
                e.Graphics.FillEllipse(blackBrush, r);
            }
        }


        /// <summary>
        /// Acts as a drawing delegate for DrawObjectWithTransform
        /// After performing the necessary transformation (translate/rotate)
        /// DrawObjectWithTransform will invoke this method
        /// </summary>
        /// <param name="o">The object to draw</param>
        /// <param name="e">The PaintEventArgs to access the graphics</param>
        private void PowerupDrawerInsideCircle(object o, PaintEventArgs e)
        {
            //Converts the object as a Powerup
            Powerups p = o as Powerups;

            //Draws a powerup of size 11
            int width = 11;
            int height = 11;
            //Smoothens the circle
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (System.Drawing.SolidBrush redBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
            using (System.Drawing.SolidBrush yellowBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow))
            using (System.Drawing.SolidBrush pinkBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Pink))
            using (System.Drawing.SolidBrush limeBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Lime))
            using (System.Drawing.SolidBrush blueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue))

            {
                // Circles are drawn starting from the top-left corner.
                // So if we want the circle centered on the powerup's location, we have to offset it
                // by half its size to the left (-width/2) and up (-height/2)
                Rectangle r = new Rectangle(-(width / 2), -(height / 2), width, height);

                //Draws a blue powerup is the id is divisible by 5
                if (p.GetID() % 5 == 0)
                    e.Graphics.FillEllipse(blueBrush, r);
                //Draws a red powerup is the id is divisible by 5 and gets a remainder of 1
                if (p.GetID() % 5 == 1)
                    e.Graphics.FillEllipse(redBrush, r);
                //Draws a yellow powerup is the id is divisible by 5 and gets a remainder of 2
                if (p.GetID() % 5 == 2)
                    e.Graphics.FillEllipse(yellowBrush, r);
                //Draws a pink powerup is the id is divisible by 5 and gets a remainder of 3
                if (p.GetID() % 5 == 3)
                    e.Graphics.FillEllipse(pinkBrush, r);
                //Draws a green powerup is the id is divisible by 5 and gets a remainder of 4
                if (p.GetID() % 5 == 4)
                    e.Graphics.FillEllipse(limeBrush, r);

            }
        }


        /// <summary>
        /// Draws a projectile
        /// </summary>
        private void ProjectileDrawer(object o, PaintEventArgs e)
        {
            //Converts the object to a projectile
            Projectile p = o as Projectile;

            //Sets the width to 30
            int width = 30;

            //Doesn't smoothen the projectile drawing
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            using (System.Drawing.SolidBrush blueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue))
            using (System.Drawing.SolidBrush yellowBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow))
            using (System.Drawing.SolidBrush darkBlueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkBlue))
            using (System.Drawing.SolidBrush greenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green))
            using (System.Drawing.SolidBrush lightGreenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen))
            using (System.Drawing.SolidBrush redBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
            using (System.Drawing.SolidBrush orangeBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Orange))
            using (System.Drawing.SolidBrush purpleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Purple))
            {
                //Centers the projectile
                Rectangle r = new Rectangle(-(width / 2), -(width / 2), width, width);

                Color c = new Color();
                foreach (int id in world.GetTanks().Keys)
                {
                    //Sets the color based on the tank's color
                    Tank t = world.GetTanks()[id];
                    if (t.GetID() == p.GetOwnerID())
                    {
                        c = t.GetColor();
                    }
                }
                //Draws the projectile based on what the tank's color is
                if (c == Color.Red)
                {
                    e.Graphics.DrawImage(redShot, r);
                }
                else if (c == Color.Yellow)
                {
                    e.Graphics.DrawImage(yellowShot, r);
                }
                else if (c == Color.Blue)
                {
                    e.Graphics.DrawImage(blueShot, r);
                }
                else if (c == Color.DarkBlue)
                {
                    e.Graphics.DrawImage(whiteShot, r);
                }
                else if (c == Color.Purple)
                {
                    e.Graphics.DrawImage(purpleShot, r);
                }
                else if (c == Color.Orange)
                {
                    e.Graphics.DrawImage(brownShot, r);
                }
                else if (c == Color.Green)
                {
                    e.Graphics.DrawImage(greenShot, r);
                }
                else if (c == Color.LightGreen)
                {
                    e.Graphics.DrawImage(greyShot, r);
                }
            }
        }


        /// <summary>
        /// This method is invoked when the DrawingPanel needs to be re-drawn
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {

            //Gets the world based on the world of the controller
            world = controller.GetWorld();

            //Gets the playerNum based on the controller's player number
            playerNum = controller.GetPlayerNum();

            //Only attempts to draw if the world contains the tanks
            if (world.GetTanks().Count > 0)
            {
                //Locks the thread with the world as the key to draw everything in one thread to avoid changes while drawing
                lock (world)
                {
                    //Locks the player's view in the center based on ther user's controlled tank, if the tank is not dead
                    if (world.GetTanks().TryGetValue(playerNum, out Tank tank) && !tank.GetDead())
                    {
                        //Sets the player's X and Y coordinate
                        playerX = tank.GetLocation().GetX();
                        playerY = tank.GetLocation().GetY();

                        //Centers the player's view
                        double ratio = (double)viewSize / (double)world.Size;
                        int halfSizeScaled = (int)(world.Size / 2.0 * ratio);

                        double inverseTranslateX = -WorldSpaceToImageSpace(world.Size, playerX) + halfSizeScaled;
                        double inverseTranslateY = -WorldSpaceToImageSpace(world.Size, playerY) + halfSizeScaled;

                        e.Graphics.TranslateTransform((float)inverseTranslateX, (float)inverseTranslateY);
                    }
                    else
                    {
                        //If the tank is not dead, then it sets the location as the last place of death of the tank and center's the player's view
                        double ratio = (double)viewSize / (double)world.Size;
                        int halfSizeScaled = (int)(world.Size / 2.0 * ratio);

                        double inverseTranslateX = -WorldSpaceToImageSpace(world.Size, playerX) + halfSizeScaled;
                        double inverseTranslateY = -WorldSpaceToImageSpace(world.Size, playerY) + halfSizeScaled;

                        e.Graphics.TranslateTransform((float)inverseTranslateX, (float)inverseTranslateY);

                    }




                    //Doesn't draw anything if the world size is less than equal to 0
                    if (world.Size <= 0)
                    {
                        return;
                    }
                    //Draws the background
                    DrawObjectWithTransform(e, null, world.Size, 0, 0, 0, BackgroundDrawer);

                    //Draws all the walls in the world
                    foreach (Wall w in world.GetWalls().Values.ToList())
                    {
                        for (double i = w.GetStartingPoint().GetX(); i <= w.GetEndingPoint().GetX(); i = i + 50)
                        {
                            for (double j = w.GetStartingPoint().GetY(); j <= w.GetEndingPoint().GetY(); j = j + 50)
                            {
                                //Draws all the walls in the condition that the starting point is smaller than ending points of the walls
                                DrawObjectWithTransform(e, w, world.Size, i, j, 0, WallDrawer);
                            }
                        }

                        for (double i = w.GetEndingPoint().GetX(); i <= w.GetStartingPoint().GetX(); i = i + 50)
                        {
                            for (double j = w.GetEndingPoint().GetY(); j <= w.GetStartingPoint().GetY(); j = j + 50)
                            {
                                //Draws all the walls in the condition that the starting point is greater than ending points of the walls
                                DrawObjectWithTransform(e, w, world.Size, i, j, 0, WallDrawer);
                            }
                        }
                    }

                    //Draws all the powerups
                    foreach (Powerups p in world.GetPowerups().Values)
                    {
                        //Draws the outside and inside circle for the powerups
                        DrawObjectWithTransform(e, p, world.Size, p.GetLocation().GetX(), p.GetLocation().GetY(), 0, PowerupDrawer);
                        DrawObjectWithTransform(e, p, world.Size, p.GetLocation().GetX(), p.GetLocation().GetY(), 0, PowerupDrawerInsideCircle);

                    }

                    //Draws all the tanks in the world
                    foreach (Tank t in world.GetTanks().Values)
                    {
                        //Draws the tank
                        DrawObjectWithTransform(e, t, world.Size, t.GetLocation().GetX(), t.GetLocation().GetY(), t.GetOrientation().ToAngle(), TankDrawer);
                        
                        //Draws the turret
                        DrawObjectWithTransform(e, t, world.Size, t.GetLocation().GetX(), t.GetLocation().GetY(), t.GetAiming().ToAngle(), TurretDrawer);
                        
                        //Draws the name
                        DrawObjectWithTransform(e, t, world.Size, t.GetLocation().GetX(), t.GetLocation().GetY() + 60, 0, NameDrawer);
                        
                        //Draws the health
                        DrawObjectWithTransform(e, t, world.Size, t.GetLocation().GetX(), t.GetLocation().GetY() - 40, 0, HealthDrawer);
                    }

                    //Draws all the projectiles in the world
                    foreach (Projectile p in world.GetProjectile().Values.ToList())
                    {
                        //Draws the projectile
                        DrawObjectWithTransform(e, p, world.Size, p.GetLocation().GetX(), p.GetLocation().GetY(), p.GetOrientation().ToAngle(), ProjectileDrawer);
                    }

                    //Draws all the beams in the world
                    foreach (Beams b in world.GetBeams().Values.ToList())
                    {
                        //Draws the beams
                        DrawObjectWithTransform(e, b, world.Size, b.GetOrigin().GetX(), b.GetOrigin().GetY(), b.GetDirection().ToAngle(), BeamDrawer);
                        //Removes the beams after 20 iterations
                        beamFrameCount++;
                        if (beamFrameCount == 20)
                        {
                            world.GetBeams().Remove(b.GetBeamID());
                            beamFrameCount = 0;
                        }
                    }
                }
            }
            //Calls the base
            base.OnPaint(e);
        }



        private void DrawingPanel_Load(object sender, EventArgs e)
        {

        }
    }
}

