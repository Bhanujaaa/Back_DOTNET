using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : Controller
    {
        private readonly ContactAPIdb dbcontext;
        public OrderController(ContactAPIdb dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrder()

        {
            return Ok(await dbcontext.Orders.ToListAsync());

        }
        [HttpGet]
        [Route("{Cid}/{Pid}")]
        public async Task<IActionResult> GetOrders([FromRoute] int Cid, int Pid)

        {
            var c=await dbcontext.Orders.Where(o => (o.ContactId == Cid && o.ProductId==Pid)).SingleOrDefaultAsync();
            if (c != null)
            {
                Console.WriteLine(DateTime.Now);
                var left= c.ModifiedDateTime.Subtract(DateTime.Now).Seconds;
                if (left > 0)
                {
                    c.timeLeftSec = left;
                }
                else
                {
                    c.timeLeftSec = 0;

                }
                await dbcontext.SaveChangesAsync();
                return Ok(c);
            }
            return NotFound();

            

        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(AddOrderReq addOrderReq)
        { var start= DateTime.Now;
            var end= DateTime.Now.AddSeconds(addOrderReq.timeLeftSec);
            var order = new Order()
            {
                Id = addOrderReq.Id,
                ContactId = addOrderReq.ContactId,
                ProductId = addOrderReq.ProductId,
                CreatedDateTime = DateTime.Now,
                ModifiedDateTime = DateTime.Now.AddSeconds(addOrderReq.timeLeftSec),
                timeLeftSec= addOrderReq.timeLeftSec
            };
            await dbcontext.Orders.AddAsync(order);
            await dbcontext.SaveChangesAsync();
            return Ok(order);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var order = await dbcontext.Orders.FindAsync(id);
            if (order != null)
            {
                dbcontext.Orders.Remove(order);
                await dbcontext.SaveChangesAsync();
                return Ok(order);
            }
            return NotFound();
        }

    }
}
