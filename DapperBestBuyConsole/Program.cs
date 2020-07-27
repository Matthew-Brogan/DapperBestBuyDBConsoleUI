using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Threading;

namespace DapperBestBuyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
           #endregion
            
            IDbConnection conn = new MySqlConnection(connString);
            #region Dep
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Hello user, Here are the current departments:");
            Console.WriteLine("Please press enter . . .");
            Console.ReadLine();
            var depos = repo.GetAllDepartments();
            Print(depos);
            
            Console.WriteLine("Do you want to add a department?");
            var userResponse = Console.ReadLine();

            if(userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your department?");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }
            #endregion

            DapperProductRepository prods = new DapperProductRepository(conn);
            Console.WriteLine("Lets take a look at our products: Press enter....");
            Console.ReadLine();

            var stuff = prods.GetAllProducts();
            PrintProds(stuff);
            #region AddProduct
            Console.WriteLine("Would you like to add a product?");
            var answer = Console.ReadLine();

            if(answer.ToLower() == "yes")
            {
                Console.WriteLine("Please enter product details:");
                Console.WriteLine("Name:");
                var name = Console.ReadLine();
                Console.WriteLine("Stock level:");
                var stock = int.Parse(Console.ReadLine());
                Console.WriteLine("Price:");
                var price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Category ID:");
                var id = int.Parse(Console.ReadLine());
                Console.WriteLine("If product is on sale please enter 1, otherwise 0");
                var sale = int.Parse(Console.ReadLine());

                prods.InsertProducts(name, stock, id, price, sale);
                PrintProds(prods.GetAllProducts());


            }
            #endregion


            #region DeleteProduct
            Console.WriteLine("Would you like to delete a product?");
            var delete = Console.ReadLine();
            if(delete == "yes")
            {
                Console.WriteLine("Please select a product to delete based on id number:");
                Console.WriteLine("Press enter to view products......");
                Console.ReadLine();
                PrintProds(prods.GetAllProducts());
                var chopped = int.Parse(Console.ReadLine());
                prods.DeleteProducts(chopped);
                PrintProds(prods.GetAllProducts());

            }
            
            #endregion


            Console.WriteLine("Would you like to update a product?");
            var userSays = Console.ReadLine();
            if(userSays == "yes")
            {
                Console.WriteLine();
                Console.WriteLine("Please select the product by ID:");
                var selectedProd = int.Parse(Console.ReadLine());
                var prodToUpdate = prods.GetProducts(selectedProd);

                Console.WriteLine("Please enter the products new name:");
                prodToUpdate.Name = Console.ReadLine();
                Console.WriteLine("Please enter the new price:");
                prodToUpdate.Price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Is this product on sale?\n : 0= no, 1= yes ");
                prodToUpdate.OnSale = int.Parse(Console.ReadLine());

                prods.UpdateProducts(prodToUpdate);


                
            }


            Console.WriteLine("Have a nice day!");
        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentId} Name: {depo.Name}");

            }
        }
        private static void PrintProds(IEnumerable<Products> prods)
        {
            foreach(var pro in prods)
            {
                Console.WriteLine($"Product ID: {pro.ProductID} \n Category Id: {pro.CategoryID}\n Name: {pro.Name} \nStock: {pro.StockLevel}\n Sale: {pro.OnSale}\n\n");
                Thread.Sleep(20);
            }
        }
        
    }
}
