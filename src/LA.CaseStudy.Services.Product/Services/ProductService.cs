using System;
using System.Threading.Tasks;
using LA.CaseStudy.Services.Product.Domain.Models;
using LA.CaseStudy.Services.Product.Domain.Repositories;

namespace LA.CaseStudy.Services.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task AddAsync(Guid id, string name, string code)
        {
            var product = new ProductEntity(id, name, code);

            await _productRepository.AddAsync(product);
        }
    }
}