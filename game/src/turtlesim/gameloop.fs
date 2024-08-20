
\ runs once before GAME-UPDATE 
\ initialize all our data 
: GAME-START ( -- )
    
    FALSE IS-ENDED ! 
    FALSE IS-ENDED-WIN ! 
    FALSE IS-ENDED-LOSS !

    DEFINE-TILES
    INIT-MAP

    INIT-TURTLE-OBJECTPOOLER 
    INIT-SHIP-RESOURCES 
    INIT-USER-WORD-DICTIONARY

    0 SET-CURRENT-TURTLE

    TRY-CREATE-TURTLE
    TRY-CREATE-TURTLE
;

\ our equivalent of any game engine's update
: GAME-UPDATE ( -- )

    \ run the code for the turtle at the current turtle index
    RUN-PLAYER-CODE
    TICK-TURTLE

    \ increment the counter
    CURRENT-TURTLE++

    \ if reached the max number of turtles, set it back to 0 
    MAX-TURTLES CURRENT-TURTLE-INDEX <= IF 
        0 SET-CURRENT-TURTLE
    THEN 
;

: PRINT-START-TEXT 
    ." MISSION BRIEFING " CR
    \ TODO?
;

