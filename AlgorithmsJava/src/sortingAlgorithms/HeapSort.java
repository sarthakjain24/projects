package sortingAlgorithms;

import java.util.Arrays;

/**
 * A class that implements a generic HeapSort
 * 
 * @author Sarthak Jain
 *
 */
public class HeapSort {
	/**
	 * Runs the main method
	 */
	public static void main(String[] args) {
		Integer[] arr = new Integer[] { 24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 324352532 };
		System.out.println("Unsorted Array before HeapSort: " + Arrays.asList(arr));
		heapSort(arr);
		System.out.println("Sorted Array after HeapSort: " + Arrays.asList(arr));
	}

	/**
	 * Sorts a generic array using heapsort
	 */
	public static <T extends Comparable<? super T>> void heapSort(T[] arr) {

		// Sets length to array's length
		int length = arr.length;

		// Builds max heap in each iteration
		for (int i = length / 2 - 1; i >= 0; i--) {
			heapify(arr, length, i);
		}
		// Removes the top item from the heap and continues swapping, and recreating a
		// maxHeap
		for (int i = length - 1; i > 0; i--) {

			//Swaps the value at index 0 and index i 
			swap(arr, 0, i);

			//Creates a maxHeap
			heapify(arr, i, 0);
		}
	}

	/**
	 * Builds a generic max heap
	 */
	private static <T extends Comparable<? super T>> void heapify(T arr[], int n, int largeIndex) {
		// Initialize largest as largeIndex
		int largest = largeIndex;

		// Sets leftChild as 2*largeIndex + 1
		int leftChild = 2 * largeIndex + 1;

		// Sets rightChild as 2*largeIndex + 2
		int rightChild = 2 * largeIndex + 2;

		// If leftChild is smaller than n, and the value at index leftChild is bigger
		// than the value at index largest in the array, then set largest to leftChild
		if (leftChild < n && arr[leftChild].compareTo(arr[largest]) > 0)
			largest = leftChild;

		// If rightChild is smaller than n, and the value at index rightChild is bigger
		// than the value at index largest in the array, then set largest to rightChild
		if (rightChild < n && arr[rightChild].compareTo(arr[largest]) > 0)
			largest = rightChild;

		// If largest is not root
		if (largest != largeIndex) {

			// Swaps largeIndex and largest
			swap(arr, largeIndex, largest);

			// Recursively heapify the affected sub-tree
			heapify(arr, n, largest);
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
