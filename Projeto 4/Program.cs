using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_4
{
    class Program
    {
        static List<IEstoque> produtos = new List<IEstoque>();
        enum Menu { Listar = 1, Adicionar, Remover, Entrada, Saida, Sair }
        static void Main(string[] args)
        {
            Carregar();
            bool escolherSair = false;
            while (escolherSair == false)
            {
                Console.WriteLine("Sistema de Estoque");
                Console.WriteLine("1-Listar\n2-Adicionar\n3-Remover\n4-Registar Entrada\n5-Registrar Saida\n6-Sair");
                string OpStr = Console.ReadLine();
                int OpInt = int.Parse(OpStr);

                if (OpInt > 0 && OpInt < 7)
                {
                    Menu escolha = (Menu)OpInt;
                    switch (escolha)
                    {
                        case Menu.Listar:
                            Listagem();
                            break;
                        case Menu.Adicionar:
                            Cadastro();
                            break;
                        case Menu.Remover:
                            Remover();
                            break;
                        case Menu.Entrada:
                            Entrada();
                            break;
                        case Menu.Saida:
                            Saida();
                            break;
                        case Menu.Sair:
                            escolherSair = true;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERRO!!! Você tem que escolher as opções entre 1 a 7");
                    Console.ReadLine();
                }
                Console.Clear();
            }
        }

        static void Listagem()
        {
            Console.WriteLine("Lista de Produtos");
            int i = 0;
            foreach(IEstoque produto in produtos)
            {
                Console.WriteLine($"ID: {i}");
                produto.Exibir();
                i++;
            }
            Console.ReadLine();
        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que deseja remover: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)
            {
                produtos.RemoveAt(id);
                Salvar();
            }
        }

        static void Entrada()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você deseja dar entrada: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarEntrada();
                Salvar();
            }
        }

        static void Saida()
        {
            Listagem();
            Console.WriteLine("Digite O ID do elemento que você deseja da baixa: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)
            {
                produtos[id].AdicionarSaida();
                Salvar();
            }
        }

        enum Menu1 { ProdutoFisico = 1, Ebook, Curso }
        static void Cadastro()
        {
            Console.WriteLine("Cadastro de Produto");
            Console.WriteLine("1 - Produto Fisico\n2 - Ebook\n3 - Curso");
            string opStr = Console.ReadLine();
            int escolhaInt = int.Parse(opStr);
            if (escolhaInt > 0 && escolhaInt < 4)
            {
                Menu1 escolha = (Menu1)escolhaInt;
                switch (escolha)
                {
                    case Menu1.ProdutoFisico:
                        CadastrarPFisico();
                        break;
                    case Menu1.Ebook:
                        CadastrarEbook();
                        break;
                    case Menu1.Curso:
                        CadastrarCurso();
                        break;
                }
            }
            else
            {
                Console.WriteLine("ERRO!!! Escolha uma das opções entre 1 a 3");
                Console.ReadLine();
            }

        }

        static void CadastrarPFisico()
        {
            Console.WriteLine("Cadastramento Produto Fisico");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Frete: ");
            float frete = float.Parse(Console.ReadLine());
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            ProdutoFisico pf = new ProdutoFisico(nome, preco, frete);
            produtos.Add(pf);
            Salvar();
        }

        static void CadastrarEbook()
        {
            Console.WriteLine("Cadastramento Ebook");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();
            Ebook eb = new Ebook(nome, preco, autor);
            produtos.Add(eb);
            Salvar();
        }

        static void CadastrarCurso()
        {
            Console.WriteLine("Cadastramento Curso");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();
            Cursos cs = new Cursos(nome, preco, autor);
            produtos.Add(cs);
            Salvar();
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("produtos.dat",FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, produtos);
            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();
            try
            {
                produtos = (List<IEstoque>)encoder.Deserialize(stream);
                if (produtos == null)
                {
                    produtos = new List<IEstoque>();
                }
            }
            catch (Exception e)
            {
                produtos = new List<IEstoque>();
            }
            
            stream.Close();
        }
      
    }
}
