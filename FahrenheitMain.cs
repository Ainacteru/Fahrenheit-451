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
                askForName();
                break;
            default:
                Console.WriteLine("why did you not press y or n, please restart the program");
                break;
        } 
    
    }
    
    public static void askForName()
    {
        Console.WriteLine("please enter your name");
        
        string answer = Console.ReadLine();
        if (checkForName(answer)){
            Console.WriteLine("yay");
        }
        else {
            Console.WriteLine("ew");
        }
    }
    
    static boolean checkForName(string answer)
    {
        string[] authors = {"Jonathan Swift", "Charles Darwin, Schopenhauer", "Einstein", "Albert Schweitzer", "Aristophanes", "Mahatma Ghandi", "Gautama Buddha", "Confucius", "Thomas Love Peacock", "Thomas Jefferson", "Lincoln", "Tom Paine", "Machiavelli", "Christ"};
        
        foreach (var writer in authors){
            if (answer == writer){
                return true;
            }
        }
        return false;
    }
}
