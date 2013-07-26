using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaContas
{
    class Program
    {
        // Menu básico para rodar a aplicação.

        static void VoltarAoMenu()
        {
            Console.WriteLine("\nAperte qualquer tecla para voltar ao Menu......");
            Console.ReadKey();
            Console.Clear();
        }

        static void Main(string[] args)
        {
            SistemaContas banco = new SistemaContas();
            bool fecharSistema = false;

            while (!fecharSistema)
            {
                Console.WriteLine("\t\t\t MENU");
                Console.WriteLine("\n1 - Criar Conta");
                Console.WriteLine("\n2 - Realizar Lançamento");
                Console.WriteLine("\n3 - Realizar Transferência entre contas");
                Console.WriteLine("\n4 - Ver Saldo");
                Console.WriteLine("\n5 - Exibir Lançamentos por Data");
                Console.WriteLine("\n6 - Exibir Lançamentos por Categoria");
                Console.WriteLine("\n7 - Exibir Lançamentos por Período");
                Console.WriteLine("\n8 - Exibir Lançamentos por Tipo");
                Console.WriteLine("\n9 - Sair");
                Console.WriteLine("\nDigite sua opção > ");
                int opcao = int.Parse(Console.ReadLine());

                Console.Clear();
                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Digite o nome do Usuário Portador da Conta:");
                        string nomeUsuario = Console.ReadLine();

                        Console.WriteLine("Prezado(a) {0}, digite o nome da Conta:", nomeUsuario);
                        string nomeConta = Console.ReadLine();

                        banco.CriarConta(nomeUsuario, nomeConta);
                        VoltarAoMenu();
                        break;

                    case 2:
                        banco.CriarLancamento();
                        VoltarAoMenu();
                        break;

                    case 3:
                        banco.RealizarTransferencia();
                        VoltarAoMenu();
                        break;

                    case 4:
                        banco.VerSaldo();
                        VoltarAoMenu();
                        break;

                    case 5:
                        banco.ExibirLancamentosOrdenadosPorData();
                        VoltarAoMenu();
                        break;

                    case 6:
                        banco.ListarCategorias();
                        
                        Console.WriteLine("\nDigite sua opção > ");
                        int categoria= int.Parse(Console.ReadLine());
                        
                        Categoria categoriaListar = banco.ObterCategoriaDigitada(categoria);
                        banco.ExibirLancamentosPorCategoria(categoriaListar);

                        VoltarAoMenu();
                        break;

                    case 7:
                        Console.WriteLine("Digite a Data Início do Período no formato ( > ");
                        DateTime dataInicioPeriodo = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Digite a Data Final do Período > ");
                        DateTime dataFimPeriodo = DateTime.Parse(Console.ReadLine());

                        banco.ExibirLancamentosPorPeriodo(dataInicioPeriodo, dataFimPeriodo);
                        VoltarAoMenu();
                        break;

                    case 8:
                        Console.WriteLine("Digite o Tipo de Lançamento a ser listado: ");
                        Console.WriteLine("1 - Receita\n2 - Despesa");
                        int tipoLancamentoListado = int.Parse(Console.ReadLine());

                        banco.ExibirLancamentosPorTipo(tipoLancamentoListado == 1 ? TipoLancamento.Receita : TipoLancamento.Despesa);
                        VoltarAoMenu();
                        break;

                    case 9:
                        fecharSistema = true;
                        break;

                }
            }
        }
    }
}
