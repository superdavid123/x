using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
class Program
{
	static async Task Main()
	{
		using var httpClient = new HttpClient();

		while (true)
		{

			var response = await httpClient.GetAsync("https://api.exchangerate-api.com/v4/latest/EUR");
			var jsonData = await response.Content.ReadAsStringAsync();
			using JsonDocument jsonDokument = JsonDocument.Parse(jsonData);
			JsonElement jsonObjekt = jsonDokument.RootElement;
			decimal czk = jsonObjekt.GetProperty("rates").GetProperty("CZK").GetDecimal();
			decimal eur = jsonObjekt.GetProperty("rates").GetProperty("EUR").GetDecimal();
			decimal usd = jsonObjekt.GetProperty("rates").GetProperty("USD").GetDecimal();
			Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Získaná data: CZK:{czk}, EUR:{eur}, USD:{usd}");
			await Task.Delay(50);
		}
	}
}
