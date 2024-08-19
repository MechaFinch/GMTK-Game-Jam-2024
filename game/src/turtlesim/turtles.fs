
1 CONSTANT METAL-PER-TURTLE
1 CONSTANT FUEL-PER-TURTLE

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

: INIT-TURTLE-OBJECTPOOLER 
    0 TURTLES-COUNT !

;

: HAS-SPACE-FOR-NEW-TURTLE
    \ use MAX-TURTLES and the ship turtle count 
;

: GET-OPEN-TURTLE-SLOT 
    \ return the first found index to put a new turtle 
;

: INIT-TURTLE 
    \ init a turtle at the selected index in the object pooler 
    \ fill the information with defaults 
    \ increment the turtle counter 
    \ TODO set turtle active based on variable passed in 
;

: CREATE-TURTLE 
        \ TODO put these in loops based on variables
    SPEND-METAL 
    SPEND-FUEL 
    INIT-TURTLE \ TODO at selected index 
    \ TODO set that turtle active 
;

: TRY-CREATE-TURTLE

    \ validate metal 
    \ validate fuel 

    \ check if object pooler has an open slot- so really, just check if the turtle count is lower than the maximum 
    HAS-SPACE-FOR-NEW-TURTLE \ TODO put this in an if 
    GET-OPEN-TURTLE-SLOT CREATE-TURTLE \ create turtle at the next open index
;


\ directions relative to turtle facing 
: FORWARD ;
: BACK ;
: LEFT ;
: RIGHT ;


: IS-PASSABLE 
    \ is the tile at the given coordinates passable? returns true if so 
;

: SET-TURTLE-POSITION 

;

: EXECUTE-TILE-LOGIC-OVERLAP
    \ do whatever the tile does when the player lands on the tile 
;
: EXECUTE-TILE-LOGIC-EXAMINE
    \ do whatever the tile does when the player examines it
;
\ TODO more of these 


\ moving the turtle 
: MOVE-FORWARD 
    \ VALIDATE-COORDINATES of what direction it's trying to move in 
    \ if IS-PASSABLE 
    \ SET-POSITION 

;


\ rotating the turtle 
: TURN-RIGHT 

;
: TURN-LEFT 

;

: SET-TURTLE-DIRECTION 

;

\ examine the thing in front of it. will count towards discoveries 
: EXAMINE-AHEAD 

;



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

: DAMAGE-TURTLE

;

: CHECK-TURTLE-DEATH ;
: TURTLE-DIES 
    \ TODO 
    CHECK-GAMEOVER 
;

\ run the logic of a turtle 
: TICK-TURTLE
    \
;

