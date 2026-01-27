using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServerInteractionsDemo_ClassLibrary
{
    public class CustomSubClass2: CustomClass
    {
        public CustomSubClass2(string field1 = "Ipsum", string field2 = "Cat2", int field3 = 97)
            : base(field1: field1, field2: field2, field3: field3)
        {

        }
    }
}
