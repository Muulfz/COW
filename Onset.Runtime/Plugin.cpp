#include "Plugin.h"
#include <string>

#ifdef LUA_DEFINE
# undef LUA_DEFINE
#endif
#define LUA_DEFINE(name) Define(#name, [](lua_State *L) -> int


Plugin::Plugin()
{
	bridge = NULL;
	LUA_DEFINE(executeNET)
	{
		std::string name;
		std::string data;
		Lua::ParseArguments(L, name, data);
		return Lua::ReturnValues(L, Plugin::Get()->bridge->execute_event(name.c_str(), data.c_str()));
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
	bridge = new NetBridge();
}

void Plugin::stopPackage()
{
	bridge->stop();
}

