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

char gCode = -2, mCode = -2, freeCode = -2;
char commandReceived[64];
bool_t limitSensorX = false, limitSensorY = false, limitSensorZ = false;
bool_t programPaused = false;//, configured = false;

// Actual Steps Position
stepsPosition_t currentStepsPosition;

/********************************************************************/
/*				Vectors of G & M functions supported				*/
/********************************************************************/
void CustomG(char code[])
{
	int d = HasValueParameter('D', code) ? GetValueParameter('D', code) : MAXFEEDRATE;
	ProcessLinearMovement(GetTargetStepsPosition(code), d);
}
void G04(char code[])
{
	int miliseconds = GetValueParameter('P', code);
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
_func gCodes[5] = {	NULL,	NULL,	NULL,	NULL,	G04	};
int gCodesCount = sizeof(gCodes) / sizeof(gCodes[4]);

// M functions Supported
void M00(char code[])
{
	programPaused = true;
}
void M02(char code[])
{
	machineState = CNCMATICCONNECTED;
}
void M03(char code[])
{
	LATBbits.LATB6 = 1;
}
void M04(char code[])
{
	LATBbits.LATB6 = 1;
}
void M05(char code[])
{
	LATBbits.LATB6 = 0;
}
// function array of MCode commands
_func mCodes[6] = {	M00,	NULL,	M02,	M03,	M04,	M05 };
int mCodesCount = sizeof(mCodes) / sizeof(mCodes[0]);

/********************************************************************/
/*	 Get the number right after the lette <name> inside <command>	*/
/********************************************************************/
int GetFreeCode(char string[])
{
	if( strstrrampgm(string, (const rom char far *)"+X") != NULL ) return 1;
	if( strstrrampgm(string, (const rom char far *)"-X") != NULL ) return 2;
	if( strstrrampgm(string, (const rom char far *)"+Y") != NULL ) return 3;
	if( strstrrampgm(string, (const rom char far *)"-Y") != NULL ) return 4;
	if( strstrrampgm(string, (const rom char far *)"+Z") != NULL ) return 5;
	if( strstrrampgm(string, (const rom char far *)"-Z") != NULL ) return 6;
	
	return -1;
}
int GetValueParameter(char name, char command[])
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
			return atoi(ptr);
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
				strcpypgm2ram(result, (const rom char far *)"CMDS|");
				*g = cmd;
				return true;
			}
			else
			{
				strcpypgm2ram(result, (const rom char far *)"ERR:CMDNS|");
				return false;
			}
		}
		if( (type == 'M') )
		{
			// CHEQUEAR QUE PASA SI SE SOBREPASA DEL TAMAÑO DEL ARRAY
			if( (cmd < mCodesCount) && (mCodes[cmd] != NULL) )
			{
				strcpypgm2ram(result, (const rom char far *)"CMDS|");
				*m = cmd;
				return true;
			}
			else
			{
				strcpypgm2ram(result, (const rom char far *)"ERR:CMDNS|");
				return false;
			}
		}
	}
	// to handle custom g code
	if( (type == 'G') && (atoi(code) == -1) )
	{
		strcpypgm2ram(result, (const rom char far *)"CMDS|");
		*g = atoi(code);
		return true;
	}
	strcpypgm2ram(result, (const rom char far *)"ERR:CMDE|");
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

/************************************************************************************/
/*							Process the Linear Movement								*/
/************************************************************************************/
void ProcessLinearMovement(stepsPosition_t targetStepsPosition, long delay)
{
	long xCounter, yCounter, zCounter;
	long maxDeltaSteps;
	bool_t xClock = false, yClock = false, zClock = false;
	bool_t xClockwise, yClockwise, zClockwise;
	stepsPosition_t deltaStepsPosition;
	
	if (targetStepsPosition.x > currentStepsPosition.x)
	{
		deltaStepsPosition.x = targetStepsPosition.x - currentStepsPosition.x;
		xClockwise = false;
		LATDbits.LATD2 = 0;
	}
	else
	{
		deltaStepsPosition.x = currentStepsPosition.x - targetStepsPosition.x;
		xClockwise = true;
		LATDbits.LATD2 = 1;
	}
	
	if(targetStepsPosition.y > currentStepsPosition.y)
	{
		deltaStepsPosition.y = targetStepsPosition.y - currentStepsPosition.y;
		yClockwise = true;
		LATCbits.LATC1 = 1;
	}
	else
	{
		deltaStepsPosition.y = currentStepsPosition.y - targetStepsPosition.y;
		yClockwise = false;
		LATCbits.LATC1 = 0;
	}
	
	if(targetStepsPosition.z > currentStepsPosition.z)
	{
		deltaStepsPosition.z = targetStepsPosition.z - currentStepsPosition.z;
		zClockwise = false;
		LATAbits.LATA1 = 0;
	}
	else
	{
		deltaStepsPosition.z = currentStepsPosition.z - targetStepsPosition.z;
		zClockwise = true;
		LATAbits.LATA1 = 1;
	}
	
	maxDeltaSteps = deltaStepsPosition.x > deltaStepsPosition.y ? deltaStepsPosition.x : deltaStepsPosition.y;
	maxDeltaSteps = maxDeltaSteps > deltaStepsPosition.z ? maxDeltaSteps : deltaStepsPosition.z;
	xCounter = (long)ceil(-maxDeltaSteps / 2);
	yCounter = (long)ceil(-maxDeltaSteps / 2);
	zCounter = (long)ceil(-maxDeltaSteps / 2);
	
	// seteo a 1 el enable de los motores
	LATEbits.LATE2 = 1;
	while( (targetStepsPosition.x != currentStepsPosition.x) || (targetStepsPosition.y != currentStepsPosition.y) || (targetStepsPosition.z != currentStepsPosition.z) )
	{
		// emergency stop
		//if( PORTBbits.RB0 ) { goto emergencyStop; }
		
		if(targetStepsPosition.x != currentStepsPosition.x)
		{
			xCounter += deltaStepsPosition.x;
			if(xCounter > 0)
			{
				if( xClock )
				{
					LATDbits.LATD4 = 0;
					xClock = false;
					// limit sensor AXIS X
					if( PORTBbits.RB1 ) { goto limitSensorAxisX; }
					
					xCounter -= maxDeltaSteps;
					if(xClockwise) currentStepsPosition.x--; else currentStepsPosition.x++;
				}
				else
				{
					xCounter -= maxDeltaSteps;
					LATDbits.LATD4 = 1;
					xClock = true;
				}
			}			
		}
		
		if(targetStepsPosition.y != currentStepsPosition.y)
		{
			yCounter += deltaStepsPosition.y;
			if(yCounter > 0)
			{
				if( yClock )
				{
					LATCbits.LATC2 = 0;
					yClock = false;
					// limit sensor AXIS Y
					if( PORTBbits.RB2 ) { goto limitSensorAxisY; }
					
					yCounter -= maxDeltaSteps;
					if(yClockwise) currentStepsPosition.y++; else currentStepsPosition.y--;
				}
				else
				{
					yCounter -= maxDeltaSteps;
					LATCbits.LATC2 = 1;
					yClock = true;
				}
			}
		}
		
		if(targetStepsPosition.z != currentStepsPosition.z)
		{
			zCounter += deltaStepsPosition.z;
			if(zCounter > 0)
			{
				if( zClock )
				{
					LATAbits.LATA2 = 0;
					zClock = false;
					// limit sensor AXIS Y
					if( PORTBbits.RB3 ) { goto limitSensorAxisZ; }
					
					zCounter -= maxDeltaSteps;
					if(zClockwise) currentStepsPosition.z--; else currentStepsPosition.z++;
				}
				else
				{
					zCounter -= maxDeltaSteps;
					LATAbits.LATA2 = 1;
					zClock = true;
				}
			}	
		}
		
		//wait for next step.
		Delay1MSx(delay);
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
		LATEbits.LATE2 = 0;
}

/*********************************************************************************/
/*				Get the final position considering the <code> received			 */
/*********************************************************************************/
stepsPosition_t GetTargetStepsPosition(char code[])
{
	stepsPosition_t position;
	
	position.x = HasValueParameter('X', code) ? GetValueParameter('X', code) : currentStepsPosition.x;
	position.y = HasValueParameter('Y', code) ? GetValueParameter('Y', code) : currentStepsPosition.y;
	position.z = HasValueParameter('Z', code) ? GetValueParameter('Z', code) : currentStepsPosition.z;
	
	return position;
}

/********************************************************/
/*		Functions to handle limitSensors activation		*/
/********************************************************/
void limitSensorAxisXHandler()
{
	// invierto el sentido de giro
	if(LATDbits.LATD2)
	{
		LATDbits.LATD2 = 0;
	}
	else
	{
		LATDbits.LATD2 = 1;
	}

	do
	{
		// tiro un paso
		LATDbits.LATD4 = 1;
		Delay1MSx(1);
		LATDbits.LATD4 = 0;
		Delay1MSx(1);
		
		if(machineState != CNCMATICCONNECTED)
		{
			currentStepsPosition.x += LATDbits.LATD2 ? -1 : 1;
		}
	} while( PORTBbits.RB1 );
	
	machineState = LIMITSENSOR;
	limitSensorX = true;
}
void limitSensorAxisYHandler()
{
	// invierto el sentido de giro
	if(LATCbits.LATC1)
	{
		LATCbits.LATC1 = 0;
	}
	else
	{
		LATCbits.LATC1 = 1;
	}
	
	do
	{
		// tiro un paso
		LATCbits.LATC2 = 1;
		Delay1MSx(1);
		LATCbits.LATC2 = 0;
		Delay1MSx(1);
		
		if(machineState != CNCMATICCONNECTED)
		{
			currentStepsPosition.y += LATCbits.LATC1 ? 1 : -1;
		}
	} while(PORTBbits.RB2);

	machineState = LIMITSENSOR;
	limitSensorY = true;
}
void limitSensorAxisZHandler()
{
	// invierto el sentido de giro
	if(PORTAbits.RA1)
	{
		LATAbits.LATA1 = 0;
	}
	else
	{
		LATAbits.LATA1 = 1;
	}
	
	do
	{
		// tiro un paso
		LATAbits.LATA2 = 1;
		Delay1MSx(1);
		LATAbits.LATA2 = 0;
		Delay1MSx(1);
		
		if(machineState != CNCMATICCONNECTED)
		{
			currentStepsPosition.z += PORTAbits.RA1 ? -1 : 1;
		}		
	} while(PORTBbits.RB3);

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
void StepOnX(int clockwise)
{
	char mesage[2];
	
	if(clockwise == 1) LATDbits.LATD2 = 1; else LATDbits.LATD2 = 0;
	
	// tiro un paso
	LATDbits.LATD4 = 1;
	Delay1MSx(1);
	LATDbits.LATD4 = 0;
	Delay1MSx(1);
	
	if(machineState != CNCMATICCONNECTED)
	{
		if(clockwise == 1) currentStepsPosition.x--; else currentStepsPosition.x++;
	}
	
	// limit sensor AXIS X
	if( PORTBbits.RB1 ) { limitSensorAxisXHandler(); }
}

void StepOnY(int clockwise)
{
	if(clockwise == 1) LATCbits.LATC1 = 1; else LATCbits.LATC1 = 0;
	
	// tiro un paso
	LATCbits.LATC2 = 1;
	Delay1MSx(1);
	LATCbits.LATC2 = 0;
	Delay1MSx(1);
	
	if(machineState != CNCMATICCONNECTED)
	{
		if(clockwise == 1) currentStepsPosition.y++; else currentStepsPosition.y--;
	}
	
	// limit sensor AXIS Y
	if( PORTBbits.RB2 ) { limitSensorAxisYHandler(); }
}

void StepOnZ(int clockwise)
{
	if(clockwise == 1) LATAbits.LATA1 = 1; else LATAbits.LATA1 = 0;
	
	// tiro un paso
	LATAbits.LATA2 = 1;
	Delay1MSx(1);
	LATAbits.LATA2 = 0;
	Delay1MSx(1);
	
	if(machineState != CNCMATICCONNECTED)
	{
		if(clockwise == 1) currentStepsPosition.z--; else currentStepsPosition.z++;
	}
	
	// limit sensor AXIS Z
	if( PORTBbits.RB3 ) { limitSensorAxisZHandler(); }
}

/********************************************************/
/*		Function to move to origin axis by axis 		*/
/********************************************************/
void MoveToOrigin()
{
	// seteo a 1 el enable de los motores
	LATEbits.LATE2 = 1;
	
	machineState = CNCMATICCONNECTED;
	/*while(!limitSensorZ && !PORTBbits.RB0)*/
	while(!limitSensorZ)
	{
		StepOnZ(1);
	}
	machineState = CNCMATICCONNECTED;
	/*while(!limitSensorY && !PORTBbits.RB0)*/
	while(!limitSensorY)
	{
		StepOnY(0);
	}
	machineState = CNCMATICCONNECTED;
	/*while(!limitSensorX && !PORTBbits.RB0)*/
	while(!limitSensorX)
	{
		StepOnX(1);
	}
	// emergency stop
	/*if( PORTBbits.RB0 ) { goto emergencyStop; }*/
	
	limitSensorX = limitSensorY = limitSensorZ = false;
	currentStepsPosition = CreateStepsPosition(0, 0, 0);
	/*goto end;
	
	emergencyStop:
		emergencyStopHandler();
		goto end;
	end:*/
	// seteo a 0 el enable de los motores
	LATEbits.LATE2 = 0;
}

void user(void)
{
	BYTE numBytesRead;
	char message[64];
	char movementCommandCode[3], movementCommandType;
	double stepDegrees, distancePerRevolution;
	
	//Blink the LEDs according to the USB device status
	//BlinkUSBStatus();
	
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
			
			// RETURN CURRENTSTEPS POSITION
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"position"))
			{
				sprintf(message, (const rom char far *)"X%ld Y%ld Z%ld|", currentStepsPosition.x, currentStepsPosition.y, currentStepsPosition.z);
				putUSBUSART(message, strlen(message));
				goto endUser;
			}
			
			// RESET CNC - stat: SERIALPORTCONNECTED
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"reset"))
			{
				limitSensorX = limitSensorY = limitSensorZ = programPaused = false; //configured = programPaused = false;
				strcpypgm2ram(message, (const rom char far *)"CNCR|");
				putUSBUSART(message, strlen(message));
				machineState = SERIALPORTCONNECTED;
				goto endUser;
			}
			
			// RETURN CNC STATE
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"status"))
			{
				sprintf(message, (const rom char far *)"CNCS:%d|", machineState);
				putUSBUSART(message, strlen(message));
				goto endUser;
			}
			
			// MOVE TO ORIGIN: absolute 0,0,0
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"origin"))
			{
				if(programPaused)
				{
					strcpypgm2ram(message, (const rom char far *)"ERR:PP|");
					putUSBUSART(message, strlen(message));
					goto endUser;
				}
				else
				{
					MoveToOrigin();
					
					if(machineState == EMERGENCYSTOP)
					{
						sprintf(message, (const rom char far *)"ERR:PE|");
						machineState = SERIALPORTCONNECTED;
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"PO|");
						machineState = WAITINGCOMMAND;
					}
					
					putUSBUSART(message, strlen(message));
					goto endUser;
				}
			}
			
			// START FREEMOVES
			if(strstrrampgm(USB_In_Buffer, (const rom char far *)"FM:"))
			{
				if(programPaused)
				{
					strcpypgm2ram(message, (const rom char far *)"ERR:PP|");
					putUSBUSART(message, strlen(message));
					goto endUser;
				}
				else
				{
					freeCode = GetFreeCode(USB_In_Buffer);
					if(freeCode != -2)
					{
						strcpypgm2ram(message, (const rom char far *)"CNCFM|");
						machineState = FREEMOVES;
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"ERR:CNCFM|");
					}
					putUSBUSART(message, strlen(message));
					goto endUser;
				}
			}
			
			// STOP FREEMOVES
			if( (machineState == FREEMOVES) && (!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"stop")) )
			{
				freeCode = -2;
				sprintf(message, (const rom char far *)"CNCSFM_X%ld Y%ld Z%ld|", currentStepsPosition.x, currentStepsPosition.y, currentStepsPosition.z);
				putUSBUSART(message, strlen(message));
				machineState = WAITINGCOMMAND;
				goto endUser;
			}
			
			switch(machineState)
			{
				case SERIALPORTCONNECTED:
					// Count characters received and send this number to PC
					sprintf(message, (const rom char far *)"%d|", numBytesRead);
					putUSBUSART(message, strlen(message));
					machineState = HANDSHAKEACKRECEIVED;
					break;
					
				case HANDSHAKEACKRECEIVED:
					// Compare confirmation message.
					if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"ok"))
					{
						strcpypgm2ram(message, (const rom char far *)"MC|");
						machineState = CNCMATICCONNECTED;
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"ERR:MNC|");
						machineState = SERIALPORTCONNECTED;
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
					if(freeCode != -2)
					{
						// seteo a 1 el enable de los motores
						LATEbits.LATE2 = 1;
						
							 if( freeCode == 1)	{	StepOnX(0);	}
						else if( freeCode == 2) {	StepOnX(1);	}
						else if( freeCode == 3) {	StepOnY(1);	}
						else if( freeCode == 4) {	StepOnY(0);	}
						else if( freeCode == 5) {	StepOnZ(0);	}
						else if( freeCode == 6) {	StepOnZ(1);	}
						
						if(machineState == LIMITSENSOR)
						{
							freeCode = -2;
							limitSensorX = limitSensorY = limitSensorZ = false;
							sprintf(message, (const rom char far *)"ERR:SFC_X%ld Y%ld Z%ld|", currentStepsPosition.x, currentStepsPosition.y, currentStepsPosition.z);
							putUSBUSART(message, strlen(message));
						}						
					
						// seteo a 0 el enable de los motores
						LATEbits.LATE2 = 0;
					}
					break;

				case CNCMATICCONNECTED:
					MoveToOrigin();
					if(machineState == EMERGENCYSTOP)
					{
						sprintf(message, (const rom char far *)"ERR:PE|");
						machineState = SERIALPORTCONNECTED;
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"PO|");
						machineState = WAITINGCOMMAND;
					}
					putUSBUSART(message, strlen(message));
					break;
					
				case PROCESSINGCOMMAND:
					// Processing command received
					if(gCode != -2)
					{
						if(gCode == -1)
						{
							CustomG(commandReceived);
						}
						else
						{
							gCodes[gCode](commandReceived);
						}
					}
					if(mCode != -2) { mCodes[mCode](commandReceived); }
					
					// Chequeamos machineState -> si se activo algun fin de carrera
					if(machineState == LIMITSENSOR)
					{
						limitSensorX = limitSensorY = limitSensorZ = false;
						sprintf(message, (const rom char far *)"ERR:SFC_X%ld Y%ld Z%ld|", currentStepsPosition.x, currentStepsPosition.y, currentStepsPosition.z);
						machineState = SERIALPORTCONNECTED;
					}
					else if(machineState == EMERGENCYSTOP)
					{
						sprintf(message, (const rom char far *)"ERR:PE_X%ld Y%ld Z%ld|", currentStepsPosition.x, currentStepsPosition.y, currentStepsPosition.z);
						machineState = SERIALPORTCONNECTED;
					}
					else
					{
						sprintf(message, (const rom char far *)"CMDDONE_X%ld Y%ld Z%ld|", currentStepsPosition.x, currentStepsPosition.y, currentStepsPosition.z);
						if(mCode != 2) machineState = WAITINGCOMMAND;
					}
					putUSBUSART(message, strlen(message));
					// reseteamos variables de comando
					gCode = mCode = -2;
					break;
						
				default:
					break;	
			}
		}
	}
	endUser:
		CDCTxService();
}
