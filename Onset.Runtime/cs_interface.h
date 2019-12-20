#pragma once

#ifndef __CS_INTERFACE_H__
#define __CS_INTERFACE_H__
#ifdef _WIN32
#   define EXPORTED  __declspec( dllexport )
#else
# define EXPORTED
#endif
#endif
