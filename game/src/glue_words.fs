\ Glue words
: placeholder ;

: VARIABLE CREATE 1 CELLS ALLOT ;
: CONSTANT CREATE , DOES> @ ;
: >= ( a b -- flag ) 2DUP > -ROT = OR ;

\ Compile editor contents
\ Really just a CATCH around EVALUATE
: compile-editor ( c-addr u -- )
	['] EVALUATE CATCH
	CASE
		0 OF CR ." OK." CR ." Confirm launch to update" CR ." turtle behavior." EXIT ENDOF
		-3 OF CR ." Aborted: Stack overflow!" ENDOF
		-4 OF CR ." Aborted: Stack underflow!" ENDOF
		-5 OF CR ." Aborted: Return stack" CR ." overflow!" ENDOF
		-6 OF CR ." Aborted: Return stack" CR ." underflow!" ENDOF
		-8 OF CR ." Aborted: Dictionary" CR ." overflow!" CR ." You're out of memory, so" CR ." please reset." ENDOF
		-11 OF CR ." Aborted: Out of range." CR ." You probably messed up thecontrol flow stack." ENDOF
		-13 OF CR ." Aborted: Undefined word." ENDOF
		-14 OF CR ." Aborted: Interpreted" CR ." compile-only word." ENDOF
		-29 OF CR ." Aborted: Attempted" CR ." compiler nesting." ENDOF
		-256 OF CR ." Aborted: Locals stack" CR ." overflow!" ENDOF
		-257 OF CR ." Aborted: Locals stack" CR ." underflow!" ENDOF
		-258 OF CR ." Aborted: Malformed locals definition." ENDOF
		-259 OF CR ." Aborted: Too many locals!" ENDOF
		CR ." Aborted: Uncaught exception: " .
	ENDCASE
	CR ." Updating turtle behavior" CR ." is not recommended."
;

VARIABLE user-update-word

\ Commit editor
\ Update turtle update word with that of the editor
: commit-editor ( -- )
	S" UPDATE" FIND-NAME
	?DUP IF
		NAME>INTERPRET
		?DUP IF
			user-update-word !
		THEN
	THEN
;

\ Run user-update-word
: RUN-PLAYER-CODE ( ? -- ? )
	user-update-word @ CATCH
	CASE
		0 OF ENDOF
		-3 OF CR ." Stack overflow!" ENDOF
		-4 OF CR ." Stack underflow!" ENDOF
		-5 OF CR ." Return stack overflow!" ENDOF
		-6 OF CR ." Return stack underflow!" ENDOF
		-8 OF CR ." Dictionary overflow!" CR ." You're out of memory, so" CR ." please reset." ENDOF
		-256 OF CR ." Locals stack overflow!" ENDOF
		-257 OF CR ." Locals stack underflow!" ENDOF
		CR ." Uncaught exception: " .
	ENDCASE
;

: init-glue ( -- )
	['] placeholder user-update-word !
;

init-glue

\
\ PLACEHOLDERS
\

: GET-TILE-SPRITE-ID ( x y -- id ) 2DROP 0 ;
: IS-ENDED-WIN ( -- flag ) FALSE ;
: IS-ENDED-LOSS ( -- flag ) FALSE ;
: PRINT-ENDING-TEXT ( -- ) ;
