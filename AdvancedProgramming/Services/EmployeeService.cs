using AdvancedProgramming.Interfaces;
using AdvancedProgramming.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Threading.Tasks;

namespace AdvancedProgramming.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateAsync(Employee employee)
        {
            try
            {
                var nameCheck = await _unitOfWork.EmployeeRepository.Find(x => x.Name.ToUpper() == employee.Name.ToUpper()).FirstOrDefaultAsync();

                if (nameCheck != null)
                {
                    throw new Exception($"{nameCheck.Name} Employee Already Exist ");
                }
                var res = _unitOfWork.EmployeeRepository.Insert(employee);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error: {ErrorMessage},{ErrorDetails}", ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
