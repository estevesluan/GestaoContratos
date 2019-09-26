using AutoMapper;
using GestaoContratos.Models;
using GestaoContratos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestaoContratos.Servicos
{
    public class ContratoServico
    {
        private readonly IRepository<Contrato> _repositoryContrato;
        private readonly IMapper _mapper;

        public ContratoServico()
        {
            _repositoryContrato = new ContratoRepositorio(new Context());

            MapperConfiguration config = new MapperConfiguration(cfg => {

                cfg.CreateMap<Contrato, ContratoViewModel>()
                    .ForMember(x => x.Fim, opt => opt.MapFrom(y => y.Inicio.AddMonths(y.DuracaoEmMeses - 1)))
                    .ForMember(x => x.Arquivo, opt => opt.Ignore());

                cfg.CreateMap<ContratoViewModel, Contrato>()
                    .ForMember(x => x.DuracaoEmMeses, opt => opt.MapFrom(y => DiferencaEntreDatasEmMeses(y.Inicio, y.Fim)))
                    .ForMember(x => x.Arquivo, opt => opt.MapFrom(y => ConverterDocumento(y.Arquivo)));

            });
            _mapper = config.CreateMapper();
        }

        public byte[] ConverterDocumento(HttpPostedFileBase arquivo)
        {
            byte[] uploadedFile = new byte[arquivo.InputStream.Length];
            arquivo.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
            return uploadedFile;
        }

        public int DiferencaEntreDatasEmMeses(DateTime d1 , DateTime d2)
        {
            DateTime inicio = d1;
            DateTime fimAno = new DateTime(d1.Year + 1, 1, 1).AddDays(-1);
            int meses = 0;

            while((d2 - fimAno).TotalSeconds > 0)
            {
                meses +=  12 - inicio.Month + 1;

                inicio = new DateTime(inicio.Year + 1, 1, 1);
                fimAno = new DateTime(inicio.Year + 1, 1, 1).AddDays(-1);
            }

            meses += d2.Month - inicio.Month + 1;

            return meses;
        }

        public void Gravar(ContratoViewModel contrato)
        {
            if ((TipoContrato)contrato.TipoContrato == TipoContrato.Venda && !ValidarContrato(contrato))
            {
                throw new Exception("Não existe quantidade disponível para venda.");
            }

            Contrato c = _mapper.Map<ContratoViewModel, Contrato>(contrato);

            if (c.Id == 0)
                _repositoryContrato.Insert(c);
            else
                _repositoryContrato.Update(c);
        }

        public void Deletar(int id)
        {
            _repositoryContrato.Delete(id);
        }

        public List<ContratoViewModel> Consultar()
        {
            return _mapper.Map<List<Contrato>, List<ContratoViewModel>>(_repositoryContrato.SelectAll().ToList());
        }

        public ContratoViewModel Consultar(int id)
        {
            return _mapper.Map<Contrato, ContratoViewModel>(_repositoryContrato.Select(id));
        }

        public ContratoPaginacaoViewModel Consultar(string pesquisa, int numeroPagina, int numeroItensPorPagina)
        {
            try
            {
                IQueryable<Contrato> lista = _repositoryContrato.SelectAll();

                if (!String.IsNullOrEmpty(pesquisa))
                {
                    lista = lista.Where(x => (x.NomeCliente).ToUpper().Contains(pesquisa.ToUpper()));
                }

                ContratoPaginacaoViewModel contratoPaginacao = new ContratoPaginacaoViewModel();
                contratoPaginacao.NumeroPagina = numeroPagina;
                contratoPaginacao.NumeroItensPorPagina = numeroItensPorPagina;
                contratoPaginacao.Total = (int)Math.Ceiling((lista.Count() / (double)numeroItensPorPagina));

                lista = lista.OrderBy(x => x.NomeCliente).Skip((numeroPagina - 1) * numeroItensPorPagina).Take(numeroItensPorPagina);

                contratoPaginacao.Contratos =_mapper.Map<List<Contrato>, List<ContratoViewModel>>(lista.ToList()); 

                return contratoPaginacao;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool ValidarContrato(ContratoViewModel contrato)
        {

            List<Contrato> contratos = _repositoryContrato.SelectAll().ToList();

            DateTime inicio = contrato.Inicio;
            bool contratoPermitido = true;

            while(inicio <= contrato.Fim && contratoPermitido)
            {
                int totalEntrada = contratos
                    .Where( x => x.TipoContrato == TipoContrato.Compra &&
                    x.Inicio <= inicio &&
                    x.Inicio.AddMonths(x.DuracaoEmMeses -1) >= inicio).Select(s => s.Quantidade).Sum();


                int totalSaida = contratos
                    .Where(x => x.TipoContrato == TipoContrato.Venda &&
                    x.Inicio <= inicio &&
                    x.Inicio.AddMonths(x.DuracaoEmMeses - 1) >= inicio).Select(s => s.Quantidade).Sum();

                if (totalEntrada - totalSaida < contrato.Quantidade)
                    contratoPermitido = false;

                inicio = inicio.AddMonths(1);
            }

            return contratoPermitido;
        }
    }
}