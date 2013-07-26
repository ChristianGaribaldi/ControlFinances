using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaContas
{
    public class Categoria
    {
        public string nome;
        public string subCategoria;

        public Categoria(string nome, string subCategoria) 
        {
            this.nome = nome;
            this.subCategoria = subCategoria;
        }
    }
}
