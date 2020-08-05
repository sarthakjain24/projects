'''
Created on May 3, 2020

@author: Sarthak Jain
'''


'''
Method that sorts an array using Selection Sort
'''
def selectionSort(arr):
    # Runs till the end of the length of the array
    for i in range(len(arr)):
        # Sets minVal to i
        minVal = i
        
        # Another loop that runs from i + 1 to the end of the array
        for j in range(i + 1, len(arr)):
            # If the value at minVal in the array is greater than the value at index j in the array, then sets minVal to j
            if arr[minVal] > arr[j]:
                minVal = j
        
        # Swaps the value at index i and minVal in the array
        arr[i], arr[minVal] = arr[minVal], arr[i]
        
'''
Runs the main method where it sorts the array and prints out the sorted array
'''
if __name__ == "__main__": 
    print("Sorting an Array using Selection Sort: ")
    arr = [24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 32435432532]
    print("Unsorted Array: " + str(arr));
    selectionSort(arr)
    print("Sorted Array: " + str(arr))
        
