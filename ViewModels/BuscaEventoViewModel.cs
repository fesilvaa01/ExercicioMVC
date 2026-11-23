using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PrimeiroProjetoMVC.Models;

namespace PrimeiroProjetoMVC.ViewModels
{
    public class BuscaEventoViewModel
    {
        [Display(Name = "Nome do Evento")]
        public string? NomeBusca { get; set; }

        [Display(Name = "Local")]
        public string? LocalBusca { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "A partir da data")]
        public DateTime? DataInicial { get; set; }

        public List<Evento> Resultados { get; set; } = new List<Evento>();
        
        // Propriedade auxiliar para saber se o formulário já foi submetido
        public bool PesquisaRealizada { get; set; } = false;
    }
}