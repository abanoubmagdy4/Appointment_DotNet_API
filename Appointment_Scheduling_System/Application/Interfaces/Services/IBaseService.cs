using Appointment_Scheduling_System.Application.Helpers;
namespace Appointment_Scheduling_System.Application.Interfaces.Services

{
    public interface IBaseService<T> where T : class
    {
        Task<ServiceResult<List<T>>> GetAllAsync();
        Task<ServiceResult<T>> GetByIdAsync(int id);
        Task<ServiceResult<T>> AddAsync(T entity);
        Task<ServiceResult<T>> UpdateAsync(T entity);
        Task<ServiceResult<bool>> DeleteAsync(int id);
        Task<ServiceResult<List<T>>> AddRangeAsync(IEnumerable<T> entities);
        ServiceResult<T> Attach(T entity);
        ServiceResult<T> SoftDelete(T entity);
        Task<ServiceResult<T>> GetByStringIdAsync(string id);
    }
}
