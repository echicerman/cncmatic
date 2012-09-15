#include "p18f4550.h"
#include "./USB/usb.h"
#include "./USB/usb_function_cdc.h"
#include "main.h"
#include "user.h"
#include <string.h>
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

void user(void)
{
	BYTE numBytesRead;
	char *configPtr, *movementPtr, message[20];
	char movementCommand[3];
	int motor = 0;

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
			/*for(i=0;i<numBytesRead;i++)
			{
				switch(USB_Out_Buffer[i])
				{
					case 0x0A:
					case 0x0D:
						USB_In_Buffer[i] = USB_Out_Buffer[i];
						break;
					default:
						USB_In_Buffer[i] = USB_Out_Buffer[i];// + 1;
						break;
				}
			}*/
			//putUSBUSART(USB_In_Buffer,numBytesRead);
			
			switch(machineState)
			{
				case SERIALPORTCONNECTED:
				//case 0:
					// Count characters received and send this number to PC
					sprintf(message, "%d", numBytesRead);
					putUSBUSART(message, strlen(message) + 1);
					//machineState = 1;
					machineState = HANDSHAKEACKRECEIVED;
					break;
					
				case HANDSHAKEACKRECEIVED:
				//case 1:
					// Compare confirmation message.
					if(!strcmppgm2ram(USB_In_Buffer, "ok"))
						//machineState = 2;
						machineState = CNCMATICCONNECTED;
					else
						//machineState = 0;
						machineState = SERIALPORTCONNECTED;
					break;
					
				case CNCMATICCONNECTED:
				//case 2:
					configPtr = strtokpgmram(USB_In_Buffer, ";");
					configuracion[motor].stepDegrees = atoi(configPtr);
					configPtr = strtokpgmram(NULL, ";");
					configuracion[motor].distancePerRevolution = atoi(configPtr);
					while( (++motor ) && ( (configPtr = strtokpgmram( NULL, ";" )) != NULL ) )    // Posteriores llamadas
					{						
						configuracion[motor].stepDegrees = atoi(configPtr);
						configPtr = strtokpgmram(NULL, ";");
						configuracion[motor].distancePerRevolution = atoi(configPtr);
					}
					
					if(motor == 3)
					{
						strcpypgm2ram(message, "Configuration OK");
						putUSBUSART(message, strlen(message) + 1);
						//machineState = 3;
						machineState = CONFIGURED;
						
						// TODO: Mover punta a (0;0;0)
						
						machineState = WAITINGCOMMAND;
						strcpypgm2ram(message, "Origin Position Set");
						putUSBUSART(message, strlen(message) + 1);
					}
					else
					{
						strcpypgm2ram(message, "Configuration FAIL");
						putUSBUSART(message, strlen(message) + 1);
						//machineState = 2;
						machineState = CNCMATICCONNECTED;
					}
					break;
					
				case CONFIGURED:
				//case 3:
					// Moving to (0;0;0)
					break;
					
				case WAITINGCOMMAND:
				//case 4:
					//machineState = 5;
					movementPtr = strtokpgmram(USB_In_Buffer, " ");
					movementCommand[0] = movementPtr[1];
					movementCommand[1] = movementPtr[2];
					movementCommand[2] = '\0';
					switch( atoi(movementCommand) )
					{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
							commandFailure = FALSE;
							strcpypgm2ram(message, "Command OK");
							putUSBUSART(message, strlen(message) + 1);
							machineState = PROCESSINGCOMMAND;
							break;
						
						default:
							commandFailure = TRUE;
							strcpypgm2ram(message, "Command Not Supported");
							putUSBUSART(message, strlen(message) + 1);
							if(commandFailure)
							{
								
								// TODO: Mover punta a (0;0;0)
						
								machineState = WAITINGCOMMAND;
								strcpypgm2ram(message, "Origin Position Set");
								putUSBUSART(message, strlen(message) + 1);
								
							}
							break;
					}
					break;
					
				 case PROCESSINGCOMMAND:
				 //case 5:
					// Processing command received
					machineState = WAITINGCOMMAND;
					break;
			}
		}
	}
	CDCTxService();
}