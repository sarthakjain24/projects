package sortingAlgorithms;

import java.util.Arrays;

/**
 * A class that implements a generic ShellSort
 * 
 * @author Sarthak Jain
 *
 */
public class ShellSort {
	/**
	 * Runs the main method
	 */
	public static void main(String[] args) {
		Integer[] arr = new Integer[] { 24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 324352532 };
		System.out.println("Unsorted Array before ShellSort: " + Arrays.asList(arr));
		shellSort(arr);
		System.out.println("Sorted Array after ShellSort: " + Arrays.asList(arr));
	}

	/**
	 * Sorts a generic array using ShellSort
	 */
	public static <T extends Comparable<? super T>> void shellSort(T[] arr) {
		// Sets length to the array's length
		int length = arr.length;

		// Start with gap as half of the arr's length and continues halving the gap
		for (int gap = length / 2; gap > 0; gap /= 2) {

			// Does an insertion sort starting from the gap to the length
			for (int i = gap; i < length; i++) {

				// Sets val as the value at index i in the array
				T val = arr[i];

				// Initializes j
				int j;

				// Sets j to i, if j is greater than equal to gap, and if the value at index
				// j-gap is greater than the val, then it sets the value at index j in the array
				// as the value from index j-gap from the array
				for (j = i; j >= gap && arr[j - gap].compareTo(val) > 0; j -= gap) {
					arr[j] = arr[j - gap];
				}
				
				// Sets val as the value at j's index
				arr[j] = val;
			}
		}
	}

}
