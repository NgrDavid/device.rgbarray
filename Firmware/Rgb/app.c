#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"

#include "uart0.h"
#include "uart1.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;
extern uint8_t app_regs_type[];
extern uint16_t app_regs_n_elements[];
extern uint8_t *app_regs_pointer[];
extern void (*app_func_rd_pointer[])(void);
extern bool (*app_func_wr_pointer[])(void*);

/************************************************************************/
/* Initialize app                                                       */
/************************************************************************/
static const uint8_t default_device_name[] = "Rgbs";

void hwbp_app_initialize(void)
{
	   /* Define versions */
    uint8_t hwH = 1;
    uint8_t hwL = 0;
    uint8_t fwH = 1;
    uint8_t fwL = 0;
    uint8_t ass = 0;
    
   	/* Start core */
    core_func_start_core(
        1264,
        hwH, hwL,
        fwH, fwL,
        ass,
        (uint8_t*)(&app_regs),
        APP_NBYTES_OF_REG_BANK,
        APP_REGS_ADD_MAX - APP_REGS_ADD_MIN + 1,
        default_device_name
    );
}

/************************************************************************/
/* Handle if a catastrophic error occur                                 */
/************************************************************************/
void core_callback_catastrophic_error_detected(void)
{
	
}

/************************************************************************/
/* General definitions                                                  */
/************************************************************************/
// #define NBYTES 23

/************************************************************************/
/* General used functions                                               */
/************************************************************************/
uint8_t cmd_array[4] = {'r', 'g', 'b', 0};   // Command and REG_LEDS_ON_BUS/2
uint8_t cmd_demo[4]  = {'r', 'g', 'c', 0};   // Command and REG_LEDS_ON_BUS/2
   
void uart0_rcv_byte_callback(uint8_t byte) {}
void uart1_rcv_byte_callback(uint8_t byte) {}
   
void update_bus (void)
{
   clr_DEMO_MODE0;   // Stop demonstration mode if active
   clr_DEMO_MODE1;
      
   cmd_array[3] = app_regs.REG_LEDS_ON_BUS >> 1;   // Divide by 2
   
   uart0_xmit(cmd_array, 4);
   uart0_xmit(app_regs.REG_COLOR_ARRAY, cmd_array[3] * 3);
   
   uart1_xmit(cmd_array, 4);
   uart1_xmit(app_regs.REG_COLOR_ARRAY + 96, cmd_array[3] * 3);
}

void start_demo_mode (void)
{
   uint8_t leds_on_bus = app_regs.REG_LEDS_ON_BUS >> 1;   // Divide by 2
   
   set_DEMO_MODE0;
   set_DEMO_MODE1;
   
   uart0_xmit(cmd_demo, 3);
   uart0_xmit(&leds_on_bus, 1);
   
   uart1_xmit(cmd_demo, 3);
   uart1_xmit(&leds_on_bus, 1);
}

void stop_demo_mode (void)
{
   clr_DEMO_MODE0;
   clr_DEMO_MODE1;
}


/************************************************************************/
/* Initialization Callbacks                                             */
/************************************************************************/
void core_callback_1st_config_hw_after_boot(void)
{
	/* Initialize IOs */
	/* Don't delete this function!!! */
	init_ios();
   
   /* Initialize UARTs */
   uart0_init(0, 1, false);   // 1 Mb/s
   //uart0_init(1, 1, false); // 500 Kb/s   
   uart1_init(0, 1, false);   // 1 Mb/s
   //uart0_init(1, 1, false); // 500 Kb/s
   uart0_enable();
   uart1_enable();
}

void core_callback_reset_registers(void)
{
	/* Initialize registers */
	/*
	app_regs.REG_CH0_FREQ = 10.0;
	app_regs.REG_CH0_DUTYCYCLE = 50;
	
	if ((app_regs.REG_MODE0 & B_M0) == GM_USB_MODE)
	{
		app_regs.REG_OUT0 = 0;
	}

	if ((app_regs.REG_MODE1AND2 & B_M1AND2) == GM_USB_MODE)
	{
		app_regs.REG_OUT1 = 0;
		app_regs.REG_OUT2 = 0;
	}
	*/
   app_regs.REG_LEDS_ON_BUS = 64;   
   
   for (uint8_t i = 0; i < 192; i++)
   {
      app_regs.REG_COLOR_ARRAY[i] = 0;
   }      
   for (uint8_t i = 0; i < 96; i++)
   {
      app_regs.REG_COLOR_ARRAY_BUS0[i] = 0;
      app_regs.REG_COLOR_ARRAY_BUS1[i] = 0;
   }
   
   
   
   
   app_regs.REG_COLOR_ARRAY_BUS0[0+0] = 255;
   app_regs.REG_COLOR_ARRAY_BUS0[3+1] = 255;
   app_regs.REG_COLOR_ARRAY_BUS0[6+2] = 32;
   
   app_regs.REG_COLOR_ARRAY_BUS1[0+1] = 255;
   app_regs.REG_COLOR_ARRAY_BUS1[3+2] = 255;
   app_regs.REG_COLOR_ARRAY_BUS1[6+3] = 32;
   
   
   
}

void core_callback_registers_were_reinitialized(void)
{
	/* Check if the user indication is valid */
	//update_enabled_pwmx();
	
	/* Update state register */
	//app_regs.REG_TRIG_STATE = (read_TRIG_IN0) ? B_LTRG0 : 0;
	//app_regs.REG_TRIG_STATE |= (read_TRIG_IN1) ? B_LTRG1 : 0;

	/* Reset start bits */
	//app_regs.REG_TRG0_START = 0;
	//app_regs.REG_TRG1_START = 0;

	/*
	if ((app_regs.REG_MODE0 & B_M0) == GM_BNC_MODE)
	{
		app_regs.REG_OUT0 = app_regs.REG_CTRL0;
		set_OUT0(app_regs.REG_OUT0);
	}
	else
	{
		set_OUT0(app_regs.REG_OUT0);
	}
	*/
   
   
   
}

/************************************************************************/
/* Callbacks: Visualization                                             */
/************************************************************************/
void core_callback_visualen_to_on(void)
{
	/* Update channels enable indicators */
	//update_enabled_pwmx();
}

void core_callback_visualen_to_off(void)
{
	/* Clear all the enabled indicators */
}

/************************************************************************/
/* Callbacks: Change on the operation mode                              */
/************************************************************************/
void core_callback_device_to_standby(void) {}
void core_callback_device_to_active(void) {}
void core_callback_device_to_enchanced_active(void) {}
void core_callback_device_to_speed(void) {}

/************************************************************************/
/* Callbacks: 1 ms timer                                                */
/************************************************************************/
extern bool update_rgbs;

void core_callback_t_before_exec(void)
{
   if (update_rgbs)
   {
      //update_bus();
      update_rgbs = false;
   }
}   
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void) {}
void core_callback_t_500us(void) {}
void core_callback_t_1ms(void) {}

/************************************************************************/
/* Callbacks: uart control                                              */
/************************************************************************/
void core_callback_uart_rx_before_exec(void) {}
void core_callback_uart_rx_after_exec(void) {}
void core_callback_uart_tx_before_exec(void) {}
void core_callback_uart_tx_after_exec(void) {}
void core_callback_uart_cts_before_exec(void) {}
void core_callback_uart_cts_after_exec(void) {}

/************************************************************************/
/* Callbacks: Read app register                                         */
/************************************************************************/
bool core_read_app_register(uint8_t add, uint8_t type)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;
	
	/* Receive data */
	(*app_func_rd_pointer[add-APP_REGS_ADD_MIN])();	

	/* Return success */
	return true;
}

/************************************************************************/
/* Callbacks: Write app register                                        */
/************************************************************************/
bool core_write_app_register(uint8_t add, uint8_t type, uint8_t * content, uint16_t n_elements)
{
	/* Check if it will not access forbidden memory */
	if (add < APP_REGS_ADD_MIN || add > APP_REGS_ADD_MAX)
		return false;
	
	/* Check if type matches */
	if (app_regs_type[add-APP_REGS_ADD_MIN] != type)
		return false;

	/* Check if the number of elements matches */
	if (app_regs_n_elements[add-APP_REGS_ADD_MIN] != n_elements)
		return false;

	/* Process data and return false if write is not allowed or contains errors */
	return (*app_func_wr_pointer[add-APP_REGS_ADD_MIN])(content);
}