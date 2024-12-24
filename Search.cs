using System;
using System.Reflection;

namespace OnlineCafe
{
    //Search Class
    public static class Search<DataType>
    {
        //Binary Search
        //With CustomList
        //Common Method for all Custom List Class
        //Returns the Object Class
        public static DataType BinarySearch(CustomList<DataType> values, string prop,string searchElement,out bool flag)
        {
            //Binary Serch
            PropertyInfo property =typeof(DataType).GetProperty(prop);
            //Getting Property of search element
            // prop
            int start=0,end=values.Count-1;
            while (start <= end)
            {
                int middle = (start + end) / 2;
                //To find the middle element value
                string middleValue = property.GetValue(values[middle]).ToString();
                int compare = String.Compare(middleValue, searchElement);
                //Comparing
                if (middleValue.Equals(searchElement))
                {
                    flag=false;
                    return values[middle];
                }

                else if (compare > 0)
                {
                    //Middle Element Greater than given Search Element
                    //Change end
                    end = middle - 1;
                }
                else
                {
                    //Middle Element Lesser than given Search Element
                    //Change start
                    start = middle + 1;
                }
            }
            //If Element not fount
            flag=true;
            //default value
            return default(DataType);
        }
    }
}

 // int start = 0, end = customers.Count - 1;
                //Binary Serch
                // while (start <= end)
                // {
                //     int middle = (start + end) / 2;
                //     int compare = String.Compare(customers[middle].CustomerID, customerID);
                //     if (customers[middle].CustomerID.Equals(customerID))
                //     {
                //         //Customer Found
                //         loginFlag = false;
                //         currentLoggedCustomer = customers[middle];
                        // System.Console.WriteLine("*************** Login Successful ****************");
                        // SubMenu();
                //     }

                //     else if (compare > 0)
                //     {
                //         //Middle Element Greater than given Customer ID
                //         //Change end
                //         end = middle - 1;
                //     }
                //     else
                //     {
                //         //Middle Element Lesser than given Customer ID
                //         //Change start
                //         start = middle + 1;
                //     }
                // }