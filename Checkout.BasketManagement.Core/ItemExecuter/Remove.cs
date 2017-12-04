using Checkout.BasketManagement.Core.Interface;
using Checkout.BasketManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Checkout.BasketManagement.Core.ItemExecuter
{
    public class Remove : IBasketOperation<int, bool>
    {
        public bool Operation(int id)
        {
            try
            {
                if (!File.Exists(Constants.CurrentBasket)) return true;
                var doc = XDocument.Load(Constants.CurrentBasket);
                var nodes = from node in doc.Descendants("Item")
                              where node.Element("Id").Value == id.ToString()
                              select node;
                nodes.Remove();
                doc.Save(Constants.CurrentBasket);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException();
            }
            return true;
        }
    }
}
