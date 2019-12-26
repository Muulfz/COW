
#pragma once
#ifndef __NET_BRIDGE_H__
#define __NET_BRIDGE_H__
#include <string>
#include <stdio.h>  /* defines FILENAME_MAX */
#ifdef _WIN32
#include <direct.h>
#define GetCurrentDir _getcwd
#else
#include <unistd.h>
#define GetCurrentDir getcwd
#endif

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <string>

// https://github.com/dotnet/coreclr/blob/master/src/coreclr/hosts/inc/coreclrhost.h
#include "coreclrhost.h"

#define MANAGED_ASSEMBLY "Onset.dll"
#define NO_ERROR 0
#define CONSOLE_ERROR -1
#define SUCCESS 1

// Define OS-specific items like the CoreCLR library's name and path elements
#if defined(_WIN32)
#include <Windows.h>
#define FS_SEPARATOR "\\"
#define PATH_DELIMITER ";"
#define CORECLR_FILE_NAME "coreclr.dll"
#elif defined(__linux__)
#include <dirent.h>
#include <dlfcn.h>
#include <limits.h>
#define FS_SEPARATOR "/"
#define PATH_DELIMITER ":"
#define MAX_PATH PATH_MAX
#if OSX
// For OSX, use Linux defines except that the CoreCLR runtime
// library has a different name
#define CORECLR_FILE_NAME "libcoreclr.dylib"
#else
#define CORECLR_FILE_NAME "libcoreclr.so"
#endif
#endif

// Function pointer types for the managed call and callback
typedef int (*report_callback_ptr)(int progress);
typedef void (*load_ptr)();
typedef void (*unload_ptr)();
typedef bool (*execute_event_ptr)(const char* name, const char* data);


/*int ReportProgressCallback(int progress)
{
	printf("Received status from managed code: %d\n", progress);
	return -progress;
}*/

#ifdef __cplusplus
extern "C"
{
#endif

class NetBridge
{
private:
	void* hostHandle;
	unsigned int domainId;
	coreclr_initialize_ptr initializeCoreClr;
	coreclr_create_delegate_ptr createManagedDelegate;
	coreclr_shutdown_ptr shutdownCoreClr;
	execute_event_ptr executeEvent;
	unload_ptr unload;
	
public:
	int last_error = NO_ERROR;
	
	NetBridge()
	{
		char gCurrentPath[FILENAME_MAX];

		if (!GetCurrentDir(gCurrentPath, sizeof(gCurrentPath)))
		{
			printf("ERROR: No coreclr path found!");
			last_error = CONSOLE_ERROR;
			return;
		}

		gCurrentPath[sizeof(gCurrentPath) - 1] = '\0';
#if defined(_WIN32)
		std::string coreClrPath = std::string(gCurrentPath) + "\\cow\\coreclr.dll";
#else	
		std::string coreClrPath = std::string(cCurrentPath) + "\\cow\\libcoreclr.so";
#endif
		char cCurrentPath[FILENAME_MAX];

		if (!GetCurrentDir(cCurrentPath, sizeof(cCurrentPath)))
		{
			printf("ERROR: No Wrapper path found!");
			last_error = CONSOLE_ERROR;
			return;
		}

		cCurrentPath[sizeof(cCurrentPath) - 1] = '\0';
#if defined(_WIN32)
		std::string wrapperPath = std::string(cCurrentPath) + "\\cow\\Onset.dll";
#else	
		std::string wrapperPath = std::string(cCurrentPath) + "\\cow\\Onset.dll";
#endif

		char rCurrentPath[FILENAME_MAX];

		if (!GetCurrentDir(rCurrentPath, sizeof(rCurrentPath)))
		{
			printf("ERROR: No Runtime path found!");
			last_error = CONSOLE_ERROR;
			return;
		}

		rCurrentPath[sizeof(rCurrentPath) - 1] = '\0';
		#if defined(_WIN32)
		std::string runtimePath = std::string(rCurrentPath) + "\\cow\\";
		#else	
		std::string runtimePath = std::string(rCurrentPath) + "\\cow\\";
		#endif
	
#if defined(_WIN32)
		HMODULE coreClr = LoadLibraryExA(coreClrPath.c_str(), NULL, 0);
#elif defined(__linux__)
		void* coreClr = dlopen(coreClrPath.c_str(), RTLD_NOW | RTLD_LOCAL);
#endif
		if (coreClr == NULL)
		{
			printf("ERROR: Failed to load CoreCLR from %s\n", coreClrPath.c_str());
			last_error = CONSOLE_ERROR;
			return;
		}
		
#if defined(_WIN32)
		initializeCoreClr = (coreclr_initialize_ptr)GetProcAddress(coreClr, "coreclr_initialize");
		createManagedDelegate = (coreclr_create_delegate_ptr)GetProcAddress(coreClr, "coreclr_create_delegate");
		shutdownCoreClr = (coreclr_shutdown_ptr)GetProcAddress(coreClr, "coreclr_shutdown");
#elif defined(__linux__)
		initializeCoreClr = (coreclr_initialize_ptr)dlsym(coreClr, "coreclr_initialize");
		createManagedDelegate = (coreclr_create_delegate_ptr)dlsym(coreClr, "coreclr_create_delegate");
		shutdownCoreClr = (coreclr_shutdown_ptr)dlsym(coreClr, "coreclr_shutdown");
#endif

		if (initializeCoreClr == NULL)
		{
			printf("ERROR: coreclr_initialize not found");
			last_error = CONSOLE_ERROR;
			return;
		}

		if (createManagedDelegate == NULL)
		{
			printf("ERROR: coreclr_create_delegate not found");
			last_error = CONSOLE_ERROR;
			return;
		}

		if (shutdownCoreClr == NULL)
		{
			printf("ERROR: coreclr_shutdown not found");
			last_error = CONSOLE_ERROR;
			return;
		}

		std::string tpaList;

		//
#if defined(_WIN32)
			std::string searchPath(runtimePath.c_str());
			searchPath.append(FS_SEPARATOR);
			searchPath.append("*");
			searchPath.append(".dll");

			WIN32_FIND_DATAA findData;
			HANDLE fileHandle = FindFirstFileA(searchPath.c_str(), &findData);

			if (fileHandle != INVALID_HANDLE_VALUE)
			{
				do
				{
					tpaList.append(runtimePath.c_str());
					tpaList.append(FS_SEPARATOR);
					tpaList.append(findData.cFileName);
					tpaList.append(PATH_DELIMITER);
				} while (FindNextFileA(fileHandle, &findData));
				FindClose(fileHandle);
			}
#elif defined(__linux__)
			DIR* dir = opendir(runtimePath.c_str());
			struct dirent* entry;
			int extLength = strlen(".dll");

			while ((entry = readdir(dir)) != NULL)
			{
				std::string filename(entry->d_name);
				int extPos = filename.length() - extLength;
				if (extPos <= 0 || filename.compare(extPos, extLength, extension) != 0)
				{
					continue;
				}
				tpaList.append(runtimePath.c_str());
				tpaList.append(FS_SEPARATOR);
				tpaList.append(filename);
				tpaList.append(PATH_DELIMITER);
			}
#endif
		//

		const char* propertyKeys[] = {
			"TRUSTED_PLATFORM_ASSEMBLIES"      
		};

		const char* propertyValues[] = {
			tpaList.c_str()
		};
		
		int hr = initializeCoreClr(
			wrapperPath.c_str(),        // App base path
			"Onset",       // AppDomain friendly name
			sizeof(propertyKeys) / sizeof(char*),   // Property count
			propertyKeys,       // Property names
			propertyValues,     // Property values
			&hostHandle,        // Host handle
			&domainId);         // AppDomain ID

		if (hr >= 0)
		{
			//printf("CoreCLR started\n");
		}
		else
		{
			printf("ERROR: coreclr_initialize failed - status: 0x%08x\n", hr);
			last_error = CONSOLE_ERROR;
			return;
		}
		load_ptr managedDelegate;
		
		hr = createManagedDelegate(
			hostHandle,
			domainId,
			"Onset",
			"Onset.Runtime.Wrapper",
			"Load",
			(void**)&managedDelegate);

		if (hr >= 0)
		{
			//printf("Managed delegate created\n");
		}
		else
		{
			printf("ERROR: load delegate failed - status: 0x%08x\n", hr);
			last_error = CONSOLE_ERROR;
			return;
		}

		hr = createManagedDelegate(
			hostHandle,
			domainId,
			"Onset",
			"Onset.Runtime.Wrapper",
			"Unload",
			(void**)&unload);

		if (hr >= 0)
		{
			//printf("Managed delegate created\n");
		}
		else
		{
			printf("ERROR: unload delegate failed - status: 0x%08x\n", hr);
			last_error = CONSOLE_ERROR;
			return;
		}

		hr = createManagedDelegate(
			hostHandle,
			domainId,
			"Onset",
			"Onset.Runtime.Wrapper",
			"ExecuteEvent",
			(void**)&executeEvent);

		if (hr >= 0)
		{
			//printf("Managed delegate created\n");
		}
		else
		{
			printf("ERROR: execute_event delegate failed - status: 0x%08x\n", hr);
			last_error = CONSOLE_ERROR;
			return;
		}

		/*printf("Managed code returned: %s\n", ret);

		// Strings returned to native code must be freed by the native code
#if defined(_WIN32)
		CoTaskMemFree(ret);
#elif defined(__linux__)
		free(ret);
#endif*/
		
		managedDelegate();
		last_error = SUCCESS;
	}
	
	void stop()
	{
		unload();
		int hr = shutdownCoreClr(hostHandle, domainId);
		if (hr >= 0)
		{
			//printf("CoreCLR successfully shutdown\n");
			last_error = SUCCESS;
		}
		else
		{
			printf("ERROR: coreclr_shutdown failed - status: 0x%08x\n", hr);
			last_error = CONSOLE_ERROR;
		}
	}
	
	bool execute_event(const char* name, const char* data)
	{
		return executeEvent(name, data);
	}
};



#ifdef __cplusplus
}
#endif
#endif
