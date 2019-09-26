using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoContratos.Models
{
    public interface IRepository<T> : IDisposable
    {
        void Insert(T obj);

        void Update(T obj);

        void Delete(int id);

        T Select(int id);

        IQueryable<T> SelectAll();
    }
}