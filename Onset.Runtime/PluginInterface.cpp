#include <PluginSDK.h>
#include "Plugin.h"
#include "cs_interface.h"

Onset::IServerPlugin* Onset::Plugin::_instance = nullptr;

EXPORTED const char* execute_lua(const char* name, const char* data)
{
	auto args = new Lua::LuaArgs_t();
	int id = Plugin::Get()->get_rval_id();
	args->push_back(id);
	args->push_back(data);
	Onset::Plugin::Get()->CallEvent(name, args);
	return Plugin::Get()->GetRval(id).c_str();
}


EXPORT(int) OnPluginGetApiVersion()
{
	return PLUGIN_API_VERSION;
}

EXPORT(void) OnPluginCreateInterface(Onset::IBaseInterface* PluginInterface)
{
	Onset::Plugin::Init(PluginInterface);
}

EXPORT(int) OnPluginStart()
{
	Plugin::Get();
	return PLUGIN_API_VERSION;
}

EXPORT(void) OnPluginStop()
{

	Plugin::Singleton::Destroy();
	Onset::Plugin::Destroy();
}

EXPORT(void) OnPluginTick(float DeltaSeconds)
{
	(void)DeltaSeconds; // unused
}

EXPORT(void) OnPackageLoad(const char* PackageName, lua_State* L)
{
	(void)PackageName; // unused

	for (auto const& f : Plugin::Get()->GetFunctions())
		Lua::RegisterPluginFunction(L, std::get<0>(f), std::get<1>(f));
}

EXPORT(void) OnPackageUnload(const char* PackageName)
{
	(void)PackageName; // unused
}