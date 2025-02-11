using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RezervariRestaurant.Data;
using RezervariRestaurant.Models.DBObjects;
using RezervariRestaurant.Models;

public class OrderDetailRepository
{
    private readonly ApplicationDbContext _context;

    public OrderDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddOrderDetailAsync(OrderDetailModel model)
    {
        var orderDetail = new OrderDetail
        {
            IdOrderDetail = model.IdOrderDetail,
            IdOrder = model.IdOrder,
            IdMenuItem = model.IdMenuItem,
            Quantity = model.Quantity
        };

        await _context.OrderDetails.AddAsync(orderDetail);
        await _context.SaveChangesAsync();
    }

    public async Task<List<OrderDetailModel>> GetOrderDetailsByOrderIdAsync(Guid orderId)
    {
        var orderDetails = await _context.OrderDetails
            .Where(od => od.IdOrder == orderId)
            .ToListAsync();

        return orderDetails.Select(od => new OrderDetailModel
        {
            IdOrderDetail = od.IdOrderDetail,
            IdOrder = od.IdOrder,
            IdMenuItem = od.IdMenuItem,
            Quantity = od.Quantity
        }).ToList();
    }

}
