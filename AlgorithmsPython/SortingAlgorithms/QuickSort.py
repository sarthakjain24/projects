'''
Created on May 3, 2020

@author: Sarthak Jain
'''

'''
A helper method that does the partition
'''
def partition(arr, low, high):
    # Sets i to 1 less than low
    i = low - 1
    
    # Sets the pivot to the val at the highest index of the arr
    pivot = arr[high]
    
    # A for loop that runs from low to high
    for j in range(low, high):
        
        # If value at index j in the array is less than equal to the pivot, then goes into this condition
        if arr[j] <= pivot:
            # Increments i by 1
            i = i + 1
    
            # Swaps the values at index i and index j in the array
            arr[i], arr[j] = arr[j], arr[i]

    # Swaps the values at index i+1 and index high in the array
    arr[i + 1], arr[high] = arr[high], arr[i + 1]
    
    # Returns the i + 1 value as the partition index
    return(i + 1)
    


'''
The main method that sorts an array using QuickSort
'''
def quickSort(arr, low, high):
    # Base case checking if low is less than high, and then finds the partition index
    if(low < high):
        partitionIndex = partition(arr, low, high)
    
    # Recursively splits the array by splitting it at the partition index, and recursively sorting again
        quickSort(arr, low, partitionIndex - 1)
        quickSort(arr, partitionIndex + 1, high)
'''
Runs the main method where it sorts the array and prints out the sorted array
'''
if __name__ == "__main__": 
    print("Sorting an Array using QuickSort: ")
    arr = [24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 32435432532]
    print("Unsorted Array: " + str(arr));
    quickSort(arr, 0, len(arr) - 1)
    print("Sorted Array: " + str(arr))
        
