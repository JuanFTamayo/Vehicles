﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Controllers.API
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly DataContext _context;

        public BrandsController(DataContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            return await _context.Brands.OrderBy(x=> x.Description) .ToListAsync();
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.id)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("ya existe esta Marca");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);

                }

            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);

            }

            
        }

        // POST: api/Brands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _context.Brands.Add(brand);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBrand", new { id = brand.id }, brand);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("ya existe esta Marca");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);

                }

            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);

            }

            
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
