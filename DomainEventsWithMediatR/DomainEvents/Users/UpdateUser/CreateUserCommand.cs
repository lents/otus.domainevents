using MediatR;

namespace DomainEventsWithMediatR.DomainEvents.Users.UpdateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
