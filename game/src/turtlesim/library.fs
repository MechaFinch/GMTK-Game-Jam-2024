
\ the forth words the player can use in their programs

\ a library word stores 
\ the fourth word 
\ a written description to display to the player  
\ a boolean: has it been discovereD?


\ default forth words 

\ game words available from the start 
\ NORTH SOUTH EAST WEST FORWARD BACK LEFT RIGHT MOVE-FORWARD TURN-RIGHT TURN-LEFT EXAMINE-AHEAD PICK-UP 
VARIABLE WINFO-MOVE-FORWARD
VARIABLE WINFO-TURN-LEFT
VARIABLE WINFO-TURN-RIGHT
VARIABLE WINFO-EXAMINE-AHEAD
VARIABLE WINFO-PICK-UP

\ words the player can earn by discovering them-- these refer to tiles 
\ CLEAR CLIFF CONES and more 
VARIABLE WINFO-T-CLEAR 
VARIABLE WINFO-T-CLIFF 
VARIABLE WINFO-T-CONES 

