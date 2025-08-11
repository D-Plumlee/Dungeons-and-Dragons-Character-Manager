using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDEntityLibrary;

public class Skills
{
    // Strength
    public int Athletics { get; set; }
    // Dexterity
    public int Acrobatics { get; set; }
    public int SleightOfHand { get; set; }
    public int Stealth { get; set; }
    // Intelligence
    public int Arcana { get; set; }
    public int History { get; set; }
    public int Investigation { get; set; }
    public int Nature { get; set; }
    public int Religion { get; set; }
    // Wisdom
    public int AnimalHandling { get; set; }
    public int Insight { get; set; }
    public int Medicine { get; set; }
    public int Perception { get; set; }
    public int Survival { get; set; }
    // Charisma
    public int Deception { get; set; }
    public int Intimidation { get; set; }
    public int Performance { get; set; }
    public int Persuasion { get; set; }

    public Skills() { }
    public Skills(Abilities a, List<SkillsEnum> proficienciesList, int profBonus)
    {
        Athletics = a.CalculateModifier(AbilitiesEnum.Strength);
        Athletics += proficienciesList.Contains(SkillsEnum.Athletics) ?
            profBonus : 0;

        Acrobatics = a.CalculateModifier(AbilitiesEnum.Dexterity);
        Acrobatics += proficienciesList.Contains(SkillsEnum.Acrobatics) ?
            profBonus : 0;

        SleightOfHand = a.CalculateModifier(AbilitiesEnum.Dexterity);
        SleightOfHand += proficienciesList.Contains(SkillsEnum.SleightOfHand) ?
            profBonus : 0;

        Stealth = a.CalculateModifier(AbilitiesEnum.Dexterity);
        Stealth += proficienciesList.Contains(SkillsEnum.Stealth) ?
            profBonus : 0;

        Arcana = a.CalculateModifier(AbilitiesEnum.Intelligence);
        Arcana += proficienciesList.Contains(SkillsEnum.Arcana) ?
            profBonus : 0;

        History = a.CalculateModifier(AbilitiesEnum.Intelligence);
        History += proficienciesList.Contains(SkillsEnum.History) ?
            profBonus : 0;

        Investigation = a.CalculateModifier(AbilitiesEnum.Intelligence);
        Investigation += proficienciesList.Contains(SkillsEnum.Investigation) ?
            profBonus : 0;

        Nature = a.CalculateModifier(AbilitiesEnum.Intelligence);
        Nature += proficienciesList.Contains(SkillsEnum.Nature) ?
            profBonus : 0;

        Religion = a.CalculateModifier(AbilitiesEnum.Intelligence);
        Religion += proficienciesList.Contains(SkillsEnum.Religion) ?
            profBonus : 0;

        AnimalHandling = a.CalculateModifier(AbilitiesEnum.Wisdom);
        AnimalHandling += proficienciesList.Contains(SkillsEnum.AnimalHandling) ?
            profBonus : 0;

        Insight = a.CalculateModifier(AbilitiesEnum.Wisdom);
        Insight += proficienciesList.Contains(SkillsEnum.Insight) ?
            profBonus : 0;

        Medicine = a.CalculateModifier(AbilitiesEnum.Wisdom);
        Medicine += proficienciesList.Contains(SkillsEnum.Medicine) ?
            profBonus : 0;

        Perception = a.CalculateModifier(AbilitiesEnum.Wisdom);
        Perception += proficienciesList.Contains(SkillsEnum.Perception) ?
            profBonus : 0;

        Survival = a.CalculateModifier(AbilitiesEnum.Wisdom);
        Survival += proficienciesList.Contains(SkillsEnum.Survival) ?
            profBonus : 0;

        Deception = a.CalculateModifier(AbilitiesEnum.Charisma);
        Deception += proficienciesList.Contains(SkillsEnum.Deception) ?
            profBonus : 0;

        Intimidation = a.CalculateModifier(AbilitiesEnum.Charisma);
        Intimidation += proficienciesList.Contains(SkillsEnum.Intimidation) ?
            profBonus : 0;

        Performance = a.CalculateModifier(AbilitiesEnum.Charisma);
        Performance += proficienciesList.Contains(SkillsEnum.Performance) ?
            profBonus : 0;

        Persuasion = a.CalculateModifier(AbilitiesEnum.Charisma);
        Persuasion += proficienciesList.Contains(SkillsEnum.Persuasion) ?
            profBonus : 0;
    }
}
