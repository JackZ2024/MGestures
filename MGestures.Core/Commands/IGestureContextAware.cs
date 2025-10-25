namespace MGestures.Core.Commands
{
    internal interface IGestureContextAware
    {
        GestureContext Context { set; }
    }
}