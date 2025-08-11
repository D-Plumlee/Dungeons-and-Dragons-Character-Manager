using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDEntityLibrary;

public class AbilityModifiers: INotifyPropertyChanged
{
    [XmlIgnore]
    private int _strength;
    public int Strength 
    {
        get => _strength;
        set
        {
            if (value == _strength) return;
            _strength = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private int _dexterity;
    public int Dexterity
    {
        get => _dexterity;
        set
        {
            if (value == _dexterity) return;
            _dexterity = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private int _constitution;
    public int Constitution
    {
        get => _constitution;
        set
        {
            if (value == _constitution) return;
            _constitution = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private int _intelligence;
    public int Intelligence
    {
        get => _intelligence;
        set
        {
            if (value == _intelligence) return;
            _intelligence = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private int _wisdom;
    public int Wisdom
    {
        get => _wisdom;
        set
        {
            if (value == _wisdom) return;
            _wisdom = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private int _charisma;
    public int Charisma
    {
        get => _charisma;
        set
        {
            if (value == _charisma) return;
            _charisma = value;
            OnPropertyChanged();
        }
    }

    [XmlIgnore]
    private bool _isChanged;
    [XmlIgnore]
    public bool IsChanged
    {
        get => _isChanged;
        set
        {
            if (value == _isChanged) return;
            _isChanged = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        if (propertyName != nameof(IsChanged))
        {
            IsChanged = true;
        }
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }




    public AbilityModifiers() { }
    public AbilityModifiers(int str, int dex, int con, int intel, int wis, int cha)
    {
        Strength = str;
        Dexterity = dex;
        Constitution = con;
        Intelligence = intel;
        Wisdom = wis;
        Charisma = cha;
        IsChanged = false;
    }
    public AbilityModifiers(Abilities fullScores)
    {
        Strength = fullScores.CalculateModifier(AbilitiesEnum.Strength);
        Dexterity = fullScores.CalculateModifier(AbilitiesEnum.Dexterity);
        Constitution = fullScores.CalculateModifier(AbilitiesEnum.Constitution);
        Intelligence = fullScores.CalculateModifier(AbilitiesEnum.Intelligence);
        Wisdom = fullScores.CalculateModifier(AbilitiesEnum.Wisdom);
        Charisma = fullScores.CalculateModifier(AbilitiesEnum.Charisma);
        IsChanged = false;
    }

}
