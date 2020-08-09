package sortingAlgorithms;

import java.util.Arrays;

/**
 * A class that implements a generic BubbleSort
 * 
 * @author Sarthak Jain
 *
 */
public class BubbleSort {

	/**
	 * Runs the main method
	 */
	public static void main(String[] args) {
		Integer[] arr = new Integer[] { 24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 324352532 };
		System.out.println("Unsorted Array before BubbleSort: " + Arrays.asList(arr));
		bubbleSort(arr);
		System.out.println("Sorted Array after BubbleSort: " + Arrays.asList(arr));

	}

	/**
	 * Sorts a generic array using bubble sort
	 */
	public static <T extends Comparable<? super T>> void bubbleSort(T arr[]) {
		// Runs till 1 less than the end of the array's length
		for (int i = 0; i < arr.length - 1; i++) {
			// Runs till 1 less than the end of the array's length subtracted by i
			for (int j = 0; j < arr.length - i - 1; j++) {
				// If the value at index j is greater than value at index j + 1, then it swaps
				// the values at the index
				if (arr[j].compareTo(arr[j + 1]) > 0) {
					swap(arr, j, j + 1);
				}
			}
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
