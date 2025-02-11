using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RezervariRestaurant.Data;
using RezervariRestaurant.Models.DBObjects;
using RezervariRestaurant.Models;

public class OrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderModel?> GetOrderByReservationIdAsync(Guid reservationId)
    {
        var dbOrder = await _context.Orders
            .FirstOrDefaultAsync(o => o.IdReservation == reservationId);

        return dbOrder != null ? MapDbObjectToModel(dbOrder) : null;
    }

    public async Task CreateOrderAsync(OrderModel model)
    {
        var order = new Order
        {
            IdOrder = model.IdOrder,
            IdReservation = model.IdReservation
        };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<List<OrderModel>> GetAllOrdersAsync()
    {
        var orders = await _context.Orders.ToListAsync();
        return orders.Select(MapDbObjectToModel).ToList();
    }

    private static OrderModel MapDbObjectToModel(Order dbObject)
    {
        return new OrderModel
        {
            IdOrder = dbObject.IdOrder,
            IdReservation = dbObject.IdReservation
        };
    }
}
