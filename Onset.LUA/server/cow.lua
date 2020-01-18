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

-- START PLAYER API --

function COW_GetPlayerDimension(data)
    return Json.encode({dim = GetPlayerDimension(Json.decode(data)["entity"])})
end

function COW_SetPlayerDimension(data)
    local obj = Json.decode(data);
    SetPlayerDimension(obj["entity"], obj["dim"])
end

function COW_GetPlayerPosition(data)
    local x_, y_, z_ = GetPlayerLocation(Json.decode(data)["entity"]);
    return Json.encode({x = x_, y = y_, z = z_})
end

function COW_SetPlayerPosition(data)
    local obj = Json.decode(data);
    SetPlayerLocation(obj["entity"], obj["x"], obj["y"], obj["z"])
end

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

function COW_GetPlayerHealth(data)
    return Json.encode({health = GetPlayerHealth(Json.decode(data)["entity"])})
end

function COW_SetPlayerRagdoll(data)
    local obj = Json.decode(data);
    SetPlayerRagdoll(obj["entity"], obj["state"])
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
    return Json.encode({steamID = GetPlayerSteamId(Json.decode(data)["player"])})
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

function COW_IsPlayerTalking(data)
    local obj = Json.decode(data);
    return Json.encode({v = IsPlayerTalking(obj["entity"])})
end

function COW_GetPlayerEquippedWeapon(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerEquippedWeapon(obj["entity"])})
end

function COW_IsPlayerDead(data)
    local obj = Json.decode(data);
    return Json.encode({v = IsPlayerDead(obj["entity"])})
end

function COW_GetPlayerIP(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerIP(obj["entity"])})
end

function COW_GetPlayerPing(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerPing(obj["entity"])})
end

function COW_GetPlayerLocale(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerLocale(obj["entity"])})
end

function COW_GetPlayerGUID(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerGUID(obj["entity"])})
end

function COW_GetPlayerGameVersion(data)
    local obj = Json.decode(data);
    return Json.encode({v = tostring(GetPlayerGameVersion(obj["entity"]))})
end

function COW_GetPlayerVehicleSeat(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerVehicleSeat(obj["entity"])})
end

function COW_GetPlayerVehicle(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerVehicle(obj["entity"])})
end

function COW_IsPlayerReloading(data)
    local obj = Json.decode(data);
    return Json.encode({v = IsPlayerReloading(obj["entity"])})
end

function COW_IsPlayerAiming(data)
    local obj = Json.decode(data);
    return Json.encode({v = IsPlayerAiming(obj["entity"])})
end

function COW_GetPlayerSpeed(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerMovementSpeed(obj["entity"])})
end

function COW_GetPlayerMovementMode(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerMovementMode(obj["entity"])})
end

function COW_GetPlayerState(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerState(obj["entity"])})
end

function COW_SetPlayerVoiceDimension(data)
    local obj = Json.decode(data);
    SetPlayerVoiceDimension(obj["entity"], obj["v"])
end

function COW_GetPlayerRespawnTime(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerRespawnTime(obj["entity"])})
end

function COW_SetPlayerRespawnTime(data)
    local obj = Json.decode(data);
    SetPlayerRespawnTime(obj["entity"], obj["v"])
end

function COW_GetPlayerArmor(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerArmor(obj["entity"])})
end

function COW_SetPlayerArmor(data)
    local obj = Json.decode(data);
    SetPlayerArmor(obj["entity"], obj["v"])
end

function COW_GetPlayerEquippedWeaponSlot(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetPlayerEquippedWeaponSlot(obj["entity"])})
end

function COW_EquipPlayerWeaponSlot(data)
    local obj = Json.decode(data);
    EquipPlayerWeaponSlot(obj["entity"], obj["v"])
end

function COW_SetPlayerSpawnLocation(data)
    local obj = Json.decode(data);
    SetPlayerSpawnLocation(obj["player"], obj["x"], obj["y"], obj["z"], obj["heading"])
end

function COW_SetPlayerSpectate(data)
    local obj = Json.decode(data);
    SetPlayerSpectate(obj["player"], obj["enable"])
end

function COW_KickPlayer(data)
    local obj = Json.decode(data);
    KickPlayer(obj["player"], obj["reason"])
end

function COW_RemovePlayerFromVehicle(data)
    local obj = Json.decode(data);
    RemovePlayerFromVehicle(obj["player"])
end

function COW_SetPlayerIntoVehicle(data)
    local obj = Json.decode(data);
    SetPlayerInVehicle(obj["player"], obj["vehicle"], obj["seat"])
end

function COW_SetPlayerWeaponStat(data)
    local obj = Json.decode(data);
    SetPlayerWeaponStat(obj["player"], obj["weapon"], obj["stat"], obj["value"])
end

function COW_GetPlayerWeapon(data)
    local obj = Json.decode(data);
    local weapon_, ammo_ = GetPlayerWeapon(obj["player"], obj["slot"])
    return Json.encode({weapon = weapon_, ammo = ammo_})
end

function COW_SetPlayerWeapon(data)
    local obj = Json.decode(data);
    SetPlayerWeapon(obj["player"], obj["weapon"], obj["ammo"], obj["equip"], obj["slot"], obj["loaded"])
end

-- END PLAYER API --

-- START VEHICLE API --

function COW_GetVehicleDimension(data)
    return Json.encode({dim = GetVehicleDimension(Json.decode(data)["entity"])})
end

function COW_SetVehicleDimension(data)
    local obj = Json.decode(data);
    SetVehicleDimension(obj["entity"], obj["dim"])
end

function COW_GetVehiclePosition(data)
    local x_, y_, z_ = GetVehicleLocation(Json.decode(data)["entity"]);
    return Json.encode({x = x_, y = y_, z = z_})
end

function COW_SetVehiclePosition(data)
    local obj = Json.decode(data);
    SetVehicleLocation(obj["entity"], obj["x"], obj["y"], obj["z"])
end

function COW_GetVehicleValidation(data)
    return Json.encode({state = IsValidVehicle(Json.decode(data)["entity"])})
end

function COW_SetVehicleProperty(data)
    local obj = Json.decode(data);
    SetVehiclePropertyValue(obj["entity"], obj["key"], obj["value"], obj["sync"])
end

function COW_GetVehicleProperty(data)
    local obj = Json.decode(data);
    return Json.encode({value = GetVehiclePropertyValue(obj["entity"], obj["key"])})
end

function COW_DestroyVehicle(data)
    local obj = Json.decode(data);
    DestroyVehicle(obj["entity"])
end

function COW_IsVehicleStreamedIn(data)
    local obj = Json.decode(data);
    return Json.encode({state = IsVehicleStreamedIn(obj["entity"], obj["player"])})
end

function COW_AttachVehicleNitro(data)
    local obj = Json.decode(data);
    AttachVehicleNitro(obj["entity"], obj["state"])
end

function COW_GetVehicleDamage(data)
    local obj = Json.decode(data);
    return Json.encode({dmg = GetVehicleDamage(obj["entity"], obj["index"])})
end

function COW_SetVehicleDamage(data)
    local obj = Json.decode(data);
    SetVehicleDamage(obj["entity"], obj["index"], obj["damage"])
end

function COW_SetVehicleRespawnParams(data)
    local obj = Json.decode(data);
    SetVehicleRespawnParams(obj["entity"], obj["state"], obj["time"], obj["repair"])
end

function COW_SetVehicleLinearVelocity(data)
    local obj = Json.decode(data);
    SetVehicleLinearVelocity(obj["entity"], obj["x"], obj["y"], obj["z"], obj["reset"])
end

function COW_SetVehicleAngularVelocity(data)
    local obj = Json.decode(data);
    SetVehicleAngularVelocity(obj["entity"], obj["x"], obj["y"], obj["z"], obj["reset"])
end

function COW_GetVehicleDamage(data)
    local obj = Json.decode(data);
    return Json.encode({dmg = GetVehicleDamage(obj["entity"], obj["index"])})
end

function COW_GetVehicleModel(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleModel(obj["entity"])})
end

function COW_GetVehicleVelocity(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleVelocity(obj["entity"])})
end

function COW_GetVehicleDriver(data)
    local obj = Json.decode(data);
    local id = GetVehicleDriver(obj["entity"])
    if id == nil or id == false then
        id = 0
    end
    return Json.encode({v = id})
end

function COW_GetVehicleNumberOfSeats(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleNumberOfSeats(obj["entity"])})
end

function COW_GetVehicleGear(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleGear(obj["entity"])})
end

function COW_GetVehicleLightColor(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleLightColor(obj["entity"])})
end

function COW_GetVehicleLicensePlate(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleLicensePlate(obj["entity"])})
end

function COW_SetVehicleLicensePlate(data)
    local obj = Json.decode(data);
    SetVehicleLicensePlate(obj["entity"], obj["v"])
end

function COW_GetVehicleHealth(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleHealth(obj["entity"])})
end

function COW_SetVehicleHealth(data)
    local obj = Json.decode(data);
    SetVehicleHealth(obj["entity"], obj["v"])
end

function COW_GetVehicleHeading(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleHeading(obj["entity"])})
end

function COW_SetVehicleHeading(data)
    local obj = Json.decode(data);
    SetVehicleHeading(obj["entity"], obj["v"])
end

function COW_GetVehicleColor(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleColor(obj["entity"])})
end

function COW_SetVehicleColor(data)
    local obj = Json.decode(data);
    SetVehicleColor(obj["entity"], obj["v"])
end

function COW_GetVehicleHood(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleHoodRatio(obj["entity"])})
end

function COW_SetVehicleHood(data)
    local obj = Json.decode(data);
    SetVehicleHoodRatio(obj["entity"], obj["v"])
end

function COW_GetVehicleTrunk(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleTrunkRatio(obj["entity"])})
end

function COW_SetVehicleHood(data)
    local obj = Json.decode(data);
    SetVehicleTrunkRatio(obj["entity"], obj["v"])
end

function COW_GetVehicleLightState(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleLightState(obj["entity"])})
end

function COW_SetVehicleLightEnabled(data)
    local obj = Json.decode(data);
    SetVehicleLightEnabled(obj["entity"], obj["v"])
end

function COW_GetVehicleEngineState(data)
    local obj = Json.decode(data);
    return Json.encode({v = GetVehicleEngineState(obj["entity"])})
end

function COW_StartVehicleEngine(data)
    local obj = Json.decode(data);
    StartVehicleEngine(obj["entity"])
end

function COW_StopVehicleEngine(data)
    local obj = Json.decode(data);
    StopVehicleEngine(obj["entity"])
end

-- END VEHICLE API --

-- START OBJECT API --

function COW_GetObjectDimension(data)
    return Json.encode({dim = GetObjectDimension(Json.decode(data)["entity"])})
end

function COW_SetObjectDimension(data)
    local obj = Json.decode(data);
    SetObjectDimension(obj["entity"], obj["dim"])
end

function COW_GetObjectPosition(data)
    local x_, y_, z_ = GetObjectLocation(Json.decode(data)["entity"]);
    return Json.encode({x = x_, y = y_, z = z_})
end

function COW_SetObjectPosition(data)
    local obj = Json.decode(data);
    SetObjectLocation(obj["entity"], obj["x"], obj["y"], obj["z"])
end

function COW_GetObjectValidation(data)
    return Json.encode({state = IsValidObject(Json.decode(data)["entity"])})
end

function COW_SetObjectProperty(data)
    local obj = Json.decode(data);
    SetObjectPropertyValue(obj["entity"], obj["key"], obj["value"], obj["sync"])
end

function COW_GetObjectProperty(data)
    local obj = Json.decode(data);
    return Json.encode({value = GetObjectPropertyValue(obj["entity"], obj["key"])})
end

function COW_DestroyObject(data)
    local obj = Json.decode(data);
    DestroyObject(obj["entity"])
end

function COW_SetObjectAttached(data)
    local obj = Json.decode(data);
    return Json.encode({state = SetObjectAttached(obj["entity"], obj["type"], obj["attach"], obj["x"], obj["y"], obj["z"], obj["rx"], obj["ry"], obj["rz"], obj["socketName"])})
end

function COW_DetachObject(data) 
    local obj = Json.decode(data)
    SetObjectDetached(obj["entity"])
end

function COW_IsObjectStreamedIn(data)
    local obj = Json.decode(data);
    return Json.encode({state = IsObjectStreamedIn(obj["entity"], obj["player"])})
end

function COW_SetObjectStreamDistance(data)
    local obj = Json.decode(data);
    return Json.encode({state = SetObjectStreamDistance(obj["entity"], obj["dist"])})
end

function COW_GetObjectAttachmentInfo(data)
    local obj = Json.decode(data);
    local atype_, attach_ = GetObjectAttachmentInfo(obj["entity"])
    return Json.encode({atype = atype_, attach = attach_})
end

function COW_StopObjectMove(data) 
    local obj = Json.decode(data)
    StopObjectMove(obj["entity"])
end

function COW_SetObjectMoveTo(data)
    local obj = Json.decode(data)
    SetObjectMoveTo(obj["entity"], obj["x"], obj["y"], obj["z"], obj["speed"])
end

function COW_SetObjectRotationAxis(data)
    local obj = Json.decode(data)
    SetObjectRotateAxis(obj["entity"], obj["x"], obj["y"], obj["z"])
end

function COW_IsObjectMoving(data)
    local obj = Json.decode(data);
    return Json.encode({state = IsObjectMoving(obj["entity"])})
end

function COW_GetObjectModel(data)
    local obj = Json.decode(data);
    return Json.encode({model = GetObjectModel(obj["entity"])})
end

function COW_GetObjectScale(data)
    local x_, y_, z_ = GetObjectScale(Json.decode(data)["entity"]);
    return Json.encode({x = x_, y = y_, z = z_})
end

function COW_SetObjectScale(data)
    local obj = Json.decode(data);
    SetObjectScale(obj["entity"], obj["x"], obj["y"], obj["z"])
end

function COW_GetObjectRotation(data)
    local x_, y_, z_ = GetObjectRotation(Json.decode(data)["entity"]);
    return Json.encode({x = x_, y = y_, z = z_})
end

function COW_SetObjectRotation(data)
    local obj = Json.decode(data);
    SetObjectRotation(obj["entity"], obj["x"], obj["y"], obj["z"])
end

-- END OBJECT API --

-- START TEXT3D API --

function COW_SetText3DAttached(data)
    local obj = Json.decode(data);
    return Json.encode({state = SetText3DAttached(obj["entity"], obj["type"], obj["attach"], obj["x"], obj["y"], obj["z"], obj["rx"], obj["ry"], obj["rz"], obj["socketName"])})
end

function COW_SetText3DText(data)
    local obj = Json.decode(data);
    SetText3DText(obj["entity"], obj["text"])
end

function COW_GetText3DDimension(data)
    return Json.encode({dim = GetText3DDimension(Json.decode(data)["entity"])})
end

function COW_SetText3DDimension(data)
    local obj = Json.decode(data);
    SetText3DDimension(obj["entity"], obj["dim"])
end

function COW_GetText3DValidation(data)
    return Json.encode({state = IsValidText3D(Json.decode(data)["entity"])})
end

function COW_SetText3DProperty(data)
    local obj = Json.decode(data);
    SetText3DPropertyValue(obj["entity"], obj["key"], obj["value"], obj["sync"])
end

function COW_GetText3DProperty(data)
    local obj = Json.decode(data);
    return Json.encode({value = GetText3DPropertyValue(obj["entity"], obj["key"])})
end

function COW_DestoryText3D(data)
    local obj = Json.decode(data);
    DestroyText3D(obj["entity"])
end

function COW_SetText3DVisibility(data)
    local obj = Json.decode(data);
    SetText3DVisibility(obj["text"], obj["player"], obj["visible"])
end

-- END TEXT3D API ---


-- START PICKUP API --

function COW_SetPickupVisibility(data)
    local obj = Json.decode(data);
    SetPickupVisibility(obj["pickup"], obj["player"], obj["visible"])
end

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

function COW_CreateText3D(data)
    local obj = Json.decode(data);
    return Json.encode({text = CreateText3D(obj["text"], obj["size"], obj["x"], obj["y"], obj["z"], obj["rx"], obj["ry"], obj["rz"]) })
end

function COW_CreateObject(data)
    local obj = Json.decode(data);
    return Json.encode({obje = CreateObject(obj["model"], obj["x"], obj["y"], obj["z"], obj["rx"], obj["ry"], obj["rz"], obj["sx"], obj["sy"], obj["sz"]) })
end

function COW_CreateVehicle(data)
    local obj = Json.decode(data);
    return Json.encode({vehicle = CreateVehicle(obj["model"], obj["x"], obj["y"], obj["z"], obj["heading"]) })
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
    return ExecuteNET('trigger-event', Json.encode({type = 0, player = playerId}))
end)

AddEvent("OnPlayerChat", function(playerId, text_)
    return ExecuteNET('trigger-event', Json.encode({type = 1, player = playerId, text = text_}))
end)

AddEvent("OnPlayerChatCommand", function(playerId, command_, exists_)
    return ExecuteNET('trigger-event', Json.encode({type = 2, player = playerId, command = command_, exists = exists_}))
end)

AddEvent("OnPlayerJoin", function(playerId)
    return ExecuteNET('trigger-event', Json.encode({type = 3, player = playerId}))
end)

AddEvent("OnPlayerPickupHit", function(playerId, pickup_)
    return ExecuteNET('trigger-event', Json.encode({type = 4, player = playerId, pickup = pickup_}))
end)

AddEvent("OnPackageStart", function()
    return ExecuteNET('trigger-event', Json.encode({type = 6}))
end)

AddEvent("OnPackageStop", function()
    return ExecuteNET('trigger-event', Json.encode({type = 7}))
end)

AddEvent("OnGameTick", function(delta_)
    return ExecuteNET('trigger-event', Json.encode({type = 8, delta = delta_}))
end)

AddEvent("OnClientConnectionRequest", function(ip_, port_)
    return ExecuteNET('trigger-event', Json.encode({type = 9, ip = ip_, port = port_}))
end)

AddEvent("OnNPCReachTarget", function(npc_)
    return ExecuteNET('trigger-event', Json.encode({type = 10, npc = npc_}))
end)

AddEvent("OnNPCDamage", function(npc_, type_, amount_)
    return ExecuteNET('trigger-event', Json.encode({type = 11, npc = npc_, damagetype = type_, amount = amount_}))
end)

AddEvent("OnNPCSpawn", function(npc_)
    return ExecuteNET('trigger-event', Json.encode({type = 12, npc = npc_}))
end)

AddEvent("OnNPCDeath", function(npc_)
    return ExecuteNET('trigger-event', Json.encode({type = 13, npc = npc_}))
end)

AddEvent("OnNPCStreamIn", function(npc_, player_)
    return ExecuteNET('trigger-event', Json.encode({type = 14, npc = npc_, player = player_}))
end)

AddEvent("OnNPCStreamOut", function(npc_, player_)
    return ExecuteNET('trigger-event', Json.encode({type = 15, npc = npc_, player = player_}))
end)

AddEvent("OnPlayerEnterVehicle", function(player_, vehicle_, seat_)
    return ExecuteNET('trigger-event', Json.encode({type = 16, player = player_, vehicle = vehicle_, seat = seat_}))
end)

AddEvent("OnPlayerLeaveVehicle", function(player_, vehicle_, seat_)
    return ExecuteNET('trigger-event', Json.encode({type = 17, player = player_, vehicle = vehicle_, seat = seat_}))
end)

AddEvent("OnPlayerStateChange", function(player_, newstate_, oldstate_)
    return ExecuteNET('trigger-event', Json.encode({type = 18, player = player_, newstate = newstate_, oldstate = oldstate_}))
end)

AddEvent("OnVehicleRespawn", function(vehicle_)
    return ExecuteNET('trigger-event', Json.encode({type = 19, vehicle = vehicle_}))
end)

AddEvent("OnVehicleStreamIn", function(vehicle_, player_)
    return ExecuteNET('trigger-event', Json.encode({type = 20, vehicle = vehicle_, player = player_}))
end)

AddEvent("OnVehicleStreamOut", function(vehicle_, player_)
    return ExecuteNET('trigger-event', Json.encode({type = 21, vehicle = vehicle_, player = player_}))
end)

AddEvent("OnPlayerServerAuth", function(player_)
    return ExecuteNET('trigger-event', Json.encode({type = 22, player = player_}))
end)

AddEvent("OnPlayerSteamAuth", function(player_)
    return ExecuteNET('trigger-event', Json.encode({type = 23, player = player_}))
end)

AddEvent("OnPlayerDownloadFile", function(player_, file_, checksum_)
    return ExecuteNET('trigger-event', Json.encode({type = 24, player = player_, file = file_, checksum = checksum_}))
end)

AddEvent("OnPlayerStreamIn", function(player_, other_)
    return ExecuteNET('trigger-event', Json.encode({type = 25, other = other_, player = player_}))
end)

AddEvent("OnPlayerStreamOut", function(player_, other_)
    return ExecuteNET('trigger-event', Json.encode({type = 26, other = other_, player = player_}))
end)

AddEvent("OnPlayerSpawn", function(player_)
    return ExecuteNET('trigger-event', Json.encode({type = 27, player = player_}))
end)

AddEvent("OnPlayerDeath", function(player_, killer_)
    return ExecuteNET('trigger-event', Json.encode({type = 28, player = player_, killer = killer_}))
end)

AddEvent("OnPlayerDamage", function(player_, type_, amount_)
    return ExecuteNET('trigger-event', Json.encode({type = 30, player = player_, damagetype = type_, amount = amount_}))
end)

AddEvent("OnPlayerInteractDoor", function(player_, door_, bWantsOpen)
    return ExecuteNET('trigger-event', Json.encode({type = 31, player = player_, door = door_, state = bWantsOpen}))
end)

AddEvent("OnPlayerWeaponShot", function(player_, weapon_, hittype_, hitid, hitX, hitY, hitZ, startX, startY, startZ, normalX, normalY, normalZ)
    return ExecuteNET('trigger-event', Json.encode({type = 29, player = player_, weapon = weapon_, hittype = hittype_, entity = hitid, hitx = hitX, hity = hitY, hitz = hitZ, startx = startX, starty = startY, startz = startZ, normalx = normalX, normaly = normalY, normalz = normalZ}))
end)

-- END SERVER EVENTS --

-- END LUA API --
print('COW: LUA Backend loaded!')
print('COW: Trigger Wrapper to finish!')
ExecuteNET('finish-wrapper', '');