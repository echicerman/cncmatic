#include "p18f4550.h"
#include "./USB/usb.h"
#include "./USB/usb_function_cdc.h"
#include "main.h"
#include "user.h"
#include <string.h>
#include <math.h>
/*
const rom char *configOk[14]	= "Configured OK";
const rom char *receptOk[12]	= "Received OK";
const rom char *positionOk[17]	= "Positioned OK";
const rom char *commandOk[18]	= "Command Supported";
const rom char *movementOk[12]	= "Movement OK";
const rom char *programEnd[12]	= "Program End";

const rom char *limitSensor[22]	= "Limit Sensor Achieved";
const rom char *commandFail[22]	= "Command Not Supported";
const rom char *fail[21]		= "Something Went Wrong";
*/

//int machineState = 0;

state_t machineState = SERIALPORTCONNECTED;
config_t configuracion[3];
bool_t  commandFailure = FALSE;
position_t currentPosition;

void LimitSensor(void)
{
	;
}

void Move(char command[])
{
	unsigned char i, count = strlen(command);
	int/*unsigned short long */xSteps = 0, ySteps = 0, zSteps = 0, xClock, yClock, zClock, clock = 0, xNextStep = 0, yNextStep = 0, zNextStep = 0;
	int/*float*/ totalDistance, totalTime;
	char *ptr;
	position_t finalPosition;
	
	// Calculamos los pasos que se deben dar en cada eje
	// pasos = ( distancia * ( 360 / gradosPorPaso) ) / distanciaPorVuelta
	for(i = 0; i < count; i++)
	{
		if( !xSteps && command[i] == 'X')
		{
			ptr = &command[i + 1];
			for(++i; (command[i] != ' ') && (i < count); i++) ;
			command[i] = '\0';
			xSteps = ( atoi(ptr) * ( 360 / configuracion[0].stepDegrees ) ) / configuracion[0].distancePerRevolution;
		}
		
		if( !ySteps && command[i] == 'Y')
		{
			ptr = &command[i + 1];
			for(++i; (command[i] != ' ') && (i < count); i++) ;
			command[i] = '\0';
			ySteps = ( atoi(ptr) * ( 360 / configuracion[1].stepDegrees ) ) / configuracion[1].distancePerRevolution;
		}
		
		if( !zSteps && command[i] == 'Z')
		{
			ptr = &command[i + 1];
			for(++i; (command[i] != ' ') && (i < count); i++) ;
			command[i] = '\0';
			zSteps = ( atoi(ptr) * ( 360 / configuracion[2].stepDegrees ) ) / configuracion[2].distancePerRevolution;
		}
	}
	
	// ABSOLUTA O RELATIVA ¿?
	
	// Calculamos la distancia total del movimiento
	// distanciaTotal = sqrt ( pasosX^2 + pasosY^2 + pasosZ^2 )
	totalDistance = sqrt( pow(xSteps, 2) + pow(ySteps, 2) + pow(zSteps, 2) );
	// Calculamos la distancia total del movimiento
	// tiempoTotal = distancia / velocidad -> pasos x segundo
	totalTime = totalDistance / 100;
	
	// Calculamos la frecuencia de cambio de estado de bit de paso por cada motor
	// frecuencia = tiempoTotal / pasosTotal / 2 -> se realiza un paso por flanco descendente; dividiendo por 2 nos indica la frecuencia para cambiar de estado: de 1 a 0; y de 0 a 1.
	xClock = totalTime / xSteps / 2;
	yClock = totalTime / ySteps / 2;
	zClock = totalTime / zSteps / 2;	
	
	// Seteamos posición final
	finalPosition.x = currentPosition.x + xSteps;
	finalPosition.y = currentPosition.y + ySteps;
	finalPosition.z = currentPosition.z + zSteps;

	// Enable PortB Interrupts
	INTCONbits.RBIE = 1;
	
	// Mientras no lleguemos a la posicion final en los 3 ejes, tenemos que hacer girar algún motor
	while( (finalPosition.x != currentPosition.x) || (finalPosition.y != currentPosition.y) || (finalPosition.z != currentPosition.z) )
	{
		// si el clock es mayor o igual al clock del próximo paso del motor y si no llegué a la posición final
		if( (clock >= xNextStep) && finalPosition.x != currentPosition.x )
		{
			if( LATAbits.LATA2 )
			{
				LATAbits.LATA2 = 0;
				currentPosition.x++;
			}
			else
			{
				LATAbits.LATA2 = 1;
			}
			xNextStep += xClock;
		}
		
		if( (clock >= yNextStep) && finalPosition.y != currentPosition.y )
		{
			if( LATCbits.LATC2 )
			{
				LATCbits.LATC2 = 0;
				currentPosition.y++;
			}
			else
			{
				LATCbits.LATC2 = 1;
			}
			yNextStep += yClock;
		}
		
		if( (clock >= zNextStep) && finalPosition.z != currentPosition.z )
		{
			if( LATDbits.LATD4 )
			{
				LATDbits.LATD4 = 0;
				currentPosition.z++;
			}
			else
			{
				LATDbits.LATD4 = 1;
			}
			zNextStep += zClock;
		}
		clock++;
	}
	
	// Disable PortB Interrupts
	INTCONbits.RBIE = 0;
}

void user(void)
{
	BYTE numBytesRead;
	char *configPtr, *movementPtr, message[20], stringAux[64];
	char movementCommand[3];
	unsigned char motor = 0, gCommand = -1, mCommand = -1;

	//Blink the LEDs according to the USB device status
	/*BlinkUSBStatus();*/
	// User Application USB tasks
	if((USBDeviceState < CONFIGURED_STATE)||(USBSuspendControl==1)) return;

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
			strcpy(stringAux, USB_In_Buffer);
			
			switch(machineState)
			{
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
					configPtr = strtokpgmram(stringAux, (const rom char far *)";");
					configuracion[motor].stepDegrees = atoi(configPtr);
					configPtr = strtokpgmram(NULL, (const rom char far *)";");
					configuracion[motor].distancePerRevolution = atoi(configPtr);
					while( (++motor ) && ( (configPtr = strtokpgmram( NULL, (const rom char far *)";" )) != NULL ) )    // Posteriores llamadas
					{						
						configuracion[motor].stepDegrees = atoi(configPtr);
						configPtr = strtokpgmram(NULL, (const rom char far *)";");
						configuracion[motor].distancePerRevolution = atoi(configPtr);
					}
					
					if(motor == 3)
					{
						// if it has been configured all 3 engines
						strcpypgm2ram(message, (const rom char far *)"Configuration OK");
						putUSBUSART(message, strlen(message));
						machineState = CONFIGURED;
						
						// TODO: Mover punta a (0;0;0)
						currentPosition.x = 0;
						currentPosition.y = 0;
						currentPosition.z = 0;
						//*******************************
						
						machineState = WAITINGCOMMAND;
						strcpypgm2ram(message, (const rom char far *)"Origin Position Set");
						putUSBUSART(message, strlen(message));
					}
					else
					{
						// not all 3 engines had been configured correctly
						strcpypgm2ram(message, (const rom char far *)"Configuration FAIL");
						putUSBUSART(message, strlen(message));
						machineState = CNCMATICCONNECTED;
					}
					break;
					
				case CONFIGURED:
					// Moving to (0;0;0)
					break;
					
				case WAITINGCOMMAND:
					movementPtr = strtokpgmram(stringAux, (const rom char far *)" ");
					movementCommand[0] = movementPtr[1];
					movementCommand[1] = movementPtr[2];
					movementCommand[2] = '\0';
					
					if(movementPtr[0] == 'G')
						gCommand = atoi(movementCommand);
					else if(movementPtr[0] == 'M')
						mCommand = atoi(movementCommand);
						
					switch( gCommand )
					{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 17:
						case 18:
						case 19:
						case 20:
						case 21:
						case 90:
						case 91:
							commandFailure = FALSE;
							strcpypgm2ram(message, (const rom char far *)"Command OK");
							putUSBUSART(message, strlen(message));
							machineState = PROCESSINGCOMMAND;
							Move(USB_In_Buffer);
							break;
						
						default:
							switch (mCommand)
							{
								case 0:
								case 2:
									commandFailure = FALSE;
									strcpypgm2ram(message, (const rom char far *)"Command OK");
									putUSBUSART(message, strlen(message));
									machineState = PROCESSINGCOMMAND;
									break;
								
								default:
								strcpypgm2ram(message, (const rom char far *)"Command Not Supported");
								putUSBUSART(message, strlen(message));
								if(commandFailure)
								{
									// TODO: Mover punta a (0;0;0)
									currentPosition.x = 0;
									currentPosition.y = 0;
									currentPosition.z = 0;
									//********************************
							
									machineState = WAITINGCOMMAND;
									strcpypgm2ram(message, (const rom char far *)"Origin Position Set");
									putUSBUSART(message, strlen(message));
									commandFailure = FALSE;
								}
								else
									commandFailure = TRUE;
								break;
							}
							break;
					}
					break;
					
				 case PROCESSINGCOMMAND:
					// Processing command received
					machineState = WAITINGCOMMAND;
					break;
			}
		}
	}
	CDCTxService();
}