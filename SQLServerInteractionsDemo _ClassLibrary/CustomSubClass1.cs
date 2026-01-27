using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServerInteractionsDemo_ClassLibrary
{
    public class CustomSubClass1: CustomClass
    {
        public CustomSubClass1(string field1 = "Lorem", string field2 = "Cat1", int field3 = 96)
            : base(field1: field1, field2: field2, field3: field3)
        {

        }
    }
}
