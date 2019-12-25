print('COW: Loading LUA Backend...')
Json = require('packages/'..GetPackageName()..'/server/library/json');
-- START LUA API --

function COW_GetGameVersion(data)
    return Json.encode({version = GetGameVersion()})
end

function COW_GetGameVersionString(data)
    return Json.encode({version = GetGameVersionString()})
end


-- START SERVER EVENTS --
AddEvent("OnPlayerQuit", function(playerId)
    ExecuteNET('trigger-event', Json.encode({type = 0, player = playerId}))
end)

AddEvent("OnPlayerChat", function(playerId, text_)
    ExecuteNET('trigger-event', Json.encode({type = 0, player = playerId, text = text_}))
end)

-- END SERVER EVENTS --

-- END LUA API --
print('COW: LUA Backend loaded!')
print('COW: Trigger Wrapper to finish!')
ExecuteNET('finish-wrapper', '');