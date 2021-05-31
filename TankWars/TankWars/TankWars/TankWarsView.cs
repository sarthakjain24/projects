using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankWars
{
    /// <summary>
    /// A class representing the main game frame(The Form)
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    /// <Class> CS 3500 University of Utah Fall 2020</Class>
    /// <Professor>Prof. Daniel Kopta</Professor>
    public partial class TankWarsView : Form
    {
        //A variable representing the GameController
        private GameController controller;

        //A variable representing the game world
        private World world;

        //A variable that represents the drawing panel
        private DrawingPanel drawingPanel;

        //A constant variable representing the player's view
        private const int viewSize = 900;

        //A constant variable representing the menu's size
        private const int menuSize = 40;

        /// <summary>
        /// Subscribes to the events and initializes the variables needed in the constructor
        /// </summary>
        public TankWarsView(GameController ctl)
        {
            InitializeComponent();
            controller = ctl;

            FormClosed += OnExit;
            world = controller.GetWorld();
            controller.UpdateArrived += OnFrame;
            controller.Error += ShowError;
            controller.Connected += HandleConnected;

            ClientSize = new Size(viewSize, viewSize + menuSize);


            drawingPanel = new DrawingPanel(world, controller);
            drawingPanel.Location = new Point(0, menuSize);
            drawingPanel.Size = new Size(viewSize, viewSize);
            drawingPanel.MouseDown += HandleMouseDown;
            drawingPanel.MouseUp += HandleMouseUp;
            drawingPanel.MouseMove += HandleMouseMovement;

            Controls.Add(drawingPanel);


            this.KeyDown += HandleKeyDown;
            this.KeyUp += HandleKeyUp;


        }

        /// <summary>
        /// Loads the form
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Closes the game when the user clicks on the 'X' button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handler for the controller's Connected event
        /// </summary>
        private void HandleConnected()
        {
            // Shows a message box saying that the user was able to connect to the server
            this.Invoke(new MethodInvoker(
              () => MessageBox.Show("Connected to server" + Environment.NewLine)));
        }
        /// <summary>
        /// Handler for the controller's Error event
        /// </summary>
        private void ShowError(string err)
        {
            // Show the errors in a MessageBox
            MessageBox.Show(err);

            //Disables KeyPreview if an error occurs
            KeyPreview = false;

            // Then re-enable the controls so the user can reconnect
            this.Invoke(new MethodInvoker(
              () =>
              {
                  connectButton.Enabled = true;
                  serverTextBox.Enabled = true;
                  nameBox.Enabled = true;
              }));
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            //If the server box is empty, then it asks the user to enter a valid server address
            if (serverTextBox.Text == "")
            {
                MessageBox.Show("Please enter a Server Address");
                return;
            }

            //If the name box is empty, then it asks the user to enter a valid name
            if (nameBox.Text == "")
            {
                MessageBox.Show("Please enter a name");
                return;
            }

            // Disables the controls and try to connect
            connectButton.Enabled = false;
            serverTextBox.Enabled = false;
            nameBox.Enabled = false;

            //Sets keyPreview to true in order for the user
            KeyPreview = true;

            //Calls the controller's connect method
            controller.Connect(serverTextBox.Text, nameBox.Text);

        }



        /// <summary>
        /// Handle the form closing by shutting down the socket cleanly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExit(object sender, FormClosedEventArgs e)
        {
            //Calls the controller's close
            controller.Close();
        }

        /// <summary>
        /// Handler for the controllers UpdateArrived event that essentially draws everything each frame
        /// </summary>
        private void OnFrame()
        {
            try
            {
                //Sets the world to the controller's world
                world = controller.GetWorld();
                //Passes this world into the drawing panel, so that the MethodInvoker calls OnPaint to redraw on each frame in the form
                MethodInvoker invoker = new MethodInvoker(() => this.Invalidate(true));
                this.Invoke(invoker);

            }

            //Catches an ObjectDisposedException and calls the Close method in the controller class
            catch (ObjectDisposedException e)
            {
                controller.Close();
            }
        }

        /// <summary>
        /// Gets the GameController
        /// </summary>
        public GameController GetController()
        {
            return controller;
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            //If the key was W, then it calls the handleMoveUp method in the controller
            if (e.KeyCode == Keys.W)
            {
                controller.HandleMoveUp();
            }
            //If the key was A, then it calls the handleMoveLeft method in the controller
            if (e.KeyCode == Keys.A)
            {
                controller.HandleMoveLeft();
            }
            //If the key was S, then it calls the handleMoveDown method in the controller
            if (e.KeyCode == Keys.S)
            {
                controller.HandleMoveDown();
            }
            //If the key was D, then it calls the handleMoveRight method in the controller
            if (e.KeyCode == Keys.D)
            {
                controller.HandleMoveRight();
            }
            e.Handled = true;
            e.SuppressKeyPress = true;

        }
        /// <summary>
        /// A key up event based on when the user releases the key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            //If the key was W, then it calls the cancelMoveUp method in the controller
            if (e.KeyCode == Keys.W)
            {
                controller.CancelMoveUp();
            }
            //If the key was A, then it calls the cancelMoveLeft method in the controller
            if (e.KeyCode == Keys.A)
            {
                controller.CancelMoveLeft();
            }
            //If the key was S, then it calls the cancelMoveDown method in the controller
            if (e.KeyCode == Keys.S)
            {
                controller.CancelMoveDown();
            }
            //If the key was D, then it calls the cancelMoveRight method in the controller
            if (e.KeyCode == Keys.D)
            {
                controller.CancelMoveRight();
            }
            e.Handled = true;
            e.SuppressKeyPress = true;

        }
        /// <summary>
        /// A private mouse event to send the mouse location
        /// </summary>
        private void HandleMouseMovement(object sender, MouseEventArgs e)
        {
            //Passes the mouse's x and y location to the mousde helper method
            controller.MouseHelper(e.X, e.Y);
        }

        /// <summary>
        /// A private mouse event for when a mouse button is released
        /// </summary>
        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            //If the left button is clicked, then it calls the controller's left mouse button let go method
            if (e.Button == MouseButtons.Left)
            {
                controller.LeftMouseLetGo();
            }
            //If the right button is clicked, then it calls the controller's right mouse button let go method
            if (e.Button == MouseButtons.Right)
            {
                controller.RightMouseLetGo();
            }
        }

        /// <summary>
        /// A private mouse event for when a mouse is clicked
        /// </summary>
        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            //If the left button is clicked, then it calls the controller's left mouse button clicked method
            if (e.Button == MouseButtons.Left)
            {
                controller.LeftMouseClicked();
            }
            //If the right button is clicked, then it calls the controller's right mouse button clicked method
            if (e.Button == MouseButtons.Right)
            {
                controller.RightMouseClicked();
            }

        }

        /// <summary>
        /// Shows the controls when the user clicks on the Controls under the help menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Controls are:" + "\n" +
                "W: Move up" + "\n" +
                "A: Move left" + "\n" +
                "S: Move down" + "\n" +
                "D: Move right" + "\n" +
                "Mouse: Aim" + "\n" +
                "Right Click: Beam" + "\n" +
                "Left Click: Projectile");


        }
        /// <summary>
        /// Shows the about section when the user clicks on About under the help menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TankWars solution:" + "\n"
                 + "By Sarthak Jain and Dimiitrius Maritsas" + "\n"
                 + "CS 3500, University of Utah, Fall 2020" + "\n"
                 + "Professor Daniel Kopta");
        }


    }
}
