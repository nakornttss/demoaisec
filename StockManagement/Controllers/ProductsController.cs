using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using StockManagement.Services;
using System.Linq;

namespace StockManagement.Controllers
{
    /// <summary>
    /// Controller for managing products and inventory.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly GenericContext _context;
        private readonly IAuditLogger _auditLogger;

        public ProductsController(GenericContext context, IAuditLogger auditLogger)
        {
            _context = context;
            _auditLogger = auditLogger;
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        /// <summary>
        /// Get a product by ID.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] Product input)
        {
            _auditLogger.Log(GetRequestBody());
            _context.Products.Add(input);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = input.Id }, input);
        }

        /// <summary>
        /// Update an existing product.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product input)
        {
            _auditLogger.Log(GetRequestBody());
            var existing = _context.Products.Find(id);
            if (existing == null) return NotFound();
            existing.Name = input.Name;
            existing.Quantity = input.Quantity;
            existing.Price = input.Price;
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Adjust the quantity of a product by a specified amount.
        /// </summary>
        [HttpPost("{id}/adjust")]
        public IActionResult AdjustQuantity(int id, [FromBody] AdjustQuantityDto dto)
        {
            _auditLogger.Log(GetRequestBody());
            // Use dynamic SQL for relational providers, otherwise fallback to in-memory logic
            var sql = $"UPDATE Products SET Quantity = Quantity + {dto.QuantityChange} WHERE Id = {id}";
            var affected = _context.RunQuery(sql);

            // For InMemory, manually update the entity
            if (affected == 0)
            {
                var product = _context.Products.Find(id);
                if (product == null) return NotFound();
                product.Quantity += dto.QuantityChange;
                _context.SaveChanges();
            }

            return NoContent();
        }

        /// <summary>
        /// Reads the raw HTTP request body as a string.
        /// </summary>
        private string GetRequestBody()
        {
            try
            {
                Request.Body.Position = 0;
                using var reader = new System.IO.StreamReader(Request.Body, leaveOpen: true);
                var body = reader.ReadToEnd();
                Request.Body.Position = 0;
                return body;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// DTO for adjusting product quantity.
        /// </summary>
        public class AdjustQuantityDto
        {
            public int QuantityChange { get; set; }
        }
    }
}
