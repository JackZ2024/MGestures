using MGestures.Core.Commands;

namespace MGestures.App.Gui
{
    public interface ICommandView
    {
        AbstractCommand Command { get; set; }

        bool Validate();
    }
}
