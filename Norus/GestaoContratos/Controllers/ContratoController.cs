using GestaoContratos.Servicos;
using GestaoContratos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestaoContratos.Controllers
{
    public class ContratoController : Controller
    {
        ContratoServico _servicoContrato;

        public ContratoController()
        {
            _servicoContrato = new ContratoServico();
        }

        [HttpGet]
        public ActionResult Cadastro(int id = 0)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpGet]
        public ActionResult Dados(int id = 0)
        {
            return Json(_servicoContrato.Consultar(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Cadastro(ContratoViewModel contrato)
        {
            try
            {
                _servicoContrato.Gravar(contrato);
            }
            catch (Exception e)
            {
                return Json(new { erro = e.Message });
            }
           
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult Remover(int id)
        {
            try
            {
                _servicoContrato.Deletar(id);
            }
            catch (Exception e)
            {
                return Json(new { erro = e.Message });
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        [HttpGet]
        public ActionResult Lista()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListaContratosPagina(string pesquisa, int numeroPagina, int numeroItensPorPagina)
        {
            return Json(_servicoContrato.Consultar(pesquisa, numeroPagina, numeroItensPorPagina), JsonRequestBehavior.AllowGet);
        }
    }
}