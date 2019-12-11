using System;
using System.Collections.Generic;
using System.Text;

namespace AlgNum3
{
    public class User
    {
        int ID;
    }
    public class Rating
    {
        int ID;
        int UserID;
        int ProductID;
        int Value;
    }
    public class Product
    {
        int ID;
        string Category;
    }
}
