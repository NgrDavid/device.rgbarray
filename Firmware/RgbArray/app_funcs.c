#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"

#include <string.h>  // For the memcpy()

void update_bus (void);
void start_demo_mode (void);
void stop_demo_mode (void);

bool start_array_pulses = false;

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
	&app_read_REG_LATCH_NEXT_UPDATE,
	&app_read_REG_INPUTS_STATE,
	&app_read_REG_OUTPUTS_SET,
	&app_read_REG_OUTPUTS_CLEAR,
	&app_read_REG_OUTPUTS_TOGGLE,
	&app_read_REG_OUTPUTS_OUT,
	&app_read_REG_PULSE_PERIOD,
	&app_read_REG_PULSE_COUNT,
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
	&app_write_REG_LATCH_NEXT_UPDATE,
	&app_write_REG_INPUTS_STATE,
	&app_write_REG_OUTPUTS_SET,
	&app_write_REG_OUTPUTS_CLEAR,
	&app_write_REG_OUTPUTS_TOGGLE,
	&app_write_REG_OUTPUTS_OUT,
	&app_write_REG_PULSE_PERIOD,
	&app_write_REG_PULSE_COUNT,
	&app_write_REG_EVNT_ENABLE
};


/************************************************************************/
/* REG_LEDS_STATUS                                                      */
/************************************************************************/
void app_read_REG_LEDS_STATUS(void)
{
	app_regs.REG_LEDS_STATUS = 0;
}

bool app_write_REG_LEDS_STATUS(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if ((reg != B_RGB_ON) && (reg != B_RGB_OFF) && (reg != B_DEMO_MODE_ON) && (reg != B_DEMO_MODE_OFF))
      return false;
   
   if (reg & B_RGB_ON)
   {
      stop_demo_mode();
      set_UPDATE_LEDS0;
      set_UPDATE_LEDS1;
      clr_UPDATE_LEDS0;
      clr_UPDATE_LEDS1;
   }
      
   if (reg & B_RGB_OFF)
   {
      stop_demo_mode();
      set_DISABLE_LEDS0;
      set_DISABLE_LEDS1;
      clr_DISABLE_LEDS0;
      clr_DISABLE_LEDS1;
   }
   
   if (reg & B_DEMO_MODE_ON)
   {
      start_demo_mode();
   }
   
   if (reg & B_DEMO_MODE_OFF)
   {
      stop_demo_mode();
   }

	app_regs.REG_LEDS_STATUS = reg;
	return true;
}


/************************************************************************/
/* REG_LEDS_ON_BUS                                                      */
/************************************************************************/
void app_read_REG_LEDS_ON_BUS(void) {}
bool app_write_REG_LEDS_ON_BUS(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg < 1 || reg > 32)
      return false;

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
   update_bus();
   
	return true;
}


/************************************************************************/
/* REG_COLOR_ARRAY_BUS0                                                 */
/************************************************************************/
// This register is an array with 96 positions
void app_read_REG_COLOR_ARRAY_BUS0(void)
{
   memcpy(app_regs.REG_COLOR_ARRAY_BUS0, app_regs.REG_COLOR_ARRAY, 96);
}
bool app_write_REG_COLOR_ARRAY_BUS0(void *a)
{
	uint8_t *reg = ((uint8_t*)a);

   memcpy(app_regs.REG_COLOR_ARRAY, reg, 96);
   update_bus();
   
	return true;
}


/************************************************************************/
/* REG_COLOR_ARRAY_BUS1                                                 */
/************************************************************************/
// This register is an array with 96 positions
void app_read_REG_COLOR_ARRAY_BUS1(void)
{
   memcpy(app_regs.REG_COLOR_ARRAY_BUS1, app_regs.REG_COLOR_ARRAY+96, 96);
}
bool app_write_REG_COLOR_ARRAY_BUS1(void *a)
{
	uint8_t *reg = ((uint8_t*)a);

	memcpy(app_regs.REG_COLOR_ARRAY+96, reg, 96);
	update_bus();
   
	return true;
}


/************************************************************************/
/* REG_RESERVED0                                                        */
/************************************************************************/
void app_read_REG_RESERVED0(void) {}
bool app_write_REG_RESERVED0(void *a)
{
	app_regs.REG_RESERVED0 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_RESERVED1                                                        */
/************************************************************************/
void app_read_REG_RESERVED1(void) {}
bool app_write_REG_RESERVED1(void *a)
{
	app_regs.REG_RESERVED1 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_DI0_CONF                                                         */
/************************************************************************/
void app_read_REG_DI0_CONF(void) {}
bool app_write_REG_DI0_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg & ~MSK_DI0_SEL)
      return false;
   
   if (reg == GM_DI0_HIGH_RGBS_ON)
   {
      if (!read_DI0)
      {
         stop_demo_mode();
         set_DISABLE_LEDS0;
         set_DISABLE_LEDS1;
         clr_DISABLE_LEDS0;
         clr_DISABLE_LEDS1;
      }
      else
      {
         stop_demo_mode();
         set_UPDATE_LEDS0;
         set_UPDATE_LEDS1;
         clr_UPDATE_LEDS0;
         clr_UPDATE_LEDS1;
      }
   }

	app_regs.REG_DI0_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_DO0_CONF                                                         */
/************************************************************************/
void app_read_REG_DO0_CONF(void) {}
bool app_write_REG_DO0_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg & ~MSK_DO_SEL)
      return false;
   
   if ((reg == GM_DO_PULSE_WHEN_UPDATED) || (reg == GM_DO_PULSE_WHEN_ARRAY_LOADED))
      clr_DO0;

	app_regs.REG_DO0_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_DO1_CONF                                                         */
/************************************************************************/
void app_read_REG_DO1_CONF(void) {}
bool app_write_REG_DO1_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg & ~MSK_DO_SEL)
      return false;
      
   if ((reg == GM_DO_PULSE_WHEN_UPDATED) || (reg == GM_DO_PULSE_WHEN_ARRAY_LOADED))
      clr_DO1;

	app_regs.REG_DO1_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_RESERVED2                                                        */
/************************************************************************/
void app_read_REG_RESERVED2(void) {}
bool app_write_REG_RESERVED2(void *a)
{
	app_regs.REG_RESERVED2 = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_RESERVED3                                                        */
/************************************************************************/
void app_read_REG_LATCH_NEXT_UPDATE(void) {}
bool app_write_REG_LATCH_NEXT_UPDATE(void *a)
{
	app_regs.REG_LATCH_NEXT_UPDATE = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_INPUTS_STATE                                                     */
/************************************************************************/
void app_read_REG_INPUTS_STATE(void) { app_regs.REG_INPUTS_STATE = read_DI0 ? B_DI0 : 0; }
bool app_write_REG_INPUTS_STATE(void *a) { return false; }


/************************************************************************/
/* REG_OUTPUTS_SET                                                      */
/************************************************************************/
void app_read_REG_OUTPUTS_SET(void)
{
	app_regs.REG_OUTPUTS_SET = 0;
}

bool app_write_REG_OUTPUTS_SET(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (app_regs.REG_DO0_CONF != GM_DO_DIG) reg &= ~B_DO0;
   if (app_regs.REG_DO1_CONF != GM_DO_DIG) reg &= ~B_DO1;
   
   PORTA_OUTSET = (reg << 1) & 0x3E;

	app_regs.REG_OUTPUTS_SET = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_CLEAR                                                    */
/************************************************************************/
void app_read_REG_OUTPUTS_CLEAR(void)
{
	app_regs.REG_OUTPUTS_CLEAR = 0;
}

bool app_write_REG_OUTPUTS_CLEAR(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (app_regs.REG_DO0_CONF != GM_DO_DIG) reg &= ~B_DO0;
   if (app_regs.REG_DO1_CONF != GM_DO_DIG) reg &= ~B_DO1;
      
   PORTA_OUTCLR = (reg << 1) & 0x3E;

	app_regs.REG_OUTPUTS_CLEAR = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_TOGGLE                                                   */
/************************************************************************/
void app_read_REG_OUTPUTS_TOGGLE(void)
{
	app_regs.REG_OUTPUTS_TOGGLE = 0;
}

bool app_write_REG_OUTPUTS_TOGGLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (app_regs.REG_DO0_CONF != GM_DO_DIG) reg &= ~B_DO0;
   if (app_regs.REG_DO1_CONF != GM_DO_DIG) reg &= ~B_DO1;
   
   PORTA_OUTTGL = (reg << 1) & 0x3E;

	app_regs.REG_OUTPUTS_TOGGLE = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_OUT                                                      */
/************************************************************************/
void app_read_REG_OUTPUTS_OUT(void)
{
	app_regs.REG_OUTPUTS_OUT  = read_DO0 ? B_DO0 : 0;
   app_regs.REG_OUTPUTS_OUT |= read_DO1 ? B_DO1 : 0;
   app_regs.REG_OUTPUTS_OUT |= read_DO2 ? B_DO2 : 0;
   app_regs.REG_OUTPUTS_OUT |= read_DO3 ? B_DO3 : 0;
   app_regs.REG_OUTPUTS_OUT |= read_DO4 ? B_DO4 : 0;
}

bool app_write_REG_OUTPUTS_OUT(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (app_regs.REG_DO0_CONF != GM_DO_DIG) reg = (reg & ~B_DO0) | ((PORTA_OUT & (1<<1)) ? B_DO0 : 0);
   if (app_regs.REG_DO1_CONF != GM_DO_DIG) reg = (reg & ~B_DO1) | ((PORTA_OUT & (1<<2)) ? B_DO1 : 0);
      
   PORTA_OUT = (PORTA_OUT & ~0x3E) | ((reg << 1) & 0x3E);

	app_regs.REG_OUTPUTS_OUT = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_PERIOD                                                     */
/************************************************************************/
void app_read_REG_PULSE_PERIOD(void) {}
bool app_write_REG_PULSE_PERIOD(void *a)
{   
   if (*((uint16_t*)a) < 20)
      return false;
   
	app_regs.REG_PULSE_PERIOD = *((uint16_t*)a);
	return true;
}


/************************************************************************/
/* REG_PULSE_COUNT                                                      */
/************************************************************************/
void app_read_REG_PULSE_COUNT(void) {}
bool app_write_REG_PULSE_COUNT(void *a)
{
   start_array_pulses = true;

	app_regs.REG_PULSE_COUNT = *((uint8_t*)a);
	return true;
}


/************************************************************************/
/* REG_EVNT_ENABLE                                                      */
/************************************************************************/
void app_read_REG_EVNT_ENABLE(void) {}
bool app_write_REG_EVNT_ENABLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);
   
   if (reg & ~(B_EVT_INPUTS_STATE | B_EVT_LED_STATUS))
      return false;

	app_regs.REG_EVNT_ENABLE = reg;
	return true;
}