using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCDemo.Models
{
    public class ModuleModels
    {
        [HttpGet]
        public List<Product> GetProducts()
        {
            List<Product> listProduct = new List<Product>();
            Product product;

            for (int i = 0; i < 100; i++)
            {
                product = new Product();

                product.ProductId = i + 1;
                product.ProductName = "Product Name " + (i + 1).ToString();
                product.ProductPrice = ((i + 1) * 1000).ToString();
                product.ProductQuantity = ((i +1)  * 10).ToString();

                listProduct.Add(product);
            }

            return listProduct;
        }
    }
}