using Onset;
using Onset.Entities;
using Onset.Enums;
using Onset.Event;
using Onset.Interop;
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

        [ServerEvent(EventType.PlayerJoin)]
        public void OnPlayerJoin(IPlayer player)
        {
            player.SendMessage("Welcome to this Server!");
        }

        [Command("vehicle")]
        public void OnVehicleCommand(IPlayer player, VehicleModel model)
        {
            IVehicle vehicle = Server.Global.CreateVehicle(model, player.Position, player.Heading);
            vehicle.Enter(player, 1);
            player.SendMessage("[TestPlugin] Vehicle spawning successful!");
        }
    }
}
