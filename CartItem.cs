using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCafe
{
    public class CartItem
    {
        private static int s_itemID=100;
        public string OrderID { get; set; }
        public string FoodID { get; set; }
        public double OrderPrice { get; set; }
        public int OrderQuantity { get; set; }
    }
}