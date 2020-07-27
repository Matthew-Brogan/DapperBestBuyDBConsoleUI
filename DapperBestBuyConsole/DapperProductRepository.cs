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
            _proconn.Execute("INSERT INTO Products (Name, StockLevel, products.CategoryID, Price, OnSale) VALUES (@Name,@Price,@Stock,@Catid,@Onsale)",
                new {Name = name, Price = price, Stock = stock, Catid = catid, Onsale = onsale });
        }
        public void DeleteProducts(int id)
        {
            _proconn.Execute($"DELETE FROM Products WhERE Products.ProductID  = (@goneID);",
                new { goneID = id });
            _proconn.Execute("DELETE FROM Sales WHere sales.ProductID = (@goneID1);",
                new { goneID1 = id });
            _proconn.Execute("DELETE FROM reviews where reviews.ProductID = (@goneID2);",
                new { goneID2 = id });
              
        }

        public void UpdateProducts(Products product)
        {
            _proconn.Execute($"Update Products Set Name = (@name), Price = (@price) where Products.ProductID = (@id1) ",
                new { name = product.Name, price = product.Price, id1 = product.ProductID } );
        }
        public Products  GetProducts( int id)
        {
           return  _proconn.QuerySingle<Products>("Select * from Products where products.Productid = (@search)",
                new { search = id });
            
        }
    }
}
