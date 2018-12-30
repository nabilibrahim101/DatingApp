using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    //GET http://localhost:5000/api/values/5
    // By default the kestrel web server listen to port 5000
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // to get data from database, we need to inject dataContext into this class (or into our controller)
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }
        // GET api/values
        [HttpGet]
        
        public async Task<IActionResult> GetValues() // we change this to return IActionResult, and this can return an http response to the client, so we can return and ok (200 response)
        {
            //throw new Exception("Test Exception");

            var values = await _context.Values.ToListAsync();

            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            // x in this case represents the (value) we are returning
            var value = await _context.Values.FirstOrDefaultAsync (x => x.Id == id);
            
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
