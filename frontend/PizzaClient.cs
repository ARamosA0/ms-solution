using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace frontend
{
   public class PizzaClient
   {
      private readonly JsonSerializerOptions options = new JsonSerializerOptions()
      {
         PropertyNameCaseInsensitive = true,
         PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };

      private readonly HttpClient client;
      private readonly ILogger<PizzaClient> _logger;

      public PizzaClient(HttpClient client, ILogger<PizzaClient> logger)
      {
         this.client = client;
         this._logger = logger;
      }

      public async Task<PizzaInfo[]> GetPizzasAsync()
      {
         try {
            var responseMessage = await this.client.GetAsync("http://ip172-19-0-67-cjsuel0gftqg008csm80-5200.direct.labs.play-with-docker.com/pizzainfo");
            
            if(responseMessage!=null)
            {
               var stream = await responseMessage.Content.ReadAsStreamAsync();
               return await JsonSerializer.DeserializeAsync<PizzaInfo[]>(stream, options);
            }
         }
         catch(HttpRequestException ex)
         {
            _logger.LogError(ex.Message);
            throw;
         }
         return new PizzaInfo[] {};

      }
   }
}