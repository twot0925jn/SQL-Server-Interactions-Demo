using System;
using System.Collections.Generic;

namespace SQLServerInteractionsDemo_ClassLibrary
{
    public class CustomSubClass4 : CustomClass
    {
        public CustomSubClass4(string field1 = "Sit", string field2 = "Cat4", int field3 = 99)
            : base(field1:field1, field2:field2, field3:field3)
        {

        }
    }
}

