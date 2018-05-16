#include <avr/io.h>
#include "cpu.h"
#include "WS2812S.h"
#include "uart0.h"

#define F_CPU 32000000
#include <util/delay.h>

/************************************************************************/
/* Definition of pins handling                                          */
/************************************************************************/
/* LEDS_READY */
#define set_LEDS_READY set_io(PORTE, 0)
#define clr_LEDS_READY clear_io(PORTE, 0)
#define tgl_LEDS_READY toggle_io(PORTE, 0)

/* DISABLING_LEDS */
#define set_DISABLING_LEDS set_io(PORTE, 1)
#define clr_DISABLING_LEDS clear_io(PORTE, 1)
#define tgl_DISABLING_LEDS toggle_io(PORTE, 1)

/* Reading pins */
#define read_UPDATE_LEDS read_io(PORTC, 2)
#define read_DISABLE_LEDS read_io(PORTC, 3)
#define read_DEMO_MODE read_io(PORTC, 4)
    
/************************************************************************/
/* External variables                                                   */
/************************************************************************/
extern uint8_t rxbuff_uart0[UART0_RXBUFSIZ];

#if UART0_RXBUFSIZ > 256
   extern uint16_t uart0_rx_pointer;
#else
   extern uint8_t uart0_rx_pointer;
#endif

/************************************************************************/
/* Global variables                                                     */
/************************************************************************/
uint8_t grb_on[32][3];
uint8_t grb_off[32][3];

uint8_t rx_state = 0;

/************************************************************************/
/* main()                                                               */
/************************************************************************/
int main(void)
{
   /* Set clock to 32 MHz */
   cpu_config_clock(32000000, true, true);
   
   /* Initialize RGB bus pin */
   initialize_rgb();
   
   /* Initialize interface pins with master */
   io_pin2in(&PORTC, 2, PULL_IO_DOWN, SENSE_IO_EDGE_RISING);   // UPDATE_LEDS
   io_set_int(&PORTC, INT_LEVEL_LOW, 0, (1<<2), false);        // UPDATE_LEDS
   io_pin2in(&PORTC, 3, PULL_IO_DOWN, SENSE_IO_EDGE_RISING);   // DISABLE_LEDS
   io_set_int(&PORTC, INT_LEVEL_LOW, 0, (1<<3), false);        // DISABLE_LEDS
   io_pin2in(&PORTC, 4, PULL_IO_DOWN, SENSE_IO_EDGE_RISING);   // DEMO_MODE
   io_set_int(&PORTC, INT_LEVEL_LOW, 1, (1<<4), false);        // DEMO_MODE
   io_pin2out(&PORTE, 0, OUT_IO_DIGITAL, IN_EN_IO_EN);         // LEDS_READY
   io_pin2out(&PORTE, 1, OUT_IO_DIGITAL, IN_EN_IO_EN);         // DISABLING_LEDS
   
   /* Initialize UART */
   uart0_init(0, 1, false);   // 1 Mb/s
   //uart0_init(1, 1, false); // 500 Kb/s
   uart0_enable();
   uart0_rx_pointer = 0;
         
   /* Initialize ON and OFF arrays */
   for (uint8_t i = 0; i < 32; i++)
      for (uint8_t j = 0; j < 3; j++)
      {
         grb_on[i][j] = 0;
         grb_off[i][j] = 0;
      }
   
   /* Enable interrupts */
   cpu_enable_int_level(INT_LEVEL_LOW);
   
   /* Do nothing */
   while(1);
}

/************************************************************************/
/* Handle UART RX                                                       */
/************************************************************************/
#define START_TIMEOUT timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV64, 250, INT_LEVEL_LOW) /* 500 us */
#define RESET_TIMEOUT timer_type0_set_counter(&TCC0, 0)
#define STOP_TIMEOUT timer_type0_stop(&TCC0)

void uart0_rcv_byte_callback(uint8_t byte)
{
   switch (rx_state)
   {
      case 0: if (byte == 'r') rx_state++; START_TIMEOUT; break;
      case 1: if (byte == 'g') rx_state++; RESET_TIMEOUT; break;
      case 2: if (byte == 'b') rx_state++; RESET_TIMEOUT; break;
      case 3:
            rxbuff_uart0[uart0_rx_pointer++];
            RESET_TIMEOUT;
            
            if (uart0_rx_pointer == UART0_RXBUFSIZ)
            {
               set_LEDS_READY;
               STOP_TIMEOUT;
               
               uart0_rx_pointer = 0;
               rx_state = 0;
               
               for (uint8_t i = 0; i < 32 * 3; i++)
               {
                  *((&grb_on[0][0]) + i) = rxbuff_uart0[i];
               }
            }
   }
}

/************************************************************************/
/* UART timeout                                                         */
/************************************************************************/
ISR(TCC0_OVF_vect, ISR_NAKED)
{
   STOP_TIMEOUT;
   
   uart0_rx_pointer = 0;
   rx_state = 0;
   
   reti();
}

/************************************************************************/
/* UPDATE_LEDS and DISABLE_LEDS (input pins)                            */
/************************************************************************/
ISR(PORTC_INT0_vect, ISR_NAKED)
{
   if (read_UPDATE_LEDS)
   {
      set_LEDS_READY;
   
      if (rx_state == 0)
      {
         disable_uart0_rx;
         update_32rgbs(&grb_on[0][0]);
         enable_uart0_rx;
      }

      clr_LEDS_READY;
   }

   if (read_DISABLE_LEDS)
   {
      set_DISABLING_LEDS;
   
      disable_uart0_rx;
      update_32rgbs(&grb_off[0][0]);
      enable_uart0_rx;
   
      clr_DISABLING_LEDS;
   }   
         
   reti();
}

/************************************************************************/
/* DEMO_MODE (input pin)                                                */
/************************************************************************/
void demo_mode (void);

ISR(PORTC_INT1_vect, ISR_NAKED)
{
   demo_mode();
   
   reti();
}

#define FINISH_DEMO // do {if (!read_DEMO_MODE) {update_32rgbs(&grb_off[0][0]); return;} } while(0)
   
void demo_mode (void)
{
   while(1)
   {
      FINISH_DEMO;
        
      for (uint8_t jj = 0; jj < 4; jj++)
      {
         for (uint8_t ii = 0; ii < 32; ii++)
         {
            for (uint8_t i = 0; i < 32; i++)
               for (uint8_t j = 0; j < 3; j++)
                  grb_on[i][j] = 0;
      
            if (jj != 3)
            {
               grb_on[ii][jj] = 255;
            }
            else
            {
               grb_on[ii][0] = 255;
               grb_on[ii][1] = 255;
               grb_on[ii][2] = 255;
            }
         
            FINISH_DEMO; update_32rgbs(&grb_on[0][0]);
            FINISH_DEMO; _delay_ms(50);
         }
      }
      
      for (uint8_t i = 0; i < 32; i++)
         for (uint8_t j = 0; j < 3; j++)
         {
            grb_on[i][j] = 255;
         }
      
      FINISH_DEMO; update_32rgbs(&grb_off[0][0]);
      FINISH_DEMO; _delay_ms(250);
      
      FINISH_DEMO; update_32rgbs(&grb_on[0][0]);
      FINISH_DEMO; _delay_ms(250);
      
      FINISH_DEMO; update_32rgbs(&grb_off[0][0]);
      FINISH_DEMO; _delay_ms(250);
      
      FINISH_DEMO; update_32rgbs(&grb_on[0][0]);
      FINISH_DEMO; _delay_ms(250);      
      
      FINISH_DEMO; update_32rgbs(&grb_off[0][0]);
      FINISH_DEMO; _delay_ms(250);      
      
      FINISH_DEMO; update_32rgbs(&grb_on[0][0]);
      FINISH_DEMO; _delay_ms(250);      
      
      FINISH_DEMO; update_32rgbs(&grb_off[0][0]);
      FINISH_DEMO; _delay_ms(250);      
   }
}