
20 CONSTANT MAX-TURTLES

VARIABLE TURTLES-COUNT
: ADD-TURTLE-COUNT ;
: DECREMENT-TURTLE-COUNT ;

\ object pooler of turtle structs that get reused 
VARIABLE TURTLES-LIST

\ a turtle stores 
\ x,y position on the grid 
\ direction facing 
\ hp (pips)
\ a timer indicating how close it is to corroding a pip 
\ 

: VALIDATE-SPACE-FOR-NEW-TURTLE
    \ use MAX-TURTLES and the ship turtle count 
;

: FIND-OPEN-TURTLE-SLOT 
    \ return the first found index to put a new turtle 
;

: INIT-TURTLE 
    \ init a turtle at the selected index in the object pooler 
    \ fill the information with defaults 
    \ increment the turtle counter 
;

: CREATE-TURTLE 
    SPEND-METAL 
    SPEND-FUEL 
    INIT-TURTLE
;

: TRY-CREATE-TURTLE
    \ validate metal 
    \ validate fuel 
    \ check if object pooler has an open slot- so really, just check if the turtle count is lower than the maximum 
    CREATE-TURTLE
;


\ a game clock for simulating turtles 
: TICK-TURTLES ;

\ directions relative to world 
0 CONSTANT NORTH-X 
1 CONSTANT NORTH-Y

0 CONSTANT SOUTH-X 
-1 CONSTANT SOUTH-Y

1 CONSTANT EAST-X 
0 CONSTANT EAST-Y 

-1 CONSTANT WEST-X 
0 CONSTANT WEST-Y

: NORTH ;
: SOUTH ;
: EAST ;
: WEST ;

\ directions relative to turtle facing 
: FORWARD ;
: BACK ;
: LEFT ;
: RIGHT ;

\ moving the turtle 
: MOVE-FORWARD ;

: SET-TURTLE-POSITION ;

\ rotating the turtle 
: TURN-RIGHT ;
: TURN-LEFT ;

: SET-TURTLE-DIRECTION ;

\ examine the thing in front of it. will count towards discoveries 
: EXAMINE-AHEAD ;



: IS-METAL-ON-TILE

;
: IS-FUEL-ON-TILE

;
: PICK-UP-FUEL 
    \ TODO provide coordinates for the below
    REMOVE-FUEL-TILE
    ADD-FUEL
;
: PICK-UP-METAL 
    \ TODO provide coordinates for the below
    REMOVE-METAL-TILE 
    ADD-METAL 
;

\ pick up the thing it's on right now 
: PICK-UP 
    \ pick up whatever's on the tile of the given coordinates

    \ IF there was fuel on the tile 

    \ IF there was metal on the tile 
    
;
\ if it's fuel or metal, it gets added 





: CHECK-TURTLE-DEATH ;
: TURTLE-DIES 
    / TODO 
    CHECK-GAMEOVER 
;