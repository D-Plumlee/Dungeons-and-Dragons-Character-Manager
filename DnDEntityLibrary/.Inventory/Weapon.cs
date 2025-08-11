using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDEntityLibrary.Inventory;

public class Weapon : Item, INotifyPropertyChanged
{
    [XmlIgnore]
    private bool _isEquipped = false;
    [XmlAttribute]
    public bool Equipped 
    { 
        get => _isEquipped; 
        set
        {
            if (value == _isEquipped) return;
            _isEquipped = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private WeaponTypeEnum _style = WeaponTypeEnum.OneHand;
    public WeaponTypeEnum Style
    {
        get => _style;
        set
        {
            if (value == _style) return;
            _style = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private string _damageType = string.Empty;
    public string DamageType
    {
        get => _damageType;
        set
        {
            if (value == _damageType) return;
            _damageType = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private int _hitModifier;
    public int HitModifier
    {
        get => _hitModifier; 
        set
        {
            if (value == _hitModifier) return;
            _hitModifier = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private string _hitDamage = string.Empty;
    public string HitDamage
    {
        get => _hitDamage;
        set
        {
            if (value == _hitDamage) return;
            _hitDamage = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private bool _isMagic = false;
    public bool IsMagic
    {
        get => _isMagic;
        set
        {
            if (value == _isMagic) return;
            _isMagic = value;
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

    public override string ToString()
    {
        return this.Name;
    }

    //public override int GetHashCode()
    //{
    //    // Should implement replacement hashcode
    //}
}
