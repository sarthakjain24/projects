package sortingAlgorithms;

import java.util.Arrays;
import java.util.Random;

/**
 * A class that implements a generic QuickSort
 * 
 * @author Sarthak Jain
 *
 */
public class QuickSort {
	/**
	 * Runs the main method
	 */
	public static void main(String[] args) {
		Integer[] arr = new Integer[] { 24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 324352532 };
		System.out.println("Unsorted Array before QuickSort: " + Arrays.asList(arr));
		quickSort(arr);
		System.out.println("Sorted Array after QuickSort: " + Arrays.asList(arr));
	}

	/**
	 * The driver method that does the quicksort on an Array from index 0 to the
	 * arrList length - 1
	 */
	public static <T extends Comparable<? super T>> void quickSort(T[] arr) {

		// Calls the private quickSort method
		quickSort(arr, 0, arr.length - 1);
	}

	/**
	 * A private method that does the recursive calling of the quickSort method
	 * after finding a pivotNumber to sort the array, and then recursively continues
	 */
	private static <T extends Comparable<? super T>> void quickSort(T[] arr, int low, int high) {
		if (low < high) {

			// Initializes the pivotNumber through the partition helper method
			int pivotNum = partition(arr, low, high);

			// Does the recursion
			quickSort(arr, low, pivotNum - 1);
			quickSort(arr, pivotNum + 1, high);
		}

		// Base case
		else {

		}

	}

	/**
	 * A private method that helps find the partition number for QuickSort
	 */
	private static <T extends Comparable<? super T>> int partition(T[] arr, int low, int high) {

		// Finds a pivotNumber
		int pivotNum = pivotFinder(2, low, high);

		// Gets the value at the pivot in the array
		T pivot = arr[pivotNum];

		// Swaps it with the lowest value in the array making the pivot the lowest value
		swap(arr, low, pivotNum);

		// Sets leftCursor to low
		int leftCursor = low;

		// Sets rightCursor to high
		int rightCursor = high;

		// A while loop to have the movement of the cursors
		while (leftCursor <= rightCursor) {

			// If the value at the leftCursor index in the array is less than equal to the
			// pivot, then it increments the leftCursor
			while (leftCursor <= rightCursor && (arr[leftCursor]).compareTo(pivot) <= 0) {
				leftCursor++;
			}

			// If the value at the rightCursor index in the array is greater than the pivot,
			// then it decrements the rightCursor
			while (leftCursor <= rightCursor && (arr[rightCursor]).compareTo(pivot) > 0) {
				rightCursor--;
			}

			// If the leftCursor is less than equal to the righCursor then it swaps the
			// values at the left and the rightCursor and increments the leftCursor and
			// decrements the rightCursor
			if (leftCursor <= rightCursor) {
				swap(arr, leftCursor, rightCursor);
				leftCursor++;
				rightCursor--;
			}

		}
		// Swaps with the lowest number and the rightCursor
		swap(arr, low, rightCursor);

		// Returns the rightCursor as the partition
		return rightCursor;
	}

	/**
	 * Finds the pivot through whatever the user enters based on the low and high of
	 * the partition
	 */
	private static int pivotFinder(int input, int low, int high) {

		// Finds the middle number in the index
		int mid = (high + low) / 2;

		// Finds the length of one half of the sub array
		int half = (high - mid);

		Random rand = new Random();

		// If the input is one, then finds a random index in the lower half
		if (input == 1) {
			return rand.nextInt((half) + 1) + low;

			// If the input is two, then finds a random index in the upper half
		} else if (input == 2) {
			return high - rand.nextInt((half) + 1);

			// Otherwise uses the middle number as the pivot
		} else {
			return mid;
		}
	}

	/**
	 * A private helper method that does the swapping of the two numbers in the
	 * array
	 */
	private static <T> void swap(T[] arr, int firstNum, int secondNum) {
		// Gets a temp val based on the value at the firstNum in the array
		T temp = arr[firstNum];

		// Sets value at firstNum index to the value from the secondNum index in the arr
		arr[firstNum] = arr[secondNum];

		// Sets the value at the secondNum index to temp
		arr[secondNum] = temp;
	}
}