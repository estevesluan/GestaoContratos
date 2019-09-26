using System;
using System.Web;

namespace GestaoContratos.ViewModel
{
    public class ContratoViewModel
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public int TipoContrato { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public HttpPostedFileBase Arquivo { get; set; }
        public byte[] ArquivoDownload { get; set; }

    }
}