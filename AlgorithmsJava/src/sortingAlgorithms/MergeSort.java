package sortingAlgorithms;

import java.util.Arrays;
/**
 * A class that implements a generic MergeSort
 * 
 * @author Sarthak Jain
 *
 */
public class MergeSort {
	/**
	 * A private static variable to see when the mergeSort method should switch to
	 * insertion sort
	 */
	private static int switchNum = 3;

	/**
	 * Runs the main method
	 */
	public static void main(String[] args) {
		Integer[] arr = new Integer[] { 24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 324352532 };
		System.out.println("Unsorted Array before MergeSort: " + Arrays.asList(arr));
		mergeSort(arr);
		System.out.println("Sorted Array after MergeSort: " + Arrays.asList(arr));
	}

	/**
	 * The driver method that does the mergesort on an Array from index 0 to the
	 * array length - 1
	 */
	@SuppressWarnings("unchecked")
	public static <T extends Comparable<? super T>> void mergeSort(T[] arr) {

		// Creates a temporary Array with the same size as the userInput Array
		// and sets it all to null
		T[] temp = (T[]) new Comparable[arr.length];
		for (int i = 0; i < arr.length; i++) {
			temp[i] = null;
		}

		// Calls the private merge sort method
		mergeSort(arr, temp, 0, arr.length - 1);
	}

	/**
	 * A private recursive method that splits the Array in half recursively and does
	 * the insertionSort once it meets a certain threshold
	 */
	private static <T extends Comparable<? super T>> void mergeSort(T[] arr, T[] temp, int low, int high) {

		// Once it meets the threshold to switch the condition, then it does the
		// insertion sort
		if (high - low <= switchNum) {
			insertionSort(arr, low, high);
		} else if (low < high) {

			// Finds the middle number
			int mid = (low + high) / 2;

			// Recursively calls this method
			mergeSort(arr, temp, low, mid);
			mergeSort(arr, temp, mid + 1, high);

			// Does the merging
			merge(arr, temp, low, mid + 1, high);
		}

		// Base case
		else {

		}
	}

	/**
	 * A private helper method that does the merging of the two Arrays
	 */
	private static <T extends Comparable<? super T>> void merge(T[] arr, T[] temp, int low, int mid, int high) {

		// Sets the leftIndex of the left half
		int leftArrIndex = low;

		// Sets the rightIndex of the right half
		int rightArrIndex = mid;

		// Sets the index to low
		int index = low;

		// Makes two conditions where it adds the values from the arr to the temporary
		// Array by going from the leftArrIndex to mid -1 and the rightArrIndex to
		// high
		while (leftArrIndex < mid && rightArrIndex <= high) {

			// If the value in the leftArrIndex is less than or equal to the value at the
			// rightArrIndex then it adds it to the temp Array and increments the
			// leftIndex
			if (arr[leftArrIndex].compareTo(arr[rightArrIndex]) <= 0) {
				temp[index] = arr[leftArrIndex];
				leftArrIndex++;

			} else {

				// If the value in the leftArrIndex is greater than to the value at the
				// rightArrIndex then it adds it to the temp Array and increments the
				// rightIndex
				temp[index] = arr[rightArrIndex];
				rightArrIndex++;
			}

			// Increments index
			index++;
		}

		// Another check to make sure that all values are copied over and not missed in
		// the left hand side of the arr and adds it to the temporary Array
		while (leftArrIndex < mid) {
			temp[index] = arr[leftArrIndex];
			leftArrIndex++;
			index++;
		}

		// Another check to make sure that all values are copied over and not missed in
		// the right hand side of the arr and adds it to the temporary Array
		while (rightArrIndex <= high) {
			temp[index] = arr[rightArrIndex];
			rightArrIndex++;
			index++;
		}

		// Copies the values from the temporary array to the original arr
		for (int i = low; i <= high; i++) {
			arr[i] = temp[i];
		}

	}

	/**
	 * A private helper method that does the insertion sort from low to high to sort
	 * the values in order, and is used once high-low is less than equal to the
	 * switchNum threshold in the private merge sort method
	 */
	private static <T extends Comparable<? super T>> void insertionSort(T[] arr, int low, int high) {

		// Creates a for loop that iterates through the array length
		for (int i = low + 1; i <= high; i++) {

			// Creates a temporary value at index at Array
			T val = arr[i];
			int j;

			// Creates another for loop that compares the values of the array at index j and
			// at the temporary value, and compares if the array at index j is greater than
			// the value, then it enters this loop
			for (j = i - 1; j >= low && (arr[j]).compareTo(val) > 0; j--) {

				// Switches the values in the Array
				arr[j + 1] = arr[j];
			}

			arr[j + 1] = val;
		}
	}

}
