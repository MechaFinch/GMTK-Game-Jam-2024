
;
; FORTH SOURCE
; Compiles forth source files and provides function hooks
;

%include "../forth/kernelv2/src/forth.asm" as forth

compile:
	PUSHW BP
	MOVW BP, SP
	
	; hello_world.fs
	PUSHW ptr hello_world
	CALL forth.interop_push
	PUSHW ptr [hello_world.len]
	CALL forth.interop_push
	PUSHW ptr forth.fword_evaluate
	CALL forth.interop_pcall
	ADD SP, 12
	
	POPW BP
	RET

hello_world:
	incbin "hello_world.fs"
.len:
	dp hello_world.len - hello_world
