using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDEntityLibrary;

[XmlRoot("Game")]
public class DnDEntityCollection : INotifyPropertyChanged, INotifyCollectionChanged
{
    [XmlIgnore]
    private string _gameSystem = "DnD";
    [XmlAttribute("System")]
    public string GameSystem 
    {
        get => _gameSystem;
        set
        {
            if (value == _gameSystem) return;
            _gameSystem = value;
            OnPropertyChanged();
        }
    }

    [XmlIgnore]
    private string _fileContainedIn = string.Empty;
    [XmlIgnore]
    public string FileContainedIn
    {
        get => _fileContainedIn;
        set
        {
            if(value  == _fileContainedIn) return;
            _fileContainedIn = value;
            OnPropertyChanged();
        }
    }


    [XmlIgnore]
    private ObservableCollection<string> _campaigns = new ObservableCollection<string>();
    [XmlIgnore]
    public ObservableCollection<string> Campaigns
    {
        get => _campaigns;
        set
        {
            if( value == _campaigns) return;
            _campaigns = value;
            OnPropertyChanged();
        }
    }
    [XmlIgnore]
    private ObservableCollection<string> _groups = new ObservableCollection<string>();
    [XmlIgnore]
    public ObservableCollection<string> Groups
    {
        get => _groups;
        set
        {
            if( value == _groups) return;
            _groups = value;
            OnPropertyChanged();
        }
    }



    [XmlElement("Entry")]
    public ObservableCollection<DnDEntity> Entities { get; set; } = new ObservableCollection<DnDEntity>();

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

    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add
        {
            ((INotifyCollectionChanged)Entities).CollectionChanged += value;
        }

        remove
        {
            ((INotifyCollectionChanged)Entities).CollectionChanged -= value;
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        if (propertyName != nameof(IsChanged))
        {
            IsChanged = true;
        }
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}