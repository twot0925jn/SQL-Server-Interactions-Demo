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

        public static void AddToDatabase(List<CustomClass> customClass)
        {
            using (SQLServerConnectionContext db = new SQLServerConnectionContext())
            {
                db.ExampleTable.Add(customClass.Last());
                db.SaveChanges();
            }

            Console.WriteLine("Database updated");
        }

        public static void DatabasePrintRows(List<CustomClass> data)
        {
            Console.Clear();
            Console.WriteLine("Current Rows:\n");
            foreach (var item in data)
            {
                Console.WriteLine($"ID: {item.Id}, Field1: {item.Field1}, Field2: {item.Field2}, Field3: {item.Field3}");
            }

            ContinuePrompt();
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
                    Console.WriteLine("New row generated");
                    break;
                case "Cat2":
                    data.Add(new CustomSubClass2());
                    Console.WriteLine("New row generated");
                    break;
                case "Cat3":
                    data.Add(new CustomSubClass3());
                    Console.WriteLine("New row generated");
                    break;
                case "Cat4":
                    data.Add(new CustomSubClass4());
                    Console.WriteLine("New row generated");
                    break;
                default:
                    Console.WriteLine("Category requirements not satisfied");
                    break;
            }

            AddToDatabase(data);
            ContinuePrompt();
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
                        DatabasePrintRows(data);
                        break;
                    case "2":
                        GenerateNewRow(data);
                        break;
                    case "3":
                        break;
                    case "4":
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
            Console.Clear();
        }

        public static void ListMenuOptions()
        {
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
