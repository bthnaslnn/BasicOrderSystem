using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
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

namespace Business.Handlers.Customers.Queries
{
    public class GetCustomersByUserIdQuery : IRequest<IDataResult<IEnumerable<Customer>>>
    {
        public int Id { get; set; }
        public class GetCustomersByUserIdQueryHandler : IRequestHandler<GetCustomersByUserIdQuery, IDataResult<IEnumerable<Customer>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetCustomersByUserIdQueryHandler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)] // Performans olarak en öncelikli çalılşması için 1-5 e kadar numaralandoırıyoruz. 5 en çok performanslı çalışsın demek.
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Customer>>> Handle(GetCustomersByUserIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Customer>>(await _customerRepository.GetListByUserId(request.Id)); //isdeleted false önemli
                // kullanıcıya silinmiş verilerin gözükmesi mantıksız.
            }
        }
    }
}
