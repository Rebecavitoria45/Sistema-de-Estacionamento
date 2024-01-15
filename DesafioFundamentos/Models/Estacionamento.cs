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
        private List<string> Veiculos = new List<string>();
        private int QuantidaDeVagas = 3;
        private  DateTime HoraSaida;
        private  DateTime HoraEntrada;
        private TimeSpan TempoEstacionado;
        
        public Estacionamento(decimal TaxaEstacionamento)
        {
          if(TaxaEstacionamento<0)
          {
            throw new Exception("O valor da taxa não pode ser negativo");
          }
            this.TaxaEstacionamento = TaxaEstacionamento;
        }
         public void AdicionarVeiculo()
        {
          //verifica se existe vaga para estacionar 
           if(QuantidaDeVagas > 0)
           {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine().ToUpper();
          
          //verifica se o carro já esta cadastrado
           if(Veiculos.Contains(placa))
           { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Placa já esta cadastrada no sistema.");
                Console.ResetColor();
                AdicionarVeiculo();
            }
            //validação da placa
             else if(Validarplaca(placa))
           {
             Veiculos.Add(placa);
             HoraEntrada = DateTime.Now;
             QuantidaDeVagas--;
             Console.ForegroundColor = ConsoleColor.Green;
             Console.WriteLine("Carro cadastrado com sucesso!");
             Console.ResetColor();
             Console.WriteLine($"Há {QuantidaDeVagas} vagas disponiveis no estacionamento.");
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
            if (Veiculos.Contains(placa))
            {
                Veiculos.Remove(placa);
                HoraSaida = DateTime.Now;
                TempoEstacionado = HoraSaida - HoraEntrada;
               
               Console.Clear();
               Console.WriteLine("--------Nota Fiscal---------"); 
               Console.WriteLine("Veículo:             " +placa);
               Console.WriteLine("Tempo estacionado:   " + FormatarTempo(TempoEstacionado));
               Console.WriteLine("Valor total R$:      " + Calculopreco(TempoEstacionado));
                   Console.WriteLine("-----------------"); 
                  
                 Console.ForegroundColor = ConsoleColor.Green;
                 Console.WriteLine("Veículo Removido com Sucesso");
                 Console.ResetColor();
                 QuantidaDeVagas++;
               Console.WriteLine($"Há {QuantidaDeVagas} vagas disponiveis no estacionamento.\n");
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
            if (Veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach(string veiculo in Veiculos )
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
      private bool Validarplaca(string placa)
       {
          string padraoPlaca = "^[A-Z]{3}[A-Z-0-9]{4}$";
          bool validar = Regex.IsMatch(placa, padraoPlaca);
          return validar;
        }
     private decimal Calculopreco(TimeSpan tempoEstacionado)
      {
        if(tempoEstacionado.Minutes <= 10)
      {
        return TaxaEstacionamento;
      }
       else
       {
      const int precoHora = 5;
       return (tempoEstacionado.Seconds/3600)*precoHora + TaxaEstacionamento; 
      }
      }
       private string FormatarTempo (TimeSpan tempoEstacionado)
        {
            return $"{tempoEstacionado.Hours:D2}:{tempoEstacionado.Minutes:D2}:{tempoEstacionado.Seconds:D2}";
        }

   }
}