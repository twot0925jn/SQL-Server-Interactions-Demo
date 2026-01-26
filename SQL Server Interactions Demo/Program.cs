using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using SQLServerInteractionsDemo_ClassLibrary;
using System.Collections.Generic;
using System.Linq;

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

        public static void Main(string[] args)
        {
            var data = LoadDatabase();
            foreach (var item in data)
            {
                System.Console.WriteLine($"ID: {item.Id}, Field1: {item.Field1}, Field2: {item.Field2}, Field3: {item.Field3}");
            }
        }
    }
}
