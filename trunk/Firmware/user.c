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

#define MAXFEEDRATE 60 

state_t machineState = SERIALPORTCONNECTED;

char gCode = -1, mCode = -1, freeCode = -1;
char commandReceived[64];
bool_t limitSensorX = false, limitSensorY = false, limitSensorZ = false;
bool_t programPaused = false;//, configured = false;

// Configuration: milimeters
enginesConfig_t mmConfiguration;

// Actual Position & Actual Steps
position_t currentPosition;
stepsPosition_t currentSteps;

/********************************************************************/
/*				Vectors of G & M functions supported				*/
/********************************************************************/
// G functions Supported
void G00(char code[])
{
	double f = HasValueParameter('F', code) ? GetValueParameter('F', code) : MAXFEEDRATE;
	ProcessLinearMovement(GetTargetPosition(code), f);
}

void G01(char code[])
{
	double f = HasValueParameter('F', code) ? GetValueParameter('F', code) : MAXFEEDRATE;
	ProcessLinearMovement(GetTargetPosition(code), f);
}
void G04(char code[])
{
	double miliseconds = GetValueParameter('P', code);
	if(miliseconds < 1000)
	{
		Delay1MSx(miliseconds);
	}
	else
	{
		Delay1Sx((int)miliseconds/1000);
	}
}

// function array of GCode commands
_func gCodes[5] = {	G00,	G01,	NULL,	NULL,	G04	};
int gCodesCount = sizeof(gCodes) / sizeof(gCodes[0]);

// M functions Supported
void M00(char code[])
{
	programPaused = true;
}
void M02(char code[])
{
	machineState = CNCMATICCONNECTED;
}
// function array of MCode commands
_func mCodes[3] = {	M00,	NULL,	M02	};
int mCodesCount = sizeof(mCodes) / sizeof(mCodes[0]);

/********************************************************************/
/*	 Get the number right after the lette <name> inside <command>	*/
/********************************************************************/
int GetFreeCode(char string[])
{
	if( strstrrampgm(string, (const rom char far *)"+X") ) return 0;
	if( strstrrampgm(string, (const rom char far *)"-X") ) return 1;
	if( strstrrampgm(string, (const rom char far *)"+Y") ) return 2;
	if( strstrrampgm(string, (const rom char far *)"-Y") ) return 3;
	if( strstrrampgm(string, (const rom char far *)"+Z") ) return 4;
	if( strstrrampgm(string, (const rom char far *)"-Z") ) return 5;
	
	return -1;
}
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
	
	//configured = true;
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
bool_t ValidateCommandReceived(char type, char code[], char result[], char* g, char* m)
{
	if(isNumber(code))
	{
		int cmd = atoi(code);
		if( (type == 'G') )
		{
			// CHEQUEAR QUE PASA SI SE SOBREPASA DEL TAMAÑO DEL ARRAY
			if( (cmd < gCodesCount) && (gCodes[cmd] != NULL) )
			{
				strcpypgm2ram(result, (const rom char far *)"CMDS");
				*g = cmd;
				return true;
			}
			else
			{
				strcpypgm2ram(result, (const rom char far *)"ERR:CMDNS");
				return false;
			}
		}
		if( (type == 'M') )
		{
			// CHEQUEAR QUE PASA SI SE SOBREPASA DEL TAMAÑO DEL ARRAY
			if( (cmd < mCodesCount) && (mCodes[cmd] != NULL) )
			{
				strcpypgm2ram(result, (const rom char far *)"CMDS");
				*m = cmd;
				return true;
			}
			else
			{
				strcpypgm2ram(result, (const rom char far *)"ERR:CMDNS");
				return false;
			}
		}
	}
	strcpypgm2ram(result, (const rom char far *)"ERR:CMDE");
	return false;
}

/********************************************************************/
/*			Conversion / Creation of Steps & Position units		 	*/
/********************************************************************/
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
stepsPosition_t ToStepsPosition(double x, double y, double z)
{
	stepsPosition_t result;
	
	result.x = (unsigned long) ceil(x * mmConfiguration.step_units_axisX);
	result.y = (unsigned long) ceil(y * mmConfiguration.step_units_axisY);
	result.z = (unsigned long) ceil(z * mmConfiguration.step_units_axisZ);
	
	return result;
}
stepsPosition_t ToStepsPositionFrom(position_t position)
{
	stepsPosition_t result;
	result = ToStepsPosition(position.x, position.y, position.z);
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

/************************************************************************************/
/*							Process the Linear Movement								*/
/************************************************************************************/
void ProcessLinearMovement(position_t targetPosition, double feedrate)
{
	long xCounter, yCounter, zCounter;
	long maxDeltaSteps, delay;
	double totalDistance, xDelta, yDelta, zDelta;
	stepsPosition_t deltaStateChangesPosition;
	
	// Target Steps -> after this movement
	stepsPosition_t targetStepsPosition = ToStepsPositionFrom(targetPosition);
	
	// Delta Position & Delta Steps -> with this movement
	xDelta = targetPosition.x - currentPosition.x > 0 ? targetPosition.x - currentPosition.x : currentPosition.x - targetPosition.x;
	yDelta = targetPosition.y - currentPosition.y > 0 ? targetPosition.y - currentPosition.y : currentPosition.y - targetPosition.y;
	zDelta = targetPosition.z - currentPosition.z > 0 ? targetPosition.z - currentPosition.z : currentPosition.z - targetPosition.z;
	
	deltaStateChangesPosition = ToStepsPosition(xDelta, yDelta, zDelta);
	
	maxDeltaSteps = deltaStateChangesPosition.x > deltaStateChangesPosition.y ? deltaStateChangesPosition.x : deltaStateChangesPosition.y;
	maxDeltaSteps = maxDeltaSteps > deltaStateChangesPosition.z ? maxDeltaSteps : deltaStateChangesPosition.z;
	
	// Calculating delay
	totalDistance = sqrt(xDelta * xDelta + yDelta * yDelta + zDelta * zDelta);
	delay = ((totalDistance * 60000000.0) / feedrate) / maxDeltaSteps; // time between steps for most dinamyc axis - microseconds
	delay /= 2; // se necesitan 2 cambios de estado por cada paso
	
	xCounter = -maxDeltaSteps / 2;
	yCounter = -maxDeltaSteps / 2;
	zCounter = -maxDeltaSteps / 2;
	
	// Seteamos bit de sentido de giro
	PORTAbits.RA1 = (targetStepsPosition.x > currentSteps.x) ? 1 : 0;
	PORTCbits.RC1 = (targetStepsPosition.y > currentSteps.y) ? 1 : 0;
	PORTDbits.RD2 = (targetStepsPosition.z > currentSteps.z) ? 1 : 0;
	
	// seteo a 1 el enable de los motores
	PORTEbits.RE2 = 1;
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
					
					xCounter -= maxDeltaSteps;
					currentSteps.x += PORTAbits.RA1 ? 1 : -1;
				}
				else
				{
					xCounter -= maxDeltaSteps;
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
					
					yCounter -= maxDeltaSteps;
					currentSteps.y += PORTCbits.RC1 ? 1 : -1;
				}
				else
				{
					yCounter -= maxDeltaSteps;
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
					
					zCounter -= maxDeltaSteps;
					currentSteps.z += PORTDbits.RD2 ? 1 : -1;
				}
				else
				{
					zCounter -= maxDeltaSteps;
					PORTDbits.RD4 = 1;
				}
			}	
		}
		
		//wait for next step.
		if(delay > 1000)
			Delay1MSx((int)ceil(delay/1000));
		else
			Delay10msx((int)ceil(delay/10));
	}
	goto end;
	
	/* Handle LimitSensors and EmergencyStop button */
	limitSensorAxisX:
		limitSensorAxisXHandler();
		goto end;
	limitSensorAxisY:
		limitSensorAxisYHandler();
		goto end;
	limitSensorAxisZ:
		limitSensorAxisZHandler();
		goto end;
	emergencyStop:
		emergencyStopHandler();
		goto end;
	end:
		// seteo a 0 el enable de los motores
		PORTEbits.RE2 = 0;
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
void limitSensorAxisXHandler()
{
	PORTAbits.RA1 = ~PORTAbits.RA1;
	// tiro un paso
	PORTAbits.RA2 = 1;
	Delay1MSx(10);
	PORTAbits.RA2 = 0;
	
	machineState = LIMITSENSOR;
	limitSensorX = true;
}
void limitSensorAxisYHandler()
{
	PORTCbits.RC1 = ~PORTCbits.RC1;
	// tiro un paso
	PORTCbits.RC2 = 1;
	Delay1MSx(10);
	PORTAbits.RA2 = 0;

	machineState = LIMITSENSOR;
	limitSensorY = true;
}
void limitSensorAxisZHandler()
{
	// invierto el sentido de giro
	PORTDbits.RD2 = ~PORTDbits.RD2;
	// tiro un paso
	PORTDbits.RD4 = 1;
	Delay1MSx(10);
	PORTDbits.RD4 = 0;

	machineState = LIMITSENSOR;
	limitSensorZ = true;
}
void emergencyStopHandler()
{
	machineState = EMERGENCYSTOP;
}

/********************************************************/
/*		Functions to move over a specified axis 		*/
/********************************************************/
void StepOnX(bool_t clockwise)
{
	PORTAbits.RA1 = clockwise;
	// tiro un paso
	PORTAbits.RA2 = 1;
	Delay1MSx(10);
	PORTAbits.RA2 = 0;
	
	// limit sensor AXIS X
	if( !PORTBbits.RB4 ) { limitSensorAxisXHandler(); }
}

void StepOnY(bool_t clockwise)
{
	PORTCbits.RC1 = clockwise;
	// tiro un paso
	PORTCbits.RC2 = 1;
	Delay1MSx(10);
	PORTCbits.RC2 = 0;
	
	// limit sensor AXIS Y
	if( !PORTBbits.RB5 ) { limitSensorAxisYHandler(); }
}

void StepOnZ(bool_t clockwise)
{
	PORTDbits.RD2 = clockwise;
	// tiro un paso
	PORTDbits.RD4 = 1;
	Delay1MSx(10);
	PORTDbits.RD4 = 0;
	
	// limit sensor AXIS Z
	if( !PORTBbits.RB6 ) { limitSensorAxisZHandler(); }
}

/********************************************************/
/*		Function to move to origin axis by axis 		*/
/********************************************************/
void MoveToOrigin()
{
	// seteo a 1 el enable de los motores
	PORTEbits.RE2 = 1;
	
	while(!limitSensorZ && !PORTBbits.RB7)
	{
		StepOnZ(false);
	}
	while(!limitSensorY && !PORTBbits.RB7)
	{
		StepOnY(false);
	}
	while(!limitSensorX && !PORTBbits.RB7)
	{
		StepOnX(false);
	}
	// emergency stop
	if( PORTBbits.RB7 ) { goto emergencyStop; }
	
	limitSensorX = limitSensorY = limitSensorZ = false;
	currentSteps = CreateStepsPosition(0, 0, 0);
	currentPosition = CreatePosition(0.0, 0.0, 0.0);
	goto end;
	
	emergencyStop:
		emergencyStopHandler();
		goto end;
	end:
		// seteo a 0 el enable de los motores
		PORTEbits.RE2 = 0;
}

void user(void)
{
	BYTE numBytesRead;
	char message[54];
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
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"position"))
			{
				// Return currentSteps position
				sprintf(message, (const rom char far *)"X%ld Y%ld Z%ld", currentSteps.x, currentSteps.y, currentSteps.z);
				putUSBUSART(message, strlen(message));
				goto endUser;
			}
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"reset"))
			{
				// Resetear la maquina si recibimos el comando 'reset'
				limitSensorX = limitSensorY = limitSensorZ = programPaused = false; //configured = programPaused = false;
				strcpypgm2ram(message, (const rom char far *)"CNCR");
				putUSBUSART(message, strlen(message));
				machineState = SERIALPORTCONNECTED;
				goto endUser;
			}
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"status"))
			{
				// devolvemos el etado en el que esta la maquina
				sprintf(message, (const rom char far *)"CNCS:%d", machineState);
				putUSBUSART(message, strlen(message));
				goto endUser;
			}
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"origin"))
			{
				if(programPaused)
				{
					strcpypgm2ram(message, (const rom char far *)"ERR:PP");
					putUSBUSART(message, strlen(message));
					goto endUser;
				}
				else
				{
					MoveToOrigin();
					
					if(machineState == EMERGENCYSTOP)
					{
						sprintf(message, (const rom char far *)"ERR:PE");
						machineState = SERIALPORTCONNECTED;
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"PO");
					}
					
					putUSBUSART(message, strlen(message));
					goto endUser;
				}
			}
			
			if(strstrrampgm(USB_In_Buffer, (const rom char far *)"FM:"))
			{
				if(programPaused)
				{
					strcpypgm2ram(message, (const rom char far *)"ERR:PP");
					putUSBUSART(message, strlen(message));
					goto endUser;
				}
				else
				{
					freeCode = GetFreeCode(USB_In_Buffer);
					if(freeCode != -1)
					{
						strcpypgm2ram(message, (const rom char far *)"CNCFM");
						machineState = FREEMOVES;
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"ERR:CNCFM");
					}
					putUSBUSART(message, strlen(message));
					goto endUser;
				}
			}
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"stop"))
			{
				freeCode = -1;
				sprintf(message, (const rom char far *)"CNCSFM_X%ld Y%ld Z%ld", currentSteps.x, currentSteps.y, currentSteps.z);
				putUSBUSART(message, strlen(message));
				machineState = READYTOCONFIGURE;
				goto endUser;
			}
			
			switch(machineState)
			{
				case SERIALPORTCONNECTED:
					// Count characters received and send this number to PC
					sprintf(message, (const rom char far *)"%d", numBytesRead);
					putUSBUSART(message, strlen(message));
					machineState = HANDSHAKEACKRECEIVED;
					break;
					
				case HANDSHAKEACKRECEIVED:
					// Compare confirmation message.
					if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"ok"))
					{
						strcpypgm2ram(message, (const rom char far *)"MC");
						machineState = CNCMATICCONNECTED;
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"ERR:MNC");
						machineState = SERIALPORTCONNECTED;
					}
						
					putUSBUSART(message, strlen(message));
					break;
					
				case READYTOCONFIGURE:
					// tokenize the configuration string
					if( ConfigureMachine(USB_In_Buffer) )
					{
						strcpypgm2ram(message, (const rom char far *)"CFGOK");
						machineState = WAITINGCOMMAND;
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"ERR:CFGE");
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
					if( ValidateCommandReceived(movementCommandType, movementCommandCode, message, &gCode, &mCode) )
					{
						strcpy(commandReceived, USB_In_Buffer);
						machineState = PROCESSINGCOMMAND;
						programPaused = false;
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
						// seteo a 1 el enable de los motores
						PORTEbits.RE2 = 1;
						
							 if( freeCode == 0)	{ StepOnX(true);	currentSteps.x++; }
						else if( freeCode == 1) { StepOnX(false);	currentSteps.x--; }
						else if( freeCode == 2) { StepOnY(true);	currentSteps.y++; }
						else if( freeCode == 3) { StepOnY(false);	currentSteps.y--; }
						else if( freeCode == 4) { StepOnZ(true);	currentSteps.z++; }
						else if( freeCode == 5) { StepOnZ(false);	currentSteps.z--; }
						
						if(machineState == LIMITSENSOR)
						{
							// volvemos 1 paso para atrás por haber tocado el sensor de fin de carrera
							switch(freeCode)
							{
								case 0: currentSteps.x--; break;
								case 1: currentSteps.x++; break;
								case 2: currentSteps.y--; break;
								case 3: currentSteps.y++; break;
								case 4: currentSteps.z--; break;
								case 5: currentSteps.z++; break;
								default: break;
							}
							freeCode = -1;
							limitSensorX = limitSensorY = limitSensorZ = false;
							sprintf(message, (const rom char far *)"ERR:SFC_X%ld Y%ld Z%ld", currentSteps.x, currentSteps.y, currentSteps.z);
							putUSBUSART(message, strlen(message));
							machineState = READYTOCONFIGURE;
						}
						// seteo a 0 el enable de los motores
						PORTEbits.RE2 = 0;
					}
					break;

				case CNCMATICCONNECTED:
					MoveToOrigin();
					
					if(machineState == EMERGENCYSTOP)
					{
						sprintf(message, (const rom char far *)"ERR:PE");
						machineState = SERIALPORTCONNECTED;
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"PO");
						machineState = READYTOCONFIGURE;
					}
					
					putUSBUSART(message, strlen(message));
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
						limitSensorX = limitSensorY = limitSensorZ = false;
						sprintf(message, (const rom char far *)"ERR:SFC_X%ld Y%ld Z%ld", currentSteps.x, currentSteps.y, currentSteps.z);
					}
					else if(machineState == EMERGENCYSTOP)
					{
						sprintf(message, (const rom char far *)"ERR:PE_X%ld Y%ld Z%ld", currentSteps.x, currentSteps.y, currentSteps.z);
					}
					else
					{
						sprintf(message, (const rom char far *)"CMDDONE_X%ld Y%ld Z%ld", currentSteps.x, currentSteps.y, currentSteps.z);
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
