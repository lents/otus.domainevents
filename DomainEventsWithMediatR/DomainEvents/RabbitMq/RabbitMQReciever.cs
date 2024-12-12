using MediatR;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Xml.Serialization;

namespace DomainEventsWithMediatR.DomainEvents.RabbitMq
{
    public class RabbitMQReciever
    {

        private readonly IEnumerable<IRabbitMessageMapper> mappers;
        IMediator mediator;
        public RabbitMQReciever(IEnumerable<IRabbitMessageMapper> mappers, IMediator mediator)
        {
            this.mappers = mappers;
            this.mediator = mediator;
        }

        public void RecieveMessage(string message, string routingKey)
        {
            var m = this.mappers.FirstOrDefault(x => x.RoutingKey == routingKey);
            var command = m.Transform(message);
            this.mediator.Send(command);
        }
    }




    public interface IRabbitMessageMapper
    {
        public string RoutingKey { get; }

        public object Transform(string message);
    }



    public class UserMessage
    {
        public string Name { get; set; }
    }

    public class UserMessageTransformer : IRabbitMessageMapper
    {
        public string RoutingKey => "UserMessage";

        public object Transform(string message)
        {
            return JsonSerializer.Deserialize<UserMessage>(message)!;
        }

    }






    public class LocationMessage
    {
        public string Address { get; set; }
    }

    public class LocationTransformer : IRabbitMessageMapper
    {
        public string RoutingKey => "LocationMessage";

        public Object Transform(string message)
        {
            var xml = new XmlSerializer(typeof(LocationMessage));

            using (var reader = new StringReader(message))
            {
                return (LocationMessage)xml.Deserialize(reader);
            }
        }
    }

}
