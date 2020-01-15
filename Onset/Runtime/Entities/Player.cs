using Onset.Entities;

namespace Onset.Runtime.Entities
{
    internal class Player : Living, IPlayer
    {
        public string Name
        {
            get => Wrapper.ExecuteLua("COW_GetPlayerName", new { player = ID }).Value<string>("playerName");
            set => Wrapper.ExecuteLua("COW_SetPlayerName", new { player = ID, playerName = value });
        }

        public long SteamID => Wrapper.ExecuteLua("COW_GetPlayerSteamID", new { player = ID }).Value<long>("steamID");

        public float HeadSize
        {
            get => Wrapper.ExecuteLua("COW_GetPlayerHeadSize", new { player = ID }).Value<float>("size");
            set => Wrapper.ExecuteLua("COW_SetPlayerHeadSize", new { player = ID, size = value });
        }

        public NetworkStats NetworkStats => new NetworkStats(Wrapper.ExecuteLua("COW_GetPlayerNetworkStats", new { player = ID }));

        internal Player(long id) : base(id, "Player")
        {
        }

        public void CallRemote(string name, params object[] args)
        {
            Wrapper.ExecuteLua("COW_CallRemoteEvent", new { eventName = name, player = ID, args });
        }

        public void SendMessage(string message)
        {
            Wrapper.ExecuteLua("COW_AddPlayerChat", new { player = ID, message });
        }

        public void AttachParachute()
        {
            Wrapper.ExecuteLua("COW_AttachPlayerParachute", new { player = ID, enable = true });
        }

        public void DetachParachute()
        {
            Wrapper.ExecuteLua("COW_AttachPlayerParachute", new { player = ID, enable = false });
        }

        public void SetPickupVisibility(IPickup pickup, bool visible = true)
        {
            Wrapper.ExecuteLua("COW_SetPickupVisibility", new {player = ID, pickup = pickup.ID, visible});
        }

        public void SetText3DVisibility(IText3D text, bool visible = true)
        {
            Wrapper.ExecuteLua("COW_SetText3DVisibility", new { player = ID, text = text.ID, visible });
        }
    }
}
