using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RezervariRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Authorize]
public class ReviewController : Controller
{
    private readonly ReviewRepository _reviewRepository;

    public ReviewController(ReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        bool isAdmin = User.IsInRole("Admin");

        var reviews = isAdmin
            ? await _reviewRepository.GetAllReviewsAsync()
            : await _reviewRepository.GetReviewsByUserAsync(userId);

        if (isAdmin)
        {
            var userNames = await _reviewRepository.GetUserNamesAsync();
            ViewBag.UserNames = userNames;
        }

        return View(reviews);
    }


    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(ReviewModel model)
    {
        model.IdUser = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        await _reviewRepository.CreateReviewAsync(model);
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _reviewRepository.DeleteReviewAsync(id);
        return RedirectToAction("Index");
    }
}
