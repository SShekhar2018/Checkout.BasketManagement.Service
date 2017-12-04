using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.BasketManagement.Core.Model
{
    public class Constants
    {
        public const char SubGroupDelimator = ';';

        public static string CurrentBasket
        {
            get
            {
                return $"{AppDomain.CurrentDomain.BaseDirectory }\\Bin\\Basket\\CurrentBasket.xml";
            }
            set {
                var directory = $"{AppDomain.CurrentDomain.BaseDirectory }\\Bin\\Basket";
                if(!Directory.Exists(directory))Directory.CreateDirectory(directory);
            }
        }
    }
}
