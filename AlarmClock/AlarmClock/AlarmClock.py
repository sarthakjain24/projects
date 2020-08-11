import datetime
import sys
from playsound import playsound
'''
An Alarm Clock

@author: Sarthak Jain
'''

# Gets the hour, and minute input from the user 
alarmHr = int(input("What Hour do you want to wake up?"))
alarmMin = int(input("What Minute do you want to wake up?"))

# Gets if it's in the morning or evening
amPm = str(input("AM or PM?"))

# If input says "am", and hr is 12, then converts it 00, to make it midnight 
if(amPm.lower() == "am"):
    if(alarmHr == 12):
        alarmHr = (alarmHr + 12) % 24

# If input says "am", and hr is not 12, then adds 12 to the alarmHr
if(amPm.lower() == "pm"):
    if(alarmHr != 12):
        alarmHr = (alarmHr + 12) % 24

        
def printTime():
    if(alarmHr < 10):
        print("Alarm went off at 0" + str(alarmHr) + ":" + str(alarmMin))
    # If the alarmMin is less than 10, then it adds a 0 in front of it
    elif(alarmMin < 10):    
        print("Alarm went off at " + str(alarmHr) + ":0" + str(alarmMin))
    # If the alarmHr and the alarmMin are both less than 10, then it adds a 0 in front of them
    elif(alarmHr < 10 and alarmMin < 10):
        print("Alarm went off at 0" + str(alarmHr) + ":0" + str(alarmMin))
    # break 


# Runs Infinitely
while(True):
    
    # If the alarmHr and alarmMin is equivalent to the current hr and minute, then goes into this condition
    while(alarmHr == datetime.datetime.now().hour and alarmMin == datetime.datetime.now().minute):
        print("Wake up")
       
        # Plays the sound of an AlarmClock
        playsound('/Songs/alarmClock.mp3')
        
        # Prints the time at which the alarm went off
        printTime()
        
        # If the user enters Q, then it stops the alarm from going off
        choice = input("To quit enter Q")
        
        # If the user enter Q, then it exits the application, else continues
        if choice.lower() == "q":
            print()
            
            print("Quitting the Application")
            
    #         break
            sys.exit(0)
            
            break
        else:
            continue
