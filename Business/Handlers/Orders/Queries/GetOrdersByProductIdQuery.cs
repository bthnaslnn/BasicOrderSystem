using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Orders.Queries
{
    public class GetOrdersByProductIdQuery : IRequest<IDataResult<IEnumerable<Order>>>
    {
        public int Id { get; set; }

        public class GetOrdersByProductIdQueryHandler : IRequestHandler<GetOrdersByProductIdQuery, IDataResult<IEnumerable<Order>>>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMediator _mediator;

            public GetOrdersByProductIdQueryHandler(IOrderRepository orderRepository, IMediator mediator)
            {
                _orderRepository = orderRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Order>>> Handle(GetOrdersByProductIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Order>>(await _orderRepository.GetListByProductId(request.Id));
            }
        }
    }
}
