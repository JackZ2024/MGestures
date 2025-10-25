using System;
using System.Windows.Forms;

namespace MGestures.App.Gui.Windows.Controls
{
    class InstantNumericUpDown : NumericUpDown
    {
        protected override void OnTextBoxTextChanged(object source, EventArgs e)
        {
            base.OnTextBoxTextChanged(source, e);
            ParseEditText();
        }
    }
}
