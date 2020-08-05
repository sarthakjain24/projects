'''
Created on May 3, 2020

@author: Sarthak Jain
'''


'''
Sorts the array using heap sort
'''
def heapSort(arr):
    # Sets n to the length of the array
    n = len(arr)
    # A loop that runs from half of n - 1 till -1, decrementing by 1
    for i in range(n // 2 - 1, -1, -1):
        # Goes into the heapify method, where it builds a MaxHeap
        heapify(arr, n, i)
    
    # A loop that runs from n-1 to 0, decrementing by 1
    for i in range(n - 1, 0, -1):
        
        # Swaps the value at index i and 0 in the array
        arr[i], arr[0] = arr[0], arr[i]
        
        # Goes into the heapify method where it builds a MaxHeap
        heapify(arr, i, 0)


'''
A helper method that builds a max heap
'''
def heapify(arr, n, i):
    # Sets largest to i
    largest = i
    
    # Sets left to 2 times i + 1
    left = 2 * i + 1
    
    # Sets right to 2 times i + 2
    right = 2 * i + 2
    
    # If left is smaller than n, and the value at index i is less than the value at index left, then it sets largest to left
    if left < n and arr[i] < arr[left]:
        largest = left
    
    # If right is smaller than n, and the value at index largest is less than the value at index right, then it sets largest to right    
    if right < n and arr[largest] < arr[right]:
        largest = right
    
    # If largest is not equal to i, then swaps the values at the largest and i index
    if largest != i:
        arr[i], arr[largest] = arr[largest], arr[i]
        
        # Recursively goes into the heapify method to make a MaxHeap
        heapify(arr, n, largest)

'''
Runs the main method where it sorts the array and prints out the sorted array
'''
if __name__ == "__main__": 
    print("Sorting an Array using HeapSort: ")
    arr = [24, 3476984, 11, 22, 69, 0, 88, 34, 331, 1, 23, 732, -1, 4325, 32435432532]
    print("Unsorted Array: " + str(arr));
    heapSort(arr)
    print("Sorted Array: " + str(arr))
