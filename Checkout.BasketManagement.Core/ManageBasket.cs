using Checkout.BasketManagement.Core.Basket;
using Checkout.BasketManagement.Core.Interface;
using Checkout.BasketManagement.Core.ItemExecuter;
using Checkout.BasketManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Checkout.BasketManagement.Core
{
    /// <summary>
    /// Manages Baskets. This class adds/ removes/
    /// cleans items into basket. 
    /// It also lists all items by category
    /// </summary>
    public class ManageBasket
    {
        private IBasketOperation<LoadRequest, List<Item>> loadOperation { get; set; }

        private IBasketOperation<AddRequest, Item> addOperation { get; set; }

        private IBasketOperation<int, bool> removeOperation { get; set; }

        /// <summary>
        /// constructor to load items
        /// </summary>
        /// <param name="load"></param>
        public ManageBasket(Load load)
        {
            this.loadOperation = load;
        }

        /// <summary>
        /// contructor to add items in basket
        /// </summary>
        /// <param name="add"></param>
        public ManageBasket(Add add)
        {
            this.addOperation = add;
        }

        /// <summary>
        /// constructor to remove items from basket
        /// </summary>
        /// <param name="remove"></param>
        public ManageBasket(Remove remove)
        {
            this.removeOperation = remove;
        }

        public ManageBasket() { }

        /// <summary>
        /// Fetches all itmes from data source by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<Item> FetchItemsByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return this.loadOperation.Operation(null);
            var itemGroups = this.GetItemName(category);
            var listItems = new List<Item>();
            foreach (var item in itemGroups)
            {
                var itemList = this.loadOperation.Operation(new LoadRequest { Category = category, Name = item });
                listItems.AddRange(itemList);
            }

            return listItems;
        }
        /// <summary>
        /// Lists all items in basket
        /// </summary>
        /// <returns></returns>
        public List<Item> CurrentBasket()
        {
            if (!File.Exists(Constants.CurrentBasket)) return new List<Item>();
            var xDoc = XDocument.Load(Constants.CurrentBasket);
            var load = new Load();
            return load.LoadItem(ItemConstant.CurrentBasket, "");
        }

        /// <summary>
        /// Adds item into basket 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Item BasketOperation(AddRequest request)
        {
            return this.addOperation.Operation(request);
        }

        /// <summary>
        /// Removes item from basket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool BasketOperation(int id)
        {
            return this.removeOperation.Operation(id);
        }

        /// <summary>
        /// Cleans basket
        /// </summary>
        public void Clean()
        {
            File.Delete(Constants.CurrentBasket);
        }

        /// <summary>
        /// Gets all sub groups in category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        private List<string> GetItemName(string category)
        {
            switch (category)
            {
                case Category.Electronic:
                    {
                        return this.GetSubGroup(Items.Electronic);
                    }
                case Category.Footwear:
                    {
                        return this.GetSubGroup(Items.Footwear);
                    }
                default:
                    {
                        return this.GetSubGroup();
                    }
            }
        }


        private List<string> GetSubGroup(string categoryValue)
        {
            return categoryValue.Split(Constants.SubGroupDelimator).ToList();
        }

        private List<string> GetSubGroup()
        {
            var listItem = new List<string>();
            listItem.AddRange(Items.Electronic.Split(Constants.SubGroupDelimator));
            listItem.AddRange(Items.Electronic.Split(Constants.SubGroupDelimator));
            return listItem;
        }
    }
}
