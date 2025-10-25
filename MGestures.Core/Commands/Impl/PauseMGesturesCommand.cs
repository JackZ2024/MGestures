using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGestures.Common.Annotation;

namespace MGestures.Core.Commands.Impl
{
    [Named("暂停MGestures"), Serializable]
    public class PauseMGesturesCommand : AbstractCommand,IGestureParserAware
    {
        public override void Execute()
        {
            Parser.Pause();
        }

        public GestureParser Parser { set; private get; }
    }
}
