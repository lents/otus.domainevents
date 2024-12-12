using DomainEventsWithMediatR.Domain.Services;
using MediatR;

namespace DomainEventsWithMediatR.DomainEvents.Users.UpdateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly UserService _userService;

        public CreateUserHandler(UserService userService)
        {
            _userService = userService;
        }
        public Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userService.CreateUser(request.Name));
        }
    }
}
