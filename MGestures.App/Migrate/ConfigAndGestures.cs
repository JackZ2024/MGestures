using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGestures.Common.Config;
using MGestures.Common.Config.Impl;
using MGestures.Core.Persistence;
using MGestures.Core.Persistence.Impl;

namespace MGestures.App.Migrate
{
    internal class ConfigAndGestures
    {
        public PlistConfig Config { get; private set; }

        public JsonGestureIntentStore GestureIntentStore { get; private set; }

        public ConfigAndGestures(PlistConfig config, JsonGestureIntentStore gestures)
        {
            Config = config;
            GestureIntentStore = gestures;
        }
    }
}
