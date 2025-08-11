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
using DnDEntityLibrary.Inventory;

namespace GuiApplication.PopupForms
{
    /// <summary>
    /// Interaction logic for SingleEntryAddInventory.xaml
    /// </summary>
    public partial class SingleEntryAddInventory : Window
    { 
        public Weapon NewWeapon
        {
            get => (Weapon)GetValue(NewWeaponProperty);
            set => SetValue(NewWeaponProperty, value);
        }
        public static readonly DependencyProperty NewWeaponProperty =
            DependencyProperty.Register("NewWeapon",
                typeof(Weapon),
                typeof(SingleEntryAddInventory),
                new PropertyMetadata(null));
        public Shield NewShield
        {
            get => (Shield)GetValue(NewShieldProperty);
            set => SetValue(NewShieldProperty, value);
        }
        public static readonly DependencyProperty NewShieldProperty =
            DependencyProperty.Register("NewShield",
                typeof(Shield),
                typeof(SingleEntryAddInventory),
                new PropertyMetadata(null));
        public Armor NewArmor
        {
            get => (Armor)GetValue(NewArmorProperty);
            set => SetValue(NewArmorProperty, value);
        }
        public static readonly DependencyProperty NewArmorProperty =
            DependencyProperty.Register("NewArmor",
                typeof(Armor),
                typeof(SingleEntryAddInventory),
                new PropertyMetadata(null));
        public Item NewItem
        {
            get => (Item)GetValue(NewItemProperty);
            set => SetValue(NewItemProperty, value);
        }
        public static readonly DependencyProperty NewItemProperty =
            DependencyProperty.Register("NewItem",
                typeof(Item),
                typeof(SingleEntryAddInventory),
                new PropertyMetadata(null));

        public SingleEntryAddInventory()
        {
            InitializeComponent();
            NewWeapon = new Weapon();
            NewShield = new Shield();
            NewArmor = new Armor();
            NewItem = new Item();
        }

        public event EventHandler ConfirmItemEvent;
        //public event EventHandler CancelItemEvent;

        public void Confirm_Click(object sender, EventArgs e)
        {
            NewWeapon.Name = NameField.Text;
            NewWeapon.Effect = EffectField.Text;

            var sel = WeaponStyleBox.SelectedItem as ComboBoxItem;
            switch (sel.Content)
            {
                case "One Handed":
                    NewWeapon.Style = WeaponTypeEnum.OneHand;
                    break;
                case "Two Handed":
                    NewWeapon.Style = WeaponTypeEnum.TwoHand;   
                    break;
                case "Versatile":
                    NewWeapon.Style = WeaponTypeEnum.Versatile;
                    break;
                default:
                    NewWeapon.Style = WeaponTypeEnum.OneHand;
                    break;
            }

            NewShield.Name = NameField.Text;
            NewShield.Effect = EffectField.Text;

            NewArmor.Name = NameField.Text;
            NewArmor.Effect = EffectField.Text;
            
            var arsel = ArmorStyleBox.SelectedItem as ComboBoxItem;
            switch (arsel.Content)
            {
                case "Light Armor":
                    NewArmor.Style = ArmorTypeEnum.LightArmor;
                    break;
                case "Medium Armor":
                    NewArmor.Style = ArmorTypeEnum.MediumArmor;
                    break;
                case "Heavy Armor":
                    NewArmor.Style = ArmorTypeEnum.HeavyArmor;
                    break;
                default:
                    NewArmor.Style = ArmorTypeEnum.LightArmor;
                    break;
            }


            NewItem.Name = NameField.Text;
            NewItem.Effect = EffectField.Text;

            try { 

                ConfirmItemEvent?.Invoke(this, EventArgs.Empty);
            }
            catch
            {
                MessageBox.Show("Failed to add item");
            }
            finally { 
                this.Close();
            }
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //CancelItemEvent?.Invoke(this, EventArgs.Empty);
        }

        private void WeaponButton_Checked(object sender, RoutedEventArgs e)
        {
            WeaponFields.Visibility = Visibility.Visible;
        }

        private void WeaponButton_Unchecked(object sender, RoutedEventArgs e)
        {
            WeaponFields.Visibility= Visibility.Collapsed;
        }

        private void ShieldButton_Checked(object sender, RoutedEventArgs e)
        {
            ShieldFields.Visibility = Visibility.Visible;
        }

        private void ShieldButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ShieldFields.Visibility= Visibility.Collapsed;
        }

        private void ArmorButton_Checked(object sender, RoutedEventArgs e)
        {
            ArmorFields.Visibility = Visibility.Visible;
        }

        private void ArmorButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ArmorFields.Visibility= Visibility.Collapsed;
        }
    }
}
