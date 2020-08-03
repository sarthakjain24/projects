from random import randint
# Initializes the options as Rock, Paper, Scissors
options = ["Rock", "Paper", "Scissors"]

# Gets a random option from the options and sets is as the computer's option
computer = options[randint(0, 2)]

# Sets the player to False
player = False

# Runs while player is False
while player == False:
    # Gets the input from the player
    player = input("Rock, Paper, or Scissors?")
    # Converts the player and computer input to lower case
    computerLowerCase = computer.lower()
    playerLowerCase = player.lower()
    
    # If computer and player are the same, then prints a tie
    if (computerLowerCase in playerLowerCase):
        print("Tie! The computer has also played " + player)
    
    # Else if the user plays rock, then it goes into this condition
    elif("rock" in playerLowerCase):
        #If computer plays Paper, then says that you lose
        if (computer == "Paper"):
            print("You lose! The Paper covers the Rock")
        #If computer plays Rock, then says that you win
        elif (computer == "Scissors"):
            print("You win! The Rock breaks the Scissors")
    # Else if the user plays Paper, then it goes into this condition
    elif("paper" in playerLowerCase):
        #If computer plays Rock, then says that you win
        if (computer == "Rock"):
            print("You win! The Paper covers the Rock")
        #If computer plays Scissors, then says that you lose
        elif (computer == "Scissors"):
            print("You lose! The Scissors cuts the Paper")
    # Else if the user plays Scissors, then it goes into this condition
    elif("scissors" in playerLowerCase):
        #If computer plays Rock, then says that you lose
        if (computer == "Rock"):
            print("You lose! The Rock breaks the Scissors")
        #If computer plays Paper, then says that you win
        elif (computer == "Paper"):
            print("You win! The Scissors cuts the Paper")
    else:
        print("Error, Try Checking Your Spelling, Or Retry")
    # Sets the player to False
    player = False
    # Gets another option for the computer
    computer = options[randint(0, 2)]

        