using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RezervariRestaurant.Models.DBObjects;
using RezervariRestaurant.Models;
using RezervariRestaurant.Data;

public class ReservationRepository
{
    private readonly ApplicationDbContext _context;

    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReservationModel>> GetAllReservationsAsync()
    {
        return await _context.Reservations
            .Select(r => MapDbObjectToModel(r))
            .ToListAsync();
    }

    public async Task<ReservationModel> GetReservationByIdAsync(Guid id)
    {
        var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.IdReservation == id);
        return MapDbObjectToModel(reservation);
    }

    public async Task<List<ReservationModel>> GetReservationsByUserAsync(string userId)
    {
        return await _context.Reservations
            .Where(r => r.IdUser == userId)
            .Select(r => MapDbObjectToModel(r))
            .ToListAsync();
    }

    public async Task<Guid> CreateReservationAsync(ReservationModel model)
    {
        var reservation = new Reservation
        {
            IdReservation = Guid.NewGuid(),
            IdUser = model.IdUser,
            ReservationDate = model.ReservationDate,
            Guests = model.Guests,
            Status = "Pending"
        };

        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();

        return reservation.IdReservation; // 🔹 Trebuie să returnăm ID-ul corect!
    }

    public async Task UpdateReservationAsync(ReservationModel reservationModel)
    {
        var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.IdReservation == reservationModel.IdReservation);
        if (reservation != null)
        {
            reservation.ReservationDate = reservationModel.ReservationDate;
            reservation.Guests = reservationModel.Guests;
            reservation.Status = reservationModel.Status;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteReservationAsync(Guid id)
    {
        var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.IdReservation == id);
        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
    }

    private static ReservationModel MapDbObjectToModel(Reservation dbObject)
    {
        if (dbObject == null) return null;
        return new ReservationModel
        {
            IdReservation = dbObject.IdReservation,
            IdUser = dbObject.IdUser,
            ReservationDate = dbObject.ReservationDate,
            Guests = dbObject.Guests,
            Status = dbObject.Status
        };
    }

    private static Reservation MapModelToDbObject(ReservationModel model)
    {
        if (model == null) return null;
        return new Reservation
        {
            IdReservation = model.IdReservation,
            IdUser = model.IdUser,
            ReservationDate = model.ReservationDate,
            Guests = model.Guests,
            Status = model.Status
        };
    }
    public async Task<Dictionary<string, string>> GetUserNamesAsync()
    {
        return await _context.Users
            .ToDictionaryAsync(u => u.Id, u => u.UserName);
    }

}
