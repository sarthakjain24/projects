package hangman;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;

/**
 * Runs the Hangman Application
 * 
 * @author Sarthak Jain
 */
public class HangmanApplication {

	public static void main(String[] args) {
		// Initializes the scanner
		Scanner scnr = new Scanner(System.in);

		// Initializes an ArrayList for the character guesses
		ArrayList<Character> charGuesses = new ArrayList<>();

		// Welcome greeting
		System.out.println("Welcome to Hangman! You get 6 tries to guess a random word");
		System.out.println();

		// A boolean that indicates that the game is being played
		boolean gamePlayed = true;

		// A loop that runs while the game is being played
		while (gamePlayed == true) {
			System.out.println("Let's play");

			// Initializes a new game of Hangman
			Hangman game = new Hangman();

			// A loop that runs while the game is not over
			while (!game.gameOver()) {

				// Draws the picture, and the the current guess
				System.out.println();
				System.out.println(game.drawPicture());
				System.out.println(game.getFormalCurrentGuess());

				// Gets a character from the user
				System.out.println("Enter a character that you think is in the word: ");
				char guess = (scnr.next().toLowerCase()).charAt(0);

				// Adds the guess to the ArrayList if it doesn't contain the guess
				if (!charGuesses.contains(guess)) {
					charGuesses.add(guess);
				}
				System.out.println();

				// A loop that checks if the character is guessed already in the game, and shows
				// previously guesses characters
				while (game.isGuessedAlready(guess)) {
					System.out.println("Try again! You've already guessed " + guess);
					guess = (scnr.next().toLowerCase()).charAt(0);
					System.out.println("Previous guesses so far: " + Arrays.toString(charGuesses.toArray()));

				}

				// Plays the guess, and checks if the letter was in the word and prints whether
				// the character exists or not in the word, and shows previously guesses
				// characters
				if (game.enterGuessInWord(guess)) {
					System.out.println(guess + " is already in the word!");
					System.out.println("Previous guesses so far: " + Arrays.toString(charGuesses.toArray()));
				} else {
					System.out.println(guess + " is not in the word");
					System.out.println("Previous guesses so far: " + Arrays.toString(charGuesses.toArray()));
				}
			}

			// Allows the user to play a new game or not, and if the response is 'Y', then
			// it sets gamePlayed to true, else exits the application
			System.out.println();
			System.out.println("To play another game enter Y, or enter N to quit");
			char response = scnr.next().toUpperCase().charAt(0);
			if (response == 'Y') {
				gamePlayed = true;
			} else if (response == 'N') {
				System.exit(0);
			}
		}
	}
}
