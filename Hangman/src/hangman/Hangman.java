package hangman;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Random;
import java.util.Scanner;

/**
 * The Hangman game
 * 
 * @author Sarthak Jain
 *
 */
public class Hangman {
	// The word being guessed
	private String wordBeingGuessed;

	// The current guess of the player
	private StringBuilder currentGuess;

	// An ArrayList of previous guesses
	private ArrayList<Character> prevGuess;

	// The number of maximum incorrect guesses
	private int maxIncorrectGuesses;

	// The number of current incorrect guesses
	private int currentIncorrectGuess;

	// An ArrayList of words that could be randomly chosen
	private ArrayList<String> dictionary;

	
	/**
	 * The constructor of the game
	 */
	public Hangman() {
		// Initializes the current and maximum incorrect guesses
		currentIncorrectGuess = 0;
		maxIncorrectGuesses = 6;

		// Initializes the two ArrayList's
		prevGuess = new ArrayList<>();
		dictionary = new ArrayList<>();

		// Adds the words to the dictionary
		addWordsToDictionary();

		// Gets a random word and sets it as the word to be guessed
		wordBeingGuessed = pickRandomWord();

		// Initializes the current guess
		currentGuess = initializeCurrentGuess();
	}

	/**
	 * A method that gets the words from the file and adds it to the dictionary
	 */
	private void addWordsToDictionary() {
		try {
			// Passes the text file with all the words
			File file = new File("dictionary.txt");
			Scanner scnr;
			scnr = new Scanner(file);
			String line;
			// Adds the words in the file into the ArrayList
			while (scnr.hasNextLine()) {
				line = scnr.nextLine();
				dictionary.add(line);
			}
			// Closes the scanner to save resources
			scnr.close();
		}

		// Catches an Exception if the File is not found, and then throws an Illegal
		// Argument Exception saying that the file passed does not exist
		catch (FileNotFoundException e) {
			throw new IllegalArgumentException("Error. The file passed does not exist");
		}
	}

	/**
	 * A function that picks a random word from the dictionary
	 */
	private String pickRandomWord() {
		Random r = new Random();

		// Gets a random index from the dictionary based on the number of items in it
		int wordIndex = r.nextInt(dictionary.size());

		// Returns a random word from the dictionary based on that index
		return dictionary.get(wordIndex);
	}

	/**
	 * Initialize the current guess for the Hangman game
	 */
	private StringBuilder initializeCurrentGuess() {
		// Initializes a StringBuilder
		StringBuilder current = new StringBuilder();

		// A loop that runs for twice the length of the word to create the current word
		// guess with an underscore and space right after to make it look more
		// aesthetically pleasing
		for (int i = 0; i < wordBeingGuessed.length() * 2; i++) {
			if (i % 2 == 0) {
				current.append("_");
			} else {
				current.append(" ");
			}
		}
		return current;
	}

	/**
	 * Method that gets the current guess
	 */
	public String getFormalCurrentGuess() {
		// Converts the StringBuilder to a String
		String currentGuess = this.currentGuess.toString();
		return "Current Guess: " + currentGuess;
	}

	/**
	 * Method to see if the game is over or not
	 */

	public boolean gameOver() {
		String guess = getCondensedCurrentGuess();
		// Indicates that the game is won if the guess is equal to the word being
		// guessed
		if (guess.equals(wordBeingGuessed)) {
			System.out.println();
			System.out.println("Congrats! You won! You guessed the right word! The word was " + wordBeingGuessed + ".");
			return true;

		}
		// Indicates that the game is over and lost if the current number of incorrect
		// guesses is greater than or equal than the maximum incorrect guesses
		else if (currentIncorrectGuess >= maxIncorrectGuesses) {
			System.out.println();
			System.out.println(
					"Sorry, you lost. You spent all of your 6 tries. " + "The word was " + wordBeingGuessed + ".");
			return true;
		}
		return false;
	}

	/**
	 * A method that gets the current guess by eliminating the space between the
	 * underscore words to make the current guess into a word
	 */
	public String getCondensedCurrentGuess() {
		// Converts the currentGuess StringBuilder to a String
		String guess = this.currentGuess.toString();

		// Gets rid of the space from the String
		String returnGuess = guess.replace(" ", "");
		return returnGuess;
	}

	/**
	 * A method to check if a character has already been guessed
	 */
	public boolean isGuessedAlready(char guess) {
		// If the prevGuess ArrayList contains the character, then it returns true
		if (prevGuess.contains(guess)) {
			return true;
		} else {
			return false;
		}
	}

	/**
	 * A method that plays the guess
	 */
	public boolean enterGuessInWord(char guess) {
		// A boolean that checks if the char is in the word or not
		boolean charInWord = false;

		// A loop that checks if the guess is in the word
		for (int i = 0; i < wordBeingGuessed.length(); i++) {
			// If the guess is in the word, then it goes into this condition
			if (wordBeingGuessed.charAt(i) == guess) {
				// Replaces the underscore with the guess and to account for the space, it
				// multiplies i by 2
				currentGuess.setCharAt(i * 2, guess);
				// Sets charInWord to true
				charInWord = true;
				// Adds the guess is to the prevGuess ArrayList
				prevGuess.add(guess);
			}
		}

		// If the charInWord is false, then it increments the number of incorrect
		// guesses by the user
		if (charInWord == false) {
			currentIncorrectGuess++;
		}

		// Returns whether the char was in the word or not
		return charInWord;
	}

	/**
	 * Draws the hangman based on the current incorrect guess
	 */
	public String drawPicture() {
		// Draws the noose frame
		if (currentIncorrectGuess == 0) {
			return " - - - - -\n" + 
			       "|        |\n" + 
				   "|        \n" + 
			       "|       \n" + 
				   "|        \n" + 
			       "|       \n" + 
				   "|\n" + 
			       "|\n";
		}
		// Draws the head
		else if (currentIncorrectGuess == 1) {
			return " - - - - -\n" + 
		           "|        |\n" + 
				   "|        O\n" + 
		           "|       \n" + 
				   "|        \n" + 
		           "|       \n" + 
				   "|\n" + 
		           "|\n";
		}
		// Draws the body
		else if (currentIncorrectGuess == 2) {
			return " - - - - -\n" + 
		           "|        |\n" + 
				   "|        O\n" + 
		           "|        | \n" + 
				   "|        |\n" + 
		           "|        \n" + 
				   "|\n" + 
		           "|\n";
		}
		// Draws the right arm
		else if (currentIncorrectGuess == 3) {
			return " - - - - -\n" + 
				   "|        |\n" + 
				   "|        O\n" + 
				   "|      / |  \n" + 
				   "|        |\n" + 
				   "|        \n" + 
				   "|\n" + 
				   "|\n";
		}
		// Draws the left arm
		else if (currentIncorrectGuess == 4) {
			return " - - - - -\n" + 
				   "|        |\n" + 
			   	   "|        O\n" + 
				   "|      / | \\ \n" + 
			   	   "|        |\n" + 
				   "|        \n" + 
			   	   "|\n" + 
				   "|\n";
		}
		// Draws the right leg
		else if (currentIncorrectGuess == 5) {
			return " - - - - -\n" + 
				   "|        |\n" + 
				   "|        O\n" + 
				   "|      / | \\ \n" + 
				   "|        |\n" + 
				   "|       / \n" + 
				   "|\n" + 
				   "|\n";
		}
		// Draws the full body by adding the left leg
		else if (currentIncorrectGuess == 6) {
			return " - - - - -\n" + 
				   "|        |\n" + 
				   "|        O\n" + 
				   "|      / | \\ \n" + 
				   "|        |\n" + 
				   "|       / \\ \n" + 
				   "|\n" + 
				   "|\n";
		} else {
			return "";
		}
	}
}
