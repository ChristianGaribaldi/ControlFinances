ControlFinances
===============
 Sistema básico para controle de finanças pessoais;
	A aplicação está desenvolvida na linguagem C# e não possui interface gráfica nem nenhum tipo de persistência.
	A execução da aplicação ocorre no Console.
	Para rodar a aplicação, é necessário reunir todos os arquivos em uma Solution do Visual Studio e
	determinar a classe Program.cs como arquivo principal da aplicação.
	Todos os arquivos necessários à execução estão na pasta principal do repositório;
	
	=>	Estrutura/Funcionalidades básicas:
	O sistema possui o conceito de contas vinculadas à usuários.
	As contas são identificadas por um nome, possuindo saldo e lançamentos diversos.
	Os lançamentos podem ser de receita ou de despesa e possuem: data, valor e categoria.
	As categorias possuem um nome, e podem ter sub-categorias, como por exemplo: Categoria: Casa, sub-categoria: Aluguel.
	Há possibilidade de transferência de valores entre contas por meio do nome de usuários e o nome das contas envolvidas.
	
	=> 	Há também algumas formas de visualização das contas e propriedades como:
	Exibição de todos lançamentos ordenados por Data;
	Exibição de todos lançamentos por Categoria
	Exibição de todos lançamentos por Periodo (Ex: 01/01/2013 até 30/03/2013)
	Exibição de todos lançamentos por Tipo (Receita ou Despesa);
	
Autor: Christian Garibaldi


  


