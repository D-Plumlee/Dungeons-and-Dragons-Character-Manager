using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDEntityLibrary.Inventory
{
    public class Armor : Item
    {
        [XmlAttribute]
        public bool Equipped { get; set; } = false;
        public ArmorTypeEnum Style { get; set; } = ArmorTypeEnum.LightArmor;
        public int BaseArmorClass { get; set; } = 10;
    }
}
