using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SQLServerInteractionsDemo_ClassLibrary
{

    //DBContect represents a session with the database and can be used to query and save instances of your entities.
    //The following is a context class
    public class SQLServerConnectionContext : DbContext
    {
        public string dataSource = "(localdb)\\MSSQLLocalDB";       //Server name or network address of the SQL Server instance
        public string initialCatalog = "ExampleDatabase";           //Database name
        public string userID = "";                                  //Username for SQL Server authentication
        public string userPass = "";                                //Password for SQL Server authentication

        //DBSet represents a collection of entities that can be queried from the database
        //Ensure that the database name is set here to match your SQL Server database (dbo.ExampleTable in the example)
        public DbSet<CustomClass> ExampleTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomClass>()
                .HasDiscriminator<string>("Field2")
                .HasValue<CustomSubClass1>("Cat1")
                .HasValue<CustomSubClass2>("Cat2")
                .HasValue<CustomSubClass3>("Cat3")
                .HasValue<CustomSubClass4>("Cat4");
        }

        // The Connection string is used to specify connection details such as server, database, and credentials
        // The context class's OnConfiguring method handles the connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server={dataSource};Database={initialCatalog};Trusted_Connection=True;Encrypt=False");


            // Use the following where credentials are required:
            // optionsBuilder.UseSqlServer($"Server={dataSource};Database={initialCatalog};User Id={userID};Password={userPass};Trusted_Connection=True;Encrypt=False");
        }
    }
}

