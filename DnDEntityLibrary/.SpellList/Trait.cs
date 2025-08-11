using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDEntityLibrary.SpellList;

public class Trait
{
    public string Name { get; set; } = "";
    public string Effect { get; set; } = "";

    // Not all Traits/Feats modify stats, but I wanted to include it optionally for stat modification.
    // (To be implemented in the future)
    //public int? EffectModifer { get; set; } = null;
    //public string? AtributeOrSkillTarget { get; set; } = null;
}
