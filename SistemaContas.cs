using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaContas
{
    public class SistemaContas
    {
        private List<Conta> contasSistema;
        private List<Categoria> categorias;

        public SistemaContas()
        {
            contasSistema = new List<Conta>();
            categorias = new List<Categoria>();
            PreencherCategorias();
        }

        public void CriarConta(string nomeUsuario, string nomeConta)
        {
            Conta novaConta = new Conta(nomeUsuario.Trim(), nomeConta);
            contasSistema.Add(novaConta);
            Console.WriteLine("Conta criada com Sucesso!");
        }

        public void CriarLancamento()
        {
            Console.WriteLine("Digite o nome do Usuário Portador da Conta para Lançamento:");
            string nomeUsuario = Console.ReadLine();

            List<Conta> contasUsuario = RecuperarContasUsuario(nomeUsuario);

            if (contasUsuario != null && contasUsuario.Count > 0)
            {
                Conta contaParaLancamento = EscolherContaParaTransacao(contasUsuario);
                if (contaParaLancamento != null)
                {
                    Console.Clear();
                    Console.WriteLine("Prezado(a) {0}, digite o Tipo de Lançamento da Conta:", nomeUsuario);
                    Console.WriteLine("1- Receita\n2- Despesa");
                    Console.Write("\nDigite sua opção > ");
                    int tipo = int.Parse(Console.ReadLine());

                    Console.Write("\nValor: R$ ");
                    double valor = double.Parse(Console.ReadLine());

                    ListarCategorias();
                    Console.Write("\nDigite sua opção > ");
                    int categoria = int.Parse(Console.ReadLine());

                    bool lancamentoValido = ValidarLancamento(tipo, valor, categoria);
                    if (lancamentoValido)
                    {
                        Lancamento novoLancamento = new Lancamento((TipoLancamento)tipo, DateTime.Now, valor, ObterCategoriaDigitada(categoria));
                        if (tipo == (int)TipoLancamento.Despesa)
                        {
                            contaParaLancamento.Saldo -= novoLancamento.Valor;
                        }
                        if (tipo == (int)TipoLancamento.Receita)
                        {
                            contaParaLancamento.Saldo += novoLancamento.Valor;
                        }

                        contaParaLancamento.lancamentos.Add(novoLancamento);

                        Console.WriteLine("Lançamento Realizado com Sucesso!");
                    }
                }
            }

            else
            {
                Console.WriteLine("\nNão existem contas associadas ao Usuário Digitado.");
            }
        }

        public void RealizarTransferencia()
        {
            Console.WriteLine("\t\tDADOS DA CONTA ORIGEM");
            Console.WriteLine("Digite o nome do Usuário Portador da Conta Origem para a Transferência:");
            string nomeUsuarioOrigem = Console.ReadLine();

            List<Conta> contas = RecuperarContasUsuario(nomeUsuarioOrigem);

            Conta contaOrigem = EscolherContaParaTransacao(contas);

            if (contaOrigem != null)
            {
                Console.Clear();
                Console.WriteLine("\t\tDADOS DA CONTA DESTINO");
                Console.WriteLine("Digite o nome do Usuário Portador da Conta Destino para a Transferência:");
                string nomeUsuarioDestino = Console.ReadLine();

                List<Conta> contasUsuarioDestino = RecuperarContasUsuario(nomeUsuarioOrigem);
                Conta contaDestino = EscolherContaParaTransacao(contasUsuarioDestino);

                Console.Write("Digite o Valor da Transferência: R$ ");
                double valorTransferencia = double.Parse(Console.ReadLine());

                if (contaDestino != null)
                {
                    if (contaOrigem.Saldo > valorTransferencia)
                    {
                        contaOrigem.Saldo -= valorTransferencia;
                        contaDestino.Saldo += valorTransferencia;
                        Console.WriteLine("\nTransferência realizada com Sucesso entre as contas {0} e {1} !", contaOrigem.nomeConta, contaDestino.nomeConta);
                    }
                    else
                    {
                        Console.WriteLine("\nO saldo da Conta Origem é insuficiente para a transferência!");
                    }
                }

                else
                {
                    Console.WriteLine("\nConta Destino usada na Transação é Inválida!");
                }
            }

            else
            {
                Console.WriteLine("\nConta Origem usada na Transação é Inválida!");
            }
        }

        public void ExibirLancamentosOrdenadosPorData()
        {
            ExibirLancamentos(RecuperarTodosLancamentos().OrderBy(_item => _item.data).ToList());
        }

        public void ExibirLancamentosPorCategoria(Categoria categoria)
        {
            if (categoria != null)
            {
                ExibirLancamentos(RecuperarTodosLancamentos().Where(_item => _item.categoriaLancamento.nome == categoria.nome).ToList());
            }

            else
            {
                Console.WriteLine("Categoria Inválida!");
            }
        }

        public void ExibirLancamentosPorPeriodo(DateTime dataInicioPeriodo, DateTime dataFimPeriodo)
        {
            ExibirLancamentos(RecuperarTodosLancamentos().Where(_item => _item.data.Date >= dataInicioPeriodo.Date && _item.data.Date <= dataFimPeriodo.Date).OrderBy(_item => _item.data).ToList());
        }

        public void ExibirLancamentosPorTipo(TipoLancamento tipo)
        {
            ExibirLancamentos(RecuperarTodosLancamentos().Where(_item => _item.tipo == tipo).ToList());
        }

        public void ExibirLancamentos(List<Lancamento> lancamentos)
        {
            if (lancamentos != null && lancamentos.Count == 0)
            {
                Console.WriteLine("Não há lançamentos para exibir.");
            }
            else
            {
                foreach (Lancamento lancamento in lancamentos)
                {
                    Console.WriteLine("\nTipo: {0}", lancamento.tipo == TipoLancamento.Despesa ? "Despesa" : "Receita");
                    Console.WriteLine("Valor: R$ {0}", lancamento.Valor);
                    Console.WriteLine("Data: {0}", lancamento.data);
                    Console.WriteLine("Categoria: {0} / SubCategoria: {1}", lancamento.categoriaLancamento.nome, lancamento.categoriaLancamento.subCategoria);
                }
            }
        }

        private List<Lancamento> RecuperarTodosLancamentos()
        {
            List<Lancamento> todosLancamentos = new List<Lancamento>();

            foreach (Conta conta in contasSistema)
            {
                foreach (Lancamento lancamento in conta.lancamentos)
                {
                    todosLancamentos.Add(lancamento);
                }
            }

            return todosLancamentos;
        }

        public Categoria ObterCategoriaDigitada(int categoria)
        {
            switch (categoria)
            {
                case 1:
                    return categorias[0];
                case 2:
                    return categorias[1];
                case 3:
                    return categorias[2];
                case 4:
                    return categorias[3];
                default:
                    return null;
            }
        }

        private bool ValidarLancamento(int tipo, double valor, int categoria)
        {
            bool lancamentoValido = true;

            if (tipo != (int)TipoLancamento.Receita && tipo != (int)TipoLancamento.Despesa)
            {
                Console.WriteLine("\nTipo de Lançamento Inválido!");
                lancamentoValido = false;
            }

            if (categoria < 0 || categoria > 4)
            {
                Console.WriteLine("\nCategoria Inválida!");
                lancamentoValido = false;
            }

            if (valor <= 0)
            {
                Console.WriteLine("\nValor do Lançamento Inválido!");
                lancamentoValido = false;
            }
            return lancamentoValido;
        }

        private List<Conta> RecuperarContasUsuario(string nomeUsuario)
        {
            List<Conta> contasUsuario = contasSistema.Where(_item => _item.usuario == nomeUsuario.Trim()).ToList();
            return contasUsuario;
        }

        private Conta EscolherContaParaTransacao(List<Conta> contasUsuario)
        {
            if (contasUsuario != null && contasUsuario.Count > 0)
            {
                int numeroContasUsuario = contasUsuario.Count;

                Console.WriteLine("\nSelecione a conta para ser realizada a Transação Solicitada:");
                for (int i = 0; i < numeroContasUsuario; i++)
                {
                    Console.WriteLine("\n{0} - {1}", (i + 1), contasUsuario[i].nomeConta);
                }

                Console.WriteLine("\nDigite sua opção > ");
                int opcaoConta = int.Parse(Console.ReadLine());

                if (opcaoConta > 0 && opcaoConta <= numeroContasUsuario)
                {
                    return contasUsuario[opcaoConta - 1];
                }
                else
                {
                    Console.WriteLine("Conta Escolhida Inválida!");
                }

            }
            return null;
        }

        private void PreencherCategorias()
        {
            Categoria categoria1 = new Categoria("Casa", "Aluguel");
            categorias.Add(categoria1);
            Categoria categoria2 = new Categoria("Lazer", "Conta de Luz");
            categorias.Add(categoria2);
            Categoria categoria3 = new Categoria("Saúde", "Conta de Água");
            categorias.Add(categoria3);
        }

        public void ListarCategorias()
        {
            Console.WriteLine("\nEscolha a Categoria:");
            for (int i = 0; i < categorias.Count; i++)
            {
                Console.WriteLine("{0} - Categoria: {1} / SubCategoria : {2}\n", (i + 1), categorias[i].nome, categorias[i].subCategoria);
            }
        }

        public void VerSaldo()
        {
            Console.WriteLine("Digite o nome do Usuário Portador da Conta para ver o Saldo:");
            string nomeUsuario = Console.ReadLine();

            List<Conta> contasUsuario = RecuperarContasUsuario(nomeUsuario);

            if (contasUsuario != null && contasUsuario.Count > 0)
            {
                Conta contaParaVisualizarSaldo = EscolherContaParaTransacao(contasUsuario);

                if (contaParaVisualizarSaldo != null)
                {
                    Console.WriteLine("SALDO: R$ {0}", contaParaVisualizarSaldo.Saldo);
                }
            }

            else
            {
                Console.WriteLine("Não existem contas para o usuário digitado.");
            }

        }
    }
}
