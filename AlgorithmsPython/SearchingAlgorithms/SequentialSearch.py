'''
Created on May 3, 2020

@author: Sarthak Jain
'''

'''
Sequentially searches an Array for an item
'''
def sequentialSearch(arr, item):
    # Loops till the end of the array
    for i  in range (len(arr)):
        # If the value at index i is equal to the item, then it returns True
        if(arr[i] == item):
            return True
    # Returns False if an item is not found
    return False

'''
Runs the main method where it finds items in an array
'''
if __name__ == "__main__": 
    print("Searches for items sequentially")
    arr = [24, 344, 11, 22, 34, 0, 88, 34, 331, 1, 23, 332, 0, 3134325, 32435432532]
    print("Array contains 88: " + str(sequentialSearch(arr, 88)));
    print("Array contains 134: " + str(sequentialSearch(arr, 134)));
   
    