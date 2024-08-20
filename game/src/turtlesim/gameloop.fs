
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

\ handling game overs 

\ this gets called by the UI 
: PRINT-ENDING-TEXT 
    \ TODO figure out what kind of ending we got 

;

\ we have 26 characters across and 22 down 
: PRINT-END-METAL 
    ."         GAME OVER         " CR
    ." Without enough FUEL to    " CR
    ." take off or METAL to build" CR
    ." new probes to collect     " CR
    ." more, you're stranded far " CR
    ." from home.                " CR
    ." You can only wait and hope" CR
    ." for a filament-thin chance" CR
    ." at rescue.                " CR
    ." Food will run out over the" CR
    ." coming weeks, then water, " CR
    ." then oxygen, after which  " CR
    ." your satellite will become" CR
    ." your coffin, as inert as  " CR
    ." the tomb-world below.     " CR
;
: PRINT-END-FUEL 
    ."         GAME OVER         " CR
    ." Without enough FUEL to    " CR
    ." take off or build new     " CR
    ." probes to collect more,   " CR
    ." you're stranded far from  " CR
    ." home.                     " CR
    ." You can only wait and hope" CR
    ." for a filament-thin chance" CR
    ." at rescue.                " CR
    ." Food will run out over the" CR
    ." coming weeks, then water, " CR
    ." then oxygen, after which  " CR
    ." your satellite will become" CR
    ." your coffin, as inert as  " CR
    ." the tomb-world below.     " CR
;
: PRINT-END-ART-ALL

    MISSION RESULTS:
    OUTSTANDING

;

: PRINT-END-ART-SOME 

;

: PRINT-END-ART-NONE

;

: PRINT-START-TEXT 
    MISSION BRIEFING

    
;

