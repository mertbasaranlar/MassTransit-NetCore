using System;
using System.Threading.Tasks;

namespace LA.CaseStudy.Services.Product.Services
{
    public interface IProductService
    {
        Task AddAsync(Guid id, string name, string code);
    }
}