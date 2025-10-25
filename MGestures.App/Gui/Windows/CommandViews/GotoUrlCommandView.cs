using System;
using MGestures.Core.Commands;
using MGestures.Core.Commands.Impl;

namespace MGestures.App.Gui.Windows.CommandViews
{
    public partial class GotoUrlCommandView : CommandViewUserControl
    {
        public GotoUrlCommandView()
        {
            InitializeComponent();
        }

        private GotoUrlCommand _command;
        public override AbstractCommand Command
        {
            get { return _command; }
            set
            {
                _command = (GotoUrlCommand) value;
                tb_url.Text = _command.Url ?? "";
            }
        }

        private void tb_url_TextChanged(object sender, EventArgs e)
        {
            _command.Url = tb_url.Text;
            OnCommandValueChanged();
        }
    }
}
