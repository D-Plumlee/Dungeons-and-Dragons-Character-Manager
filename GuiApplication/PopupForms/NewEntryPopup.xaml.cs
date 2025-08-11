using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
/// Interaction logic for NewEntryPopup.xaml
/// </summary>
public partial class NewEntryPopup : Popup
{
    public string NewEntryName
    {
        get => (string)GetValue(NewEntryNameProperty);
        set => SetValue(NewEntryNameProperty, value);
    }
    public static readonly DependencyProperty NewEntryNameProperty =
        DependencyProperty.Register("NewEntryName",
            typeof(string),
            typeof(NewEntryPopup),
            new PropertyMetadata(null));

    public ObservableCollection<string> AllFiles
    {
        get => (ObservableCollection<string>)GetValue(AllFilesProperty);
        set => SetValue(AllFilesProperty, value);
    }
    public static readonly DependencyProperty AllFilesProperty =
        DependencyProperty.Register("AllFiles",
            typeof(ObservableCollection<string>),
            typeof(NewEntryPopup),
            new PropertyMetadata(null));

    public string SelectedFile {
        get => (string)GetValue(SelectedFileProperty);
        set => SetValue(SelectedFileProperty, value);
    }
    public static readonly DependencyProperty SelectedFileProperty =
        DependencyProperty.Register("SelectedFile",
            typeof(string),
            typeof(NewEntryPopup),
            new PropertyMetadata(null));

    public NewEntryPopup()
    {
        InitializeComponent();
        NewEntryName = string.Empty;
        SelectedFile = string.Empty;
    }


    public event EventHandler CreateEntryEvent;
    public event EventHandler ClosePopupEvent;

    private void CreateButton_Clicked(object sender, RoutedEventArgs e)
    {
        CreateEntryEvent?.Invoke(this, EventArgs.Empty);
    }

    private void CloseButton_Clicked(object sender, RoutedEventArgs e)
    {
        ClosePopupEvent?.Invoke(this, EventArgs.Empty);
    }
    private void RadioButtonHandler(object sender, RoutedEventArgs e)
    {
        SelectedFile = ((RadioButton)sender).Content.ToString();
    }
}
