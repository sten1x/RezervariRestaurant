using RezervariRestaurant.Models;

public class ReservationEditViewModel
{
    public ReservationModel Reservation { get; set; }
    public OrderModel Order { get; set; }
    public List<OrderDetailModel> OrderDetails { get; set; }
    public List<MenuItemModel> MenuItems { get; set; }

    public ReservationEditViewModel()
    {
        OrderDetails = new List<OrderDetailModel>();
        MenuItems = new List<MenuItemModel>();
    }
}