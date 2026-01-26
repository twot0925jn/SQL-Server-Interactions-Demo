using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SQLServerInteractionsDemo__ClassLibrary
{
    public class SQLServerConnectionContext
    {

        public string dataSource = "(local)"; //Server name or network address of the SQL Server instance
        public string initialCatalog = "";  //Database name
        public string userID = ""; //Username for SQL Server authentication
        public string password = ""; //Password for SQL Server authentication


        //DBContect represents a session with the database and can be used to query and save instances of your entities.
        //The following is a context class
        public class ConnectionContext : DbContext
        {
            //DBSet represents a collection of entities that can be queried from the database
            public DbSet<CustomClass> CustomClass { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<CustomClass>()
                    .HasDiscriminator<string>("Field1")
                    .HasValue<CustomSubClass1>("ValueVariant1")
                    .HasValue<CustomSubClass2>("ValueVariant2")
                    .HasValue<CustomSubClass3>("ValueVariant3");
            }

            // The Connection string is used to specify connection details such as server, database, and credentials
            //The context class's OnConfiguring method handles the connection string
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer($"Server={dataSource};Database={initialCatalog};Trusted_Connection=True;Encrypt=False");
            }
        }
    }
}

