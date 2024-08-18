
;
; FORTH SOURCE
; Compiles forth source files and provides function hooks
;

%include "../forth/kernelv2/src/forth.asm" as forth
%include "fakeos/terminal.asm" as term

%define LOCAL_DICT_END fhead_terminal_bounds
%define LOCAL_DICT_LATEST fhead_terminal_bounds

compile:
	PUSHW BP
	MOVW BP, SP
	
	CALL housekeeping
	
	; hello_world.fs
	PUSHW ptr [hello_world.len]
	PUSHW ptr hello_world
	CALL compile_file
	ADD SP, 8
	
	POPW BP
	RET

;
; FORTH SOURCE FILE INCLUSIONS
;

hello_world:
	incbin "hello_world.fs"
.len:
	dp hello_world.len - hello_world



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
	
	POPW J:I
	POPW BP
	RET
