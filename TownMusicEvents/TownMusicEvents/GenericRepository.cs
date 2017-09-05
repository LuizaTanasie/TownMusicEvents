using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownMusicEvents
{
    public class GenericRepository<E> : IRepository<E> where E : class
    {
        private DbSet<E> objs;
        private DatabaseContext dbc;

        public GenericRepository(DatabaseContext dbc)
        {
            this.objs = dbc.Set<E>();
            this.dbc = dbc;
        }

        public E Add(E entity)
        {
            return objs.Add(entity);
        }

        public void Remove(int id)
        {
            E entity = Find(id);
            objs.Remove(entity);
        }

        public E Find(int id)
        {
            return objs.Find(id);
        }

        public void Update(E entityToUpdate)
        {
            var entry = dbc.Entry(entityToUpdate);
            entry.State = EntityState.Modified;
        }

        public IQueryable<E> GetAll()
        {
            return objs;
        }


    }
}
