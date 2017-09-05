using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownMusicEvents
{
    public interface IRepository<E>
    {
        E Add(E entity);

        void Remove(int id);

        E Find(int id);

        void Update(E e2);

        IQueryable<E> GetAll();
    }
}
