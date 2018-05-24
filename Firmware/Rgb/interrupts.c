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
/* DI0                                                                  */
/************************************************************************/
ISR(PORTA_INT0_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* LEDS_READY0 && LEDS_READY1                                           */
/************************************************************************/
bool leds_ready0 = false;
bool leds_ready1 = false;

void leds_are_ready (void)
{
   if (app_regs.REG_DI0_CONF == GM_DI0_SYNC)
   {
      set_UPDATE_LEDS0;
      set_UPDATE_LEDS1;
      clr_UPDATE_LEDS0;
      clr_UPDATE_LEDS1;
   }
      
   if (app_regs.REG_DI0_CONF == GM_DI0_HIGH_RGBS_ON && read_DI0)
   {  
      set_UPDATE_LEDS0;
      set_UPDATE_LEDS1;
      clr_UPDATE_LEDS0;
      clr_UPDATE_LEDS1;
   }
}

ISR(PORTB_INT0_vect, ISR_NAKED)
{
   if (read_LEDS_READY0)
   {
      leds_ready0 = true;   
      if (leds_ready0 && leds_ready1)
      {
         leds_ready0 = false;
         leds_ready1 = false;
         leds_are_ready();
      }
   }   
   
   reti();
}

ISR(PORTC_INT0_vect, ISR_NAKED)
{  
   if (read_LEDS_READY1)
   {
      leds_ready1 = true; 
      if (leds_ready0 && leds_ready1)
      {
         leds_ready0 = false;
         leds_ready1 = false;
         leds_are_ready();    
      }
   }      
   
	reti();
}

/************************************************************************/ 
/* DISABLING_LEDS0                                                      */
/************************************************************************/
ISR(PORTB_INT1_vect, ISR_NAKED)
{
	reti();
}

/************************************************************************/ 
/* DISABLING_LEDS1                                                      */
/************************************************************************/
ISR(PORTC_INT1_vect, ISR_NAKED)
{
	reti();
}

