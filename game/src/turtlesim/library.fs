
\ the forth words the player can use in their programs


\ for the jam, just storing TRUE/FALSE has the word been discovered 
\ and a print statement that prints the 

\ parallel by index: 
\ bool variables if they're visible/available to the user or not (i.e. have they been discovered)
\ a print statement of the name 
\ a print statement of the description (wishlist item for after the jam)


: INIT-USER-WORD-DICTIONARY


;



\ a library word stores 
\ the fourth word 
\ a written description to display to the player  
\ a boolean: has it been discovereD?


\ default forth words 

\ game words available from the start 
\ NORTH SOUTH EAST WEST FORWARD BACK LEFT RIGHT MOVE-FORWARD TURN-RIGHT TURN-LEFT EXAMINE-AHEAD PICK-UP SELF-REPLICATE
VARIABLE WINFO-MOVE-FORWARD
VARIABLE WINFO-TURN-LEFT
VARIABLE WINFO-TURN-RIGHT
VARIABLE WINFO-EXAMINE-AHEAD
VARIABLE WINFO-PICK-UP
VARIABLE WINFO-SELF-REPLICATE

\ words the player can earn by discovering them-- these refer to tiles 
\ CLEAR CLIFF CONES and more 
VARIABLE WINFO-T-CLEAR 
VARIABLE WINFO-T-CLIFF 
VARIABLE WINFO-T-ACID 
VARIABLE WINFO-T-ARTIFACT

\ more words 
\ they should have access to the ship variables like FUEL-COUNT and METAL-COUNT 
\ they should be able to tell a drone to construct a new drone 

