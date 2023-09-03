using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API;
using EmailConfigReader;

class Program
{
    static HttpClient client = new HttpClient();
    static double sellPrice;
    static double buyPrice;
    static string? ticker;

    static async Task Main(string[] args)
    {   
        if (args.Length == 3) 
        {
            ticker = args[0];
            SetBuyAndSellPrice(double.Parse(args[1]), double.Parse(args[2]), out buyPrice, out sellPrice); 

            while (true) 
            {
            double RMP = await API.ApiRequest.GetRegularMarketPrice(ticker);
            SendEmail (RMP, sellPrice, buyPrice, ticker);

            await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
        else 
        {
            Console.WriteLine("Entrada inválida!");
        }
    }

    static void SetBuyAndSellPrice(double valor1, double valor2, out double buyPrice, out double sellPrice) 
    {
        if (valor1 > valor2) 
        {
            sellPrice = valor1;
            buyPrice = valor2; 
        }
        else if (valor1 < valor2) 
        {
            sellPrice = valor2;
            buyPrice = valor1;
        }
        else 
        {
            throw new ArgumentException("Erro na entrada dos argumentos!");
        }
    }
    static void SendEmail (double rmp, double sellprice, double buyprice, string ticker) 
    {
            double percent1 = (buyprice - rmp) * 100 / buyprice;
            double percent2 = (rmp - sellprice) * 100 / sellprice; 
            int decimalPlaces = 2;
            double percentFinal1 = Math.Abs(Math.Round(percent1, decimalPlaces));
            double percentFinal2 = Math.Abs(Math.Round(percent2, decimalPlaces));
            string emailBody1 = $"A cotação do ativo {ticker} ({rmp}) caiu {percentFinal1}% do preço {buyprice} de compra";            
            string emailBody2 = $"A cotação do ativo {ticker} ({rmp}) subiu {percentFinal2}% do preço {sellprice} de venda";        

        if (rmp < buyprice) 
        {
            EmailConfigurations emailConfigs = EmailConfigReader.Email.GetConfig();
            EmailConfigReader.Email.SendEmail(emailConfigs, emailBody1);
        }
        else if (rmp > sellprice) 
        {
            EmailConfigurations emailConfigs = EmailConfigReader.Email.GetConfig();
            EmailConfigReader.Email.SendEmail(emailConfigs, emailBody2); 
        }        
    } 
}

