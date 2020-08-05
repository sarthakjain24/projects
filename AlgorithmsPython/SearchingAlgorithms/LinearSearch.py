'''
Created on May 3, 2020

@author: Sarthak Jain
'''


'''
Returns the largest item in the arr
'''
def returnSmallest(arr):
    # Sets smallest to the value at index 0
    smallest = arr[0]
    
    # Runs till the end of the arr
    for i in range (len(arr)):
           
        # If the value at index i is smaller, then sets smallest to that value
        if arr[i] < smallest:
            smallest = arr[i]
            
    # Returns smallest value
    return smallest

'''
Returns the largest item in the arr
'''
def returnLargest(arr):
     # Sets largest to the value at index 0
    largest = arr[0]
    
    # Runs till the end of the arr
    for i in range (len(arr)):
        
        # If the value at index i is larger, then sets largest to that value
        if arr[i] > largest:
            largest = arr[i]
    
    # Returns largest value
    return largest

'''
Runs the main method where it finds items in an array
'''
if __name__ == "__main__": 
    print("Returns smallest items and largest items in an array")
    arr = [24, 344, 11, 22, 34, -20, 88, 34, 331, 1, 23, 332, 0, 3134325, 32435432532]
    print("Smallest item in Array: " + str(returnSmallest(arr)));
    print("Largest item in Array: " + str(returnLargest(arr)));
    
