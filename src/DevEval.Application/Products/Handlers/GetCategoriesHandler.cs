using DevEval.Application.Products.Queries;
using DevEval.Domain.Repositories;
using MediatR;

namespace DevEval.Application.Products.Handlers
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<string>>
    {
        private readonly IProductRepository _repository;

        public GetCategoriesHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetCategoriesAsync();
        }
    }
}
