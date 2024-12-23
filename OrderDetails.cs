using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCafe
{
    public class OrderDetails
    {
        private static int s_orderID=1000;
        public string OrderID { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}