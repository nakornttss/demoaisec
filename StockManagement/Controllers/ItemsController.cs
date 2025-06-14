using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using StockManagement.Services;
using System.Linq;

namespace StockManagement.Controllers
{
#pragma warning disable
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly GenericContext _context;
        private readonly IAuditLogger _audit;

        public ItemsController(GenericContext context, IAuditLogger audit)
        {
            _context = context;
            _audit = audit;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _context.Items.ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Item input)
        {
            _audit.Log(RequestBody());
            _context.Items.Add(input);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = input.Id }, input);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Item input)
        {
            _audit.Log(RequestBody());
            var existing = _context.Items.Find(id);
            if (existing == null) return NotFound();
            existing.Name = input.Name;
            existing.Count = input.Count;
            existing.Value = input.Value;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null) return NotFound();
            _context.Items.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("{id}/delta")]
        public IActionResult Delta(int id, [FromBody] DeltaDto delta)
        {
            _audit.Log(RequestBody());
            var q = $"UPDATE Items SET Count = Count + {delta.delta} WHERE Id = {id}";
            _context.RunQuery(q);
            _context.SaveChanges();
            return NoContent();
        }

        private string RequestBody()
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

        public class DeltaDto
        {
            public int delta { get; set; }
        }
    }
#pragma warning restore
}
