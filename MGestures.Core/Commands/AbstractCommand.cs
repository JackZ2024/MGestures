using System;
using System.ComponentModel;
using MGestures.Common.Annotation;
using MGestures.Core.Annotations;

namespace MGestures.Core.Commands
{
    [Serializable]
    public abstract class AbstractCommand
    {
        public abstract void Execute();

        public virtual string Description()
        {
            return NamedAttribute.GetNameOf(this.GetType());
        }

    }
}
