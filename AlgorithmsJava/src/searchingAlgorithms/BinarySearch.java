package searchingAlgorithms;

/**
 * Does a Binary Search on a sorted generic array finding a generic item, with
 * both a recursive and iterative implementation
 * 
 * @author Sarthak Jain
 */
public class BinarySearch {
	/**
	 * Runs the main method where it finds items in an array
	 **/
	public static void main(String[] args) {
		System.out.println("Searches for items sequentially");
		Integer[] arr = new Integer[] { -1, 0, 1, 11, 22, 23, 24, 34, 69, 88, 331, 732, 4325, 3476984, 324352532 };

		System.out.println(
				"Index of Array containing 22 using Recursion: " + binarySearchRecursive(arr, 22, 0, arr.length));
		System.out.println(
				"Index of Array containing 4 using Recursion: " + binarySearchRecursive(arr, 4, 0, arr.length));

		System.out.println("Index of Array containing 34 using Iteration: " + binarySearchIterative(arr, 34));
		System.out.println("Index of Array containing -5 using Iteration: " + binarySearchIterative(arr, -5));
	}

	/**
	 * Finds the goal in the arr using an Iterative form of Binary Search, returning
	 * the index
	 **/
	public static <T extends Comparable<? super T>> int binarySearchIterative(T[] arr, T goal) {

		int low = 0;

		// Sets high to the length of the array
		int high = arr.length - 1;

		// Runs while low is less than equal to high
		while (low <= high) {

			// Sets mid to the half of high and low
			int mid = (high + low) / 2;

			// If goal is equal to the val at the mid index in the array, then it returns
			// mid
			if (goal == arr[mid]) {
				return mid;

				// If goal is greater than the val at the mid index in the array, then it sets
				// low to mid + 1
			} else if (goal.compareTo(arr[mid]) > 0) {
				low = mid + 1;

				// If goal is smaller than the val at the mid index in the array, then it sets
				// mid to high -1
			} else {
				high = mid - 1;
			}
		}
		// Returns -1 if nothing is found
		return -1;
	}

	/**
	 * Finds the goal in the arr using a Recursive form of Binary Search, returning
	 * the index
	 **/
	public static <T extends Comparable<? super T>> int binarySearchRecursive(T[] arr, T goal, int low, int high) {

		// Checks if high is greater than equal to low
		if (high >= low) {

			// Sets mid to the half of high and low
			int mid = (low + high) / 2;

			// If the value at the mid index in the array is equal to the goal, then it
			// returns mid
			if (arr[mid] == goal) {
				return mid;

				// Else if the value at the mid index in the array is less than the goal, then
				// it goes into this method again with the low index to mid + 1 and high index
				// to high
			} else if (arr[mid].compareTo(goal) < 0) {
				return binarySearchRecursive(arr, goal, mid + 1, high);

				// Else if the value at the mid index in the array is more than the goal, then
				// it goes into this method again with the low index to low and high index to
				// mid - 1
			} else if (arr[mid].compareTo(goal) > 0) {
				return binarySearchRecursive(arr, goal, low, mid - 1);
			}
		}
		// Returns -1 if nothing is found
		return -1;
	}
}