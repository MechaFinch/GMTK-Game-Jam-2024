
VARIABLE TURTLE-TURNTAKER

\ runs once before GAME-UPDATE 
: GAME-START 
    \ initialize all our data 



    0 TURTLE-TURNTAKER !
;

\ our equivalent of any game engine's update
: GAME-UPDATE 

    \ run the code for the turtle at index TURTLE-TURNTAKER ie TICK-TURTLE

    \ increment the turntaker 

    \ if reached the max number of turtles, set it back to 0 
;
