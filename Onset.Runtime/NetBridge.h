#pragma once
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

#if defined(_WIN32)
void BuildTpaList(const char* directory, const char* extension, std::string& tpaList)
{
	std::string searchPath(directory);
	searchPath.append(FS_SEPARATOR);
	searchPath.append("*");
	searchPath.append(extension);

	WIN32_FIND_DATAA findData;
	HANDLE fileHandle = FindFirstFileA(searchPath.c_str(), &findData);

	if (fileHandle != INVALID_HANDLE_VALUE)
	{
		do
		{
			tpaList.append(directory);
			tpaList.append(FS_SEPARATOR);
			tpaList.append(findData.cFileName);
			tpaList.append(PATH_DELIMITER);
		} while (FindNextFileA(fileHandle, &findData));
		FindClose(fileHandle);
	}
}
#elif defined(__linux__)
void BuildTpaList(const char* directory, const char* extension, std::string& tpaList)
{
	DIR* dir = opendir(directory);
	struct dirent* entry;
	int extLength = strlen(extension);

	while ((entry = readdir(dir)) != NULL)
	{
		std::string filename(entry->d_name);
		int extPos = filename.length() - extLength;
		if (extPos <= 0 || filename.compare(extPos, extLength, extension) != 0)
		{
			continue;
		}
		tpaList.append(directory);
		tpaList.append(FS_SEPARATOR);
		tpaList.append(filename);
		tpaList.append(PATH_DELIMITER);
	}
}
#endif

int ReportProgressCallback(int progress)
{
	printf("Received status from managed code: %d\n", progress);
	return -progress;
}

std::string get_coreclr_path()
{
	char cCurrentPath[FILENAME_MAX];

	if (!GetCurrentDir(cCurrentPath, sizeof(cCurrentPath)))
	{
		return NULL;
	}

	cCurrentPath[sizeof(cCurrentPath) - 1] = '\0';
#if defined(_WIN32)
	return std::string(cCurrentPath) + "\\cow\\coreclr.dll";
#else	
	return std::string(cCurrentPath) + "\\cow\\libcoreclr.so";
#endif
}

std::string get_runtime_dir()
{
	char cCurrentPath[FILENAME_MAX];

	if (!GetCurrentDir(cCurrentPath, sizeof(cCurrentPath)))
	{
		return NULL;
	}

	cCurrentPath[sizeof(cCurrentPath) - 1] = '\0';
#if defined(_WIN32)
	return std::string(cCurrentPath) + "\\cow\\";
#else	
	return std::string(cCurrentPath) + "\\cow\\";
#endif
}

std::string get_wrapper_path()
{
	char cCurrentPath[FILENAME_MAX];

	if (!GetCurrentDir(cCurrentPath, sizeof(cCurrentPath)))
	{
		return NULL;
	}

	cCurrentPath[sizeof(cCurrentPath) - 1] = '\0';
#if defined(_WIN32)
	return std::string(cCurrentPath) + "\\cow\\Onset.dll";
#else	
	return std::string(cCurrentPath) + "\\cow\\Onset.dll";
#endif
}

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
		std::string coreClrPath = get_coreclr_path();
		std::string wrapperPath = get_wrapper_path();
		std::string runtimePath = get_runtime_dir();
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
		else
		{
			printf("Loaded CoreCLR from %s\n", coreClrPath.c_str());
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
			printf("coreclr_initialize not found");
			last_error = CONSOLE_ERROR;
			return;
		}

		if (createManagedDelegate == NULL)
		{
			printf("coreclr_create_delegate not found");
			last_error = CONSOLE_ERROR;
			return;
		}

		if (shutdownCoreClr == NULL)
		{
			printf("coreclr_shutdown not found");
			last_error = CONSOLE_ERROR;
			return;
		}

		std::string tpaList;
		BuildTpaList(runtimePath.c_str(), ".dll", tpaList);

		const char* propertyKeys[] = {
			"TRUSTED_PLATFORM_ASSEMBLIES"      
		};

		const char* propertyValues[] = {
			tpaList.c_str()
		};

		printf(wrapperPath.c_str());
		
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
			printf("CoreCLR started\n");
		}
		else
		{
			printf("coreclr_initialize failed - status: 0x%08x\n", hr);
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
			printf("Managed delegate created\n");
		}
		else
		{
			printf("load delegate failed - status: 0x%08x\n", hr);
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
			printf("Managed delegate created\n");
		}
		else
		{
			printf("unload delegate failed - status: 0x%08x\n", hr);
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
			printf("Managed delegate created\n");
		}
		else
		{
			printf("execute_event delegate failed - status: 0x%08x\n", hr);
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
			printf("CoreCLR successfully shutdown\n");
			last_error = SUCCESS;
		}
		else
		{
			printf("coreclr_shutdown failed - status: 0x%08x\n", hr);
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