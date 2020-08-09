package searchingAlgorithms;

/**
 * Does a Sequential Search on a generic array finding a generic item
 * 
 * @author Sarthak Jain
 */
public class SequentialSearch {

	/**
	 * Runs the main method
	 */
	public static void main(String args[]) {
		System.out.println("Searches for items sequentially");
		Integer[] arr = new Integer[] { 24, 344, 11, 22, 34, 0, 88, 34, 331, 1, 23, 332, 0, 3134325, 324354532 };
		System.out.println("Array contains 88: " + sequentialSearch(arr, 88));
		System.out.println("Array contains 134: " + sequentialSearch(arr, 134));

	}

	/**
	 * The method that finds an item in an arr using Sequential Searching, searching
	 * every index in the array until the item is found
	 */
	public static <T> boolean sequentialSearch(T[] arr, T item) {
		// Loops till the end of the array
		for (int i = 0; i < arr.length; i++) {

			// If the value at index i is equal to the item, then it returns True
			if (arr[i] == item) {
				return true;
			}
		}
		// Returns False if an item is not found
		return false;
	}

}
