#ifndef USER_H
#define USER_H

/*#define PI 3.14*/

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

void user(void);

/****************************/
/*	 Handle Interruptions	*/
/****************************/
void limitSensorAxisX(void);
void limitSensorAxisY(void);
void limitSensorAxisZ(void);
void emergencyStop(void);

/********************************************************************************/
/*								Steps <-> Position								*/
/********************************************************************************/
stepsPosition_t CreateStepsPosition(unsigned long, unsigned long, unsigned long);
stepsPosition_t CreateStepsPositionFrom(stepsPosition_t);
stepsPosition_t ToSteps(double, double, double);
stepsPosition_t ToStepsFrom(position_t);
position_t ToPosition(unsigned long, unsigned long, unsigned long);
position_t ToPositionFrom(stepsPosition_t);

/******************************************/
/*   	  Process String Functions	      */
/******************************************/
bool_t ConfigureMachine(char[]);
double GetValueParameter(char, char[]);
bool_t HasValueParameter(char, char[]);
position_t GetTargetPosition(char[]);
position_t GetCenterPosition(char[]);

/********************************************************************************/
/* 								Movement Functions 								*/
/********************************************************************************/
void ProcessCurveMovement(position_t, position_t, unsigned char, bool_t);
void ProcessLinearMovement(position_t, int);
void MoveToOrigin(void);
#endif