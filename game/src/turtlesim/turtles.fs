

VARIABLE TURTLES-COUNT
: ADD-TURTLE-COUNT ;
: DECREMENT-TURTLE-COUNT ;

: TURTLES-LIST ;

\ we'll need to build a game clock for simulating turtles 
: TICK-TURTLES ;

\ a library word stores 
\ the fourth word 
\ a written description 
\ a boolean: has it been discovereD?
\ NORTH SOUTH EAST WEST FORWARD BACK LEFT RIGHT MOVE-FORWARD TURN-RIGHT TURN-LEFT EXAMINE-AHEAD PICK-UP 
\ and then the words that refer to tiles 
\ CLEAR CLIFF and more 

\ a turtle stores 
\ x,y position on the grid 
\ direction facing 
\ hp (pips)
\ a timer indicating how close it is to corroding a pip 
\ 

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

\ pick up the thing it's on right now 
: PICK-UP ;
\ if it's fuel or metal, it gets added 

: PICK-UP-FUEL ;
: PICK-UP-METAL ;

: CREATE-TURTLE 
    SPEND-METAL 
    SPEND-FUEL 
    INIT-TURTLE
;
: INIT-TURTLE ;

: CHECK-TURTLE-DEATH ;
: TURTLE-DIES ;