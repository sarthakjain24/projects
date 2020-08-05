'''
Created on May 3, 2020

@author: Sarthak Jain
'''

'''
Method that sorts an array using Insertion Sort
'''
def insertionSort(arr):
# Runs till the end of the array's length
    for i in range(1, len(arr)):
        # Sets the key as the value at the index i of an array
        key = arr[i]
        
        # Sets j to one less than i to account for index 0
        j = i - 1
        
        # Loop that runs while j is greater than equal to 0 and the key is less than the value at index j in the array
        while j >= 0 and key < arr[j]:
            # Sets the value at index j+1 to the value of index j 
            arr[j + 1] = arr[j]
        
            # Decrements j by 1
            j = j - 1
        
        # Sets the value at index j + 1 to the key
        arr[j + 1] = key

'''
Runs the main method where it sorts the array and prints out the sorted array
'''
if __name__ == "__main__": 
    print("Sorting an Array using InsertionSort: ")
    arr = [24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 32435432532]
    print("Unsorted Array: " + str(arr));
    insertionSort(arr)
    print("Sorted Array: " + str(arr))
        
