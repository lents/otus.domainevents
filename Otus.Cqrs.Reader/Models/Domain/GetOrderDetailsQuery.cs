
using MediatR;

public class GetOrderDetailsQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }
    }

    // OrderDto.cs
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

