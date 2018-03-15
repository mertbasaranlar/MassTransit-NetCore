using System;
using System.Threading.Tasks;
using LA.CaseStudy.Common.Commands;
using LA.CaseStudy.Services.Product.Services;
using MassTransit;

namespace LA.CaseStudy.Services.Product.Consumers
{
    public class AddProductConsumer : IConsumer<AddProductCommand>
    {
        private readonly IProductService _productService;

        public AddProductConsumer(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Consume(ConsumeContext<AddProductCommand> context)
        {
            Console.WriteLine(context.Message.Name);
            Console.WriteLine(context.Message.Code);

            await _productService.AddAsync(Guid.NewGuid(), context.Message.Name, context.Message.Code);
        }
    }
}