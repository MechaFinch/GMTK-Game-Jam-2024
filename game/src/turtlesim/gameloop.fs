

\ runs once before GAME-UPDATE 
: GAME-START 
    \ initialize all our data 



    0 SET-CURRENT-TURTLE
;

\ our equivalent of any game engine's update
: GAME-UPDATE 

    \ run the code for the turtle at index TURTLE-TURNTAKER ie TICK-TURTLE

    \ increment the turntaker 
    CURRENT-TURTLE++

    \ if reached the max number of turtles, set it back to 0 
    MAX-TURTLES CURRENT-TURTLE-INDEX >= IF 
        0 SET-CURRENT-TURTLE
    ELSE 

    THEN 

;
