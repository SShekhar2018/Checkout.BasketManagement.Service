using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.BasketManagement.Core.Model
{
    public class AddRequest : LoadRequest
    {
        public int Id { get; set; }
    }

    public class LoadRequest
    {
        public string Category { get; set; }
        public string Name { get; set; }
    }
}
