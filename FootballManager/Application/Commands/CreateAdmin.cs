using MediatR;

namespace Application.Commands
{
    public class CreateAdmin : IRequest<Object>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }
        public string Image { get; set; }
    }
}
