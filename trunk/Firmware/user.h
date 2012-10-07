#ifndef USER_H
#define USER_H

typedef enum {
	SERIALPORTCONNECTED,		// 0
	HANDSHAKEACKRECEIVED,	// 1
	CNCMATICCONNECTED,			// 2
	CONFIGURED,							// 3
	WAITINGCOMMAND,					// 4
	PROCESSINGCOMMAND,			// 5
	LIMITSENSOR,							// 6
	EMERGENCYSTOP,					// 7
	TEST,
	ANSWERTEST
} state_t;

typedef struct 
{
	int stepDegrees;
	int distancePerRevolution;
} config_t;

typedef struct 
{
unsigned long x;
unsigned long y;
unsigned long z;
	/*unsigned short long x;
	unsigned short long y;
	unsigned short long z;*/
} position_t;

void LimitSensorHandler(void);
void user(void);

#endif