#ifndef MAIN_H
#define MAIN_H

/** V A R I A B L E S ********************************************************/
#pragma udata
extern char USB_In_Buffer[64];
extern char USB_Out_Buffer[64];

extern BOOL stringPrinted;
extern volatile BOOL buttonPressed;
extern volatile BYTE buttonCount;

/** P R I V A T E  P R O T O T Y P E S ***************************************/
void InitializeSystem(void);
void ProcessIO(void);
void USBDeviceTasks(void);
void YourHighPriorityISRCode();
void YourLowPriorityISRCode();
void USBCBSendResume(void);
void BlinkUSBStatus(void);
void UserInit(void);

#endif