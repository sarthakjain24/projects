package sortingAlgorithms;

import java.util.Arrays;
import java.util.Comparator;

/**
 * Performs an Insertion Sort on a Generic Array
 * 
 * @author Sarthak Jain
 */
public class InsertionSort {
	/**
	 * Runs the main method
	 */
	public static void main(String[] args) {
		Integer[] arr = new Integer[] { 24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 324352532 };
		System.out.println("Unsorted Array before Insertion Sort: " + Arrays.asList(arr));
		insertionSort(arr);
		System.out.println("Sorted Array after Insertion Sort: " + Arrays.asList(arr));
	}

	/**
	 * A generic method that performs an insertion sort on a generic array
	 */
	public static <T extends Comparable<? super T>> void insertionSort(T[] arr) {
		// Creates a for loop that iterates through the array length
		for (int i = 1; i < arr.length; i++) {

			// Creates a temporary value at index at array
			T val = arr[i];

			// Creates another for loop that compares the values of the array at index j and
			// at the temporary value, and compares if the array at index j is greater than
			// the value, then it enters this loop
			for (int j = i - 1; j >= 0 && arr[j].compareTo(val) > 0; j--) {
				// Switches the values in the array
				arr[j + 1] = arr[j];
				arr[j] = val;
			}
		}
	}

}
