using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Checkout.BasketManagement.Core.Model
{
    [XmlRoot("Items")]
    public class ItemList
    {
        [XmlElement("Item")]
        
        public List<Item> Items = new List<Item>();
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
