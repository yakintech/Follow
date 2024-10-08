using Follow.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Business.Repository
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        List<T> GetAll();
        T GetByQuery(Func<T, bool> query);
        List<T> GetAllWithIncludes(string[] includes);
        T GetById(int id);
        T GetByIdWitIncludes(int id, string[] includes);
        T Create(T entity);
        void Delete(int id);
        T Update(T entity);
    }
}
