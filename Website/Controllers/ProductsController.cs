using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ContactAPIdb dbcontext;
        public ProductsController(ContactAPIdb dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()

        {
            return Ok(await dbcontext.Products.ToListAsync());

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await dbcontext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();

            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(Models.Product addProductReq)
        {
            var product = new Product()
            {
                Id = addProductReq.Id,
                
                PName = addProductReq.PName,
                PCost = addProductReq.PCost
            };
            await dbcontext.Products.AddAsync(product);
            await dbcontext.SaveChangesAsync();
            return Ok(product);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            var product = await dbcontext.Products.FindAsync(id);
            if (product != null)
            {
                dbcontext.Remove(product);
                await dbcontext.SaveChangesAsync();
                return Ok(product);
            }
            return NotFound();
        }

    }
}
