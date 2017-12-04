using Checkout.BasketManagement.Core;
using Checkout.BasketManagement.Core.ItemExecuter;
using Checkout.BasketManagement.Core.Model;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Checkout.BasketManagement.Service.Controllers
{
    /// <summary>
    /// API to manage basket of item for purchasing
    /// </summary>
    public class BasketManagementController : ApiController
    {
        /// <summary>
        /// Gets items by category. Where category is optional
        /// </summary>
        /// <param name="category">value Electronic/ Footwear</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Show/Items")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Succedeed", typeof(List<Item>))]
        public IHttpActionResult ListOfItems(string category = "")
        {
            var manage = new ManageBasket(new Load());
            return this.Ok(manage.FetchItemsInCategory(category));
        }

        /// <summary>
        /// Shows selected items in basket
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Basket/Items")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Succedeed", typeof(List<Item>))]
        public IHttpActionResult CurrentBasket()
        {
            return this.Ok(new ManageBasket().CurrentBasket());
        }

        /// <summary>
        /// Adds/ Removes item into basket by item
        /// by type of operation
        /// </summary>
        /// <param name="category">Value can be Electronic/ Footwear</param>
        /// <param name="name">value can be Mobile/ Shoes/ Laptop</param>
        /// <param name="id">value can be from 1-9</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Basket/Items/add")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Item added to basket", typeof(Item))]
        public IHttpActionResult AddItem(string category, string name, int id)
        {
            var manage = new ManageBasket(new Add());
            var item = manage.BasketOperation(new AddRequest { Category = category, Id = id, Name = name });
            return this.Ok(item);
        }

        /// <summary>
        /// Removes item from basket
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Basket/Items/remove{id}")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Removed")]
        public IHttpActionResult RemoveItem(int id)
        {
            try
            {
                var manage = new ManageBasket(new Remove());
                return this.Ok(manage.BasketOperation(id));
            }
            catch(Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        /// <summary>
        /// Cleans basket by removing all items
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("Basket/Items/Clean")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, "Cleaned")]
        public IHttpActionResult CleanItemsFromBasket()
        {
            try
            {
                var manage = new ManageBasket();
                manage.Clean();
                return this.Ok();
            }
            catch(Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }
    }
}