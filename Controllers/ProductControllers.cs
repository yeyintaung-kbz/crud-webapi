using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Product 1", Price = 10.99m, Quantity = 5 },
        new Product { Id = 2, Name = "Product 2", Price = 19.99m, Quantity = 3 },
        new Product { Id = 3,   Name = "Product 3", Price = 5.99m, Quantity = 10 }
    };

    private static int _nextId = 4;

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(Products);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetProductById(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product)
    {
        product.Id = _nextId++;
        Products.Add(product);
        return CreatedAtAction(
            nameof(GetProductById),
            new { id = product.Id },
            product
            );
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateProduct(int id, Product updatedProduct)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Quantity = updatedProduct.Quantity;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        Products.Remove(product);
        return NoContent();
    }
    
}

