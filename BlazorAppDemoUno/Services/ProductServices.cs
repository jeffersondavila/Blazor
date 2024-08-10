﻿using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorAppDemoUno
{
	public class ProductServices : IProductServices
	{
		private readonly HttpClient _httpClient;
		private readonly JsonSerializerOptions _serializerOptions;

		public ProductServices(HttpClient cliente)
		{
			this._httpClient = cliente;
			_serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		//Listar productos
		public async Task<List<Product>?> GetProducts()
		{
			var response = await _httpClient.GetAsync("/api/v1/products");
			var content = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<List<Product>>(content, _serializerOptions);
		}

		//Enviar productos
		public async Task PostProducts(Product product)
		{
			var response = await _httpClient.PostAsJsonAsync("/api/v1/products", product);
			var content = await response.Content.ReadAsStringAsync();
		}

		//Eliminar productos
		public async Task DeleteProduct(int id)
		{
			var response = await _httpClient.DeleteAsync($"/api/v1/products/{id}");
			var content = await response.Content.ReadAsStringAsync();
		}
	}

	public interface IProductServices
	{
		Task<List<Product>?> GetProducts();
		Task PostProducts(Product product);
		Task DeleteProduct(int id);
	}
}
