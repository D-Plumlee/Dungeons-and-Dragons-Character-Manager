using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDEntityLibrary.Inventory;

public class EquippedComparer<T> : IComparer<T> where T : Item
{
    public int Compare(T item1, T item2) 
    {
        if (item1 is Weapon weapon1 && item2 is Weapon weapon2)
        {
            if (weapon1.Equipped == true && weapon2.Equipped == false)
            {
                return 1;
            }
            else if (weapon1.Equipped == false && weapon2.Equipped == true)
            {
                return -1;
            }
            return 0;
        }

        else if (item1 is Shield shield1 && item2 is Shield shield2)
        {
            if (shield1.Equipped == true && shield2.Equipped == false) 
            {
                return 1;
            }
            else if (shield1.Equipped == false && shield2.Equipped == true)
            {
                return -1;
            }
            return 0;
        }

        else if (item1 is Armor armor1 && item2 is Armor armor2)
        {
            if (armor1.Equipped == true && armor2.Equipped == false)
            {
                return 1;
            }
            else if (armor1.Equipped == false && armor2.Equipped == true)
            {
                return -1;
            }
            return 0;
        }

        return 0;
    }

}
