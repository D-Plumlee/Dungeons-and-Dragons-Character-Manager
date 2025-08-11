using DnDEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GuiApplication.PopupForms;

/// <summary>
/// Interaction logic for CampAndGroupPopup.xaml
/// </summary>
public partial class CampAndGroupPopup : Window
{
    public DnDEntity EditingEntry
    {
        get => (DnDEntity)GetValue(EditingEntryProperty);
        set => SetValue(EditingEntryProperty, value);
    }   
    public static readonly DependencyProperty EditingEntryProperty =
        DependencyProperty.Register("EditingEntry",
            typeof(DnDEntity),
            typeof(CampAndGroupPopup),
            new PropertyMetadata(null));

    public CampAndGroupPopup()
    {
        InitializeComponent();
        CampaignRadioButton.IsChecked = true;
    }


    public void CampaignChecked(object sender, RoutedEventArgs e)
    {
        CampaignInputs.Visibility = Visibility.Visible;
    }
    public void CampaignUnchecked(object sender, RoutedEventArgs e)
    {
        CampaignInputs.Visibility = Visibility.Collapsed;
    }
    public void GroupChecked(object sender, RoutedEventArgs e)
    {
        GroupInputs.Visibility = Visibility.Visible;
    }
    public void GroupUnchecked(object sender, RoutedEventArgs e)
    {
        GroupInputs.Visibility = Visibility.Collapsed;
    }

    private void RemoveCampaignButton_Click(object sender, RoutedEventArgs e)
    {
        var index = CampaignCombobox.SelectedIndex;
        EditingEntry.Campaign.RemoveAt(index);
    }

    private void AddCampaignButton_Click(object sender, RoutedEventArgs e)
    {
        if(CampaignTextBox.Text.Length > 0)
        {
            EditingEntry.Campaign.Add(CampaignTextBox.Text);
            CampaignTextBox.Text = string.Empty;
        }
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void RemoveGroup_Click(object sender, RoutedEventArgs e)
    {
        var index = GroupCombobox.SelectedIndex;
        EditingEntry.Group.RemoveAt(index);
    }

    private void AddGroup_Click(object sender, RoutedEventArgs e)
    {
        if(GroupTextBox.Text.Length > 0)
        {
            EditingEntry.Group.Add(GroupTextBox.Text);
            GroupTextBox.Text = string.Empty;
        }
    }
}
