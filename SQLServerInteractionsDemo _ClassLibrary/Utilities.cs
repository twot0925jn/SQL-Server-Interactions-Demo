using System;
using System.Collections.Generic;

namespace SQLServerInteractionsDemo__ClassLibrary
{
    public class Utilities
    {
        public static HashSet<string> AcceptableCategories = new HashSet<string> 
        { 
            "Cat1", 
            "Cat2", 
            "Cat3", 
            "Cat4" 
        };

        public static HashSet<string> AllowedMenuChoices = new HashSet<string> 
        { 
            "1", 
            "2", 
            "3", 
            "4", 
            "7", 
            "8", 
            "9" 
        };

        public static Dictionary<string, string> ConnectionString = new Dictionary<string, string> 
        {
            { "DataSource", "(localdb)\\MSSQLLocalDB" },
            { "InitialCatalog", "ExampleDatabase" },
            { "UserID", "" },
            { "UserPass", "" }
        };
    }
}
