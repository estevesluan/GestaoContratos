using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoContratos.ViewModel
{
    public class ContratoPaginacaoViewModel
    {
        public int Total { get; set; }
        public int NumeroItensPorPagina { get; set; }
        public int NumeroPagina { get; set; }
        public IEnumerable<ContratoViewModel> Contratos { get; set; }
    }
}