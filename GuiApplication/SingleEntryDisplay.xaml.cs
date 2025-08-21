using DnDEntityLibrary;
using DnDEntityLibrary.Inventory;
using DnDEntityLibrary.SpellList;
using GuiApplication.cmds;
using GuiApplication.PopupForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.IO;
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
using System.Xml.Linq;
using DND = DnDEntityLibrary;
using DNDINV = DnDEntityLibrary.Inventory;

namespace GuiApplication;

/// <summary>
/// Interaction logic for SingleEntryDisplay.xaml
/// </summary>
public partial class SingleEntryDisplay : UserControl
{
    public string FileContainedIn
    {
        get => (string)GetValue(FileContainedInProperty);
        set => SetValue(FileContainedInProperty, value);    
    }
    public static readonly DependencyProperty FileContainedInProperty =
        DependencyProperty.Register("FileContainedIn",
            typeof(string), 
            typeof(SingleEntryDisplay), 
            new PropertyMetadata(null));
    public DND::DnDEntity RepresentedEntity
    {
        get => (DND::DnDEntity)GetValue(RepresentedEntityProperty);
        set => SetValue(RepresentedEntityProperty, value);
    }

    public static readonly DependencyProperty RepresentedEntityProperty =
        DependencyProperty.Register("RepresentedEntity",
            typeof(DND::DnDEntity),
            typeof(SingleEntryDisplay),
            new PropertyMetadata(null));


    public ObservableCollection<DND::DnDEntityCollection> AllEntriesByFile { get; set; }


    public SingleEntryDisplay()
    {
        InitializeComponent();
        ds1.GroupName = "x" + this.GetHashCode() + "1";
        ds2.GroupName = "x" + this.GetHashCode() + "2";
        ds3.GroupName = "x" + this.GetHashCode() + "3";
        ds4.GroupName = "x" + this.GetHashCode() + "4";
        ds5.GroupName = "x" + this.GetHashCode() + "5";
        ds6.GroupName = "x" + this.GetHashCode() + "6";
        //EntityEditingCopy = new DND::DnDEntity();


        
        NewPopup.ClosePopupEvent += (sender, e) =>
        {
            var s = sender as PopupForms.NewFilePopup;
            s.IsOpen = false;
            s.NewFileName = string.Empty;

        };
    }


    public event EventHandler BubblePopupConfirm;

    public void LoadEntryData()
    {
        UpdateFileLocationDisplay();
        NewPopup.CreateFileEvent += (sender, e) => 
        {
            ((NewFilePopup)sender).IsOpen = false;
            BubblePopupConfirm?.Invoke(sender, e);
            ((NewFilePopup)sender).NewFileName = string.Empty;
        }; 
    }

    private void NewFileMenuButton_Clicked(object sender, RoutedEventArgs e)
    {
        NewPopup.IsOpen = true;
    }




    private RelayCommand<Weapon> _deleteWeaponCommand = null;
    public RelayCommand<Weapon> DeleteWeaponCommand
    {
        get => _deleteWeaponCommand ??= new RelayCommand<Weapon>(DeleteWeapon, CanDeleteWeapon);
    }
    public bool CanDeleteWeapon(Weapon wep) => wep != null;
    public void DeleteWeapon(Weapon wep)
    {
        RepresentedEntity.Inventory.Weapons.Remove(wep);
    }

    private RelayCommand<Shield> _deleteShieldCommand = null;
    public RelayCommand<Shield> DeleteShieldCommand
    {
        get => _deleteShieldCommand ??= new RelayCommand<Shield>(DeleteShield, CanDeleteShield);
    }
    public bool CanDeleteShield(Shield sh) => sh != null;
    public void DeleteShield(Shield sh)
    {
        RepresentedEntity.Inventory.Shields.Remove(sh);
    }


    private RelayCommand<Armor> _deleteArmorCommand = null;
    public RelayCommand<Armor> DeleteArmorCommand
    {
        get => _deleteArmorCommand ??= new RelayCommand<Armor>(DeleteArmor, CanDeleteArmor);
    }
    public bool CanDeleteArmor(Armor ar) => ar != null;
    public void DeleteArmor(Armor ar)
    {
        RepresentedEntity.Inventory.Armors.Remove(ar);
    }


    private RelayCommand<Item> _deleteItemCommand = null;
    public RelayCommand<Item> DeleteItemCommand
    {
        get => _deleteItemCommand ??= new RelayCommand<Item>(DeleteItem, CanDeleteItem);
    }
    public bool CanDeleteItem(Item itm) => itm != null;
    public void DeleteItem(Item itm)
    {
        RepresentedEntity.Inventory.Items.Remove(itm);
    }


    private RelayCommand<Spell> _deleteSpellCommand = null;
    public RelayCommand<Spell> DeleteSpellCommand
    {
        get => _deleteSpellCommand ??= new RelayCommand<Spell>(DeleteSpell, CanDeleteSpell);
    }
    public bool CanDeleteSpell(Spell spl) => spl != null;
    public void DeleteSpell(Spell spl)
    {
        RepresentedEntity.SpellList.Spells.Remove(spl);
    }

    private RelayCommand<Ability> _deleteAbilityCommand = null;
    public RelayCommand<Ability> DeleteAbilityCommand
    {
        get => _deleteAbilityCommand ??= new RelayCommand<Ability>(DeleteAbility, CanDeleteAbility);
    }
    public bool CanDeleteAbility(Ability abl) => abl != null;
    public void DeleteAbility(Ability abl)
    {
        RepresentedEntity.SpellList.AbilityList.Remove(abl);
    }


    private RelayCommand<Trait> _deleteTraitCommand = null;
    public RelayCommand<Trait> DeleteTraitCommand
    {
        get => _deleteTraitCommand ??= new RelayCommand<Trait>(DeleteTrait, CanDeleteTrait);
    }
    public bool CanDeleteTrait(Trait trt) => trt != null;
    public void DeleteTrait(Trait trt)
    {
        RepresentedEntity.TraitsAndFeats.Remove(trt);
    }

    

    public void UpdateFileLocationDisplay()
    {
        StatusBarLabel.Text = "Current Location: ";
        StatusBarLabel.Text += FileContainedIn.Substring(FileContainedIn.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
    }




    private void AddItem_ButtonClick(object sender, RoutedEventArgs e)
    {
        SingleEntryAddInventory addItem = new SingleEntryAddInventory();
        addItem.ConfirmItemEvent += AddItemFromPopup;
        addItem.Show();
    }

    public void AddItemFromPopup(object sender, EventArgs e)
    {
        var s = (SingleEntryAddInventory)sender;
        if ((bool)s.WeaponButton.IsChecked)
        {
            RepresentedEntity.Inventory.Weapons.Add(s.NewWeapon);
            
        }
        else if ((bool)s.ShieldButton.IsChecked)
        {
            RepresentedEntity.Inventory.Shields.Add(s.NewShield);
           
        }
        else if ((bool)s.ArmorButton.IsChecked)
        {
            RepresentedEntity.Inventory.Armors.Add(s.NewArmor);
         
        }
        else if ((bool)s.ItemButton.IsChecked)
        {
            RepresentedEntity.Inventory.Items.Add(s.NewItem);
        }
        else
        {
            throw new InvalidOperationException("No button returned checked");
        }
    }

    private void AddSpell_ButtonClick(object sender, RoutedEventArgs e)
    {
        SingleEntryAddSpell addSpell = new SingleEntryAddSpell();
        addSpell.ConfirmSpellEvent += AddSpellFromPopup;
        addSpell.Show();
    }

    public void AddSpellFromPopup(object sender, EventArgs e)
    {
        var s = (SingleEntryAddSpell)sender;
        if ((bool)s.SpellButton.IsChecked)
        {
            RepresentedEntity.SpellList.Spells.Add(s.NewSpell);

        }
        else if ((bool)s.AbilityButton.IsChecked)
        {
            RepresentedEntity.SpellList.AbilityList.Add(s.NewAbility);

        }
        else if ((bool)s.TraitButton.IsChecked)
        {
            RepresentedEntity.TraitsAndFeats.Add(s.NewTrait);

        }
        else if ((bool)s.FeatButton.IsChecked)
        {
            RepresentedEntity.TraitsAndFeats.Add(s.NewTrait);
        }
        else
        {
            throw new InvalidOperationException("No button returned checked");
        }
    }

    private void SaveChanges_Click(object sender, RoutedEventArgs e)
    {
        

        
        EditEntry.IsEnabled = true;
        SaveChanges.IsEnabled = false;

        ClassificationDisplay.Visibility = Visibility.Visible;
        ClassificationEdit.Visibility = Visibility.Collapsed;

        RaceDisplay.Visibility = Visibility.Visible;
        RaceEdit.Visibility = Visibility.Collapsed;

        ClassDisplay.Visibility = Visibility.Visible;
        ClassEdit.Visibility = Visibility.Collapsed;

        LevelCRDisplay.Visibility = Visibility.Visible;
        LevelCREdit.Visibility = Visibility.Collapsed;

        ArmorClassDisplay.Visibility = Visibility.Visible;
        ArmorClassEdit.Visibility = Visibility.Collapsed;

        HPDisplay.Visibility = Visibility.Visible;
        HPEdit.Visibility = Visibility.Collapsed;

        HitDiceDisplay.Visibility = Visibility.Visible;
        HitDiceEdit.Visibility = Visibility.Collapsed;

        InitiativeBonusDisplay.Visibility = Visibility.Visible;
        InitiativeBonusEdit.Visibility = Visibility.Collapsed;

        MoveSpeedDisplay.Visibility = Visibility.Visible;
        MoveSpeedEdit.Visibility = Visibility.Collapsed;

        ProficiencyBonusDisplay.Visibility = Visibility.Visible;
        ProficiencyBonusEdit.Visibility = Visibility.Collapsed;

        PassivePerceptionDisplay.Visibility = Visibility.Visible;
        PassivePerceptionEdit.Visibility = Visibility.Collapsed;

        DeathSavesPanel.Visibility = Visibility.Visible;

        StrScoreDisplay.Visibility = Visibility.Visible;
        StrScoreEdit.Visibility = Visibility.Collapsed;
        DexScoreDisplay.Visibility = Visibility.Visible;
        DexScoreEdit.Visibility = Visibility.Collapsed;
        ConScoreDisplay.Visibility = Visibility.Visible;
        ConScoreEdit.Visibility = Visibility.Collapsed;
        IntScoreDisplay.Visibility = Visibility.Visible;
        IntScoreEdit.Visibility = Visibility.Collapsed;
        WisScoreDisplay.Visibility = Visibility.Visible;
        WisScoreEdit.Visibility = Visibility.Collapsed;
        ChaScoreDisplay.Visibility = Visibility.Visible;
        ChaScoreEdit.Visibility = Visibility.Collapsed;

        SavingThrowDisplay.Visibility = Visibility.Visible;
        SavingThrowEdit.Visibility = Visibility.Collapsed;

        SkillsDisplay.Visibility = Visibility.Visible;
        SkillsEdit.Visibility = Visibility.Collapsed;


    }

  

    private void EditMode_ButtonClicked(object sender, RoutedEventArgs e)
    {

        EditEntry.IsEnabled = false;
        SaveChanges.IsEnabled = true;
        
        switch (RepresentedEntity.Classification)
        {
            case DND::DnDEntityCategoryEnum.NPC:
                NPCButton.IsChecked = true;
                break;
            case DND::DnDEntityCategoryEnum.PlayerCharacter:
                PlayerButton.IsChecked = true; 
                break;
            case DND::DnDEntityCategoryEnum.Monster:
                MonsterButton.IsChecked = true;
                break;
        }

        ClassificationDisplay.Visibility = Visibility.Collapsed;
        ClassificationEdit.Visibility = Visibility.Visible;

        RaceDisplay.Visibility = Visibility.Collapsed;
        RaceEdit.Visibility = Visibility.Visible;

        ClassDisplay.Visibility = Visibility.Collapsed;
        ClassEdit.Visibility = Visibility.Visible;

        LevelCRDisplay.Visibility = Visibility.Collapsed;
        LevelCREdit.Visibility = Visibility.Visible;

        ArmorClassDisplay.Visibility = Visibility.Collapsed;
        ArmorClassEdit.Visibility = Visibility.Visible;

        HPDisplay.Visibility = Visibility.Collapsed;
        HPEdit.Visibility = Visibility.Visible;

        HitDiceDisplay.Visibility = Visibility.Collapsed;
        HitDiceEdit.Visibility = Visibility.Visible;

        InitiativeBonusDisplay.Visibility = Visibility.Collapsed;
        InitiativeBonusEdit.Visibility = Visibility.Visible;

        MoveSpeedDisplay.Visibility = Visibility.Collapsed;
        MoveSpeedEdit.Visibility = Visibility.Visible;

        ProficiencyBonusDisplay.Visibility = Visibility.Collapsed;
        ProficiencyBonusEdit.Visibility = Visibility.Visible;

        PassivePerceptionDisplay.Visibility = Visibility.Collapsed;
        PassivePerceptionEdit.Visibility = Visibility.Visible;

        DeathSavesPanel.Visibility = Visibility.Collapsed;

        StrScoreDisplay.Visibility = Visibility.Collapsed;
        StrScoreEdit.Visibility = Visibility.Visible;
        DexScoreDisplay.Visibility = Visibility.Collapsed;
        DexScoreEdit.Visibility = Visibility.Visible;
        ConScoreDisplay.Visibility = Visibility.Collapsed;
        ConScoreEdit.Visibility = Visibility.Visible;
        IntScoreDisplay.Visibility = Visibility.Collapsed;
        IntScoreEdit.Visibility = Visibility.Visible;
        WisScoreDisplay.Visibility = Visibility.Collapsed;
        WisScoreEdit.Visibility = Visibility.Visible;
        ChaScoreDisplay.Visibility = Visibility.Collapsed;
        ChaScoreEdit.Visibility = Visibility.Visible;

        SavingThrowDisplay.Visibility = Visibility.Collapsed;
        SavingThrowEdit.Visibility = Visibility.Visible;

        SkillsDisplay.Visibility = Visibility.Collapsed;
        SkillsEdit.Visibility = Visibility.Visible;
    }

    private void ClassificationSet_Handler(object sender, RoutedEventArgs e)
    {
        var btn = sender as RadioButton; 
        if(btn == NPCButton)
        {
            RepresentedEntity.Classification = DND::DnDEntityCategoryEnum.NPC;
        }
        else if(btn == PlayerButton)
        {
            RepresentedEntity.Classification = DND::DnDEntityCategoryEnum.PlayerCharacter;
        }
        else if (btn == MonsterButton)
        {
            RepresentedEntity.Classification = DND::DnDEntityCategoryEnum.Monster; 
        }
        ClassificationDisplay.UpdateLayout();
    }

    private void RecalculateMods(object sender, TextChangedEventArgs e)
    {
        RepresentedEntity.AbilityScoreModifiers.Strength = RepresentedEntity.AbilityScores.CalculateModifier(DND.AbilitiesEnum.Strength);
        RepresentedEntity.AbilityScoreModifiers.Dexterity = RepresentedEntity.AbilityScores.CalculateModifier(DND.AbilitiesEnum.Dexterity);
        RepresentedEntity.AbilityScoreModifiers.Constitution = RepresentedEntity.AbilityScores.CalculateModifier(DND.AbilitiesEnum.Constitution);
        RepresentedEntity.AbilityScoreModifiers.Intelligence = RepresentedEntity.AbilityScores.CalculateModifier(DND.AbilitiesEnum.Intelligence);
        RepresentedEntity.AbilityScoreModifiers.Wisdom = RepresentedEntity.AbilityScores.CalculateModifier(DND.AbilitiesEnum.Wisdom);
        RepresentedEntity.AbilityScoreModifiers.Charisma = RepresentedEntity.AbilityScores.CalculateModifier(DND.AbilitiesEnum.Charisma);   

    }




    public event RoutedEventHandler DeleteTabEvent;
    private void DeleteEntry_Click(object sender, RoutedEventArgs e)
    {
        var confirmation = MessageBox.Show("Delete selected entry? Entries will be lost forever once changes are saved.", "Confirm Delete?", MessageBoxButton.YesNo);
        if (confirmation == MessageBoxResult.No)
        {
            return;
        }
         
            foreach (var coll in AllEntriesByFile)
            {
                if (coll.Entities.Contains(RepresentedEntity))
                {
                    coll.Entities.Remove(RepresentedEntity);
                }
            }
        DeleteTabEvent?.Invoke(this,e);
    }

    private void EditCampaignGroup_Click(object sender, RoutedEventArgs e)
    {
        CampAndGroupPopup editGroupingWindow = new CampAndGroupPopup()
        {
            EditingEntry = RepresentedEntity,
            AllEntries = AllEntriesByFile, 
            File = FileContainedIn
        };
        editGroupingWindow.ShowDialog();
    }
}
