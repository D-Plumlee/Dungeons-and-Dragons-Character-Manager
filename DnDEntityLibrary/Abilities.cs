using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDEntityLibrary;

public class Abilities
{
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Wisdom { get; set; }
    public int Charisma { get; set; }
    public Abilities() { }
    public Abilities(int s, int d, int c, int i, int w, int cha)
    {
        Strength = s;
        Dexterity = d;
        Constitution = c;
        Intelligence = i;
        Wisdom = w;
        Charisma = cha;
    }
    public int CalculateModifier(AbilitiesEnum abil)
    {
        int mod;

        switch (abil)
        {
            case AbilitiesEnum.Strength:
                mod = Strength;
                break;
            case AbilitiesEnum.Dexterity:
                mod = Dexterity;
                break;
            case AbilitiesEnum.Constitution:
                mod = Constitution;
                break;
            case AbilitiesEnum.Intelligence:
                mod = Intelligence;
                break;
            case AbilitiesEnum.Wisdom:
                mod = Wisdom;
                break;
            case AbilitiesEnum.Charisma:
                mod = Charisma;
                break;
            default:
                return 0;
        }

        mod = (mod >= 10) ? (mod - 10) / 2 
            : ((mod - 10) / 2) + ((mod - 10) % 2);

        return mod;
    }
}
