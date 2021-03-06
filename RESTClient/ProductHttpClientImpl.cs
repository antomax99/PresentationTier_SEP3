using System.Text;
using System.Text.Json;
using Contracts;
using Entities;

namespace RESTClient;

public class ProductHttpClientImpl : IProductService
{
    private readonly int APPLICATION_IP=8080;
    public async Task<IList<Product>> GetProductsAsync()
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync($"http://localhost:{APPLICATION_IP}/products");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        IList<Product> products = JsonSerializer.Deserialize<IList<Product>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        Console.WriteLine("GetProductsAsync returned: " + products); //Console line
        return products;
    }

    public async Task<Product> GetProductById(int id)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync($"http://localhost:{APPLICATION_IP}/product/{id}/retrieve");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        Product product = JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        Console.WriteLine("GetProductById returned: " + product); //Console line
        return product;
    }

    public async Task AddProductAsync(Product product)
    {
        using HttpClient client = new ();
        //CamelCase for application 
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        string UserAsJson = JsonSerializer.Serialize(product,options);

        StringContent usercontent = new(UserAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync($"http://localhost:{APPLICATION_IP}/product/add",usercontent);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        Product returned = JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        Console.WriteLine("AddProductAsync returned: " + returned); //Console line
    }

    public async Task DeleteProductAsync(int id)
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.DeleteAsync($"http://localhost:{APPLICATION_IP}/product/{id}/delete");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
    }

    public async Task UpdateProductAsync(Product product)
    {
        using HttpClient client = new ();
        //CamelCase for application 
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        string productAsJson = JsonSerializer.Serialize(product,options);
        StringContent postcontent = new(productAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PutAsync($"http://localhost:{APPLICATION_IP}/product/update",postcontent);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        Product returned = JsonSerializer.Deserialize<Product>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        Console.WriteLine("UpdateProductAsync returned: " + returned); //Console line
    }
    
}