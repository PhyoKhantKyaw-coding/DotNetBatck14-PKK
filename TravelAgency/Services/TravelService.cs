using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;

namespace TravelAgency.Services;

public class TravelService
{
    private readonly AppDbContext _context;
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
    }

    //Bookings
    public async Task<List<BookingModel>> GetBookings()
    {
        return await _context.Booking.AsNoTracking().ToListAsync();
    }
    public async Task<ResponseModel> CreateBooking(BookingRequestModel booking)
    {
        var user = await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == booking.UserId);
        if (user == null)
        {
            return new ResponseModel { Success = "false", Message = "User not found" , Data=null};
        }
        var travelPackage = await _context.TravelPackage.AsNoTracking().FirstOrDefaultAsync(tp => tp.Id == booking.TravelPackageId);
        if (travelPackage == null)
        {
            return new ResponseModel { Success = "false", Message = "Travel package not found", Data = null };
        }
        var Travelerlst = booking.Travelers;

        var Book = new BookingModel
        {
            Id = Guid.NewGuid().ToString(),
            UserId = booking.UserId,
            TravelPackageId = booking.TravelPackageId,
            NumberOfTravelers = Travelerlst.Count,
            TotalPrice = travelPackage.Price * Travelerlst.Count,
            BookingDate = DateTime.Now,
            Status = "Pending"
        };
        _context.Booking.Add(Book);
        var result  = await _context.SaveChangesAsync();
        foreach (var traveler in Travelerlst)
        {
            var Traveler = new TravelerModel
            {
                Id = Guid.NewGuid().ToString(),
                BookingId = Book.Id,
                Name = traveler.Name,
                Age = traveler.Age,
                Gender = traveler.Gender
            };
            _context.Traveler.Add(Traveler);
            var res = await _context.SaveChangesAsync();
            if (res != 1)
            {
                return new ResponseModel { Success = "false", Message = "Traveler addition failed", Data = null };
            }
        }
        return result == 1 ?
            new ResponseModel { Success = "true", Message = "Booking created successfully", Data = Book } :
            new ResponseModel { Success = "false", Message = "Booking creation failed", Data = null };
    }
    public async Task<BookingModel> GetBookingbyid(string id)
    {
        var model = await _context.Booking.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        return model!;
    }

    public async Task<BookingModel> UpdateBookingStatus(string id, string status)
    {
        var booking = await _context.Booking.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null)
        {
            return null!;
        }
        booking.Status = status;
        _context.Booking.Update(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    //Traveler
    public async Task<List<TravelerModel>> GetTravelersbyBookingid(string id)
    {
        return await _context.Traveler.Where(t => t.BookingId == id).AsNoTracking().ToListAsync();
    }

    public async Task<ResponseModel> AddTraveler(TravelerModel model)
    {
        var booking = await _context.Booking.AsNoTracking().FirstOrDefaultAsync(b => b.Id == model.BookingId);
        if (booking == null)
        {
            return new ResponseModel { Success = "false", Message = "Booking not found", Data = null };
        }
        var traveler = new TravelerModel
        {
            Id = Guid.NewGuid().ToString(),
            BookingId = model.BookingId,
            Name = model.Name,
            Age = model.Age,
            Gender = model.Gender
        };
        var travepackage = await _context.TravelPackage.AsNoTracking().FirstOrDefaultAsync(tp => tp.Id == booking.TravelPackageId);
        _context.Traveler.Add(traveler);
        booking.NumberOfTravelers++;
        booking.TotalPrice += travepackage.Price;
        _context.Booking.Update(booking);
        var result = await _context.SaveChangesAsync();
        return result == 2 ?
            new ResponseModel { Success = "true", Message = "Traveler added successfully", Data = traveler } :
            new ResponseModel { Success = "false", Message = "Traveler addition failed", Data = null };
    }
    public async Task<ResponseModel> RemoveTraveler(string id)
    {
        var traveler = await _context.Traveler.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        if (traveler == null)
        {
            return new ResponseModel { Success = "false", Message = "Traveler not found", Data = null };
        }
        _context.Traveler.Remove(traveler);
        BookingModel booking = await _context.Booking.AsNoTracking().FirstOrDefaultAsync(b => b.Id == traveler.BookingId);
        booking.NumberOfTravelers--;
        var travelPackage = await _context.TravelPackage.AsNoTracking().FirstOrDefaultAsync(tp => tp.Id == booking.TravelPackageId);
        booking.TotalPrice -= travelPackage.Price;
        _context.Booking.Update(booking);
        var result = await _context.SaveChangesAsync();
        return result == 2 ?
            new ResponseModel { Success = "true", Message = "Traveler removed successfully", Data = booking } :
            new ResponseModel { Success = "false", Message = "Traveler removal failed", Data = null };
    }

    //TravelPackage
    public async Task<List<TravelPackageModel>> GetTravelPackages()
    {
        return await _context.TravelPackage.AsNoTracking().ToListAsync();
    }
    {
            await photo.CopyToAsync(fileStream);
        }

        photoPath = "/images/" + uniqueFileName;

        var tp = new TravelPackageModel
        {
            Id = Guid.NewGuid().ToString(),
            Title = model.Title,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Inclusions = model.Inclusions,
            CancellationPolicy = model.CancellationPolicy,
            Description = model.Description,
            Price = model.Price,
            Destination = model.Destination,
        };

        _context.TravelPackage.Add(tp);
        var result = await _context.SaveChangesAsync();
    }
    public async Task<ResponseModel> ActivateTravelPackage(String id)
    {
        var package = await _context.TravelPackage.FirstOrDefaultAsync(tp => tp.Id == id);
        if (package == null)
        {
            return new ResponseModel { Success = "false", Message = "Travel package not found", Data = null };
        }

        package.Status = "Active";
        _context.TravelPackage.Update(package);
        var result = await _context.SaveChangesAsync();

        return result == 1
            ? new ResponseModel { Success = "true", Message = "Travel package activated successfully", Data = package }
            : new ResponseModel { Success = "false", Message = "Failed to activate travel package", Data = null };
    }

    public async Task<ResponseModel> DeactivateTravelPackage(String id)
    {
        var package = await _context.TravelPackage.FirstOrDefaultAsync(tp => tp.Id == id);
        if (package == null)
        {
            return new ResponseModel { Success = "false", Message = "Travel package not found", Data = null };
        }

        package.Status = "Inactive";
        _context.TravelPackage.Update(package);
        var result = await _context.SaveChangesAsync();

        return result == 1
            ? new ResponseModel { Success = "true", Message = "Travel package deactivated successfully", Data = package }
            : new ResponseModel { Success = "false", Message = "Failed to deactivate travel package", Data = null };
    }
    //Payment
    public async Task<ResponseModel> CreatePayment(PaymentRequestModel model)
    {
        var booking = await _context.Booking.AsNoTracking().FirstOrDefaultAsync(b => b.Id == model.BookingId);
        if (booking == null)
        {
            return new ResponseModel { Success = "false", Message = "Booking not found", Data = null };
        }
        if(booking.TotalPrice != model.Amount)
        {
            return new ResponseModel { Success = "false", Message = "Amount mismatch", Data = null };
        }
        if (booking.Status == "Paid")
        {
            return new ResponseModel { Success = "false", Message = "Booking already paid", Data = null };
        }
        var payment = new PaymentModel
        {
            Id = Guid.NewGuid().ToString(),
            BookingId = model.BookingId,
            UserId = model.UserId,
            PaymentDate = DateTime.Now,
            Amount = model.Amount,
            PaymentStatus = "Success",
        };

        _context.Payment.Add(payment);
        booking.Status = "Paid";
        _context.Booking.Update(booking);
        var result = await _context.SaveChangesAsync();
        return result == 2 ?
            new ResponseModel { Success = "true", Message = "Payment added successfully", Data = payment } :
            new ResponseModel { Success = "false", Message = "Payment addition failed", Data = null };
    }
    public async Task<PaymentModel> GetPaymentbyUserid(string userid)
    {
        var model = await _context.Payment.AsNoTracking().FirstOrDefaultAsync(p => p.UserId == userid);
        return model!;
    }
    public async Task<PaymentModel> GetPaymentbyBookingid(string bookingid)
    {
        var model = await _context.Payment.AsNoTracking().FirstOrDefaultAsync(p => p.BookingId == bookingid);
        return model!;
    }
    public async Task<List<PaymentModel>> GetPayments()
    {
        return await _context.Payment.AsNoTracking().ToListAsync();
    }
    //Users
    public async Task<List<UserModel>> GetUsers()
    {
        return await _context.User.AsNoTracking().ToListAsync();
    }
    public async Task<ResponseModel> CreateUser(UserModel user)
    {
        var userExist = await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == user.Id);
        if (userExist != null)
        {
            return new ResponseModel { Success = "false", Message = "User already exists", Data = null };
        }
        _context.User.Add(user);
        var result = await _context.SaveChangesAsync();
        return result == 1 ?
        new ResponseModel { Success = "true", Message = "User created successfully", Data = user } :
        new ResponseModel { Success = "false", Message = "User creation failed", Data = null };
    }
    public async Task<UserModel> GetUserbyid(string id)
    {
        var model = await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        return model!;
    }
}
