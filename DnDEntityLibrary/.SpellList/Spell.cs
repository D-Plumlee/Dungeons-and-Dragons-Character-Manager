using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDEntityLibrary.SpellList;

public class Spell : MagicEffect
{
    public int Level { get; set; }
    public string CastTime { get; set; } = "Action";
    public bool Concentrate { get; set; } = false;

    // Components would include "Verbal", "Somatic", and any list of material components.
    // Nullable to make it optional.
    //public string? Components { get; set; } = null;
}
