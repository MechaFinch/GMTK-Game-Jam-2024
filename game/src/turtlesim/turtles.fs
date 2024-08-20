
10 CONSTANT STARTING-HP 
0 CONSTANT STARTING-DIRECTION 
10 CONSTANT STARTING-COUNTDOWN

: STARTING-X ( -- int )
    MAP-SIZE 2 /
;
: STARTING-Y ( -- int )
    MAP-SIZE 2 /
;

: TURTLE-COUNT++
    1 TURTLE-COUNT +!
;
: TURTLE-COUNT--
    1 TURTLE-COUNT -!
;

\ since we'll often want to re-use the index of the turtle we're talking about, these variables will keep track of the turtle all these functions will act on 
VARIABLE CURRENT-TURTLE-INDEX

: SET-CURRENT-TURTLE ( index -- )
    CURRENT-TURTLE-INDEX !
;

: CURRENT-TURTLE++ 
    1 CURRENT-TURTLE-INDEX +!
;


\ the turtle data struct the current index points to 
\ : CURRENT-TURTLE ( -- currentturtleaddr )
\    CURRENT-TURTLE-INDEX CELLS TURTLES-LIST +
\ ;

\ a turtle stores 
\ x,y position on the grid 
\ direction facing 
\ hp (pips)
\ a timer indicating how close it is to corroding a pip 
\ 

\ parallel arrays storing the information about the turtles 
VARIABLE ARR-ISACTIVE   MAX-TURTLES CELLS ALLOT
VARIABLE ARR-HP         MAX-TURTLES CELLS ALLOT
VARIABLE ARR-X          MAX-TURTLES CELLS ALLOT
VARIABLE ARR-Y          MAX-TURTLES CELLS ALLOT
VARIABLE ARR-DIRECTION  MAX-TURTLES CELLS ALLOT
VARIABLE ARR-COUNTDOWN  MAX-TURTLES CELLS ALLOT

\ object pooler of turtle structs that get reused 
\ VARIABLE TURTLES-LIST


\ words to get specific properties of the turtle of specified index 
: TURTLES[].ISACTIVE ( index -- isactive )
    CELLS ARR-ISACTIVE + 
;
: TURTLES[].HP ( index -- hp )
    CELLS ARR-HP + 
;
: TURTLES[].X ( index -- x )
    CELLS ARR-X + 
;
: TURTLES[].Y ( index -- y)
    CELLS ARR-Y + 
;
: TURTLES[].COORDS ( index -- x y )
    TURTLES[].X TURTLES[].Y
;
: TURTLES[].DIRECTION ( index -- direction )
    CELLS ARR-DIRECTION + 
;
: TURTLES[].COUNTDOWN ( index -- countdown )
    CELLS ARR-COUNTDOWN + 
;

\ words to get specific properties of the current turtle 
: TURTLES[CURRENT].ISACTIVE ( -- isactive )
    CURRENT-TURTLE-INDEX @ TURTLES[].ISACTIVE
;
: TURTLES[CURRENT].HP ( -- hp )
    CURRENT-TURTLE-INDEX @ TURTLES[].HP
;
: TURTLES[CURRENT].X ( -- x )
    CURRENT-TURTLE-INDEX @ TURTLES[].X
;
: TURTLES[CURRENT].Y ( -- y)
    CURRENT-TURTLE-INDEX @ TURTLES[].Y
;
: TURTLES[CURRENT].COORDS ( -- x y )
    CURRENT-TURTLE-INDEX @ TURTLES[].COORDS
;
: TURTLES[CURRENT].DIRECTION ( -- direction )
    CURRENT-TURTLE-INDEX @ TURTLES[].DIRECTION
;
: TURTLES[CURRENT].COUNTDOWN ( -- countdown )
    CURRENT-TURTLE-INDEX @ TURTLES[].COUNTDOWN
;


\ directions relative to current turtle facing 
\ these return the coordinates of the tile referred to by this
\ used by the player 
: FORWARD ( -- x y )
    TURTLES[CURRENT].DIRECTION ENUM-NORTH @ = IF
        NORTH-OF-ME
    THEN 
    TURTLES[CURRENT].DIRECTION ENUM-EAST @ = IF
        EAST-OF-ME
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-SOUTH @ = IF
        SOUTH-OF-ME
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-WEST @ = IF
        WEST-OF-ME
    THEN
;
: BACK ( -- x y )
    TURTLES[CURRENT].DIRECTION ENUM-NORTH @ = IF
        SOUTH-OF-ME
    THEN 
    TURTLES[CURRENT].DIRECTION ENUM-EAST @ = IF
        WEST-OF-ME
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-SOUTH @ = IF
        NORTH-OF-ME
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-WEST @ = IF
        EAST-OF-ME
    THEN
;
: LEFT ( -- x y )
    TURTLES[CURRENT].DIRECTION ENUM-NORTH @ = IF
        WEST-OF-ME
    THEN 
    TURTLES[CURRENT].DIRECTION ENUM-EAST @ = IF 
        NORTH-OF-ME
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-SOUTH @ = IF
        EAST-OF-ME
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-WEST @ = IF
        SOUTH-OF-ME
    THEN
;
: RIGHT ( -- x y )
    TURTLES[CURRENT].DIRECTION ENUM-NORTH @ = IF
        EAST-OF-ME
    THEN 
    TURTLES[CURRENT].DIRECTION ENUM-EAST @ = IF
        SOUTH-OF-ME
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-SOUTH @ = IF
        WEST-OF-ME
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-WEST @ = IF
        NORTH-OF-ME
    THEN
;

: NORTH-OF-ME ( -- x y )
    TURTLES[CURRENT].COORDS NORTH +COORD
;

: EAST-OF-ME ( -- x y )
    TURTLES[CURRENT].COORDS EAST +COORD
;

: SOUTH-OF-ME ( -- x y )
    TURTLES[CURRENT].COORDS SOUTH +COORD
;

: WEST-OF-ME ( -- x y )
    TURTLES[CURRENT].COORDS WEST +COORD
;


\ inits an INACTIVE turtle at the current index with default starting values 
: INIT-CURRENT-TURTLE-EMPTY 
    FALSE TURTLES[CURRENT].ISACTIVE SWAP ! 
    STARTING-HP TURTLES[CURRENT].HP SWAP ! 
    STARTING-X TURTLES[CURRENT].X SWAP ! 
    STARTING-Y TURTLES[CURRENT].Y SWAP !
    STARTING-DIRECTION TURTLES[CURRENT].DIRECTION SWAP !
    STARTING-COUNTDOWN TURTLES[CURRENT].COUNTDOWN SWAP !
;

: INIT-TURTLE-OBJECTPOOLER 
    0 TURTLE-COUNT !

    \ create empty turtles 
    MAX-TURTLES 0 DO 
        I CURRENT-TURTLE-INDEX ! 
        INIT-CURRENT-TURTLE-EMPTY
    LOOP

    \ put the current back where it started 
    0 CURRENT-TURTLE-INDEX !
;

: HAS-SPACE-FOR-NEW-TURTLE ( -- bool )
    \ use MAX-TURTLES and the ship turtle count 
    TURTLE-COUNT @ MAX-TURTLES < IF 
        TRUE
    ELSE 
        FALSE
    THEN 
;

\ return the first found index to put a new turtle 
: GET-OPEN-TURTLE-SLOT ( -- index )
    
    MAX-TURTLES 0 DO 
        I TURTLES[].ISACTIVE IF 
            I 
            EXIT
        THEN 
    LOOP

    \ none found 
    -1
;

\ inits an ACTIVE turtle 
VARIABLE TURTLE-INDEX-CREATING
VARIABLE CURRENT-TURTLE-INDEX-STORAGE
: CREATE-TURTLE 

    SPEND-METAL 
    SPEND-FUEL 

    \ init a turtle at the selected index in the object pooler
    GET-OPEN-TURTLE-SLOT TURTLE-INDEX-CREATING !

    \ fill the information with defaults 
    CURRENT-TURTLE-INDEX @ CURRENT-TURTLE-INDEX-STORAGE !
    TURTLE-INDEX-CREATING @ CURRENT-TURTLE-INDEX !
    INIT-CURRENT-TURTLE-EMPTY
    
    \ increment the turtle counter 
    TURTLE-COUNT++

    \ set turtle active based on variable passed in 
    TRUE TURTLES[CURRENT].ISACTIVE !
    
    \ reset current turtle counter to what it was at before not the new turtle 
    CURRENT-TURTLE-INDEX-STORAGE @ CURRENT-TURTLE-INDEX !
;

: FAILED-CREATE-TURTLE-METAL
    ." Not enough METAL! " CR
;
: FAILED-CREATE-TURTLE-FUEL
    ." Not enough FUEL! " CR
;
: FAILED-CREATE-TURTLE-COUNTLIMIT
    ." Maximum probes reached! " CR
;

: TRY-CREATE-TURTLE ( -- )

    \ validate metal 
    VALIDATE-NEWTURTLE-METAL IF

        \ validate fuel 
        VALIDATE-NEWTURTLE-FUEL IF

            \ validate enough turtles
            \ check if object pooler has an open slot- so really, just check if the turtle count is lower than the maximum 
            HAS-SPACE-FOR-NEW-TURTLE IF 

                CREATE-TURTLE \ create turtle at the next open index
            ELSE 
                FAILED-CREATE-TURTLE-COUNTLIMIT
            THEN 
        ELSE 
            FAILED-CREATE-TURTLE-FUEL
        THEN 
    ELSE 
        FAILED-CREATE-TURTLE-METAL
    THEN 
;

\ the user-facing word for creating a turtle 
: CLONE-SELF
    TRY-CREATE-TURTLE
;


\ just sets coordinates nothing else 
: SET-TURTLE-POSITION ( x y -- )
    TURTLES[CURRENT].Y SWAP !
    TURTLES[CURRENT].X SWAP !
;



\ what happens when the current turtle overlaps the current tile 
VARIABLE OVERLAPPING-TILE-TYPE
: OVERLAP-TILE ( x y -- )
    \ effectively a switch statement based on the tile type id determining what happens 
    \ we can also print stuff here 

    GET-TILE-TYPEID OVERLAPPING-TILE-TYPE !
    
    \ artifact: artifact added, removed from tile 
    OVERLAPPING-TILE-TYPE TID-ARTIFACT = IF 
        PICK-UP-ARTIFACT
    THEN

    \ acid: hurts you
    OVERLAPPING-TILE-TYPE TID-ACID = IF 
        DAMAGE-TURTLE 
    THEN

    \ fuel and metal are not automatically picked up I guess? since there's a word for it?

    \ fuel 

    \ metal 

    \ clear, ect: nothing happens

;
\ : EXECUTE-TILE-LOGIC-EXAMINE
    \ do whatever the tile does when the player examines it
\ ;
\ TODO more of these 





VARIABLE MTT-X 
VARIABLE MTT-Y
: MOVE-TO-TILE ( x y -- )

    MTT-Y ! 
    MTT-X ! 

    MTT-X @ MTT-Y @ SET-TURTLE-POSITION 

    MTT-X @ MTT-Y @ DISCOVER-TILE

    MTT-X @ MTT-Y @ OVERLAP-TILE

    \ do anything that needs to be done on this tile, and use TURTLES[CURRENT].COORDS to get the coords again 
;

: FAILED-INVALID-COORDINATES 
    ." Could not move forward into an impenetrable mist! " CR
;

: FAILED-IMPASSABLE 
    ." Could not move forward: impassable! " CR
;




\ moving the turtle 
VARIABLE MOVING-TO-X 
VARIABLE MOVING-TO-Y
: MOVE-FORWARD 
    \ calculate the coordinates its moving to 
    FORWARD
    MOVING-TO-Y ! 
    MOVING-TO-X !

    \ VALIDATE-COORDINATES of what direction it's trying to move in
    MOVING-TO-X @ MOVING-TO-Y @ VALIDATE-COORDINATES IF 

        \ if IS-PASSABLE 
        MOVING-TO-X @ MOVING-TO-Y @ IS-PASSABLE IF 
            MOVE-TO-TILE
        ELSE 
            FAILED-IMPASSABLE 
        THEN
    ELSE 
        FAILED-INVALID-COORDINATES
    THEN 
;



: SET-TURTLE-DIRECTION ( int -- )
    TURTLES[CURRENT].DIRECTION SWAP !
;

\ rotating the current turtle 
: TURN-RIGHT 
    TURTLES[CURRENT].DIRECTION ENUM-NORTH @ = IF
        ENUM-EAST @ SET-TURTLE-DIRECTION
    THEN 
    TURTLES[CURRENT].DIRECTION ENUM-EAST @ = IF
        ENUM-SOUTH @ SET-TURTLE-DIRECTION
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-SOUTH @ = IF
        ENUM-WEST @ SET-TURTLE-DIRECTION
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-WEST @ = IF
        ENUM-NORTH @ SET-TURTLE-DIRECTION
    THEN
;
: TURN-LEFT 
    TURTLES[CURRENT].DIRECTION ENUM-NORTH @ = IF
        ENUM-WEST @ SET-TURTLE-DIRECTION
    THEN 
    TURTLES[CURRENT].DIRECTION ENUM-EAST @ = IF
        ENUM-NORTH @ SET-TURTLE-DIRECTION
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-SOUTH @ = IF
        ENUM-EAST @ SET-TURTLE-DIRECTION
    THEN
    TURTLES[CURRENT].DIRECTION ENUM-WEST @ = IF
        ENUM-SOUTH @ SET-TURTLE-DIRECTION
    THEN
;


\ examine the thing in front of the current turtle. will count towards discoveries 
: EXAMINE-AHEAD ( -- int )
    \ return the tile id ie the int 
    FORWARD GET-TILE-TYPEID 
;


\ is there something on the tile of the current turtle 
: IS-METAL-ON-CURRENT-TILE ( -- bool )
    TURTLES[CURRENT].COORDS IS-METAL-ON-TILE
;
: IS-FUEL-ON-CURRENT-TILE ( -- bool )
    TURTLES[CURRENT].COORDS IS-FUEL-ON-TILE
;


: PICK-UP-FUEL 
    TURTLES[CURRENT].COORDS REMOVE-ITEM-ON-TILE
    ADD-FUEL
;

: PICK-UP-METAL 
    TURTLES[CURRENT].COORDS REMOVE-ITEM-ON-TILE 
    ADD-METAL 
;

: PICK-UP-ARTIFACT
    TURTLES[CURRENT].COORDS REMOVE-ITEM-ON-TILE
    ADD-DISCOVERY
;

\ pick up the thing it's on right now 
: PICK-UP 
    \ pick up whatever's on the tile of the given coordinates

    \ IF there was fuel on the tile 
    IS-FUEL-ON-CURRENT-TILE IF 
        PICK-UP-FUEL 
    ELSE 
    THEN
    \ IF there was metal on the tile 
    IS-METAL-ON-CURRENT-TILE IF
        PICK-UP-METAL 
    ELSE 
    THEN
    
;
\ if it's fuel or metal, it gets added 

: TURTLE-DIES ( index --  )

    \ set inactive 
    FALSE TURTLES[].ISACTIVE !

    TURTLE-COUNT--
    ." A Turtle perished! " CR
    CHECK-GAMEOVER 
;

\ check if current turtle should die 
: CHECK-TURTLE-DEATH 
    TURTLES[CURRENT].HP @ 0 <= IF 
        TURTLE-DIES 
    ELSE 
    THEN
;

VARIABLE NEWDMGVAL
: DAMAGE-TURTLE

    \ decrement turtle hp 
    TURTLES[CURRENT].HP 1 - NEWDMGVAL ! 
    TURTLES[CURRENT].HP NEWDMGVAL SWAP !

    CHECK-TURTLE-DEATH
;

\ run the logic of a turtle 
: TICK-TURTLE

    \ TODO the counter ticking down to represent corrosive whatever 
    
    CHECK-TURTLE-DEATH
;

