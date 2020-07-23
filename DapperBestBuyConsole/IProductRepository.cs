using System;
using System.Collections.Generic;
using System.Text;

namespace DapperBestBuyConsole
{
    public interface IProductRepository
    {
        IEnumerable<Products> GetAllProducts();
        void InsertProducts(string name, int stock, int catid, decimal price, int onsale);
    }
}
