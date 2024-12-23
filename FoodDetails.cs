using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCafe
{
    public class FoodDetails
    {
        private static int s_foodID=100;
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public int AvailableQuantity { get; set; }
    }
}