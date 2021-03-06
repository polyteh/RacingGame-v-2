﻿using Microsoft.EntityFrameworkCore;
using RacingGameDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingGameDAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity, IName
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(DbContext curContext)
        {
            _context = curContext;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var itemsAsync = await _dbSet.ToListAsync();
            return itemsAsync;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var itemById = await _dbSet.SingleOrDefaultAsync<T>(e=>e.Id==id);
            return itemById;
        }
    }
}
