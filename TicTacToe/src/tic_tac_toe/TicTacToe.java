package tic_tac_toe;

import java.awt.BorderLayout;
import java.awt.Container;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.SwingUtilities;

/**
 * A TicTacToe implementation using GUI
 * 
 * @author Sarthak Jain
 */
public class TicTacToe implements ActionListener {
	/**
	 * Runs the program
	 */
	public static void main(String[] args) {
		SwingUtilities.invokeLater(() -> new TicTacToe());
	}

	/**
	 * The frame of the application
	 */
	private JFrame frame;
	/**
	 * The size of the TicTacToe board
	 */
	private JButton[][] board;
	/**
	 * The moves played so far
	 */
	private int moves;
	/**
	 * An indicator to check if the game is over or not
	 */
	private boolean gameOver;
	/**
	 * Counts the number of wins for X
	 */
	private int xWins;
	/**
	 * Counts the number of wins for O
	 */
	private int oWins;
	/**
	 * Counts the number of ties
	 */
	private int ties;
	/**
	 * The label for the number of wins by X
	 */
	private JLabel xCount;
	/**
	 * The label for the number of wins by O
	 */
	private JLabel oCount;
	/**
	 * The label for the number of ties
	 */
	private JLabel tiesCount;
	/**
	 * The panel for the grid
	 */
	private JPanel grid;
	/**
	 * The main panel
	 */
	private JPanel main;

	/**
	 * The constructor of the TicTacToe game
	 */
	public TicTacToe() {
		// Initializes the frame
		frame = new JFrame();

		// Sets the title of the frame
		frame.setTitle("TicTacToe");

		// Closes the application when closed
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		// Sets the size of the frame
		frame.setSize(500, 500);

		// Sets the content pane to create the frame
		frame.setContentPane(createFrame());

		// Makes the frame visible
		frame.setVisible(true);

		// Initializes the xWins, oWins and ties to 0
		xWins = 0;
		oWins = 0;
		ties = 0;
	}

	/**
	 * Creates the frame of the TicTacToe board
	 */
	private JPanel createFrame() {
		// Initializes the main panel and sets a BorderLayout on it
		main = new JPanel();
		main.setLayout(new BorderLayout());

		// Initializes the grid and the TicTacToe board, and sets a GridLayout on the
		// grid
		grid = new JPanel();
		board = new JButton[3][3];
		grid.setLayout(new GridLayout(3, 3));

		// Adds the buttons to the TicTacToe board
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				JButton button = new JButton();
				button.setFont(new Font(Font.SANS_SERIF, Font.BOLD, 60));
				button.addActionListener(this);
				grid.add(button);
				board[i][j] = button;
			}
		}

		// Sets the grid to the center
		main.add(grid, "Center");

		// Initializes the controls and sets a FlowLayout to it, and adds buttons to it
		JPanel controls = new JPanel();
		controls.setLayout(new FlowLayout());
		// Adds a button called "New Game" to the controls panel
		JButton newGame = new JButton("New Game");
		controls.add(newGame);
		newGame.addActionListener(this);

		// Adds a button called "Quit Game" to the controls panel
		JButton quit = new JButton("Quit Game");
		controls.add(quit);
		quit.addActionListener(this);

		// Adds the controls to the bottom
		main.add(controls, "South");

		// Initializes a label panel and sets a BoxLayout to it
		JPanel label = new JPanel();
		label.setLayout(new BoxLayout(label, BoxLayout.PAGE_AXIS));
		// Initializes the xCount for indicating the wins for X
		xCount = new JLabel("Wins by X: " + xWins);
		label.add(xCount);
		// Initializes the oCount for indicating the wins for O
		oCount = new JLabel("Wins by O: " + oWins);
		label.add(oCount);
		// Initializes the tiesCount for indicating the ties
		tiesCount = new JLabel("Ties: " + ties);
		label.add(tiesCount);
		// Adds the labels to the top of the main panel
		main.add(label, "North");

		// Returns the main panel
		return main;
	}

	/**
	 * A helper method that clears and resets the TicTacToe board
	 */
	private void clearBoard() {
		// Resets the gameOver boolean, indicating game is not over
		gameOver = false;

		// Resets moves to 0
		moves = 0;

		// Clears the text on the board
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				board[i][j].setText("");
			}
		}
	}

	/**
	 * A private helper method that checks for ties
	 */
	private boolean checkTie() {
		// Sets the boolean tie to true
		boolean tie = true;

		// If an empty space is found in the board, then it returns that there is no tie
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (board[i][j].getText().equals("")) {
					tie = false;
					return tie;
				}
			}
		}

		// If there is a tie, then it displays a message saying it's a tie, making the
		// gameOver, incrementing the ties
		if (tie == true) {
			JOptionPane.showMessageDialog(null, "It's a tie!");
			gameOver = true;
			ties++;
			tiesCount.setText("Ties: " + ties);
		}
		return tie;
	}

	/**
	 * A private helper method that checks for the winner on the board
	 */
	private boolean checkBoard(int row, int col, int deltaRow, int deltaCol) {

		String s1 = board[row][col].getText();
		String s2 = board[row + deltaRow][col + deltaCol].getText();
		String s3 = board[row + 2 * deltaRow][col + 2 * deltaCol].getText();

		// If the text for all three strings are equal to each other, then it returns
		// true
		return (s1.length() > 0 && s1.equals(s2) && s2.equals(s3));
	}

	/**
	 * A private helper method that checks if game is won whether it is diagonally,
	 * horizontally or vertically
	 */
	private boolean gameWon() {
		return checkBoard(0, 0, 0, 1) || checkBoard(1, 0, 0, 1) || checkBoard(2, 0, 0, 1) || checkBoard(0, 0, 1, 0)
				|| checkBoard(0, 1, 1, 0) || checkBoard(0, 2, 1, 0) || checkBoard(0, 0, 1, 1)
				|| checkBoard(0, 2, 1, -1);
	
	}

	/**
	 * Performs the action for the button's clicked on
	 */
	@Override
	public void actionPerformed(ActionEvent e) {
		// If the New Game button is clicked, then it clears the board
		if (e.getActionCommand().equals("New Game")) {
			// Enables all the buttons again
			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 3; j++) {
					board[i][j].setEnabled(true);
				}
			}
			// Clears the board
			clearBoard();
		}
		// If the Quit Game button is clicked, it exits the program
		else if (e.getActionCommand().equals("Quit Game")) {
			System.exit(0);
		}
		// If the game is not over, then it goes into this loop
		else if (gameOver == false) {
			// Initializes the button clicked on
			JButton button = (JButton) e.getSource();

			// If the moves are even, then it goes into the condition
			if (moves % 2 == 0) {

				// Sets the text to X
				button.setText("X");
				// Disables this button, so it can't be changed
				button.setEnabled(false);
				// If the game is won, then it shows a message saying X wins, and increments the
				// wins for X, and sets the gameOver to true
				if (gameWon() == true) {
					JOptionPane.showMessageDialog(null, "X wins!");
					xWins++;
					xCount.setText(("Wins by X: " + xWins));
					gameOver = true;
				}
				if (e.getSource().equals("New Game")) {
					button.setEnabled(true);
				}
				// If the game is not over, then it checks for a tie
				if (gameOver == false) {
					checkTie();
				}
				
				// Increments the moves by 1
				moves++;
			}

			// If the moves are odd, then it goes into this condition
			else {

				// Sets the text to O
				button.setText("O");
				// Disables this button so that it cant be changed
				button.setEnabled(false);
				// If the game is won, then it shows a message saying O wins, and increments the
				// wins for O, and sets the gameOver to true
				if (gameWon() == true) {
					JOptionPane.showMessageDialog(null, "O wins!");
					oWins++;
					oCount.setText(("Wins by O: " + oWins));
					gameOver = true;
				}

				// If the game is not over, then it checks for a tie
				if (gameOver == false) {
					checkTie();
				}

				// Increments the moves by 1
				moves++;
			}
		}
	}
}
