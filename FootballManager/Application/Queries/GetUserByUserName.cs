using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetUserByUserName : IRequest<User>
    {
        public string UserName { get; set; }
    }
}
