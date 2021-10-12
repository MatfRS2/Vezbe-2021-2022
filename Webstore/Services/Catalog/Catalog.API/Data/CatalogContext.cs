using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("CatalogDB");

            Products = database.GetCollection<Product>("Products");
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
