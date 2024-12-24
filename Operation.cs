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
            ReadWriteFS<FoodDetails>.ReadFiles(fileName: "FoodDetails.csv", values: out foods,[]);

            //Read User Details
            ReadWriteFS<UserDetails>.ReadFiles(fileName: "UserDetails.csv", values: out users,[GenderDetails.Select]);

            //Read Cart Items
            ReadWriteFS<CartItem>.ReadFiles(fileName: "CartItems.csv", values: out cartItems,[]);

            //Read Orders
            ReadWriteFS<OrderDetails>.ReadFiles(fileName: "Orders.csv", values: out orders,[OrderStatus.Default]);
        }

        public static void WriteFiles()
        {
            //Read Food Details
            ReadWriteFS<FoodDetails>.WriteFiles(fileName: "FoodDetails.csv", values: foods);

            //Read User Details
            ReadWriteFS<UserDetails>.WriteFiles(fileName: "UserDetails.csv", values: users);

            //Read Cart Items
            ReadWriteFS<CartItem>.WriteFiles(fileName: "CartItems.csv", values: cartItems);

            //Read Orders
            ReadWriteFS<OrderDetails>.WriteFiles(fileName: "Orders.csv", values: orders);
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
                bool flag = true;
                currentLoggedCustomer = Search<UserDetails>.BinarySearch(searchElement: userID, values: users, prop: "UserID", flag: out flag);
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
                //Show all Food items
                Grid<FoodDetails>.ShowTable(foods);
                //Create a Order with Status Intiated
                OrderDetails orderDetails = new OrderDetails(orderDate: DateTime.Now, userID: currentLoggedCustomer.UserID, orderStatus: OrderStatus.Initiated, totalPrice: 0);
                //Local Cart Item
                CustomList<CartItem> localCartItem = new CustomList<CartItem>();
                double totalPrice = 0;
                string option = "yes";
                do
                {
                    //Get Food ID
                    System.Console.WriteLine("Enter Food ID : ");
                    string foodID = Console.ReadLine().ToUpper();
                    System.Console.WriteLine("Enter Quantity Required : ");
                    int quantity = int.Parse(Console.ReadLine());
                    bool flag = true;
                    //Food Object with Food ID
                    //Binary Search
                    FoodDetails food = Search<FoodDetails>.BinarySearch(searchElement: foodID, values: foods, prop: "FoodID", flag: out flag);
                    if (flag)
                    {
                        //If Food ID not Found
                        System.Console.WriteLine("Invalid Food ID ...");
                    }
                    else
                    {
                        //Food ID Valid
                        //Check Required Quantity with Available Quantity
                        if (quantity <= food.AvailableQuantity)
                        {
                            //Create a Cart Item
                            CartItem cartItem = new CartItem(orderQuantity: quantity, orderPrice: food.FoodPrice * quantity, foodID: food.FoodID, orderID: orderDetails.OrderID);
                            //Decrement Food Available Quantity
                            food.AvailableQuantity -= quantity;
                            //Adding Total Price of all Cart Items
                            totalPrice = totalPrice + food.FoodPrice * quantity;
                            //Add to Local Cart Item
                            localCartItem.Add(cartItem);
                        }
                        else
                        {
                            //If quantity not available
                            System.Console.WriteLine("Required Quantity Not Available...");
                        }
                        //Ask if User wants to choose another product
                        System.Console.WriteLine("Do you want to pick another product (yes/no): ");
                        option = Console.ReadLine().ToLower();
                    }
                } while (option == "yes");
                //Confirm user wants to purchase
                System.Console.WriteLine("Do you wish to Confirm the Purchase (yes/no): ");
                string confirm = Console.ReadLine().ToLower();
                if (confirm == "yes")
                {
                    //Continue to Order Check Total Price with Wallet
                    if (totalPrice <= currentLoggedCustomer.WalletBalance)
                    {
                        //Place Order
                        PlaceOrder(totalPrice: totalPrice, orderDetails: orderDetails, localCartItem: localCartItem);

                    }
                    else
                    {
                        // bool balance=false;
                        System.Console.WriteLine("Insufficient Balance");
                        System.Console.WriteLine("Are you willing to recharge (yes/no): ");
                        string recharge = Console.ReadLine().ToLower();
                        if (recharge == "yes")
                        {
                            System.Console.WriteLine($"Total Order Amount : {totalPrice}\nMinimum Recharge Amount : {totalPrice - currentLoggedCustomer.WalletBalance}");
                            System.Console.WriteLine("Enter Recharge Amount : ");
                            currentLoggedCustomer.WalletRecharge(double.Parse(Console.ReadLine()));
                            bool balance = totalPrice <= currentLoggedCustomer.WalletBalance ? true : false;
                            if (balance)
                            {
                                //Place Order
                                PlaceOrder(totalPrice: totalPrice, orderDetails: orderDetails, localCartItem: localCartItem);

                            }
                            else
                            {
                                //Increment Food Quantity
                                TraverseCartItems(localCartItem:localCartItem);
                            }
                        }
                        //User doesn't Wish to Recharge
                        else
                        {
                            TraverseCartItems(localCartItem:localCartItem);
                        }

                    }

                }
                else
                {
                    //If not confirmed
                    foreach (CartItem item in localCartItem)
                    {
                        //Traverse Cart Item and Increment Food Available Quantity
                        foreach (FoodDetails food in foods)
                        {
                            if (item.FoodID.Equals(food.FoodID))
                            {
                                food.AvailableQuantity += item.OrderQuantity;
                            }
                        }
                    }
                    System.Console.WriteLine("Exiting Order....");
                }
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
                    bool flag = true;
                    OrderDetails order = Search<OrderDetails>.BinarySearch(searchElement: orderID, values: orders, prop: "OrderID", flag: out flag);

                    if (flag)
                    {
                        //If user not found
                        System.Console.WriteLine("OrderID Invalid....");
                    }
                    else
                    {
                        //If order Found
                        //Add Food Item Quantity
                        foreach (CartItem cartItem in cartItems)
                        {
                            if (order.OrderID.Equals(cartItem.OrderID))
                            {
                                foreach (FoodDetails foodDetails in foods)
                                {
                                    if (foodDetails.FoodID.Equals(cartItem.FoodID))
                                    {
                                        foodDetails.AvailableQuantity += cartItem.OrderQuantity;
                                    }
                                }
                            }
                        }
                        //Order Status
                        order.OrderStatus = OrderStatus.Cancelled;
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

        public static void PlaceOrder(double totalPrice, CustomList<CartItem> localCartItem, OrderDetails orderDetails)
        {
            //Deduct Wallet Amount
            currentLoggedCustomer.DeductAmount(totalPrice);
            //Global CartItem
            cartItems.AddRange(localCartItem);
            orderDetails.OrderStatus = OrderStatus.Ordered;
            orderDetails.TotalPrice = totalPrice;
            orders.Add(orderDetails);
            System.Console.WriteLine($"Order Placed Successfully {orderDetails.OrderID}");
        }
        public static void TraverseCartItems(CustomList<CartItem> localCartItem)
        {
            foreach (CartItem item in localCartItem)
            {
                //Traverse Cart Item and Increment Food Available Quantity
                foreach (FoodDetails food in foods)
                {
                    if (item.FoodID.Equals(food.FoodID))
                    {
                        food.AvailableQuantity += item.OrderQuantity;
                    }
                }
            }
            System.Console.WriteLine("Exiting without Order due to Insufficient Balance");
        }
    }
}