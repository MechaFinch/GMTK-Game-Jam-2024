
\ overall information about the ship 
\ stores some authoritative variables 

1 CONSTANT FUEL-STARTING
1 CONSTANT METAL-STARTING 
0 CONSTANT DISCOVERIES-STARTING
10 CONSTANT DISCOVERIES-TOTAL

20 CONSTANT FUEL-TO-WIN 
\ 10 CONSTANT DISCOVERIES-TO-WIN

VARIABLE FUEL-COUNT
VARIABLE METAL-COUNT
VARIABLE DISCOVERIES-COUNT

VARIABLE DISCOVERIES DISCOVERIES-TOTAL CELLS ALLOT
: DISCOVERIES[]  ( discovery # -- addr ) CELLS DISCOVERIES + ; \ get the discovery at this index 


: INIT-SHIP-RESOURCES 
    FUEL-STARTING FUEL-COUNT !
    METAL-STARTING METAL-COUNT !
    DISCOVERIES-STARTING DISCOVERIES-COUNT !
    
    \ init all discoveries as false to start with 
    DISCOVERIES-TOTAL 0 DO 
        FALSE I DISCOVERIES[] !
    LOOP
;

: ADD-FUEL 
    1 FUEL-COUNT +!
    UIUPDATE-FUEL
    CHECK-GAMEWIN
;
: ADD-METAL 
    1 METAL-COUNT +!
    UIUPDATE-METAL
;
: ADD-DISCOVERY 
    1 DISCOVERY-COUNT +!
    UIUPDATE-DISCOVERIES
;

: VALIDATE-NEWTURTLE-FUEL
    FUEL-COUNT FUEL-PER-TURLTE >= IF true ELSE false THEN
;
: VALIDATE-NEWTURTLE-METAL 
    METAL-COUNT METAL-PER-TURLTE >= IF true ELSE false THEN
;

: SPEND-FUEL 
    1 FUEL-COUNT -!
    UIUPDATE-FUEL
;
: SPEND-METAL 
    1 METAL-COUNT -!
    UIUPDATE-METAL
;


: CHECK-IF-DISCOVERED ;

\ create a turtle IF there's enough resources 
: TRY-CREATE-TURTLE 
    \ TODO check if we have enough resources 
    CREATE-TURTLE
;


\ enough fuel and enough discoveries to take off 
: CHECK-GAMEWIN 
    \ returns true if we've won the game

;
: CHECK-FUEL-WIN 
    \ returns true if we have enough fuel to win 

;
\ (wait, or is it fuel and then discoveries is score?)
\: CHECK-DISCOVERIES-WIN ;

\ no turtles alive AND 0 of either fuel or metal to make more 
: CHECK-GAMEOVER 
    \ returns true if we've lost the game 

;
: CHECK-FUEL-GAMEOVER 
    \ returns true if we're out of fuel AND no turtles remain 

;
: CHECK-METAL-GAMEOVER 
    \ returns true if we're out of fuel AND no turtles remain 

;

: END-GAME 
    \ TODO 
    \ display total discoveries 
;

