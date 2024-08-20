
;
; FORTH SOURCE
; Compiles forth source files and provides function hooks
;

%include "../forth/kernelv2/src/forth.asm" as forth
%include "fakeos/terminal.asm" as term
%include "util.asm" as util

%define LOCAL_DICT_END fhead_terminal_bounds
%define LOCAL_DICT_LATEST fhead_terminal_bounds

compile:
	PUSHW BP
	MOVW BP, SP
	
	CALL housekeeping
	
	; glue_words.fs
	PUSHW ptr [glue_words.len]
	PUSHW ptr glue_words
	CALL compile_file
	ADD SP, 8

	PUSHW ptr [gameendings.len]
	PUSHW ptr gameendings
	CALL compile_file
	ADD SP, 8

	PUSHW ptr [ship.len]
	PUSHW ptr ship
	CALL compile_file
	ADD SP, 8

	PUSHW ptr [map.len]
	PUSHW ptr map
	CALL compile_file
	ADD SP, 8

	PUSHW ptr [turtles.len]
	PUSHW ptr turtles
	CALL compile_file
	ADD SP, 8

	PUSHW ptr [library.len]
	PUSHW ptr library
	CALL compile_file
	ADD SP, 8

	PUSHW ptr [playercode.len]
	PUSHW ptr playercode
	CALL compile_file
	ADD SP, 8

	PUSHW ptr [gameloop.len]
	PUSHW ptr gameloop
	CALL compile_file
	ADD SP, 8
	

	POPW BP
	RET

;
; FORTH SOURCE FILE INCLUSIONS
;

glue_words:
.start:
	incbin "glue_words.fs"
.len:
	dp .len - .start

gameendings:
.start:
incbin "turtlesim/gameendings.fs"
.len:
dp .len - .start

ship:
.start:
incbin "turtlesim/ship.fs"
.len:
dp .len - .start

map:
.start:
incbin "turtlesim/map.fs"
.len:
dp .len - .start

turtles:
.start:
incbin "turtlesim/turtles.fs"
.len:
dp .len - .start

library:
.start:
incbin "turtlesim/library.fs"
.len:
dp .len - .start

playercode:
.start:
incbin "turtlesim/playercode.fs"
.len:
dp .len - .start

gameloop:
.start:
incbin "turtlesim/gameloop.fs"
.len:
dp .len - .start



;
; MACHINE CODE FORTH WORDS
;

; TERMINAL-BOUNDS ( min-x min-y max-x max-y -- )
; Set terminal bounds
fhead_terminal_bounds:
	dp 0 ; end of this dict
	db 15
	db "TERMINAL-BOUNDS"
fword_terminal_bounds:
	MOV [term.max_y], AL
	BPOPW D:A
	MOV [term.max_x], AL
	BPOPW D:A
	MOV [term.min_y], AL
	BPOPW D:A
	MOV [term.min_x], AL
	BPOPW D:A
	RET



;
; FUNCTIONS
;

; none compile_file(ptr data, u32 size)
; Compiles a file
compile_file:
	PUSHW BP
	MOVW BP, SP
	
	PUSHW ptr [BP + 8]
	CALL forth.interop_push
	PUSHW ptr [BP + 12]
	CALL forth.interop_push
	PUSHW ptr forth.fword_evaluate
	CALL forth.interop_pcall
	ADD SP, 12
	
	POPW BP
	RET



; none housekeeping()
; do some housekeeping
housekeeping:
	PUSHW BP
	MOVW BP, SP
	PUSHW J:I
	
	; patch local dictionary into forth's dictionary
	MOVW D:A, [forth.uvar_latest]
	MOVW [LOCAL_DICT_END], D:A
	MOVW D:A, LOCAL_DICT_LATEST
	MOVW [forth.uvar_latest], D:A
	
	; disable cursor & clear screen
	CALL forth.kernel_print_inline
	db 13, 0x1B, "[?25l", 0x1B, "[2J", 0x1B, "[H"
	
	; set exception handler
	MOVW D:A, exception_handler
	MOVW [forth.interop_exception_handler], D:A
	
	POPW J:I
	POPW BP
	RET



; Exception handler
exception_handler:
	PUSHW D:A

	; default terminal
	CALL util.unbuffer_screen
	MOV AL, 29
	MOV [term.max_y], AL
	MOV AL, 39
	MOV [term.max_x], AL
	MOV AL, 0
	MOV [term.min_y], AL
	MOV [term.min_x], AL
	
	; disable cursor & clear screen
	CALL forth.kernel_print_inline
	db 23, 0x1B, "[44m", 0x1B, "[37m", 0x1B, "[?25l", 0x1B, "[2J", 0x1B, "[H"
	
	; message
	CALL forth.kernel_print_inline
	db 111, "Uncaught exception during FORTH", 0x0A, "execution. Please do not cause this.", 0x0A, "Press any key to restart.", 0x0A, "Exception code: "

.what1:
	POPW J:I
	MOV B, 0x010A
	CALL forth.kernel_print_number
	
.what2:
	; echo off, ansi off, blocking on
	MOV D, 0
	MOV C, 0b0100
	MOV A, 0x0026
	INT 0x20
	
	; read 1 char
	MOV D, 0
	MOVW B:C, 1
	MOVW J:I, .buf
	MOV A, 0x0022
	INT 0x20
	
	; exit
	MOV A, 0x0000
	INT 0x20

.buf:	resb 2



; hide_words
; Hide words from the dictionary
hide_words:
	PUSHW BP
	MOVW BP, SP
	PUSHW J:I
	PUSHW L:K
	
	; B:C = current address
	; J:I = start address
	; L:K = remaining bytes
	MOVW B:C, hidden_words
	MOVW J:I, B:C
	MOVW L:K, hidden_words.len
	
.loop:
	; find end of the word
	MOV AL, [B:C]
	CMP AL, 0x0A
	JNE .next
	
	PUSHW B:C
	
	; push start address
	PUSHW J:I
	CALL forth.interop_push
	ADD SP, 4
	
	; push length
	SUB C, I	; length = current - start
	SBB B, J
	PUSHW B:C
	CALL forth.interop_push
	ADD SP, 4
	
	; FIND-NAME
	PUSHW ptr forth.fword_find_name
	CALL forth.interop_pcall
	ADD SP, 4
	
	; if zero (not found), skip
	CALL forth.interop_peek
	
	CMP A, 0
	JNZ .found
	CMP D, 0
	JZ .not_found
	
.found:
	; HIDE
	PUSHW ptr forth.fword_hide
	CALL forth.interop_pcall
	ADD SP, 4
	
.not_found:
	POPW B:C
	LEA J:I, [B:C + 1]

.next:
	INC C
	ICC B
	DEC K
	DCC L
	JNZ .loop
	CMP K, 0
	JNZ .loop
	
	POPW L:K
	POPW J:I
	POPW BP
	RET

hidden_words:
	incbin "turtlesim/UserHiddenWords.txt"
.len:
	dp hidden_words.len - hidden_words



; find
; Finds a word
find:
	PUSHW BP
	MOVW BP, SP
	
	; push pointer
	PUSHW ptr [BP + 8]
	CALL forth.interop_push
	ADD SP, 4
	
	; push length
	PUSHW ptr [BP + 12]
	CALL forth.interop_push
	ADD SP, 4
	
	; FIND-NAME
	PUSHW ptr forth.fword_find_name
	CALL forth.interop_pcall
	ADD SP, 4
	
	CALL forth.interop_peek
	CMP D, 0
	JNZ .found
	CMP A, 0
	JZ .not_found
	
.found:
	; NAME>INTERPRET
	PUSHW ptr forth.fword_nametointerpret
	CALL forth.interop_pcall
	ADD SP, 4
	
	; return it
	CALL forth.interop_pop
	
.ret:
	POPW BP
	RET

.not_found:
	CALL forth.kernel_print_inline
	db 16, 0x0A, "Couldn't find: "
	
	PUSHW J:I
	
	MOVW B:C, [BP + 12]
	MOVW J:I, [BP + 8]
	CALL forth.kernel_print_string
	
.not_found_after:
	POPW J:I
	JMP .ret


playerdictionary:	incbin "turtlesim/playerdictionary.txt"
defaultnotes:		incbin "turtlesim/defaultnotes.txt"
