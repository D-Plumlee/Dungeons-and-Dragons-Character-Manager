using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDEntityLibrary.SpellList;

public class SpellListAsTwoLists
{
    public ObservableCollection<Spell> Spells { get; set; } = new ObservableCollection<Spell>();
    public ObservableCollection<Ability> AbilityList { get; set; } = new ObservableCollection<Ability>();
}
