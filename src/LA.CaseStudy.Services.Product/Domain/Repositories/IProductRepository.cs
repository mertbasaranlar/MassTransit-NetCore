using System.Threading.Tasks;
using LA.CaseStudy.Services.Product.Domain.Models;

namespace LA.CaseStudy.Services.Product.Domain.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(ProductEntity product);
    }
}