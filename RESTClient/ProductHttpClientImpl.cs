using System.Text;
using System.Text.Json;
using Contracts;
using Entities;

namespace RESTClient;

public class ProductHttpClientImpl : IProductService
{
    public async Task<IList<Product>> GetProductsAsync()
    {
        using HttpClient client = new ();
        HttpResponseMessage response = await client.GetAsync("http://localhost:9292/products");
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
        HttpResponseMessage response = await client.GetAsync($"http://localhost:9292/id/{id}/product");
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
        
        HttpResponseMessage response = await client.PostAsync($"http://localhost:9292/product/add",usercontent);
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
        HttpResponseMessage response = await client.GetAsync($"http://localhost:9292/id/{id}/product/remove");
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
        
        HttpResponseMessage response = await client.PatchAsync($"http://localhost:9292/product/update",postcontent);
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
    
    public async Task AddProductToCart(Product product, int orderId)
    {
        using HttpClient client = new ();
        //CamelCase for application 
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        string UserAsJson = JsonSerializer.Serialize(product,options);

        StringContent usercontent = new(UserAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await client.PostAsync($"http://localhost:9292/product/{orderId}/addtocart",usercontent);
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
        //Returned order for debugging 
        Order returned = JsonSerializer.Deserialize<Order>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        Console.WriteLine("AddProductToCart returned: " + returned); //Console line
    }
}