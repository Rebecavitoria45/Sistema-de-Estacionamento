using System.Drawing;
using DesafioFundamentos.Models;

// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;

decimal TaxaEstacionamento = 0;


Console.WriteLine("Seja bem vindo ao sistema de estacionamento!\n" +
                  "Digite a taxa do estacionamento:");
TaxaEstacionamento = Convert.ToDecimal(Console.ReadLine());




// Instancia a classe Estacionamento, já com os valores obtidos anteriormente
Estacionamento es = new Estacionamento(TaxaEstacionamento);

string opcao = string.Empty;
bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Encerrar");

    switch (Console.ReadLine())
    {
        case "1":
            es.AdicionarVeiculo();
            break;

        case "2":
            es.RemoverVeiculo();
            
            break;

        case "3":
            es.ListarVeiculos();
            break;

        case "4":
            exibirMenu = false;
            break;

        default:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Opção inválida");
            Console.ResetColor();
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("O programa se encerrou");
 Console.ResetColor();

