using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RezervariRestaurant.Models;

[Authorize]
public class ReservationController : Controller
{
    private readonly ReservationRepository _reservationRepository;
    private readonly OrderRepository _orderRepository;
    private readonly OrderDetailRepository _orderDetailRepository;
    private readonly MenuRepository _menuRepository;

    public ReservationController(ReservationRepository reservationRepository, OrderRepository orderRepository, OrderDetailRepository orderDetailRepository, MenuRepository menuRepository)
    {
        _reservationRepository = reservationRepository;
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _menuRepository = menuRepository;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        bool isAdmin = User.IsInRole("Admin");

        var reservations = isAdmin
            ? await _reservationRepository.GetAllReservationsAsync()
            : await _reservationRepository.GetReservationsByUserAsync(userId);

        if (isAdmin)
        {
            var userNames = await _reservationRepository.GetUserNamesAsync();
            ViewBag.UserNames = userNames;
        }

        return View(reservations);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(ReservationModel model)
    {
        model.IdUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(model.IdUser))
        {
            return Unauthorized();
        }

        var reservationId = await _reservationRepository.CreateReservationAsync(model);

        return RedirectToAction("Edit", new { id = reservationId });
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();

        var order = await _orderRepository.GetOrderByReservationIdAsync(id);
        var orderDetails = order != null
            ? await _orderDetailRepository.GetOrderDetailsByOrderIdAsync(order.IdOrder)
            : new List<OrderDetailModel>();

        var menuItems = await _menuRepository.GetAllMenuItemsAsync();

        var viewModel = new ReservationEditViewModel
        {
            Reservation = reservation,
            Order = order,
            OrderDetails = orderDetails,
            MenuItems = menuItems
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ReservationModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var existingReservation = await _reservationRepository.GetReservationByIdAsync(model.IdReservation);
        if (existingReservation == null)
        {
            return NotFound();
        }

        model.Status = "Pending";

        await _reservationRepository.UpdateReservationAsync(model);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _reservationRepository.DeleteReservationAsync(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddMenuItemToOrder(Guid reservationId, Guid menuItemId, int quantity)
    {
        if (quantity <= 0)
        {
            return BadRequest("Cantitatea trebuie să fie cel puțin 1.");
        }

        var order = await _orderRepository.GetOrderByReservationIdAsync(reservationId);
        if (order == null)
        {
            order = new OrderModel
            {
                IdOrder = Guid.NewGuid(),
                IdReservation = reservationId
            };
            await _orderRepository.CreateOrderAsync(order);
        }

        var orderDetail = new OrderDetailModel
        {
            IdOrderDetail = Guid.NewGuid(),
            IdOrder = order.IdOrder,
            IdMenuItem = menuItemId,
            Quantity = quantity
        };

        await _orderDetailRepository.AddOrderDetailAsync(orderDetail);

        return RedirectToAction("Edit", new { id = reservationId });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(Guid reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
        if (reservation == null) return NotFound();

        reservation.Status = "Approved";
        await _reservationRepository.UpdateReservationAsync(reservation);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CancelReservation(Guid reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
        if (reservation == null) return NotFound();

        reservation.Status = "Cancelled";
        await _reservationRepository.UpdateReservationAsync(reservation);

        return RedirectToAction("Index");
    }
}
