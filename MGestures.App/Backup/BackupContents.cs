using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGestures.Core.Persistence.Impl;

namespace MGestures.App.Backup
{
    internal sealed class BackupContents
    {
        public UserSettings Settings { get; }

        public JsonGestureIntentStore GestureIntentStore { get; }

        public BackupContents(UserSettings settings, JsonGestureIntentStore gestures)
        {
            Settings = settings;
            GestureIntentStore = gestures;
        }
    }
}
