using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestaoContratos.Models
{
    public class ContratoRepositorio : IRepository<Contrato>
    {
        private Context _context;

        public ContratoRepositorio(Context context)
        {
            _context = context;
        }

        public void Insert(Contrato obj)
        {
            _context.Set<Contrato>().Add(obj);
            _context.SaveChanges();
        }
        public void Update(Contrato obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<Contrato>().Remove(Select(id));
            _context.SaveChanges();
        }

        public Contrato Select(int id)
        {
            return _context.Set<Contrato>().Find(id);
        }

        public IQueryable<Contrato> SelectAll()
        {
            return _context.Set<Contrato>();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}