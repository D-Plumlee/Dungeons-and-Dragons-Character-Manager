using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GuiApplication.PopupForms;

/// <summary>
/// Interaction logic for NewFilePopup.xaml
/// </summary>
public partial class NewFilePopup : Popup
{

    public string NewFileName
    {
        get => (string)GetValue(NewFileNameProperty);
        set => SetValue(NewFileNameProperty, value);
    }
    public static readonly DependencyProperty NewFileNameProperty = 
        DependencyProperty.Register("NewFileName",
            typeof(string),
            typeof(NewFilePopup),
            new PropertyMetadata(null));


    public NewFilePopup()
    {
        InitializeComponent();
        NewFileName = string.Empty;
    }


    public event EventHandler CreateFileEvent;
    public event EventHandler ClosePopupEvent;

    private void CreateButton_Clicked(object sender, RoutedEventArgs e)
    {
        CreateFileEvent?.Invoke(this, EventArgs.Empty);
    }

    private void CloseButton_Clicked(object sender, RoutedEventArgs e)
    {
        ClosePopupEvent?.Invoke(this, EventArgs.Empty);
    }
}
