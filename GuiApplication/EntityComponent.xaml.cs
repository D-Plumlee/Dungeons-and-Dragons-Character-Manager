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
            
            InitializeComponent();
            ds1.GroupName = "x" + this.GetHashCode() + "1";
            ds2.GroupName = "x" + this.GetHashCode() + "2";
            ds3.GroupName = "x" + this.GetHashCode() + "3";
            ds4.GroupName = "x" + this.GetHashCode() + "4";
            ds5.GroupName = "x" + this.GetHashCode() + "5";
            ds6.GroupName = "x" + this.GetHashCode() + "6";  
        }



        public void ResetComboBoxes()
        {
            
            
            if(SpellsBox.Items.Count > 0)
            {
                SpellsBox.SelectedIndex = 0;
            }
           
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

    }
}
