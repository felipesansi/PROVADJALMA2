using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string codigo_produto { get; set; }
        public double Preco_venda { get; set; }
        public string Marca { get; set; }
        public int Estoque_atual { get; set; }
        public string Excluido { get; set; }

    }
}