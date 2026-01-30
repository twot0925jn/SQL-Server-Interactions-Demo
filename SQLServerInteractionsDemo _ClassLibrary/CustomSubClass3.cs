using System;
using System.Collections.Generic;

namespace SQLServerInteractionsDemo_ClassLibrary
{
    public class CustomSubClass3: CustomClass
    {
        public CustomSubClass3(string field1 = "Dolor", string field2 = "Cat3", int field3 = 98)
            : base(field1: field1, field2: field2, field3: field3)
        {

        }
    }
}
