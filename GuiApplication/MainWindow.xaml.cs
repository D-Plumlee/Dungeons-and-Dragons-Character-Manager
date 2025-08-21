using DnDEntityLibrary;
using DnDEntityLibrary.Inventory;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DND = DnDEntityLibrary;
using DNDINV = DnDEntityLibrary.Inventory;
using DNDSPL = DnDEntityLibrary.SpellList;
using System.Windows.Controls.Primitives;


namespace GuiApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        protected ObservableCollection<DND::DnDEntityCollection> _allEntriesByFile;


        private ObservableCollection<TabItem> _tabs;
        private TabItem _lastTab;

        public ObservableCollection<string> XmlFiles
        {   get => (ObservableCollection<string>)GetValue(XmlFilesProperty);
            set => SetValue(XmlFilesProperty, value);
        }
        public static readonly DependencyProperty XmlFilesProperty = 
            DependencyProperty.Register("XmlFiles",
                typeof(ObservableCollection<string>),
                typeof(MainWindow), 
                new PropertyMetadata(null));


        protected ObservableCollection<string> _selectedFiles { get; set; }
        private string _directoryPath;

        
        public MainWindow()
        {
            try
            {
                _selectedFiles = new ObservableCollection<string>();
                _tabs = new ObservableCollection<TabItem>();
                
                InitializeComponent();
                this.Closing += MainWindow_Closing;

                _lastTab = TabManager.SelectedItem as TabItem;
                _tabs.Add(_lastTab);

                _directoryPath = Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}"; /*   Use _directoryPath to write fully qualified names of new files  !!!   */

                XmlFiles = new ObservableCollection<string>();  

                foreach(var f in (from file
                           in Directory.GetFiles(_directoryPath)
                                  where file.Contains(".xml")
                                  select file))
                {
                    XmlFiles.Add(f);
                }
                
                
                
                _allEntriesByFile = new ObservableCollection<DND::DnDEntityCollection>();


                
                List<string> ignoreFiles = new List<string>();
                foreach (string file in XmlFiles)
                {
                    DND::DnDEntityCollection col = DND::DnDEntityCollectionXmlConverter.LoadXmlCollectionFromFile(file);
                    col.FileContainedIn = file;
                    if(col.Entities.Count == 0)
                    {
                        // LoadXmlCollectionFromFile returns a new collection on exception, so an empty collection likely means the file 
                        // is unrelated
                        ignoreFiles.Add(file);
                        
                        continue;
                    }
                    
                    ObservableCollection<string> camps = new ObservableCollection<string>();
                    ObservableCollection<string> groups = new ObservableCollection<string>();
                    foreach(DND::DnDEntity ent in col.Entities)
                    {
                        foreach(var camp in (from c in ent.Campaign where !camps.Contains(c) select c).Distinct()) 
                        { 
                            camps.Add(camp);
                        }
                        foreach(var grup in (from g in ent.Group where !groups.Contains(g) select g).Distinct())
                        {
                            groups.Add(grup);
                        }
                        

                    }

                    col.Campaigns = camps;
                    col.Groups = groups;
                    

                    _allEntriesByFile.Add(col);
                }   
                foreach(string f in ignoreFiles)
                {
                    XmlFiles.Remove(f);
                }    

                

                foreach (var s in _allEntriesByFile)
                {
                    foreach (DnDEntity ent in s.Entities)
                    {
                        ScrubEquipped(ent);
                    }
                }

                NewPopup.CreateFileEvent += (sender, e) =>
                {
                    var s = sender as PopupForms.NewFilePopup;
                    if(s.NewFileName != null && s.NewFileName != "") { 
                    string path = _directoryPath + s.NewFileName + ".xml"; 
                    XmlFiles.Add(path);
                    _allEntriesByFile.Add(new DnDEntityCollection() { FileContainedIn = path });
                    //LoadCheckboxes();
                    }
                    else
                    {
                        MessageBox.Show("Invalid file name. File not created");
                    }
                    NewPopup.IsOpen = false;

                    NewPopup.NewFileName = string.Empty;
                };
                NewPopup.ClosePopupEvent += (sender, e) =>
                {
                    var s = sender as PopupForms.NewFilePopup;
                    s.IsOpen = false;
                    s.NewFileName = string.Empty;

                };


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        } // End MainWindow()



        private void ScrubEquipped(DnDEntity ent)
        {
            bool alreadyEquipped = false;
            foreach(Weapon w in ent.Inventory.Weapons)
            {
                if (alreadyEquipped)
                {
                    w.Equipped = false;
                }
                else if (w.Equipped)
                {
                    alreadyEquipped = true;
                }
            }

            alreadyEquipped = false;
            foreach (Shield s in ent.Inventory.Shields)
            {
                if (alreadyEquipped)
                {
                    s.Equipped = false;
                }
                else if (s.Equipped)
                {
                    alreadyEquipped = true;
                }
            }

            alreadyEquipped = false;
            foreach (Armor a in ent.Inventory.Armors)
            {
                if (alreadyEquipped)
                {
                    a.Equipped = false;
                }
                else if (a.Equipped)
                {
                    alreadyEquipped = true;
                }
            }
        }

        private void tabManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        } // End tabManager_SelectionChanged()

        protected void AddTabItem(TabItem page)
        {
            var header = (string)page.Header;
            //var hContent = (string)header.Content;
            var newheader = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };
            newheader.Children.Add(new TextBlock
            {
                Margin = new Thickness(0, 0, 5, 0),
                Text = header
            });
            var buttn = new Button
            {
                Content = "X",
            };
            buttn.Click += btnDelete_Tab;
            newheader.Children.Add(buttn);
            page.Header = newheader;
            _tabs.Add(page);
            _lastTab = page;
            TabManager.Items.Add(page);
            TabManager.UpdateLayout(); 
            //TabManager.DataContext = null;
            //TabManager.
            //TabManager.DataContext = _tabs;
            TabManager.SelectedItem = page;
        } // End AddTabItem()

        private void btnDelete_Tab(object sender, RoutedEventArgs e)
        {
            var tabHeaderContent = (sender as Button).Parent as StackPanel;
            var tabName = (tabHeaderContent.Parent as TabItem).Name.ToString();

            var tab = TabManager.Items.Cast<TabItem>().Where(
                i => i.Name.Equals(tabName)).SingleOrDefault();
            //TabItem tab = item as TabItem;

            if (tab != null)
            {
                TabItem selectedTab = TabManager.SelectedItem as TabItem;

                //TabManager.DataContext = null;
                _tabs.Remove(tab);
                TabManager.Items.Remove(tab);
                //TabManager.DataContext = _tabs;
                if (selectedTab == null || selectedTab.Equals(tab))
                {
                    selectedTab = _tabs[0];
                }
                TabManager.SelectedItem = TabManager.Items.GetItemAt(0);
            }
        } // End btnDelete_Tab()
        private void Delete_Tab(object caller)
        {
            var tab = (caller as TabItem);


            if (tab != null)
            {
                TabItem selectedTab = TabManager.SelectedItem as TabItem;

                //TabManager.DataContext = null;
                _tabs.Remove(tab);
                TabManager.Items.Remove(tab);
                //TabManager.DataContext = _tabs;
                if (selectedTab == null || selectedTab.Equals(tab))
                {
                    selectedTab = _tabs[0];
                }
                TabManager.SelectedItem = TabManager.Items.GetItemAt(0);
            }
        } // End btnDelete_Tab()

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var box = sender as CheckBox;
            var name = box.Content.ToString();
            var fullyQualifiedName = (from c in XmlFiles where c.Contains(name) select c).Last();
            _selectedFiles.Add(fullyQualifiedName); 
            
            UpdateViewSelectionList();
        } // End CheckBox_Checked()

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var box = sender as CheckBox;
            var name = box.Content.ToString();
            var fullyQualifiedName = (from c in XmlFiles where c.Contains(name) select c).Last();
            _selectedFiles.Remove(fullyQualifiedName);
           
            UpdateViewSelectionList();
        } // End CheckBox_Unchecked

        private void UpdateViewSelectionList()
        {

            var campButtonSource = new CompositeCollection();
            foreach (var fil in _selectedFiles)
            {
                var col = (from c in _allEntriesByFile where c.FileContainedIn.Equals(fil) select c).FirstOrDefault();
                CollectionContainer collCont = new CollectionContainer() { Collection = col.Campaigns };
                campButtonSource.Add(collCont);
            }

            var groupButtonSource = new CompositeCollection();
            foreach (var fil in _selectedFiles)
            {
                var col = (from c in _allEntriesByFile where c.FileContainedIn.Equals(fil) select c).FirstOrDefault();
                CollectionContainer collCont = new CollectionContainer() { Collection = col.Groups };
                groupButtonSource.Add(collCont);
            }

            CampaignButtons.ItemsSource = campButtonSource;
            GroupButtons.ItemsSource = groupButtonSource;

        } // End UpdateViewSelectionList()




        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // See if the user wants to save before exiting
            string msg = "Would you like to save your changes before closing?";
            MessageBoxResult result = MessageBox.Show(msg, "Save Dialog", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes)
            {
                // Save data
                // Ensure data from all tabs is saved
                // Add close dialogs for tabs to validate saves? Prob

                foreach(var f in _allEntriesByFile)
                {
                    DnDEntityCollectionXmlConverter.DnDEntityCollectionToXml(f,f.FileContainedIn);
                }

                MessageBox.Show("Saving... Done!"); // Keep something like this for users to receive feedback
            }
            else if(result == MessageBoxResult.Cancel){
                e.Cancel = true;
            }
            // Cancel stops the window from closing, Yes saves and closes, No closes without saving

        } // End MainWindow_Closing()

        private void ViewSelectionList_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string selectionCriteria = btn.Content.ToString();
            ObservableCollection<EntityComponent> displayComponents;

            ObservableCollection<string> selectedFilesCopy = new ObservableCollection<string>();
            foreach(string f in _selectedFiles)
            {
                selectedFilesCopy.Add(f);
            }

            EntriesPage newDisplay = new EntriesPage() // right now returns null for three top buttons, implement them for testing EntriesPage
            {
                ActiveFiles = selectedFilesCopy,
                ActiveCategory = selectionCriteria,
                AllEntries = _allEntriesByFile,
                AllFiles = XmlFiles
            };
            newDisplay.BubbleEntryEvent += AddSingleEntryPage;
            newDisplay.NewEntryPageEvent += AddNewSingleEntryPage;
           // newDisplay.InitializeDisplayComponents();
            AddTabItem(new TabItem()
            {
                Content = newDisplay,   
                Header = selectionCriteria,
                Name = "x"+(TabManager.Items.Count)
            }); 
        }

        public void AddSingleEntryPage(object sender, EventArgs e)
        {
            var CallingEntry = sender as EntityComponent;
            var beargs = e as BubbleEventArgs;

            SingleEntryDisplay newPage = new SingleEntryDisplay()
            {
                FileContainedIn = beargs.FileName,
                RepresentedEntity = 
                    (from subEntity 
                     in ((DnDEntityCollection)(from ent 
                                               in _allEntriesByFile 
                                               where ent.FileContainedIn == beargs.FileName 
                                               select ent).First()).Entities
                     where subEntity.Name == CallingEntry.RepresentedEntity.Name 
                     select subEntity).FirstOrDefault(),

                AllEntriesByFile = _allEntriesByFile
            };
            
            newPage.BubblePopupConfirm += (sender, e) =>
            {
                var s = sender as PopupForms.NewFilePopup;
                if (s.NewFileName != null && s.NewFileName != "")
                {
                    NewPopup.IsOpen = false;
                    MessageBox.Show($"New file \"{s.NewFileName}.xml\" added!");
                    string path = Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + s.NewFileName + ".xml";
                    XmlFiles.Add(path);
                    _allEntriesByFile.Add(new DnDEntityCollection() { FileContainedIn = path });
                }
                else
                {
                    MessageBox.Show("Invalid file name. File not created");
                    NewPopup.IsOpen = false;
                }

                NewPopup.NewFileName = string.Empty;
            };


            newPage.DeleteTabEvent += (sender, e) =>
            {
                var thisTab = newPage.Parent as TabItem;
                Delete_Tab(thisTab);
                MessageBox.Show($"Entry \"{newPage.RepresentedEntity.Name}\" deleted.");
            };


            newPage.LoadEntryData();
            AddTabItem(new TabItem()
            {
                Content = newPage,
                Header = newPage.RepresentedEntity.Name,
                Name = "x" + (TabManager.Items.Count)
            });

        }
        public void AddNewSingleEntryPage(object sender, EventArgs e)
        {
            var Caller = sender as PopupForms.NewEntryPopup;
            

            SingleEntryDisplay newPage = new SingleEntryDisplay()
            {
                FileContainedIn = Caller.SelectedFile,
                RepresentedEntity = 
                    (from subEntity 
                     in ((DnDEntityCollection)(from ent 
                                               in _allEntriesByFile 
                                               where ent.FileContainedIn == (Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + Caller.SelectedFile)
                                               select ent).First()).Entities
                     where subEntity.Name == Caller.NewEntryName
                     select subEntity).FirstOrDefault(),

                AllEntriesByFile = _allEntriesByFile
            };
            
            newPage.BubblePopupConfirm += (sender, e) =>
            {
                var s = sender as PopupForms.NewFilePopup;
                if (s.NewFileName != null && s.NewFileName != "")
                {
                    NewPopup.IsOpen = false;
                    MessageBox.Show($"New file \"{s.NewFileName}.xml\" added!");
                    string path = Directory.GetCurrentDirectory() + $"{System.IO.Path.DirectorySeparatorChar}" + s.NewFileName + ".xml";
                    XmlFiles.Add(path);
                    _allEntriesByFile.Add(new DnDEntityCollection() { FileContainedIn = path });
                }
                else
                {
                    MessageBox.Show("Invalid file name. File not created");
                    NewPopup.IsOpen = false;
                }

                NewPopup.NewFileName = string.Empty;
            };


            newPage.DeleteTabEvent += (sender, e) =>
            {
                var thisTab = newPage.Parent as TabItem;
                Delete_Tab(thisTab);
                MessageBox.Show($"Entry \"{newPage.RepresentedEntity.Name}\" deleted.");
            };


            newPage.LoadEntryData();
            AddTabItem(new TabItem()
            {
                Content = newPage,
                Header = newPage.RepresentedEntity.Name,
                Name = "x" + (TabManager.Items.Count)
            });

        }

        private void NewFileButton_Clicked(object sender, RoutedEventArgs e)
        {
            NewPopup.IsOpen = true;
            NewPopup.NewFileName = string.Empty;
        }


    }
}