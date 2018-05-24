#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"

#include <string.h>

bool update_rgbs = false;
bool slave_is_ready = true;

//uint8_t temporary_rgb_arrays_bus0[96];
//uint8_t temporary_rgb_arrays_bus1[96];

void update_bus (void);

/************************************************************************/
/* Create pointers to functions                                         */
/************************************************************************/
extern AppRegs app_regs;

void (*app_func_rd_pointer[])(void) = {
	&app_read_REG_LEDS_STATUS,
	&app_read_REG_LEDS_ON_BUS,
	&app_read_REG_COLOR_ARRAY,
	&app_read_REG_COLOR_ARRAY_BUS0,
	&app_read_REG_COLOR_ARRAY_BUS1,
	&app_read_REG_RESERVED0,
	&app_read_REG_RESERVED1,
	&app_read_REG_DI0_CONF,
	&app_read_REG_DO0_CONF,
	&app_read_REG_DO1_CONF,
	&app_read_REG_RESERVED2,
	&app_read_REG_RESERVED3,
	&app_read_REG_INPUTS_STATE,
	&app_read_REG_OUTPUTS_SET,
	&app_read_REG_OUTPUTS_CLEAR,
	&app_read_REG_OUTPUTS_TOGGLE,
	&app_read_REG_OUTPUTS_OUT,
	&app_read_REG_RESERVED4,
	&app_read_REG_RESERVED5,
	&app_read_REG_EVNT_ENABLE
};

bool (*app_func_wr_pointer[])(void*) = {
	&app_write_REG_LEDS_STATUS,
	&app_write_REG_LEDS_ON_BUS,
	&app_write_REG_COLOR_ARRAY,
	&app_write_REG_COLOR_ARRAY_BUS0,
	&app_write_REG_COLOR_ARRAY_BUS1,
	&app_write_REG_RESERVED0,
	&app_write_REG_RESERVED1,
	&app_write_REG_DI0_CONF,
	&app_write_REG_DO0_CONF,
	&app_write_REG_DO1_CONF,
	&app_write_REG_RESERVED2,
	&app_write_REG_RESERVED3,
	&app_write_REG_INPUTS_STATE,
	&app_write_REG_OUTPUTS_SET,
	&app_write_REG_OUTPUTS_CLEAR,
	&app_write_REG_OUTPUTS_TOGGLE,
	&app_write_REG_OUTPUTS_OUT,
	&app_write_REG_RESERVED4,
	&app_write_REG_RESERVED5,
	&app_write_REG_EVNT_ENABLE
};


/************************************************************************/
/* REG_LEDS_STATUS                                                      */
/************************************************************************/
void app_read_REG_LEDS_STATUS(void)
{
	//app_regs.REG_LEDS_STATUS = 0;

}

bool app_write_REG_LEDS_STATUS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_LEDS_STATUS = reg;
	return true;
}


/************************************************************************/
/* REG_LEDS_ON_BUS                                                      */
/************************************************************************/
void app_read_REG_LEDS_ON_BUS(void)
{
	//app_regs.REG_LEDS_ON_BUS = 0;

}

bool app_write_REG_LEDS_ON_BUS(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_LEDS_ON_BUS = reg;
	return true;
}


/************************************************************************/
/* REG_COLOR_ARRAY                                                      */
/************************************************************************/
// This register is an array with 192 positions
//volatile uint8_t the_checksum;
void app_read_REG_COLOR_ARRAY(void) {}
bool app_write_REG_COLOR_ARRAY(void *a)
{
	uint8_t *reg = ((uint8_t*)a);
   
   memcpy(app_regs.REG_COLOR_ARRAY, reg, 192);
   //memcpy(app_regs.REG_COLOR_ARRAY_BUS0, reg, 96);
   //memcpy(app_regs.REG_COLOR_ARRAY_BUS1, reg+96, 96);
   
   //memcpy(temporary_rgb_arrays_bus0, app_regs.REG_COLOR_ARRAY_BUS0, 96);
   //memcpy(temporary_rgb_arrays_bus1, app_regs.REG_COLOR_ARRAY_BUS1, 96);
   
   //temporary_rgb_arrays
   //the_checksum = reg[192];
   update_bus();
   
   update_rgbs = true;
   
	return true;
}


/************************************************************************/
/* REG_COLOR_ARRAY_BUS0                                                 */
/************************************************************************/
// This register is an array with 96 positions
void app_read_REG_COLOR_ARRAY_BUS0(void) {}
bool app_write_REG_COLOR_ARRAY_BUS0(void *a)
{
	uint8_t *reg = ((uint8_t*)a);

   memcpy(app_regs.REG_COLOR_ARRAY, reg, 96);
   memcpy(app_regs.REG_COLOR_ARRAY_BUS0, reg, 96);
   
   //memcpy(temporary_rgb_arrays_bus0, app_regs.REG_COLOR_ARRAY_BUS0, 96);
   
   update_rgbs = true;
   
	return true;
}


/************************************************************************/
/* REG_COLOR_ARRAY_BUS1                                                 */
/************************************************************************/
// This register is an array with 96 positions
void app_read_REG_COLOR_ARRAY_BUS1(void) {}
bool app_write_REG_COLOR_ARRAY_BUS1(void *a)
{
	uint8_t *reg = ((uint8_t*)a);

	memcpy(app_regs.REG_COLOR_ARRAY+96, reg, 96);
	memcpy(app_regs.REG_COLOR_ARRAY_BUS1, reg, 96);   
   
   //memcpy(temporary_rgb_arrays_bus1, app_regs.REG_COLOR_ARRAY_BUS1, 96);
	
	update_rgbs = true;
   
	return true;
}


/************************************************************************/
/* REG_RESERVED0                                                        */
/************************************************************************/
void app_read_REG_RESERVED0(void)
{
	//app_regs.REG_RESERVED0 = 0;

}

bool app_write_REG_RESERVED0(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED0 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED1                                                        */
/************************************************************************/
void app_read_REG_RESERVED1(void)
{
	//app_regs.REG_RESERVED1 = 0;

}

bool app_write_REG_RESERVED1(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED1 = reg;
	return true;
}


/************************************************************************/
/* REG_DI0_CONF                                                         */
/************************************************************************/
void app_read_REG_DI0_CONF(void)
{
	//app_regs.REG_DI0_CONF = 0;

}

bool app_write_REG_DI0_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DI0_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_CONF                                                         */
/************************************************************************/
void app_read_REG_DO0_CONF(void)
{
	//app_regs.REG_DO0_CONF = 0;

}

bool app_write_REG_DO0_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO0_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_DO1_CONF                                                         */
/************************************************************************/
void app_read_REG_DO1_CONF(void)
{
	//app_regs.REG_DO1_CONF = 0;

}

bool app_write_REG_DO1_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DO1_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED2                                                        */
/************************************************************************/
void app_read_REG_RESERVED2(void)
{
	//app_regs.REG_RESERVED2 = 0;

}

bool app_write_REG_RESERVED2(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED2 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED3                                                        */
/************************************************************************/
void app_read_REG_RESERVED3(void)
{
	//app_regs.REG_RESERVED3 = 0;

}

bool app_write_REG_RESERVED3(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED3 = reg;
	return true;
}


/************************************************************************/
/* REG_INPUTS_STATE                                                     */
/************************************************************************/
void app_read_REG_INPUTS_STATE(void)
{
	//app_regs.REG_INPUTS_STATE = 0;

}

bool app_write_REG_INPUTS_STATE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_INPUTS_STATE = reg;
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_SET                                                      */
/************************************************************************/
void app_read_REG_OUTPUTS_SET(void)
{
	//app_regs.REG_OUTPUTS_SET = 0;

}

bool app_write_REG_OUTPUTS_SET(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_OUTPUTS_SET = reg;
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_CLEAR                                                    */
/************************************************************************/
void app_read_REG_OUTPUTS_CLEAR(void)
{
	//app_regs.REG_OUTPUTS_CLEAR = 0;

}

bool app_write_REG_OUTPUTS_CLEAR(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_OUTPUTS_CLEAR = reg;
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_TOGGLE                                                   */
/************************************************************************/
void app_read_REG_OUTPUTS_TOGGLE(void)
{
	//app_regs.REG_OUTPUTS_TOGGLE = 0;

}

bool app_write_REG_OUTPUTS_TOGGLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_OUTPUTS_TOGGLE = reg;
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_OUT                                                      */
/************************************************************************/
void app_read_REG_OUTPUTS_OUT(void)
{
	//app_regs.REG_OUTPUTS_OUT = 0;

}

bool app_write_REG_OUTPUTS_OUT(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_OUTPUTS_OUT = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED4                                                        */
/************************************************************************/
void app_read_REG_RESERVED4(void)
{
	//app_regs.REG_RESERVED4 = 0;

}

bool app_write_REG_RESERVED4(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED4 = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED5                                                        */
/************************************************************************/
void app_read_REG_RESERVED5(void)
{
	//app_regs.REG_RESERVED5 = 0;

}

bool app_write_REG_RESERVED5(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_RESERVED5 = reg;
	return true;
}


/************************************************************************/
/* REG_EVNT_ENABLE                                                      */
/************************************************************************/
void app_read_REG_EVNT_ENABLE(void)
{
	//app_regs.REG_EVNT_ENABLE = 0;

}

bool app_write_REG_EVNT_ENABLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_EVNT_ENABLE = reg;
	return true;
}