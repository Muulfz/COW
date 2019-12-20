#include "cs_interface.h"
#include <cstdio>


extern "C"
{
	EXPORTED void print_to_console(const char* message)
	{
		printf(message);
		printf("\n");
	}
}
