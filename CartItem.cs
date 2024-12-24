using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCafe
{
    /// <summary>
    /// Cart Item Class
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// <see cref="CartItem"> Static Unique Cart Item 
        /// </summary>
        private static int s_itemID=100;
        /// <summary>
        /// <see cref="CartItem"/> Cart ID Auto Generated
        /// </summary>
        /// <value></value>
        public string ItemID { get; set; }
        /// <summary>
        /// <see cref="CartItem"> Order ID
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// <see cref="CartItem"> Food ID Unique
        /// </summary>
        public string FoodID { get; set; }
        /// <summary>
        /// <see cref="CartItem"> Total Order Orice
        /// </summary>
        public double OrderPrice { get; set; }
        /// <summary>
        /// <see cref="CartItem"> Total Order Quantity
        /// </summary>
        public int OrderQuantity { get; set; }
        public CartItem()
        {

        }
        public CartItem(string orderID,string foodID,double orderPrice,int orderQuantity)
        {
            ItemID=$"ITID{++s_itemID}";
            OrderID=orderID;
            FoodID=foodID;
            OrderPrice=orderPrice;
            OrderQuantity=orderQuantity;
        }
    }
}