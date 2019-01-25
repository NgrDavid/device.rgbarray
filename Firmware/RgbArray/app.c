#include "hwbp_core.h"
#include "hwbp_core_regs.h"
#include "hwbp_core_types.h"

#include "app.h"
#include "app_funcs.h"
#include "app_ios_and_regs.h"

#include "uart0.h"
#include "uart1.h"

#define F_CPU 32000000
#include "util/delay.h"

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
static const uint8_t default_device_name[] = "RgbArray";

void hwbp_app_initialize(void)
{
	   /* Define versions */
    uint8_t hwH = 1;
    uint8_t hwL = 0;
    uint8_t fwH = 2;
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
   clr_DEMO_MODE0;
   clr_DEMO_MODE1;
   
   set_DISABLE_LEDS0;
   set_DISABLE_LEDS1;
   clr_DISABLE_LEDS0;
   clr_DISABLE_LEDS1;
   
   clr_DO0;
   clr_DO1;
   clr_DO2;
   clr_DO3;
   clr_DO4;
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
uint8_t cmd_off[4]  = {'r', 'g', 'd', 0};   // Command and REG_LEDS_ON_BUS/2
   
void update_bus (void)
{
   clr_DEMO_MODE0;   // Stop demonstration mode if active
   clr_DEMO_MODE1;
      
   cmd_array[3] = app_regs.REG_LEDS_ON_BUS;
   
   uart0_xmit(cmd_array, 4);
   uart1_xmit(cmd_array, 4);
   
   uart0_xmit(app_regs.REG_COLOR_ARRAY, cmd_array[3] * 3);
   uart1_xmit(app_regs.REG_COLOR_ARRAY + 96, cmd_array[3] * 3);
}

void start_demo_mode (void)
{
   uint8_t leds_on_bus = app_regs.REG_LEDS_ON_BUS;
   
   set_DEMO_MODE0;
   set_DEMO_MODE1;
   
   uart0_xmit(cmd_demo, 3);
   uart1_xmit(cmd_demo, 3);
   
   uart0_xmit(&leds_on_bus, 1);
   uart1_xmit(&leds_on_bus, 1);
}

void define_off_values (uint8_t red, uint8_t green, uint8_t blue)
{
   clr_DEMO_MODE0;   // Stop demonstration mode if active
   clr_DEMO_MODE1;
   
   cmd_off[3] = app_regs.REG_LEDS_ON_BUS;   
   uint8_t rgb[3] = {red, green, blue};
   
   uart0_xmit(cmd_off, 4);   
   uart1_xmit(cmd_off, 4);
   
   uart0_xmit(rgb, 3);
   uart1_xmit(rgb, 3);
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
   uart1_init(0, 1, false);   // 1 Mb/s
   uart0_enable();
   uart1_enable();
}

void core_callback_reset_registers(void)
{
	app_regs.REG_LEDS_STATUS = 0;
   
   app_regs.REG_LEDS_ON_BUS = 32;
   
   for (uint8_t i = 0; i < 192; i++)
      app_regs.REG_COLOR_ARRAY[i] = 0;
      
   app_regs.REG_OUTPUTS_OUT = 0;
   app_regs.REG_DI0_CONF = GM_DI0_SYNC;
   app_regs.REG_DO0_CONF = GM_DO_DIG;
   app_regs.REG_DO1_CONF = GM_DO_DIG;
      
   app_regs.REG_COLOR_OFF[0] = 0;
   app_regs.REG_COLOR_OFF[1] = 0;
   app_regs.REG_COLOR_OFF[2] = 0;
   
   app_regs.REG_RESERVED1 = 0;
   app_regs.REG_RESERVED2 = 0;
   
   app_regs.REG_LATCH_NEXT_UPDATE = 0;
   
   app_regs.REG_PULSE_PERIOD = 100;
   app_regs.REG_PULSE_COUNT = 0;
   
   app_regs.REG_EVNT_ENABLE = B_EVT_LED_STATUS | B_EVT_INPUTS_STATE;
}

void core_callback_registers_were_reinitialized(void)
{  
   stop_demo_mode();
   
   app_regs.REG_LEDS_STATUS = B_RGB_OFF;
   
   _delay_ms(200);
   define_off_values(app_regs.REG_COLOR_OFF[0], app_regs.REG_COLOR_OFF[1], app_regs.REG_COLOR_OFF[2]);
   timer_type1_enable(&TCD1, TIMER_PRESCALER_DIV256, 3125, INT_LEVEL_LOW);  // 25 ms
   
   app_write_REG_OUTPUTS_OUT(&app_regs.REG_OUTPUTS_OUT);
   
   app_write_REG_DI0_CONF(&app_regs.REG_DI0_CONF);
   app_write_REG_DO0_CONF(&app_regs.REG_DO0_CONF);
   app_write_REG_DO1_CONF(&app_regs.REG_DO1_CONF);
}

/************************************************************************/
/* Callbacks: Visualization                                             */
/************************************************************************/
void core_callback_visualen_to_on(void) {}
void core_callback_visualen_to_off(void) {}

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
void core_callback_t_before_exec(void) {}
void core_callback_t_after_exec(void) {}
void core_callback_t_new_second(void) {}
void core_callback_t_500us(void) {}

extern bool start_array_pulses;

uint16_t pulse_counter = 0;
bool last_array_state = false;

void core_callback_t_1ms(void)
{
   if (start_array_pulses)
   {
      start_array_pulses = false;
      
      pulse_counter = 0;
      last_array_state = true;
      
      uint8_t aux = B_RGB_ON;
      app_write_REG_LEDS_STATUS(&aux);
   }
   else
   {
      if (app_regs.REG_PULSE_COUNT > 0)
      {
         pulse_counter++;
         
         if (pulse_counter == (app_regs.REG_PULSE_PERIOD/2))
         {
            pulse_counter = 0;
            
            if (last_array_state == false)
            {
               last_array_state = true;
               
               uint8_t aux = B_RGB_ON;
               app_write_REG_LEDS_STATUS(&aux);
            }
            else
            {
               last_array_state = false;
               
               uint8_t aux = B_RGB_OFF;
               app_write_REG_LEDS_STATUS(&aux);
               
               app_regs.REG_PULSE_COUNT--;
            }
         }
      }
   }      
}

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