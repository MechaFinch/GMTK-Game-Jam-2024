
;
; Periodic
; Deals with PIT interrupts
;

time:					dp 0
game_updates_enabled:	db 0
update_in_progress:		db 0

; none init()
; Run initialization
; Requires privilege
init:
	PUSHW BP
	MOVW BP, SP
	
	MOVW D:A, 0
	MOVW [time], D:A
	MOV [game_updates_enabled], AL
	MOV [update_in_progress], AL
	
	; place handler
	MOVW D:A, handler
	MOVW [12 * 4], D:A
	
	POPW BP
	RET

; u32 time_ms()
; Gets the time in ms
time_ms:
	MOVW D:A, [time]
	RET

; none set_updates_enabled(u8 enabled)
; Sets whether game updates are enabled
set_updates_enabled:
	PUSHW BP
	MOVW BP, SP
	
	MOV AL, [BP + 8]
	MOV [game_updates_enabled], AL
	
	POPW BP
	RET

; Interrupt handler
handler:
	PUSHA
	
	MOVW D:A, [time]
	ADD A, 10
	ICC D
	MOVW [time], D:A
	
	POPA
	IRET
