
;
; Terminal Utilities
;

%include "../forth/kernelv2/src/forth.asm" as forth
%include "fakeos/terminal.asm" as term

; none set_term_bounds(u8 min_x, u8 min_y, u8 max_x, u8 max_y)
set_term_bounds:
	PUSHW BP
	MOVW BP, SP
	
	MOV AL, [BP + 8]
	MOV [term.min_x], AL
	MOV AL, [BP + 9]
	MOV [term.min_y], AL
	MOV AL, [BP + 10]
	MOV [term.max_x], AL
	MOV AL, [BP + 11]
	MOV [term.max_y], AL
	
	POPW BP
	RET

; none clear_term()
; clears the terminal
clear_term:
	PUSHW BP
	MOVW BP, SP
	PUSHW J:I
	
	CALL forth.kernel_print_inline
	db 7, 0x1B, "[2J", 0x1B, "[H"
	
	POPW J:I
	POPW BP
	RET

; none set_pos(u8 x, u8 y)
; set cursor position
set_pos:
	PUSHW BP
	MOVW BP, SP
	PUSHW J:I
	
	CALL forth.kernel_print_inline
	db 2, 0x1B, "["
	
	MOV B, 0x000A
	MOVZ I, [BP + 9]
	MOV J, 0
	CALL forth.kernel_print_number
	
	MOV CL, ';'
	CALL forth.kernel_print_char
	
	MOV B, 0x000A
	MOVZ I, [BP + 8]
	MOV J, 0
	CALL forth.kernel_print_number
	
	MOV CL, 'f'
	CALL forth.kernel_print_char
	
	POPW J:I
	POPW BP
	RET

; none set_pos_direct(u8 x, u8 y)
set_pos_direct:
	PUSHW BP
	MOVW BP, SP
	
	MOV A, [BP + 8]
	MOV [term.cursor_x], AL
	MOV [term.cursor_y], AH
	
	POPW BP
	RET

; u16 get_pos()
; Gets cursor pos
; AH = y
; AL = x
get_pos:
	MOV AL, [term.cursor_x]
	MOV AH, [term.cursor_y]
	RET

; none print_esc(char c)
; print c
print_char:
	PUSHW BP
	MOVW BP, SP
	PUSHW J:I
	
	MOV CL, [BP + 8]
	CALL forth.kernel_print_char
	
	POPW J:I
	POPW BP
	RET

; none set_fgc(u8 c)
; sets foreground color
set_fgc:
	PUSHW BP
	MOVW BP, SP
	
	MOV AL, [BP + 8]
	MOV [term.color_foreground], AL
	
	POPW BP
	RET

; none set_bgc(u8 c)
; sets background color
set_bgc:
	PUSHW BP
	MOVW BP, SP
	
	MOV AL, [BP + 8]
	MOV [term.color_background], AL
	
	POPW BP
	RET

; none set_color(u8 fgc, u8 bgc)
; sets text color
set_color:
	PUSHW BP
	MOVW BP, SP
	
	MOV A, [BP + 8]
	MOV [term.color_foreground], AL
	MOV [term.color_background], AH
	
	POPW BP
	RET
