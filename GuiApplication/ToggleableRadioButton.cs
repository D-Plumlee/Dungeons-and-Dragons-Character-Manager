using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GuiApplication;

public class ToggleableRadioButton : RadioButton
{
    protected override void OnClick()
    {
        bool? isChecked = IsChecked;
        base.OnClick();
        if (isChecked == true)
            IsChecked = false;
    }
}

