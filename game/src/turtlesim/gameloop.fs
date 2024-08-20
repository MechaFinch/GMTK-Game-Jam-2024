
\ when these flags get fliped, the end screen will be displayed 
VARIABLE IS-ENDED
VARIABLE IS-ENDED-WIN
VARIABLE IS-ENDED-LOSS

\ runs once before GAME-UPDATE 
\ initialize all our data 
: GAME-START 
    
    FALSE IS-ENDED ! 
    FALSE IS-ENDED-WIN ! 
    FALSE IS-ENDED-LOSS !

    INIT-TURTLE-INFO-ARRAYS
    INIT-TURTLE-OBJECTPOOLER 
    INIT-SHIP-RESOURCES 
    INIT-USER-WORD-DICTIONARY

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

: PRINT-START-TEXT 
    MISSION BRIEFING

    
;

