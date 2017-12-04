using Checkout.BasketManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.BasketManagement.Core.Interface
{
    public interface IBasketOperation<TParam, TReturn> 
    {
        TReturn Operation(TParam param);
    }
}
