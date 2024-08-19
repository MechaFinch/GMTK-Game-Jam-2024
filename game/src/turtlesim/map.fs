


\ a tile stores 
\ its forth word 
\ its written description 
\ bool: is it passable
\ int: what discovery it is, if any. TODO this should default to like -1 or null or something so we can check for nil
\ sprites: overhead view, side view close, side view far, in-front-of-you view;
    \ SPRITE-TOPDOWN SPRITE-WINDOW-CLOSE SPRITE-WINDOW-FAR SPRITE-WINDOW-CENTER
    \ SPRITE-<filename>
\ functions for when turtles interact with it? or do turtles code their interaction with tiles.


\ define the data about the types of tiles 
: DEFINE-TILES ;


\ a map is a grid of forth words that correspond to their data type 
\ e.g. 
\ CLIFF CLIFF CLIFF 
\ CLIFF CLEAR CLIFF 
\ CLIFF METAL CLIFF
\ for monospacing all words are 5 letters 
\ CLEAR METAL ROVER/DRONE CLIFF FUEL_ DSCV0-9 CLONE SPIKE EYES_ WATER ACIDW ACIDL 
\ and they'll store a name that gets printed 

\ TODO a separate grid of turtles, which can occupy grid spaces but not run into each other? 

8 CONSTANT MAP-SIZE 


\ creature mechanic ideas 
\ kills you when picked up but fine otherwise 
\ kills you when examined 
\ kills you when scanned 
\ kills you when walked on 
\ kills you when walked next to but not on 
\ kills you when walked next to and on 
\ kills you if you look at it but fine otherwise 
\ a ghost turtle clone thats not yours but copies your code 


\ define the map 
\ TODO we can generate this if time allows 
: MAP ;

\ directions relative to world 
0 CONSTANT NORTH-X 
1 CONSTANT NORTH-Y

0 CONSTANT SOUTH-X 
-1 CONSTANT SOUTH-Y

1 CONSTANT EAST-X 
0 CONSTANT EAST-Y 

-1 CONSTANT WEST-X 
0 CONSTANT WEST-Y

: NORTH NORTH-X NORTH-Y ;
: SOUTH SOUTH-X SOUTH-Y ;
: EAST EAST-X EAST-Y ;
: WEST WEST-X WEST-Y ;

\ are the coordinates within the size of the map?
: VALIDATE-COORDINATES 

;


\ get tile by position 
: GET-TILE ;



\ for picking things up from a given tile 
: REMOVE-FUEL-TILE 
    \ remove the fuel on a tile of the given coordinates
;
: REMOVE-METAL-TILE 
    \ remove the metal on a tile of the given coordinates
;

\ return the number referring to a sprite according to the dictionary sprite-dictionary.txt given an x y coordinate representing the position of the tile
\ returns 0 if undiscovered, which corresponds to the undiscovered sprite 
: GET-TILE-SPRITE-ID ( x y -- id ) 

;
