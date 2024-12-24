using System.Collections.Generic;

namespace OnlineCafe
{
    public static class Operation
    {
        static CustomList<UserDetails> users=new CustomList<UserDetails>();

        static CustomList<FoodDetails>foods=new CustomList<FoodDetails>();

        static List<CartItem>cartItems=new List<CartItem>();
        static CustomList<OrderDetails> orders=new CustomList<OrderDetails>();
    }
}