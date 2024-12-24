using System;

namespace OnlineCafe;

class Program 
{
    //Main Class
    //Main Method to Start Application
    public static void Main(string[] args)
    {
        FileHandling.CreateFS();
        Operation.LoadFiles();
        Operation.MainMenu();
    }
}