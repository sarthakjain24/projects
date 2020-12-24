package lightsOut;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.FlowLayout;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.util.Random;
import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.SwingUtilities;

/**
 * The game LightsOut where the goal is to turn all the boxes black to win the game, where there is an option to play
 * the game manually, where clicking on the box turns the adjacent boxes change color, and the goal is to make all the
 * boxes black which means that the game is over, and a green box being the box is empty, and a black box meaning that
 * the box is full
 * 
 * @author Sarthak Jain
 *
 */
public class LightsOut implements ActionListener, MouseListener
{
    /**
     * A private constant variable representing the rows of the board which is 5
     */
    private static final int ROW = 5;
    /**
     * A private constant variable representing the cols of the board which is 5
     */
    private static final int COL = 5;
    /**
     * A private constant variable representing the number of grids on the board which is 25
     */
    private static final int NUMGRIDS = 25;
    /**
     * A private instance variable representing the board
     */
    private Grid board[][];
    /**
     * A private instance variable creating the frame, which displays everything
     */
    private JFrame frame;
    /**
     * A private instance variable which creates the label of the win
     */
    private JLabel win;
    /**
     * A private instance variable which creates the label of the moves
     */
    private JLabel move;
    /**
     * A private instance variable which creates the button of which can switch the game to manual
     */
    private JButton manualMode;
    /**
     * A private instance variable which creates the button with option of quitting the game
     */
    private JButton quit;
    /**
     * A private instance variable which creates the button of which can create a new game
     */
    private JButton newGame;
    /**
     * A private instance variable which creates the panel where the grid exists
     */
    private JPanel grid;
    /**
     * A private instance variable which creates the panel of where the controls exist
     */
    private JPanel controls;
    /**
     * A private instance variable which creates the panel of where the labels exist
     */
    private JPanel labels;
    /**
     * A private instance variable which creates the panel of the finalFrames
     */
    private JPanel finalFrame;
    /**
     * A private instance variable which keeps tracks of the moves
     */
    private int moves;
    /**
     * A private instance variable which keeps tracks of the wins
     */
    private int wins;
    /**
     * A private instance variable which keeps track of an individual grid being filled
     */
    private int filledGrid;
    /**
     * A private instance variable that indicates whether the game is over or not
     */
    private boolean gameOver;
    /**
     * A private instance variable that indicates whether the game is manual or not
     */
    private boolean manualGame;
    /**
     * A private instance variable that indicates whether the game is finished or not
     */
    private boolean finish;

    /**
     * Launches the program
     */
    public static void main (String args[])
    {
        SwingUtilities.invokeLater( () -> new LightsOut());
    }

    /**
     * The constructor which creates the frame, and where the board of the game is made
     */
    public LightsOut ()
    {
        // Creates the frame
        frame = new JFrame();

        // Sets the title of the JFrame
        frame.setTitle("Lights Out by Sarthak Jain, CS 1410, Fall 2019");

        // Ends the program if the 'X' button is clicked
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        // Makes the frame size 600 pixels by 600 pixels
        frame.setSize(600, 600);

        // Makes the board of the game
        board = new Grid[ROW][COL];

        // Makes the game
        frame.setContentPane(gameSetup());

        // Shows the frame
        frame.setVisible(true);

        // Sets wins to 0
        wins = 0;
    }

    /**
     * Creates the board setup, and everything in the frame, and makes the board random
     */
    public JPanel gameSetup ()
    {
        // Creates a new grid and sets the layout to 5 by 5
        grid = new JPanel();
        grid.setLayout(new GridLayout(5, 5));

        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                // Creates an individual grid
                Grid individualGrid = new Grid();

                // Sets the individualGrid to Green to represent an empty grid
                individualGrid.setBackground(Color.GREEN);

                // Sets the border after each individualGrid
                individualGrid.setBorder(BorderFactory.createLineBorder(Color.BLUE, 1));

                // Sets the size of the individualGrid to 5 * 5
                individualGrid.setSize(5, 5);

                // Sets the row of the int i of the individualGrid
                individualGrid.setRow(i);

                // Sets the col of the int j of the individualGrid
                individualGrid.setCol(j);

                // Puts the individualGrid to the main grid
                board[i][j] = individualGrid;

                // Adds a mouseListener to the individualGrid
                individualGrid.addMouseListener(this);

                // Adds the individualGrid to the main grid
                grid.add(individualGrid);
            }
        }

        // Sets moves to 0
        moves = 0;

        // Sets gameOver to false
        gameOver = false;

        // Sets manualGame to false
        manualGame = false;

        // Sets finish to false
        finish = false;

        // Sets filledGrid to 0
        filledGrid = 0;
        random();

        // Creates a flowLayout for the JPanel
        controls = new JPanel();
        controls.setLayout(new FlowLayout());

        // Adds the label moves representing the moves in the controls Panel
        move = new JLabel("Moves: " + moves);
        controls.add(move);

        // Adds a button called NewGame in the controls panel and adds an actionListener on it, because it is something
        // that can be clicked on
        newGame = new JButton("New Game");
        controls.add(newGame);
        newGame.addActionListener(this);

        // Adds a button called Quit in the controls panel and adds an actionListener on it, because it is something
        // that can be clicked on
        quit = new JButton("Quit");
        controls.add(quit);
        quit.addActionListener(this);

        // Adds a button called manualMode in the controls panel and adds an actionListener on it, because it is
        // something that can be clicked on
        manualMode = new JButton("Enter Manual Mode: ");
        controls.add(manualMode);
        manualMode.addActionListener(this);

        // Adds the label moves representing the wins in the controls Panel
        win = new JLabel("Wins: " + wins);
        controls.add(win);

        // Creates a JPanel for the labels and makes it a boxLayout
        labels = new JPanel();
        labels.setLayout(new BoxLayout(labels, BoxLayout.PAGE_AXIS));

        // Adds the label colorDescription representing the colors in the labels Panel
        JLabel colorDescription = new JLabel("Empty Box Color: GREEN \t Filled Box Color: BLACK");
        labels.add(colorDescription);

        // Adds the label instructions representing the instructions in the labels Panel
        JLabel instructions = new JLabel("Aim: Make all the boxes black");
        labels.add(instructions);

        // Creates a new JPanel called finalFrame
        finalFrame = new JPanel();

        // Sets finalFrame to a borderLayout and adds the grid in the center, controls on the top, and labels on the
        // bottom
        BorderLayout bo = new BorderLayout();
        finalFrame.setLayout(bo);
        finalFrame.add(controls, BorderLayout.NORTH);
        finalFrame.add(grid, BorderLayout.CENTER);
        finalFrame.add(labels, BorderLayout.SOUTH);

        // Return the finalFrame
        return finalFrame;
    }

    /**
     * Simulates a randomly started game by iterating a random move 15 times and setting the location to black
     * indicating that it is full
     */
    public void random ()
    {
        // Iterates randomly 15 times and sets the background black
        for (int i = 0; i < 15; i++)
        {
            Random rand = new Random();
            int randomRow = rand.nextInt(ROW);
            int randomCol = rand.nextInt(COL);
            JPanel random = new JPanel();
            random = board[randomRow][randomCol];
            random.setBackground(Color.BLACK);
        }
    }

    /**
     * If all the values in the board are full then it sets gameOver to true
     */
    public void checkBoard ()
    {
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COL; j++)
            {
                // If filledGrid is equal to 25, meaning that all 25 grids are black, then it sets gameOver to true
                if ((filledGrid == NUMGRIDS))
                {
                    gameOver = true;
                }
            }
        }
    }

    /**
     * Returns checkForWinner where if the game is over, then it shows a message dialog saying that the player has won
     * and reports the number of moves it took to win, and gives the user instructions on what to do next
     */
    public void checkForWinner ()
    {
        // Calls checkBoard()
        checkBoard();

        // If gameOver is true, then it shows a message dialog which reports that you have won a game, and how many
        // moves it took
        if (gameOver == true)
        {
            JOptionPane.showMessageDialog(frame, "Game Over! \n" + "You Win! It took you " + moves + " moves to win");

            // Sets finish to true
            finish = true;

            // Increments wins
            wins++;
        }

        // If finish is true, then it gives instructions on what to do next
        if (finish == true)
        {
            JOptionPane.showMessageDialog(frame,
                    "If you want to play another game click on New Game.\n"
                            + " Do Not Click on the tiles before you click New Game \n"
                            + "Otherwise close the window or click on Quit to Exit the game");
        }
    }

    /**
     * What will happen when a JButton is clicked
     */
    @Override
    public void actionPerformed (ActionEvent ae)
    {
        // Exits the game if "Quit" is clicked and displays a dialog box saying that you are leaving the game
        if (ae.getActionCommand().equals("Quit"))
        {
            JOptionPane.showMessageDialog(frame, "You are leaving the game :( Thank you for playing");
            System.exit(0);
        }
        // If "Enter Manual Mode:" is clicked then it switches the sign to Exit Manual Mode and turns manualGame to true
        if (ae.getActionCommand().equals("Enter Manual Mode: "))
        {
            manualGame = true;
            manualMode.setText("Exit Manual Mode: ");
        }
        // If "New Game" is clicked, then it clears the board and calls gameSetup again to make the new grid and makes
        // the frame visible
        if (ae.getActionCommand().equals("New Game"))
        {
            frame.setContentPane(gameSetup());
            frame.setVisible(true);
        }
        // If "Exit Manual Mode:" is clicked then it switches the sign to Enter Manual Mode and turns manualGame to
        // false
        if (ae.getActionCommand().equals("Exit Manual Mode: "))
        {
            manualGame = false;
            manualMode.setText("Enter Manual Mode: ");
        }
    }

    /**
     * The actions that will happen when a mouse is pressed
     */
    @Override
    public void mousePressed (MouseEvent me)
    {
        // Gets the source of the Grid class when a mouse is pressed
        Grid gridVariable = (Grid) me.getSource();

        // If manualGame is false, then it should go into the if statement
        if (manualGame == false)
        {
            // Gets the row from the grid and sets it to the int row
            int row = gridVariable.getRow();

            // Gets the col from the grid and sets it to the int col
            int col = gridVariable.getCol();

            // Changes the light from the Grid class depending on the situation based on the specific row and col
            gridVariable.changeLight(row, col);

            // Increments the moves
            moves++;

            // Shows the moves incrementing each time there is a move made
            move.setText("Moves: " + moves + "");

            // Calls for checkForWinner() and if that is true, then it changes the wins and shows the win being
            // incremented
            checkForWinner();
            win.setText("Wins: " + wins + "");
        }
        // If manualGame is true, then it should go into the else statement
        else
        {
            // Doesn't change the moves made because it is in manual
            moves = moves;

            // If the grid clicked on is green, then it converts it to black
            if (gridVariable.getBackground().equals(Color.GREEN))
            {
                gridVariable.setBackground(Color.BLACK);
            }

            // If the grid clicked on is black, then it converts it to green
            else
            {
                gridVariable.setBackground(Color.GREEN);
            }
        }
    }

    @Override
    public void mouseClicked (MouseEvent me)
    {
        // Not being used
    }

    @Override
    public void mouseEntered (MouseEvent me)
    {
        // Not being used
    }

    @Override
    public void mouseExited (MouseEvent me)
    {
        // Not being used
    }

    @Override
    public void mouseReleased (MouseEvent me)
    {
        // Not being used
    }

    /**
     * A class where the individual grids are made and methods for the actions on the grids are written, which is used
     * to make the individual grid
     */
    @SuppressWarnings("serial")
	private class Grid extends JPanel
    {
        /**
         * A private instance variable to keep track of the row, which is an int
         */
        private int row;
        /**
         * A private instance variable to keep track of the column, which is an int
         */
        private int col;

        /**
         * Sets the row to the rowNumber specified by the user
         */
        public void setRow (int input)
        {
            row = input;
        }

        /**
         * Sets the column to the columnNumber specified by the user
         */
        public void setCol (int input)
        {
            col = input;
        }

        /**
         * Returns the row of the grid
         */
        public int getRow ()
        {
            return row;
        }

        /**
         * Returns the column of the grid
         */
        public int getCol ()
        {
            return col;
        }

        /**
         * Changes the lights of the row and col, and the cols and rows adjacent to it, depending on where the row and
         * col is located in the grid
         */
        private void changeLight (int row, int col)
        {
            // Gets the values of the row and col and sets it on the grid
            Grid grid = board[row][col];
            row = grid.getRow();
            col = grid.getCol();

            // Checks for the top left corner and changes the colors of the sides adjacent to it, that is change one
            // horizontal side and one vertical side, as well as the original box and checks if the boxes are filled
            // through the individualGridFilled method
            if (row == 0 && col == 0)
            {
                changeColor(row + 1, col);
                changeColor(row, col + 1);
                changeColor(row, col);
                individualGridFilled(row + 1, col);
                individualGridFilled(row, col + 1);
                individualGridFilled(row, col);
            }

            // Checks for the top right corner and changes the colors of the sides adjacent to it, that is change one
            // horizontal side and one vertical side, as well as the original box and checks if the boxes are filled
            // through the individualGridFilled method
            else if (row == 0 && col == 4)
            {
                changeColor(row + 1, col);
                changeColor(row, col - 1);
                changeColor(row, col);
                individualGridFilled(row + 1, col);
                individualGridFilled(row, col - 1);
                individualGridFilled(row, col);
            }

            // Checks for the bottom left corner and changes the colors of the sides adjacent to it, that is change one
            // horizontal side and one vertical side, as well as the original box and checks if the boxes are filled
            // through the individualGridFilled method
            else if (row == 4 && col == 0)
            {
                changeColor(row - 1, col);
                changeColor(row, col + 1);
                changeColor(row, col);
                individualGridFilled(row - 1, col);
                individualGridFilled(row, col + 1);
                individualGridFilled(row, col);
            }

            // Checks for the bottom right corner and changes the colors of the sides adjacent to it, that is change one
            // horizontal side and one vertical side, as well as the original box and checks if the boxes are filled
            // through the individualGridFilled method
            else if (row == 4 && col == 4)
            {
                changeColor(row - 1, col);
                changeColor(row, col - 1);
                changeColor(row, col);
                individualGridFilled(row - 1, col);
                individualGridFilled(row, col - 1);
                individualGridFilled(row, col);
            }

            // Checks for the top row and changes the colors of the sides adjacent to it, that is change one
            // horizontal side and two vertical sides, as well as the original box and checks if the boxes are filled
            // through the individualGridFilled method
            else if (row == 0 && col > 0 && col < 4)
            {
                changeColor(row, col);
                changeColor(row, col + 1);
                changeColor(row, col - 1);
                changeColor(row + 1, col);
                individualGridFilled(row, col);
                individualGridFilled(row, col - 1);
                individualGridFilled(row, col + 1);
                individualGridFilled(row + 1, col);
            }

            // Checks for the bottom row and changes the colors of the sides adjacent to it, that is change one
            // horizontal side and two vertical sides, as well as the original box and checks if the boxes are filled
            // through the individualGridFilled method
            else if (row == 4 && col > 0 && col < 4)
            {
                changeColor(row, col);
                changeColor(row, col + 1);
                changeColor(row, col - 1);
                changeColor(row - 1, col);
                individualGridFilled(row, col);
                individualGridFilled(row - 1, col);
                individualGridFilled(row, col + 1);
                individualGridFilled(row, col - 1);
            }

            // Checks for the left column and changes the colors of the sides adjacent to it, that is change two
            // horizontal sides and one vertical side, as well as the original box and checks if the boxes are filled
            // through the individualGridFilled method
            else if (col == 0 && row > 0 && row < 4)
            {
                changeColor(row, col);
                changeColor(row, col + 1);
                changeColor(row - 1, col);
                changeColor(row + 1, col);
                individualGridFilled(row, col);
                individualGridFilled(row - 1, col);
                individualGridFilled(row + 1, col);
                individualGridFilled(row, col + 1);
            }

            // Checks for the right column and changes the colors of the sides adjacent to it, that is change two
            // horizontal sides and one vertical side, as well as the original box and checks if the boxes are filled
            // through the individualGridFilled method
            else if (col == 4 && row > 0 && row < 4)
            {
                changeColor(row, col);
                changeColor(row - 1, col);
                changeColor(row, col - 1);
                changeColor(row + 1, col);
                individualGridFilled(row, col);
                individualGridFilled(row - 1, col);
                individualGridFilled(row + 1, col);
                individualGridFilled(row, col - 1);
            }

            // Checks for the box other than the conditions listed above and changes the colors of the sides adjacent to
            // it, that is change the left, right, top and bottom box, as well as the original box and checks if the
            // boxes are filled through the individualGridFilled method
            else if (col > 0 && col < 4 && row > 0 && row < 4)
            {
                changeColor(row, col);
                changeColor(row - 1, col);
                changeColor(row + 1, col);
                changeColor(row, col - 1);
                changeColor(row, col + 1);
                individualGridFilled(row, col);
                individualGridFilled(row - 1, col);
                individualGridFilled(row + 1, col);
                individualGridFilled(row, col - 1);
                individualGridFilled(row, col + 1);
            }

        }

        /**
         * Gets the coordinates of the grid and if the background is black, then it sets it to green, and if background
         * is green, then it sets it to black
         */
        private void changeColor (int row, int col)
        {
            // Gets the value of the row and the col and sets it on the grid
            Grid grid = board[row][col];
            row = grid.getRow();
            col = grid.getCol();
            // If row and col is greater than equal to 0, and less than the constant variable ROW and COL respectively,
            // then it would go into the if statement
            if (row < ROW && col < COL && row >= 0 && col >= 0)
            {
                // If the background is black, then it sets it to green
                if (board[row][col].getBackground().equals(Color.BLACK))
                {
                    board[row][col].setBackground(Color.GREEN);
                }
                // If the background is green, then it sets it to black
                else if (board[row][col].getBackground().equals(Color.GREEN))
                {
                    board[row][col].setBackground(Color.BLACK);
                }
            }
        }

        /**
         * If the individual grid is occupied in the JPanel, then it increments filledGrid, but if it isn't occupied,
         * then it sets filledGrid back to 0
         */
        public void individualGridFilled (int row, int col)
        {
            // If the coordinates don't have a black background, then it sets filledGrid back to 0
            filledGrid = 0;
            for (row = 0; row < ROW; row++)
            {
                for (col = 0; col < COL; col++)
                {
                    // If the coordinates have a black background, then it increments filledGrid
                    if (board[row][col].getBackground().equals(Color.BLACK))
                    {
                        filledGrid++;
                    }
                }
            }
        }
    }
}
