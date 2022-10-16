using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetTeamNextFixture : IRequest<Fixture>
    {
        public long TeamId { get; set; }
    }
}
