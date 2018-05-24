#include <avr/io.h>
#include "cpu.h"
#include "WS2812S.h"
#include "uart0.h"

#define F_CPU 32000000
#include <util/delay.h>

/************************************************************************/
/* Defines                                                              */
/************************************************************************/
#define EVENT_LOAD_DONE 0xA1
#define EVENT_LEDS_UPDATED 0xA2
#define EVENT_LEDS_OFF 0xA3

/************************************************************************/
/* Definition of pins handling                                          */
/************************************************************************/
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
#define MAX_LEDS 255
uint8_t grb_on[MAX_LEDS][3];
uint8_t grb_off[MAX_LEDS][3];
uint8_t grb_demo[MAX_LEDS][3];

uint8_t rx_state = 0;
uint8_t _3rd_byte;

uint8_t num_of_leds_on_bus = MAX_LEDS;

/************************************************************************/
/* Prototypes                                                           */
/************************************************************************/
void demo_mode (void);

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
   io_set_int(&PORTC, INT_LEVEL_LOW, 1, (1<<3), false);        // DISABLE_LEDS
   io_pin2in(&PORTC, 4, PULL_IO_DOWN, SENSE_IO_EDGES_BOTH);    // DEMO_MODE   
   io_pin2in(&PORTE, 0, PULL_IO_DOWN, SENSE_IO_EDGES_BOTH);    // DUMMY0
   io_pin2in(&PORTE, 1, PULL_IO_DOWN, SENSE_IO_EDGES_BOTH);    // DUMMY1
   
   /* Initialize UART */
   disable_uart0_rx;
   uart0_init(0, 1, false);   // 1 Mb/s
   uart0_enable();
   uart0_rx_pointer = 0;
         
   /* Initialize ON and OFF arrays */
   for (uint16_t i = 0; i < MAX_LEDS; i++)
      for (uint8_t j = 0; j < 3; j++)
      {
         grb_on[i][j] = 0;
         grb_off[i][j] = 0;
      }
   
   /* Turn LEDs off */
   //update_32rgbs(&grb_off[0][0]);
   update_ws2812_bus(&grb_off[0][0], num_of_leds_on_bus);
   
   /* Enable interrupts */
   cpu_enable_int_level(INT_LEVEL_LOW);
   
   /* Do nothing */
   enable_uart0_rx;
   while(1);
}

/************************************************************************/
/* Handle UART RX                                                       */
/************************************************************************/
#define START_TIMEOUT timer_type0_enable(&TCC0, TIMER_PRESCALER_DIV64, 250, INT_LEVEL_LOW) /* 500 us */
#define RESET_TIMEOUT timer_type0_set_counter(&TCC0, 0)
#define STOP_TIMEOUT timer_type0_stop(&TCC0)

// Protocol:
// RGB array: 'r' 'g' 'b' num_of_leds_on_bus array[num_of_leds_on_bus * 3]
// RGB demo:  'r' 'g' 'c' num_of_leds_on_bus

void uart0_rcv_byte_callback(uint8_t byte)
{
   switch (rx_state)
   {
      case 0:
            if (byte == 'r')
            {               
               uart0_rx_pointer = 0;
               
               rx_state++;
               START_TIMEOUT; 
            }
            break;
      case 1:
            if (byte == 'g')
            {
               rx_state++;
               START_TIMEOUT;
            }
            else
            {
               rx_state = 0;
               STOP_TIMEOUT;
            }
            break;
      case 2:
            if (byte == 'b' || byte == 'c')
            {
               rx_state++;
               _3rd_byte = byte;
               RESET_TIMEOUT;
            }
            else
            {
               rx_state = 0;
               STOP_TIMEOUT;
            }
            break;
      
      case 3:
            num_of_leds_on_bus = byte;          
            
            if (_3rd_byte == 'c')
            {
               STOP_TIMEOUT;
               rx_state = 0;
               
               disable_uart0_rx;
               demo_mode();
               enable_uart0_rx;
            }
            else
            {  
               rx_state++;
               RESET_TIMEOUT;
            }
            
            break;
      
      case 4:
            rxbuff_uart0[uart0_rx_pointer++] = byte;
            RESET_TIMEOUT;
            
            if (uart0_rx_pointer == num_of_leds_on_bus * 3 /*+ 1*/)
            {  
               STOP_TIMEOUT;
               rx_state = 0;
               
               uart0_xmit_now_byte(EVENT_LOAD_DONE);
                  
               for (uint16_t i = 0; i < num_of_leds_on_bus; i++)
               {
                  *((&grb_on[0][0]) + i*3 + 0) = rxbuff_uart0[i*3 + 1];
                  *((&grb_on[0][0]) + i*3 + 1) = rxbuff_uart0[i*3 + 0];
                  *((&grb_on[0][0]) + i*3 + 2) = rxbuff_uart0[i*3 + 2];
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
/* UPDATE_LEDS                                                          */
/************************************************************************/
ISR(PORTC_INT0_vect, ISR_NAKED)
{
   if (rx_state == 0)
   {
      disable_uart0_rx;
      update_ws2812_bus(&grb_on[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_on[0][0]);
      uart0_xmit_now_byte(EVENT_LEDS_UPDATED);
      enable_uart0_rx;
   }
   
   reti();
}   

/************************************************************************/
/* DISABLE_LEDS                                                         */
/************************************************************************/
ISR(PORTC_INT1_vect, ISR_NAKED)
{   
   disable_uart0_rx;
   update_ws2812_bus(&grb_off[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_off[0][0]);
   uart0_xmit_now_byte(EVENT_LEDS_OFF);
   enable_uart0_rx;
         
   reti();
}

/************************************************************************/
/* Demonstration mode                                                   */
/************************************************************************/
#define FINISH_DEMO do {if (!read_DEMO_MODE) {update_32rgbs(&grb_off[0][0]); return;} } while(0)
   
void demo_mode (void)
{
   while(1)
   {
      FINISH_DEMO;
        
      for (uint8_t jj = 0; jj < 4; jj++)
      {
         for (uint16_t ii = 0; ii < num_of_leds_on_bus; ii++)
         {
            for (uint16_t i = 0; i < num_of_leds_on_bus; i++)
               for (uint8_t j = 0; j < 3; j++)
                  grb_demo[i][j] = 0;
      
            if (jj != 3)
            {
               grb_demo[ii][jj] = 128;
            }
            else
            {
               grb_demo[ii][0] = 32;
               grb_demo[ii][1] = 32;
               grb_demo[ii][2] = 32;
            }
         
            FINISH_DEMO; update_ws2812_bus(&grb_demo[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_on[0][0]);
            FINISH_DEMO; _delay_ms(50);
         }
      }
      
      for (uint16_t i = 0; i < num_of_leds_on_bus; i++)
         for (uint8_t j = 0; j < 3; j++)
         {
            grb_demo[i][j] = 64;
         }
      
      FINISH_DEMO; update_ws2812_bus(&grb_off[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_off[0][0]);
      FINISH_DEMO; _delay_ms(250);
      
      FINISH_DEMO; update_ws2812_bus(&grb_demo[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_on[0][0]);
      FINISH_DEMO; _delay_ms(250);
      
      FINISH_DEMO; update_ws2812_bus(&grb_off[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_off[0][0]);
      FINISH_DEMO; _delay_ms(250);
      
      FINISH_DEMO; update_ws2812_bus(&grb_demo[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_on[0][0]);
      FINISH_DEMO; _delay_ms(250);      
      
      FINISH_DEMO; update_ws2812_bus(&grb_off[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_off[0][0]);
      FINISH_DEMO; _delay_ms(250);      
      
      FINISH_DEMO; update_ws2812_bus(&grb_demo[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_on[0][0]);
      FINISH_DEMO; _delay_ms(250);      
      
      FINISH_DEMO; update_ws2812_bus(&grb_off[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_off[0][0]);
      FINISH_DEMO; _delay_ms(250);  
      
      for (uint8_t intensity = 0; intensity < 128; intensity++)
      {
         for (uint16_t i = 0; i < num_of_leds_on_bus; i++)
            for (uint8_t j = 0; j < 3; j++)
            {
               grb_demo[i][j] = intensity;  
            }    
            
         FINISH_DEMO; update_ws2812_bus(&grb_demo[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_off[0][0]);
         FINISH_DEMO; _delay_ms(20);
      }
      
      for (uint8_t intensity = 128; intensity != 0; intensity--)
      {
         for (uint16_t i = 0; i < num_of_leds_on_bus; i++)
            for (uint8_t j = 0; j < 3; j++)
            {
               grb_demo[i][j] = intensity;  
            }    
            
         FINISH_DEMO; update_ws2812_bus(&grb_demo[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_off[0][0]);
         FINISH_DEMO; _delay_ms(20);
      }
      
      FINISH_DEMO; update_ws2812_bus(&grb_off[0][0], num_of_leds_on_bus); //update_32rgbs(&grb_off[0][0]);
      FINISH_DEMO; _delay_ms(250);
   }
}