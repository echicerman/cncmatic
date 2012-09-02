#include "p18f4550.h"
#include "./USB/usb.h"
#include "./USB/usb_function_cdc.h"
#include "main.h"

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
			char xParam[10], yParam[10], zParam[10];
			int intToStr;

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

			putUSBUSART(USB_In_Buffer,numBytesRead);


			for(i=0;i<numBytesRead;i++)
			{
				xParam[i] = USB_In_Buffer[i];
			}
			xParam[i]='\0';
			intToStr = atoi(xParam);
		}
	}
	CDCTxService();
}