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
        
            string placas = Console.ReadLine();
            string placasMaiuscula = placas.ToUpper();

            if(veiculos.Contains(placasMaiuscula)){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Placa já esta cadastrada no sistema.");
                 Console.ResetColor();
            }
            
             else if(Validarplaca(placasMaiuscula)){

                if(quantidadevagas == 0){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não há mais vagas disponiveis no estacionamento.");
                    Console.ResetColor();
                }

                else{
                veiculos.Add(placasMaiuscula);
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
            }
           
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            string placas = Console.ReadLine();
            string placasMaiuscula = placas.ToUpper();

            // Verifica se o veículo existe
            if (veiculos.Contains(placasMaiuscula))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                
                int horas = int.Parse(Console.ReadLine());;
                decimal valorTotal = precoInicial+(precoPorHora*horas); 

                veiculos.Remove(placasMaiuscula);
                
                 quantidadevagas++;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"O veículo {placasMaiuscula} foi removido e o preço total foi de: R$ {valorTotal}");
                Console.ResetColor();

                 Console.WriteLine($"Há {quantidadevagas} vagas disponiveis no estacionamento.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                Console.ResetColor();
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

        public bool Validarplaca(string placasMaiuscula){
          
          string padraoPlaca = "^[A-Z]{3}[A-Z-0-9]{4}$";
          bool validar = Regex.IsMatch(placasMaiuscula, padraoPlaca);

         return validar;
        }
    }
}