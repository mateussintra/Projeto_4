using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_4
{
    [System.Serializable]
    class Ebook : Produto, IEstoque
    {
        public string autor;
        private int vendas;

        public Ebook(string nome, float preco, string autor)
        {
            this.nome = nome;
            this.preco = preco;
            this.autor = autor;
        }

        public void AdicionarEntrada()
        {
            Console.WriteLine("Não é possivel da entrada, pois o E-book se trata de um produto digital");
            Console.ReadLine();
        }

        public void AdicionarSaida() 
        {
            Console.WriteLine($"Adicionar venda no E-book {nome}");
            Console.WriteLine("Digite a Qtd. de vendas que você quer da entrada: ");
            int entrada = int.Parse(Console.ReadLine());
            vendas -= entrada;
            Console.WriteLine("Saida Registrada");
            Console.ReadLine();
        }

        public void Exibir()
        {
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Autor: {autor}");
            Console.WriteLine($"Preço: {preco}");
            Console.WriteLine($"Vendas: {vendas}");
            Console.WriteLine("===========================");
        }
    }
}
