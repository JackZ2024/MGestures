namespace MGestures.Core.Commands
{
    interface INeedInit
    {
        void Init();
        bool IsInitialized { get; }
    }
}
