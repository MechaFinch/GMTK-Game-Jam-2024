
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
\: CURRENT-TURTLE ( -- currentturtleaddr )
\    CURRENT-TURTLE-INDEX CELLS TURTLES-LIST +
\;

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
\VARIABLE TURTLES-LIST

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
    CURRENT-TURTLE-INDEX TURTLES[].ISACTIVE
;
: TURTLES[CURRENT].HP ( -- hp )
    CURRENT-TURTLE-INDEX TURTLES[].HP
;
: TURTLES[CURRENT].X ( -- x )
    CURRENT-TURTLE-INDEX TURTLES[].X
;
: TURTLES[CURRENT].Y ( -- y)
    CURRENT-TURTLE-INDEX TURTLES[].Y
;
: TURTLES[CURRENT].COORDS ( -- x y )
    CURRENT-TURTLE-INDEX TURTLES[].COORDS
;
: TURTLES[CURRENT].DIRECTION ( -- direction )
    CURRENT-TURTLE-INDEX TURTLES[].DIRECTION
;
: TURTLES[CURRENT].COUNTDOWN ( -- countdown )
    CURRENT-TURTLE-INDEX TURTLES[].COUNTDOWN
;

\ inits an INACTIVE turtle at the current index with default starting values 
: INIT-CURRENT-TURTLE-EMPTY 
    FALSE TURTLES[CURRENT].ISACTIVE ! 
    STARTING-HP TURTLES[CURRENT].HP ! 
    STARTING-X TURTLES[CURRENT].X ! 
    STARTING-Y TURTLES[CURRENT].Y !
    STARTING-DIRECTION TURTLES[CURRENT].DIRECTION !
    STARTING-COUNTDOWN TURTLES[CURRENT].COUNTDOWN !
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
    TURTLE-COUNT MAX-TURTLES < IF 
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
    CURRENT-TURTLE-INDEX CURRENT-TURTLE-INDEX-STORAGE !
    TURTLE-INDEX-CREATING CURRENT-TURTLE-INDEX !
    INIT-CURRENT-TURTLE-EMPTY
    
    \ increment the turtle counter 
    TURTLE-COUNT++

    \ set turtle active based on variable passed in 
    TRUE TURTLES[CURRENT].ISACTIVE !
    
    \ reset current turtle counter to what it was at before not the new turtle 
    CURRENT-TURTLE-INDEX-STORAGE CURRENT-TURTLE-INDEX !
;

: FAILED-CREATE-TURTLE-METAL
    ."Not enough METAL! " CR
;
: FAILED-CREATE-TURTLE-FUEL
    ."Not enough FUEL! " CR
;
: FAILED-CREATE-TURTLE-COUNTLIMIT
    ."Maximum probes reached! " CR
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
: SELF-REPLICATE
    TRY-CREATE-TURTLE
;


\ directions relative to turtle facing 
: FORWARD ;
: BACK ;
: LEFT ;
: RIGHT ;


: SET-TURTLE-POSITION 

;

\: EXECUTE-TILE-LOGIC-OVERLAP
    \ do whatever the tile does when the player lands on the tile 
\;
\: EXECUTE-TILE-LOGIC-EXAMINE
    \ do whatever the tile does when the player examines it
\;
\ TODO more of these 


\ moving the turtle 
: MOVE-FORWARD 
    \ VALIDATE-COORDINATES of what direction it's trying to move in 
    \ if IS-PASSABLE 
    \ SET-POSITION 

;


\ rotating the current turtle 
: TURN-RIGHT 

;
: TURN-LEFT 

;

: SET-TURTLE-DIRECTION 

;

\ examine the thing in front of the current turtle. will count towards discoveries 
: EXAMINE-AHEAD 

;


\ is there something on the tile of the current turtle 
: IS-METAL-ON-CURRENT-TILE ( -- bool )

;
: IS-FUEL-ON-CURRENT-TILE ( -- bool )

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

: TURTLE-DIES 
    \ TODO 
    CHECK-GAMEOVER 
;

: CHECK-TURTLE-DEATH 

;

: DAMAGE-TURTLE
    \ decrement turtle hp 
    CHECK-TURTLE-DEATH
;

\ run the logic of a turtle 
: TICK-TURTLE
    \
;

