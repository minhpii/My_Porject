using Microsoft.EntityFrameworkCore;
using My_Pro.Data;
using My_Pro.Model.DTO;
using My_Pro.Model.Entity;
using My_Pro.Model.Request;
using System.Security.Claims;
using static My_Pro.Data.Enum;

namespace My_Pro.Bussiness.BookingServices
{
    public class BookingServices : IBookingServices
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BookingServices(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Create(BookingRequest request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var room = await _appDbContext.Rooms.Where(x => x.Id == request.RoomId && !x.IsDeleted && x.Status == RoomStatus.ConPhong).FirstOrDefaultAsync();
            if (room == null)
            {
                throw new AggregateException($"RoomId {request.RoomId} Not Found!");
            }
            var booking = new Booking()
            {
                UsersId = currentUser,
                RoomId = request.RoomId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
            };
            await _appDbContext.Bookings.AddAsync(booking);
            await _appDbContext.SaveChangesAsync();

            var numberOfDays = (int)(booking.CheckOutDate - booking.CheckInDate).TotalDays;
            booking.TotalPrice = room.PricePerNight * numberOfDays;
            if (request.ServiceId != null)
            {
                decimal totalPriceService = 0;
                foreach (var item in request.ServiceId)
                {
                    var service = await _appDbContext.Services.Where(x => x.Id == item && !x.IsDeleted).FirstOrDefaultAsync();
                    if (service == null)
                    {
                        throw new AggregateException($"ServiceId {item} Not Found!");
                    }
                    var bookingService = new BookingService()
                    {
                        BookingId = booking.Id,
                        ServiceId = service.Id,
                    };
                    totalPriceService += service.Price;
                    await _appDbContext.BookingServices.AddAsync(bookingService);
                }
                booking.TotalPrice += totalPriceService;
            }
            room.Status = RoomStatus.HetPhong;
            _appDbContext.Rooms.Update(room);

            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var booking = await CheckById(id);
            booking.IsDeleted = true;
            _appDbContext.Bookings.Update(booking);

            var bookingService = _appDbContext.BookingServices.Where(x => x.BookingId == booking.Id);
            foreach (var item in bookingService)
            {
                item.IsDeleted = true;
                _appDbContext.BookingServices.Update(item);
            }

            var room = await _appDbContext.Rooms.Where(x => x.Id == booking.RoomId).FirstOrDefaultAsync();
            room.Status = RoomStatus.ConPhong;
            _appDbContext.Rooms.Update(room);

            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<BookingDTO> GetById(int id)
        {
            var booking = await CheckById(id);
            var bookingDTO = MapToBookingDTO(booking);
            return bookingDTO;
        }

        public async Task<List<BookingDTO>> GetList(decimal? totalStart, decimal? totalEnd)
        {
            var query = _appDbContext.Bookings.Include(u => u.Users)
                .Include(r => r.Rooms).ThenInclude(img => img.Images)
                .Include(bs => bs.BookingServices).ThenInclude(s => s.Services)
                .Where(x => !x.IsDeleted).AsQueryable();
            if (totalStart.HasValue)
            {
                query = query.Where(x => x.TotalPrice >= totalStart.Value);
            }
            if (totalEnd.HasValue)
            {
                query = query.Where(x => x.TotalPrice >= totalEnd.Value);
            }
            var bookingDTO = query.Select(b => MapToBookingDTO(b));
            return await bookingDTO.ToListAsync();
        }
        public async Task<bool> Update(int id, BookingRequest request)
        {
            var booking = await CheckById(id);
            var roomOld = await _appDbContext.Rooms.Where(x => x.Id == booking.RoomId).FirstOrDefaultAsync();
            roomOld.Status = RoomStatus.ConPhong;
            _appDbContext.Rooms.Update(roomOld);

            var roomNew = await _appDbContext.Rooms.Where(x => x.Id == request.RoomId).FirstOrDefaultAsync();
            roomNew.Status = RoomStatus.HetPhong;
            _appDbContext.Rooms.Update(roomNew);

            booking.RoomId = request.RoomId;
            booking.CheckInDate = request.CheckInDate;
            booking.CheckOutDate = request.CheckOutDate;

            var numberOfDays = (int)(booking.CheckOutDate - booking.CheckInDate).TotalDays;
            booking.TotalPrice = roomNew.PricePerNight * numberOfDays;

            var oldBookingServices = _appDbContext.BookingServices.Where(x => x.BookingId == booking.Id);
            _appDbContext.BookingServices.RemoveRange(oldBookingServices);

            if (request.ServiceId != null)
            {
                decimal totalPriceService = 0;
                foreach (var item in request.ServiceId)
                {
                    var service = await _appDbContext.Services.Where(x => x.Id == item && !x.IsDeleted).FirstOrDefaultAsync();
                    if (service == null)
                    {
                        throw new AggregateException($"ServiceId {item} Not Found!");
                    }
                    var bookingService = new BookingService()
                    {
                        BookingId = booking.Id,
                        ServiceId = service.Id,
                    };
                    totalPriceService += service.Price;
                    await _appDbContext.BookingServices.AddAsync(bookingService);
                }
                booking.TotalPrice += totalPriceService;
            }
            return await _appDbContext.SaveChangesAsync() > 0;

        }
        private async Task<Booking> CheckById(int id)
        {
            var checkBoongking = await _appDbContext.Bookings.Include(u => u.Users)
                .Include(r => r.Rooms).ThenInclude(img => img.Images)
                .Include(bs => bs.BookingServices).ThenInclude(s => s.Services)
                .Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            if (checkBoongking == null)
            {
                throw new AggregateException($"BookingId {id} Not Found!");
            }
            return checkBoongking;
        }
        private static BookingDTO MapToBookingDTO(Booking booking)
        {
            if (booking == null)
                return null;

            return new BookingDTO()
            {
                Id = booking.Id,
                UserId = booking.UsersId,
                Users = new UserDTO()
                {
                    Id = booking.Users.Id,
                    FirtName = booking.Users.FirtName,
                    LastName = booking.Users.LastName,
                    UserName = booking.Users.UserName,
                    Email = booking.Users.Email,
                    PhoneNumber = booking.Users.PhoneNumber
                },
                RoomId = booking.RoomId,
                Rooms = new RoomDTO()
                {
                    Id = booking.Rooms.Id,
                    Name = booking.Rooms.Name,
                    Description = booking.Rooms.Description,
                    RoomTypeId = booking.Rooms.RoomTypeId,
                    PricePerNight = booking.Rooms.PricePerNight,
                    Images = booking.Rooms.Images.Select(x => new RoomImageDTO()
                    {
                        Id = x.Id,
                        FileName = x.FileName,
                        ImagePath = x.ImagePath,
                    }).ToList(),
                    Status = booking.Rooms.Status,
                },
                BookingServices = booking.BookingServices.Select(bs => MapToBookingServiceDTO(bs)).ToList(),
                TotalPrice = booking.TotalPrice,
            };
        }
        private static BookingServiceDTO MapToBookingServiceDTO(BookingService bookingService)
        {
            if (bookingService == null)
                return null;
            return new BookingServiceDTO()
            {
                Id = bookingService.Id,
                BookingId = bookingService.Id,
                ServiceId = bookingService.ServiceId,
                Services = new ServiceDTO()
                {
                    Id = bookingService.Services.Id,
                    Name = bookingService.Services.Name,
                    Description = bookingService.Services.Description,
                    ServiceTypeId = bookingService.Services.ServiceTypeId,
                    Price = bookingService.Services.Price,
                }
            };
        }
    }
}
