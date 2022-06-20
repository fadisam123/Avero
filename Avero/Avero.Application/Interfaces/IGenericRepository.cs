using Avero.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avero.Application.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Insert(T obj);
        T Update(T obj);
        T Delete(int id);
    }
}
