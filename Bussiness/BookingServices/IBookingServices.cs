using My_Pro.Model.DTO;
using My_Pro.Model.Request;

namespace My_Pro.Bussiness.BookingServices
{
    public interface IBookingServices
    {
        Task<List<BookingDTO>> GetList(decimal? totalStart, decimal? totalEnd);
        Task<BookingDTO> GetById(int id);
        Task<bool> Create(BookingRequest request);
        Task<bool> Update(int id, BookingRequest request);
        Task<bool> Delete(int id);
    }
}
