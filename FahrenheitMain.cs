using System;

public class FahrenheitMain
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to my Farhenheit 451 thingy");
        Console.WriteLine("Type y to say yes, n to say no, type y to continue");
        string answer = Console.ReadLine();
        
        switch(answer) {
            case "y":
                a();
                break;
            default:
                Console.WriteLine("why did you not press y or n, please restart the program");
                break;
        } 
    
    }
    
    public static void a()
    {
        Console.WriteLine("please enter your name");
        
        string answer = Console.ReadLine();
        
        switch(answer) {
            case "":
                
                break;
            default:
                a();
                break;
        } 
    }
}
