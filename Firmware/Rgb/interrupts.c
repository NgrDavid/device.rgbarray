#include "cpu.h"
#include "hwbp_core_types.h"
#include "app_ios_and_regs.h"
#include "app_funcs.h"
#include "hwbp_core.h"

/************************************************************************/
/* Declare application registers                                        */
/************************************************************************/
extern AppRegs app_regs;

/************************************************************************/
/* Interrupts from Timers                                               */
/************************************************************************/
// ISR(TCC0_OVF_vect, ISR_NAKED)
// ISR(TCD0_OVF_vect, ISR_NAKED)
// ISR(TCE0_OVF_vect, ISR_NAKED)
// ISR(TCF0_OVF_vect, ISR_NAKED)
// 
// ISR(TCC0_CCA_vect, ISR_NAKED)
// ISR(TCD0_CCA_vect, ISR_NAKED)
// ISR(TCE0_CCA_vect, ISR_NAKED)
// ISR(TCF0_CCA_vect, ISR_NAKED)
// 
// ISR(TCD1_OVF_vect, ISR_NAKED)
// 
// ISR(TCD1_CCA_vect, ISR_NAKED)

/************************************************************************/
/* Defines                                                              */
/************************************************************************/
#define EVENT_LOAD_DONE 0xA1
#define EVENT_LEDS_UPDATED 0xA2
#define EVENT_LEDS_OFF 0xA3

/************************************************************************/
/* UARTS                                                                */
/************************************************************************/
void load_was_done (void);
void leds_were_updated (void);
void leds_were_turned_off (void);

void uart0_rcv_byte_callback(uint8_t byte) {}
void uart1_rcv_byte_callback(uint8_t byte)
{
   if (byte == EVENT_LOAD_DONE) load_was_done(); 
   if (byte == EVENT_LEDS_UPDATED) leds_were_updated();
   if (byte == EVENT_LEDS_OFF) leds_were_turned_off();
}

/************************************************************************/
/* Clear DO0 and DO1 pins                                               */
/************************************************************************/
ISR(TCC0_OVF_vect, ISR_NAKED) {timer_type0_stop(&TCC0); clr_DO0; reti();}
ISR(TCD0_OVF_vect, ISR_NAKED) {timer_type0_stop(&TCD0); clr_DO1; reti();}

/************************************************************************/
/* Slave has a new set of data for the LEDs                             */
/************************************************************************/
void load_was_done (void)
{
   if ((app_regs.REG_DI0_CONF == GM_DI0_SYNC) ||
       (app_regs.REG_DI0_CONF == GM_DI0_HIGH_RGBS_ON && read_DI0))
   {
      set_UPDATE_LEDS0;
      set_UPDATE_LEDS1;
      clr_UPDATE_LEDS0;
      clr_UPDATE_LEDS1;
   }
   
   if (app_regs.REG_DO0_CONF == GM_DO_PULSE_WHEN_ARRAY_LOADED)
   {
      timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV256, 125, INT_LEVEL_LOW);
      set_DO0;
   }
   if (app_regs.REG_DO0_CONF == GM_DO_TOGGLE_WHEN_ARRAY_LOADED)
   {
      tgl_DO0;
   }
   
   if (app_regs.REG_DO1_CONF == GM_DO_PULSE_WHEN_ARRAY_LOADED)
   {  
      timer_type0_enable(&TCD0, TIMER_PRESCALER_DIV256, 125, INT_LEVEL_LOW);
      set_DO1;
   }
   if (app_regs.REG_DO1_CONF == GM_DO_TOGGLE_WHEN_ARRAY_LOADED)
   {
      tgl_DO1;
   }
}

/************************************************************************/
/* LEDS were updated                                                    */
/************************************************************************/
void leds_were_updated (void)
{
   if (app_regs.REG_EVNT_ENABLE & B_EVT_LED_STATUS)
   {
      app_regs.REG_LEDS_STATUS = B_RGB_ON;
      core_func_send_event(ADD_REG_LEDS_STATUS, true);
   }
   
   if (app_regs.REG_DO0_CONF == GM_DO_PULSE_WHEN_UPDATED)
   {
      timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV256, 125, INT_LEVEL_LOW);
      set_DO0;
   }
   if (app_regs.REG_DO0_CONF == GM_DO_TOGGLE_WHEN_UPDATED)
   {
      tgl_DO0;
   }
      
   if (app_regs.REG_DO1_CONF == GM_DO_PULSE_WHEN_UPDATED)
   {
      timer_type0_enable(&TCD0, TIMER_PRESCALER_DIV256, 125, INT_LEVEL_LOW);
      set_DO1;
   }
   if (app_regs.REG_DO1_CONF == GM_DO_TOGGLE_WHEN_UPDATED)
   {
      tgl_DO1;
   }
}

/************************************************************************/
/* LEDS were updated                                                    */
/************************************************************************/
void leds_were_turned_off (void)
{
   if (app_regs.REG_EVNT_ENABLE & B_EVT_LED_STATUS)
   {
      app_regs.REG_LEDS_STATUS = B_RGB_OFF;
      core_func_send_event(ADD_REG_LEDS_STATUS, true);
   }
}

/************************************************************************/ 
/* DI0                                                                  */
/************************************************************************/
ISR(PORTA_INT0_vect, ISR_NAKED)
{
   uint8_t reg = read_DI0 ? B_DI0 : 0;   
   
   if (app_regs.REG_INPUTS_STATE == reg)
   {
      reti();
   }
   else
   {
      app_regs.REG_INPUTS_STATE = reg;
   }      
   
   if (app_regs.REG_EVNT_ENABLE & B_EVT_INPUTS_STATE)
   {
      core_func_send_event(ADD_REG_INPUTS_STATE, true);
   }
   
   if ((app_regs.REG_DI0_CONF == GM_DI0_RISE_UPDATE_RGBS) && read_DI0)
   {
      set_UPDATE_LEDS0;
      set_UPDATE_LEDS1;
      clr_UPDATE_LEDS0;
      clr_UPDATE_LEDS1;
   }
   
   if ((app_regs.REG_DI0_CONF == GM_DI0_HIGH_RGBS_ON) && reg)
   {
      set_UPDATE_LEDS0;
      set_UPDATE_LEDS1;
      clr_UPDATE_LEDS0;
      clr_UPDATE_LEDS1;
   }
   
   if ((app_regs.REG_DI0_CONF == GM_DI0_HIGH_RGBS_ON) && !reg)
   {
      set_DISABLE_LEDS0;
      set_DISABLE_LEDS1;
      clr_DISABLE_LEDS0;
      clr_DISABLE_LEDS1;
   }
         
	reti();
}