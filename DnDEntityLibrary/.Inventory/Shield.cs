using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DnDEntityLibrary.Inventory
{
    public class Shield : Item
    {
        [XmlAttribute]
        public bool Equipped { get; set; } = false;
        public int ArmorClassBonus { get; set; } = 2;
    }
}
