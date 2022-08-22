using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllManagers : IRequest<List<Manager>>
    {
    }
}
