print('COW: Loading LUA Backend...')
Json = require('packages/'..GetPackageName()..'/server/library/json');
-- START LUA API --

function COW_GetGameVersion(data)
    return Json.encode({version = GetGameVersion()})
end

function COW_GetGameVersionString(data)
    return Json.encode({version = GetGameVersionString()})
end

-- START PLAYER API --

function COW_GetPlayerName(data)
    return Json.encode({playerName = GetPlayerName(Json.decode(data)["player"])})
end

function COW_SetPlayerName(data)
    local obj = Json.decode(data);
    SetPlayerName(obj["player"], obj["playerName"])
end

function COW_GetPlayerSteamID(data)
    return Json.encode({steamID = GetPlayerName(Json.decode(data)["player"])})
end

-- END PLAYER API --

function COW_AddRemoteEvent(data)
    local eventName_ = Json.decode(data)["eventName"];
    AddRemoteEvent(eventName_, function(playerId, ...)
        ExecuteNET('trigger-remote-event', Json.encode({eventName = eventName_, player = playerId, args = {...}}))
    end)
end

function COW_AddCommand(data)
    local commandName_ = Json.decode(data)["commandName"];
    AddCommand(commandName_, function(playerId, ...)
        ExecuteNET('trigger-command', Json.encode({commandName = commandName_, player = playerId, args = {...}}))
    end)
end

function COW_CallRemoteEvent(data)
    local obj = Json.decode(data);
    local eventName = obj["eventName"];
    local player = obj["player"];
    local args = obj["args"];
    CallRemoteEvent(player, eventName, args);
end

-- START SERVER EVENTS --
AddEvent("OnPlayerQuit", function(playerId)
    ExecuteNET('trigger-event', Json.encode({type = 0, player = playerId}))
end)

AddEvent("OnPlayerChat", function(playerId, text_)
    ExecuteNET('trigger-event', Json.encode({type = 1, player = playerId, text = text_}))
end)

AddEvent("OnPlayerChatCommand", function(playerId, command_, exists_)
    ExecuteNET('trigger-event', Json.encode({type = 2, player = playerId, command = command_, exists = exists_}))
end)

AddEvent("OnPlayerJoin", function(playerId)
    ExecuteNET('trigger-event', Json.encode({type = 3, player = playerId}))
end)



-- END SERVER EVENTS --

-- END LUA API --
print('COW: LUA Backend loaded!')
print('COW: Trigger Wrapper to finish!')
ExecuteNET('finish-wrapper', '');