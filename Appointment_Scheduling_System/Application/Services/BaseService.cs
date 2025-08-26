using Appointment_Scheduling_System.Application.Interfaces.Repositories;
using Appointment_Scheduling_System.Application.Interfaces.Services;
using Appointment_Scheduling_System.Application.Helpers;
using Appointment_Scheduling_System.Infrastructure.Persistence.Repositories;

namespace Appointment_Scheduling_System.Application.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<T>> AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                _unitOfWork.SaveChangesAsync(); 
                return ServiceResult<T>.Ok(entity);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<List<T>>> AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _repository.AddRangeAsync(entities);
                _unitOfWork.SaveChangesAsync();
                return ServiceResult<List<T>>.Ok(entities.ToList());
            }
            catch (Exception ex)
            {
                return ServiceResult<List<T>>.Fail(ex.Message);
            }
        }

        public ServiceResult<T> Attach(T entity)
        {
            try
            {
                _repository.Attach(entity);
                return ServiceResult<T>.Ok(entity);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                _unitOfWork.SaveChangesAsync();

                return ServiceResult<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<List<T>>> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync();
                return ServiceResult<List<T>>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<T>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<T>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                if (result == null)
                    return ServiceResult<T>.Fail("Entity not found");

                return ServiceResult<T>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<T>> GetByStringIdAsync(string id)
        {
            try
            {
                var result = await _repository.GetByStringIdAsync(id);
                if (result == null)
                    return ServiceResult<T>.Fail("Entity not found");

                return ServiceResult<T>.Ok(result);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail(ex.Message);
            }
        }

        public ServiceResult<T> SoftDelete(T entity)
        {
            try
            {
                _repository.SoftDelete(entity);
                return ServiceResult<T>.Ok(entity);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<T>> UpdateAsync(T entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
                return ServiceResult<T>.Ok(entity);
            }
            catch (Exception ex)
            {
                return ServiceResult<T>.Fail(ex.Message);
            }
        }
    }
}
