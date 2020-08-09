package sortingAlgorithms;

import java.util.Arrays;

/**
 * Performs a Selection Sort on a Generic Array
 * 
 * @author Sarthak Jain
 */
public class SelectionSort {
	/**
	 * Runs the main method
	 */
	public static void main(String[] args) {
		Integer[] arr = new Integer[] { 24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 324352532 };
		System.out.println("Unsorted Array before Selection Sort: " + Arrays.asList(arr));
		selectionSort(arr);
		System.out.println("Sorted Array after Selection Sort: " + Arrays.asList(arr));
	}

	/**
	 * Performs a Selection sort on a generic array
	 */
	public static <T extends Comparable<? super T>> void selectionSort(T[] arr) {

		// Loop that runs to one less than the length of the array
		for (int i = 0; i < arr.length - 1; i++) {

			// Sets the minimum index to i
			int min_idx = i;

			// Another loop that starts from i + 1 to the length of the array
			for (int j = i + 1; j < arr.length; j++) {

				// If the value at index j is smaller than the value at the minimum index, then
				// it sets the minimum index to j
				if (arr[j].compareTo(arr[min_idx]) < 0) {
					min_idx = j;
				}
			}

			// Swaps the found minimum index with the index i
			swap(arr, min_idx, i);
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
