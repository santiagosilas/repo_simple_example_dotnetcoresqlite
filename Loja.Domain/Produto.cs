using System;

namespace Loja.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataAquisicao { get; set; }
        public bool Ativo { get; set; }
        public int IdCategoria { get; set; }
        public virtual Categoria categoria { get; set; }

    }
}
