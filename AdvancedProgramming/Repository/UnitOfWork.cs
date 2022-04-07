﻿using AdvancedProgramming.Data;
using AdvancedProgramming.Interfaces;
using AdvancedProgramming.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace AdvancedProgramming.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly EmployeeContext _dbContext;
        private IRepository<Employee> _employeeRepository;

        public UnitOfWork(EmployeeContext dbContext) => _dbContext = dbContext ?? throw new ArgumentNullException("Context was not supplied");
        public IRepository<Employee> EmployeeRepository =>
         _employeeRepository ?? (_employeeRepository = new Repository<Employee>(_dbContext));



        public int SaveChanges() => _dbContext.SaveChanges();

        public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();

        public void BeginTransaction()
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(UnitOfWork).Name);

            if (_transaction == null)
                _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null)
                    _transaction.Commit();
            }
            finally
            {
                if (_transaction != null)
                    _transaction = null;
            }
        }

        public void RollBack()
        {
            try
            {
                if (_transaction != null)
                    _transaction.Rollback();
            }
            finally
            {
                if (_transaction != null)
                    _transaction = null;
            }
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
