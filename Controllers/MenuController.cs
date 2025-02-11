using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RezervariRestaurant.Models;
using System;
using System.Threading.Tasks;

public class MenuController : Controller
{
    private readonly MenuRepository _menuRepository;

    public MenuController(MenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<IActionResult> Index()
    {
        var menuItems = await _menuRepository.GetAllMenuItemsAsync();
        return View(menuItems);
    }

    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MenuItemModel model)
    {
        if (ModelState.IsValid)
        {
            await _menuRepository.CreateMenuItemAsync(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var menuItem = await _menuRepository.GetMenuItemByIdAsync(id);
        if (menuItem == null) return NotFound();

        return View(menuItem);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, MenuItemModel model)
    {
        if (id != model.IdMenuItem) return BadRequest();

        if (ModelState.IsValid)
        {
            await _menuRepository.UpdateMenuItemAsync(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var menuItem = await _menuRepository.GetMenuItemByIdAsync(id);
        if (menuItem == null) return NotFound();

        return View(menuItem);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _menuRepository.DeleteMenuItemAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
