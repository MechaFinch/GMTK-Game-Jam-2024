

32 CONSTANT MAP-SIZE 

\ LIST OF TILE TYPES 
\ 0 CLEAR 
\ 1 IMPASSABLE
\ 2 METAL 
\ 3 FUEL 
\ 4 ARTIFACT
\ 5 ACID

6 CONSTANT TILE-TYPE-COUNT

\ a tile stores 
\ its forth word 
\ its written description 
\ bool: is it passable
\ int: what discovery it is, if any. TODO this should default to like -1 or null or something so we can check for nil
\ sprites: overhead view, side view close, side view far, in-front-of-you view;
    \ SPRITE-TOPDOWN SPRITE-WINDOW-CLOSE SPRITE-WINDOW-FAR SPRITE-WINDOW-CENTER
    \ SPRITE-<filename>
\ functions for when turtles interact with it? or do turtles code their interaction with tiles.

\ tiles are referenced by ID 
\ goes to arrays of information about a tile type 
\ yet again I am using the flyweight pattern for a tile system 


\ more parallel arrays of info 
\ its forth word 
\ 



\ set information about a tile type 
: !TILETYPE ( id word passable isdiscovery -- )

;

\ define the data about the types of tiles 
: DEFINE-TILES 


;





\ directions relative to world 
0 CONSTANT NORTH-X 
1 CONSTANT NORTH-Y

0 CONSTANT SOUTH-X 
-1 CONSTANT SOUTH-Y

1 CONSTANT EAST-X 
0 CONSTANT EAST-Y 

-1 CONSTANT WEST-X 
0 CONSTANT WEST-Y

: NORTH     NORTH-X NORTH-Y ;
: SOUTH     SOUTH-X SOUTH-Y ;
: EAST      EAST-X  EAST-Y ;
: WEST      WEST-X  WEST-Y ;

\ add 2 coordinates 
\ TODO use locals later
VARIABLE +COORDX 
VARIABLE +COORDY
: +COORD ( x1 y1 x2 y2 -- x3 y3 )
    +COORDY !
    +COORDX ! 
    +COORDY +!
    +COORDX +! 
    +COORDX +COORDY
;

\ is a single coordinate within the size of the map?
: VALIDATE-COORDINATE ( x -- bool )
    MAP-SIZE <= IF 
        TRUE
    ELSE 
        FALSE
    THEN 
;

\ are the coordinates within the size of the map?
: VALIDATE-COORDINATES ( x y -- bool )
    VALIDATE-COORDINATE IF 
        VALIDATE-COORDINATE IF 
            TRUE
        ELSE 
            FALSE
        THEN
    ELSE 
        FALSE
    THEN 
;

\ define the map 
\ TODO we can generate this if time allows 
\ stores a fake 2d array (via pointer math) of ints representing the tile type 
VARIABLE MAP MAP-SIZE 2 * CELLS ALLOT

: SET-TILE-TYPEID 

;

: INIT-MAP 

;

\ int representing type index to use looking up data about the type 
: GET-TILE-TYPEID ( x y -- int )
    
;

\ get tile by position, will return null if out of range 
: GET-TILE ( x y -- tile )

;

: IS-PASSABLE ( x y -- bool )
    \ is the tile at the given coordinates passable? returns true if so 
;

\ is there something on the tile of the given coordinates
: IS-METAL-ON-TILE ( x y -- bool )

;
: IS-FUEL-ON-TILE ( x y -- bool )

;


\ for picking things up from a given tile 
: REMOVE-FUEL-TILE ( x y -- )
    \ remove the fuel on a tile of the given coordinates
;
: REMOVE-METAL-TILE ( x y -- )
    \ remove the metal on a tile of the given coordinates
;

\ return the number referring to a sprite according to the dictionary sprite-dictionary.txt given an x y coordinate representing the position of the tile
\ returns 0 if undiscovered, which corresponds to the undiscovered sprite 
: GET-TILE-SPRITE-ID ( x y -- id ) 

;
