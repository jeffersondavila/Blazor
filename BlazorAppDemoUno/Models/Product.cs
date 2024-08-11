using System;
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
                string cleanImage = images[i].Trim('[', ']', '\"');

                try
                {
                    // Intenta deserializar en caso de que sea un JSON con array
                    var imageList = JsonSerializer.Deserialize<string[]>(cleanImage);
                    if (imageList != null && imageList.Length > 0)
                    {
                        cleanImage = imageList[0].Trim('\"');
                    }
                }
                catch (JsonException)
                {
                    // Si no es un JSON válido, asumimos que cleanImage ya es una URL y no hacemos nada
                }

                // Actualiza el array images con la URL limpia
                images[i] = cleanImage;
            }

            // Actualiza image con la primera URL limpia de images si está disponible
            if (images.Length > 0)
            {
                image = images[0];
            }
        }

        public bool IsValidImageUrl()
        {
            // Verifica si la URL principal es válida
            return Uri.TryCreate(image, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
