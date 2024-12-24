using System;
using System.Collections.Generic;
using System.Reflection;

namespace OnlineCafe
{
    public static class Grid<DataType>
    {
        public static void ShowTable(CustomList<DataType> list)
        {
            if(list!=null &&list.Count>0)
            {
                    PropertyInfo [] properties =typeof(DataType).GetProperties();
                    System.Console.WriteLine(new string('-',properties.Length*20));
                    //Property Name
                    System.Console.Write("|");
                    foreach(var property in properties)
                    {
                        System.Console.Write($"{property.Name,-15} | ");
                    }
                    System.Console.WriteLine();
                    System.Console.WriteLine(new string('-',properties.Length*20));

                    foreach(var data in list)
                    {
                        System.Console.Write("|");
                        foreach(var property in properties)
                        {
                            if(property.CanRead)
                            {
                                if(property.PropertyType==typeof(DateTime))
                                {
                                    var value=((DateTime)property.GetValue(data)).ToString("dd/MM/yyyy");
                                    Console.Write($"{value,-15}");
                                }
                                else{
                                    var value=property.GetValue(data);
                                    System.Console.Write($"{value,-15} | ");
                                }
                            }
                            
                        }
                        System.Console.WriteLine();
                    }
                    System.Console.WriteLine(new string('-',properties.Length*20));
            }
        }
    }
}