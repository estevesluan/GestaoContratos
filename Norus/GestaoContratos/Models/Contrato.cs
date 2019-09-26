using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestaoContratos.Models
{
    public enum TipoContrato
    {
        Compra, Venda
    }

    public class Contrato
    {
        [Key]
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public TipoContrato TipoContrato { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        public DateTime Inicio { get; set; }
        public int DuracaoEmMeses { get; set; }
        public byte[] Arquivo { get; set; }
    }
}