using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGestures.Core;
using MGestures.Core.Persistence;

namespace MGestures.App.Gui
{
    /// <summary>
    /// 用于注入“作用于”的应用程序
    /// </summary>
    interface ITargetAppAware
    {
        AbstractApp TargetApp { set; }
    }
}
