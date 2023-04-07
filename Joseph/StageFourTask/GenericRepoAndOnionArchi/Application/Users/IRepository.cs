using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Users
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(int Id);
        List<T> Filter(string name);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}