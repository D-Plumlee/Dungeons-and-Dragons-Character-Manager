using DnDEntityLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public ObservableCollection<DnDEntityCollection> AllEntries { get; set; }
    public string File { get; set; } 

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
        if(CampaignCombobox.SelectedIndex < 0)
        {
            return;
        }
        var index = CampaignCombobox.SelectedIndex;
        string removedCamp = EditingEntry.Campaign[index];
        EditingEntry.Campaign.RemoveAt(index);

        var col = FindContainerColl();

        if (col != null)
        {
            bool removeCampFromCollection = true;
            foreach(var ent in col.Entities)
            {
                if (ent.Campaign.Contains(removedCamp))
                {
                    removeCampFromCollection = false;   
                    break;
                }
            }
            if (removeCampFromCollection)
            {
                col.Campaigns.Remove(removedCamp);
            }
        }

        
    }

    private void AddCampaignButton_Click(object sender, RoutedEventArgs e)
    {
        if(CampaignTextBox.Text.Length > 0)
        {
            string newCamp = CampaignTextBox.Text;
            EditingEntry.Campaign.Add(newCamp);
            CampaignTextBox.Text = string.Empty;

            var col = FindContainerColl();
            if (col != null)
            {
                if (!col.Campaigns.Contains(newCamp))
                {
                    col.Campaigns.Add(newCamp);
                }
            }
        }
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void RemoveGroup_Click(object sender, RoutedEventArgs e)
    {
        if (GroupCombobox.SelectedIndex < 0)
        {
            return;
        }
        var index = GroupCombobox.SelectedIndex;
        string removedGroup = EditingEntry.Group[index];
        EditingEntry.Group.RemoveAt(index);

        var col = FindContainerColl();

        if (col != null)
        {
            bool removeGroupFromCollection = true;
            foreach (var ent in col.Entities)
            {
                if (ent.Group.Contains(removedGroup))
                {
                    removeGroupFromCollection = false;
                    break;
                }
            }
            if (removeGroupFromCollection)
            {
                col.Groups.Remove(removedGroup);
            }
        }
    }

    private void AddGroup_Click(object sender, RoutedEventArgs e)
    {
        if(GroupTextBox.Text.Length > 0)
        {
            string newGroup = GroupTextBox.Text;
            EditingEntry.Group.Add(newGroup);
            GroupTextBox.Text = string.Empty;

            var col = FindContainerColl();

            if(col != null)
            {
                if (!col.Groups.Contains(newGroup))
                {
                    col.Groups.Add(newGroup);
                }
            }
        }
    }

    private DnDEntityCollection? FindContainerColl()
    {
        foreach(var coll in AllEntries)
        {
            if (coll.Entities.Contains(EditingEntry))
            {
                return coll;
            }
        }
        return null;
    }
}
