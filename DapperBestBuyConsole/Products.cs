using System;
using System.Collections.Generic;
using System.Text;

namespace DapperBestBuyConsole
{
    public class Products
    {
        public int StockLevel { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }

        public int OnSale { get; set; }
        public int ProductID { get; set; }
    }
}
