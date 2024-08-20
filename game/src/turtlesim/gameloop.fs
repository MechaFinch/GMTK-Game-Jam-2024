

\ runs once before GAME-UPDATE 
: GAME-START 
    \ initialize all our data 

    INIT-TURTLE-INFO-ARRAYS
    INIT-TURTLE-OBJECTPOOLER 
    INIT-SHIP-RESOURCES 

    0 SET-CURRENT-TURTLE
;

\ our equivalent of any game engine's update
: GAME-UPDATE 

    \ run the code for the turtle at index TURTLE-TURNTAKER ie TICK-TURTLE
    RUN-PLAYER-CODE

    \ increment the counter
    CURRENT-TURTLE++

    \ if reached the max number of turtles, set it back to 0 
    MAX-TURTLES CURRENT-TURTLE-INDEX >= IF 
        0 SET-CURRENT-TURTLE
    THEN 
;
