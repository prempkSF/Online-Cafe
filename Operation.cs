using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;

namespace OnlineCafe
{
    
    public static class Operation
    {
        static CustomList<UserDetails> users = new CustomList<UserDetails>();
        static CustomList<FoodDetails> foods = new CustomList<FoodDetails>();
        static CustomList<CartItem> cartItems = new CustomList<CartItem>();
        static CustomList<OrderDetails> orders = new CustomList<OrderDetails>();

        static UserDetails currentLoggedCustomer;

        public static void LoadFiles()
        {
            //Read Food Details
            ReadWriteFS<FoodDetails>readWriteFSFood=new ReadWriteFS<FoodDetails>();
            readWriteFSFood.ReadFiles(fileName:"FoodDetails.csv",values:out foods);

            //Read User Details
            ReadWriteFS<UserDetails>readWriteFSUser=new ReadWriteFS<UserDetails>();
            readWriteFSUser.ReadFiles(fileName:"UserDetails.csv",values:out users);

            //Read Cart Items
            ReadWriteFS<CartItem>readWriteFSCart=new ReadWriteFS<CartItem>();
            readWriteFSCart.ReadFiles(fileName:"CartItems.csv",values:out cartItems);

            //Read Orders
            ReadWriteFS<OrderDetails>readWriteFSOrder=new ReadWriteFS<OrderDetails>();
            readWriteFSOrder.ReadFiles(fileName:"Orders.csv",values:out orders);
        }
        public static void MainMenu()
        {
            try
            {
                bool flag = true;
                do
                {
                    System.Console.WriteLine("\n******** Main Menu ********\n");
                    System.Console.WriteLine("\n1.User Registration\n2.User Login\n3.Exit\n");
                    int choice;
                    while (!(int.TryParse(Console.ReadLine(), out choice)))
                    {
                        System.Console.WriteLine("Enter Valid option : ");
                    }
                    switch (choice)
                    {
                        case 1:
                            {
                                Registration();
                                break;
                            }
                        case 2:
                            {
                                Login();
                                break;
                            }
                        case 3:
                            {
                                flag = false;
                                break;
                            }
                        default:
                            {
                                System.Console.WriteLine("\nEnter Correct Option : \n");
                                break;
                            }
                    }
                } while (flag);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try Again");
            }
        }

        public static void Registration()
        {
            try
            {
                System.Console.WriteLine("User Name : ");
                string userName = Console.ReadLine();

                System.Console.WriteLine("Father Name : ");
                string fatherName = Console.ReadLine();

                System.Console.WriteLine("Mobile Number : ");
                string mobileNumber = Console.ReadLine();

                System.Console.WriteLine("Mail ID : ");
                string mailID = Console.ReadLine();

                System.Console.WriteLine("Gender : ");
                GenderDetails gender = Enum.Parse<GenderDetails>(Console.ReadLine(), true);

                System.Console.WriteLine("Work Station : ");
                string workStation = Console.ReadLine().ToUpper();

                System.Console.WriteLine("Enter Wallet Balance : ");
                double walletBalance = double.Parse(Console.ReadLine());

                UserDetails user = new(name: userName, fatherName: fatherName, gender: gender, mobileNumber: mobileNumber, mailID: mailID, workStation: workStation, walletBalance: walletBalance);
                users.Add(user);
                System.Console.WriteLine("Registration Successful");
                System.Console.WriteLine($"Your UserID : {user.UserID}");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try Again");
            }
        }

        public static void Login()
        {
            try
            {
                System.Console.WriteLine("Enter User ID : ");
                string userID = Console.ReadLine().ToUpper();
                //Binary Search to find the Login User
                Search<UserDetails> search = new Search<UserDetails>();
                bool flag = true;
                currentLoggedCustomer = search.BinarySearch(searchElement: userID, values: users, prop: "UserID", flag: out flag);

                if (flag)
                {
                    //If user not found
                    System.Console.WriteLine("Customer Not Found..!");
                }
                else
                {
                    //If user found
                    System.Console.WriteLine("*************** Login Successful ****************");
                    SubMenu();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try Again");
            }
        }


        public static void SubMenu()
        {
            try
            {
                bool subMenuFlag = true;
                do
                {
                    System.Console.WriteLine("1. Show Profile | 2. Food Order      | 3. Modify Order");
                    System.Console.WriteLine("4. Cancel Order | 5. Wallet Recharge | 6. Show Balance | 7. Exit");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                ShowProfile();
                                break;
                            }
                        case 2:
                            {
                                FoodOrder();
                                break;
                            }
                        case 3:
                            {
                                ModifyOrder();
                                break;
                            }
                        case 4:
                            {
                                CancelOrder();
                                break;
                            }
                        case 5:
                            {
                                WalletRecharge();
                                break;
                            }
                        case 6:
                            {
                                ShowBalance();
                                break;
                            }
                        case 7:
                            {
                                System.Console.WriteLine("*************** Exit ****************");
                                subMenuFlag = false;
                                break;
                            }
                    }
                } while (subMenuFlag);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try Again");
            }
        }


        public static void ShowProfile()
        {
            try
            {
                Grid<UserDetails>.ShowTable([currentLoggedCustomer]);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try Again");
            }
        }

        public static void FoodOrder()
        {
            try
            {
                
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try Again");
            }
        }
        public static void ModifyOrder()
        {
            try
            {

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try Again");
            }
        }
        public static void CancelOrder()
        {
            try
            {
                //Show all Ordered Orders
                bool isOrder = false;
                CustomList<OrderDetails> orderedOrder = [];
                foreach (OrderDetails orderDetails in orders)
                {
                    if (currentLoggedCustomer.UserID.Equals(orderDetails.UserID) && orderDetails.OrderStatus.Equals(OrderStatus.Ordered))
                    {
                        isOrder = true;
                        orderedOrder.Add(orderDetails);
                    }
                }
                if (isOrder)
                {
                    Grid<OrderDetails>.ShowTable(orderedOrder);
                    System.Console.WriteLine("Enter Order Id to Cancel : ");
                    string orderID = Console.ReadLine();
                    //Binary Search to find the Login User
                    Search<OrderDetails> search = new Search<OrderDetails>();
                    bool flag = true;
                    OrderDetails order = search.BinarySearch(searchElement: orderID, values: orders, prop: "OrderID", flag: out flag);

                    if (flag)
                    {
                        //If user not found
                        System.Console.WriteLine("OrderID Invalid....");
                    }
                    else
                    {
                        //If order Found
                        //Add Food Item Quantity
                        foreach(CartItem cartItem in cartItems)
                        {
                            if(order.OrderID.Equals(cartItem.OrderID))
                            {
                                foreach(FoodDetails foodDetails in foods)
                                {
                                    if(foodDetails.FoodID.Equals(cartItem.FoodID))
                                    {
                                        foodDetails.AvailableQuantity+=cartItem.OrderQuantity;
                                    }
                                }
                            }
                        }
                        //Order Status
                        order.OrderStatus=OrderStatus.Cancelled;
                        //User Wallet Refund
                        currentLoggedCustomer.WalletRecharge(order.TotalPrice);
                        System.Console.WriteLine("Order Cancelled Successfully");
                        
                    }
                }
                else
                {
                    System.Console.WriteLine("No Order History...");
                }


            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try Again");
            }
        }
        public static void WalletRecharge()
        {
            try
            {
                System.Console.WriteLine("Enter Amount to Recharge : ");
                currentLoggedCustomer.WalletRecharge(double.Parse(Console.ReadLine()));
                System.Console.WriteLine($"Wallet Recharged Your Wallet Balance is {currentLoggedCustomer.WalletBalance}");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try Again");
            }
        }
        public static void ShowBalance()
        {
            try
            {
                System.Console.WriteLine($"Your Wallet Balance is {currentLoggedCustomer.WalletBalance}");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Try Again");
            }
        }
    }
}