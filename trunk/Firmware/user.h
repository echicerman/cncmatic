#ifndef USER_H
#define USER_H

#include "myDelays.h"

/* Type of function's vector - G & M codes*/
typedef void (*_func)(char[]);

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

typedef enum
{
	false,
	true
} bool_t;

typedef struct 
{
	double step_units_axisX;
	double step_units_axisY;
	double step_units_axisZ;
} enginesConfig_t;
typedef struct
{
	double mmSections;
	double inchesSections;
} curvesConfig_t;

typedef struct 
{
	unsigned long x;
	unsigned long y;
	unsigned long z;
} stepsPosition_t;
typedef struct 
{
	double x;
	double y;
	double z;
} position_t;

/********************************************************************************/
/* 								Movement Functions 								*/
/********************************************************************************/
void G00(char[]);
void G01(char[]);
void G04(char[]);
void M00(char[]);
void M02(char[]);
void ProcessLinearMovement(position_t, double);
void MoveToOrigin(void);

/******************************************/
/*   	  Process String Functions	      */
/******************************************/
double GetValueParameter(char, char[]);
bool_t HasValueParameter(char, char[]);
bool_t ConfigureMachine(char[]);
bool_t isNumber(char[]);
bool_t ValidateCommandReceived(char, char[], char[], char*, char*);
position_t GetTargetPosition(char[]);

/********************************************************************************/
/*								Steps <-> Position								*/
/********************************************************************************/
stepsPosition_t CreateStepsPosition(unsigned long, unsigned long, unsigned long);
stepsPosition_t CreateStepsPositionFrom(stepsPosition_t);
position_t CreatePosition(double, double, double);
position_t CreatePositionFrom(position_t);
stepsPosition_t ToStepsPosition(double, double, double);
stepsPosition_t ToStepsPositionFrom(position_t);
position_t ToPosition(unsigned long, unsigned long, unsigned long);
position_t ToPositionFrom(stepsPosition_t);

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
void StepOnX(bool_t);
void StepOnY(bool_t);
void StepOnZ(bool_t);

void user(void);
#endif