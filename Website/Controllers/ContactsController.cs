using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Website.Data;
using Website.Models;

namespace Website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    { private readonly ContactAPIdb dbcontext;
        public static Contact Cust = new Contact();
        private readonly IConfiguration _configuration;
        public ContactsController(ContactAPIdb dbcontext,IConfiguration configuration )
        {
            this.dbcontext = dbcontext;
            this._configuration = configuration;
        }
        [HttpGet,Authorize]
        public async Task<IActionResult> GetContacts()

        {
            return Ok(await dbcontext.Contacts.ToListAsync());
            
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        { var contact = await dbcontext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();

            }
            return Ok(contact);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginContact(Contact Req)

        {
            var c = await dbcontext.Contacts.Where(o => (o.Email == Req.Email && o.Password == Req.Password)).SingleOrDefaultAsync();
            if(c == null)
            {
                return BadRequest("User not found.");
            
        }

            string token = CreateToken(c);

            return Ok(token);
        }

       
        private string CreateToken(Contact contact)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, contact.Name),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred

                );
            var jwt=new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        [HttpPost("register")]
        public async Task<IActionResult> AddContact(Contact addContactReq)
        {
            var contact = new Contact()
            {
                Id = addContactReq.Id,
                Email = addContactReq.Email,
                Name = addContactReq.Name,
                Password = addContactReq.Password
            };
           await dbcontext.Contacts.AddAsync(contact);
            await dbcontext.SaveChangesAsync();
            return Ok(contact);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            var contact = await dbcontext.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbcontext.Remove(contact);
                await dbcontext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

    }
    

}
