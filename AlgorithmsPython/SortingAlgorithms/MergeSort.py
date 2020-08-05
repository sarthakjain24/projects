'''
Created on May 3, 2020

@author: Sarthak Jain
'''

'''
Method that sorts an array using MergeSort
'''
def mergeSort(arr, left, right):
    # Goes into this if left is smaller than right
    if left < right:
        
        # Finds the middle between left and right to get the index to divide the array by half
        middle = (left + (right - 1)) // 2
        
        # Recursively goes into the mergeSort method setting left to the left index and middle to the right index 
        mergeSort(arr, left, middle)
        
        # Recursively goes into the mergeSort method setting middle + 1 to the left index and right to the right index 
        mergeSort(arr, middle + 1, right)
        
        # Merges the arrays together 
        merge(arr, left, middle, right)


'''
Merges the items where it sorts an array in a smaller portion of the array
'''
def merge(arr, left, middle, right):
    # Sets the leftSide by subtracting left from one more than middle to get the size of the array
    leftSide = middle - left + 1
    
    # Sets the rightSide by subtracting middle from right to get the size of the array
    rightSide = right - middle
    
    # Creates a LeftArr based on the size of the leftSide
    LeftArr = [0] * (leftSide)

    # Creates a RightArr based on the size of the rightSide
    RightArr = [0] * (rightSide)
    
    # Adds values from the arr to the LeftArr starting from left
    for i in range(0, leftSide):
        LeftArr[i] = arr[left + i]

    # Adds values from the arr to the RightArr starting from middle + 1    
    for i in range(0, rightSide):
        RightArr[i] = arr[middle + 1 + i]
    
    # Initializes variables  
    i = 0
    j = 0
    k = left
    
    # Loop that runs while i is smaller than leftSide and j is smaller than rightSide
    while i < leftSide and j < rightSide:

        # If the value at index i in the leftArr is less than equal to the value at index j in the rightArr, then goes into this condition
        if LeftArr[i] <= RightArr[j]:
        # Sets the value at index k in the arr to the value at index i in the leftArr, and increments i
            arr[k] = LeftArr[i]
            i += 1
        else:
        
        # Otherwise Sets the value at index k in the arr to the value at index j in the rightArr, and increments j
            arr[k] = RightArr[j]
            j += 1
            
        # Increments k by 1
        k += 1
        
    # Loop that runs while i is less than leftSide
    while i < leftSide:

        # Sets the value at index k in the array to the value at index i in the leftArr
        arr[k] = LeftArr[i]
        
        # Increments i and k
        i += 1
        k += 1
    
    # Loop that runs while j is less than rightSide
    while j < rightSide:
        
        # Sets the value at index k in the array to the value at index j in the rightArr
        arr[k] = RightArr[j]
        
        # Increments j and k
        j += 1
        k += 1

'''
Runs the main method where it sorts the array and prints out the sorted array
'''
if __name__ == "__main__": 
    print("Sorts an array using MergeSort")
    arr = [24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 32435432532]
    print("Unsorted Array: " + str(arr));
    mergeSort(arr, 0, len(arr) - 1)
#     mergesort(arr)
    print("Sorted Array: " + str(arr))
