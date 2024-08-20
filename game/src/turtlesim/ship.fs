
\ overall information about the ship 
\ stores some authoritative variables 

\ this needs to be here for ending validation
16 CONSTANT MAX-TURTLES
VARIABLE TURTLE-COUNT

1 CONSTANT FUEL-STARTING
1 CONSTANT METAL-STARTING 

1 CONSTANT METAL-PER-TURTLE
1 CONSTANT FUEL-PER-TURTLE

0 CONSTANT DISCOVERIES-STARTING
4 CONSTANT DISCOVERIES-TOTAL

10 CONSTANT FUEL-TO-WIN 

VARIABLE FUEL-COUNT
VARIABLE METAL-COUNT
VARIABLE DISCOVERIES-COUNT

\ getters for the player (almost had a really funny oversight where the player would just be able to set their own fuel and metal values... hahaha)
: GET-FUEL-COUNT 
    FUEL-COUNT
;
: GET-METAL-COUNT 
    METAL-COUNT
;

\VARIABLE DISCOVERIES DISCOVERIES-TOTAL CELLS ALLOT
\: DISCOVERIES[]  ( discovery # -- addr ) CELLS DISCOVERIES + ; \ get the discovery at this index 


: INIT-SHIP-RESOURCES 
    FUEL-STARTING FUEL-COUNT !
    METAL-STARTING METAL-COUNT !
    DISCOVERIES-STARTING DISCOVERIES-COUNT !
    
    \ init all discoveries as false to start with 
    \DISCOVERIES-TOTAL 0 DO 
    \    FALSE I DISCOVERIES[] !
    \LOOP
;


: VALIDATE-NEWTURTLE-FUEL ( -- bool )
    FUEL-COUNT FUEL-PER-TURTLE >= IF TRUE ELSE FALSE THEN
;
: VALIDATE-NEWTURTLE-METAL ( -- bool )
    METAL-COUNT METAL-PER-TURTLE >= IF TRUE ELSE FALSE THEN
;

\ spend the amount of resources needed for a new turtle 
: SPEND-FUEL 
    FUEL-PER-TURTLE FUEL-COUNT -!
;
: SPEND-METAL 
    METAL-PER-TURTLE METAL-COUNT -!
;


\: CHECK-IF-DISCOVERED 
\;


: WIN-GAME 
    TRUE IS-ENDED-WIN !
    TRUE IS-ENDED !
;
: LOSE-GAME 
    TRUE IS-ENDED-LOSS !
    TRUE IS-ENDED !
;

: CHECK-FUEL-WIN 
    \ returns true if we have enough fuel to win 
    FUEL-COUNT FUEL-TO-WIN >= IF
        TRUE
    ELSE 
        FALSE 
    THEN 
;

: TRY-FUEL-WIN
    CHECK-FUEL-WIN IF 
        WIN-GAME
    ELSE 
    THEN
;


: ADD-FUEL 
    1 FUEL-COUNT +!
    TRY-FUEL-WIN
;
: ADD-METAL 
    1 METAL-COUNT +!
;
: ADD-DISCOVERY 
    1 DISCOVERY-COUNT +!
;


: CHECK-FUEL-GAMEOVER ( -- bool )
    \ returns true if we're out of fuel AND no turtles remain 
    TURTLE-COUNT 0 <= IF
        FUEL-COUNT 0 <= IF
            TRUE
        ELSE 
            FALSE 
        THEN 
    ELSE 
        FALSE 
    THEN
;
: CHECK-METAL-GAMEOVER ( -- bool )
    \ returns true if we're out of fuel AND no turtles remain 
    TURTLE-COUNT 0 <= IF
        METAL-COUNT 0 <= IF
            TRUE
        ELSE 
            FALSE 
        THEN 
    ELSE 
        FALSE 
    THEN
;

\ no turtles alive AND 0 of either fuel or metal to make more 
: CHECK-GAMEOVER 
    \ returns true if we've lost the game 
    CHECK-FUEL-GAMEOVER IF 
        LOSE-GAME
    ELSE 
        CHECK-METAL-GAMEOVER IF 
            LOSE-GAME
        ELSE 
        THEN
    THEN
;

\ handling game overs 

\ this gets called by the UI 
: PRINT-ENDING-TEXT 
    \ figure out what kind of ending we got 
    CHECK-FUEL-GAMEOVER IF 
        PRINT-END-FUEL
    ELSE    
        CHECK-METAL-GAMEOVER IF 
            PRINT-END-METAL
        ELSE 
            CHECK-FUEL-WIN IF 
                DISCOVERIES-COUNT DISCOVERIES-TOTAL >= IF 
                    PRINT-END-ART-ALL 
                ELSE 
                    DISCOVERIES-COUNT 0 > IF 
                        PRINT-END-ART-SOME 
                    ELSE 
                        PRINT-END-ART-SOME 
                    THEN 
                THEN
            ELSE 
            THEN 
        THEN
    THEN
;
