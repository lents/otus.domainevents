using MediatR;
using DomainEventsWithMediatR.Domain.Models;
using System.Net.Sockets;
namespace DomainEventsWithMediatR.DomainEvents.Users.GetUser
{
    public class GetUserQuery : IRequest<User?>
    {
        public GetUserQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
