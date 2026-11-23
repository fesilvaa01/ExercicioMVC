using Microsoft.AspNetCore.Mvc;
using PrimeiroProjetoMVC.Models;
using PrimeiroProjetoMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiroProjetoMVC.Controllers
{
    public class EventosController : Controller
    {
        // Lista em memória (static para manter os dados entre requisições)
        private static List<Evento> _eventos = new List<Evento>
        {
            new Evento 
            { 
                Id = 1, 
                Nome = "Workshop ASP.NET Core", 
                Descricao = "Aprenda a construir aplicações web modernas com .NET.", 
                Local = "Auditório Principal", 
                DataEvento = DateTime.Now.AddDays(10), 
                QuantidadeVagas = 50, 
                ValorIngresso = 49.90m 
            }
        };
        private static int _proximoId = 2;

        // GET: /Eventos
        public IActionResult Index()
        {
            return View(_eventos);
        }

        // GET: /Eventos/Criar
        [HttpGet]
        public IActionResult Criar()
        {
            return View(new Evento());
        }

        // POST: /Eventos/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return View(evento);
            }

            evento.Id = _proximoId++;
            _eventos.Add(evento);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Eventos/Buscar
        [HttpGet]
        public IActionResult Buscar()
        {
            var viewModel = new BuscaEventoViewModel();
            return View(viewModel);
        }

        // POST: /Eventos/Buscar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Buscar(BuscaEventoViewModel model)
        {
            var query = _eventos.AsEnumerable();

            // Filtro por Nome (requisito obrigatório)
            if (!string.IsNullOrWhiteSpace(model.NomeBusca))
            {
                query = query.Where(e => e.Nome.Contains(model.NomeBusca, StringComparison.OrdinalIgnoreCase));
            }

            // Filtros complementares (aproveitando os campos do ViewModel)
            if (!string.IsNullOrWhiteSpace(model.LocalBusca))
            {
                query = query.Where(e => e.Local.Contains(model.LocalBusca, StringComparison.OrdinalIgnoreCase));
            }

            if (model.DataInicial.HasValue)
            {
                query = query.Where(e => e.DataEvento.Date >= model.DataInicial.Value.Date);
            }

            model.Resultados = query.ToList();
            model.PesquisaRealizada = true;

            return View(model);
        }

        // DESAFIO ADICIONAL - GET: /Eventos/Detalhes/1
        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            var evento = _eventos.FirstOrDefault(e => e.Id == id);
            
            if (evento == null)
            {
                return NotFound("Evento não encontrado.");
            }

            return View(evento);
        }
    }
}