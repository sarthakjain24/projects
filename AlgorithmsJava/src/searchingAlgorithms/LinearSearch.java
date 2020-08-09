package searchingAlgorithms;

/**
 * Uses Linear Search to find items in arrays
 * 
 * @author Sarthak Jain
 */
public class LinearSearch {

	/**
	 * Runs the main method
	 */
	public static void main(String args[]) {
		System.out.println("Returns smallest items and largest items in an array");
		Integer[] arr = new Integer[] { 24, 344, 11, 22, 34, -20, 88, 34, 331, 1, 23, 332, 0, 3134325, 325432532 };
		System.out.println(("Smallest item in Array: " + returnSmallest(arr)));
		System.out.println(("Largest item in Array: " + returnLargest(arr)));
	}

	/**
	 * Returns the smallest item in the arr
	 */
	public static <T extends Comparable<? super T>> T returnSmallest(T[] arr) {
		// Sets smallest to the value at index 0
		T smallest = arr[0];

		// Runs till the end of the arr
		for (int i = 0; i < arr.length; i++) {
			// If the value at index i is smaller, then sets smallest to that value
			if (arr[i].compareTo(smallest) < 0)
				smallest = arr[i];

		}

		// Returns smallest value
		return smallest;
	}

	/**
	 * Returns the largest item in the arr
	 **/
	public static <T extends Comparable<? super T>> T returnLargest(T[] arr) {
		// Sets largest to the value at index 0
		T largest = arr[0];

		// Runs till the end of the arr
		for (int i = 0; i < arr.length; i++) {

			// If the value at index i is larger, then sets largest to that value
			if (arr[i].compareTo(largest) > 0) {
				largest = arr[i];
			}
		}

		// Returns largest value
		return largest;
	}
}
