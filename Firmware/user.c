#include "p18f4550.h"
#include "./USB/usb.h"
#include "./USB/usb_function_cdc.h"
#include "main.h"
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

/*typedef enum state_t {
	SERIALPORTCONNECTED,	// 0
	HANDSHAKERECEIVED,		// 1
	CNCMATICCONNECTED,		// 2
	CONFIGURED,							// 3
	WAITINGCOMMAND,				// 4
	PROCESSINGCOMMAND		// 5
}state;
state machineState = SERIALPORTCONNECTED;*/

char ok[3] = "ok";
int gradoPasoMotor[3];
int vueltaRosca[3];
int machineState = 0;

void user(void)
{
	BYTE numBytesRead;

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
			char length[3];
			
			for(i=0;i<numBytesRead;i++)
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
			}
			//putUSBUSART(USB_In_Buffer,numBytesRead);
			USB_In_Buffer[numBytesRead] = '\0';
			switch(machineState)
			{
				//case SERIALPORTCONNECTED:
				case 0:
					// Count characters received and send them to PC
					sprintf(length, "%d", numBytesRead);
					putUSBUSART(length, 2);
					machineState = 1;
					break;
				//case HANDSHAKERECEIVED:
				case 1:
					// Compare confirmation message.
					if(strcmp(USB_In_Buffer, ok) == 0)
						machineState = 2;
					else
						machineState = 0;
					break;
				//case CNCMATICCONNECTED:
				case 2:
					machineState = 3;
					break;
				//case CONFIGURED:
				case 3:
					machineState = 4;
					break;
				//case WAITINGCOMMAND:
				case 4:
					machineState = 5;
					break;
				 //case PROCESSINGCOMMAND:
				 case 5:
					machineState = 4;
					break;
			}

/*
			for(i=0;i<numBytesRead;i++)
			{
				xParam[i] = USB_In_Buffer[i];
			}
			xParam[i]='\0';

			asd = atof(xParam);
			sprintf(zParam, "%f", asd);
*/
		}
	}
	CDCTxService();
}