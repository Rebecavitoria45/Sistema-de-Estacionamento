using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using Microsoft.Win32.SafeHandles;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal TaxaEstacionamento = 0;
        private List<string> veiculos = new List<string>();
        private int quantidadevagas = 3;
        private  DateTime horaSaida;
        private  DateTime horaEntrada;
        private TimeSpan tempoEstacionado;
        
        public Estacionamento(decimal TaxaEstacionamento)
        {
            this.TaxaEstacionamento = TaxaEstacionamento;
        }
         public void AdicionarVeiculo()
        {
            //verifica se existe vaga para estacionar 
           if(quantidadevagas > 0)
           {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine().ToUpper();
          
           //verifica se o carro já esta cadastrado
           if(veiculos.Contains(placa)){ 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Placa já esta cadastrada no sistema.");
                Console.ResetColor();
                AdicionarVeiculo();
            }
           
            //validação da placa
             else if(Validarplaca(placa))
           {
             veiculos.Add(placa);
             horaEntrada = DateTime.Now;
             quantidadevagas--;
        
             Console.ForegroundColor = ConsoleColor.Green;
             Console.WriteLine("Carro cadastrado com sucesso!");
             Console.ResetColor();

             Console.WriteLine($"Há {quantidadevagas} vagas disponiveis no estacionamento.");
            }
             //placa fora do padrão
             else 
             {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Placa não corresponde ao padrão do mercosul.");
                Console.ResetColor();
                AdicionarVeiculo(); //tente novamente
               } 
          }
          
          //sem quantidade de vaga no estacionamento
           else
         {
              Console.ForegroundColor = ConsoleColor.Red;
             Console.WriteLine("Não há mais vagas disponiveis no estacionamento.");
             Console.ResetColor();
            }
         }
          public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = Console.ReadLine().ToUpper();
            
        // Verifica se o veículo existe
            if (veiculos.Contains(placa))
            {
                veiculos.Remove(placa);
                horaSaida = DateTime.Now;
                tempoEstacionado = horaSaida - horaEntrada;
               
               
               Console.Clear();
               Console.WriteLine("--------Nota Fiscal---------"); 
               Console.WriteLine("Veículo:             " +placa);
               Console.WriteLine("Tempo estacionado:   " + FormatarTempo(tempoEstacionado));
               Console.WriteLine("Valor total R$:      " + Calculopreco(tempoEstacionado));
                   Console.WriteLine("-----------------"); 
                  
                 Console.ForegroundColor = ConsoleColor.Green;
                 Console.WriteLine("Veículo Removido com Sucesso");
                 Console.ResetColor();
                 quantidadevagas++;
             
             Console.WriteLine($"Há {quantidadevagas} vagas disponiveis no estacionamento.\n");
             }
             // veículo não cadastrado
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
               
                foreach(string veiculo in veiculos )
                {
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
        //validação de placa usando expressões regulares
       public bool Validarplaca(string placa)
       {
          string padraoPlaca = "^[A-Z]{3}[A-Z-0-9]{4}$";
          bool validar = Regex.IsMatch(placa, padraoPlaca);
          return validar;
        }
      decimal Calculopreco(TimeSpan tempoEstacionado)
      {
        if(tempoEstacionado.Minutes <= 10){
        return TaxaEstacionamento;
      }
       else
       {
       int precoHora = 5;
       return Math.Round(TaxaEstacionamento + (tempoEstacionado.Minutes/60) * precoHora, 2); 
      }
      }
      private string FormatarTempo (TimeSpan tempoEstacionado)
        {
            return $"{tempoEstacionado.Hours:D2}:{tempoEstacionado.Minutes:D2}:{tempoEstacionado.Seconds:D2}";
        }

   }
}