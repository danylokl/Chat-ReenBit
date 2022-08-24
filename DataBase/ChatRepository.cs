using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class ChatRepository<T> : IRepository<T> where T : class
    {
        private readonly Context _context;
        protected DbSet<T> DbSet;

        public ChatRepository(Context context)
        {
            //context.Database.EnsureDeleted();
            _context = context;

            //context.Database.EnsureCreated();

            DbSet = _context.Set<T>();
        }
        public async Task<List<T>> GetAllAsync()
        {
            var result = await DbSet.ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await DbSet.FindAsync(id);
            return result;
        }

        public async Task<T> Create(T entity)
        {
            var result = await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(T entity)
        {
            DbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
            _context.SaveChanges();
        }

        public async Task Remove(int id)
        {
            var entity = await GetByIdAsync(id);
            Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
