// GetOrderDetailsQueryHandler.cs
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDto>
{
    private readonly ApplicationDbContext _dbContext;

    public GetOrderDetailsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderDto> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Where(o => o.Id == request.OrderId)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerName = o.CustomerName,
                TotalAmount = o.TotalAmount,
                CreatedAt = o.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        return order;
    }
}
