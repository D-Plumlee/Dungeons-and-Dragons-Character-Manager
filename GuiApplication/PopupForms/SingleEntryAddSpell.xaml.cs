using DnDEntityLibrary.Inventory;
using DnDEntityLibrary.SpellList;
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

namespace GuiApplication.PopupForms
{
    /// <summary>
    /// Interaction logic for SingleEntryAddSpell.xaml
    /// </summary>
    public partial class SingleEntryAddSpell : Window
    {

        public Spell NewSpell
        {
            get => (Spell)GetValue(NewSpellProperty);
            set => SetValue(NewSpellProperty, value);
        }
        public static readonly DependencyProperty NewSpellProperty =
            DependencyProperty.Register("NewSpell",
                typeof(Spell),
                typeof(SingleEntryAddInventory),
                new PropertyMetadata(null));
        public Ability NewAbility
        {
            get => (Ability)GetValue(NewAbilityProperty);
            set => SetValue(NewAbilityProperty, value);
        }
        public static readonly DependencyProperty NewAbilityProperty =
            DependencyProperty.Register("NewAbility",
                typeof(Ability),
                typeof(SingleEntryAddInventory),
                new PropertyMetadata(null));
        public Trait NewTrait
        {
            get => (Trait)GetValue(NewTraitProperty);
            set => SetValue(NewTraitProperty, value);
        }
        public static readonly DependencyProperty NewTraitProperty =
            DependencyProperty.Register("NewTrait",
                typeof(Trait),
                typeof(SingleEntryAddInventory),
                new PropertyMetadata(null));



        public SingleEntryAddSpell()
        {
            InitializeComponent();
            NewSpell = new Spell();
            NewAbility = new Ability();
            NewTrait = new Trait();
        }


        public event EventHandler ConfirmSpellEvent;

        public void Confirm_Click(object sender, EventArgs e)
        {
            NewSpell.Name = NameField.Text;
            NewSpell.Effect = EffectField.Text;
            NewSpell.Area = AOEField.Text;
            NewSpell.Duration = DurationField.Text;
            NewSpell.Concentrate = (bool)ConcentrateCheck.IsChecked;
           

            NewAbility.Name = NameField.Text;
            NewAbility.Effect = EffectField.Text;
            NewAbility.Area = AOEField.Text;
            NewAbility.Duration = DurationField.Text;

            NewTrait.Name = NameField.Text;
            NewTrait.Effect = EffectField.Text;

            try
            {

                ConfirmSpellEvent?.Invoke(this, EventArgs.Empty);
            }
            catch
            {
                MessageBox.Show("Failed to add item");
            }
            finally
            {
                this.Close();
            }
        }

        public void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        private void SpellButton_Checked(object sender, RoutedEventArgs e)
        {
            MagicEffectFields.Visibility = Visibility.Visible;
            SpellLevelFields.Visibility = Visibility.Visible;
            SpellFields.Visibility = Visibility.Visible;
        }

        private void SpellButton_Unchecked(object sender, RoutedEventArgs e)
        {
            MagicEffectFields.Visibility = Visibility.Collapsed;
            SpellLevelFields.Visibility = Visibility.Collapsed;
            SpellFields.Visibility = Visibility.Collapsed;
        }

        private void AbilityButton_Checked(object sender, RoutedEventArgs e)
        {
            MagicEffectFields.Visibility = Visibility.Visible;
            AbilityFields.Visibility = Visibility.Visible;
        }

        private void AbilityButton_Unchecked(object sender, RoutedEventArgs e)
        {
            MagicEffectFields.Visibility = Visibility.Collapsed;
            AbilityFields.Visibility = Visibility.Collapsed;
        }
    }
}
