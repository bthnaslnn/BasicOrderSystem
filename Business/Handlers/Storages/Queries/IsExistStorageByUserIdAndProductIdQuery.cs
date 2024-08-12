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

namespace Business.Handlers.Storages.Queries
{
    public class IsExistStorageByUserIdAndProductIdQuery : IRequest<IDataResult<bool>>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public class IsExistStorageByUserIdAndProductIdQueryHandler : IRequestHandler<IsExistStorageByUserIdAndProductIdQuery, IDataResult<bool>>
        {
            private readonly IStorageRepository _storageRepository;
            private readonly IMediator _mediator;

            public IsExistStorageByUserIdAndProductIdQueryHandler(IStorageRepository storageRepository, IMediator mediator)
            {
                _storageRepository = storageRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<bool>> Handle(IsExistStorageByUserIdAndProductIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<bool>( await _storageRepository.IsExistStorageByUserIdAndProductId(request.UserId, request.ProductId));
            }
        }
    }
}
