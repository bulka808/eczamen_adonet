using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eczamen_adonet
{
    internal class Book
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Publisher { get; set; }
            public int Pages { get; set; }
            public string Genre { get; set; }
            public int Year { get; set; }
            public decimal Cost { get; set; }
            public decimal SalePrice { get; set; }
            public bool IsSequel { get; set; }
            public string SequelOf { get; set; }
    }
}
