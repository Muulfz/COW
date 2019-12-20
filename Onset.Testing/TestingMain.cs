using System;
using Onset.Plugin;

namespace Onset.Testing
{
    [Meta("onset-testing", 1, "1.0", IsDebug = true)]
    public class TestingMain : OnsetPlugin
    {
        public override void Load()
        {
            Logger.Info("Hallo von der Klasse Testing Main!");
        }

        public override void Unload()
        {
            Logger.Debug("Unload von Klasse Testing Main!");
        }
    }
}
