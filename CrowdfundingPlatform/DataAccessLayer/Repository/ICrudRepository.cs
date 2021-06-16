﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.Repository
{
    public interface ICrudRepository<T> where T : BaseEntity
    {
        public Task Create(T entity);

        public Task<T> ReadById(int id);

        public Task<IEnumerable<T>> ReadAll();

        public Task Update(T entity);

        public Task<T> DeleteById(int id);
    }
}
