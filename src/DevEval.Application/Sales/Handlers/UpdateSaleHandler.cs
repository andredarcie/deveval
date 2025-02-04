using AutoMapper;
using DevEval.Application.Sales.Commands;
using DevEval.Application.Sales.Dtos;
using DevEval.Application.Sales.Services;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Sales.Handlers
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, SaleDto>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISaleEventPublisher _eventPublisher;

        public UpdateSaleHandler(ISaleRepository repository, IMapper mapper, ISaleEventPublisher eventPublisher)
        {
            _repository = repository;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        public async Task<SaleDto> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var existingSale = await _repository.GetByIdAsync(request.Id);

            if (existingSale == null) throw new KeyNotFoundException($"Sale with ID {request.Id} not found.");

            _mapper.Map(request, existingSale);

            var updatedSale = await _repository.UpdateAsync(existingSale);

            await _eventPublisher.PublishSaleModifiedAsync(updatedSale);

            var result = _mapper.Map<SaleDto>(updatedSale);

            return result;
        }
    }
}
