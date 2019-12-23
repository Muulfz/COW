#include "Plugin.h"
#include <string>
#include "cs_interface.h"

#ifdef LUA_DEFINE
# undef LUA_DEFINE
#endif
#define LUA_DEFINE(name) Define(#name, [](lua_State *L) -> int

lua_State* L;

EXPORTED const char* execute_lua(const char* name, const char* data)
{
	lua_getglobal(L, name);
	lua_pushstring(L, data);
	int state = lua_pcall(L, 1, 1, 0);
	if (state == LUA_OK)
	{
		return lua_tostring(L, -1);
	}
	return "failed";
}

EXPORTED void log_to_console(const char* mesg)
{
	Onset::Plugin::Get()->Log(mesg);
}

Plugin::Plugin()
{
	bridge = NULL;
	LUA_DEFINE(ExecuteNET)
	{
		std::string name;
		std::string data;
		Lua::ParseArguments(L, name, data);
		bool r = Plugin::Get()->bridge->execute_event(name.c_str(), data.c_str());
		Lua::ReturnValues(L, r);
		return 1;
	});
	LUA_DEFINE(setCowReturn)
	{
		int id;
		std::string rval;
		Lua::ParseArguments(L, id, rval); // maybe not working? do not know how it actually works
		Plugin::Get()->AddRval(id, rval);
		return 1;
	});
}

void Plugin::startPackage(lua_State* lua)
{
	L = lua;
	bridge = new NetBridge();
}

void Plugin::stopPackage()
{
	bridge->stop();
}

