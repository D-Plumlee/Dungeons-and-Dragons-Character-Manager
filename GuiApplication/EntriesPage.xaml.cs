using DnDEntityLibrary;
using GuiApplication;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Printing;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using DND = DnDEntityLibrary;

namespace GuiApplication;

/// <summary>
/// Interaction logic for EntriesPage.xaml
/// </summary>





public partial class EntriesPage : UserControl
{

    public ObservableCollection< DND::DnDEntityCollection> AllEntries
    {
        get => (ObservableCollection<DND::DnDEntityCollection>)GetValue(AllEntriesProperty);
        set => SetValue(AllEntriesProperty, value);
    } 
    public static readonly DependencyProperty AllEntriesProperty =
        DependencyProperty.Register("AllEntries",
            typeof(ObservableCollection<DND::DnDEntityCollection>),
            typeof(EntriesPage),
            new PropertyMetadata(null));
     public ObservableCollection<string> AllFiles
    {
        get => (ObservableCollection<string>)GetValue(AllFilesProperty);
        set => SetValue(AllFilesProperty, value);
    }
    public static readonly DependencyProperty AllFilesProperty = 
        DependencyProperty.Register("AllFiles",
            typeof(ObservableCollection<string>),
            typeof(EntriesPage),
            new PropertyMetadata(null));

    public ObservableCollection<EntityComponent> DisplayComponents
    {
        get => (ObservableCollection<EntityComponent>)GetValue(DisplayComponentsProperty);
        set => SetValue(DisplayComponentsProperty, value);
    }
    public static readonly DependencyProperty DisplayComponentsProperty =
        DependencyProperty.Register("DisplayComponents",
            typeof(ObservableCollection<EntityComponent>),
            typeof(EntriesPage),
            new PropertyMetadata(null));

    public ObservableCollection<string> ActiveFiles
    {
        get => (ObservableCollection<string>)GetValue(ActiveFilesProperty);
        set => SetValue(ActiveFilesProperty, value);
    }
    public static readonly DependencyProperty ActiveFilesProperty =
        DependencyProperty.Register("ActiveFiles",
            typeof(ObservableCollection<string>),
            typeof(EntriesPage),
            new PropertyMetadata(null));

    public string ActiveCategory
    {
        get => (string)GetValue(ActiveCategoryProperty);
        set => SetValue(ActiveCategoryProperty, value);
    }
    public static readonly DependencyProperty ActiveCategoryProperty =
        DependencyProperty.Register("ActiveCategory",
            typeof(string),
            typeof(EntriesPage),
            new PropertyMetadata(null));
    public ObservableCollection<EntityComponent> CheckedComponents { get; set; } = new ObservableCollection<EntityComponent>();

   


    public EntriesPage()
    {


        DisplayComponents = new ObservableCollection<EntityComponent>();
        AllEntries = new ObservableCollection<DnDEntityCollection>();
            

        InitializeComponent();

        

        NewPopup.CreateFileEvent += (sender, e) =>
        {
            var s = sender as PopupForms.NewFilePopup;
            
            NewPopup.IsOpen = false;

            if (s.NewFileName != null && s.NewFileName != "")
            {
                
                MessageBox.Show($"New file \"{s.NewFileName}.xml\" added!");
                string path = Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + s.NewFileName + ".xml";
                AllFiles.Add(path);
                AllEntries.Add(new DnDEntityCollection() { FileContainedIn = path });
            }
            else
            {
                MessageBox.Show("Invalid file name. File not created");
                
            }

            NewPopup.NewFileName = string.Empty;
        };
        NewPopup.ClosePopupEvent += (sender, e) =>
        {
            var s = sender as PopupForms.NewFilePopup;
            s.IsOpen = false;
            s.NewFileName = string.Empty;
        };

        EntryPopup.CreateEntryEvent += (sender, e) =>
        {
            var s = sender as PopupForms.NewEntryPopup;
            
            s.IsOpen = false;
            if(s.SelectedFile == "" || s.SelectedFile == null)
            {
                MessageBox.Show("File not selected. Entry not created.");
            }
            else if (s.NewEntryName != null && s.NewEntryName != "")
            {
                
                MessageBox.Show($"New entry \"{s.NewEntryName}\" added to {s.SelectedFile}");
                string path = Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + s.SelectedFile ;
                DnDEntity newEntry = new DnDEntity()
                {
                    Name = s.NewEntryName,
                };
                foreach(var coll in AllEntries)
                {
                    if(coll.FileContainedIn == path)
                    {
                        coll.Entities.Add(newEntry);
                        break;
                    }
                }
                 
                NewEntryPageEvent?.Invoke(sender, e);
            }
            else
            {
                MessageBox.Show("Invalid name. Entry not created.");
                
            }

            s.NewEntryName = string.Empty;
        };
        EntryPopup.ClosePopupEvent += (sender, e) =>
        {
            var s = sender as PopupForms.NewEntryPopup;
            s.IsOpen = false;
            s.NewEntryName = string.Empty;
        };

        



        SaveToNewPopup.CreateFileEvent += (sender, e) =>
        {
            var s = sender as PopupForms.NewFilePopup;
            
            

            if (s.NewFileName != null && s.NewFileName != "")
            {
                SaveToNewPopup.IsOpen = false;
                MessageBox.Show($"New file \"{s.NewFileName}.xml\" added!");
                string path = Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + s.NewFileName + ".xml";
                AllFiles.Add(path);

                var compositeCollection = new DnDEntityCollection()
                { 
                    FileContainedIn = path
                };
                foreach (var col in AllEntries)
                {
                    if (!ActiveFiles.Contains(col.FileContainedIn))
                    {
                        continue;
                    }
                    foreach (var ent in col.Entities)
                    {
                        compositeCollection.Entities.Add(ent);
                    }
                    foreach (var camp in col.Campaigns)
                    {
                        if (!compositeCollection.Campaigns.Contains(camp))
                        {
                            compositeCollection.Campaigns.Add(camp);
                        }
                    }
                    foreach (var grp in col.Groups)
                    {
                        if (!compositeCollection.Groups.Contains(grp))
                        {
                            compositeCollection.Groups.Add(grp);
                        }
                    }
                }
                compositeCollection.IsChanged = false;

                AllEntries.Add(compositeCollection);
                DND::DnDEntityCollectionXmlConverter.DnDEntityCollectionToXml(compositeCollection, compositeCollection.FileContainedIn);
            }
            else
            {
                MessageBox.Show("Invalid file name. File not created");
                
            }

            NewPopup.NewFileName = string.Empty;
        };
        SaveToNewPopup.ClosePopupEvent += (sender, e) =>
        {
            var s = sender as PopupForms.NewFilePopup;
            s.IsOpen = false;
            s.NewFileName = string.Empty;

        };

        //this.DataContext = CategoryMatchingComponents;
    }



    public event EventHandler NewEntryPageEvent;
    public void InitializeDisplayComponents()
    {
        foreach(var comp in AllEntries)
        {
            foreach(var ent in comp.Entities) {
                var nextComp = new EntityComponent()
                {
                    FileContainedIn = comp.FileContainedIn,
                    RepresentedEntity = ent,
                    //ActiveFiles = ActiveFiles,
                    //ActiveCategory = ActiveCategory
                };
                nextComp.ResetComboBoxes();
                nextComp.SingleEntryEvent += BubbleEntryEvent;
                DisplayComponents.Add(nextComp);
            }
        }
    }


    public void setComponents()
    {

    }


    public event EventHandler BubbleEntryEvent;

    public void MidPointBubbleEventMethod(object sender, EventArgs e)
    {
        var s = sender as EntityComponent;
        string passFileName = (from fn in AllEntries where fn.Entities.Contains(s.RepresentedEntity) select fn).First().FileContainedIn;
        BubbleEntryEvent?.Invoke(sender, new BubbleEventArgs { FileName = passFileName});
    }


    private void SearchFilter_TextChanged(object sender, TextChangedEventArgs e)
    {

        // Does not work with current DataBinding iteration of Entity Component list

        //string filter = SearchFilter.Text;
        //if(filter.Length == 0)
        //{
        //    foreach(var outeritem in ComponentBox.Items) 
        //    {
        //        foreach(var inneritem in ((ItemsControl)outeritem).Items) { 
        //        //if (ec.IsValid) { 
        //        var ec = inneritem as EntityComponent;
        //            ec.Visibility = Visibility.Visible;
        //        //}
        //        }
        //    }
        //}
        //else
        //{
        //    foreach (var outeritem in ComponentBox.Items)
        //    {
        //        foreach (var inneritem in ((ItemsControl)outeritem).Items)
        //        {
        //            //if (ec.IsValid) { 
        //            var ec = inneritem as EntityComponent;
                    
        //            //}
                
        //    if (ec.RepresentedEntity.Name.Contains(filter, StringComparison.OrdinalIgnoreCase))
        //        {
        //            ec.Visibility = Visibility.Visible;
        //        }
        //        else
        //        {
        //            ec.Visibility= Visibility.Collapsed;
        //        }
        //    }
            
        //    }
        //}
    }



    private void NewFileMenuButton_Clicked(object sender, RoutedEventArgs e)
    {
        NewPopup.IsOpen = true;
    }

    private void NewEntry_Click(object sender, RoutedEventArgs e)
    {
        EntryPopup.IsOpen = true;
    }

    private void FileManager_Close(object sender, RoutedEventArgs e)
    {
        // Should cause view source to update,
        // could not figure out how to trigger it.

        // Updates only the filter, not triggering INotifyPropertyChanged
        // to run the filter again.
    }

    


    private void MenuItem_Loaded(object sender, RoutedEventArgs e)
    {
        var s = (MenuItem)sender;
        var header = s.Header.ToString();
        if(ActiveFiles.Contains(Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + header))
        {
            s.IsChecked = true;
        }
    }

    private void ActiveFileButton_Checked(object sender, RoutedEventArgs e)
    {
        var s = (MenuItem)sender;
        if(!ActiveFiles.Contains(Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + s.Header.ToString())) 
        {
        
        ActiveFiles.Add(Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + s.Header.ToString());
        }
    }
    private void ActiveFileButton_Unchecked(object sender, RoutedEventArgs e)
    {
        var s = (MenuItem)sender;
        if (ActiveFiles.Contains(Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + s.Header.ToString()))
        {
            ActiveFiles.Remove(Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + s.Header.ToString());
        }
    }

    private void EntriesFilesValidated_Filter(object sender, FilterEventArgs e)
    {
        DnDEntityCollection coll = e.Item as DnDEntityCollection;
        if (!ActiveFiles.Contains(coll.FileContainedIn))
        {
            e.Accepted = false;
            
        }
        else if(ActiveCategory == "View All Entries")
        {
            e.Accepted = true;
        }
        else if (coll.Campaigns.Contains(ActiveCategory))
        {
            e.Accepted = true;
            
        }
        else if (coll.Groups.Contains(ActiveCategory))
        {
            e.Accepted = true;
            
        }
        else
        {
            e.Accepted = false;
        }
    }

    private void EntitiesGroupingsValidated_Filter(object sender, FilterEventArgs e)
    {

    }

    private void Component_LoadBoxes(object sender, RoutedEventArgs e)
    {
        var comp = sender as EntityComponent;
        comp.WeaponBox.SelectedIndex = comp.WeaponBox.Items.Count == 0 ? -1 : 0;
        comp.SpellsBox.SelectedIndex = comp.SpellsBox.Items.Count == 0 ? -1 : 0;
        comp.Display = 
            (ActiveCategory == "View All Entries" || 
            comp.RepresentedEntity.Campaign.Contains(ActiveCategory) || 
            comp.RepresentedEntity.Group.Contains(ActiveCategory)) ?
            Visibility.Visible : Visibility.Collapsed;
    }



    private void ComponentChecked(object sender, RoutedEventArgs e)
    {
       CheckedComponents.Add((EntityComponent)sender);
    } 

    private void ComponentUnchecked(object sender, RoutedEventArgs e)
    {
        CheckedComponents.Remove((EntityComponent)sender);
    }


    private void DeleteEntries_Click(object sender, RoutedEventArgs e)
    {
        var confirmation = MessageBox.Show("Delete selected entries from collection? Entries will be lost forever once changes are saved.", "Confirm Delete?", MessageBoxButton.YesNo);
        if (confirmation == MessageBoxResult.No)
        {
            return;
        }
        foreach(EntityComponent comp in CheckedComponents)
        {
            var entity = comp.RepresentedEntity;
            foreach(var coll in AllEntries)
            {
                if (coll.Entities.Contains(entity))
                {
                    coll.Entities.Remove(entity);
                }
            }
        }
    }


    private void DeselectEntries_Click(object sender, RoutedEventArgs e)
    {
        foreach(var comp in CheckedComponents.ToList())
        {
            comp.ManualInvokeUncheck();
        }
    }

    private void Entries_Loaded(object sender, RoutedEventArgs e)
    {
        ListCollectionView view = new ListCollectionView(AllEntries)
        {
            Filter = coll => ActiveFiles.Contains(((DnDEntityCollection)coll).FileContainedIn)
                && (ActiveCategory == "View All Entries"
                || ((DnDEntityCollection)coll).Campaigns.Contains(ActiveCategory)
                || ((DnDEntityCollection)coll).Groups.Contains(ActiveCategory)),
            IsLiveFiltering = true,
            LiveFilteringProperties = { nameof(DnDEntityCollection.FileContainedIn), nameof(DnDEntityCollection.Groups), nameof(DnDEntityCollection.Campaigns), nameof(ActiveFiles) },
            
        };

        var myBinding = new Binding()
        {
            Source = view,
            NotifyOnSourceUpdated = true,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,

        };

        ComponentBox.SetBinding(ItemsControl.ItemsSourceProperty, myBinding);
        

    }

    private void SaveToOrigins_Click(object sender, RoutedEventArgs e)
    {
        string msg = "Save files "; 
        foreach(var fil in ActiveFiles)
        {
            var temp = fil.Substring(fil.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
            msg += $"\"{temp}\", ";
        }
        msg = msg.Remove(msg.Length - 2);
        msg += "?";
        var result = MessageBox.Show(msg, "Save Current View", MessageBoxButton.YesNo);
        if(result == MessageBoxResult.No)
        {
            MessageBox.Show("Files not saved.");
            return;
        }

        foreach(var coll in AllEntries)
        {
            if (ActiveFiles.Contains(coll.FileContainedIn))
            {
                DND::DnDEntityCollectionXmlConverter.DnDEntityCollectionToXml(coll, coll.FileContainedIn);
            }
        }
        MessageBox.Show("All selected files saved.");
    }

    private void SaveToNew_Click(object sender, RoutedEventArgs e)
    {
        SaveToNewPopup.IsOpen = true;
    }


}
public class BubbleEventArgs : EventArgs
{
    public string FileName { get; set; }
}
