#include "myDelays.h"
void Delay1MS()
{
	Delay1MSx(1);
}
void Delay1MSx(int x)
{ 
	int i;
	for(i = 0; i < x; i++) Delay1KTCYx(12);
}
void Delay10MSx(int x)
{
	int i;
	for (i = 0; i < x; i++) Delay10KTCYx(12);
}
void Delay100MSx(int x)
{
	int i;
	for (i = 0; i < x; i++) Delay10KTCYx(120);
}
void Delay1Sx(int x)
{
	int i;
	for (i = 0; i < 10 * x; i++) Delay10KTCYx(120);
}