using MediatR;
using Otus.CqrsUpdater.Models.Domain;
using Otus.CqrsUpdater.Services;

namespace Otus.CqrsUpdater.Handlers
{
    public record SaveUserCommand(UserEvent UserEvent) : IRequest;
    public record SaveUserHandler(IntegrationService IntegrationService) : IRequestHandler<SaveUserCommand>
    {
        public Task Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            return IntegrationService.SendAsync(request.UserEvent);
        }
    }
}
