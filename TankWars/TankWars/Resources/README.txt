By Sarthak Jain and Dimitrius Maritsas

Implementation of the client side code of the TankWars game.
A lot of the implementation we did was on the basis of the provided MVCChatSystem and the Lab12 code. 
The code we wrote took us about 30 hrs to write in about two weeks, albeit a lot of this time was for 
thinking and using trial and error. The code we wrote is pretty similar to the implementation of the provided
client. We have added a couple of our own additional implementations as well. We added more powerup colors, added 
the beam color to match the color of the tank who shot it. We considered adding the circles after a tank dies, as
an animation, however, due to time constraints, we decided to avoid doing so. We also showed message boxes after connecting 
to the server of if there was an issue with connecting to the server, giving the user the ability to know their status if
they were connected or not.


Implementation of the server side code of the TankWars game(22nd Nov 2020 - 4th Dec 2020)
A lot of the implementation we did was looking at the MVCChatSystem where we had a ChatServer. The code we had to write took about
30 hrs to write in about two weeks. A lot of it involved in debugging, and the biggest issue we had was detecting collisions that took us
a solid 5 hrs to figure out. The way to figure out was through drawing on pieces of paper, and by drawing in order to figure out collisions.
It may seem that there is a lot of repitition in terms of checking for collisions, however, the four methods were required in order to ensure 
that we are able to check for all possible scenarios, and are not necessarily considered as repeated logic.
Apart from that, the biggest issue we had was disconnecting, something that made it hard to understand as we believed we were doing the right 
thing. However, we clearly were not, and had to change the disconnection from the receiving information to sending information. We had initially
written all the code in the server just so that we could understand how to properly implement it, but unfortunately we had to change most of the 
code from the server to the world side, which made it a bit error prone, but something that we had to do in order to achieve MVC to the fullest. 
As an additional implementation, we ensured that the tank is shown dead until it respawns.

Edit: 6th Dec 2020
There is a very weird issue dealing with the game randomly freezing, something that we had no clue about, and were unable to find the cause of the 
issue. The issue was also highlighted at @2029 on Piazza.

Note to grader: If you were able to find out what was causing the issue, can you let us know via a comment on Gradescope?