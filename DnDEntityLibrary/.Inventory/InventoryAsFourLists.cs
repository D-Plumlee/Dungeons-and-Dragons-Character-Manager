using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDEntityLibrary.Inventory;

public class InventoryAsFourLists
{

    public ObservableCollection<Weapon> Weapons { get; set; } = new ObservableCollection<Weapon>();
    public ObservableCollection<Shield> Shields { get; set; } = new ObservableCollection<Shield>();
    public ObservableCollection<Armor> Armors { get; set; } = new ObservableCollection<Armor>();
    public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
   
}
