using System.Text.Json;

namespace BlazorAppDemoUno
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public decimal? price { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public string[] images { get; set; }
        public string image { get; set; }

        public void CleanAndFixImageUrls()
        {
            for (int i = 0; i < images.Length; i++)
            {
                try
                {
                    var imageList = JsonSerializer.Deserialize<string[]>(images[i]);

                    // Reemplazar el string original con la URL correcta
                    if (imageList != null && imageList.Length > 0)
                    {
                        images[i] = imageList[0].Trim('\"');
                    }
                }
                catch (JsonException)
                {
                    // En caso de que no sea un JSON válido, no hacer nada
                }
            }
        }
    }
}
