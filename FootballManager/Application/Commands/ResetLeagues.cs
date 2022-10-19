using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class ResetLeagues : IRequest<List<League>>
    {
    }
}
