using System;

namespace LA.CaseStudy.Services.Product.Domain.Models
{
    public class ProductEntity
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Code { get; protected set; }

        protected ProductEntity()
        { 
        }

        public ProductEntity(Guid id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }
    }
}