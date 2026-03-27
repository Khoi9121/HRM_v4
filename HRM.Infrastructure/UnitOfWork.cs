using HRM.Domain.Entities;
using HRM.Domain.Interfaces;
using HRM.Infrastructure.Data;
using HRM.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entities;
using HRM.Domain.Interfaces;
using HRM.Infrastructure.Data;
using HRM.Infrastructure.Repositories;
namespace HRM.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repos = new();

        public UnitOfWork(AppDbContext context) => _context = context;

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T);
            if (!_repos.ContainsKey(type))
                _repos[type] = new GenericRepository<T>(_context);
            return (IGenericRepository<T>)_repos[type];
        }

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
