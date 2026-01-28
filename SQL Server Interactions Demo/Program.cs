using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SQLServerInteractionsDemo_ClassLibrary;
using System.Collections.Generic;
using System.Globalization;


namespace SQL_Server_Interactions_Demo
{
    internal class Program
    {
        public static List<CustomClass> LoadDatabase()
        {
            var databaseContents = new List<CustomClass>();
            using (SQLServerConnectionContext db = new SQLServerConnectionContext())
            {
                databaseContents = db.ExampleTable.ToList();
            }

            return databaseContents;
        }

        public static void AddToDatabase(List<CustomClass> data)
        {
            using (SQLServerConnectionContext db = new SQLServerConnectionContext())
            {
                db.ExampleTable.Add(data.Last());
                db.SaveChanges();
            }

            Console.WriteLine("\nDatabase updated");
        }

        public static void RemoveFromDatabase(List<CustomClass> data)
        {
            string removalCandidate = EntrySelection(data, "remove");
            
            if (removalCandidate == "0")
            {
                return;
            }
            else
            {
                using (SQLServerConnectionContext db = new SQLServerConnectionContext())
                {
                    CustomClass databaseMatchedCandidate = db.ExampleTable.Single(a => a.Id.ToString() == removalCandidate); //return the only entry in the database that matches the returned removalCandidate string 
                    db.ExampleTable.Remove(databaseMatchedCandidate);
                    db.SaveChanges();
                }

                Console.WriteLine($"\nDatabase updated, entry ID {removalCandidate} removed.");
                ContinuePrompt();
            }       
        }

        public static void DatabasePrintRows(List<CustomClass> data)
        {
            LoadDatabase();
            //Console.WriteLine("Current Rows:\n");
            foreach (var item in data)
            {
                Console.WriteLine($"ID: {item.Id}, Field1: {item.Field1}, Field2: {item.Field2}, Field3: {item.Field3}");
            }
        }

        public static void GenerateNewRow(List<CustomClass> data)
        {
            string input = "";
            HashSet<string> acceptableCategories = new HashSet<string> { "Cat1", "Cat2", "Cat3", "Cat4" };

            Console.Clear();

            //validate input against acceptableCategories hashset
            while (!acceptableCategories.Contains(input))
            {
                Console.WriteLine($"Please select from the following: {string.Join(", ", acceptableCategories)}\n");

                input = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().Trim() ?? string.Empty); //Convert input to title case
                
                if (!acceptableCategories.Contains(input))
                {
                    Console.Clear();
                    Console.WriteLine($"Selection \"{input}\" is invalid.\n");
                }
                else
                {
                    break;
                }

            }

            switch (input)
            {
                case "Cat1":
                    data.Add(new CustomSubClass1());
                    Console.WriteLine($"\nNew {input} row generated");
                    break;
                case "Cat2":
                    data.Add(new CustomSubClass2());
                    Console.WriteLine($"\nNew {input} row generated");
                    break;
                case "Cat3":
                    data.Add(new CustomSubClass3());
                    Console.WriteLine($"\nNew {input} row generated");
                    break;
                case "Cat4":
                    data.Add(new CustomSubClass4());
                    Console.WriteLine($"\nNew {input} row generated");
                    break;
                default:
                    Console.WriteLine("Category requirements not satisfied");
                    break;
            }

            AddToDatabase(data);
            ContinuePrompt();
        }

        public static string EntrySelection(List<CustomClass> data, string mode)
        {
            Console.Clear();
            List<string> id = new List<string> ();
            string selection = "";

            if (data.Count() == 0)
            {
                Console.WriteLine($"There are no entries currently available to {mode}.");
                return "0";
            }
            else
                foreach (var item in data)
                {
                    id.Add(item.Id.ToString());
                }

            while (!id.Contains(selection))
            {
                Console.WriteLine($"Please select the id that you want to {mode}:\n");
                DatabasePrintRows(data);
                Console.WriteLine("\n");
                selection = Console.ReadLine().Trim();
                if (!id.Contains(selection))
                {
                    Console.Clear();
                    Console.WriteLine($"Invalid Selection, please choose from: {string.Join(", ", id)}\n");
                }
                else
                {
                    break;
                }
            }

            return selection;
        }

        public static void SelectionMenu()
        {
            bool exitApplication = false;

            while (!exitApplication)
            {

                HashSet<string> allowedChoices = new HashSet<string> { "1", "2", "3", "4", "9" };

                List<CustomClass> data = LoadDatabase();

                ListMenuOptions();
                string choice = Console.ReadLine().Trim();

                while (!allowedChoices.Contains(choice))
                {
                    Console.Clear();
                    Console.WriteLine($"Invalid selection, only the following are allowed: {string.Join(", ", allowedChoices)}\n");

                    ListMenuOptions();

                    choice = Console.ReadLine().Trim();
                }

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Current entries:\n");
                        DatabasePrintRows(data);
                        ContinuePrompt();
                        break;
                    case "2":
                        GenerateNewRow(data);
                        break;
                    case "3":
                        Console.WriteLine("Option not currently in use...");
                        break;
                    case "4":
                        RemoveFromDatabase(data);
                        break;
                    case "9":
                        Console.Clear();
                        Console.WriteLine("Exiting application...\n");
                        exitApplication = true;
                        break;
                    default:
                        break;
                }
            }

        }
        public static void ContinuePrompt()
        {
            Console.WriteLine("\nPress enter to continue");
            Console.ReadLine();
        }

        public static void ListMenuOptions()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. List Database Entries");
            Console.WriteLine("2. Generate and add new entry (default data)");
            Console.WriteLine("3. Modify entry");
            Console.WriteLine("4. Remove entry");
            Console.WriteLine("9. Exit\n");
        }

        public static void Main(string[] args)
        {
            SelectionMenu();
        }
    }
}
