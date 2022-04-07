using AdvancedProgramming.Models;
using System;
using System.Threading.Tasks;

namespace AdvancedProgramming.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Employee> EmployeeRepository { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();

        void BeginTransaction();

        void Commit();

        void RollBack();
    }
}
