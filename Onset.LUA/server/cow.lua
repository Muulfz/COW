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

function COW_Set%ENTITIY%Property(data)
    local obj = Json.decode(data);
    Set%ENTITIY%PropertyValue(obj["entity"], obj["key"], obj["value"], obj["sync"])
end

function COW_Get%ENTITIY%Property(data)
    local obj = Json.decode(data);
    return Json.encode({value = Get%ENTITIY%PropertyValue(obj["entity"], obj["key"])})
end
]]--

-- FOR LIFELESS --
--[[
function COW_Destroy%ENTITIY%(data)
    local obj = Json.decode(data);
    Destroy%ENTITIY%(obj["entity"])
end
]]--
-- FOR LIFELESS --

-- FOR LIVING --
--[[
function COW_Set%ENTITIY%Ragdoll(data)
    local obj = Json.decode(data);
    Set%ENTITIY%Ragdoll(obj["entity"], obj["state"])
end

function COW_Set%ENTITIY%Animation(data)
    local obj = Json.decode(data);
    Set%ENTITIY%Animation(obj["entity"], obj["anim"])
end

function COW_Set%ENTITIY%Health(data)
    local obj = Json.decode(data);
    Set%ENTITIY%Health(obj["entity"], obj["health"])
end

function COW_Get%ENTITIY%HeadSize(data)
    return Json.encode({health = Get%ENTITIY%Health(Json.decode(data)["entity"])})
end

function COW_Set%ENTITIY%Heading(data)
    local obj = Json.decode(data);
    Set%ENTITIY%Heading(obj["entity"], obj["heading"])
end

function COW_Get%ENTITIY%Heading(data)
    return Json.encode({heading = Get%ENTITIY%Heading(Json.decode(data)["entity"])})
end
]]--
-- FOR LIVING --

-- BASE ENTITY FUNCTIONS --

function COW_GetGameVersion(data)
    return Json.encode({version = GetGameVersion()})
end

function COW_GetGameVersionString(data)
    return Json.encode({version = GetGameVersionString()})
end

-- START PICKUP API --

function COW_GetPickupScale(data)
    local x_, y_, z_ = GetPickupScale(Json.decode(data)["entity"]);
    return Json.encode({x = x_, y = y_, z = z_})
end

function COW_SetPickupScale(data)
    local obj = Json.decode(data);
    SetPickupScale(obj["entity"], obj["x"], obj["y"], obj["z"])
end

function COW_GetPickupDimension(data)
    return Json.encode({dim = GetPickupDimension(Json.decode(data)["entity"])})
end

function COW_SetPickupDimension(data)
    local obj = Json.decode(data);
    SetPickupDimension(obj["entity"], obj["dim"])
end

function COW_GetPickupPosition(data)
    local x_, y_, z_ = GetPickupLocation(Json.decode(data)["entity"]);
    return Json.encode({x = x_, y = y_, z = z_})
end

function COW_SetPickupPosition(data)
    local obj = Json.decode(data);
    SetPickupLocation(obj["entity"], obj["x"], obj["y"], obj["z"])
end

function COW_GetPickupValidation(data)
    return Json.encode({state = IsValidPickup(Json.decode(data)["entity"])})
end

function COW_SetPickupProperty(data)
    local obj = Json.decode(data);
    SetPickupPropertyValue(obj["entity"], obj["key"], obj["value"], obj["sync"])
end

function COW_GetPickupProperty(data)
    local obj = Json.decode(data);
    return Json.encode({value = GetPickupPropertyValue(obj["entity"], obj["key"])})
end

function COW_DestroyPickup(data)
    local obj = Json.decode(data);
    DestroyPickup(obj["entity"])
end

-- END PICKUP API --

-- START PLAYER API --

function COW_SetPlayerProperty(data)
    local obj = Json.decode(data);
    SetPlayerPropertyValue(obj["entity"], obj["key"], obj["value"], obj["sync"])
end

function COW_GetPlayerProperty(data)
    local obj = Json.decode(data);
    return Json.encode({value = GetPlayerPropertyValue(obj["entity"], obj["key"])})
end

function COW_SetPlayerHeading(data)
    local obj = Json.decode(data);
    SetPlayerHeading(obj["entity"], obj["heading"])
end

function COW_GetPlayerHeading(data)
    return Json.encode({heading = GetPlayerHeading(Json.decode(data)["entity"])})
end

function COW_SetPlayerHealth(data)
    local obj = Json.decode(data);
    SetPlayerHealth(obj["entity"], obj["health"])
end

function COW_GetPlayerHeadSize(data)
    return Json.encode({health = GetPlayerHealth(Json.decode(data)["entity"])})
end

function COW_SetPlayerRagdoll(data)
    local obj = Json.decode(data);
    SetPlayerRagdoll(obj["entity"], obj["state"])
end

function COW_SetPlayerHeadSize(data)
    local obj = Json.decode(data);
    SetPlayerHeadSize(obj["player"], obj["size"])
end

function COW_GetPlayerHeadSize(data)
    return Json.encode({size = GetPlayerHeadSize(Json.decode(data)["player"])})
end

function COW_SetPlayerHeadSize(data)
    local obj = Json.decode(data);
    SetPlayerHeadSize(obj["player"], obj["size"])
end

function COW_SetPlayerAnimation(data)
    local obj = Json.decode(data);
    SetPlayerAnimation(obj["entity"], obj["anim"])
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

function COW_SetPickupVisibility(data)
    local obj = Json.decode(data);
    SetPickupVisibility(obj["pickup"], obj["player"], obj["visible"])
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

function COW_DestroyVehicle(data)
    local obj = Json.decode(data);
    DestroyVehicle(obj["entity"])
end

-- END VEHICLE API --

-- START DOOR API --

function COW_DestroyDoor(data)
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

-- START NPC API --

function COW_IsNPCStreamedIn(data)
    local obj = Json.decode(data);
    return Json.encode({state = IsNPCStreamedIn(obj["entity"], obj["player"])})
end

function COW_SetNPCFollowVehicle(data)
    local obj = Json.decode(data);
    SetNPCFollowVehicle(obj["entity"], obj["vehicle"], obj["speed"])
end

function COW_SetNPCFollowPlayer(data)
    local obj = Json.decode(data);
    SetNPCFollowPlayer(obj["entity"], obj["player"], obj["speed"])
end

function COW_SetNPCTargetLocation(data)
    local obj = Json.decode(data);
    SetNPCTargetLocation(obj["entity"], obj["x"], obj["y"], obj["z"], obj["speed"])
end

function COW_DestroyNPC(data)
    local obj = Json.decode(data);
    DestroyNPC(obj["entity"])
end

function COW_SetNPCRagdoll(data)
    local obj = Json.decode(data);
    SetNPCRagdoll(obj["entity"], obj["state"])
end

function COW_SetNPCAnimation(data)
    local obj = Json.decode(data);
    SetNPCAnimation(obj["entity"], obj["anim"])
end

function COW_SetNPCHealth(data)
    local obj = Json.decode(data);
    SetNPCHealth(obj["entity"], obj["health"])
end

function COW_GetNPCHeadSize(data)
    return Json.encode({health = GetNPCHealth(Json.decode(data)["entity"])})
end

function COW_SetNPCHeading(data)
    local obj = Json.decode(data);
    SetNPCHeading(obj["entity"], obj["heading"])
end

function COW_GetNPCHeading(data)
    return Json.encode({heading = GetNPCHeading(Json.decode(data)["entity"])})
end

function COW_GetNPCDimension(data)
    return Json.encode({dim = GetNPCDimension(Json.decode(data)["entity"])})
end

function COW_SetNPCDimension(data)
    local obj = Json.decode(data);
    SetNPCDimension(obj["entity"], obj["dim"])
end

function COW_GetNPCPosition(data)
    local x_, y_, z_ = GetNPCLocation(Json.decode(data)["entity"]);
    return Json.encode({x = x_, y = y_, z = z_})
end

function COW_SetNPCPosition(data)
    local obj = Json.decode(data);
    SetNPCLocation(obj["entity"], obj["x"], obj["y"], obj["z"])
end

function COW_GetNPCValidation(data)
    return Json.encode({state = IsValidNPC(Json.decode(data)["entity"])})
end

function COW_SetNPCProperty(data)
    local obj = Json.decode(data);
    SetNPCPropertyValue(obj["entity"], obj["key"], obj["value"], obj["sync"])
end

function COW_GetNPCProperty(data)
    local obj = Json.decode(data);
    return Json.encode({value = GetNPCPropertyValue(obj["entity"], obj["key"])})
end

-- END NPC API --

-- START DIMENSION API --

function COW_CreateExplosion(data)
    local obj = Json.decode(data);
    return Json.encode({success = CreateExplosion(obj["id"], obj["x"], obj["y"], obj["z"], obj["dim"], obj["hasSound"], obj["camShakeRadius"], obj["radialForce"]) })
end

function COW_CreateDoor(data)
    local obj = Json.decode(data);
    return Json.encode({door = CreateDoor(obj["model"], obj["x"], obj["y"], obj["z"], obj["yaw"], obj["interactable"]) })
end

function COW_CreatePickup(data)
    local obj = Json.decode(data);
    return Json.encode({pickup = CreatePickup(obj["model"], obj["x"], obj["y"], obj["z"]) })
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