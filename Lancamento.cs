using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaContas
{
    public enum TipoLancamento
    {
        Receita = 1,
        Despesa = 2
    }
    
    public class Lancamento
    {
        public TipoLancamento tipo;
        public DateTime data;
        private double valor;
        public Categoria categoriaLancamento;

        public Lancamento(TipoLancamento tipo, DateTime data, double valor, Categoria categoria)
        {
            this.tipo = tipo;
            this.data = data;
            this.valor = valor;
            this.categoriaLancamento = categoria;
        }

        public double Valor 
        {
            get { return valor; }
            set { valor = value; }
        }
    }
}
