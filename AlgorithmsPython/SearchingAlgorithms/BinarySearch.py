'''
Created on May 3, 2020

@author: Sarthak Jain
'''


'''
Finds the goal in the arr using an Iterative form of Binary Search
'''
def binarySearchIterative(arr, goal):
    # Sets low to 0
    low = 0

    # Sets high to the length of the array
    high = len(arr)
    
    # Runs while low is less than equal to high
    while low <= high:
       
        # Sets mid to the half of high and low
        mid = (high + low) // 2
        
        # If goal is equal to the val at the mid index in the array, then it returns mid
        if(goal == arr[mid]):
            return mid
        
        # If goal is greater than the val at the mid index in the array, then it sets low to mid + 1
        elif(goal > arr[mid]):
            low = mid + 1
        
        # If goal is smaller than the val at the mid index in the array, then it sets mid to high -1
        else:
            high = mid - 1
            
    # Returns -1 if nothing is found
    return -1


'''
Finds the goal in the arr using a Recursive form of Binary Search
'''
def binarySearchRecursive(arr, goal, low, high):

    # Checks if high is greater than equal to low
    if(high >= low):
        
        # Sets mid to the half of high and low
        mid = (low + high) // 2
        
        # If the value at the mid index in the array is equal to the goal, then it returns mid
        if(arr[mid] == goal):
            return mid
        
        # Else if the value at the mid index in the array is less than the goal, then it goes into this method again with the low index to mid + 1 and high index to high
        elif(arr[mid] < goal):
            return binarySearchRecursive(arr, goal, mid + 1, high)
        
        # Else if the value at the mid index in the array is more than the goal, then it goes into this method again with the low index to low and high index to mid - 1
        elif(arr[mid] > goal):
            return binarySearchRecursive(arr, goal, low, mid - 1)
   
    # Returns -1 if nothing is found
    return -1
'''
Runs the main method where it finds items in an array
'''
if __name__ == "__main__": 
    print("Searches for items sequentially")
    arr = [24, 344, 11, 22, 34, 0, 88, 34, 331, 1, 23, 332, 0, 3134325, 32435432532]
    print("Index of Array containing 22 using Recursion: " + str(binarySearchRecursive(arr, 22, 0, len(arr))))
    print("Index of Array containing 4 using Recursion: " + str(binarySearchRecursive(arr, 4, 0, len(arr))))
    print("Index of Array containing 34 using Iteration: " + str(binarySearchIterative(arr, 34)))
    print("Index of Array containing -1 using Iteration: " + str(binarySearchIterative(arr, -1)))
    
