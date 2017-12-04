using Checkout.BasketManagement.Core.Interface;
using Checkout.BasketManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Checkout.BasketManagement.Core.ItemExecuter
{
    public class Add : IBasketOperation<AddRequest, Item>
    {
        /// <summary>
        /// Loads the item from data source
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Item Operation(AddRequest request)
        {
            var load = new Load();
            var items = load.LoadItem(request.Name, request.Category);
            var item = items.First(it => it.Id == request.Id);
            this.AddItemToBasket(item);
            return item;
        }

        /// <summary>
        /// Method to write selected item into basket
        /// Basket is a file here
        /// </summary>
        /// <param name="item"></param>
        private void AddItemToBasket(Item item)
        {
            if (!File.Exists(Constants.CurrentBasket))
            {
                var xdoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Items",
                    new XElement("Item",
                    new XElement("Id", item.Id),
                    new XElement("Name", item.Name),
                    new XElement("Price", item.Price))));
                xdoc.Save(Constants.CurrentBasket);
            }
            else
            {
                XDocument xDocument = XDocument.Load(Constants.CurrentBasket);                
                xDocument.Root.Add(
                           new XElement("Item",
                           new XElement("Id", item.Id),
                           new XElement("Name", item.Name),
                           new XElement("Price", item.Price)));
                xDocument.Save(Constants.CurrentBasket);
            }
        }
    }
}
