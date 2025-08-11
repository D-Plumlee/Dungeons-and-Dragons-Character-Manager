using DnDEntityLibrary.Inventory;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DND = DnDEntityLibrary;
using DNDINV = DnDEntityLibrary.Inventory;

namespace GuiApplication
{
    /// <summary>
    /// Interaction logic for EntityComponent.xaml
    /// </summary>
    public partial class EntityComponent : UserControl
    {
        public string FileContainedIn
        {
            get => (string)GetValue(FileContainedInProperty);
            set => SetValue(FileContainedInProperty, value);
        }
        public static readonly DependencyProperty FileContainedInProperty =
            DependencyProperty.Register("FileContainedIn",
                typeof(string),
                typeof(EntityComponent),
                new PropertyMetadata(null));
        public DND::DnDEntity RepresentedEntity 
        {
            get => (DND::DnDEntity)GetValue(RepresentedEntityProperty);
            set => SetValue(RepresentedEntityProperty, value);
        }
        
        public static readonly DependencyProperty RepresentedEntityProperty =
            DependencyProperty.Register("RepresentedEntity",
                typeof(DND::DnDEntity),
                typeof(EntityComponent),
                new PropertyMetadata(null));

        //public ExpandedEntityComponent ExpandedVersion { get; set; }      // replace with a method for returning the alternative in each
        //public DNDINV::Weapon EquippedWeapon 
        //{ 
        //    get => (DNDINV::Weapon)GetValue(EquippedWeaponProperty);
        //    set => SetValue(EquippedWeaponProperty, value); 
        //}
        //public static readonly DependencyProperty EquippedWeaponProperty =
        //    DependencyProperty.Register("EquippedWeapon",
        //        typeof(DNDINV::Weapon),
        //        typeof(EntityComponent),
        //        new PropertyMetadata(null));   //, new PropertyChangedCallback(WeaponEquippedChanged));


        ////public ObservableCollection<string> ActiveFiles
        ////{
        ////    get => (ObservableCollection<string>)GetValue(ActiveFilesProperty);
        ////    set => SetValue(ActiveFilesProperty, value);
        ////}
        ////public static readonly DependencyProperty ActiveFilesProperty =
        ////    DependencyProperty.Register("ActiveFiles",
        ////        typeof(ObservableCollection<string>),
        ////        typeof(EntriesPage),
        ////        new PropertyMetadata(null));

        ////public string ActiveCategory
        ////{
        ////    get => (string)GetValue(ActiveCategoryProperty);
        ////    set => SetValue(ActiveCategoryProperty, value);
        ////}
        ////public static readonly DependencyProperty ActiveCategoryProperty =
        ////    DependencyProperty.Register("ActiveCategory",
        ////        typeof(string),
        ////        typeof(EntriesPage),
        ////        new PropertyMetadata(null));
        //public ObservableCollection<string> ActiveFiles { get; set; } 
        //public string ActiveCategory { get; set; }

        public Visibility Display
        {
            get => (Visibility)GetValue(DisplayProperty);
            set => SetValue(DisplayProperty, value);
        }
        public static readonly DependencyProperty DisplayProperty =
            DependencyProperty.Register("Display",
                typeof(Visibility),
                typeof(EntityComponent),
                new PropertyMetadata(null));

        public EntityComponent()
        {
            //EquippedWeapon = (DNDINV::Weapon)(from w in RepresentedEntity.Inventory.Weapons where w.Equipped select w);
            InitializeComponent();
            ds1.GroupName = "x" + this.GetHashCode() + "1";
            ds2.GroupName = "x" + this.GetHashCode() + "2";
            ds3.GroupName = "x" + this.GetHashCode() + "3";
            ds4.GroupName = "x" + this.GetHashCode() + "4";
            ds5.GroupName = "x" + this.GetHashCode() + "5";
            ds6.GroupName = "x" + this.GetHashCode() + "6";  
        }

        //private static void WeaponEquippedChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        //{
        //    EntityComponent comp = (EntityComponent)depObj;
        //    comp.EquippedWeapon = ()
        //}


        public void ResetComboBoxes()
        {
            //var weps = WeaponBox;
            //var styles = WeaponStyleBox;
            //var abilSpells = SpellsBox;

            //weps.Items.Clear();
            //foreach (Weapon w in RepresentedEntity.Inventory.Weapons)
            //{
            //    weps.Items.Add(w);
            //    if (w.Equipped)
            //    {
            //        weps.SelectedItem = w;
            //    }
            //}
           

            //EquippedWeapon = (from w in RepresentedEntity.Inventory.Weapons where w.Equipped == true select w).FirstOrDefault(); 
            
            if(SpellsBox.Items.Count > 0)
            {
                SpellsBox.SelectedIndex = 0;
            }
            //Binding myBinding = new Binding("Equipped");
            //myBinding.Source = RepresentedEntity.Inventory.Weapons;
            //WeaponBox.SetBinding(, myBinding);
            //styles.Items.Clear();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EntityComponentWrapPanel.Visibility = (EntityComponentWrapPanel.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
            ExpandedEntityComponentStackPanel.Visibility = (ExpandedEntityComponentStackPanel.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
            
        }


        public event EventHandler SingleEntryEvent;

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            SingleEntryEvent?.Invoke(this, new EventArgs() { });
        }

        private void EntityComponentWrap_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            //if (!ActiveFiles.Contains(FileContainedIn))
            //{
            //    IsValid = false; 
            //    return;
            //}
            //foreach(var c in RepresentedEntity.Campaign)
            //{
            //    if(ActiveCategory == c)
            //    {
            //        IsValid = true;
            //        return;
            //    }
            //}
            //foreach(var g in RepresentedEntity.Group)
            //{
            //    if(ActiveCategory == g)
            //    {
            //        IsValid = true;
            //        return;
            //    }
            //}
            //IsValid = false;
            //return;
        }

        public event RoutedEventHandler BubbleCheckboxCheckedEvent;
        public event RoutedEventHandler BubbleCheckboxUncheckedEvent;


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            BubbleCheckboxCheckedEvent?.Invoke(this, e);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            BubbleCheckboxUncheckedEvent?.Invoke(this, e);
        }

        public void ManualInvokeUncheck()
        {
            CheckboxWrap.IsChecked = false;
        }



        //private void ComboBoxItem_SourceUpdated(object sender, DataTransferEventArgs e)
        //{
        //    var bx = (ComboBoxItem)sender;
        //    string name = bx.Content.ToString(); 
        //    //Weapon w = 
        //    //if (RepresentedEntity.Inventory.Weapons.)
        //}

        //private void WeaponBox_SourceUpdated(object sender, DataTransferEventArgs e)
        //{
        //    WeaponBox.SelectedItem = (from w in RepresentedEntity.Inventory.Weapons where w.Equipped == true select w).FirstOrDefault();
        //}

        //private void ComboBoxItem_SourceUpdated(object sender, DataTransferEventArgs e)
        //{
        //    var bx = (ComboBoxItem)sender;
        //    Binding myBinding = BindingOperations.GetBinding(bx, ComboBoxItem.DataContextProperty);
        //    //MessageBox.Show(myBinding.ElementName);
        //}
    }
}
