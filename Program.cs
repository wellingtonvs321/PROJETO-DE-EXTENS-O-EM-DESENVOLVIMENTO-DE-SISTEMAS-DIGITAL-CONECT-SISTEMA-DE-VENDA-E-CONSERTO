// UNINOVE - Projeto de Extensão
// Empresa: Digital Conect
// Sistema de Controle de Ordens de Serviço
// Integrantes: Adson, Cauê, Daiane, Vitor e Wellington

using System;

class Program
{
    const int LIMITE = 100;
    const decimal TAXA_URGENCIA = 0.20m;

    static int quantidade = 0;

    static int[] numeros = new int[LIMITE];
    static string[] clientes = new string[LIMITE];
    static string[] notebooks = new string[LIMITE];
    static string[] defeitos = new string[LIMITE];
    static string[] status = new string[LIMITE];
    static decimal[] valores = new decimal[LIMITE];
    static bool[] urgentes = new bool[LIMITE];

    static void Main()
    {
        int opcao;

        // Menu principal do sistema.
        do
        {
            Console.WriteLine("\n=== DIGITAL CONECT ===");
            Console.WriteLine("1 - Cadastrar ordem");
            Console.WriteLine("2 - Listar ordens");
            Console.WriteLine("3 - Consultar ordem");
            Console.WriteLine("4 - Atualizar status");
            Console.WriteLine("5 - Resumo financeiro");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");

            while (!int.TryParse(Console.ReadLine(), out opcao))
                Console.Write("Digite uma opção válida: ");

            switch (opcao)
            {
                case 1:
                    Cadastrar();
                    break;
                case 2:
                    Listar();
                    break;
                case 3:
                    Consultar();
                    break;
                case 4:
                    Atualizar();
                    break;
                case 5:
                    Resumo();
                    break;
                case 0:
                    Console.WriteLine("Sistema encerrado!");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Pausar();
                    break;
            }

        } while (opcao != 0);
    }

    // Cadastra uma nova ordem de serviço.
    static void Cadastrar()
    {
        if (quantidade >= LIMITE)
        {
            Console.WriteLine("Limite de ordens atingido!");
            Pausar();
            return;
        }

        Console.Write("Nome do cliente: ");
        string cliente = Console.ReadLine() ?? "";

        Console.Write("Modelo do notebook: ");
        string notebook = Console.ReadLine() ?? "";

        Console.Write("Defeito apresentado: ");
        string defeito = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(cliente) ||
            string.IsNullOrWhiteSpace(notebook) ||
            string.IsNullOrWhiteSpace(defeito))
        {
            Console.WriteLine("Preencha todos os campos!");
            Pausar();
            return;
        }

        decimal valor;
        Console.Write("Valor do serviço: R$ ");

        while (!decimal.TryParse(Console.ReadLine(), out valor) || valor <= 0)
            Console.Write("Valor inválido. Digite novamente: R$ ");

        string resposta;
        Console.Write("Atendimento urgente? (S/N): ");
        resposta = (Console.ReadLine() ?? "").Trim().ToUpper();

        while (resposta != "S" && resposta != "N")
        {
            Console.Write("Digite somente S ou N: ");
            resposta = (Console.ReadLine() ?? "").Trim().ToUpper();
        }

        bool urgente = resposta == "S";

        if (urgente)
            valor += valor * TAXA_URGENCIA;

        numeros[quantidade] = quantidade + 1;
        clientes[quantidade] = cliente;
        notebooks[quantidade] = notebook;
        defeitos[quantidade] = defeito;
        valores[quantidade] = valor;
        urgentes[quantidade] = urgente;
        status[quantidade] = "Recebido";

        quantidade++;

        Console.WriteLine("Ordem cadastrada com sucesso!");
        Console.WriteLine($"Número: OS-{quantidade:D3}");
        Console.WriteLine($"Valor: R$ {valor:F2}");
        Pausar();
    }

    // Exibe todas as ordens cadastradas.
    static void Listar()
    {
        if (quantidade == 0)
            Console.WriteLine("Nenhuma ordem cadastrada.");

        for (int i = 0; i < quantidade; i++)
            Exibir(i);

        Pausar();
    }

    // Consulta uma ordem pelo número.
    static void Consultar()
    {
        int indice = Buscar();

        if (indice == -1)
            Console.WriteLine("Ordem não encontrada.");
        else
            Exibir(indice);

        Pausar();
    }

    // Localiza uma ordem no vetor.
    static int Buscar()
    {
        Console.Write("Número da ordem: ");
        int numero;

        if (!int.TryParse(Console.ReadLine(), out numero))
            return -1;

        for (int i = 0; i < quantidade; i++)
            if (numeros[i] == numero)
                return i;

        return -1;
    }

    // Atualiza o status de uma ordem.
    static void Atualizar()
    {
        int indice = Buscar();

        if (indice == -1)
        {
            Console.WriteLine("Ordem não encontrada.");
            Pausar();
            return;
        }

        Console.WriteLine("1 - Em diagnóstico");
        Console.WriteLine("2 - Aguardando aprovação");
        Console.WriteLine("3 - Em conserto");
        Console.WriteLine("4 - Finalizado");
        Console.WriteLine("5 - Cancelado");
        Console.Write("Novo status: ");

        int opcao;
        if (!int.TryParse(Console.ReadLine(), out opcao))
            opcao = 0;

        switch (opcao)
        {
            case 1: status[indice] = "Em diagnóstico"; break;
            case 2: status[indice] = "Aguardando aprovação"; break;
            case 3: status[indice] = "Em conserto"; break;
            case 4: status[indice] = "Finalizado"; break;
            case 5: status[indice] = "Cancelado"; break;
            default:
                Console.WriteLine("Status inválido!");
                Pausar();
                return;
        }

        Console.WriteLine("Status atualizado com sucesso!");
        Pausar();
    }

    // Apresenta o resumo financeiro.
    static void Resumo()
    {
        int finalizadas = 0;
        decimal total = 0;

        for (int i = 0; i < quantidade; i++)
        {
            if (status[i] == "Finalizado")
                finalizadas++;

            if (status[i] != "Cancelado")
                total += valores[i];
        }

        Console.WriteLine($"Ordens cadastradas: {quantidade}");
        Console.WriteLine($"Ordens finalizadas: {finalizadas}");
        Console.WriteLine($"Valor estimado: R$ {total:F2}");
        Pausar();
    }

    // Exibe os dados de uma ordem.
    static void Exibir(int i)
    {
        Console.WriteLine($"\nOS-{numeros[i]:D3}");
        Console.WriteLine($"Cliente: {clientes[i]}");
        Console.WriteLine($"Notebook: {notebooks[i]}");
        Console.WriteLine($"Defeito: {defeitos[i]}");
        Console.WriteLine($"Status: {status[i]}");
        Console.WriteLine($"Urgente: {(urgentes[i] ? "Sim" : "Não")}");
        Console.WriteLine($"Valor: R$ {valores[i]:F2}");
    }

    // Pausa a tela antes de voltar ao menu.
    static void Pausar()
    {
        Console.WriteLine("\nPressione ENTER para continuar...");
        Console.ReadLine();
    }
}