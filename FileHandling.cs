using System.IO;

namespace OnlineCafe
{
    public static class FileHandling
    {
        public static void CreateFS()
        {
            //Create Directory
            if(!Directory.Exists("OnlineCafe"))
            {
                Directory.CreateDirectory("OnlineCafe");
            }

            //Create Food Details CSV
            if(!File.Exists("OnlineCafe/FoodDetails.csv"))
            {
                File.Create("OnlineCafe/FoodDetails.csv").Close();
            }

            //Create User Details CSV
            if(!File.Exists("OnlineCafe/UserDetails.csv"))
            {
                File.Create("OnlineCafe/UserDetails.csv").Close();
            }

            //Create Cart Items CSV
            if(!File.Exists("OnlineCafe/CartItems.csv"))
            {
                File.Create("OnlineCafe/CartItems.csv").Close();
            }

            //Create Orders CSV
            if(!File.Exists("OnlineCafe/Orders.csv"))
            {
                File.Create("OnlineCafe/Orders.csv").Close();
            }
        }
    }
}