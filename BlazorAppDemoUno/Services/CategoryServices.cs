using System.Text.Json;

namespace BlazorAppDemoUno
{
	public class CategoryServices : ICategoryServices
	{
		private readonly HttpClient _httpClient;
		private readonly JsonSerializerOptions _serializerOptions;

		public CategoryServices(HttpClient cliente)
		{
			this._httpClient = cliente;
			_serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		}

		//Listar categorias
		public async Task<List<Category>> GetCategories()
		{
			var response = await _httpClient.GetAsync("/api/v1/categories");
			var content = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<List<Category>>(content, _serializerOptions);
		}
	}

	public interface ICategoryServices
	{
		Task<List<Category>> GetCategories();
	}
}
