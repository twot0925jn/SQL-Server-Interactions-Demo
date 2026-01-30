using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SQLServerInteractionsDemo__ClassLibrary;
using SQLServerInteractionsDemo_ClassLibrary;
using System.Data;
using System.Globalization;


namespace SQL_Server_Interactions_Demo
{
    internal class Program
    {

        public static List<string> GetTables() //Retrieval and display of table names from the connected database, no specific function beyond that currently
        {


            using (SqlConnection connection = new SqlConnection
                (
                $"Server={Utilities.ConnectionString["DataSource"]};" +
                $"Database={Utilities.ConnectionString["InitialCatalogue"]};" +
                "Trusted_Connection=True;" +
                "Encrypt=False")
                )
            {
                connection.Open();
                DataTable schema = connection.GetSchema("Tables");
                List<string> tableNames = new List<string>();
                foreach (DataRow row in schema.Rows)
                {
                    string tableName = row[2].ToString();
                    Console.WriteLine(tableName);
                    tableNames.Add(tableName);
                }
                return tableNames;
            }
        }

        public static List<CustomClass> LoadDatabase() //Retrieval of entries from connected database
        {
            var databaseContents = new List<CustomClass>();
            using (SQLServerConnectionContext db = new SQLServerConnectionContext())
            {
                databaseContents = db.ExampleTable.ToList();
            }

            return databaseContents;
        }

        public static void AddToDatabase(List<CustomClass> data) //Addition of new entry to connected database
        {
            using (SQLServerConnectionContext db = new SQLServerConnectionContext())
            {
                db.ExampleTable.Add(data.Last());
                db.SaveChanges();
            }

            Console.WriteLine("\nDatabase updated");
        }

        public static void RemoveFromDatabase(List<CustomClass> data) //Remocal of entry selection fro connected database
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

        public static void ModifyDatabaseEntry(List<CustomClass> data) //Modification of entry selection from connected database
        {
            string modificationCandidate = EntrySelection(data, "modify");
            if (modificationCandidate == "0")
            {
                return;
            }
            else
            {
                using (SQLServerConnectionContext db = new SQLServerConnectionContext())
                {
                    CustomClass databaseMatchedCandidate = db.ExampleTable.Single(a => a.Id.ToString() == modificationCandidate); //return the only entry in the database that matches the returned removalCandidate string 
                    databaseMatchedCandidate.Field1 = "Dolor";
                    databaseMatchedCandidate.Field3 = 3;

                    db.SaveChanges();
                }
                Console.WriteLine($"\nDatabase updated, entry ID {modificationCandidate} modified.");
                ContinuePrompt();
            }
        }

        public static void DatabasePrintRows(List<CustomClass> data) //Retrieve and display all entries from connected database
        {
            LoadDatabase();
            foreach (var item in data)
            {
                Console.WriteLine($"ID: {item.Id}, Field1: {item.Field1}, Field2: {item.Field2}, Field3: {item.Field3}");
            }
        }

        public static void GenerateNewRow(List<CustomClass> data) //Generation and addition of new database row based on user selected category (otherwise default values)
        {
            string input = "";
            Console.Clear();

            while (!CustomClass.allowedCategories.Contains(input))
            {
                Console.WriteLine($"Please select from the following: {string.Join(", ", CustomClass.allowedCategories)}\n");

                input = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().Trim() ?? string.Empty); //Convert input to title case
                
                if (!CustomClass.allowedCategories.Contains(input))
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

        public static string EntrySelection(List<CustomClass> data, string mode) //Selection of entry to modify/remove depending on given mode
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

        public static string GetAttributeInput(string field) //User promting for attribute retrieval (specifically for attribute modification)
        {
            Console.WriteLine($"Please enter a value for {field}: ");

            string value = Console.ReadLine().Trim();

            if (ValidateAttribute(field, value))
            {
                return value;
            }
            else
            {
                return "";
            }
        }

        public static bool ValidateAttribute(string field, string value) //Validation of attribute given by user against allowed values/ranges (specifically for attribute modification)
        {
            switch (field)
            {
                case "Field1":
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                            break;
                    }
                case "Field2":
                    {
                        if (CustomClass.allowedCategories.Contains(value))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
                case "Field3":
                    {
                        if (int.TryParse(value, out int num) && (num >= 0 && num <= 100))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
                default:
                    {
                        return false;
                        break;
                    }
            }
            return false;
        }

        public static void SelectionMenu() //Main application menu loop
        {
            bool exitApplication = false;

            while (!exitApplication)
            {
                List<CustomClass> data = LoadDatabase();

                ListMenuOptions();
                string choice = Console.ReadLine().Trim();

                while (!CustomClass.allowedCategories.Contains(choice))
                {
                    Console.Clear();
                    Console.WriteLine($"Invalid selection, only the following are allowed: {string.Join(", ", CustomClass.allowedCategories)}\n");

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
                        ModifyDatabaseEntry(data);
                        break;
                    case "4":
                        RemoveFromDatabase(data);
                        break;
                    case "7":
                        Console.WriteLine("Feature not implemented yet.\n");
                        break;
                    case "8":
                        Console.Clear();
                        GetTables();
                        ContinuePrompt();
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
        public static void ContinuePrompt() //Prompt to pause application until user is ready to continue
        {
            Console.WriteLine("\nPress enter to continue");
            Console.ReadLine();
        }

        public static void ListMenuOptions() //Display of main menu options
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. List Database Entries");
            Console.WriteLine("2. Generate and add new entry (default data)");
            Console.WriteLine("3. Modify entry");
            Console.WriteLine("4. Remove entry");
            Console.WriteLine("7. List server databases");
            Console.WriteLine("8. List database tables");
            Console.WriteLine("9. Exit\n");
        }

        public static void Main(string[] args)
        {
            SelectionMenu();
        }
    }
}
