#include <avr/io.h>
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"

/************************************************************************/
/* Configure and initialize IOs                                         */
/************************************************************************/
void init_ios(void)
{	/* Configure input pins */
	io_pin2in(&PORTA, 0, PULL_IO_UP, SENSE_IO_EDGES_BOTH);               // DI0
	io_pin2in(&PORTB, 1, PULL_IO_DOWN, SENSE_IO_EDGES_BOTH);             // DUMMY0_0
	io_pin2in(&PORTB, 2, PULL_IO_DOWN, SENSE_IO_EDGES_BOTH);             // DUMMY1_0
	io_pin2in(&PORTC, 6, PULL_IO_DOWN, SENSE_IO_EDGES_BOTH);             // DUMMY0_1
	io_pin2in(&PORTC, 7, PULL_IO_DOWN, SENSE_IO_EDGES_BOTH);             // DUMMY1_1

	/* Configure input interrupts */
	io_set_int(&PORTA, INT_LEVEL_LOW, 0, (1<<0), false);                 // DI0

	/* Configure output pins */
	io_pin2out(&PORTA, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO0
	io_pin2out(&PORTA, 2, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO1
	io_pin2out(&PORTA, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO2
	io_pin2out(&PORTA, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO3
	io_pin2out(&PORTA, 5, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DO4
	io_pin2out(&PORTA, 6, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // UPDATE_LEDS0
	io_pin2out(&PORTA, 7, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DISABLE_LEDS0
	io_pin2out(&PORTC, 3, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // UPDATE_LEDS1
	io_pin2out(&PORTC, 4, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DISABLE_LEDS1
	io_pin2out(&PORTB, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DEMO_MODE0
	io_pin2out(&PORTC, 5, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DEMO_MODE1
	io_pin2out(&PORTB, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DEMO_MODE0
	io_pin2out(&PORTC, 5, OUT_IO_DIGITAL, IN_EN_IO_EN);                  // DEMO_MODE1

	/* Initialize output pins */
	clr_DO0;
	clr_DO1;
	clr_DO2;
	clr_DO3;
	clr_DO4;
	clr_UPDATE_LEDS0;
	clr_DISABLE_LEDS0;
	clr_UPDATE_LEDS1;
	clr_DISABLE_LEDS1;
	clr_DEMO_MODE0;
	clr_DEMO_MODE1;
	clr_DEMO_MODE0;
	clr_DEMO_MODE1;
}

/************************************************************************/
/* Registers' stuff                                                     */
/************************************************************************/
AppRegs app_regs;

uint8_t app_regs_type[] = {
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8,
	TYPE_U8
};

uint16_t app_regs_n_elements[] = {
	1,
	1,
	192,
	96,
	96,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1,
	1
};

uint8_t *app_regs_pointer[] = {
	(uint8_t*)(&app_regs.REG_LEDS_STATUS),
	(uint8_t*)(&app_regs.REG_LEDS_ON_BUS),
	(uint8_t*)(app_regs.REG_COLOR_ARRAY),
	(uint8_t*)(app_regs.REG_COLOR_ARRAY_BUS0),
	(uint8_t*)(app_regs.REG_COLOR_ARRAY_BUS1),
	(uint8_t*)(&app_regs.REG_RESERVED0),
	(uint8_t*)(&app_regs.REG_RESERVED1),
	(uint8_t*)(&app_regs.REG_DI0_CONF),
	(uint8_t*)(&app_regs.REG_DO0_CONF),
	(uint8_t*)(&app_regs.REG_DO1_CONF),
	(uint8_t*)(&app_regs.REG_RESERVED2),
	(uint8_t*)(&app_regs.REG_RESERVED3),
	(uint8_t*)(&app_regs.REG_INPUTS_STATE),
	(uint8_t*)(&app_regs.REG_OUTPUTS_SET),
	(uint8_t*)(&app_regs.REG_OUTPUTS_CLEAR),
	(uint8_t*)(&app_regs.REG_OUTPUTS_TOGGLE),
	(uint8_t*)(&app_regs.REG_OUTPUTS_OUT),
	(uint8_t*)(&app_regs.REG_RESERVED4),
	(uint8_t*)(&app_regs.REG_RESERVED5),
	(uint8_t*)(&app_regs.REG_EVNT_ENABLE)
};