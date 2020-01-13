using System;
using System.Collections.Generic;
using System.Text;
using Onset;
using Onset.Entities;
using Onset.Utils;

namespace TestPlugin
{
    public class TestCommand
    {
        [Command("test")]
        public void OnTestCommand(IPlayer player, string test, int arg, bool b)
        {
            long firstTime = Time.CurrentTimeMillis();
            player.CallRemote("trigger-client-test", test, arg, b);
            long newTime = Time.CurrentTimeMillis();
            PluginMain.Instance.Logger.Debug((firstTime - newTime) + "ms for CallRemote!");
        }
    }
}
