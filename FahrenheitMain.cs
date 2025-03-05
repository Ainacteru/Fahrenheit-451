using System;

public class FahrenheitMain
{
    public static int author = 0;
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
                Console.WriteLine("ew");
                break;
        } 
    
    }
    
    public static void askForName()
    {
        Console.WriteLine("please enter your name");
        
        string answer = Console.ReadLine();
        if (checkForName(answer)){
            nameBook();
        }
        else {
            Console.WriteLine("ew");
        }
    }
    
    static bool checkForName(string answer)
    {
        string[] authors = {"Jonathan Swift", 
        "Charles Darwin, Schopenhauer", "Einstein", "Albert Schweitzer", "Aristophanes", "Mahatma Ghandi", "Gautama Buddha", "Confucius", "Thomas Love Peacock", "Thomas Jefferson", "Lincoln", "Tom Paine", "Machiavelli", "Christ"};
        
        for(int i = 0; i < authors.Length; i++){
            if (answer == authors[i]){
                author = i;
                return true;
            }
        }
        return false;
    }
    
    static void nameBook()
    {
        string[] books = {
            "Gulliver's Travels", // Jonathan Swift
            "On the Origin of Species, The World as Will and Representation", // Charles Darwin, Schopenhauer
            "Relativity: The Special and the General Theory", // Einstein
            "The Philosophy of Civilization", // Albert Schweitzer
            "The Clouds", // Aristophanes
            "The Story of My Experiments with Truth", // Mahatma Gandhi
            "Dhammapada", // Gautama Buddha
            "Analects", // Confucius
            "Nightmare Abbey", // Thomas Love Peacock
            "The Declaration of Independence", // Thomas Jefferson
            "The Gettysburg Address", // Lincoln
            "Common Sense", // Tom Paine
            "The Prince", // Machiavelli
            "The Bible" // Christ
        };
        Console.WriteLine("Please name a book you have written");
        var answer = Console.ReadLine();
        if (answer == books[author])
        {
            Console.WriteLine("yayy");
        }
        else 
        {
            Console.WriteLine("ew");
        }
        
    }
}
