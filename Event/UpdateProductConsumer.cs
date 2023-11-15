using AutoMapper;
using BasketService.API.Commands;
using BasketService.API.Models;
using MassTransit;
using MediatR;

namespace BasketService.API.Event
{
    public class UpdateProductConsumer: IConsumer<Item>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateProductConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<Item> context)
        {
            var command = _mapper.Map<UpdateItemCommand>(context.Message);
            var result = await _mediator.Send(command);

        }
    }
}
