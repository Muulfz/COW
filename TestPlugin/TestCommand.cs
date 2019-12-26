using System;
using System.Collections.Generic;
using System.Text;
using Onset;
using Onset.Entities;

namespace TestPlugin
{
    public class TestCommand
    {

        [Command("test")]
        public void OnTestCommand(IPlayer player, string test, int arg, bool b)
        {
            player.CallRemote("trigger-client-test", test, arg, b);
        }
    }
}
