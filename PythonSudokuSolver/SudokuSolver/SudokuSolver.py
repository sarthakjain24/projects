'''
Method that displays the arr in a 2D grid layout
'''


def displayGrid(arr):
    for row in arr:
        for col in row:
            print(col, end=" ")
        print("")

'''
Method that checks if the number is in the row in the arr
'''


def isInRow(arr, row, number):
    # A loop that runs from the start to the end of the column, and if is at index matching with row, then returns True, else False
    for i in range(9):
        if (arr[row][i] == number):
            return True
    return False

'''
Method that checks if the number is in the col in the arr
'''


def isInCol(arr, col, number):
    # A loop that runs from the start to the end of the row, and if is at index matching with column, then returns True, else False
    for i in range(9):
        if(arr[i][col] == number):
            return True
    return False

'''
Method that checks if the number is in the mini box in the arr
'''


def isInBox(arr, row, col, number):
    # Mods the row to get the size of a box
    r = row - row % 3
    # Mods the col to get the size of a box
    c = col - col % 3
    # A loop that runs from the start to the end of the box, and if is at index, then returns True, else False
    for i in range(r, r + 3):
        for j in range(c, c + 3):
            if(arr[i][j] == number):
                return True
    return False

'''
Method that checks if the number is not repeated twice
'''


def isOk(arr, row, col, number):
    # Returns True or False based on if the number is not in the row, not in the col, and not in the box validating it
    return not isInRow(arr, row, number) and not isInCol(arr, col, number) and not isInBox(arr, row, col, number)

'''
Solves the Sudoku board
'''


def solve(arr):
    # A loop that runs till the end of the row
    for row in range(9):
    # A loop that runs till the end of the col
        for col in range(9):
            # If the arr is empty at given index, goes into condition
            if(arr[row][col] == 0):
                # Runs from 1-10, and checks if the number is valid in the given row and col, and if so sets that to the number
                for number in range(1, 10):
                    if(isOk(arr, row, col, number)):
                        arr[row][col] = number
                        # Recursively goes into to solve the arr until it is able to solve, in which case it returns True
                        if(solve(arr)):
                            return True
                        else:
                            arr[row][col] = 0
                return False 
    return True   

'''
The main method running the program
'''
if __name__ == "__main__": 
      
    # creating a 2D array for the grid 
    grid = [[0 for x in range(9)]for y in range(9)] 
      
    # assigning values to the grid 
    grid = [[0, 5, 0, 0, 0, 4, 0, 0, 0],
            [6, 0, 1, 0, 0, 0, 0, 0, 0],
            [0, 0, 4, 6, 9, 0, 0, 0, 1],
            [0, 3, 0, 0, 2, 5, 0, 0, 0],
            [7, 0, 0, 0, 0, 0, 0, 0, 8],
            [0, 0, 0, 7, 8, 0, 0, 6, 0],
            [3, 0, 0, 0, 6, 9, 2, 0, 0],
            [0, 0, 0, 0, 0, 0, 8, 0, 5],
            [0, 0, 0, 3, 0, 0, 0, 9, 0]] 
    # Prints the unsolved grid
    print("The unsolved grid is: ")
    displayGrid(grid)
    print()
    print()
    
    # Solves the grid and prints it if a solution exists, otherwise prints "No solution exists" 
    if(solve(grid)):
        print("The solved grid is: ") 
        displayGrid(grid) 
    else: 
        print ("No solution exists")
