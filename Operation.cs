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
            ReadWriteFS<FoodDetails>.ReadFiles(fileName: "FoodDetails.csv", values: out foods);

            //Read User Details
            ReadWriteFS<UserDetails>.ReadFiles(fileName: "UserDetails.csv", values: out users, typeof(GenderDetails));

            //Read Cart Items
            ReadWriteFS<CartItem>.ReadFiles(fileName: "CartItems.csv", values: out cartItems);

            //Read Orders
            ReadWriteFS<OrderDetails>.ReadFiles(fileName: "Orders.csv", values: out orders, typeof(OrderStatus));
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
                    System.Console.WriteLine("4. Cancel Order | 5. Order History   | 6. Wallet Recharge | 7. Show Balance | 8. Exit");
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
                                OrderHistory();
                                break;
                            }
                        case 6:
                            {
                                WalletRecharge();
                                break;
                            }
                        case 7:
                            {
                                ShowBalance();
                                break;
                            }
                        case 8:
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
                                TraverseCartItems(localCartItem: localCartItem);
                            }
                        }
                        //User doesn't Wish to Recharge
                        else
                        {
                            TraverseCartItems(localCartItem: localCartItem);
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
                //Check Order ID is valid
                bool orderedOrder = true;
                //Current Customer Orders
                CustomList<OrderDetails> currentCustomerOrders = new CustomList<OrderDetails>();
                foreach (OrderDetails order in orders)
                {
                    if (currentLoggedCustomer.UserID.Equals(order.UserID) && order.OrderStatus.Equals(OrderStatus.Ordered))
                    {
                        orderedOrder = false;
                        currentCustomerOrders.Add(order);
                    }
                }
                //If no Orders
                if (orderedOrder)
                {
                    System.Console.WriteLine("No Order History...");
                }
                else
                {
                    //Show all Orders
                    Grid<OrderDetails>.ShowTable(currentCustomerOrders);
                    //Get Order ID
                    System.Console.WriteLine("Enter Order ID : ");
                    string orderID = Console.ReadLine().ToUpper();
                    //Binary Search
                    OrderDetails cancelOrder = Search<OrderDetails>.BinarySearch(values: currentCustomerOrders, prop: "OrderID", searchElement: orderID, out orderedOrder);
                    if (orderedOrder)
                    {
                        //If Order ID Incorrect
                        System.Console.WriteLine("Order ID Invalid...");
                    }
                    else
                    {
                        //Get Cart Items of the Order
                        CustomList<CartItem> cancelCart = new CustomList<CartItem>();
                        double totalPrice = 0;
                        foreach (CartItem cartItem in cartItems)
                        {
                            if (cartItem.OrderID.Equals(cancelOrder.OrderID))
                            {
                                totalPrice += cartItem.OrderPrice;
                                cancelCart.Add(cartItem);
                            }
                        }
                        //Show all Cart Items
                        Grid<CartItem>.ShowTable(cancelCart);
                        //Get Cart Item to Modify
                        System.Console.WriteLine("Enter Item ID to Modify : ");
                        string itemID = Console.ReadLine().ToUpper();
                        //To check Cart ID is Valid
                        bool cartNotExists = true;
                        CartItem modifyItem = Search<CartItem>.BinarySearch(values: cartItems, prop: "ItemID", searchElement: itemID, out cartNotExists);
                        if (cartNotExists)
                        {
                            //Invalid Cart Item
                            System.Console.WriteLine("Cart Item Invalid...");
                        }
                        else
                        {
                            //Get new Quantity
                            System.Console.WriteLine("Enter New Quantity to Modify : ");
                            int newQuantity = int.Parse(Console.ReadLine());
                            //Calculate New Price
                            //If Extra Quantity Ordered
                            //Check weather need to Add or Minus Total Price
                            if (newQuantity > modifyItem.OrderQuantity)
                            {
                                //Remove Modify Cart Price from Total Price
                                totalPrice -= modifyItem.OrderPrice;
                                //To get Food Price
                                double foodPrice = modifyItem.OrderPrice / modifyItem.OrderQuantity;
                                //New Quantity Price
                                double newPrice = newQuantity * foodPrice;
                                //New Total Price
                                totalPrice += newPrice;
                                //Check New Quantity Price is Available in Customer Wallet
                                //If Wallet Amount is Not Enough
                                if (newPrice > currentLoggedCustomer.WalletBalance)
                                {
                                    //Insufficient Balance
                                    System.Console.WriteLine("Insufficient Balance");
                                    //Recharge Wallet
                                    System.Console.WriteLine("Are you willing to recharge (yes/no): ");
                                    string recharge = Console.ReadLine().ToLower();
                                    if (recharge == "yes")
                                    {
                                        System.Console.WriteLine($"New Order Quantity Amount : {newPrice}\nMinimum Recharge Amount : {newPrice - currentLoggedCustomer.WalletBalance}");
                                        System.Console.WriteLine("Enter Recharge Amount : ");
                                        currentLoggedCustomer.WalletRecharge(double.Parse(Console.ReadLine()));
                                        bool balance = newPrice <= currentLoggedCustomer.WalletBalance ? true : false;
                                        if (balance)
                                        {
                                            //Change Cart Details Food Quantiy
                                            FoodDetails food = Search<FoodDetails>.BinarySearch(values: foods, prop: "FoodID", searchElement: modifyItem.FoodID, out balance);
                                            //Change Food Quantity
                                            food.AvailableQuantity -= (newQuantity - modifyItem.OrderQuantity);
                                            modifyItem.OrderPrice = newPrice;
                                            modifyItem.OrderQuantity = newQuantity;
                                            //Deduct Wallet Amount
                                            currentLoggedCustomer.DeductAmount(newPrice);
                                            System.Console.WriteLine("Order Modified Succesfully...");
                                        }
                                        else
                                        {
                                            //Recharge Amount is Lower than New Cart Price
                                            System.Console.WriteLine("Order Modified Cancelled Insufficient Balance");
                                        }
                                    }
                                    //Customer doesn't Wish to Recharge Modify Order Cancelled
                                    else
                                    {
                                        System.Console.WriteLine("Order Modified Cancelled Insufficient Balance");
                                    }
                                }
                                else
                                //Wallet has Balance 
                                {
                                    //Change Cart Details Food Quantiy
                                    FoodDetails food = Search<FoodDetails>.BinarySearch(values: foods, prop: "FoodID", searchElement: modifyItem.FoodID, out cartNotExists);
                                    //Change Food Quantity
                                    food.AvailableQuantity -= (newQuantity - modifyItem.OrderQuantity);
                                    modifyItem.OrderPrice = newPrice;
                                    modifyItem.OrderQuantity = newQuantity;
                                    //Deduct Wallet Amount
                                    currentLoggedCustomer.DeductAmount(newPrice);
                                    System.Console.WriteLine("Order Modified Succesfully...");
                                }
                            }
                            else if (newQuantity == modifyItem.OrderQuantity)
                            //Same Quantity as New Quantity
                            {
                                System.Console.WriteLine("Same Quantity Number in Cart");
                            }
                            else
                            //New Quantity is minimum than Cart Quantity
                            //Add the Remaining Amount to User Wallet
                            {
                                //Remove Modify Cart Price from Total Price
                                totalPrice -= modifyItem.OrderPrice;
                                //To get Food Price
                                double foodPrice = modifyItem.OrderPrice / modifyItem.OrderQuantity;
                                //New Quantity Price
                                double newPrice = newQuantity * foodPrice;
                                //To get Balance Price
                                double balance = modifyItem.OrderPrice - newPrice;
                                //New Total Price
                                totalPrice += newPrice;
                                //Add Balance to Customer wallet
                                currentLoggedCustomer.WalletRecharge(balance);
                                //Change Cart Food Details
                                FoodDetails food = Search<FoodDetails>.BinarySearch(values: foods, prop: "FoodID", searchElement: modifyItem.FoodID, out cartNotExists);
                                //Change Food Quantity
                                food.AvailableQuantity += (modifyItem.OrderQuantity - newQuantity);
                                modifyItem.OrderPrice = newPrice;
                                modifyItem.OrderQuantity = newQuantity;
                                System.Console.WriteLine("Order Modified Successfully...");
                            }
                        }
                    }
                }

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
                    System.Console.WriteLine(orderDetails.OrderStatus);
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
                    string orderID = Console.ReadLine().ToUpper();
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
        
        public static void OrderHistory()
        {
            try
            {
                CustomList<OrderDetails> historyOrders=new CustomList<OrderDetails>();
                foreach(OrderDetails order in orders)
                {
                    if(order.UserID.Equals(currentLoggedCustomer.UserID))
                    {
                        historyOrders.Add(order);
                    }
                }
                //Show Order History
                Grid<OrderDetails>.ShowTable(historyOrders);
            }
            catch(Exception e)
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