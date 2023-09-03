using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

namespace API
{
    class ApiRequest
    {   
        static HttpClient client = new HttpClient();

        public static async Task <double> GetRegularMarketPrice(string assetSymbol)
        {
            string baseApiUrl = "https://brapi.dev/api/quote"; 
            string accessToken = "3Up8nF5vLyCc4ZVpr5ie7c"; 

            string apiUrl = $"{baseApiUrl}/{assetSymbol}?token={accessToken}";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                ApiResponse? apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseBody);

                if (apiResponse != null && apiResponse.results != null && apiResponse.results.Length > 0)
                {
                    double regularMarketPrice = apiResponse.results[0].regularMarketPrice;
                    return regularMarketPrice;
                }
                else
                {
                    throw new ArgumentException("Resposta JSON inválida ou vazia.");
                }
            }
            else
            {
                throw new ArgumentException("Erro na requisição: " + response.StatusCode);
            }
        }
    }
    public class ApiResponse
    {
        public Result[]? results { get; set; }
    }

    public class Result
    {
        public double regularMarketPrice { get; set; }
    }
}
