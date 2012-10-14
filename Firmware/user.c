#include "p18f4550.h"
#include "./USB/usb.h"
#include "./USB/usb_function_cdc.h"
#include "main.h"
#include "user.h"
#include <string.h>
#include <math.h>
#include <delays.h>
#include <limits.h>
#include <stdio.h>

volatile state_t machineState = SERIALPORTCONNECTED;

char gCode = -1, mCode = -1, freeCode = -1;
char commandReceived[64];
int interruptionX = 0, interruptionY = 0, interruptionZ = 0;

// absolute programming mode
bool_t absoluteProgramming = TRUE;
// units programming - milimeters : inches
bool_t mmUnitProgramming = TRUE;
// Configuration: milimeters - inches - curveSection
enginesConfig_t mmConfiguration;
enginesConfig_t inchesConfiguration;
curvesConfig_t curvesConfiguration;

// Actual Position & Actual Steps
position_t currentPosition;
stepsPosition_t currentSteps;

stepsPosition_t CreateStepsPosition(unsigned long x, unsigned long y, unsigned long z)
{
	stepsPosition_t result;
	
	result.x = x;
	result.y = y;
	result.z = z;
	
	return result;
}
stepsPosition_t CreateStepsPositionFrom(stepsPosition_t position)
{
	return CreateStepsPosition(position.x, position.y, position.z);
}

/********************************************************************/
/*	 Get the number right after the lette <name> inside <command>	*/
/********************************************************************/
double GetValueParameter(char name, char command[])
{
	int i, count = strlen(command);
	char* ptr, *temp;
	strcpy(temp, command);
	
	for(i = 0; i < count; i++)
	{
		if( temp[i] == name)
		{
			ptr = &temp[i + 1];
			for(++i; (temp[i] != ' ') && (i < count); i++) ;
			temp[i] = '\0';
			return atof(ptr);
		}
	}
	return 0;
}

/********************************************************************/
/*			Conversions between Steps & Position units			 	*/
/********************************************************************/
stepsPosition_t ToSteps(double x, double y, double z)
{
	stepsPosition_t result;
	if(mmUnitProgramming)
	{
		result.x = (unsigned long) ceil(x * mmConfiguration.step_units_axisX);
		result.y = (unsigned long) ceil(y * mmConfiguration.step_units_axisY);
		result.z = (unsigned long) ceil(z * mmConfiguration.step_units_axisZ);
	}
	else
	{
		result.x = (unsigned long) ceil(x * inchesConfiguration.step_units_axisX);
		result.y = (unsigned long) ceil(y * inchesConfiguration.step_units_axisY);
		result.z = (unsigned long) ceil(z * inchesConfiguration.step_units_axisZ);
	}
	return result;
}
stepsPosition_t ToStepsFrom(position_t position)
{
	stepsPosition_t result;
	result = ToSteps(position.x, position.y, position.z);
	return result;
}
position_t ToPosition(unsigned long x, unsigned long y, unsigned long z)
{
	position_t result;
	if(mmUnitProgramming)
	{
		result.x = (double) (x / mmConfiguration.step_units_axisX);
		result.y = (double) (y / mmConfiguration.step_units_axisY);
		result.z = (double) (z / mmConfiguration.step_units_axisZ);
	}
	else
	{
		result.x = (double) (x / inchesConfiguration.step_units_axisX);
		result.y = (double) (y / inchesConfiguration.step_units_axisY);
		result.z = (double) (z / inchesConfiguration.step_units_axisZ);
	}
	return result;
}
position_t ToPositionFrom(stepsPosition_t steps)
{
	position_t result;
	result = ToPosition(steps.x, steps.y, steps.z);
	return result;
}

/************************************************************************/
/*																		*/
/************************************************************************/
bool_t ConfigureMachine(char configurationString[])
{
	char *configPtr;
	
	// MM configuration
	configPtr = strtokpgmram(USB_In_Buffer, (const rom char far *)";");
	if(configPtr == NULL) return FALSE;
	mmConfiguration.step_units_axisX = atof(configPtr);
	if(mmConfiguration.step_units_axisX == 0) return FALSE;
	
	configPtr = strtokpgmram(NULL, (const rom char far *)";");
	if(configPtr == NULL) return FALSE;
	mmConfiguration.step_units_axisY = atof(configPtr);
	if(mmConfiguration.step_units_axisY == 0) return FALSE;
	
	configPtr = strtokpgmram(NULL, (const rom char far *)";");
	if(configPtr == NULL) return FALSE;
	mmConfiguration.step_units_axisZ = atof(configPtr);
	if(mmConfiguration.step_units_axisZ == 0) return FALSE;
	
	// inches configuration
	configPtr = strtokpgmram(USB_In_Buffer, (const rom char far *)";");
	if(configPtr == NULL) return FALSE;
	inchesConfiguration.step_units_axisX = atof(configPtr);
	if(inchesConfiguration.step_units_axisX == 0) return FALSE;
	
	configPtr = strtokpgmram(NULL, (const rom char far *)";");
	if(configPtr == NULL) return FALSE;
	if(inchesConfiguration.step_units_axisY == 0) return FALSE;
	
	configPtr = strtokpgmram(NULL, (const rom char far *)";");
	if(configPtr == NULL) return FALSE;
	if(inchesConfiguration.step_units_axisZ == 0) return FALSE;

	// curveSection
	configPtr = strtokpgmram(NULL, (const rom char far *)";");
	if(configPtr == NULL) return FALSE;
	curvesConfiguration.mmSections = atof(configPtr);
	if(curvesConfiguration.mmSections == 0) return FALSE;
	
	configPtr = strtokpgmram(NULL, (const rom char far *)";");
	if(configPtr == NULL) return FALSE;
	curvesConfiguration.inchesSections = atof(configPtr);
	if(curvesConfiguration.inchesSections == 0) return FALSE;
	
	return TRUE;
}

void ProcessCurveMovement(position_t finalPosition, position_t centerPosition, unsigned char speed, bool_t clockwiseRotation)
{
	unsigned long aX, aY, bX, bY;
	float angleA, angleB, angle, radius, length;
	int steps, step, s;
	position_t stepPosition; 	// each one of the points over the curve that will be joined
	
	aX = currentPosition.x - centerPosition.x;
	aY = currentPosition.y - centerPosition.y;
	bX = finalPosition.x - centerPosition.x;
	bY = finalPosition.y - centerPosition.y;
	
	angleA = atan2(bY, bX);
	angleB = atan2(aY, aX);
	
	if(angleB <= angleA) angleB += 2 * PI;
	angle = angleB - angleA;
	
	radius = sqrt(aX * aX + aY * aY);
	length = radius * angle;
	steps = (int) ceil(length / (mmUnitProgramming ? curvesConfiguration.mmSections : curvesConfiguration.inchesSections));
	
	for(s = 1; s <= steps; s++)
	{
		step = steps - s;
		stepPosition.x = centerPosition.x + radius * cos(angleA + angle * ((float)step / steps));
		stepPosition.y = centerPosition.y + radius * sin(angleA + angle * ((float)step / steps));
		ProcessLinearMovement(stepPosition, 50);
	}
}

void ProcessLinearMovement(position_t targetPosition, unsigned char speed)
{
	unsigned long xClock, yClock, zClock, totalSteps;
	double totalDistance, xDelta, yDelta, zDelta;
	stepsPosition_t deltaSteps;
	
	// Target Steps -> after this movement
	stepsPosition_t targetSteps = ToStepsFrom(targetPosition);
	
	// Delta Position & Delta Steps -> with this movement
	xDelta = (targetPosition.x - currentPosition.x) > 0 ? targetPosition.x - currentPosition.x : currentPosition.x - targetPosition.x;
	yDelta = (targetPosition.y - currentPosition.y) > 0 ? targetPosition.y - currentPosition.y : currentPosition.y - targetPosition.y;
	zDelta = (targetPosition.z - currentPosition.z) > 0 ? targetPosition.z - currentPosition.z : currentPosition.z - targetPosition.z;
	deltaSteps = ToSteps(xDelta, yDelta, zDelta);
	
	// Total Distance & Total Steps -> in this movement
	totalDistance = sqrt( xDelta * xDelta + yDelta * yDelta + zDelta * zDelta);
	totalSteps = sqrt( deltaSteps.x * deltaSteps.x + deltaSteps.y * deltaSteps.y + deltaSteps.z * deltaSteps.z);
	
	// Clock per each axis
	xClock = ceil( speed * totalSteps / deltaSteps.x );
	yClock = ceil( speed * totalSteps / deltaSteps.y );
	zClock = ceil( speed * totalSteps / deltaSteps.z );
	
	LinearMove(targetSteps, xClock, yClock, zClock);
}

// G functions Supported
void G00(char code[])
{
	ProcessLinearMovement(GetTargetPosition(code), 50);
}

void G01(char code[])
{
	ProcessLinearMovement(GetTargetPosition(code), 100);
}

void G02(char code[])
{
	ProcessCurveMovement(GetTargetPosition(code), GetCenterPosition(code), 50, TRUE);
}

void G03(char code[])
{
	ProcessCurveMovement(GetTargetPosition(code), GetCenterPosition(code), 50, FALSE);
}

void G04(char code[])
{
	;
}
void G17(char code[])
{
	;
}
void G18(char code[])
{
	;
}
void G19(char code[])
{
	;
}
void G20(char code[])
{
	mmUnitProgramming = FALSE;
}
void G21(char code[])
{
	mmUnitProgramming = TRUE;
}
void G90(char code[])
{
	absoluteProgramming = TRUE;
}
void G91(char code[])
{
	absoluteProgramming = FALSE;
}
// function array of GCode commands
_func gCodes[100] = {	G00, 	G01, 	G02, 	G03, 	G04, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL,
						NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	G17, 	G18, 	G19, 
						G20, 	G21, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 
						NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 
						NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 
						NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 
						NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 
						NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 
						NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 
						G90, 	G91, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL};

// M functions Supported
void M00(char code[])
{
	;
}
void M02(char code[])
{
	;
}
// function array of MCode commands
_func mCodes[3] = {	M00,		NULL,	M02	};

/*********************************************************************************/
/*				Get the final position considering the <code> received			 */
/*********************************************************************************/
position_t GetTargetPosition(char code[])
{
	position_t position;
	
	if(absoluteProgramming)
	{
		position.x = GetValueParameter('X', code);
		position.y = GetValueParameter('Y', code);
		position.z = GetValueParameter('Z', code);
	}
	else
	{
		position.x = GetValueParameter('X', code) + currentPosition.x;
		position.y = GetValueParameter('Y', code) + currentPosition.y;
		position.z = GetValueParameter('Z', code) + currentPosition.z;
	}
	
	return position;
}
position_t GetCenterPosition(char code[])
{
	position_t position;
	
	position.x = GetValueParameter('I', code) + currentPosition.x;
	position.y = GetValueParameter('J', code) + currentPosition.y;
	position.z = GetValueParameter('K', code) + currentPosition.z;
	
	return position;
}

void limitSensorAxisX()
{
	PORTAbits.RA1 = ~PORTAbits.RA1;
	// tiro un paso
	PORTAbits.RA2 = 1;
	Delay100TCYx(120);	// Delay 1ms
	PORTAbits.RA2 = 0;

	machineState = LIMITSENSOR;
	interruptionX = 1;
}
void limitSensorAxisY()
{
	PORTCbits.RC1 = ~PORTCbits.RC1;
	// tiro un paso
	PORTCbits.RC2 = 1;
	Delay100TCYx(120);	// Delay 1ms
	PORTAbits.RA2 = 0;

	machineState = LIMITSENSOR;
	interruptionY = 1;
}
void limitSensorAxisZ()
{
	// invierto el sentido de giro
	PORTDbits.RD2 = ~PORTDbits.RD2;
	// tiro un paso
	PORTDbits.RD4 = 1;
	Delay100TCYx(120);	// Delay 1ms
	PORTDbits.RD4 = 0;

	machineState = LIMITSENSOR;
	interruptionZ = 1;
}
void emergencyStop()
{
	machineState = EMERGENCYSTOP;
}

void LinearMove(stepsPosition_t targetStepPosition, unsigned long xFreq, unsigned long yFreq, unsigned long zFreq)
{
	unsigned long clock = 0, xNextStep = 0, yNextStep = 0, zNextStep = 0;
	
	// Seteamos bit de sentido de giro
	PORTAbits.RA1 = (targetStepPosition.x > currentSteps.x) ? 1 : 0;
	PORTCbits.RC1 = (targetStepPosition.y > currentSteps.y) ? 1 : 0;
	PORTDbits.RD2 = (targetStepPosition.z > currentSteps.z) ? 1 : 0;
	
	// Mientras no lleguemos a la posicion final en los 3 ejes, tenemos que hacer girar algún motor
	while( (targetStepPosition.x != currentSteps.x) || (targetStepPosition.y != currentSteps.y) || (targetStepPosition.z != currentSteps.z) )
	{
		// emergency stop
		if( PORTBbits.RB7 ) { goto emergencyStop; }
		
		// si el clock es mayor o igual al clock del próximo paso del motor y si no llegué a la posición final
		if( (clock >= xNextStep) && (currentSteps.x != targetStepPosition.x) )
		{
			if( PORTAbits.RA2 )
			{
				PORTAbits.RA2 = 0;
				// limit sensor AXIS X
				if( !PORTBbits.RB4 ) { goto limitSensorAxisX; }
				
				currentSteps.x += PORTAbits.RA1 ? 1 : -1;
			}
			else
			{
				PORTAbits.RA2 = 1;
			}
			xNextStep += xFreq;
		}
		
		if( (clock >= yNextStep) && (currentSteps.y != targetStepPosition.y) )
		{
			if( PORTCbits.RC2 )
			{
				PORTCbits.RC2 = 0;
				// limit sensor AXIS y
				if( !PORTBbits.RB5 ) { goto limitSensorAxisY; }
				
				currentSteps.y += PORTCbits.RC1 ? 1 : -1;
			}
			else
			{
				PORTCbits.RC2 = 1;
			}
			yNextStep += yFreq;
		}
		
		if( (clock >= zNextStep) && (currentSteps.z != targetStepPosition.z) )
		{
			if( PORTDbits.RD4 )
			{
				PORTDbits.RD4 = 0;
				// limit sensor AXIS z
				if( !PORTBbits.RB6 ) { goto limitSensorAxisZ; }
				
				currentSteps.z += PORTDbits.RD2 ? 1 : -1;
			}
			else
			{
				PORTDbits.RD4 = 1;
			}
			zNextStep += zFreq;
		}
		clock++;
	}
	goto end;
	
	/* Handle LimitSensors and EmergencyStop button */
	limitSensorAxisX:
		limitSensorAxisX();
		goto end;
	limitSensorAxisY:
		limitSensorAxisY();
		goto end;
	limitSensorAxisZ:
		limitSensorAxisZ();
		goto end;
	emergencyStop:
		emergencyStop();
		goto end;
	end:
		currentPosition = ToPositionFrom(currentSteps);
}

void MoveToOrigin()
{
	// seteamos posicion actual como la mas lejana posible
	stepsPosition_t fakeFinalPosition = CreateStepsPosition(ULONG_MAX, ULONG_MAX, ULONG_MAX);
	currentSteps = CreateStepsPositionFrom(fakeFinalPosition);

	while( (interruptionX == 0) || (interruptionY == 0) || (interruptionZ == 0) )
	{
		stepsPosition_t fakeFinalSteps = CreateStepsPosition(ULONG_MAX, ULONG_MAX, ULONG_MAX);
		currentSteps = CreateStepsPositionFrom(fakeFinalPosition);

		// Primero movemos solo el eje z hacia su punto 0...
		fakeFinalPosition.z = 0;
		LinearMove(fakeFinalPosition, ULONG_MAX, ULONG_MAX, 50);
		currentPosition.z = 0;
		
		// Luego el eje y hasta su origen...
		fakeFinalPosition.y = 0;
		LinearMove(fakeFinalPosition, ULONG_MAX, 50, ULONG_MAX);
		currentPosition.y = 0;
		
		// por ultimo, el eje x hacia su posicion inicial.
		fakeFinalPosition.x = 0;
		LinearMove(fakeFinalPosition, 50, ULONG_MAX, ULONG_MAX);
		currentPosition.x = 0;
	}
}

void user(void)
{
	BYTE numBytesRead;
	char message[8];
	char movementCommandCode[3], movementCommandType;
	double stepDegrees, distancePerRevolution;

	//Blink the LEDs according to the USB device status
	/*BlinkUSBStatus();*/
	
	// User Application USB tasks
	if( (USBDeviceState < CONFIGURED_STATE) || (USBSuspendControl == 1) ) return;

	if(USBUSARTIsTxTrfReady())
	{
		numBytesRead = getsUSBUSART(USB_Out_Buffer,64);
		if(numBytesRead != 0)
		{
			BYTE i;
			for(i = 0; i < numBytesRead; i++)
			{
				USB_In_Buffer[i] = USB_Out_Buffer[i];
			}
			USB_In_Buffer[i] = '\0';
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"reset"))
			{
				// Resetear la maquina si recibimos el comando 'reset'
				strcpypgm2ram(message, (const rom char far *)"CNCR");
				putUSBUSART(message, strlen(message));
				machineState = SERIALPORTCONNECTED;
				goto endUser;
			}
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"status"))
			{
				// devolvemos el etado en el que esta la maquina
				sprintf(message, "CNCS:%d", machineState);
				putUSBUSART(message, strlen(message));
				goto endUser;
			}
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"freemoves"))
			{
				strcpypgm2ram(message, (const rom char far *)"CNCFM");
				putUSBUSART(message, strlen(message));
				machineState = FREEMOVES;
				goto endUser;
			}
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"stop"))
			{
				freeCode = -1;
				strcpypgm2ram(message, (const rom char far *)"CNCSFM");
				putUSBUSART(message, strlen(message));
				machineState = SERIALPORTCONNECTED;
				goto endUser;
			}
			
			switch(machineState)
			{
				case FREEMOVES:
					if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"+X")) { freeCode = 0; goto endUser; }
					if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"-X")) { freeCode = 1; goto endUser; }
					if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"+Y")) { freeCode = 2; goto endUser; }
					if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"-Y")) { freeCode = 3; goto endUser; }
					if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"+Z")) { freeCode = 4; goto endUser; }
					if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"-Z")) { freeCode = 5; goto endUser; }
					break;
					
				case SERIALPORTCONNECTED:
					// Count characters received and send this number to PC
					sprintf(message, "%d", numBytesRead);
					putUSBUSART(message, strlen(message));
					machineState = HANDSHAKEACKRECEIVED;
					break;
					
				case HANDSHAKEACKRECEIVED:
					// Compare confirmation message.
					if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"ok"))
						machineState = CNCMATICCONNECTED;
					else
						machineState = SERIALPORTCONNECTED;
					break;
					
				case CNCMATICCONNECTED:
					// tokenize the configuration string
					if( ConfigureMachine(USB_In_Buffer) )
					{
						strcpypgm2ram(message, (const rom char far *)"CFGOK");
						machineState = CONFIGURED;
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"CFGE");
						machineState = CNCMATICCONNECTED;
					}
					putUSBUSART(message, strlen(message));
					break;
					
				case WAITINGCOMMAND:
					// Tipo de codigo [ G | M ]
					movementCommandType = USB_In_Buffer[0];
					// Numero de codigo
					movementCommandCode[0] = USB_In_Buffer[1];
					movementCommandCode[1] = USB_In_Buffer[2];
					movementCommandCode[2] = '\0';
					
					// Chequeamos si el comando es G
					if(movementCommandType == 'G')
					{						
						gCode = atoi(movementCommandCode);
						if(gCodes[gCode] != NULL)
						{
							strcpy(commandReceived, USB_In_Buffer);
							strcpypgm2ram(message, (const rom char far *)"CMDS");
							machineState = PROCESSINGCOMMAND;
						}
						else
						{
							strcpypgm2ram(message, (const rom char far *)"CMDNS");
						}
					}
					// Chequeamos si el comando es M
					else if(movementCommandType == 'M')
					{
						mCode = atoi(movementCommandCode);
						if(mCodes[mCode] != NULL)
						{
							strcpy(commandReceived, USB_In_Buffer);
							strcpypgm2ram(message, (const rom char far *)"CMDS");
							machineState = PROCESSINGCOMMAND;
						}
						else
						{
							strcpypgm2ram(message, (const rom char far *)"CMDNS");
						}
					}
					// El comando no era ni G ni M
					else
					{
						strcpypgm2ram(message, (const rom char far *)"CMDE");
					}
					
					// mandamos el mensaje correspondiente a la PC
					putUSBUSART(message, strlen(message));
					break;
					
				default:
					break;
			}
		}
		else
		{
			switch(machineState)
			{
				case FREEMOVES:
					if(freeCode != -1)
					{
						stepsPosition_t freeTargetSteps = CreateStepsPositionFrom(currentSteps);
						
							 if( freeCode == 0)	{ freeTargetSteps.x++; LinearMove(freeTargetSteps, 50, ULONG_MAX, ULONG_MAX); }
						else if( freeCode == 1) { freeTargetSteps.x--; LinearMove(freeTargetSteps, 50, ULONG_MAX, ULONG_MAX); }
						else if( freeCode == 2) { freeTargetSteps.y++; LinearMove(freeTargetSteps, ULONG_MAX, 50, ULONG_MAX); }
						else if( freeCode == 3) { freeTargetSteps.y--; LinearMove(freeTargetSteps, ULONG_MAX, 50, ULONG_MAX); }
						else if( freeCode == 4) { freeTargetSteps.z++; LinearMove(freeTargetSteps, ULONG_MAX, ULONG_MAX, 50); }
						else if( freeCode == 5) { freeTargetSteps.z--; LinearMove(freeTargetSteps, ULONG_MAX, ULONG_MAX, 50); }
						
						if(machineState == LIMITSENSOR)
						{
							freeCode = -1;
							strcpypgm2ram(message, (const rom char far *)"SFC");
							putUSBUSART(message, strlen(message));
						}
						machineState = FREEMOVES;
					}
					break;
					
				case CONFIGURED:
					MoveToOrigin();
					strcpypgm2ram(message, (const rom char far *)"PO");
					putUSBUSART(message, strlen(message));
					machineState = WAITINGCOMMAND;
					break;
					
				case PROCESSINGCOMMAND:
					// Processing command received
					if(gCode != -1) { gCodes[gCode](commandReceived); }
					if(mCode != -1) { mCodes[mCode](commandReceived); }
					
					// reseteamos variables de comando
					gCode = mCode = -1;
					
					// Chequeamos machineState -> si se activo algun fin de carrera
					if(machineState == LIMITSENSOR)
					{
						strcpypgm2ram(message, (const rom char far *)"SFC");
					}
					else if(machineState == EMERGENCYSTOP)
					{
						strcpypgm2ram(message, (const rom char far *)"PE");
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"CMDDONE");
					}
					putUSBUSART(message, strlen(message));
					machineState = WAITINGCOMMAND;
					break;
						
				default:
					break;
				
			}
		}
	}
	endUser:
		CDCTxService();
}
