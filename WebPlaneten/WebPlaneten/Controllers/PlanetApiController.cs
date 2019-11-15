using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPlaneten.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebPlaneten.Controllers
{
    [Route("api/Planet")]
    [ApiController]
    public class PlanetApiController : ControllerBase
    {
        private readonly WebPlanetenContext _context;

        public PlanetApiController(WebPlanetenContext context)
        {
            _context = context;
        }

        // GET: api/PlanetApi
        [HttpGet]
        public async Task<IEnumerable<Planet>> Get()
        {
            return await _context.Planet.Include(x => x.Monde).ToListAsync();
        }

        // GET: api/PlanetApi/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Planet> Get(int id)
        {
            var p = await _context.Planet.FirstOrDefaultAsync(x => x.Id == id);


            return p;
        }

        // POST: api/PlanetApi
        [HttpPost]
        public async Task Post([FromBody] Planet value)
        {

            foreach (var item in value.Monde)
            {
                _context.Entry(item).State = EntityState.Unchanged;
            }
            _context.Planet.Add(value);

            await _context.SaveChangesAsync();
        }

        // PUT: api/PlanetApi/5
        [HttpPut]
        public async Task Put([FromBody] Planet value)
        {
            _context.Planet.Update(value);


            await _context.SaveChangesAsync();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _context.Planet.Remove(await Get(id));
            await _context.SaveChangesAsync();
        }
    }
}
