using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestaoContratos.Models
{
    public class Context : DbContext
    {

        public Context() : base("name=DefaultConnection")
        {

        }
        public DbSet<Contrato> Contratos { get; set; }

    }
}