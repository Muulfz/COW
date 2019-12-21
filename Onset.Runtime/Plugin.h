/*
Copyright (C) 2019 Blue Mountains GmbH
This program is free software: you can redistribute it and/or modify it under the terms of the Onset
Open Source License as published by Blue Mountains GmbH.
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the Onset Open Source License for more details.
You should have received a copy of the Onset Open Source License along with this program. If not,
see https://bluemountains.io/Onset_OpenSourceSoftware_License.txt
*/

#pragma once

#include "net_bridge.h"
#include <PluginSDK.h>

#include "Singleton.h"

#include <vector>
#include <tuple>
#include <functional>


class Plugin : public Singleton<Plugin>
{
	friend class Singleton<Plugin>;
private:
	NetBridge* bridge;
	Plugin();
	~Plugin() = default;

private:
	using FuncInfo_t = std::tuple<const char*, lua_CFunction>;
	std::vector<FuncInfo_t> _func_list;
	std::string _rvals[100];
	int current_id = -1;

private:
	inline void Define(const char* name, lua_CFunction func)
	{
		_func_list.emplace_back(name, func);
	}

public:
	decltype(_func_list) const& GetFunctions() const
	{
		return _func_list;
	}
	inline void AddRval(int idx, std::string rval)
	{
		_rvals[idx] = rval;
	}
	inline std::string GetRval(int id)
	{
		std::string rval = _rvals[id];
		_rvals[id] = "";
		return rval;
	}
	inline int get_rval_id()
	{
		current_id++;
		if (current_id >= 100)
		{
			current_id = 0;
		}
		return current_id;
	}
	
public:
	void startPackage(lua_State* lua);
	void stopPackage();

};
