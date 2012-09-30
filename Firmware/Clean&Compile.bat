echo off
cd "C:\Documents and Settings\gabriel\Mis documentos\Firmware"

echo * Delete files *
del "Objects\PICDEM FSUSB\usb_descriptors.o"
del "Objects\PICDEM FSUSB\main.o"
del "Objects\PICDEM FSUSB\usb_function_cdc.o"
del "Objects\PICDEM FSUSB\usb_device.o"
del "Objects\PICDEM FSUSB\user.o"
del "USB Device - CDC - Basic Demo -  C18 - PICDEM FSUSB.cof"
del "USB Device - CDC - Basic Demo -  C18 - PICDEM FSUSB.hex"
del "USB Device - CDC - Basic Demo -  C18 - PICDEM FSUSB.map"

echo * Compile *
"C:\Archivos de programa\Microchip\mplabc18\v3.40\bin\mcc18.exe" -p=18F4550 /i"C:\Program Files\Microchip\mplabc18\v3.38\h" -I"..\..\..\Microchip\include" -I"." "usb_descriptors.c" -fo=".\Objects\PICDEM FSUSB\usb_descriptors.o"
"C:\Archivos de programa\Microchip\mplabc18\v3.40\bin\mcc18.exe" -p=18F4550 /i"C:\Program Files\Microchip\mplabc18\v3.38\h" -I"..\..\..\Microchip\include" -I"." "main.c" -fo=".\Objects\PICDEM FSUSB\main.o"
"C:\Archivos de programa\Microchip\mplabc18\v3.40\bin\mcc18.exe" -p=18F4550 /i"C:\Program Files\Microchip\mplabc18\v3.38\h" -I"..\..\..\Microchip\include" -I"." "usb_function_cdc.c" -fo=".\Objects\PICDEM FSUSB\usb_function_cdc.o"
"C:\Archivos de programa\Microchip\mplabc18\v3.40\bin\mcc18.exe" -p=18F4550 /i"C:\Program Files\Microchip\mplabc18\v3.38\h" -I"..\..\..\Microchip\include" -I"." "usb_device.c" -fo=".\Objects\PICDEM FSUSB\usb_device.o"
"C:\Archivos de programa\Microchip\mplabc18\v3.40\bin\mcc18.exe" -p=18F4550 /i"C:\Program Files\Microchip\mplabc18\v3.38\h" -I"..\..\..\Microchip\include" -I"." "user.c" -fo=".\Objects\PICDEM FSUSB\user.o"
"C:\Archivos de programa\Microchip\mplabc18\v3.40\bin\mplink.exe" /p18F4550 "rm18f4550 - HID Bootload.lkr" "Objects\PICDEM FSUSB\usb_descriptors.o" "Objects\PICDEM FSUSB\main.o" "Objects\PICDEM FSUSB\usb_function_cdc.o" "Objects\PICDEM FSUSB\usb_device.o" "Objects\PICDEM FSUSB\user.o" /u_CRUNTIME /z__MPLAB_BUILD=1 /o"USB Device - CDC - Basic Demo -  C18 - PICDEM FSUSB.cof" /M"USB Device - CDC - Basic Demo -  C18 - PICDEM FSUSB.map" /W

pause