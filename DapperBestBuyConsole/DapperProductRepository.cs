using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace DapperBestBuyConsole
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _proconn;
        public DapperProductRepository(IDbConnection dbConnection)
        {
            _proconn = dbConnection;
        }
        public IEnumerable<Products> GetAllProducts()
        {
            var prods = _proconn.Query<Products>("Select * From products;");
            return prods;
        }

        public void InsertProducts(string name, int stock, int catid, decimal price, int onsale)
        {
            _proconn.Execute("INSERT INTO Products (Name, StockLevel, CategoryID, Price, OnSale) VALUES (@Name,@Price,@Stock,@Catid,@Onsale)",
                new {Name = name, Price = price, Stock = stock, Catid = catid, Onsale = onsale });
        }
    }
}
