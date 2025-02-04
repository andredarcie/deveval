using MediatR;

namespace DevEval.Application.Products.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<string>>
    {
    }
}
