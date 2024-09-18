using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using demoSOA.Models;



namespace demoSOA.Services
{

	public class ProductService
	{
		private readonly HttpClient _httpClient;

		public ProductService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<Product>> GetProductsAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7271/api/product");
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<Product>($"https://localhost:7271/api/product/{id}");
		}

		public async Task AddProductAsync(Product product)
		{
			await _httpClient.PostAsJsonAsync("https://localhost:7271/api/product", product);
		}

		public async Task UpdateProductAsync(int id, Product product)
		{
			await _httpClient.PutAsJsonAsync($"https://localhost:7271/api/product/{id}", product);
		}

		public async Task DeleteProductAsync(int id)
		{
			await _httpClient.DeleteAsync($"https://localhost:7271/api/product/{id}");
		}
	}

	
}
