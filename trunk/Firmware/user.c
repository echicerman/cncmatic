#include "p18f4550.h"
#include "./USB/usb.h"
#include "./USB/usb_function_cdc.h"
#include "main.h"
#include "user.h"
#include "myDelays.h"
#include <string.h>
#include <math.h>
#include <delays.h>
#include <limits.h>
#include <stdio.h>

state_t machineState = SERIALPORTCONNECTED;

char gCode = -1, mCode = -1, freeCode = -1;
char commandReceived[64];
int interruptionX = 0, interruptionY = 0, interruptionZ = 0;

// Configuration: milimeters
enginesConfig_t mmConfiguration;

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
position_t CreatePosition(double x, double y, double z)
{
	position_t result;
	
	result.x = x;
	result.y = y;
	result.z = z;
	
	return result;
}
position_t CreatePositionFrom(position_t position)
{
	return CreatePosition(position.x, position.y, position.z);
}

/********************************************************************/
/*				Vectors of G & M functions supported				*/
/********************************************************************/
// G functions Supported
void G00(char code[])
{
	ProcessLinearMovement(GetTargetPosition(code), 50);
}

void G01(char code[])
{
	ProcessLinearMovement(GetTargetPosition(code), 100);
}
void G04(char code[])
{
	;
}

// function array of GCode commands
_func gCodes[5] = {	G00,	G01,	NULL,	NULL,	G04	};

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
_func mCodes[3] = {	M00,	NULL,	M02	};

/********************************************************************/
/*	 Get the number right after the lette <name> inside <command>	*/
/********************************************************************/
double GetValueParameter(char name, char command[])
{
	int i, count = strlen(command);
	char* ptr;
	char temp[64];
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
bool_t HasValueParameter(char name, char command[])
{
	int i, count = strlen(command);
	for(i = 0; i < count; i++)
	{
		if(command[i] == name)
		{
			return true;
		}
	}
	return false;
}

/********************************************************************/
/*			Conversions between Steps & Position units			 	*/
/********************************************************************/
stepsPosition_t ToSteps(double x, double y, double z)
{
	stepsPosition_t result;
	
	result.x = (unsigned long) ceil(x * mmConfiguration.step_units_axisX);
	result.y = (unsigned long) ceil(y * mmConfiguration.step_units_axisY);
	result.z = (unsigned long) ceil(z * mmConfiguration.step_units_axisZ);
	
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
	
	result.x = (double) (x / mmConfiguration.step_units_axisX);
	result.y = (double) (y / mmConfiguration.step_units_axisY);
	result.z = (double) (z / mmConfiguration.step_units_axisZ);
		
	return result;
}
position_t ToPositionFrom(stepsPosition_t steps)
{
	position_t result;
	result = ToPosition(steps.x, steps.y, steps.z);
	return result;
}

/************************************************************************/
/*					Configure the axis of the CNC						*/
/************************************************************************/
bool_t ConfigureMachine(char configurationString[])
{
	char *configPtr;
	
	// MM configuration
	configPtr = strtokpgmram(USB_In_Buffer, (const rom char far *)";");
	if(configPtr == NULL) return false;
	mmConfiguration.step_units_axisX = atof(configPtr);
	if(mmConfiguration.step_units_axisX == 0) return false;
	
	configPtr = strtokpgmram(NULL, (const rom char far *)";");
	if(configPtr == NULL) return false;
	mmConfiguration.step_units_axisY = atof(configPtr);
	if(mmConfiguration.step_units_axisY == 0) return false;
	
	configPtr = strtokpgmram(NULL, (const rom char far *)";");
	if(configPtr == NULL) return false;
	mmConfiguration.step_units_axisZ = atof(configPtr);
	if(mmConfiguration.step_units_axisZ == 0) return false;
	
	return true;
}

/************************************************************************************/
/*							Validate the command received							*/
/************************************************************************************/
bool_t isNumber(char string[])
{
	int i, count = strlen(string);
	for(i = 0; i < count; i++)
	{
		if( (string[i] < 48) || (string[i] > 57) ) return false; // si < '0' o > '9'
	}
	return true;
}
bool_t ValidateCommandReceived(char type, char code[], char result[])
{
	if(isNumber(code))
	{
		if( (type == 'G') )
		{
			// CHEQUEAR QUE PASA SI SE SOBREPASA DEL TAMAÑO DEL ARRAY
			if(gCodes[atoi(code)] != NULL)
			{
				strcpypgm2ram(result, (const rom char far *)"CMDS");
				return true;
			}
			else
			{
				strcpypgm2ram(result, (const rom char far *)"CMDNS");
				return false;
			}
		}
		if( (type == 'M') )
		{
			// CHEQUEAR QUE PASA SI SE SOBREPASA DEL TAMAÑO DEL ARRAY
			if(mCodes[atoi(code)] != NULL)
			{
				strcpypgm2ram(result, (const rom char far *)"CMDS");
				return true;
			}
			else
			{
				strcpypgm2ram(result, (const rom char far *)"CMDNS");
				return false;
			}
		}
	}
	strcpypgm2ram(result, (const rom char far *)"CMDE");
	return false;
}

void StepOn(char axis, bool_t clockwise)
{
	switch(axis)
	{
		case 'X':
			PORTAbits.RA1 = clockwise;
			// tiro un paso
			PORTAbits.RA2 = 1;
			Delay1MS();
			PORTAbits.RA2 = 0;
			
			// limit sensor AXIS X
			if( !PORTBbits.RB4 ) { goto limitSensorAxisX; }
			break;

		case 'Y':	
			PORTCbits.RC1 = clockwise;
			// tiro un paso
			PORTCbits.RC2 = 1;
			Delay1MS();
			PORTCbits.RC2 = 0;
			
			// limit sensor AXIS Y
			if( !PORTBbits.RB5 ) { goto limitSensorAxisY; }
			break;

		case 'Z':
			PORTDbits.RD2 = clockwise;
			// tiro un paso
			PORTDbits.RD4 = 1;
			Delay1MS();
			PORTDbits.RD4 = 0;
			
			// limit sensor AXIS Y
			if( !PORTBbits.RB6 ) { goto limitSensorAxisZ; }
			break;

		default:
			break;
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

/************************************************************************************/
/*							Process the Linear Movement								*/
/************************************************************************************/
void ProcessLinearMovement(position_t targetPosition, int delay)
{
	unsigned long xCounter, yCounter, zCounter, totalSteps;
	long maxDelta;
	double totalDistance, xDelta, yDelta, zDelta;
	stepsPosition_t deltaStateChangesPosition;
	
	// Target Steps -> after this movement
	stepsPosition_t targetStepsPosition = ToStepsFrom(targetPosition);
	
	// Delta Position & Delta Steps -> with this movement
	xDelta = ( (targetPosition.x - currentPosition.x) > 0 ? targetPosition.x - currentPosition.x : currentPosition.x - targetPosition.x);
	yDelta = ( (targetPosition.y - currentPosition.y) > 0 ? targetPosition.y - currentPosition.y : currentPosition.y - targetPosition.y);
	zDelta = ( (targetPosition.z - currentPosition.z) > 0 ? targetPosition.z - currentPosition.z : currentPosition.z - targetPosition.z);
	
	deltaStateChangesPosition = ToSteps(xDelta * 2, yDelta * 2, zDelta * 2);
	maxDelta = ( (deltaStateChangesPosition.x > deltaStateChangesPosition.y) ? deltaStateChangesPosition.x : deltaStateChangesPosition.y);
	maxDelta = ( (maxDelta > deltaStateChangesPosition.z) ? maxDelta : deltaStateChangesPosition.z);
	
	xCounter = -maxDelta / 2;
	yCounter = -maxDelta / 2;
	zCounter = -maxDelta / 2;
	
	// Seteamos bit de sentido de giro
	PORTAbits.RA1 = (targetStepsPosition.x > currentSteps.x) ? 1 : 0;
	PORTCbits.RC1 = (targetStepsPosition.y > currentSteps.y) ? 1 : 0;
	PORTDbits.RD2 = (targetStepsPosition.z > currentSteps.z) ? 1 : 0;
	
	// HAY QUE MULTIPLICAR POR 2 PARA TENER EN CUENTA LOS 2 CAMBIOS DE ETADOQUE DAN UN PASO
	
	while( (targetStepsPosition.x != currentSteps.x) || (targetStepsPosition.y != currentSteps.y) || (targetStepsPosition.z != currentSteps.z) )
	{
		// emergency stop
		if( PORTBbits.RB7 ) { goto emergencyStop; }
		
		if(targetStepsPosition.x != currentSteps.x)
		{
			xCounter += deltaStateChangesPosition.x;
			if(xCounter > 0)
			{
				if( PORTAbits.RA2 )
				{
					PORTAbits.RA2 = 0;
					// limit sensor AXIS X
					if( !PORTBbits.RB4 ) { goto limitSensorAxisX; }
					
					xCounter -= maxDelta;
					currentSteps.x += PORTAbits.RA1 ? 1 : -1;
				}
				else
				{
					xCounter -= maxDelta;
					PORTAbits.RA2 = 1;
				}
			}			
		}
		
		if(targetStepsPosition.y != currentSteps.y)
		{
			yCounter += deltaStateChangesPosition.y;
			if(yCounter > 0)
			{
				if( PORTCbits.RC2 )
				{
					PORTCbits.RC2 = 0;
					// limit sensor AXIS Y
					if( !PORTBbits.RB5 ) { goto limitSensorAxisY; }
					
					yCounter -= maxDelta;
					currentSteps.y += PORTCbits.RC1 ? 1 : -1;
				}
				else
				{
					yCounter -= maxDelta;
					PORTCbits.RC2 = 1;
				}
			}			
		}
		
		if(targetStepsPosition.z != currentSteps.z)
		{
			zCounter += deltaStateChangesPosition.z;
			if(zCounter > 0)
			{
				if( PORTDbits.RD4 )
				{
					PORTDbits.RD4 = 0;
					// limit sensor AXIS Y
					if( !PORTBbits.RB6 ) { goto limitSensorAxisZ; }
					
					zCounter -= maxDelta;
					currentSteps.z += PORTDbits.RD2 ? 1 : -1;
				}
				else
				{
					zCounter -= maxDelta;
					PORTDbits.RD4 = 1;
				}
			}	
		}
		
		//wait for next step.
		if(delay > 10)
			Delay10MSx((int)delay/10);
		else
			Delay1MSx(delay);
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

/*********************************************************************************/
/*				Get the final position considering the <code> received			 */
/*********************************************************************************/
position_t GetTargetPosition(char code[])
{
	position_t position;
	
	position.x = HasValueParameter('X', code) ? GetValueParameter('X', code) : currentPosition.x;
	position.y = HasValueParameter('Y', code) ? GetValueParameter('Y', code) : currentPosition.y;
	position.z = HasValueParameter('Z', code) ? GetValueParameter('Z', code) : currentPosition.z;
	
	return position;
}

/********************************************************/
/*		Functions to handle limitSensors activation		*/
/********************************************************/
void limitSensorAxisX()
{
	PORTAbits.RA1 = ~PORTAbits.RA1;
	// tiro un paso
	PORTAbits.RA2 = 1;
	Delay1MS();
	PORTAbits.RA2 = 0;
	
	machineState = LIMITSENSOR;
	interruptionX = 1;
}
void limitSensorAxisY()
{
	PORTCbits.RC1 = ~PORTCbits.RC1;
	// tiro un paso
	PORTCbits.RC2 = 1;
	Delay1MS();
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
	Delay1MS();
	PORTDbits.RD4 = 0;

	machineState = LIMITSENSOR;
	interruptionZ = 1;
}
void emergencyStop()
{
	machineState = EMERGENCYSTOP;
}

void MoveToOrigin()
{
	// seteamos posicion actual como la mas lejana posible
	position_t fakeFinalPosition = CreatePosition(HUGE_VAL, HUGE_VAL, HUGE_VAL);
	currentPosition = CreatePositionFrom(fakeFinalPosition);

	while( (interruptionX == 0) || (interruptionY == 0) || (interruptionZ == 0) )
	{
		// Primero movemos solo el eje z hacia su punto 0...
		fakeFinalPosition.z = 0;
		ProcessLinearMovement(fakeFinalPosition, 20);
		currentPosition.z = 0;
		
		// Luego el eje y hasta su origen...
		fakeFinalPosition.y = 0;
		ProcessLinearMovement(fakeFinalPosition, 20);
		currentPosition.y = 0;
		
		// por ultimo, el eje x hacia su posicion inicial.
		fakeFinalPosition.x = 0;
		ProcessLinearMovement(fakeFinalPosition, 20);
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
					
					// Validamos el comando
					if( ValidateCommandReceived(movementCommandType, movementCommandCode, message) )
					{
						strcpy(commandReceived, USB_In_Buffer);
						machineState = PROCESSINGCOMMAND;
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
						
							 if( freeCode == 0)	{ StepOn('X', true);  }
						else if( freeCode == 1) { StepOn('X', false); }
						else if( freeCode == 2) { StepOn('Y', true);  }
						else if( freeCode == 3) { StepOn('Y', false); }
						else if( freeCode == 4) { StepOn('Z', true);  }
						else if( freeCode == 5) { StepOn('Z', false); }
						
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
