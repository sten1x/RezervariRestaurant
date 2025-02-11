using RezervariRestaurant.Models.DBObjects;

namespace RezervariRestaurant.Models.ViewModels
{
    public class ManageOrderViewModel
    {
        public OrderModel? Order;
        public List<MenuItemModel> MenuItems;

        public ManageOrderViewModel()
        {
            MenuItems = new List<MenuItemModel>(); // Evităm null reference exception
        }
    }
}
