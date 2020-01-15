using Onset;
using Onset.Entities;
using Onset.Event;
using Onset.Plugin;
using Onset.Utils;

namespace TestPlugin
{
    [Meta("test-plugin", 1, "1.0", IsDebug = true)]
    public class PluginMain : OnsetPlugin
    {
        public static PluginMain Instance { get; private set; }

        public override void Load()
        {
            Instance = this;
            Server.RegisterCommands<TestCommand>();
        }

        public override void Unload()
        {

        }

        [Command("ping")]
        public void OnPingCommand(IPlayer player, long calls = 100)
        {
            long sum = 0;
            for (int i = 0; i < calls; i++)
            {
                long test = Test(player);
                sum += test;
                Logger.Debug("Test nr. " + i + " took " + test + "ns");
            }

            double avg = sum / (double)calls;
            player.SendMessage("Ping result with " + calls + " calls: " + avg);
        }

        private long Test(IPlayer player)
        {
            Time.StartTest();
            player.CallRemote("test-call", "Hallo");
            return Time.StopTest();
        }

        #region 

        [RemoteEvent("respond-client-test")]
        public void OnRespondClientTest(IPlayer player, string arg1, int arg2, bool arg3)
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
            Logger.Debug("Command executed by " + player.Name + ": " + command + " -> " + exists);
        }

        #endregion
    }
}
