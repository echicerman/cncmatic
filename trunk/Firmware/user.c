#include "p18f4550.h"
#include "./USB/usb.h"
#include "./USB/usb_function_cdc.h"
#include "main.h"
#include "user.h"
#include <string.h>
#include <math.h>
#include <delays.h>

volatile state_t machineState = SERIALPORTCONNECTED;
config_t configuracion[3];
position_t currentPosition;
char gCommand = -1, mCommand = -1;

typedef void (*_func)(char[]);
position_t GetFinalPosition(char[]);
void Line(long, long, long, position_t);

void LimitSensorHandler(void)
{
	if( PORTBbits.RB4 ) // fin de carrera en EJE X
	{
		PORTAbits.RA1 = ~PORTAbits.RA1;
		PORTAbits.RA2 = 1;
		Delay100TCYx(120);
		PORTAbits.RA2 = 0;
		PORTAbits.RA1 = ~PORTAbits.RA1;

		machineState = LIMITSENSOR;
	}
	
	if( PORTBbits.RB5 ) // fin de carrera en EJE Y
	{
		PORTCbits.RC1 = ~PORTCbits.RC1;
		PORTCbits.RC2 = 1;
		Delay100TCYx(120);
		PORTAbits.RA2 = 0;
		PORTCbits.RC1 = ~PORTCbits.RC1;

		machineState = LIMITSENSOR;
	}
	
	if( PORTBbits.RB6 ) // fin de carrera en EJE Z
	{
		PORTDbits.RD2 = ~PORTDbits.RD2;
		PORTDbits.RD4 = 1;
		Delay100TCYx(120);
		PORTDbits.RD4 = 0;
		PORTDbits.RD2 = ~PORTDbits.RD2;

		machineState = LIMITSENSOR;
	}
	
	if( PORTBbits.RB7 ) // parada de emergencia
	{
		machineState = EMERGENCYSTOP;
	}
}

// Definimos funciones Gsoportadas
void G00(char code[])
{
	unsigned long xClock, yClock, zClock, xPow, yPow, zPow, distance, time;
	unsigned char speed = 100;
	position_t position = GetFinalPosition(code);
	
	xPow = (position.x - currentPosition.x) * (position.x - currentPosition.x);
	yPow = (position.y - currentPosition.y) * (position.y - currentPosition.y);
	zPow = (position.z - currentPosition.z) * (position.z - currentPosition.z);
	distance = sqrt( xPow + yPow + zPow );
	time = ( distance / speed ) * 1000; // milisegundos
	
	/** time / cantidadDeCambiosDeEstado **/
	xClock = time / ( (position.x - currentPosition.x)  * 2 );
	yClock = time / ( (position.y - currentPosition.y)  * 2 );
	zClock = time / ( (position.z - currentPosition.z) * 2 );
	
	Line(xClock, yClock, zClock, position);
}

void G01(char code[])
{
	unsigned long xClock, yClock, zClock, xPow, yPow, zPow, distance, time;
	unsigned char speed = 100;
	position_t position = GetFinalPosition(code);
	
	xPow = (position.x - currentPosition.x) * (position.x - currentPosition.x);
	yPow = (position.y - currentPosition.y) * (position.y - currentPosition.y);
	zPow = (position.z - currentPosition.z) * (position.z - currentPosition.z);
	distance = sqrt( xPow + yPow + zPow );
	time = ( distance / speed ) * 1000; // milisegundos
	
	/** time / cantidadDeCambiosDeEstado **/
	xClock = time / ( (position.x - currentPosition.x)  * 2 );
	yClock = time / ( (position.y - currentPosition.y)  * 2 );
	zClock = time / ( (position.z - currentPosition.z) * 2 );
	
	Line(xClock, yClock, zClock, position);
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
_func gCode[100] = {	G00, 	G01, 	G02, 	G03, 	G04, 	NULL, 	NULL, 	NULL, 	NULL, 	NULL,
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
_func mCode[3] = {	M00,		NULL,	M02	};

/*********************************************************************************/
/*************** Obtiene la posici�n final segun comando ******************/
/** code -----------> codigo G a realizar por la maquina ********************/
/*********************************************************************************/
position_t GetFinalPosition(char code[])
{
	unsigned char i, count = strlen(code);
	char *ptr;
	position_t position;
	position.x = currentPosition.x;
	position.y = currentPosition.y;
	position.z = currentPosition.z;
	
	// Calculamos los pasos que se deben dar en cada eje
	// pasos = ( distancia * ( 360 / gradosPorPaso) ) / distanciaPorVuelta
	for(i = 0; i < count; i++)
	{
		if( code[i] == 'X')
		{
			ptr = &code[i + 1];
			for(++i; (code[i] != ' ') && (i < count); i++) ;
			code[i] = '\0';
			position.x = ( atoi(ptr) * ( 360 / configuracion[0].stepDegrees ) ) / configuracion[0].distancePerRevolution;
		}
		
		if( code[i] == 'Y')
		{
			ptr = &code[i + 1];
			for(++i; (code[i] != ' ') && (i < count); i++) ;
			code[i] = '\0';
			position.y = ( atoi(ptr) * ( 360 / configuracion[1].stepDegrees ) ) / configuracion[1].distancePerRevolution;
		}
		
		if( code[i] == 'Z')
		{
			ptr = &code[i + 1];
			for(++i; (code[i] != ' ') && (i < count); i++) ;
			code[i] = '\0';
			position.z = ( atoi(ptr) * ( 360 / configuracion[2].stepDegrees ) ) / configuracion[2].distancePerRevolution;
		}
	}	
	return position;
}

/*********************************************************************************/
/***********************Realiza la l�nea en el espacio ************************/
/** xFreq , yFreq, zFreq -> cada cu�ntos pulsos cambiar el bit de clock */
/** finalPosition -----------> posici�n final del movimiento ******************/
/*********************************************************************************/
void Line(long xFreq, long yFreq, long zFreq, position_t finalPosition)
{
	long clock = 0, xNextStep = 0, yNextStep = 0, zNextStep = 0;
	
	// Seteamos bit de sentido de giro
	PORTAbits.RA1 = finalPosition.x > currentPosition.x ? 0 : 1;
	PORTCbits.RC1 = finalPosition.y > currentPosition.y ? 0 : 1;
	PORTDbits.RD2 = finalPosition.z > currentPosition.z ? 0 : 1;
	
	// Enable PortB Interrupts
	INTCONbits.RBIF = 0;  //limpia bandera
	INTCONbits.RBIE = 1;
	// Mientras no lleguemos a la posicion final en los 3 ejes, tenemos que hacer girar alg�n motor
	while( ( machineState == PROCESSINGCOMMAND ) && ( (finalPosition.x != currentPosition.x) || (finalPosition.y != currentPosition.y) || (finalPosition.z != currentPosition.z) ) )
	{
		// si el clock es mayor o igual al clock del pr�ximo paso del motor y si no llegu� a la posici�n final
		if( (clock >= xNextStep) && (currentPosition.x != finalPosition.x) )
		{
			if( PORTAbits.RA2 )
			{
				currentPosition.x += (PORTAbits.RA1 ? -1 : 1);
				PORTAbits.RA2 = 0;
			}
			else
			{
				PORTAbits.RA2 = 1;
			}
			xNextStep += xFreq;
		}
		
		if( (clock >= yNextStep) && (currentPosition.y != finalPosition.y) )
		{
			if( PORTCbits.RC2 )
			{
				currentPosition.y += (PORTCbits.RC1 ? -1 : 1);
				PORTCbits.RC2 = 0;
			}
			else
			{
				PORTCbits.RC2 = 1;
			}
			yNextStep += yFreq;
		}
		
		if( (clock >= zNextStep) && (currentPosition.z != finalPosition.z) )
		{
			if( PORTDbits.RD4 )
			{
				currentPosition.z += (PORTDbits.RD2 ? -1 : 1);
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

void user(void)
{
	BYTE numBytesRead;
	char *configPtr, message[25];
	char movementCommandCode[3], movementCommandType;
	unsigned char motor = 0;

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
					configPtr = strtokpgmram(USB_In_Buffer, (const rom char far *)";");
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
						strcpypgm2ram(message, (const rom char far *)"Configuracion Correcta");
						machineState = CONFIGURED;
						
						// TODO: Mover punta a (0;0;0)
						currentPosition.x = 0;
						currentPosition.y = 0;
						currentPosition.z = 0;
						//*******************************
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
						gCommand = atoi(movementCommandCode);
						if(gCode[gCommand] != NULL)
						{
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
						mCommand = atoi(movementCommandCode);
						if(mCode[mCommand] != NULL)
						{
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
				case CONFIGURED:
					strcpypgm2ram(message, (const rom char far *)"Posicion de Origen");
					putUSBUSART(message, strlen(message));
					machineState = WAITINGCOMMAND;
					break;
					
				case PROCESSINGCOMMAND:
					// Processing command received
					if(gCommand != -1) { gCode[gCommand](USB_In_Buffer); }
					if(mCommand != -1) { mCode[mCommand](USB_In_Buffer); }
					
					// reseteamos variables de comando
					gCommand = mCommand = -1;
					
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