// Projeto de Extensão – Digital Conect
// Sistema de Cadastro de Ordens de Serviço

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> ordens = new List<string>();
        int opcao;

        do
        {
            Console.WriteLine("\n===== DIGITAL CONECT =====");
            Console.WriteLine("1 - Cadastrar ordem de serviço");
            Console.WriteLine("2 - Listar ordens de serviço");
            Console.WriteLine("3 - Sair");
            Console.Write("Escolha uma opção: ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Write("Nome do cliente: ");
                    string cliente = Console.ReadLine();

                    Console.Write("Modelo do notebook: ");
                    string modelo = Console.ReadLine();

                    Console.Write("Defeito apresentado: ");
                    string defeito = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(cliente) ||
                        string.IsNullOrWhiteSpace(modelo) ||
                        string.IsNullOrWhiteSpace(defeito))
                    {
                        Console.WriteLine("Preencha todos os campos!");
                    }
                    else
                    {
                        string ordem = "Cliente: " + cliente +
                            " | Notebook: " + modelo +
                            " | Defeito: " + defeito;

                        ordens.Add(ordem);

                        Console.WriteLine("Ordem cadastrada com sucesso!");
                    }
                    break;

                case 2:
                    Console.WriteLine("\n--- ORDENS CADASTRADAS ---");

                    if (ordens.Count == 0)
                    {
                        Console.WriteLine("Nenhuma ordem cadastrada.");
                    }
                    else
                    {
                        foreach (string item in ordens)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;

                case 3:
                    Console.WriteLine("Sistema encerrado!");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

        } while (opcao != 3);
    }
}