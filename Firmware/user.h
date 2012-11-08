#ifndef USER_H
#define USER_H

#include "myDelays.h"

/* Type of function's vector - G & M codes*/
typedef void (*_func)(char[]);

typedef enum {
	SERIALPORTCONNECTED,	// 0
	HANDSHAKEACKRECEIVED,	// 1
	CNCMATICCONNECTED,		// 2
	WAITINGCOMMAND,			// 3
	PROCESSINGCOMMAND,		// 4
	LIMITSENSOR,			// 5
	EMERGENCYSTOP,			// 6
	FREEMOVES				// 7
} state_t;

typedef enum
{
	false,
	true
} bool_t;

typedef struct 
{
	unsigned long x;
	unsigned long y;
	unsigned long z;
} stepsPosition_t;

/********************************************************************************/
/* 								Movement Functions 								*/
/********************************************************************************/
void CustomG(char[]);
void G04(char[]);
void M00(char[]);
void M02(char[]);
void M03(char[]);
void M04(char[]);
void M05(char[]);
void ProcessLinearMovement(stepsPosition_t, long);
void MoveToOrigin(void);

/******************************************/
/*   	  Process String Functions	      */
/******************************************/
int GetValueParameter(char, char[]);
bool_t HasValueParameter(char, char[]);
bool_t ConfigureMachine(char[]);
bool_t isNumber(char[]);
bool_t ValidateCommandReceived(char, char[], char[], char*, char*);
stepsPosition_t GetTargetStepsPosition(char[]);

/********************************************************************************/
/*								Steps <-> Position								*/
/********************************************************************************/
stepsPosition_t CreateStepsPosition(unsigned long, unsigned long, unsigned long);
stepsPosition_t CreateStepsPositionFrom(stepsPosition_t);

/****************************************/
/*	 		Handle Interruptions		*/
/****************************************/
void limitSensorAxisXHandler(void);
void limitSensorAxisYHandler(void);
void limitSensorAxisZHandler(void);
void emergencyStopHandler(void);

/****************************/
/*	Step on Specific Axis	*/
/****************************/
void StepOnX(int);
void StepOnY(int);
void StepOnZ(int);

void user(void);
#endif