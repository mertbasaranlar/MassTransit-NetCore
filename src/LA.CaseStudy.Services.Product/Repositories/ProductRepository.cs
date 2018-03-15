using System;
using System.Threading.Tasks;
using LA.CaseStudy.Services.Product.Domain.Models;
using LA.CaseStudy.Services.Product.Domain.Repositories;
using MongoDB.Driver;

namespace LA.CaseStudy.Services.Product.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase _database;

        public ProductRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(ProductEntity product) => await Collection.InsertOneAsync(product);

        private IMongoCollection<ProductEntity> Collection => _database.GetCollection<ProductEntity>("Product");
    }
}