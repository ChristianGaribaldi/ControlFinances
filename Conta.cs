using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaContas
{
    public class Conta
    {
        public string usuario;
        public string nomeConta;
        private double saldo;
        public List<Lancamento> lancamentos;

        public Conta(string usuario, string nomeConta) 
        {
            this.usuario = usuario;
            this.nomeConta = nomeConta;
            this.saldo = 0;
            this.lancamentos = new List<Lancamento>();
        }

        public double Saldo 
        {
            get { return saldo; }
            set { saldo = value; }
        }
    }
}
