using System;

public class FahrenheitMain
{
    public static int author = 0;
    static string[] books = {
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
    static string[] authors = {
        "Jonathan Swift", 
        "Charles Darwin", 
        "Schopenhauer", 
        "Einstein", 
        "Albert Schweitzer", 
        "Aristophanes", 
        "Mahatma Ghandi", 
        "Gautama Buddha", 
        "Confucius", 
        "Thomas Love Peacock", 
        "Thomas Jefferson", 
        "Lincoln", 
        "Tom Paine", 
        "Machiavelli", 
        "Christ"
    };
    static bool limited = true;
        
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to my Farhenheit 451 thingy");
        Console.WriteLine("Type y to say yes, n to say no, type y to continue");
        string answer = Console.ReadLine();
        
        switch(answer) {
            case "y":
                AskForName();
                break;
            default:
                Denied();
                break;
        } 
    
    }
    
    public static void AskForName()
    {
        Console.WriteLine("please enter your name");
        
        string answer = Console.ReadLine();
        if (CheckForName(answer)){
            NameBook();
        }
        else {
            Denied();
        }
    }
    
    static bool CheckForName(string answer)
    {
        for(int i = 0; i < authors.Length; i++){
            if (answer == authors[i]){
                author = i -1;
                //Console.WriteLine(i);
                return true;
            }
        }
        return false;
    }
    
    static void NameBook()
    {
        Console.WriteLine("Please name your assigned title");
        var answer = Console.ReadLine();
        if (answer == books[author])
        {
           ShowLimited();
        }
        else if (answer != books[author])
        {
           Denied();
        }
        
    }
    
    static void ShowLimited()
    {
        Console.WriteLine("Welcome!");
        ListBooks();
    }
    
    static void ListBooks()
    {
        if(limited) {
            Console.WriteLine("You can access 1 book(s)");
            Console.WriteLine("");
            Console.WriteLine(books[author].ToString());
            
        }
        else if (!limited) {
            
        }
        
        Console.WriteLine("");
        Console.WriteLine("Type a command, type help for commands");
        MoreAccess();
        
    }
    
    static void MoreAccess()
    {
        var answer = Console.ReadLine();
        switch (answer) {
            case "help":
                GetHelp();
                break;
            case "get permission":
                GetPermissions();
                break;
            default:
            Console.WriteLine("Not a command, type help for a list of valid commands");
            MoreAccess();
                break;
        }
    }
    
    static void GetPermissions()
    {
        Console.WriteLine("I rise from the ashes, reborn anew, in a world of fire, where books are few a symbol of hope when all seems lost, I am reborn at any cost.");
        Console.WriteLine("Type...");
        
        if (Console.ReadLine() == "Phoenix")
        {
            limited = false;
            Console.WriteLine("Permission Granted");
            MoreAccess();
        }
        else {
            Console.WriteLine("Permission Denied");
            MoreAccess();
        }
    }
    
    static void GetHelp()
    {
        Console.WriteLine("help: opens this menu");
        MoreAccess();
    }
    
    
    static void Denied()
    {
        Console.WriteLine("Access Denied");
    }
}
