public class Database {

    // Path to the folder containing the text files
    public static readonly string _textFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/TextFiles");
     public static string[] books = {
            "Gulliver's Travels",
            "On the Origin of Species", 
            "The World as Will and Representation",
            "Relativity: The Special and the General Theory",
            "The Philosophy of Civilization",
            "The Clouds",
            "The Story of My Experiments with Truth",
            "Dhammapada",
            "Analects",
            "Nightmare Abbey",
            "The Declaration of Independence",
            "The Gettysburg Address",
            "Common Sense",
            "The Prince",
            "The Bible"
        };

        public static string[] authors = {
            "Jonathan Swift", 
            "Charles Darwin", 
            "Schopenhauer", 
            "Einstein", 
            "Albert Schweitzer",
            "Aristophanes", 
            "Mahatma Gandhi", 
            "Gautama Buddha", 
            "Confucius", 
            "Thomas Love Peacock",
            "Thomas Jefferson", 
            "Lincoln", 
            "Tom Paine", 
            "Machiavelli", 
            "Christ"
        };
}