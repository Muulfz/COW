print('COW: Loading LUA Backend...')
Json = require('packages/'..GetPackageName()..'/server/library/json');
-- START LUA API --

-- BASE ENTITY FUNCTIONS --

--[[
function COW_Get%ENTITIY%Dimension(data)
    return Json.encode({dim = Get%ENTITIY%Dimension(Json.decode(data)["entity"])})
end

function COW_Set%ENTITIY%Dimension(data)
    local obj = Json.decode(data);
    Set%ENTITIY%Dimension(obj["entity"], obj["dim"])
end

function COW_Get%ENTITIY%Position(data)
    local x_, y_, z_ = Get%ENTITIY%Location(Json.decode(data)["entity"]);
    return Json.encode({x = x_, y = y_, z = z_})
end

function COW_Set%ENTITIY%Position(data)
    local obj = Json.decode(data);
    Set%ENTITIY%Location(obj["entity"], obj["x"], obj["y"], obj["z"])
end

function COW_Get%ENTITIY%Validation(data)
    return Json.encode({state = IsValid%ENTITIY%(Json.decode(data)["entity"])})
end
]]--

-- FOR LIFELESS --
--[[
function COW_Destory%ENTITIY%(data)
    local obj = Json.decode(data);
    Destroy%ENTITIY%(obj["entity"])
end
]]--
-- FOR LIFELESS --

-- BASE ENTITY FUNCTIONS --

function COW_GetGameVersion(data)
    return Json.encode({version = GetGameVersion()})
end

function COW_GetGameVersionString(data)
    return Json.encode({version = GetGameVersionString()})
end

-- START PLAYER API --

function COW_GetPlayerHeadSize(data)
    return Json.encode({size = GetPlayerHeadSize(Json.decode(data)["player"])})
end

function COW_SetPlayerHeadSize(data)
    local obj = Json.decode(data);
    SetPlayerHeadSize(obj["player"], obj["size"])
end

function COW_SetPlayerAnimation(data)
    local obj = Json.decode(data);
    SetPlayerAnimation(obj["player"], obj["anim"])
end

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

function COW_AddPlayerChat(data)
    local obj = Json.decode(data);
    AddPlayerChat(obj["player"], obj["message"])
end

function COW_AttachPlayerParachute(data)
    local obj = Json.decode(data);
    AttachPlayerParachute(obj["player"], obj["enable"])
end

function COW_GetPlayerNetworkStats(data)
    local obj = Json.decode(data);
    local totalPacketLoss_, lastSecondPacketLoss_, messagesInResendBuffer_, bytesInResendBuffer_, bytesSend_, 
    bytesReceived_, bytesResend_, totalBytesSend_, totalBytesReceived_, isLimitedByCongestionControl_, 
    isLimitedByOutgoingBandwidthLimit_ = GetPlayerNetworkStats(obj["player"])
    return Json.encode(
        {
            totalPacketLoss = totalPacketLoss_, 
            lastSecondPacketLoss = lastSecondPacketLoss_, 
            messagesInResendBuffer = messagesInResendBuffer_,
            bytesInResendBuffer = bytesInResendBuffer_,
            bytesSend = bytesSend_, bytesReceived = bytesReceived_,
            bytesResend = bytesResend_, totalBytesSend = totalBytesSend_,
            totalBytesReceived = totalBytesReceived_, 
            isLimitedByCongestionControl = isLimitedByCongestionControl_,
            isLimitedByOutgoingBandwidthLimit = isLimitedByOutgoingBandwidthLimit_
        })
end

function COW_GetPlayerValidation(data)
    return Json.encode({state = IsValidPlayer(Json.decode(data)["entity"])})
end

-- END PLAYER API --

-- START VEHICLE API --

function COW_DestoryVehicle(data)
    local obj = Json.decode(data);
    DestroyVehicle(obj["entity"])
end

-- END VEHICLE API --

-- START DOOR API --

function COW_DestoryDoor(data)
    local obj = Json.decode(data);
    DestroyDoor(obj["entity"])
end

function COW_GetDoorDimension(data)
    return Json.encode({dim = GetDoorDimension(Json.decode(data)["entity"])})
end

function COW_SetDoorDimension(data)
    local obj = Json.decode(data);
    SetDoorDimension(obj["entity"], obj["dim"])
end

function COW_GetDoorPosition(data)
    local x_, y_, z_ = GetDoorLocation(Json.decode(data)["entity"]);
    return Json.encode({x = x_, y = y_, z = z_})
end

function COW_SetDoorPosition(data)
    local obj = Json.decode(data);
    SetDoorLocation(obj["entity"], obj["x"], obj["y"], obj["z"])
end

function COW_GetDoorValidation(data)
    return Json.encode({state = IsValidDoor(Json.decode(data)["entity"])})
end

function COW_GetDoorModel(data)
    return Json.encode({model = GetDoorModel(Json.decode(data)["entity"])})
end

function COW_IsDoorOpen(data)
    return Json.encode({state = IsDoorOpen(Json.decode(data)["entity"])})
end

function COW_SetDoorPosition(data)
    local obj = Json.decode(data);
    SetDoorOpen(obj["entity"], obj["state"])
end

-- END DOOR API --

-- START DIMENSION API --

function COW_CreateExplosion(data)
    local obj = Json.decode(data);
    return Json.encode({success = CreateExplosion(obj["id"], obj["x"], obj["y"], obj["z"], obj["dim"], obj["hasSound"], obj["camShakeRadius"], obj["radialForce"]) })
end

function COW_CreateDoor(data)
    local obj = Json.decode(data);
    return Json.encode({door = CreateDoor(obj["model"], obj["x"], obj["y"], obj["z"], obj["yaw"], obj["interactable"]) })
end

-- END DIMENSION API --

-- START SERVER API --

function COW_GetMaxPlayers(data)
    return Json.encode({val = GetMaxPlayers()})
end

function COW_GetServerTickRate(data)
    return Json.encode({val = GetServerTickRate()})
end

function COW_SetServerName(data)
    local obj = Json.decode(data);
    SetServerName(obj["name"])
end

function COW_GetServerName(data)
    return Json.encode({name = GetServerName()})
end

function COW_ServerExit(data)
    local obj = Json.decode(data);
    ServerExit(obj["reason"])
end

function COW_GetAllPackages(data)
    return Json.encode({packages = GetAllPackages()})
end

function COW_StartPackage(data)
    local obj = Json.decode(data);
    StartPackage(obj["name"])
end

function COW_StopPackage(data)
    local obj = Json.decode(data);
    StopPackage(obj["name"])
end

function COW_IsPackageStarted(data)
    return Json.encode({state = Json.decode(data)["name"]})
end

-- END SERVER API --

-- START TIME API --

function COW_GetTimeSeconds(data)
    return Json.encode({val = GetTimeSeconds()})
end

function COW_GetDeltaSeconds(data)
    return Json.encode({val = GetDeltaSeconds()})
end

function COW_GetTickCount(data)
    return Json.encode({val = GetTickCount()})
end

-- END TIME API --

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
    local len = #args
    if len == 1 then
        CallRemoteEvent(player, eventName, args[1])
    elseif len == 2 then
        CallRemoteEvent(player, eventName, args[1], args[2])
    elseif len == 3 then
        CallRemoteEvent(player, eventName, args[1], args[2], args[3])
    elseif len == 4 then
        CallRemoteEvent(player, eventName, args[1], args[2], args[3], args[4])
    elseif len == 5 then
        CallRemoteEvent(player, eventName, args[1], args[2], args[3], args[4], args[5])
    elseif len == 6 then
        CallRemoteEvent(player, eventName, args[1], args[2], args[3], args[4], args[5], args[6])
    elseif len == 7 then
        CallRemoteEvent(player, eventName, args[1], args[2], args[3], args[4], args[5], args[6], args[7])
    elseif len == 8 then
        CallRemoteEvent(player, eventName, args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8])
    elseif len == 9 then
        CallRemoteEvent(player, eventName, args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9])
    elseif len == 10 then
        CallRemoteEvent(player, eventName, args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10])
    else
        CallRemoteEvent(player, eventName);
    end
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