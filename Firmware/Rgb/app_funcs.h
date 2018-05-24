#ifndef _APP_FUNCTIONS_H_
#define _APP_FUNCTIONS_H_
#include <avr/io.h>


/************************************************************************/
/* Define if not defined                                                */
/************************************************************************/
#ifndef bool
	#define bool uint8_t
#endif
#ifndef true
	#define true 1
#endif
#ifndef false
	#define false 0
#endif


/************************************************************************/
/* Prototypes                                                           */
/************************************************************************/
void app_read_REG_LEDS_STATUS(void);
void app_read_REG_LEDS_ON_BUS(void);
void app_read_REG_COLOR_ARRAY(void);
void app_read_REG_COLOR_ARRAY_BUS0(void);
void app_read_REG_COLOR_ARRAY_BUS1(void);
void app_read_REG_RESERVED0(void);
void app_read_REG_RESERVED1(void);
void app_read_REG_DI0_CONF(void);
void app_read_REG_DO0_CONF(void);
void app_read_REG_DO1_CONF(void);
void app_read_REG_RESERVED2(void);
void app_read_REG_RESERVED3(void);
void app_read_REG_INPUTS_STATE(void);
void app_read_REG_OUTPUTS_SET(void);
void app_read_REG_OUTPUTS_CLEAR(void);
void app_read_REG_OUTPUTS_TOGGLE(void);
void app_read_REG_OUTPUTS_OUT(void);
void app_read_REG_RESERVED4(void);
void app_read_REG_RESERVED5(void);
void app_read_REG_EVNT_ENABLE(void);

bool app_write_REG_LEDS_STATUS(void *a);
bool app_write_REG_LEDS_ON_BUS(void *a);
bool app_write_REG_COLOR_ARRAY(void *a);
bool app_write_REG_COLOR_ARRAY_BUS0(void *a);
bool app_write_REG_COLOR_ARRAY_BUS1(void *a);
bool app_write_REG_RESERVED0(void *a);
bool app_write_REG_RESERVED1(void *a);
bool app_write_REG_DI0_CONF(void *a);
bool app_write_REG_DO0_CONF(void *a);
bool app_write_REG_DO1_CONF(void *a);
bool app_write_REG_RESERVED2(void *a);
bool app_write_REG_RESERVED3(void *a);
bool app_write_REG_INPUTS_STATE(void *a);
bool app_write_REG_OUTPUTS_SET(void *a);
bool app_write_REG_OUTPUTS_CLEAR(void *a);
bool app_write_REG_OUTPUTS_TOGGLE(void *a);
bool app_write_REG_OUTPUTS_OUT(void *a);
bool app_write_REG_RESERVED4(void *a);
bool app_write_REG_RESERVED5(void *a);
bool app_write_REG_EVNT_ENABLE(void *a);


#endif /* _APP_FUNCTIONS_H_ */