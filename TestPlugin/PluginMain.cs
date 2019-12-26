using Onset;
using Onset.Entities;
using Onset.Event;
using Onset.Plugin;

namespace TestPlugin
{
    [Meta("test-plugin", 1, "1.0", IsDebug = true)]
    public class PluginMain : OnsetPlugin
    {
        public override void Load()
        {
            
        }

        public override void Unload()
        {
            
        }

        [Command("test")]
        public void OnTestCommand(IPlayer player, string test, int arg, string test2)
        {
            player.CallRemote("trigger-client-test", test, arg, test2);
        }

        [RemoteEvent("respond-client-test")]
        public void OnRespondClientTest(IPlayer player, string arg1, int arg2, string arg3)
        {
            Logger.Debug("Received client test for " + player.Name + ": " + arg1 + " " + arg2 + " " + arg3);
        }

        [ServerEvent(EventType.PlayerJoin)]
        public void OnPlayerJoin(IPlayer player)
        {
            Logger.Debug("Player joined this server yh! " + player.Name);
        }

        [ServerEvent(EventType.PlayerChatCommand)]
        public void OnPlayerChatCommand(IPlayer player, string command, bool exists)
        {
            Logger.Debug("Command executed by " + player.Name + ": " + command + " -> " +  exists);
        }
    }
}
