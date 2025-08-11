using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Serialization;
using DnDEntityLibrary.Inventory;
using DnDEntityLibrary.SpellList;

namespace DnDEntityLibrary;

[Serializable]
public class DnDEntity : ICloneable
{
    public ObservableCollection<string> Campaign { get; set; } = new ObservableCollection<string>();
    public ObservableCollection<string> Group { get; set; } = new ObservableCollection<string>();
    public DnDEntityCategoryEnum Classification { get; set; } = DnDEntityCategoryEnum.NPC;
    public string Name { get; set; } = "";
    public string Race { get; set; } = "Human";
    public string ClassName { get; set; } = "";

    // Use Level for CR on Monsters
    public string Level { get; set; } = "1";
    public int ProficiencyBonus { get; set; }
    public int InitiativeBonus { get; set; }
    public int ArmorClass { get; set; }
    public int CurrentHitPoints { get; set; }
    public int MaxHitPoints { get; set; }
    public string HitDice { get; set; } = "";
    [XmlIgnore]
    public ObservableCollection<bool> DeathSaves { get; set; } = new ObservableCollection<bool> { false, false, false, false, false, false };
    public int MoveSpeed { get; set; } = 30; // In feet
    public int PassivePerception { get; set; }
    public Abilities AbilityScores { get; set; } =
        new Abilities(10, 10, 10, 10, 10, 10);
    public AbilityModifiers AbilityScoreModifiers { get; set; } =
        new AbilityModifiers();
    public AbilityModifiers SavingThrows { get; set; } =
        new AbilityModifiers();
    public Skills Skills { get; set; }
    = new Skills();
    public ObservableCollection<SkillsEnum> SkillProficiencies { get; set; } = new ObservableCollection<SkillsEnum>();
    //public List<string> SkillProficienciesBackup {  get
    //    {
    //        List<string> output = new List<string>();
    //        foreach(var skil in SkillProficiencies)
    //        {
    //            output.Add(skil.ToString());
    //        }
    //        return output;
    //    } }
    //public List<Item> Inventory { get; set; } = new List<Item>();
    public InventoryAsFourLists Inventory { get; set; } = new InventoryAsFourLists();
    //public List<MagicEffect> SpellList { get; set; } = new List<MagicEffect>();
    public SpellListAsTwoLists SpellList { get; set; } = new SpellListAsTwoLists();
    public ObservableCollection<Trait> TraitsAndFeats { get; set; } = new ObservableCollection<Trait>();


    public object Clone()
    {
        DnDEntity entityClone = new DnDEntity()
        {
            Campaign = new ObservableCollection<string>(),
            Group = new ObservableCollection<string>(),
            Classification = this.Classification,
            Name = this.Name,
            Race = this.Race,
            ClassName = this.ClassName,
            Level = this.Level,
            ProficiencyBonus = this.ProficiencyBonus,
            InitiativeBonus = this.InitiativeBonus,
            ArmorClass = this.ArmorClass,
            CurrentHitPoints = this.CurrentHitPoints,
            MaxHitPoints = this.MaxHitPoints,
            HitDice = this.HitDice,
            MoveSpeed = this.MoveSpeed,
            PassivePerception = this.PassivePerception,
            AbilityScores =
                new Abilities(this.AbilityScores.Strength, this.AbilityScores.Dexterity, this.AbilityScores.Constitution,
                this.AbilityScores.Intelligence, this.AbilityScores.Wisdom, this.AbilityScores.Charisma),
            SavingThrows =
                new AbilityModifiers(this.SavingThrows.Strength, this.SavingThrows.Dexterity, this.SavingThrows.Constitution,
                this.SavingThrows.Intelligence, this.SavingThrows.Wisdom, this.SavingThrows.Charisma),
            Skills = 
                new Skills()
                {
                    Athletics = this.Skills.Athletics,
                    Acrobatics = this.Skills.Acrobatics,
                    SleightOfHand = this.Skills.SleightOfHand,
                    Stealth = this.Skills.Stealth,
                    Arcana = this.Skills.Arcana,
                    History = this.Skills.History,
                    Investigation = this.Skills.Investigation,
                    Nature = this.Skills.Nature,
                    Religion = this.Skills.Religion,
                    AnimalHandling = this.Skills.AnimalHandling,
                    Insight = this.Skills.Insight,
                    Medicine = this.Skills.Medicine,
                    Perception = this.Skills.Perception,
                    Survival = this.Skills.Survival,
                    Deception = this.Skills.Deception,
                    Intimidation = this.Skills.Intimidation,
                    Performance = this.Skills.Performance,
                    Persuasion = this.Skills.Persuasion,
                },
            SkillProficiencies = new ObservableCollection<SkillsEnum>(),
            Inventory = new InventoryAsFourLists(),
            SpellList = new SpellListAsTwoLists(),
            TraitsAndFeats = new ObservableCollection<Trait>()
        };
        foreach (string camp in this.Campaign)
        {
            entityClone.Campaign.Add(camp);
        }
        foreach (string grp in this.Group)
        {
            entityClone.Group.Add(grp);
        }
        foreach (SkillsEnum prof in this.SkillProficiencies)
        {
            entityClone.SkillProficiencies.Add(prof);
        }

        foreach(Weapon w in this.Inventory.Weapons)
        {
            entityClone.Inventory.Weapons.Add(new Weapon()
            {
                Name = w.Name,
                Effect = w.Effect,
                Equipped = w.Equipped,
                Style = w.Style,
                DamageType = w.DamageType,
                HitDamage = w.HitDamage,
                IsMagic = w.IsMagic
            });
        }
        foreach(Shield s in this.Inventory.Shields)
        {
            entityClone.Inventory.Shields.Add(new Shield()
            {
                Name = s.Name,
                Effect = s.Effect,
                Equipped = s.Equipped,
                ArmorClassBonus = s.ArmorClassBonus
            });
        }
        foreach(Armor a in this.Inventory.Armors)
        {
            entityClone.Inventory.Armors.Add(new Armor()
            {
                Name = a.Name,
                Effect = a.Effect,
                Equipped = a.Equipped,
                Style = a.Style,
                BaseArmorClass = a.BaseArmorClass
            });
        }
        foreach(Item i in this.Inventory.Items)
        {
            entityClone.Inventory.Items.Add(new Item()
            {
                Name = i.Name,
                Effect = i.Effect
            });
        }

        foreach(Spell s in this.SpellList.Spells)
        {
            entityClone.SpellList.Spells.Add(new Spell()
            {
                Name = s.Name,
                Area = s.Area,
                Duration = s.Duration, 
                Effect = s.Effect,
                Level = s.Level,
                CastTime = s.CastTime,
                Concentrate = s.Concentrate
            });
        }
        foreach(Ability a in this.SpellList.AbilityList)
        {
            entityClone.SpellList.AbilityList.Add(new Ability()
            {
                Name = a.Name,
                Area = a.Area,
                Duration = a.Duration, 
                Effect = a.Effect,
                Uses = a.Uses
            });
        }

        foreach(Trait tof in this.TraitsAndFeats)
        {
            entityClone.TraitsAndFeats.Add(new Trait()
            {
                Name = tof.Name,
                Effect = tof.Effect,
                //EffectModifer = tof.EffectModifer,
                //AtributeOrSkillTarget = tof.AtributeOrSkillTarget,
            });
        }

        return entityClone;
    } // End Clone()
}

