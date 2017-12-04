using Checkout.BasketManagement.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.BasketManagement.Core.Model;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace Checkout.BasketManagement.Core.ItemExecuter
{
    public class Load : IBasketOperation<LoadRequest, List<Item>>
    {
        public List<Item> Operation(LoadRequest request)
        {
            if (request!=null)
                return this.LoadItem(request.Name, request.Category);
            return this.LoadItem();
        }

        /// <summary>
        /// Method to load items from file when category is given
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        internal List<Item> LoadItem(string itemName, string category)
        {
            var rootPath = $"{AppDomain.CurrentDomain.BaseDirectory }\\Bin\\Basket";
            var file = $"{rootPath}\\{category}\\{itemName}.xml";
            return this.Read(file).Items;
        }

        /// <summary>
        /// Method to load items from all files in dir
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        internal List<Item> LoadItem()
        {
            var rootPath = $"{AppDomain.CurrentDomain.BaseDirectory }\\Bin\\Basket";
            //Since category is not given list all items
            //But ignore the items from currentbasket
            var files = Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories)
                .Where(name => !name.StartsWith("CurrentBasket"));
            var itemList = new List<Item>();
            foreach (var file in files)
                itemList.AddRange(this.Read(file).Items);
            return itemList;
        }

        /// <summary>
        /// Reads and serializes items from data source i.e. file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        internal ItemList Read(string file)
        {
            var xmlDocumentText = File.ReadAllText(file);
            XmlSerializer serializer = new XmlSerializer(typeof(ItemList));

            using (var reader = new StringReader(xmlDocumentText))
            {
                return (ItemList)(serializer.Deserialize(reader));
            }
        }
    }
}
