using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntroSoftwareProj
{
    class Product
    {
        public string id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
    }
    class Program
    {
        static HttpClient client = new HttpClient();
        public void ShowProduct(Product product)
        {
            Console.WriteLine($"song name: {product.name}\n{product.category}\t{product.id}");
        }
        static async Task<Uri> CreateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/products", product);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }
        static async Task<Product> GetProductAsync(string path)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }
        static async Task<Product> UpdateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/products/{product.id}", product);
            response.EnsureSuccessStatusCode();

            product = await response.Content.ReadAsAsync<Product>();
            return product;
        }
        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/products/{id}");
            return response.StatusCode;
        }
        static void Main(string[] args)
        {
            RunAsyc().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://spotify.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                Product product = new Product
                {
                    name = "example",
                    category = "attempt"
                };
                var url = await CreateProductAsync(product);
                Console.WriteLine($"")
            }
        }
    }
}
