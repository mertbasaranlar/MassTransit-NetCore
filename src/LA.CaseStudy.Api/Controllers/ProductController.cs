using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LA.CaseStudy.Common.Commands;
using LA.CaseStudy.Common.MassTransit;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LA.CaseStudy.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IBusControl _bus;

        private readonly RabbitMqOptions _rabbitMqOptions;

        public ProductController(IBusControl bus, IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _bus = bus;
            _rabbitMqOptions = rabbitMqOptions.Value;
        }

        [HttpPost]
        public async Task Post([FromBody]AddProductCommand command)
        {
            var uri = $"{_rabbitMqOptions.Uri}/{QueueHelper.GetQueueName<AddProductCommand>()}";

            var endpoint = await _bus.GetSendEndpoint(new Uri(uri));

            await endpoint.Send(command);
        }
    }
}
