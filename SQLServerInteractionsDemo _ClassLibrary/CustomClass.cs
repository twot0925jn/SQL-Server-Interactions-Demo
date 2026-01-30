using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLServerInteractionsDemo_ClassLibrary
{
    public abstract class CustomClass
    {
        public static int id { get; set; } = 0;
        public static readonly HashSet<string> allowedCategories = new HashSet<string> { "Cat1", "Cat2", "Cat3", "Cat4" };

        //Private instance fields start with an underscore
        private int _id = 0;
        private string _field1;
        private string _field2;
        private int _field3;

        // Class constructors are used to initialise objects and set default fields
        public CustomClass(int? id = null, string field1 = "None", string field2 = "Cat4", int field3 = 99)
        {
            Field1 = field1;
            Field2 = field2;
            Field3 = field3;
            _id = id != null ? (int)id : 0;

        }

        [Key] //requires System.ComponentModel.DataAnnotations;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public HashSet<string> AllowedCategories
        {
            get => allowedCategories;
        }

        public string Field1
        {
            get { return _field1; }
            set
            {
                _field1 = value;
            }
        }

        public string Field2
        {
            get => _field2;
            set => _field2 = value;
        }

        public int Field3
        {
            get => _field3;
            set => _field3 = value;
        }
}
}
