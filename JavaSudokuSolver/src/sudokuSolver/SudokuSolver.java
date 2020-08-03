package sudokuSolver;

/**
 * A class that solves Sudoku
 * 
 * @author Sarthak Jain
 */
public class SudokuSolver {

	public static int[][] GRID_TO_SOLVE = { { 7, 0, 0, 0, 2, 5, 0, 0, 6 }, { 0, 9, 0, 0, 0, 0, 0, 8, 2 },
			{ 0, 0, 1, 7, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 7, 0 }, { 0, 3, 0, 1, 0, 8, 0, 9, 0 },
			{ 0, 6, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 1, 8, 0, 0 }, { 3, 1, 0, 0, 0, 0, 0, 4, 0 },
			{ 8, 0, 0, 4, 5, 0, 0, 0, 7 }, };
	/**
	 * The Sudoku Board
	 */
	private int[][] sudokuBoard;
	/**
	 * An empty value on the sudoku board represented as 0
	 */
	public static int EMPTY = 0;
	/**
	 * The size of the sudoku board, in terms of length, and width of a row, col,
	 * maximum possible numbers in a box
	 */
	public static int SIZE = 9;

	public static void main(String[] args) {
		// Initializes the grid to solve
		SudokuSolver sudoku = new SudokuSolver(GRID_TO_SOLVE);
		System.out.println("Sudoku grid to solve");
		System.out.println("Unsolved grid");

		// Displays the unsolved grid
		sudoku.displayGrid();

		// Solves the grid and displays it
		if (sudoku.solve()) {
			System.out.println("Grid solved");
			sudoku.displayGrid();
		} else {
			System.out.println("Unsolvable");
		}

	}

	public SudokuSolver(int[][] board) {
		this.sudokuBoard = new int[SIZE][SIZE];
		for (int i = 0; i < SIZE; i++) {
			for (int j = 0; j < SIZE; j++) {
				this.sudokuBoard[i][j] = board[i][j];
			}
		}
	}

	/**
	 * A method that sees if the number previously exists is in the row
	 */
	private boolean isInRow(int row, int number) {
		// If the number exists in the row specified, then it returns true, else false
		for (int i = 0; i < SIZE; i++) {
			if (sudokuBoard[row][i] == number) {
				return true;
			}
		}
		return false;
	}

	/**
	 * A method that sees if the number previously exists is in the column
	 */
	private boolean isInCol(int col, int number) {
		// If the number exists in the column specified, then it returns true, else
		// false
		for (int i = 0; i < SIZE; i++) {
			if (sudokuBoard[i][col] == number) {
				return true;
			}
		}
		return false;
	}

	/**
	 * A method that checks if the number is in the sudoku mini box
	 */
	private boolean isInBox(int row, int col, int number) {
		int r = row - row % 3;
		int c = col - col % 3;
		for (int i = r; i < r + 3; i++) {
			for (int j = c; j < c + 3; j++) {
				if (sudokuBoard[i][j] == number) {
					return true;
				}
			}
		}
		return false;
	}

	/**
	 * Checks if the number is valid in the given row, col
	 */
	private boolean isValid(int row, int col, int number) {
		// Returns false if it does not meet any of these conditions
		return !isInRow(row, number) && !isInCol(col, number) && !isInBox(row, col, number);
	}

	/**
	 * Solves the grid
	 */
	public boolean solve() {
		for (int row = 0; row < SIZE; row++) {
			for (int col = 0; col < SIZE; col++) {
				// Goes into this if the sudoku board is empty
				if (sudokuBoard[row][col] == EMPTY) {
					// Checks if valid, and then adds the number on the board
					for (int number = 1; number <= SIZE; number++) {
						if (isValid(row, col, number)) {
							sudokuBoard[row][col] = number;
							// Recursively continues to solve, and returns true if completely solved, else
							// makes it the row and col empty
							if (solve()) {
								return true;
							} else {
								sudokuBoard[row][col] = EMPTY;
							}
						}
					}
					// Returns false if unable to see if valid
					return false;
				}
			}
		}
		// Returns true after solving the grid
		return true;
	}

	/**
	 * A method that displays the grid
	 */
	public void displayGrid() {
		for (int i = 0; i < SIZE; i++) {
			for (int j = 0; j < SIZE; j++) {
				System.out.print(" " + sudokuBoard[i][j]);
			}
			System.out.println();
		}
		System.out.println();
	}

}
