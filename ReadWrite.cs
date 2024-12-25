using System.IO;
using System.Collections.Generic;
using System;

namespace OnlineCafe
{

    public static class ReadWriteFS<DataType>
    {
        public static void ReadFiles(string fileName, out CustomList<DataType> values, object enumTypes=null)
        {
            values = [];
            string[] csvRead = File.ReadAllLines($"OnlineCafe/{fileName}");
            for (int i = 0; i < csvRead.Length; i++)
            {
                var line = csvRead[i];
                var fieldValues = line.Split(",");
                var infoArray = typeof(DataType).GetProperties();
                var dataType = Activator.CreateInstance<DataType>();
                for (int j = 0; j < fieldValues.Length; j++)
                {

                    if (infoArray[j].PropertyType == typeof(double))
                    {
                        infoArray[j].SetValue(dataType, double.Parse(fieldValues[j]));
                    }
                    else if (infoArray[j].PropertyType == typeof(int))
                    {
                        infoArray[j].SetValue(dataType, int.Parse(fieldValues[j]));
                    }
                    else if (infoArray[j].PropertyType == typeof(string))
                    {
                        infoArray[j].SetValue(dataType, fieldValues[j]);
                    }
                    else if (infoArray[j].PropertyType == typeof(DateTime))
                    {
                        infoArray[j].SetValue(dataType, DateTime.ParseExact(fieldValues[j], "MM/dd/yyyy h:mm:ss tt", null));
                    }
                    else
                    {
                        if(infoArray[j].PropertyType.IsEnum)
                        {
                            Enum.TryParse(infoArray[j].PropertyType,fieldValues[j],true,out enumTypes);
                            infoArray[j].SetValue(dataType,enumTypes);
                        }

                        // infoArray[j].SetValue(dataType, Enum.Parse<OrderStatus>(fieldValues[j], true));
                    }
                }


                // DataType dataTypeDetail = new DataType()
                values.Add(dataType);
            }
        }

        public static void WriteFiles(string fileName, CustomList<DataType> values)
        {
            string[] textWrite = new string[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                var infoArray = typeof(DataType).GetProperties();

                for (int j = 0; j < infoArray.Length; j++)
                {
                    if (j == infoArray.Length - 1)
                    {
                        textWrite[i] = textWrite[i] + infoArray[j].GetValue(values[i]).ToString();
                    }
                    else
                    {
                        textWrite[i] = textWrite[i] + infoArray[j].GetValue(values[i]).ToString() + ",";
                    }

                }
            }
            //Writing to File
            File.WriteAllLines($"OnlineCafe/{fileName}", textWrite);
        }
    }
}

