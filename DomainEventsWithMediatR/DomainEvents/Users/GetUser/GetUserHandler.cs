using DomainEventsWithMediatR.Domain.Models;
using DomainEventsWithMediatR.Domain.Services;
using MediatR;

namespace DomainEventsWithMediatR.DomainEvents.Users.GetUser
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, User?>
    {
        private readonly UserService _userService;

        public GetUserHandler(UserService userService)
        {
            _userService = userService;
        }

        public Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userService.GetUser(request.Id));
        }
    }
}
