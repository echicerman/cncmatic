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
config_t configuracion[3];
position_t currentPosition;
char gCode = -1, mCode = -1, freeCode = -1;
char commandReceived[64];

typedef void (*_func)(char[]);
position_t GetFinalPosition(char[]);
void LinearMoveTo(position_t, unsigned long, unsigned long, unsigned long);
void MoveToOrigin(void);

position_t CreatePosition(unsigned long x, unsigned long y, unsigned long z)
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

void LimitSensorHandler(void)
{
	char message[25];
    strcpypgm2ram(message, (const rom char far *)"Limit Sensor");
	putUSBUSART(message, strlen(message));
	
	if( !PORTBbits.RB4 ) // fin de carrera en EJE X
	{
		// invierto el sentido de giro
		PORTAbits.RA1 = ~PORTAbits.RA1;
		// tiro un paso
		PORTAbits.RA2 = 1;
		Delay100TCYx(120);
		PORTAbits.RA2 = 0;

		LATBbits.LATB4 = ~PORTBbits.RB4;
		machineState = LIMITSENSOR;
		strcpypgm2ram(message, (const rom char far *)"Eje X - RB4");
		putUSBUSART(message, strlen(message));
	}

	if( !PORTBbits.RB5 ) // fin de carrera en EJE Y
	{
		// invierto el sentido de giro
		PORTCbits.RC1 = ~PORTCbits.RC1;
		// tiro un paso
		PORTCbits.RC2 = 1;
		Delay100TCYx(120);
		PORTAbits.RA2 = 0;

		LATBbits.LATB5 = ~PORTBbits.RB5;
		machineState = LIMITSENSOR;
		strcpypgm2ram(message, (const rom char far *)"Eje Y - RB5");
		putUSBUSART(message, strlen(message));
	}
/*
	if( PORTBbits.RB6 == 0) // fin de carrera en EJE Z
	{
		// invierto el sentido de giro
		PORTDbits.RD2 = ~PORTDbits.RD2;
		// tiro un paso
		PORTDbits.RD4 = 1;
		Delay100TCYx(120);
		PORTDbits.RD4 = 0;

		LATBbits.LATB6 = ~PORTBbits.RB6;
		machineState = LIMITSENSOR;
	}

	if( PORTBbits.RB7 == 0) // parada de emergencia
	{
		LATBbits.LATB7 = ~PORTBbits.RB7;
		machineState = EMERGENCYSTOP;
	}
*/
	INTCONbits.RBIF = 0;  //limpia bandera y salimos
}

/*
	Cuanto más grande sea la velocidad, más lento gira el motor
*/
void LinearMovement (char command[], unsigned char speed)
{
	unsigned long xPow, yPow, zPow, totalSteps, xClock, yClock, zClock;
	position_t finalPosition = GetFinalPosition(command);
	
	xPow = (unsigned long) (finalPosition.x - currentPosition.x) * (finalPosition.x - currentPosition.x);
	yPow = (unsigned long) (finalPosition.y - currentPosition.y) * (finalPosition.y - currentPosition.y);
	zPow = (unsigned long) (finalPosition.z - currentPosition.z) * (finalPosition.z - currentPosition.z);
	totalSteps = ceil( sqrt( xPow + yPow + zPow ) );
	
	xClock = (finalPosition.x > currentPosition.x)? ceil( speed * totalSteps / (finalPosition.x - currentPosition.x) ) : ceil( speed * totalSteps / (currentPosition.x - finalPosition.x) );
	yClock = (finalPosition.y > currentPosition.y)? ceil( speed * totalSteps / (finalPosition.y - currentPosition.y) ) : ceil( speed * totalSteps / (currentPosition.y - finalPosition.y) );
	zClock = (finalPosition.z > currentPosition.z)? ceil( speed * totalSteps / (finalPosition.z - currentPosition.z) ) : ceil( speed * totalSteps / (currentPosition.z - finalPosition.z) );
	
	LinearMoveTo(finalPosition, xClock, yClock, zClock);
}

// Definimos funciones G soportadas
void G00(char code[])
{
	LinearMovement(code, 50);
}

void G01(char code[])
{
	LinearMovement(code, 100);
}

void G02(char code[])
{
	;
}

void G03(char code[])
{
	;
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
	;
}
void G21(char code[])
{
	;
}
void G90(char code[])
{
	;
}
void G91(char code[])
{
	;
}
// Cargamos nuestro vector de funciones G
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

// Definimos funciones M soportadas
void M00(char code[])
{
	;
}
void M02(char code[])
{
	;
}
// Cargamos nuestro vector de funciones M
_func mCodes[3] = {	M00,		NULL,	M02	};

/*********************************************************************************/
/*************** Obtiene la posición final segun comando ******************/
/** code -----------> codigo G a realizar por la maquina ********************/
/*********************************************************************************/
position_t GetFinalPosition(char code[])
{
	unsigned char i, count = strlen(code);
	char *ptr;
	
	position_t position = CreatePositionFrom(currentPosition);
	
	// Calculamos los pasos que se deben dar en cada eje
	for(i = 0; i < count; i++)
	{
		if( code[i] == 'X')
		{
			ptr = &code[i + 1];
			for(++i; (code[i] != ' ') && (i < count); i++) ;
			code[i] = '\0';
			position.x = (unsigned long) ceil(atof(ptr) * configuracion[0].axisFactor);
		}
		
		if( code[i] == 'Y')
		{
			ptr = &code[i + 1];
			for(++i; (code[i] != ' ') && (i < count); i++) ;
			code[i] = '\0';
			position.y = (unsigned long) ceil(atof(ptr) * configuracion[1].axisFactor);
		}
		
		if( code[i] == 'Z')
		{
			ptr = &code[i + 1];
			for(++i; (code[i] != ' ') && (i < count); i++) ;
			code[i] = '\0';
			position.z = (unsigned long) ceil(atof(ptr) * configuracion[2].axisFactor);
		}
	}	
	return position;
}

/*********************************************************************************/
/***********************Realiza la línea en el espacio ************************/
/** targetPosition -----------> posición final del movimiento ******************/
/** xFreq , yFreq, zFreq -> cada cuántos pulsos cambiar el bit de clock */
/*********************************************************************************/
void LinearMoveTo(position_t targetPosition, unsigned long xFreq, unsigned long yFreq, unsigned long zFreq)
{
	unsigned long clock = 0, xNextStep = 0, yNextStep = 0, zNextStep = 0;
	
	// Seteamos bit de sentido de giro
	PORTAbits.RA1 = (targetPosition.x > currentPosition.x) ? 1 : 0;
	PORTCbits.RC1 = (targetPosition.y > currentPosition.y) ? 1 : 0;
	PORTDbits.RD2 = (targetPosition.z > currentPosition.z) ? 1 : 0;
	
	// Enable PortB Interrupts
	INTCONbits.RBIE = 1;
	// Mientras no lleguemos a la posicion final en los 3 ejes, tenemos que hacer girar algún motor
	while( ( machineState == PROCESSINGCOMMAND ) && ( (targetPosition.x != currentPosition.x) || (targetPosition.y != currentPosition.y) || (targetPosition.z != currentPosition.z) ) )
	{
		// si el clock es mayor o igual al clock del próximo paso del motor y si no llegué a la posición final
		if( (clock >= xNextStep) && (currentPosition.x != targetPosition.x) )
		{
			if( PORTAbits.RA2 )
			{
				if(PORTAbits.RA1)
				{
					currentPosition.x++;
				}
				else
				{
					currentPosition.x--;
				}
				
				PORTAbits.RA2 = 0;
			}
			else
			{
				PORTAbits.RA2 = 1;
			}
			xNextStep += xFreq;
		}
		
		if( (clock >= yNextStep) && (currentPosition.y != targetPosition.y) )
		{
			if( PORTCbits.RC2 )
			{
				if(PORTCbits.RC1)
				{
					currentPosition.y++;
				}
				else
				{
					currentPosition.y--;
				}
				PORTCbits.RC2 = 0;
			}
			else
			{
				PORTCbits.RC2 = 1;
			}
			yNextStep += yFreq;
		}
		
		if( (clock >= zNextStep) && (currentPosition.z != targetPosition.z) )
		{
			if( PORTDbits.RD4 )
			{
				if(PORTDbits.RD2)
				{
					currentPosition.z++;
				}
				else
				{
					currentPosition.z--;
				}
				PORTDbits.RD4 = 0;
			}
			else
			{
				PORTDbits.RD4 = 1;
			}
			zNextStep += zFreq;
		}
		clock++;
	}
	// Disable PortB Interrupts
	INTCONbits.RBIE = 0;
}

void MoveToOrigin()
{
	// seteamos posicion actual como la mas lejana posible
	position_t fakeFinalPosition = CreatePosition(ULONG_MAX, ULONG_MAX, ULONG_MAX);
	currentPosition = CreatePositionFrom(fakeFinalPosition);
	
	// Primero movemos solo el eje z hacia su punto 0...
	fakeFinalPosition.z = 0;
	machineState = PROCESSINGCOMMAND;
	LinearMoveTo(fakeFinalPosition, ULONG_MAX, ULONG_MAX, 50);
	currentPosition.z = 0;
	
	// Luego el eje y hasta su origen...
	fakeFinalPosition.y = 0;
	machineState = PROCESSINGCOMMAND;
	LinearMoveTo(fakeFinalPosition, ULONG_MAX, 50, ULONG_MAX);
	currentPosition.y = 0;
	
	// por ultimo, el eje x hacia su posicion inicial.
	fakeFinalPosition.x = 0;
	machineState = PROCESSINGCOMMAND;
	LinearMoveTo(fakeFinalPosition, 50, ULONG_MAX, ULONG_MAX);
	currentPosition.x = 0;
}

void user(void)
{
	BYTE numBytesRead;
	char *configPtr, message[25];
	char movementCommandCode[3], movementCommandType;
	unsigned char motor = 0;
	double stepDegrees, distancePerRevolution;

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
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"reset"))
			{
				// Resetear la maquina si recibimos el comando 'reset'
				strcpypgm2ram(message, (const rom char far *)"CNC Reset");
				putUSBUSART(message, strlen(message));
				machineState = SERIALPORTCONNECTED;
				goto endUser;
			}
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"status"))
			{
				// devolvemos el etado en el que esta la maquina
				sprintf(message, "CNC Status: %d", machineState);
				putUSBUSART(message, strlen(message));
				goto endUser;
			}
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"freemoves"))
			{
				strcpypgm2ram(message, (const rom char far *)"CNC Free Moves");
				putUSBUSART(message, strlen(message));
				machineState = FREEMOVES;
				goto endUser;
			}
			
			if(!strcmppgm2ram(USB_In_Buffer, (const rom char far *)"stop"))
			{
				freeCode = -1;
				strcpypgm2ram(message, (const rom char far *)"CNC Stop Free Moves");
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
					configPtr = strtokpgmram(USB_In_Buffer, (const rom char far *)";");
					stepDegrees = atof(configPtr);
					configPtr = strtokpgmram(NULL, (const rom char far *)";");
					distancePerRevolution = atof(configPtr);
					configuracion[motor].axisFactor = 360 / (stepDegrees * distancePerRevolution);
					while( (++motor ) && ( (configPtr = strtokpgmram( NULL, (const rom char far *)";" )) != NULL ) )    // Posteriores llamadas
					{
						stepDegrees = atof(configPtr);
						configPtr = strtokpgmram(NULL, (const rom char far *)";");
						distancePerRevolution = atof(configPtr);
						configuracion[motor].axisFactor = 360 / (stepDegrees * distancePerRevolution);
					}
					
					if(motor == 3)
					{
						// if it has been configured all 3 engines
						strcpypgm2ram(message, (const rom char far *)"Configuracion Correcta");
						machineState = CONFIGURED;
					}
					else
					{
						// not all 3 engines had been configured correctly
						strcpypgm2ram(message, (const rom char far *)"Error en Configuracion");
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
							strcpypgm2ram(message, (const rom char far *)"Comando Soportado");
							machineState = PROCESSINGCOMMAND;
						}
						else
						{
							strcpypgm2ram(message, (const rom char far *)"Comando No Soportado");
						}
					}
					// Chequeamos si el comando es M
					else if(movementCommandType == 'M')
					{
						mCode = atoi(movementCommandCode);
						if(mCodes[mCode] != NULL)
						{
							strcpy(commandReceived, USB_In_Buffer);
							strcpypgm2ram(message, (const rom char far *)"Comando Soportado");
							machineState = PROCESSINGCOMMAND;
						}
						else
						{
							strcpypgm2ram(message, (const rom char far *)"Comando No Soportado");
						}
					}
					// El comando no era ni G ni M
					else
					{
						strcpypgm2ram(message, (const rom char far *)"Error en Comando");
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
						position_t freeTargetPosition = CreatePositionFrom(currentPosition);
						
						machineState = PROCESSINGCOMMAND;
						if( freeCode == 0) { freeTargetPosition.x += ceil(configuracion[0].axisFactor); LinearMoveTo(freeTargetPosition, 50, ULONG_MAX, ULONG_MAX); }
						else if( freeCode == 1) { freeTargetPosition.x -= ceil(configuracion[0].axisFactor); LinearMoveTo(freeTargetPosition, 50, ULONG_MAX, ULONG_MAX); }
						else if( freeCode == 2) { freeTargetPosition.y += ceil(configuracion[1].axisFactor); LinearMoveTo(freeTargetPosition, ULONG_MAX, 50, ULONG_MAX); }
						else if( freeCode == 3) { freeTargetPosition.y -= ceil(configuracion[1].axisFactor); LinearMoveTo(freeTargetPosition, ULONG_MAX, 50, ULONG_MAX); }
						else if( freeCode == 4) { freeTargetPosition.z += ceil(configuracion[2].axisFactor); LinearMoveTo(freeTargetPosition, ULONG_MAX, ULONG_MAX, 50); }
						else if( freeCode == 5) { freeTargetPosition.z -= ceil(configuracion[2].axisFactor); LinearMoveTo(freeTargetPosition, ULONG_MAX, ULONG_MAX, 50); }
						
						if(machineState == LIMITSENSOR)
						{
							freeCode = -1;
							strcpypgm2ram(message, (const rom char far *)"Sensor Fin de Carrera");
							putUSBUSART(message, strlen(message));
						}
						machineState = FREEMOVES;
					}
					break;
					
				case CONFIGURED:
					MoveToOrigin();
					strcpypgm2ram(message, (const rom char far *)"Posicion de Origen");
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
						strcpypgm2ram(message, (const rom char far *)"Sensor Fin de Carrera");
					}
					else if(machineState == EMERGENCYSTOP)
					{
						strcpypgm2ram(message, (const rom char far *)"Parada de Emergencia");
					}
					else
					{
						strcpypgm2ram(message, (const rom char far *)"Comando Ejecutado");
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