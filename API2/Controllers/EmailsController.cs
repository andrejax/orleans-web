using Orleans;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Grains;
using System;

namespace Api.Controllers
{
    public class EmailsController : ControllerBase
    {
        [HttpGet, Route("{email}")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            try
            {
                var status = await EmailServices.CheckEmail(email);
                if (status)
                    return Ok("Email is in the list!");
                
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost, Route("{email}")]
        public async Task<IActionResult> Post(string email)
        {
            try
            {
                var status = await EmailServices.CreateEmail(email);
                if (status)
                    return Created("", email);
                
                return Conflict();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
