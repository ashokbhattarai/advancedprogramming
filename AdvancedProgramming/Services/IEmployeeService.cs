using AdvancedProgramming.Models;
using System.Threading.Tasks;

namespace AdvancedProgramming.Services
{
    public interface IEmployeeService
    {
        Task<bool> CreateAsync(Employee employee);

    }
}
