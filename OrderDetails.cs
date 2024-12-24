using System;

namespace OnlineCafe
{
    /// <summary>
    /// Order Details Class
    /// </summary>
    public class OrderDetails
    {
        /// <summary>
        /// <see cref="OrderDetails"/> static Order ID
        /// </summary>
        private static int s_orderID=1000;
        /// <summary>
        /// <see cref="OrderDetails"/> Order ID Auto Generated
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// <see cref="OrderDetails"/> User ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// <see cref="OrderDetails"/> Order Date
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// <see cref="OrderDetails"/> Total Price of Orderr
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// <see cref="OrderDetails"/> Order Status ENUM
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        public OrderDetails()
        {
            
        }
    }
}