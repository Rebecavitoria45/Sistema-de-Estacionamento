using System.Diagnostics;
using System.Security;
using System.Text.RegularExpressions;
using Microsoft.Win32.SafeHandles;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();
        private int quantidadevagas = 2;
        
      

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }
         
        public void AdicionarVeiculo()
        {
           
            
            Console.WriteLine("Digite a placa do veículo para estacionar:");
        
            string placas = Console.ReadLine().ToUpper();
           

            if(veiculos.Contains(placas)){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Placa já esta cadastrada no sistema.");
                 Console.ResetColor();
                 AdicionarVeiculo();
            }
            
             else if(Validarplaca(placas)){

                if(quantidadevagas == 0){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não há mais vagas disponiveis no estacionamento.");
                    Console.ResetColor();
                }

                else{
                veiculos.Add(placas);
                quantidadevagas--;
             
              Console.ForegroundColor = ConsoleColor.Green;
              Console.WriteLine("Carro cadastrado com sucesso!");
              Console.ResetColor();

            Console.WriteLine($"Há {quantidadevagas} vagas disponiveis no estacionamento.");
                }
            }

            else{
                 Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Placa não corresponde ao padrão do mercosul.");
                 Console.ResetColor();
                  AdicionarVeiculo();
              
              
            }
           
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            string placas = Console.ReadLine().ToUpper();
            

            // Verifica se o veículo existe
            if (veiculos.Contains(placas))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                
                int horas = int.Parse(Console.ReadLine());;
                decimal valorTotal = precoInicial+(precoPorHora*horas); 

                veiculos.Remove(placas);
               
                
                 quantidadevagas++;
             
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"O veículo {placas} foi removido e o preço total foi de: R$ {valorTotal}");
                Console.ResetColor();

                 Console.WriteLine($"Há {quantidadevagas} vagas disponiveis no estacionamento.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                Console.ResetColor();
                RemoverVeiculo();
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                
                 Console.ForegroundColor = ConsoleColor.Yellow;
                foreach(string veiculo in veiculos ){
                    Console.WriteLine(veiculo);
                }
                Console.ResetColor();
            }
            else
            {   
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Não há veículos estacionados.");
                Console.ResetColor();
            }
        }

        public bool Validarplaca(string placas){
          
          string padraoPlaca = "^[A-Z]{3}[A-Z-0-9]{4}$";
          bool validar = Regex.IsMatch(placas, padraoPlaca);

         return validar;
        }
    }
}