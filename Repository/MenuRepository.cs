using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RezervariRestaurant.Models.DBObjects;
using RezervariRestaurant.Models;
using RezervariRestaurant.Data;

public class MenuRepository
{
    private readonly ApplicationDbContext _context;

    public MenuRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // 🔹 Obține toate produsele din meniu
    public async Task<List<MenuItemModel>> GetAllMenuItemsAsync()
    {
        return await _context.MenuItems
            .Select(m => MapDbObjectToModel(m))
            .ToListAsync();
    }

    // 🔹 Obține un produs din meniu după ID
    public async Task<MenuItemModel> GetMenuItemByIdAsync(Guid id)
    {
        var menuItem = await _context.MenuItems.FirstOrDefaultAsync(m => m.IdMenuItem == id);
        return MapDbObjectToModel(menuItem);
    }

    // 🔹 Adaugă un produs nou în meniu (Admin)
    public async Task CreateMenuItemAsync(MenuItemModel menuItemModel)
    {
        var newMenuItem = MapModelToDbObject(menuItemModel);
        newMenuItem.IdMenuItem = Guid.NewGuid();
        await _context.MenuItems.AddAsync(newMenuItem);
        await _context.SaveChangesAsync();
    }

    // 🔹 Actualizează un produs din meniu (Admin)
    public async Task UpdateMenuItemAsync(MenuItemModel menuItemModel)
    {
        var menuItem = await _context.MenuItems.FirstOrDefaultAsync(m => m.IdMenuItem == menuItemModel.IdMenuItem);
        if (menuItem != null)
        {
            menuItem.Name = menuItemModel.Name;
            menuItem.Description = menuItemModel.Description;
            menuItem.Price = menuItemModel.Price;
            await _context.SaveChangesAsync();
        }
    }

    // 🔹 Șterge un produs din meniu (Admin)
    public async Task DeleteMenuItemAsync(Guid id)
    {
        var menuItem = await _context.MenuItems.FirstOrDefaultAsync(m => m.IdMenuItem == id);
        if (menuItem != null)
        {
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
        }
    }

    private static MenuItemModel MapDbObjectToModel(MenuItem dbObject)
    {
        if (dbObject == null) return null;
        return new MenuItemModel
        {
            IdMenuItem = dbObject.IdMenuItem,
            Name = dbObject.Name,
            Description = dbObject.Description,
            Price = dbObject.Price
        };
    }

    private static MenuItem MapModelToDbObject(MenuItemModel model)
    {
        if (model == null) return null;
        return new MenuItem
        {
            IdMenuItem = model.IdMenuItem,
            Name = model.Name,
            Description = model.Description,
            Price = model.Price
        };
    }
}
