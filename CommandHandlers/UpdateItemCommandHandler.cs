using AutoMapper;
using BasketService.API.Commands;
using MassTransit.Riders;
using MediatR;

namespace BasketService.API.CommandHandlers
{
   
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, int>
    {
        private readonly IMapper _mapper;

        public UpdateItemCommandHandler(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            //var orderEntity = _mapper.Map<Order>(request);
            //var newOrder = await _orderRepository.AddAsync(orderEntity);

            //_logger.LogInformation($"Order {newOrder.Id} is successfully created.");

            return 1;
            //return newOrder.Id;
        }

    }
}
