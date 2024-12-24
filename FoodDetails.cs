

namespace OnlineCafe
{
    //Food Details Class
    public class FoodDetails
    {
        /// <summary>
        /// <see cref="FoodDetails"> Static Food ID
        /// </summary>
        private static int s_foodID=100;
        /// <summary>
        /// <see cref="FoodDetails"> Food ID Auto Generated
        /// </summary>
        public string FoodID { get; set; }
        /// <summary>
        /// <see cref="FoodDetails"> Food Name
        /// </summary>
        public string FoodName { get; set; }
        /// <summary>
        /// <see cref="FoodDetails"> Food Price
        /// </summary>
        public double FoodPrice { get; set; }
        /// <summary>
        /// <see cref="FoodDetails"> Found Available Count
        /// </summary>
        public int AvailableQuantity { get; set; }
    }
}