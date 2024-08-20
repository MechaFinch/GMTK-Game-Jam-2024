

20 CONSTANT MAP-SIZE \ 20x20 tiles rendered

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



\ LIST OF TILE TYPES 
0 CONSTANT TID-CLEAR 
1 CONSTANT TID-IMPASSABLE 
2 CONSTANT TID-METAL 
3 CONSTANT TID-FUEL 
4 CONSTANT TID-ARTIFACT 
5 CONSTANT TID-ACID 

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

VARIABLE T-SPRITEIDS TILE-TYPE-COUNT CELLS ALLOT 
VARIABLE T-WORDS     TILE-TYPE-COUNT CELLS ALLOT 
VARIABLE T-PASSABLE TILE-TYPE-COUNT CELLS ALLOT 

: GET-TYPE-SPRITEID ( type -- int )
     CELLS T-SPRITEIDS + ;
: GET-TYPE-WORD ( type -- int )
     CELLS T-WORDS + ;
: GET-TYPE-PASSABLE ( type -- bool )
     CELLS T-PASSABLE + ;


\ set information about a tile type 
VARIABLE S-T-SPRITEID 
VARIABLE S-T-PASSABLE 
VARIABLE S-T-INDEX
: SET-TILETYPE ( index passable spriteid -- ) \ we can do IsDiscovery later, for now just checks type like metal/fuel

    S-T-SPRITEID !
    S-T-PASSABLE !
    S-T-INDEX ! 

    S-T-INDEX CELLS T-SPRITEIDS + 
    S-T-SPRITEID SWAP ! 

    S-T-INDEX CELLS T-WORDS + 
    S-T-INDEX SWAP ! 

    S-T-INDEX CELLS T-PASSABLE + 
    S-T-PASSABLE SWAP !
;

\ define the data about the types of tiles 
: DEFINE-TILES 
    TID-CLEAR     TRUE  TSPR-CLEAR          SET-TILETYPE 
    TID-PASSABLE  FALSE TSPR-PASSABLE    SET-TILETYPE 
    TID-METAL     TRUE  TSPR-METAL          SET-TILETYPE
    TID-FUEL      TRUE  TSPR-FUEL           SET-TILETYPE
    TID-ARTIFACT  TRUE  TSPR-ARTIFACT       SET-TILETYPE
    TID-ACID      TRUE  TSPR-ACID           SET-TILETYPE
;


\ define the map 
\ TODO we can generate this if time allows 

\ stores a fake 2d array (via pointer math) of ints representing the tile type 
VARIABLE MAP MAP-SIZE 2 * CELLS ALLOT

\ parallel array storing if the tile has been discovered 
VARIABLE MAP-DISCOVERED MAP-SIZE 2 * CELLS ALLOT 

\ 2d array x y to 1d array x
VARIABLE 2D1DV
: 2D-TO-1D ( x y -- address )
    CELLS * MAP-SIZE * 2D1DV ! 
    CELLS * 2D1DV +!
    2D1DV
;

\ int representing type index to use looking up data about the type 
: GET-TILE-TYPEID ( x y -- int )
    2D-TO-1D MAP +
;
VARIABLE STIDV
: SET-TILE-TYPEID ( x y id -- )
    STIDV !
    2D-TO-1D
    MAP +
    STIDV SWAP !
;

\ TODO procgen 
: INIT-MAP 
    
    \ fill with impassable to start 
    MAP-SIZE 0 DO 
        I
        MAP-SIZE 0 DO 
            DUP 
            I TID-IMPASSABLE SET-TILE-TYPEID
        LOOP
    LOOP 

    \ fill interior with a mix of stuff 
    MAP-SIZE 1 - 1 DO 
        I
        MAP-SIZE 1 - 1 DO 
            DUP 
            I 
            TID-CLEAR SET-TILE-TYPEID
        LOOP
    LOOP 

    \ set artifacts 
    3 13 TID-ARTIFACT SET-TILE-TYPEID 
    11 14 TID-ARTIFACT SET-TILE-TYPEID 
    7 7 TID-ARTIFACT SET-TILE-TYPEID
    18 9 TID-ARTIFACT SET-TILE-TYPEID
;


\ discover this tile ie show it on the map ie set discovered  MAP-DISCOVERED 
: DISCOVER-TILE ( x y -- )
    2D-TO-1D
    MAP-DISCOVERED +
    TRUE SWAP !
;
: IS-DISCOVERED ( x y -- bool )
    2D-TO-1D MAP-DISCOVERED +
;

: IS-PASSABLE ( x y -- bool )
    \ is the tile at the given coordinates passable? returns true if so
    GET-TILE-TYPEID GET-TYPE-PASSABLE
;

\ is there something on the tile of the given coordinates
: IS-METAL-ON-TILE ( x y -- bool )
    GET-TILE-TYPEID TID-METAL = IF 
        TRUE 
    ELSE 
        FALSE 
    THEN
;
: IS-FUEL-ON-TILE ( x y -- bool )
    GET-TILE-TYPEID TID-FUEL = IF 
        TRUE 
    ELSE 
        FALSE 
    THEN
;
: IS-ARTIFACT ( x y -- bool )
    GET-TILE-TYPEID TID-ARTIFACT = IF 
        TRUE 
    ELSE 
        FALSE 
    THEN
;

\ for removing metal, fuel, artifacts
: REMOVE-ITEM-ON-TILE ( x y -- )
    TID-CLEAR SET-TILE-TYPEID
;


\ return the number referring to a sprite according to the dictionary sprite-dictionary.txt given an x y coordinate representing the position of the tile
\ returns 0 if undiscovered, which corresponds to the undiscovered sprite 
VARIABLE GTSID-X 
VARIABLE GTSID-Y
: GET-TILE-SPRITE-ID ( x y -- id ) 

    GTSID-Y ! 
    GTSID-X ! 

    \ first get if the tile has been discovered 
    GTSID-X GTSID-Y IS-DISCOVERED IF 
        GTSID-X GTSID-Y GET-TILE-TYPEID GET-TYPE-SPRITEID
    ELSE 
        TSPR-UNDISCOVERED
    THEN
;
