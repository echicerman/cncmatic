#ifndef USER_H
#define USER_H

typedef enum {
	SERIALPORTCONNECTED,	// 0
	HANDSHAKEACKRECEIVED,	// 1
	CNCMATICCONNECTED,		// 2
	CONFIGURED,				// 3
	WAITINGCOMMAND,			// 4
	PROCESSINGCOMMAND,		// 5
	LIMITSENSOR,			// 6
	EMERGENCYSTOP,			// 7
	FREEMOVES				// 8
} state_t;

typedef struct 
{
	double axisFactor;
} config_t;

typedef struct 
{
	unsigned long x;
	unsigned long y;
	unsigned long z;
} position_t;

void LimitSensorHandler(void);
void user(void);

position_t CreatePosition(unsigned long, unsigned long, unsigned long);
position_t CreatePositionFrom(position_t);

#endif