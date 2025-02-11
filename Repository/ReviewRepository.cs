using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RezervariRestaurant.Models.DBObjects;
using RezervariRestaurant.Models;
using RezervariRestaurant.Data;

public class ReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReviewModel>> GetAllReviewsAsync()
    {
        return await _context.Reviews
            .Select(r => MapDbObjectToModel(r))
            .ToListAsync();
    }

    public async Task<ReviewModel> GetReviewByIdAsync(Guid id)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.IdReview == id);
        return MapDbObjectToModel(review);
    }

    public async Task<List<ReviewModel>> GetReviewsByUserAsync(string userId)
    {
        return await _context.Reviews
            .Where(r => r.IdUser == userId)
            .Select(r => MapDbObjectToModel(r))
            .ToListAsync();
    }

    public async Task CreateReviewAsync(ReviewModel reviewModel)
    {
        var newReview = MapModelToDbObject(reviewModel);
        newReview.IdReview = Guid.NewGuid();
        await _context.Reviews.AddAsync(newReview);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReviewAsync(ReviewModel reviewModel)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.IdReview == reviewModel.IdReview);
        if (review != null)
        {
            review.Text = reviewModel.Text;
            review.Rating = reviewModel.Rating;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteReviewAsync(Guid id)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.IdReview == id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }

    private static ReviewModel MapDbObjectToModel(Review dbObject)
    {
        if (dbObject == null) return null;
        return new ReviewModel
        {
            IdReview = dbObject.IdReview,
            IdUser = dbObject.IdUser,
            Text = dbObject.Text,
            Rating = dbObject.Rating
        };
    }

    private static Review MapModelToDbObject(ReviewModel model)
    {
        if (model == null) return null;
        return new Review
        {
            IdReview = model.IdReview,
            IdUser = model.IdUser,
            Text = model.Text,
            Rating = model.Rating
        };
    }

    public async Task<Dictionary<string, string>> GetUserNamesAsync()
    {
        return await _context.Users
            .ToDictionaryAsync(u => u.Id, u => u.UserName);
    }

}
